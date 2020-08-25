using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to union shapes into a single shape
    /// </summary>
    public partial class UnionShapesSample : UserControl, IDisposable
    {
        public UnionShapesSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay. Also, add the dividedCityLimits and unionLayer layers into a grouped LayerOverlay and display them on the map.
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            ShapeFileFeatureLayer dividedCityLimits = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/FriscoCityLimitsDivided.shp");
            InMemoryFeatureLayer unionLayer = new InMemoryFeatureLayer();
            LayerOverlay layerOverlay = new LayerOverlay();

            // Project dividedCityLimits layer to Spherical Mercator to match the map projection
            dividedCityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style dividedCityLimits layer
            dividedCityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(128, GeoColors.LightOrange), GeoColors.DimGray, 2);
            dividedCityLimits.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyle.CreateSimpleTextStyle("NAME", "Segoe UI", 12, DrawingFontStyles.Bold, GeoColors.Black, GeoColors.White, 2);
            dividedCityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style unionLayer
            unionLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Green), GeoColors.DimGray, 2);
            unionLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add dividedCityLimits to a LayerOverlay
            layerOverlay.Layers.Add("dividedCityLimits", dividedCityLimits);

            // Add unionLayer to the layerOverlay
            layerOverlay.Layers.Add("unionLayer", unionLayer);

            // Set the map extent to the dividedCityLimits layer bounding box
            dividedCityLimits.Open();
            mapView.CurrentExtent = dividedCityLimits.GetBoundingBox();
            dividedCityLimits.Close();

            // Add LayerOverlay to Map
            mapView.Overlays.Add("layerOverlay",layerOverlay);
        }

        /// <summary>
        /// Unions all the features in the dividedCityLimits layer and displays the results on the map
        /// </summary>
        private void UnionShapes_OnClick(object sender, RoutedEventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];

            ShapeFileFeatureLayer dividedCityLimits = (ShapeFileFeatureLayer)layerOverlay.Layers["dividedCityLimits"];
            InMemoryFeatureLayer unionLayer = (InMemoryFeatureLayer)layerOverlay.Layers["unionLayer"];

            // Query the dividedCityLimits layer to get all the features
            dividedCityLimits.Open();
            var features = dividedCityLimits.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns);
            dividedCityLimits.Close();

            // Union all the features into a single Multipolygon Shape
            var union = AreaBaseShape.Union(features);

            // Add the union shape into unionLayer to display the result.
            // If this were to be a permanent change to the dividedCityLimits FeatureSource, you would modify the underlying data using BeginTransaction and CommitTransaction instead.
            unionLayer.InternalFeatures.Clear();
            unionLayer.InternalFeatures.Add(new Feature(union));

            // Hide the dividedCityLimits layer
            dividedCityLimits.IsVisible = false;

            // Redraw the layerOverlay to see the unioned features on the map
            layerOverlay.Refresh();
        }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

    }
}
