using System;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public class OverlayChangedOverlaySwitcherEventArgs : EventArgs
    {
        private bool cancel;
        private Overlay overlay;

        public OverlayChangedOverlaySwitcherEventArgs()
            : this(null)
        { }

        public OverlayChangedOverlaySwitcherEventArgs(Overlay overlay)
            : base()
        {
            this.overlay = overlay;
        }

        public Overlay Overlay
        {
            get { return overlay; }
            set { overlay = value; }
        }

        public bool Cancel
        {
            get { return cancel; }
            set { cancel = value; }
        }
    }
}