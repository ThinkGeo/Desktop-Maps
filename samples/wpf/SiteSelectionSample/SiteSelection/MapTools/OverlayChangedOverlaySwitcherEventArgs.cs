using System;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public class OverlayChangedOverlaySwitcherEventArgs : EventArgs
    {
        private bool cancel;
        private Overlay overlay;

        public OverlayChangedOverlaySwitcherEventArgs()
        { }

        public OverlayChangedOverlaySwitcherEventArgs(Overlay overlay)
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