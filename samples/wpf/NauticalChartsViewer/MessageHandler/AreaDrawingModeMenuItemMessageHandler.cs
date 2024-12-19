﻿using System;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace NauticalChartsViewer
{
    internal class AreaDrawingModeMenuItemMessageHandler : MenuItemMessageHandler
    {
        private const string chartsOverlayName = "ChartsOverlay";

        public async override void Handle(Window owner, MapView map, MenuItemMessage message)
        {
            Globals.CurrentBoundaryDisplayMode = (NauticalChartsBoundaryDisplayMode)Enum.Parse(typeof(NauticalChartsBoundaryDisplayMode), message.MenuItem.Action, true);
            if (map.Overlays.Contains(chartsOverlayName))
            {
                LayerOverlay chartsOverlay = map.Overlays[chartsOverlayName] as LayerOverlay;
                foreach (var item in chartsOverlay.Layers)
                {
                    NauticalChartsFeatureLayer maritimeFeatureLayer = item as NauticalChartsFeatureLayer;
                    maritimeFeatureLayer.BoundaryDisplayMode = Globals.CurrentBoundaryDisplayMode;
                }
                await map.RefreshAsync();
            }
        }

        public override string[] Actions
        {
            get { return new[] { "Plain", "Symbolized" }; }
        }
    }
}
