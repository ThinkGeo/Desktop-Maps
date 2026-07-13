using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace ThinkGeo.UI.WinForms.Serialization.UnitTest;

// Runs each test body on a fresh STA thread. ThinkGeo.UI.WinForms.MapView is an ElementHost
// that embeds a WPF visual tree — same STA requirement as the WPF tests.
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
        if (captured != null) ExceptionDispatchInfo.Capture(captured).Throw();
    }
}
