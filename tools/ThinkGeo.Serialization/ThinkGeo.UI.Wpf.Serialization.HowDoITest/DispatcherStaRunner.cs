using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace ThinkGeo.UI.Wpf.Serialization.HowDoITest;

/// <summary>
/// Runs a test body on a fresh STA thread with a live <see cref="Dispatcher"/>, so WPF
/// <c>Loaded</c> / <c>SizeChanged</c> events (which HowDoI samples use to bootstrap their MapView)
/// actually fire. Test bodies receive the dispatcher and a "pump until idle or timeout" helper.
/// </summary>
internal static class DispatcherStaRunner
{
    /// <summary>
    /// Context handed to each test body so it can observe out-of-band dispatcher state —
    /// specifically, environmental failures (missing native deps) reported to
    /// <see cref="Dispatcher.UnhandledException"/> during the sample's bootstrap.
    /// </summary>
    public sealed class Context
    {
        public required Dispatcher Dispatcher { get; init; }
        public required Action<TimeSpan> Pump { get; init; }

        /// <summary>
        /// Set to the first <c>FileNotFoundException</c> / <c>DllNotFoundException</c> /
        /// similar that bubbled through the dispatcher while the body was running. Non-null
        /// means the sample tripped an environmental issue and the body should bail.
        /// </summary>
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

                // Install the WPF synchronization context. Without this, async code inside MapView
                // (e.g. Map.RefreshAsync continuations) falls back to the thread pool and throws
                // cross-thread InvalidOperationException when it tries to touch InternalChildren,
                // crashing the whole test host via Task.ThrowAsync.
                SynchronizationContext.SetSynchronizationContext(
                    new DispatcherSynchronizationContext(dispatcher));

                // When we tear down a sample, Dispose() on MapView disposes the adornment layers'
                // GeoImages while a render pass is still queued on the dispatcher. The queued
                // OnRender then throws ObjectDisposedException, and unhandled dispatcher exceptions
                // surface as the test's own failure even though the fixed-point JSON comparison
                // already succeeded. Swallow them here — the assertion is authoritative, not the
                // post-teardown render fallout.
                dispatcher.UnhandledException += (_, e) =>
                {
                    // Swallow post-teardown render faults (see above).
                    if (e.Exception is ObjectDisposedException) { e.Handled = true; return; }

                    // Some samples touch native dependencies from their dispatcher-driven bootstrap
                    // (e.g. DisplayCADFile hits ODA's OdSwig_TD_RootIntegrated_Mgd only after the
                    // layer is opened during Map_SizeChanged). On dev boxes without the native DLL
                    // installed that throws FileNotFoundException / DllNotFoundException into
                    // Dispatcher.UnhandledException rather than synchronously at construction.
                    // Treat these as environmental, not snapshot regressions — stash so the caller
                    // can tell the difference and skip cleanly.
                    if (e.Exception is System.IO.FileNotFoundException
                        or System.IO.DirectoryNotFoundException
                        or System.DllNotFoundException
                        or System.BadImageFormatException)
                    {
                        envFailure ??= e.Exception;
                        e.Handled = true;
                    }
                };

                // Drain pending dispatcher work by posting a sentinel at the lowest priority and
                // spinning the frame until it fires (or the caller's timeout elapses).
                Action<TimeSpan> pump = (duration) =>
                {
                    var deadline = DateTime.UtcNow + duration;
                    while (DateTime.UtcNow < deadline)
                    {
                        var frame = new DispatcherFrame();
                        dispatcher.BeginInvoke(
                            DispatcherPriority.ContextIdle,
                            new Action(() => frame.Continue = false));
                        Dispatcher.PushFrame(frame);

                        // If nothing got queued during this tick, break out early — pumping more
                        // wouldn't make anything new happen.
                        if (frame.Continue) break;

                        Thread.Sleep(50);
                    }
                };

                var ctx = new Context { Dispatcher = dispatcher, Pump = pump };

                // Sync the captured envFailure (written by the dispatcher handler) into the
                // context on every pump tick by wrapping the original pump.
                ctx = new Context
                {
                    Dispatcher = dispatcher,
                    Pump = d => { pump(d); ctx!.EnvironmentalFailure = envFailure; },
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
