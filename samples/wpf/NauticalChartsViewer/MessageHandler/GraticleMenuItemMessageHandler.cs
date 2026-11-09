using System.Windows;

using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace NauticalChartsViewer
{
    internal class GraticleMenuItemMessageHandler : MenuItemMessageHandler
    {
        private const string GraticuleLayerName = "Graticule";

        public async override void Handle(Window owner, MapView map, MenuItemMessage message)
        {
            if (!map.AdornmentOverlay.Layers.Contains(GraticuleLayerName))
            {
                var graticuleLayer = new GraticuleAdornmentLayer()
                {
                    GraticuleLineStyle = new LineStyle(new GeoPen(GeoColors.Gray))
                };
                // The charts are reprojected to Web Mercator (the map is in meters); tell the
                // graticule so it projects its lat/lon lines to match. Leave it unset (null) when the
                // map is already in decimal degrees.
                if (map.MapUnit == GeographyUnit.Meter)
                {
                    graticuleLayer.Projection = new Projection(3857);
                }
                map.AdornmentOverlay.Layers.Add(GraticuleLayerName, graticuleLayer);
            }

            map.AdornmentOverlay.Layers[GraticuleLayerName].IsVisible = message.MenuItem.IsChecked;

            await map.AdornmentOverlay.RefreshAsync();
        }

        public override string[] Actions
        {
            get { return new[] { "graticule" }; }
        }
    }
}
