using System;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace NauticalChartsViewer
{
    internal class PointDrawingModeMenuItemMessageHandler : MenuItemMessageHandler
    {
        private const string chartsOverlayName = "ChartsOverlay";

        public async override void Handle(Window owner, MapView map, MenuItemMessage message)
        {
            Globals.CurrentSymbolDisplayMode = (NauticalChartsSymbolDisplayMode)Enum.Parse(typeof(NauticalChartsSymbolDisplayMode), message.MenuItem.Action, true);
            if (map.Overlays.Contains(chartsOverlayName))
            {
                LayerOverlay chartsOverlay = map.Overlays[chartsOverlayName] as LayerOverlay;
                foreach (var item in chartsOverlay.Layers)
                {
                    NauticalChartsFeatureLayer maritimeFeatureLayer = item as NauticalChartsFeatureLayer;
                    maritimeFeatureLayer.SymbolDisplayMode = Globals.CurrentSymbolDisplayMode;
                }
                await map.RefreshAsync();
            }
        }

        public override string[] Actions
        {
            get { return new[] { "simplified", "paperchart" }; }
        }
    }
}
