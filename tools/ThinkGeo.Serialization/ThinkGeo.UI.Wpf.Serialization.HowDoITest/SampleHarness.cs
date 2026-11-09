using System;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ThinkGeo.UI.Wpf;
using ThinkGeo.UI.Wpf.Serialization;

namespace ThinkGeo.UI.Wpf.Serialization.HowDoITest;

/// <summary>
/// Hosts a HowDoI <see cref="UserControl"/> in an off-screen <see cref="Window"/>, pumps the
/// dispatcher until its <c>MapView</c> bootstrapping has run, then drives a fixed-point snapshot
/// check: serialize → restore → serialize again, assert the two JSON strings are byte-equal.
/// </summary>
internal static class SampleHarness
{
    /// <summary>
    /// Outcome flavors <see cref="FixedPoint"/> can report. Callers use this to distinguish an
    /// actual snapshot bug (<c>Passed</c> or a failed assertion) from a sample that genuinely
    /// can't be exercised by the harness:
    /// <list type="bullet">
    /// <item><c>SkippedNoMapView</c> — the UserControl has no MapView (reusable subcontrol etc.).</item>
    /// <item><c>SkippedNoBootstrap</c> — overlays are added only by a button click, not on load.</item>
    /// <item><c>SkippedSampleError</c> — the sample itself threw during bootstrap (missing native
    ///   dependency, external server unreachable, etc.) so we never got to exercise the snapshot.</item>
    /// </list>
    /// </summary>
    public enum Outcome { Passed, SkippedNoMapView, SkippedNoBootstrap, SkippedSampleError }

    /// <summary>
    /// Runs the fixed-point check (<c>JSON1 == JSON2</c>) for a sample. When the sample doesn't
    /// host a MapView or doesn't populate overlays during the pump window, the result is a skip —
    /// not a failure — so callers can tell "unsuitable sample" apart from "real regression".
    /// </summary>
    public static (Outcome outcome, string? firstJson, string? secondJson, string? reason) FixedPoint(
        Type sampleType, DispatcherStaRunner.Context ctx,
        TimeSpan? bootstrapTimeout = null)
    {
        var timeout = bootstrapTimeout ?? TimeSpan.FromSeconds(10);

        UserControl sample;
        try
        {
            sample = (UserControl)Activator.CreateInstance(sampleType)!;
        }
        catch (Exception ex) when (IsEnvironmentalFailure(ex))
        {
            // Sample couldn't even instantiate — typically a missing native dependency
            // (e.g. CAD's OdSwig_TD_RootIntegrated_Mgd) or missing auth for a service. Nothing
            // to snapshot here, and it isn't a snapshot-tool bug.
            return (Outcome.SkippedSampleError, null, null,
                $"{sampleType.Name}: bootstrap threw {ex.GetType().Name}: {ex.Message.Split('\n')[0]}");
        }

        // Hide-but-show: use a normal Window (WindowStyle.None + zero opacity keeps it off-screen
        // visually but still gives the UserControl a real HWND + visual tree, which is required
        // for Loaded / SizeChanged to fire).
        var window = new Window
        {
            Content = sample,
            Width = 1024,
            Height = 768,
            WindowStyle = WindowStyle.None,
            ShowInTaskbar = false,
            Opacity = 0,
            Left = -10000,
            Top = -10000,
        };
        window.Show();

        try
        {
            MapView? mapView;
            try { mapView = FindMapView(sample); }
            catch (InvalidOperationException)
            {
                return (Outcome.SkippedNoMapView, null, null,
                    $"{sampleType.Name} is a UserControl without a MapView (likely a reusable subcontrol).");
            }

            // Pump until the sample's MapView has overlays or the timeout elapses.
            var deadline = DateTime.UtcNow + timeout;
            while (DateTime.UtcNow < deadline)
            {
                ctx.Pump(TimeSpan.FromMilliseconds(250));
                if (ctx.EnvironmentalFailure != null) break;
                if (mapView.Overlays.Count > 0) break;
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
            var rebuilt = new MapView();
            MapSerializer.Deserialize(rebuilt, firstJson);
            var secondJson = MapSerializer.Serialize(rebuilt);

            return (Outcome.Passed, firstJson, secondJson, null);
        }
        finally
        {
            // Samples fire-and-forget Map.RefreshAsync() from Map_SizeChanged. Its continuations
            // touch the WPF visual tree; if we tear down before they finish, the threadpool
            // continuation throws a cross-thread InvalidOperationException via Task.ThrowAsync —
            // which NEITHER UnobservedTaskException NOR AppDomain.UnhandledException can suppress,
            // so the test host crashes. Prevent by:
            //   1) cancelling the MapView's token to make RefreshAsync short-circuit,
            //   2) pumping the dispatcher long enough for in-flight threadpool continuations to
            //      observe the cancellation and quietly exit,
            //   3) closing the window and disposing the MapView only after that drain completes.
            try
            {
                TryFindMapView(sample)?.CancellationTokenSource?.Cancel();
            }
            catch { /* best-effort */ }
            ctx.Pump(TimeSpan.FromMilliseconds(1500));

            try { TryFindMapView(sample)?.Dispose(); } catch { /* best-effort */ }
            ctx.Pump(TimeSpan.FromMilliseconds(250));

            window.Close();
            if (sample is IDisposable d) d.Dispose();
        }
    }

    private static MapView? TryFindMapView(DependencyObject root)
    {
        try { return FindMapView(root); } catch { return null; }
    }

    private static bool IsEnvironmentalFailure(Exception ex)
    {
        // Walk the inner-exception chain; XAML constructors wrap real failures in TypeInitializer
        // and TargetInvocation layers. We treat any of the below as "the dev box doesn't have
        // what this sample needs" rather than a snapshot regression.
        for (var cur = ex; cur != null; cur = cur.InnerException)
        {
            if (cur is System.IO.FileNotFoundException) return true;
            if (cur is System.IO.DirectoryNotFoundException) return true;
            if (cur is System.DllNotFoundException) return true;
            if (cur is System.BadImageFormatException) return true;
            if (cur is System.TypeInitializationException) continue; // keep walking
        }
        return false;
    }

    private static MapView FindMapView(DependencyObject root)
    {
        // Walk the logical/visual tree looking for the MapView. Every HowDoI sample has exactly one.
        var queue = new System.Collections.Generic.Queue<DependencyObject>();
        queue.Enqueue(root);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current is MapView mv) return mv;

            var count = System.Windows.Media.VisualTreeHelper.GetChildrenCount(current);
            for (int i = 0; i < count; i++)
            {
                queue.Enqueue(System.Windows.Media.VisualTreeHelper.GetChild(current, i));
            }

            // Visual tree isn't populated until Loaded; fall back to logical children.
            foreach (var child in LogicalTreeHelper.GetChildren(current))
            {
                if (child is DependencyObject dep) queue.Enqueue(dep);
            }
        }
        throw new InvalidOperationException($"No MapView found in sample '{root.GetType().Name}'.");
    }
}
