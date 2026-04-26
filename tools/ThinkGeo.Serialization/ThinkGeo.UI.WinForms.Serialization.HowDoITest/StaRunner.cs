using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;

namespace ThinkGeo.UI.WinForms.Serialization.HowDoITest;

/// <summary>
/// Runs a test body on a fresh STA thread with both a WPF <see cref="Dispatcher"/> and a
/// <see cref="Application.DoEvents"/>-style message pump. WinForms HowDoI samples are
/// <c>System.Windows.Forms.UserControl</c> subclasses, but they host a <c>MapView</c> that
/// derives from <c>ElementHost</c> — so internally we still need the WPF dispatcher to keep
/// the embedded WPF tree responsive.
/// </summary>
internal static class DispatcherStaRunner
{
    public sealed class Context
    {
        public required Dispatcher Dispatcher { get; init; }
        public required Action<TimeSpan> Pump { get; init; }
        public Exception? EnvironmentalFailure { get; internal set; }
    }

    public static void Run(Action<Context> body, TimeSpan? testTimeout = null)
    {
        var timeout = testTimeout ?? TimeSpan.FromSeconds(30);
        Exception? captured = null;
        Exception? envFailure = null;

        var thread = new Thread(() =>
        {
            try
            {
                var dispatcher = Dispatcher.CurrentDispatcher;

                SynchronizationContext.SetSynchronizationContext(
                    new DispatcherSynchronizationContext(dispatcher));

                // WinForms default: unhandled exceptions in UI-thread event handlers (Form_Load,
                // SizeChanged, etc.) either pop a JIT dialog (if a debugger is attached) or go
                // through Application.ThreadException — they do NOT propagate back to our
                // try/catch. ThrowException mode escalates to AppDomain and crashes the test
                // host. Use CatchException mode + our own ThreadException handler so we can
                // route the sample's own bootstrap errors (GetBoundingBox before Open, etc.)
                // to the SkippedSampleError bucket without crashing or blocking on a dialog.
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                Application.ThreadException += (_, e) =>
                {
                    envFailure ??= e.Exception;
                };

                dispatcher.UnhandledException += (_, e) =>
                {
                    if (e.Exception is ObjectDisposedException) { e.Handled = true; return; }
                    if (e.Exception is System.IO.FileNotFoundException
                        or System.IO.DirectoryNotFoundException
                        or System.DllNotFoundException
                        or System.BadImageFormatException)
                    {
                        envFailure ??= e.Exception;
                        e.Handled = true;
                    }
                };

                // Pump drains BOTH the WinForms message queue and the WPF dispatcher queue.
                // Samples render via ElementHost → WPF, but overlay bootstrap is posted via the
                // WinForms Load event which only fires when WinForms events are pumped.
                void Pump(TimeSpan duration)
                {
                    var deadline = DateTime.UtcNow + duration;
                    while (DateTime.UtcNow < deadline)
                    {
                        Application.DoEvents();

                        var frame = new DispatcherFrame();
                        dispatcher.BeginInvoke(
                            DispatcherPriority.ContextIdle,
                            new Action(() => frame.Continue = false));
                        Dispatcher.PushFrame(frame);

                        if (frame.Continue) break;
                        Thread.Sleep(50);
                    }
                }

                Context ctx = null!;
                ctx = new Context
                {
                    Dispatcher = dispatcher,
                    Pump = d => { Pump(d); ctx.EnvironmentalFailure = envFailure; },
                };

                body(ctx);
            }
            catch (Exception ex)
            {
                captured = ex;
            }
            finally
            {
                Dispatcher.CurrentDispatcher.InvokeShutdown();
            }
        });
        thread.SetApartmentState(ApartmentState.STA);
        thread.IsBackground = true;
        thread.Start();

        if (!thread.Join(timeout))
        {
            throw new TimeoutException($"Test body did not complete within {timeout.TotalSeconds}s.");
        }
        if (captured != null) ExceptionDispatchInfo.Capture(captured).Throw();
    }
}
