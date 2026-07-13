using System;
using System.Threading;
using System.Threading.Tasks;

namespace ThinkGeo.UI.Wpf.Serialization.UnitTest;

// Runs each test body on a fresh STA thread. WPF types (MapView, Canvas, Control) require an
// STA apartment on the construction thread; xUnit's default runner is MTA.
internal static class StaRunner
{
    public static void Run(Func<Task> body)
    {
        Exception? captured = null;
        var thread = new Thread(() =>
        {
            try
            {
                body().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                captured = ex;
            }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.IsBackground = true;
        thread.Start();
        thread.Join();
        if (captured != null)
        {
            throw captured;
        }
    }
}
