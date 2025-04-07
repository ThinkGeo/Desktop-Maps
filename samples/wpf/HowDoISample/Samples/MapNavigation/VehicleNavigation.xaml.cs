using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;
using Point = System.Windows.Point;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to programmatically zoom, pan, and rotate the map control.
    /// </summary>
    public partial class VehicleNavigation
    {
        private Collection<Vertex> _gpsPoints;
        private int _currentGpsPointIndex;
        private Marker _vehicleMarker;
        private readonly List<Vertex> _visitedVertices = new List<Vertex>();
        private InMemoryFeatureLayer _visitedRoutesLayer;
        private bool _disposed;
        private bool _busy; // mark if the map is busy redrawing

        private bool _initialized;
        private CancellationTokenSource _cancellationTokenSource;
        private ThinkGeoCloudRasterMapsOverlay _backgroundOverlay;

        private LayerWpfDrawingOverlay _routesOverlay;
        private SimpleMarkerOverlay _markerOverlay;
        private bool _showOverview = false;


        public VehicleNavigation()
        {
            InitializeComponent();
            this.Unloaded += VehicleNavigation_Unloaded;
        }

        private void VehicleNavigation_Unloaded(object sender, RoutedEventArgs e)
        {
            this._disposed = true;
        }

        private async void MapView_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_initialized)
                return;
            _initialized = true;

            _cancellationTokenSource = new CancellationTokenSource();
            // Add Cloud Maps as a background overlay
            _backgroundOverlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X2,
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_raster_light")
            };
            MapView.Overlays.Add(_backgroundOverlay);
            MapView.ExtentOverlay = new CustomEventView(); // Use the custom eventview which disables all the map events.

            var animationSettings = new AnimationSettings
            {
                Type = MapAnimationType.DrawWithAnimation,
                Duration = 1000,
                Easing = null
            };

            MapView.DefaultAnimationSettings = animationSettings;

            _gpsPoints = await InitGpsData();

            // Create the Layer for the Route
            var routeLayer = GetRouteLayerFromGpsPoints(_gpsPoints);
            _visitedRoutesLayer = new InMemoryFeatureLayer();
            _visitedRoutesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle =
                LineStyle.CreateSimpleLineStyle(GeoColors.Green, 6, true);
            _visitedRoutesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _routesOverlay = new LayerWpfDrawingOverlay();
            _routesOverlay.UpdateDataWhileTransforming = true;
            _routesOverlay.Layers.Add(routeLayer);
            _routesOverlay.Layers.Add(_visitedRoutesLayer);
            MapView.Overlays.Add(_routesOverlay);

            // Create a marker overlay to show where the vehicle is
            _markerOverlay = new SimpleMarkerOverlay();
            // Create the marker of the vehicle
            _vehicleMarker = new Marker()
            {
                Position = new Point(_gpsPoints[0].X, _gpsPoints[0].Y),
                ImageSource = new BitmapImage(new Uri("/Resources/vehicle_location.png", UriKind.RelativeOrAbsolute)),
                Width = 24,
                Height = 24
            };
            _markerOverlay.Markers.Add(_vehicleMarker);
            MapView.Overlays.Add(_markerOverlay);

            MapView.CurrentExtentChangedInAnimation += MapViewOnCurrentExtentChangedInAnimation;

            MapView.CenterPoint = new PointShape(_gpsPoints[0]);
            MapView.CurrentScale = 5000;
            await MapView.RefreshAsync();

            await ZoomToGpsPointsAsync();
        }

        private async Task ZoomToGpsPointsAsync()
        {
            for (_currentGpsPointIndex = 0; _currentGpsPointIndex < _gpsPoints.Count; _currentGpsPointIndex++)
            {
                while (_busy || _cancellationTokenSource.IsCancellationRequested)
                    await Task.Delay(500); // delay zooming to GPS Points if the map is refreshing

                await ZoomToGpsPointAsync(_currentGpsPointIndex, _cancellationTokenSource.Token);

                if (_disposed)
                    break;
            }
        }

        private void MapViewOnCurrentExtentChangedInAnimation(object sender,
            CurrentExtentChangedInAnimationMapViewEventArgs e)
        {

            if (!MapUtil.IsSameDouble(e.FromResolution, e.ToResolution))
                return;

            UpdateRoutesAndMarker(e.Progress);
        }

        private void UpdateRoutesAndMarker(double progress, double angle = 0)
        {
            if (_currentGpsPointIndex == 0)
                return;

            if (_visitedVertices.Count == 0)
                return;

            if (_currentGpsPointIndex >= _gpsPoints.Count)
                return;

            var fromPoint = _gpsPoints[_currentGpsPointIndex - 1];
            var toPoint = _gpsPoints[_currentGpsPointIndex];

            var x = (toPoint.X - fromPoint.X) * progress + fromPoint.X;
            var y = (toPoint.Y - fromPoint.Y) * progress + fromPoint.Y;

            if (!MapUtil.IsSamePoint(_visitedVertices[_visitedVertices.Count - 1], _gpsPoints[_currentGpsPointIndex - 1]))
            {
                _visitedVertices.RemoveAt(_visitedVertices.Count - 1);
            }

            UpdateVisitedRoutes(new Vertex(x, y));

            _vehicleMarker.RotateAngle = angle;
            _vehicleMarker.Position = new Point(x, y);
            _ = _markerOverlay.RefreshAsync();
        }

        private void UpdateVisitedRoutes(Vertex newVertex)
        {
            _visitedVertices.Add(newVertex);

            if (_visitedVertices.Count < 2)
                return;

            var lineShape = new LineShape(_visitedVertices);
            _visitedRoutesLayer.InternalFeatures.Clear();
            _visitedRoutesLayer.InternalFeatures.Add(new Feature(lineShape));
        }

        private double _currentScale = 5000;

        private async Task ZoomToGpsPointAsync(int gpsPointIndex, CancellationToken cancellationToken)
        {
            if (gpsPointIndex >= _gpsPoints.Count)
                return;

            var currentLocation = _gpsPoints[gpsPointIndex];
            var angle = GetRotationAngle(gpsPointIndex, _gpsPoints);


            var centerPoint = new PointShape(currentLocation);

            // Recenter the map to display the GPS location towards the bottom for improved visibility.
            centerPoint =
                MapUtil.OffsetPointWithScreenOffset(centerPoint, 0, 200, angle, _currentScale, MapView.MapUnit);

            if (_showOverview)
            {
                var totalTime = 1000.0;
                var currentTime = System.DateTime.Now;

                while (true)
                {
                    double duration = (DateTime.Now - currentTime).TotalMilliseconds;
                    var process = duration / totalTime;

                    if (process > 1)
                        break;

                    UpdateRoutesAndMarker(process, angle);

                    await _routesOverlay.RefreshAsync();
                    await Task.Delay(10);
                }
            }


            else
                await MapView.ZoomToAsync(centerPoint, _currentScale, angle, cancellationToken);
            UpdateVisitedRoutes(_gpsPoints[gpsPointIndex]);

            _vehicleMarker.Position = new Point(currentLocation.X, currentLocation.Y);
        }

        private static async Task<Collection<Vertex>> InitGpsData()
        {
            var gpsPoints = new Collection<Vertex>();

            // read the csv file from the embed resource. 
            using (var stream = new FileStream(@"./Data/Csv/vehicle-route.csv", FileMode.Open))
            {
                if (stream == null) return null;

                // Convert GPS Points from Lat/Lon (srid:4326) to Spherical Mercator (Srid:3857), which is the projection of the base map
                var converter = new ProjectionConverter(4326, 3857);
                converter.Open();

                using (var reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        var location = await reader.ReadLineAsync();
                        if (location == null)
                            continue;
                        var posItems = location.Split(',');
                        var lat = double.Parse(posItems[0]);
                        var lon = double.Parse(posItems[1]);
                        var vertexInSphericalMercator = converter.ConvertToExternalProjection(lon, lat);
                        gpsPoints.Add(vertexInSphericalMercator);
                    }

                    converter.Close();
                }
            }
            return gpsPoints;
        }

        private static InMemoryFeatureLayer GetRouteLayerFromGpsPoints(Collection<Vertex> gpsPoints)
        {
            var lineShape = new LineShape();
            foreach (var vertex in gpsPoints)
            {
                lineShape.Vertices.Add(vertex);
            }

            // create the layers for the routes.
            var routeLayer = new InMemoryFeatureLayer();
            routeLayer.InternalFeatures.Add(new Feature(lineShape));
            routeLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle =
                LineStyle.CreateSimpleLineStyle(GeoColors.Yellow, 6, true);
            routeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            return routeLayer;
        }

        private static double GetRotationAngle(int currentIndex, IReadOnlyList<Vertex> gpsPoints)
        {
            Vertex currentLocation;
            Vertex nextLocation;

            if (currentIndex < gpsPoints.Count - 1)
            {
                currentLocation = gpsPoints[currentIndex];
                nextLocation = gpsPoints[currentIndex + 1];
            }
            else
            {
                currentLocation = gpsPoints[currentIndex - 1];
                nextLocation = gpsPoints[currentIndex];
            }

            double angle;
            if (nextLocation.X - currentLocation.X != 0)
            {
                var dx = nextLocation.X - currentLocation.X;
                var dy = nextLocation.Y - currentLocation.Y;

                angle = Math.Atan2(dx, dy) / Math.PI * 180; // get the angle in degrees 
                angle = -angle;
            }
            else
            {
                angle = nextLocation.Y - currentLocation.Y >= 0 ? 0 : 180;
            }

            return angle;
        }

        private void RefreshCancellationTokenAsync()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private async void AerialBackgroundCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            while (_busy)
                await Task.Delay(500); // delay the operation is it's rendering 
            _busy = true;
            RefreshCancellationTokenAsync();

            if (AerialBackgroundCheckBox.IsChecked == null)
                return;

            _backgroundOverlay.MapType = AerialBackgroundCheckBox.IsChecked.Value
                ? ThinkGeoCloudRasterMapsMapType.Aerial_V2_X2
                : ThinkGeoCloudRasterMapsMapType.Light_V2_X2;
            await _backgroundOverlay.RefreshAsync();

            _busy = false;
        }

        private async void OverviewButton_OnClick(object sender, RoutedEventArgs e)
        {
            _showOverview = !_showOverview;

            if (_showOverview)
            {
                var layer = GetRouteLayerFromGpsPoints(_gpsPoints);
                layer.Open();
                var bbox = layer.GetBoundingBox();
                var center = bbox.GetCenterPoint();

                var scale = MapUtil.GetScale(MapView.MapUnit, bbox, MapView.MapWidth, MapView.MapHeight) * 1.2;

                //var scale = MapUtil.GetScale(bbox, MapView.MapWidth, MapView.MapUnit) * 1.2;

                RefreshCancellationTokenAsync();

                await MapView.ZoomToAsync(center, scale, 0);
            }
            else
            {
                //  MapView.CurrentScale = 5000;
                _currentScale = 5000;
            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);

            _disposed = true;
        }

    }

    /// <summary>
    /// This customized class disables all the Touch events.
    /// </summary>
    class CustomEventView : ExtentInteractiveOverlay
    {
        protected override InteractiveResult MouseDownCore(InteractionArguments interactionArguments)
            => new InteractiveResult();

        protected override InteractiveResult MouseMoveCore(InteractionArguments interactionArguments)
            => new InteractiveResult();
    }
}