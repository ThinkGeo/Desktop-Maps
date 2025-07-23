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
        private readonly List<Vertex> _visitedVertices = new List<Vertex>();
        private int _currentGpsPointIndex;
        private Marker _vehicleMarker;
        private bool _disposed;
        private bool _showOverview;

        private CancellationTokenSource _cancellationTokenSource;
        private ThinkGeoCloudRasterMapsOverlay _backgroundOverlay;

        private FeatureLayerWpfDrawingOverlay _routesOverlay;
        private SimpleMarkerOverlay _markerOverlay;
        private InMemoryFeatureLayer _routeLayer;
        private InMemoryFeatureLayer _visitedRoutesLayer;

        private const double DefaultScale = 5000;

        public VehicleNavigation()
        {
            InitializeComponent();
            this.Unloaded += VehicleNavigation_Unloaded;
        }

        private void VehicleNavigation_Unloaded(object sender, RoutedEventArgs e)
        {
            this._disposed = true;
        }

        private void MapView_OnLoaded(object sender, RoutedEventArgs e)
        {
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

            MapView.DefaultAnimationSettings = new MapAnimationSettings
            {
                Type = MapAnimationType.DrawWithAnimation,
                Duration = 1500,
                Easing = null
            };

            _gpsPoints = CollectGpsData();

            // Create the Layer for the Route
            _routeLayer = InitRouteLayerFromGpsPoints(_gpsPoints);
            _visitedRoutesLayer = new InMemoryFeatureLayer();
            _visitedRoutesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle =
                LineStyle.CreateSimpleLineStyle(GeoColors.Green, 6, true);
            _visitedRoutesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _routesOverlay = new FeatureLayerWpfDrawingOverlay();
            _routesOverlay.FeatureLayers.Add(_routeLayer);
            _routesOverlay.FeatureLayers.Add(_visitedRoutesLayer);
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
            MapView.CurrentScale = DefaultScale;

            _ = ZoomToGpsPointsAsync(_gpsPoints);
        }

        private async Task ZoomToGpsPointsAsync(Collection<Vertex> gpsPoints)
        {
            await MapView.RefreshAsync();

            for (_currentGpsPointIndex = 0; _currentGpsPointIndex < gpsPoints.Count; _currentGpsPointIndex++)
            {
                try
                {
                    await ZoomToGpsPointAsync(gpsPoints, _currentGpsPointIndex, _cancellationTokenSource.Token);
                }
                catch (TaskCanceledException)
                {
                    await Task.Delay(500);
                }

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

            if (_currentGpsPointIndex >= _gpsPoints.Count)
                return;

            var fromPoint = _gpsPoints[_currentGpsPointIndex - 1];
            var toPoint = _gpsPoints[_currentGpsPointIndex];

            var x = (toPoint.X - fromPoint.X) * progress + fromPoint.X;
            var y = (toPoint.Y - fromPoint.Y) * progress + fromPoint.Y;

            if (_visitedVertices.Count > 0 && !MapUtil.IsSamePoint(_visitedVertices[_visitedVertices.Count - 1], _gpsPoints[_currentGpsPointIndex - 1]))
            {
                _visitedVertices.RemoveAt(_visitedVertices.Count - 1);
            }

            UpdateVisitedRoutes(new Vertex(x, y));

            _vehicleMarker.RotationAngle = -angle;
            _vehicleMarker.Position = new Point(x, y);

            _ = _routesOverlay.RefreshAsync();
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

        private async Task ZoomToGpsPointAsync(Collection<Vertex> gpsPoints, int gpsPointIndex, CancellationToken cancellationToken)
        {
            if (gpsPointIndex >= gpsPoints.Count)
                return;

            var angle = GetRotationAngle(gpsPointIndex, gpsPoints);

            if (_showOverview)
            {
                var totalTime = 1000.0; // Set a 1-second animation
                var currentTime = DateTime.Now;

                while (true)
                {
                    if (cancellationToken.IsCancellationRequested)
                        await Task.Delay(500);
                    double duration = (DateTime.Now - currentTime).TotalMilliseconds;
                    var process = duration / totalTime;

                    if (process > 1)
                        break;

                    UpdateRoutesAndMarker(process, angle);

                    await _routesOverlay.RefreshAsync();
                    await Task.Delay(1);
                }
            }
            else
            {
                var currentLocation = gpsPoints[gpsPointIndex];
                var centerPoint = new PointShape(currentLocation);
                // Recenter the map to display the GPS location 200 pixels towards the bottom for improved visibility.
                centerPoint = MapUtil.OffsetPointWithScreenOffset(centerPoint, 0, 200, angle, DefaultScale, MapView.MapUnit);

                //UpdateRoutesAndMarker(0, angle);
                await MapView.ZoomToAsync(centerPoint, DefaultScale, angle, cancellationToken);
            }
        }

        private static Collection<Vertex> CollectGpsData()
        {
            var gpsPoints = new Collection<Vertex>();
            var lines = File.ReadAllLines(@"./Data/Csv/vehicle-route.csv");

            // Convert GPS Points from Lat/Lon (srid:4326) to Spherical Mercator (Srid:3857), which is the projection of the base map
            var converter = new ProjectionConverter(4326, 3857);
            converter.Open();

            foreach (var location in lines)
            {
                var posItems = location.Split(',');
                var lat = double.Parse(posItems[0]);
                var lon = double.Parse(posItems[1]);
                var vertexInSphericalMercator = converter.ConvertToExternalProjection(lon, lat);
                gpsPoints.Add(vertexInSphericalMercator);
            }

            converter.Close();
            return gpsPoints;
        }

        private static InMemoryFeatureLayer InitRouteLayerFromGpsPoints(Collection<Vertex> gpsPoints)
        {
            var lineShape = new LineShape();
            foreach (var gpsPoint in gpsPoints)
                lineShape.Vertices.Add(gpsPoint);

            // create the layers for the routes.
            var routeLayer = new InMemoryFeatureLayer();
            routeLayer.InternalFeatures.Add(new Feature(lineShape));
            routeLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Yellow, 6, true);
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

                angle = -Math.Atan2(dx, dy) / Math.PI * 180; // get the angle in degrees 
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

        private void AerialBackgroundCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            RefreshCancellationTokenAsync();

            _backgroundOverlay.MapType = AerialBackgroundCheckBox.IsChecked.Value
                ? ThinkGeoCloudRasterMapsMapType.Aerial_V2_X2
                : ThinkGeoCloudRasterMapsMapType.Light_V2_X2;
            _ = _backgroundOverlay.RefreshAsync();
        }

        private void OverviewButton_OnClick(object sender, RoutedEventArgs e)
        {
            _showOverview = !_showOverview;
            if (_showOverview)
            {
                OverviewButton.Content = "Tracking Mode";

                RefreshCancellationTokenAsync();

                var boundingBox = _routeLayer.GetBoundingBox();
                var center = boundingBox.GetCenterPoint();

                // Multiply the current scale by 1.5 to zoom out 50%.
                var scale = MapUtil.GetScale(MapView.MapUnit, boundingBox, MapView.MapWidth, MapView.MapHeight) * 1.5;

                _ = MapView.ZoomToAsync(center, scale, 0);
            }
            else
            {
                OverviewButton.Content = "Overview Mode";
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
}