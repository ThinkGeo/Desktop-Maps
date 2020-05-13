using System;
using ThinkGeo.MapSuite.WinForms;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public class OverlayChangedOverlaySwitcherEventArgs : EventArgs
    {
        private Overlay overlay;
        private bool cancel;

        public OverlayChangedOverlaySwitcherEventArgs()
            : this(null)
        { }

        public OverlayChangedOverlaySwitcherEventArgs(Overlay overlay)
        {
            this.overlay = overlay;
        }

        public Overlay Overlay
        {
            get { return overlay; }
        }

        public bool Cancel
        {
            get { return cancel; }
            set { cancel = value; }
        }
    }
}