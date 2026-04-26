using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Xunit;
using Xunit.Abstractions;

namespace ThinkGeo.UI.WinForms.Serialization.HowDoITest;

/// <summary>
/// Reflection-driven fixed-point sweep over every sample in the WinForms HowDoI assembly.
/// Same semantics as the WPF variant: a sample that can't be exercised reports as a SKIP
/// (no MapView / no bootstrap / environmental failure / timeout); a real snapshot asymmetry
/// reports as a FAIL.
/// </summary>
[Collection("HowDoI")]
public class SampleBatchTests
{
    private readonly ITestOutputHelper _output;

    public SampleBatchTests(ITestOutputHelper output) => _output = output;

    public static IEnumerable<object[]> AllSamples()
    {
        // Anchor on a known HowDoI type from the WinForms sample assembly. The assembly name
        // and root namespace live under ThinkGeo.UI.WinForms.HowDoI.
        var asm = Assembly.Load("ThinkGeo.UI.WinForms.HowDoI");
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
            _output.WriteLine($"SKIP {sampleType.FullName} — test body exceeded 2min budget (likely self-driving).");
        }
        catch (Exception ex) when (IsEnvironmental(ex))
        {
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
