using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to set the map extent using a variety of different methods.
    /// </summary>
    public partial class ZoomToExtents : IDisposable
    {
        private ShapeFileFeatureLayer _friscoCityBoundary;

        public ZoomToExtents()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map and a shapefile with simple data to work with
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Set the map's unit of measurement to meters(Spherical Mercator)
                MapView.MapUnit = GeographyUnit.Meter;

                // Add Cloud Maps as a background overlay
                var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
                {
                    ClientId = SampleKeys.ClientId,
                    ClientSecret = SampleKeys.ClientSecret,
                    MapType = ThinkGeoCloudVectorMapsMapType.Light,
                    // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                    TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
                };
                MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

                // Load the Frisco data to a layer
                _friscoCityBoundary = new ShapeFileFeatureLayer(@"./Data/Shapefile/City_ETJ.shp")
                {
                    FeatureSource =
                    {
                        // Convert the Frisco shapefile from its native projection to Spherical Mercator, to match the map
                        ProjectionConverter = new ProjectionConverter(2276, 3857)
                    }
                };

                // Style the data so that we can see it on the map
                _friscoCityBoundary.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(16, GeoColors.Blue), GeoColors.DimGray, 2);
                _friscoCityBoundary.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Add Frisco data to a LayerOverlay and add it to the map
                var layerOverlay = new LayerOverlay();
                layerOverlay.TileType = TileType.SingleTile;
                layerOverlay.Layers.Add(_friscoCityBoundary);
                MapView.Overlays.Add(layerOverlay);

                // Set the map extent
                MapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

                // Populate Controls
                _friscoCityBoundary.Open();
                FeatureIds.ItemsSource = _friscoCityBoundary.FeatureSource.GetFeatureIds();
                _friscoCityBoundary.Close();
                FeatureIds.SelectedIndex = 0;

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Zoom to a scale programmatically. Note that the scales are bound by a ZoomLevelSet.
        /// </summary>
        private async void ZoomToScale_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                await MapView.ZoomToScaleAsync(Convert.ToDouble(ZoomScale.Text));
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Set the map extent to fix a layer's bounding box
        /// </summary>
        private async void LayerBoundingBox_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                MapView.CurrentExtent = _friscoCityBoundary.GetBoundingBox();
                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Set the map extent to fix a feature's bounding box
        /// </summary>
        private async void FeatureBoundingBox_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                var feature = _friscoCityBoundary.FeatureSource.GetFeatureById(FeatureIds.SelectedItem.ToString(), ReturningColumnsType.NoColumns);
                MapView.CurrentExtent = feature.GetBoundingBox();
                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Zoom to a lat/lon at a desired scale by converting the lat/lon to match the map's projection
        /// </summary>
        private async void ZoomToLatLon_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                // Create a PointShape from the lat-lon
                var latlonPoint = new PointShape(Convert.ToDouble(Latitude.Text), Convert.ToDouble(Longitude.Text));

                // Convert the lat-lon projection to match the map
                var projectionConverter = new ProjectionConverter(4326, 3857);
                projectionConverter.Open();
                var convertedPoint = (PointShape)projectionConverter.ConvertToExternalProjection(latlonPoint);
                projectionConverter.Close();

                // Zoom to the converted lat-lon at the desired scale
                await MapView.ZoomToAsync(convertedPoint, Convert.ToDouble(LatlonScale.Text));
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}