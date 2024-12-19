using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace NauticalChartsViewer
{
    internal class LightsMenuItemMessageHandler : MenuItemMessageHandler
    {
        private const string chartsOverlayName = "ChartsOverlay";

        public async override void Handle(Window owner, MapView map, MenuItemMessage message)
        {
            Globals.IsFullLightLineVisible = message.MenuItem.IsChecked;
            if (map.Overlays.Contains(chartsOverlayName))
            {
                LayerOverlay chartsOverlay = map.Overlays[chartsOverlayName] as LayerOverlay;
                foreach (var item in chartsOverlay.Layers)
                {
                    NauticalChartsFeatureLayer maritimeFeatureLayer = item as NauticalChartsFeatureLayer;
                    maritimeFeatureLayer.IsFullLightLineVisible = Globals.IsFullLightLineVisible;
                }
                await map.RefreshAsync();
            }
        }

        public override string[] Actions
        {
            get { return new[] { "lights" }; }
        }
    }
}