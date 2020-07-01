using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to selectively style features using a FilterStyle
    /// </summary>
    public partial class CreateFilterStyleSample : UserControl
    {
        public CreateFilterStyleSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay. Also, project and style the Frisco Crime layer
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10780196.9469504, 3916119.49665258, -10776231.7761301, 3912703.71697007);

            ShapeFileFeatureLayer friscoCrimeLayer = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Frisco_Crime.shp");

            // Project the layer's data to match the projection of the map
            friscoCrimeLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add friscoCrimeLayer to a LayerOverlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(friscoCrimeLayer);

            AddFilterStyle(friscoCrimeLayer);

            // Add layerOverlay to the mapView
            mapView.Overlays.Add(layerOverlay);
        }

        /// <summary>
        /// Adds a filter style to various categories of the Frisco Crime layer
        /// </summary>
        private void AddFilterStyle(ShapeFileFeatureLayer layer)
        {
            // Create a filter style based on the "Drugs" Offense Group 
            var drugFilterStyle = new FilterStyle()
            {
                Conditions = { new FilterCondition("OffenseGro", "Drugs") },
                Styles = {
                    new PointStyle(PointSymbolType.Circle, 28, GeoBrushes.White,GeoPens.Red),
                    new PointStyle(new GeoImage(@"../../../Resources/drugs_icon.png")) { ImageScale = .60 }
                }
            };

            // Create a filter style based on the "Weapons" Offense Group 
            var weaponFilterStyle = new FilterStyle()
            {
                Conditions = { new FilterCondition("OffenseGro", "Weapons") },
                Styles = {
                    new PointStyle(PointSymbolType.Circle, 28, GeoBrushes.White,GeoPens.Red),
                    new PointStyle(new GeoImage(@"../../../Resources/weapon_icon.png")) { ImageScale = .25 }
                }
            };

            // Create a filter style based on the "Vandalism" Offense Group 
            var vandalismFilterStyle = new FilterStyle()
            {
                Conditions = { new FilterCondition("OffenseGro", "Vandalism") },
                Styles = {
                    new PointStyle(PointSymbolType.Circle, 28, GeoBrushes.White,GeoPens.Red),
                    new PointStyle(new GeoImage(@"../../../Resources/vandalism_icon.png")) { ImageScale = .25 }
                }
            };

            // Add the filter styles to the CustomStyles collection
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(drugFilterStyle);
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(weaponFilterStyle);
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(vandalismFilterStyle);
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }
    }
}
