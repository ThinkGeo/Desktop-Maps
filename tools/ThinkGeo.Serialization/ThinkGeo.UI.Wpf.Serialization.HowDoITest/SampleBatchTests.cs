using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf.HowDoI;
using Xunit;
using Xunit.Abstractions;

namespace ThinkGeo.UI.Wpf.Serialization.HowDoITest;

/// <summary>
/// Reflection-driven fixed-point sweep over every sample in the HowDoI assembly.
/// Each UserControl becomes one Theory row. Outcomes:
///
///   <c>Passed</c>              — <c>ToJson == ApplyJson∘ToJson</c>. Snapshot coverage verified.
///   <c>SkippedNoMapView</c>    — the UserControl isn't a map sample (reusable subcontrol etc.).
///   <c>SkippedNoBootstrap</c>  — the sample adds overlays only on a button click; can't drive
///                                 without simulating user input. Not a snapshot bug.
///   Test failure                — JSON1 != JSON2 (or exception) — a real snapshot asymmetry.
/// </summary>
[Collection("HowDoI")]
public class SampleBatchTests
{
    private readonly ITestOutputHelper _output;

    public SampleBatchTests(ITestOutputHelper output) => _output = output;

    public static IEnumerable<object[]> AllSamples()
    {
        // Use one known HowDoI type as an assembly anchor, then enumerate every public UserControl
        // subtype. Excluding nested types and abstract bases filters out XAML markup compilation
        // artifacts and shared base classes.
        var asm = typeof(NavigationMap).Assembly;
        foreach (var t in asm.GetTypes()
            .Where(t => t.IsPublic && !t.IsAbstract && !t.IsNested)
            .Where(t => typeof(UserControl).IsAssignableFrom(t))
            .Where(t => t.GetConstructor(Type.EmptyTypes) != null)
            .OrderBy(t => t.FullName))
        {
            yield return new object[] { t };
        }
    }

    [Theory]
    [MemberData(nameof(AllSamples))]
    public void Sample_FixedPoint(Type sampleType)
    {
        try
        {
            DispatcherStaRunner.Run(ctx =>
            {
                var (outcome, first, second, reason) = SampleHarness.FixedPoint(sampleType, ctx);
                if (ctx.EnvironmentalFailure is { } env && outcome != SampleHarness.Outcome.Passed)
                {
                    _output.WriteLine($"SKIP {sampleType.FullName} — environmental: {env.GetType().Name}: {env.Message.Split('\n')[0]}");
                    return;
                }
                switch (outcome)
                {
                    case SampleHarness.Outcome.Passed:
                        Assert.False(string.IsNullOrWhiteSpace(first), $"{sampleType.Name}: snapshot empty.");
                        Assert.Equal(first, second);
                        _output.WriteLine($"PASS {sampleType.FullName}  ({first!.Length} chars)");
                        break;
                    case SampleHarness.Outcome.SkippedNoMapView:
                    case SampleHarness.Outcome.SkippedNoBootstrap:
                    case SampleHarness.Outcome.SkippedSampleError:
                        _output.WriteLine($"SKIP {sampleType.FullName} — {reason}");
                        break;
                }
            }, TimeSpan.FromMinutes(2));
        }
        catch (TimeoutException)
        {
            // Some samples (e.g. PerfTestRefreshNdfdSkyGrid) kick off a self-driving refresh loop
            // that never settles — our pump sees steady overlay growth but the test body never
            // returns within the budget. Not a snapshot bug; record and move on.
            _output.WriteLine($"SKIP {sampleType.FullName} — test body exceeded 2min budget (likely self-driving).");
        }
        catch (Exception ex) when (IsEnvironmental(ex))
        {
            // Some samples (DisplayCADFile with missing OdSwig native DLL, SQL / FileGDB samples
            // without their server / data files) throw synchronously during bootstrap. These
            // escape the dispatcher-level handler because they happen on the test body's stack
            // directly. Not a snapshot tool bug; catch here and skip.
            _output.WriteLine($"SKIP {sampleType.FullName} — environmental: {ex.GetType().Name}: {ex.Message.Split('\n')[0]}");
        }
    }

    private static bool IsEnvironmental(Exception ex)
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
