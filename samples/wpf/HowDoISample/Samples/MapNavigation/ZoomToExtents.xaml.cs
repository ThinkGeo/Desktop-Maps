using System;
using System.Threading;
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
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;
            MapView.CurrentExtentChanged += MapView_CurrentExtentChanged;

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
            MapView.CenterPoint = new PointShape(-10778000, 3912000);
            MapView.CurrentScale = 180000;

            _ = MapView.RefreshAsync();
        }

        /// <summary>
        /// Zoom in on the map
        /// The same effect can be achieved by using the ZoomPanBar bar on the upper left of the map, double left-clicking on the map, or by using the scroll wheel.
        /// </summary>
        private void ZoomIn_Click(object sender, RoutedEventArgs e)
        {
            _ = MapView.ZoomInAsync();
        }

        /// <summary>
        /// Zoom out on the map
        /// The same effect can be achieved by using the ZoomPanBar bar on the upper left of the map, double right-clicking on the map, or by using the scroll wheel.
        /// </summary>
        private void ZoomOut_Click(object sender, RoutedEventArgs e)
        {
            _ = MapView.ZoomOutAsync();
        }

        /// <summary>
        /// Zoom to a scale programmatically. Note that the scales are bound by a ZoomLevelSet.
        /// </summary>
        private void ZoomToScale_Click(object sender, RoutedEventArgs e)
        {
            _ = MapView.ZoomToAsync(Convert.ToDouble(ZoomScale.Text));
        }

        /// <summary>
        /// Set the map extent to fix a layer's bounding box
        /// </summary>
        private void LayerBoundingBox_Click(object sender, RoutedEventArgs e)
        {
            var friscoCityBoundaryBBox = _friscoCityBoundary.GetBoundingBox();
            MapView.CenterPoint = friscoCityBoundaryBBox.GetCenterPoint();
            MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit,friscoCityBoundaryBBox, MapView.MapWidth, MapView.MapHeight);
            _ = MapView.RefreshAsync();
        }

        /// <summary>
        /// Set the map extent to center at a point
        /// </summary>
        private void CenterAt_Click(object sender, RoutedEventArgs e)
        {
            var pointInMercator = ProjectionConverter.Convert(4326, 3857, new PointShape(-96.82, 33.15));
            _ = MapView.CenterAtAsync(pointInMercator);
        }

        // Register the dependency property.
        public static readonly DependencyProperty TxtCoordinatesProperty =
            DependencyProperty.Register(
                nameof(TxtCoordinates),
                typeof(string),
                typeof(ZoomToExtents),
                null);

        /// <summary>
        /// Gets or sets the text that represents the coordinates.
        /// This is a bindable property.
        /// </summary>
        public string TxtCoordinates
        {
            get => (string)GetValue(TxtCoordinatesProperty);
            set => SetValue(TxtCoordinatesProperty, value);
        }

        private void MapView_CurrentExtentChanged(object sender, CurrentExtentChangedMapViewEventArgs e)
        {
            var center = MapView.CurrentExtent.GetCenterPoint();
            var centerInDecimalDegrees = ProjectionConverter.Convert(3857, 4326, center);
            TxtCoordinates = $"Center Point: (Lat: {centerInDecimalDegrees.Y:N4}, Lon: {centerInDecimalDegrees.X:N4})";
        }

        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private void RotateAngle_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            _ = MapView.ZoomToAsync(MapView.CurrentExtent.GetCenterPoint(), MapView.CurrentScale,
                RotateAngle.Value, _cancellationTokenSource.Token);
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