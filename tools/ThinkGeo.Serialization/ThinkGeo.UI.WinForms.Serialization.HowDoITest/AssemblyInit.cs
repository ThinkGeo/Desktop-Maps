using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace ThinkGeo.UI.WinForms.Serialization.HowDoITest;

public sealed class AssemblyInit : IDisposable
{
    public AssemblyInit()
    {
        TaskScheduler.UnobservedTaskException += OnUnobserved;

        // WinForms HowDoI samples use relative paths like "../../../Data/SQLite/..." that assume
        // the process's CWD is the HowDoI sample exe's bin folder. Our test process's default
        // CWD is our test bin, so those paths resolve to non-existent folders, layer.Open()
        // silently fails, and the sample's subsequent GetBoundingBox() throws
        // "Layer must be opened". Pin CWD to the HowDoI assembly's bin so relative paths work.
        // (WPF samples use "./Data/..." which works from any CWD that has a Data folder copied —
        // their test bin happens to have one, so this asymmetry only bites WinForms.)
        var howDoIAssemblyDir = Path.GetDirectoryName(
            Assembly.Load("ThinkGeo.UI.WinForms.HowDoI").Location);
        if (!string.IsNullOrEmpty(howDoIAssemblyDir))
        {
            Environment.CurrentDirectory = howDoIAssemblyDir;
        }
    }

    private static void OnUnobserved(object? sender, UnobservedTaskExceptionEventArgs e) => e.SetObserved();

    public void Dispose()
    {
        TaskScheduler.UnobservedTaskException -= OnUnobserved;
    }
}

[Xunit.CollectionDefinition("HowDoI", DisableParallelization = true)]
public sealed class HowDoICollection : Xunit.ICollectionFixture<AssemblyInit> { }
