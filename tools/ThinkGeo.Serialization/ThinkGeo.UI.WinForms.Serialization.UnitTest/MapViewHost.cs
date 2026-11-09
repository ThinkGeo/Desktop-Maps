using System;
using System.Windows.Forms;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.Serialization.UnitTest;

/// <summary>
/// Hosts a fresh <see cref="MapView"/> inside an off-screen <see cref="Form"/> and returns it
/// fully initialized. Required because <c>MapView</c> derives from <c>ElementHost</c>; the
/// ElementHost wires a WPF child tree on top of a Win32 handle, and that handle doesn't get
/// created until the control is parented and shown. Calling <c>mapView.Overlays.Clear()</c>
/// before that produces a <see cref="NullReferenceException"/> from <c>MapViewBase</c>'s
/// ClearingItems handler.
/// </summary>
internal sealed class MapViewHost : IDisposable
{
    private readonly Form _form;
    public MapView Map { get; }

    public MapViewHost()
    {
        Map = new MapView { Dock = DockStyle.Fill };
        _form = new Form
        {
            WindowState = FormWindowState.Minimized,
            ShowInTaskbar = false,
            StartPosition = FormStartPosition.Manual,
            Location = new System.Drawing.Point(-10000, -10000),
            Width = 1024,
            Height = 768,
        };
        _form.Controls.Add(Map);
        _form.Show();
        _form.Hide();
    }

    public void Dispose()
    {
        try { Map.Dispose(); } catch { /* best-effort */ }
        _form.Close();
        _form.Dispose();
    }
}
