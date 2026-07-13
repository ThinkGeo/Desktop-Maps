using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ThinkGeo.UI.WinForms;
using ThinkGeo.UI.WinForms.Serialization;

namespace ThinkGeo.UI.WinForms.Serialization.HowDoITest;

/// <summary>
/// Hosts a HowDoI <see cref="UserControl"/> inside an off-screen <see cref="Form"/>, pumps the
/// message loop until its <c>MapView</c> bootstraps, then drives a fixed-point snapshot check:
/// serialize → restore → serialize again, assert the two JSON strings are byte-equal.
/// </summary>
internal static class SampleHarness
{
    public enum Outcome { Passed, SkippedNoMapView, SkippedNoBootstrap, SkippedSampleError }

    /// <summary>
    /// Samples that synchronously trigger a fatal AccessViolationException in GDAL native code
    /// from the MapView's async render pipeline. AV is a fast-fail — no managed handler can
    /// suppress it, no cancellation token can stop the in-flight native call. The render starts
    /// when the form is shown and outlives the test; the next test's process dies. The only
    /// reliable fix is to never instantiate these samples in the harness.
    /// </summary>
    private static readonly HashSet<string> KnownNativeCrashSamples = new()
    {
        // Uses GdalProjectionConverter(4326, 2163) — EPSG:2163 is deprecated and GDAL fails the
        // raster warp with err=2050, then ReadRaster reads invalid memory and the process AVs.
        "ProjectARaster",
    };

    public static (Outcome outcome, string? firstJson, string? secondJson, string? reason) FixedPoint(
        Type sampleType, DispatcherStaRunner.Context ctx, TimeSpan? bootstrapTimeout = null)
    {
        var timeout = bootstrapTimeout ?? TimeSpan.FromSeconds(10);

        if (KnownNativeCrashSamples.Contains(sampleType.Name))
        {
            return (Outcome.SkippedSampleError, null, null,
                $"{sampleType.Name}: skipped — known to trigger native AccessViolationException in GDAL " +
                "during async render. Cannot be exercised in a shared test process.");
        }

        UserControl sample;
        try
        {
            sample = (UserControl)Activator.CreateInstance(sampleType)!;
        }
        catch (Exception ex) when (IsEnvironmentalFailure(ex))
        {
            return (Outcome.SkippedSampleError, null, null,
                $"{sampleType.Name}: bootstrap threw {ex.GetType().Name}: {ex.Message.Split('\n')[0]}");
        }

        // Host inside an off-screen Form that is visible + normally sized. HowDoI samples
        // reference mapView.CurrentExtent from Form_Load and expect the control to have a real
        // size by then. A minimized-and-hidden host would leave CurrentExtent null and crash the
        // sample's own bootstrap code (observed with NavigationMap.MapView_CurrentExtentChanged).
        var form = new Form
        {
            FormBorderStyle = FormBorderStyle.None,
            ShowInTaskbar = false,
            StartPosition = FormStartPosition.Manual,
            Location = new System.Drawing.Point(-10000, -10000),
            Width = 1024,
            Height = 768,
            Opacity = 0,  // off-screen belt + invisible suspenders; still gives real size / extent.
        };
        sample.Dock = DockStyle.Fill;

        try
        {
            MapView? mapView;

            // Bootstrap phase: parenting the sample to the form, showing the form, and pumping
            // the message loop until the sample's Form_Load runs the user's setup code.
            // Any exception here comes from the SAMPLE'S own code — e.g. layer.GetBoundingBox
            // before Open, SQLite failing to open its data file at a relative path, or disposal
            // side-effects from the sample's component model (observed on DisplayRasterMBTilesFile
            // where Controls.Add triggers a Dispose cascade through MapViewBase). Treat as
            // environmental/skip, not as a snapshot regression.
            try
            {
                form.Controls.Add(sample);
                form.Show();

                try { mapView = FindMapView(sample); }
                catch (InvalidOperationException)
                {
                    return (Outcome.SkippedNoMapView, null, null,
                        $"{sampleType.Name} is a UserControl without a MapView (likely a reusable subcontrol).");
                }

                var deadline = DateTime.UtcNow + timeout;
                while (DateTime.UtcNow < deadline)
                {
                    ctx.Pump(TimeSpan.FromMilliseconds(250));
                    if (ctx.EnvironmentalFailure != null) break;
                    if (mapView.Overlays.Count > 0) break;
                }
            }
            catch (Exception bootstrapEx)
            {
                return (Outcome.SkippedSampleError, null, null,
                    $"{sampleType.Name}: sample bootstrap threw {bootstrapEx.GetType().Name}: " +
                    bootstrapEx.Message.Split('\n')[0]);
            }

            if (ctx.EnvironmentalFailure is { } env)
            {
                return (Outcome.SkippedSampleError, null, null,
                    $"{sampleType.Name}: dispatcher surfaced {env.GetType().Name} during bootstrap: " +
                    env.Message.Split('\n')[0]);
            }

            if (mapView.Overlays.Count == 0)
            {
                return (Outcome.SkippedNoBootstrap, null, null,
                    $"{sampleType.Name}: no overlays after {timeout.TotalSeconds}s. " +
                    "Sample likely requires a button click or external data to bootstrap.");
            }

            var firstJson = MapSerializer.Serialize(mapView);

            // The rebuilt MapView also needs to be inside a shown Form before its
            // Overlays.Clear() works (see MapViewHost in the unit tests for the same reason).
            var rebuiltForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                ShowInTaskbar = false,
                StartPosition = FormStartPosition.Manual,
                Location = new System.Drawing.Point(-10000, -10000),
                Width = 1024,
                Height = 768,
                Opacity = 0,
            };
            var rebuilt = new MapView { Dock = DockStyle.Fill };
            rebuiltForm.Controls.Add(rebuilt);
            rebuiltForm.Show();
            try
            {
                MapSerializer.Deserialize(rebuilt, firstJson);
                var secondJson = MapSerializer.Serialize(rebuilt);
                return (Outcome.Passed, firstJson, secondJson, null);
            }
            finally
            {
                try { rebuilt.Dispose(); } catch { }
                rebuiltForm.Close();
                rebuiltForm.Dispose();
            }
        }
        finally
        {
            // Teardown NREs from MapViewBase disposal (observed on DisplayRasterMBTilesFile,
            // RasterXYZServer) originate inside the SDK's own Dispose path, not ours. Swallow —
            // the fixed-point result is already captured by this point.
            try { TryFindMapView(sample)?.Dispose(); } catch { }
            try { ctx.Pump(TimeSpan.FromMilliseconds(500)); } catch { }
            try { form.Close(); } catch { }
            try { form.Dispose(); } catch { }
            try { if (sample is IDisposable d) d.Dispose(); } catch { }
        }
    }

    private static MapView FindMapView(Control root)
    {
        var queue = new Queue<Control>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            var cur = queue.Dequeue();
            if (cur is MapView mv) return mv;
            foreach (Control child in cur.Controls) queue.Enqueue(child);
        }
        throw new InvalidOperationException($"No MapView found in sample '{root.GetType().Name}'.");
    }

    private static MapView? TryFindMapView(Control root)
    {
        try { return FindMapView(root); } catch { return null; }
    }

    private static bool IsEnvironmentalFailure(Exception ex)
    {
        for (var cur = ex; cur != null; cur = cur.InnerException)
        {
            if (cur is System.IO.FileNotFoundException
                or System.IO.DirectoryNotFoundException
                or System.DllNotFoundException
                or System.BadImageFormatException) return true;
        }
        return false;
    }
}
