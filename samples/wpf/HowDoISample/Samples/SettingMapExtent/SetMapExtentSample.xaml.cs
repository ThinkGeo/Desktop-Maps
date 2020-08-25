using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to set the map extent using a variety of different methods.
    /// </summary>
    public partial class SetMapExtentSample : UserControl, IDisposable
    {
        ShapeFileFeatureLayer friscoCityBoundary;

        public SetMapExtentSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay to show a basic map and a shapefile with simple data to work with
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Load the Frisco data to a layer
            friscoCityBoundary = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/City_ETJ.shp");

            // Convert the Frisco shapefile from its native projection to Spherical Mercator, to match the map
            friscoCityBoundary.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style the data so that we can see it on the map
            friscoCityBoundary.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(16, GeoColors.Blue), GeoColors.DimGray, 2);
            friscoCityBoundary.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add Frisco data to a LayerOverlay and add it to the map
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(friscoCityBoundary);
            mapView.Overlays.Add(layerOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

            // Populate Controls
            friscoCityBoundary.Open();
            featureIds.ItemsSource = friscoCityBoundary.FeatureSource.GetFeatureIds();
            friscoCityBoundary.Close();
            featureIds.SelectedIndex = 0;
        }

        /// <summary>
        /// Zoom to a scale programmatically. Note that the scales are bound by a ZoomLevelSet.
        /// </summary>
        private void ZoomToScale_Click(object sender, RoutedEventArgs e)
        {
            mapView.ZoomToScale(Convert.ToDouble(zoomScale.Text));
        }

        /// <summary>
        /// Set the map extent to fix a layer's bounding box
        /// </summary>
        private void LayerBoundingBox_Click(object sender, RoutedEventArgs e)
        {
            mapView.CurrentExtent = friscoCityBoundary.GetBoundingBox();
            mapView.Refresh();
        }

        /// <summary>
        /// Set the map extent to fix a feature's bounding box
        /// </summary>
        private void FeatureBoundingBox_Click(object sender, RoutedEventArgs e)
        {
            var feature = friscoCityBoundary.FeatureSource.GetFeatureById(featureIds.SelectedItem.ToString(), ReturningColumnsType.NoColumns);
            mapView.CurrentExtent = feature.GetBoundingBox();
            mapView.Refresh();
        }

        /// <summary>
        /// Zoom to a lat/lon at a desired scale by converting the lat/lon to match the map's projection
        /// </summary>
        private void ZoomToLatLon_Click(object sender, RoutedEventArgs e)
        {
            // Create a PointShape from the lat-lon
            var latlonPoint = new PointShape(Convert.ToDouble(latitude.Text), Convert.ToDouble(longitude.Text));

            // Convert the lat-lon projection to match the map
            var projectionConverter = new ProjectionConverter(4326, 3857);
            projectionConverter.Open();
            var convertedPoint = (PointShape)projectionConverter.ConvertToExternalProjection(latlonPoint);
            projectionConverter.Close();

            // Zoom to the converted lat-lon at the desired scale
            mapView.ZoomTo(convertedPoint, Convert.ToDouble(latlonScale.Text));
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
