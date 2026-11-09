using System;
using System.Threading.Tasks;

namespace ThinkGeo.UI.Wpf.Serialization.HowDoITest;

/// <summary>
/// HowDoI samples routinely fire-and-forget <c>Map.RefreshAsync()</c>. When our harness tears a
/// sample down while that task is still in flight, its continuation may throw against a disposed
/// visual tree on a threadpool worker. Normally .NET would escalate that to the process-level
/// <c>UnhandledException</c> and crash the test host. We mark the exceptions observed instead —
/// the fixed-point JSON check has already happened by the time we're in teardown, so these
/// post-test failures carry no signal.
/// </summary>
public sealed class AssemblyInit : IDisposable
{
    public AssemblyInit()
    {
        TaskScheduler.UnobservedTaskException += OnUnobserved;
    }

    private static void OnUnobserved(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        e.SetObserved();
    }

    public void Dispose()
    {
        TaskScheduler.UnobservedTaskException -= OnUnobserved;
    }
}

// xUnit collection fixture — instantiated once for any test that declares membership.
[Xunit.CollectionDefinition("HowDoI", DisableParallelization = true)]
public sealed class HowDoICollection : Xunit.ICollectionFixture<AssemblyInit> { }
