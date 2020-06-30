using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// TODO: This sample is a Work In Progress and is disabled in the app!
    /// Learn how to snap a shape to a nearby shape for precise placement.
    /// </summary>
    public partial class SnapToShapeSample : UserControl
    {
        private readonly ShapeFileFeatureLayer friscoParks = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Parks.shp");
        private readonly InMemoryFeatureLayer snapLayer = new InMemoryFeatureLayer();
        private readonly LayerOverlay layerOverlay = new LayerOverlay();

        public SnapToShapeSample()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Project friscoParks layer to Spherical Mercator to match the map projection
            friscoParks.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style friscoParks layer
            friscoParks.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
            friscoParks.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style the splitLayer
            snapLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Green), GeoColors.DimGray);
            snapLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add friscoParks to a LayerOverlay
            layerOverlay.Layers.Add(friscoParks);

            // Add splitLayer to the layerOverlay
            layerOverlay.Layers.Add(snapLayer);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10782307.6877106, 3918904.87378907, -10774377.3460701, 3912073.31442403);

            // Add LayerOverlay to Map
            mapView.Overlays.Add(layerOverlay);

            // Add Toyota Stadium feature to stadiumLayer
            var stadium = new Feature(new PointShape(-10779651.500992451, 3915933.0023557912));
            snapLayer.InternalFeatures.Add(stadium);
        }

        /// <summary>
        /// WIP
        /// </summary>
        private void SnapToShape_Click(object sender, RoutedEventArgs e)
        {
            // WIP
        }
    }
}
