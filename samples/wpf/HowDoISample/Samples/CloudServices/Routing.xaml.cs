using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Route through a set of stops in order or in the shortest order, or find how far you can drive from a point.
    /// </summary>
    public partial class Routing
    {
        private static readonly Collection<PointShape> Stops = new Collection<PointShape>
        {
            new PointShape(-10776986.85, 3908680.24),
            new PointShape(-10776836.12, 3912348.04),
            new PointShape(-10778917.01, 3909965.17),
            new PointShape(-10779631.80, 3915721.82),
            new PointShape(-10774900.61, 3912552.36),
        };

        private static readonly TimeSpan[] TravelTimes =
        {
            TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(45), TimeSpan.FromMinutes(60),
        };

        private static readonly GeoColor[] TravelTimeColors = { GeoColors.Green, GeoColors.Yellow, GeoColors.Orange, GeoColors.Red };

        private bool _initialized;
        private string _mode = "route";
        private RoutingCloudClient _routingCloudClient;

        // The highlight is added after the result, so a picked segment draws over its route.
        private readonly DynamicSource _result = new DynamicSource();
        private readonly DynamicSource _highlight = new DynamicSource();

        public Routing()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;

            var style = new MapStyle(ThinkGeoVectorStyles.Dark, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });
            style.Images.Add("star", new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple));
            style.Images.Add("center", new PointStyle(PointSymbolType.Cross, 20, GeoBrushes.Red));
            style.AddDynamic(_result);
            style.AddDynamic(_highlight);
            Map.Basemap = new GpuBasemap(style);

            Map.CenterPoint = new PointShape(-10778720, 3915154);
            Map.CurrentScale = 202090;

            _routingCloudClient = new RoutingCloudClient
            {
                ClientId = SampleShared.ClientId,
                ClientSecret = SampleShared.ClientSecret,
            };

            _ = RunModeAsync();
        }

        private void Mode_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _mode = (string)((RadioButton)sender).Tag;
            _ = RunModeAsync();
        }

        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (_mode != "area" || e.MouseButton != MapMouseButton.Left) return;

            _ = RunModeAsync(e.WorldLocation);
        }

        private async Task RunModeAsync(PointShape serviceAreaCenter = null)
        {
            _result.Clear();
            _highlight.Clear();
            LsbRouteSegments.ItemsSource = null;
            TravelTimeLegend.Visibility = _mode == "area" ? Visibility.Visible : Visibility.Collapsed;
            LoadingImage.Visibility = Visibility.Visible;

            try
            {
                switch (_mode)
                {
                    case "route":
                        await RouteThroughStopsAsync();
                        break;
                    case "optimized":
                        await RouteInShortestOrderAsync();
                        break;
                    case "area":
                        await FindServiceAreaAsync(serviceAreaCenter ?? new PointShape(-10776836.14, 3912350.71));
                        break;
                }
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
            finally
            {
                LoadingImage.Visibility = Visibility.Hidden;
            }
        }

        private async Task RouteThroughStopsAsync()
        {
            ShowStopsWhileWaiting();
            var result = await _routingCloudClient.GetRouteAsync(Stops, 3857, new CloudRoutingGetRouteOptions { TurnByTurn = true });
            if (_mode != "route") return;
            if (result.Exception != null)
            {
                Status.Text = result.Exception.Message;
                return;
            }

            var labels = result.RouteResult.Waypoints.Select((_, i) => i == 0 ? "Start" : "Stop " + i).ToList();
            await DrawRouteAsync(result.RouteResult.Routes, result.RouteResult.Waypoints.Select(w => new PointShape(w.Coordinate)).ToList(), labels);
        }

        private async Task RouteInShortestOrderAsync()
        {
            ShowStopsWhileWaiting();

            // Any stop may come first, and the route comes back to where it started.
            var options = new CloudRoutingOptimizationOptions
            {
                TurnByTurn = true,
                Source = CloudRoutingTspFixSourceCoordinate.Any,
                Destination = CloudRoutingTspFixDestinationCoordinate.Any,
                Roundtrip = true,
            };
            var result = await _routingCloudClient.GetOptimizedRouteAsync(Stops, 3857, options);
            if (_mode != "optimized") return;
            if (result.Exception != null)
            {
                Status.Text = result.Exception.Message;
                return;
            }

            // VisitSequences lists the stops in the order they are visited, as indexes into the stops sent.
            var visits = result.TspResult.VisitSequences;
            var labels = visits.Select((_, i) => i == 0 || i == visits.Count - 1 ? "Start and end" : "Stop " + i).ToList();
            await DrawRouteAsync(result.TspResult.Routes, visits.Select(index => Stops[index]).ToList(), labels);
        }

        private async Task FindServiceAreaAsync(PointShape center)
        {
            Status.Text = "Click the map to start somewhere else.";
            var options = new CloudRoutingGetServiceAreaOptions { DistanceUnit = DistanceUnit.Meter };
            var result = await _routingCloudClient.GetServiceAreaAsync(center, 3857, TravelTimes, options);
            if (_mode != "area") return;
            if (result.Exception != null)
            {
                Status.Text = result.Exception.Message;
                return;
            }

            // One area per travel time. The longest goes first, so each shorter one draws over it.
            var areas = result.ServiceAreaResult.ServiceAreas;
            for (var i = areas.Count - 1; i >= 0; i--)
            {
                _result.SetArea("area-" + i, new Feature(areas[i]), GeoColor.FromArgb(60, TravelTimeColors[i]), TravelTimeColors[i], 1f);
            }
            _result.SetPoint("center", new Feature(new PointShape(result.ServiceAreaResult.Waypoint.Coordinate)), sizeInPixel: 20f, iconName: "center");

            await FitAsync(MapUtil.GetBoundingBoxOfItems(areas));
        }

        private void ShowStopsWhileWaiting()
        {
            Status.Text = "Asking for the route...";
            for (var i = 0; i < Stops.Count; i++)
            {
                _result.SetPoint("stop-" + i, new Feature(Stops[i]), sizeInPixel: 24f, iconName: "star");
            }
        }

        private async Task DrawRouteAsync(IEnumerable<CloudRoutingRoute> routes, IList<PointShape> stops, IList<string> labels)
        {
            var routeLines = new List<BaseShape>();
            var segments = new List<CloudRoutingSegment>();
            var distance = 0d;
            var duration = TimeSpan.Zero;
            foreach (var route in routes)
            {
                routeLines.Add(route.Shape);
                segments.AddRange(route.Segments);
                distance += route.Distance;
                duration += route.Duration;
            }

            // The marker and the label of a stop share its id: one feature, drawn two ways.
            _result.Clear();
            for (var i = 0; i < routeLines.Count; i++)
            {
                _result.SetLine("route-" + i, new Feature(routeLines[i]), GeoColors.MediumPurple, 3f);
            }
            for (var i = 0; i < stops.Count; i++)
            {
                var stop = new Feature(stops[i]);
                stop.ColumnValues["Label"] = labels[i];
                _result.SetPoint("stop-" + i, stop, sizeInPixel: 24f, iconName: "star");
                _result.SetLabel("stop-" + i, stop, "Label", sizeInPixel: 15f, offsetYInPixel: -20f);
            }

            Status.Text = $"{distance / 1000:0.0} km, about {duration.TotalMinutes:0} minutes. Pick a step to see it on the map.";
            LsbRouteSegments.ItemsSource = segments;
            await FitAsync(MapUtil.GetBoundingBoxOfItems(routeLines));
        }

        private async void LsbRouteSegments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(LsbRouteSegments.SelectedItem is CloudRoutingSegment segment)) return;

            try
            {
                _highlight.Clear();
                _highlight.SetLine("segment", new Feature(segment.Shape), GeoColors.BrightYellow, 6f);

                var extent = segment.Shape.GetBoundingBox();
                var fit = MapUtil.GetScale(Map.MapUnit, extent, Map.MapWidth, Map.MapHeight) * 1.5;
                await Map.ZoomToAsync(extent.GetCenterPoint(), Math.Max(fit, new ZoomLevelSet().ZoomLevel15.Scale));
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }

        private Task FitAsync(RectangleShape extent)
        {
            var scale = MapUtil.GetScale(Map.MapUnit, extent, Map.MapWidth, Map.MapHeight) * 1.2;
            return Map.ZoomToAsync(extent.GetCenterPoint(), scale);
        }
    }
}
