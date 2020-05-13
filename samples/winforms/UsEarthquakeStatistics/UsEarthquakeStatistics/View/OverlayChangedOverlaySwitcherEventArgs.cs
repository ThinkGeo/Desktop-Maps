﻿using System;
using ThinkGeo.MapSuite.WinForms;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public class OverlayChangedOverlaySwitcherEventArgs : EventArgs
    {
        public OverlayChangedOverlaySwitcherEventArgs()
            : this(null)
        { }

        public OverlayChangedOverlaySwitcherEventArgs(Overlay overlay)
        {
            Overlay = overlay;
        }

        public Overlay Overlay { get; private set; }

        public bool Cancel { get; set; }
    }
}