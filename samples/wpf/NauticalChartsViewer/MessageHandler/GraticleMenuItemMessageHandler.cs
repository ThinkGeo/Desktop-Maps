using System.Windows;

using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace NauticalChartsViewer
{
    internal class GraticleMenuItemMessageHandler : MenuItemMessageHandler
    {
        private const string GraticuleOverlayName = "GraticuleOverlay";

        public async override void Handle(Window owner, MapView map, MenuItemMessage message)
        {
            LayerOverlay adornmentOverlay;
            if (!map.Overlays.Contains(GraticuleOverlayName))
            {
                var graticuleLayer = new GraticuleFeatureLayer()
                {
                    GraticuleLineStyle = new LineStyle(new GeoPen(GeoColors.LightGray))
                };
                adornmentOverlay = new LayerOverlay();
                adornmentOverlay.Layers.Add(graticuleLayer);
                map.Overlays.Add(GraticuleOverlayName, adornmentOverlay);
            }

            adornmentOverlay = map.Overlays[GraticuleOverlayName] as LayerOverlay;
            adornmentOverlay.IsVisible = message.MenuItem.IsChecked;

            await map.RefreshAsync();
        }

        public override string[] Actions
        {
            get { return new[] { "graticule" }; }
        }
    }
}
