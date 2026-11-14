using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Route through a set of stops in order or in the shortest order, or find how far you can drive from a point.
    /// </summary>
    public partial class Routing : IDisposable
    {
        private static readonly Collection<PointShape> Stops = new Collection<PointShape>
        {
            new PointShape(-10776986.85, 3908680.24),
            new PointShape(-10776836.12, 3912348.04),
            new PointShape(-10778917.01, 3909965.17),
            new PointShape(-10779631.80, 3915721.82),
            new PointShape(-10774900.61, 3912552.36),
        };

        private static readonly Collection<TimeSpan> TravelTimes = new Collection<TimeSpan>
        {
            TimeSpan.FromMinutes(15), TimeSpan.FromMinutes(30), TimeSpan.FromMinutes(45), TimeSpan.FromMinutes(60),
        };

        private bool _initialized;
        private string _mode = "route";
        private RoutingCloudClient _routingCloudClient;
        private InMemoryFeatureLayer _serviceAreaLayer;
        private InMemoryFeatureLayer _routeLayer;
        private InMemoryFeatureLayer _highlightLayer;
        private LayerOverlay _routingOverlay;
        private LegendAdornmentLayer _travelTimeLegend;

        public Routing()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            var travelTimeBreaks = new Collection<ClassBreak>
            {
                new ClassBreak(15, AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(60, GeoColors.Green), GeoColors.Green)),
                new ClassBreak(30, AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(60, GeoColors.Yellow), GeoColors.Yellow)),
                new ClassBreak(45, AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(60, GeoColors.Orange), GeoColors.Orange)),
                new ClassBreak(60, AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(60, GeoColors.Red), GeoColors.Red)),
            };
            _serviceAreaLayer = new InMemoryFeatureLayer();
            _serviceAreaLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new ClassBreakStyle("TravelTime", BreakValueInclusion.IncludeValue, travelTimeBreaks));
            _serviceAreaLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _routeLayer = new InMemoryFeatureLayer();
            _routeLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 24, GeoBrushes.MediumPurple, GeoPens.Purple);
            _routeLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.MediumPurple, 3, false);
            _routeLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyle.CreateMaskTextStyle("Label", new GeoFont("Verdana", 12), GeoBrushes.White, AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(90, GeoColors.Black), GeoColors.Black, 2), 0, 0);
            _routeLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.TextPlacement = TextPlacement.Upper;
            _routeLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.YOffsetInPixel = -8;
            _routeLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.OverlappingRule = LabelOverlappingRule.AllowOverlapping;
            _routeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _highlightLayer = new InMemoryFeatureLayer();
            _highlightLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.BrightYellow, 6, GeoColors.Black, 2, false);
            _highlightLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _routingOverlay = new LayerOverlay();
            _routingOverlay.Layers.Add(_serviceAreaLayer);
            _routingOverlay.Layers.Add(_routeLayer);
            _routingOverlay.Layers.Add(_highlightLayer);
            Map.Overlays.Add(_routingOverlay);

            _travelTimeLegend = new LegendAdornmentLayer
            {
                Title = new LegendItem { TextStyle = new TextStyle("Travel Times", new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black) },
                Location = AdornmentLocation.LowerRight,
                IsVisible = false
            };
            foreach (var travelTimeBreak in travelTimeBreaks)
            {
                _travelTimeLegend.LegendItems.Add(new LegendItem
                {
                    ImageStyle = travelTimeBreak.DefaultAreaStyle,
                    TextStyle = new TextStyle($"{travelTimeBreak.Value} minutes", new GeoFont("Verdana", 10), GeoBrushes.Black)
                });
            }
            Map.AdornmentOverlay.Layers.Add(_travelTimeLegend);

            Map.CenterPoint = new PointShape(-10778720, 3915154);
            Map.CurrentScale = 202090;

            _routingCloudClient = new RoutingCloudClient
            {
                ClientId = SampleKeys.ClientId2,
                ClientSecret = SampleKeys.ClientSecret2,
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
            _serviceAreaLayer.InternalFeatures.Clear();
            _routeLayer.InternalFeatures.Clear();
            _highlightLayer.InternalFeatures.Clear();
            LsbRouteSegments.ItemsSource = null;
            _travelTimeLegend.IsVisible = _mode == "area";
            LoadingImage.Visibility = Visibility.Visible;

            try
            {
                await Map.RefreshAsync();
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
            catch (OperationCanceledException)
            {
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
            Status.Text = "Asking for the route...";
            var result = await _routingCloudClient.GetRouteAsync(Stops, 3857, new CloudRoutingGetRouteOptions { TurnByTurn = true });
            if (_mode != "route") return;
            if (result.Exception != null)
            {
                Status.Text = result.Exception.Message;
                return;
            }

            var stops = result.RouteResult.Waypoints.Select(waypoint => new PointShape(waypoint.Coordinate)).ToList();
            var labels = stops.Select((_, i) => i == 0 ? "Start" : "Stop " + i).ToList();
            await DrawRouteAsync(result.RouteResult.Routes, stops, labels);
        }

        private async Task RouteInShortestOrderAsync()
        {
            Status.Text = "Asking for the route...";

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
                var travelTime = new Dictionary<string, string> { { "TravelTime", TravelTimes[i].TotalMinutes.ToString(CultureInfo.InvariantCulture) } };
                _serviceAreaLayer.InternalFeatures.Add(new Feature(areas[i], travelTime));
            }
            _routeLayer.InternalFeatures.Add(new Feature(new PointShape(result.ServiceAreaResult.Waypoint.Coordinate), new Dictionary<string, string> { { "Label", "Start" } }));

            await FitAsync(MapUtil.GetBoundingBoxOfItems(areas));
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

            foreach (var routeLine in routeLines)
            {
                _routeLayer.InternalFeatures.Add(new Feature(routeLine));
            }
            for (var i = 0; i < stops.Count; i++)
            {
                _routeLayer.InternalFeatures.Add(new Feature(stops[i], new Dictionary<string, string> { { "Label", labels[i] } }));
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
                _highlightLayer.InternalFeatures.Clear();
                _highlightLayer.InternalFeatures.Add(new Feature(segment.Shape));

                var extent = segment.Shape.GetBoundingBox();
                var fit = MapUtil.GetScale(Map.MapUnit, extent, Map.MapWidth, Map.MapHeight) * 1.5;
                await Map.ZoomToAsync(extent.GetCenterPoint(), Math.Max(fit, new ZoomLevelSet().ZoomLevel15.Scale));
                await _routingOverlay.RefreshAsync();
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }

        private async Task FitAsync(RectangleShape extent)
        {
            var scale = MapUtil.GetScale(Map.MapUnit, extent, Map.MapWidth, Map.MapHeight) * 1.2;
            await Map.ZoomToAsync(extent.GetCenterPoint(), scale);
            await _routingOverlay.RefreshAsync();
            await Map.AdornmentOverlay.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
