using System;
using System.Collections.Generic;
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
    /// Draw a shape and get the World Maps features - buildings, roads, places and more - that it touches.
    /// </summary>
    public partial class WorldMapsQuery
    {
        private bool _initialized;
        private MapsQueryCloudClient _mapsQueryCloudClient;

        // The query shape is added last, so it draws over what it found.
        private readonly DynamicSource _querySource = new DynamicSource();
        private readonly DynamicSource _resultsSource = new DynamicSource();
        private BaseShape _queryShape;

        public WorldMapsQuery()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;

            var style = new MapStyle(ThinkGeoVectorStyles.Dark, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });
            style.Images.Add("star-blue", new PointStyle(PointSymbolType.Star, 20, GeoBrushes.DodgerBlue));
            style.Images.Add("star-orange", new PointStyle(PointSymbolType.Star, 20, GeoBrushes.OrangeRed));
            style.AddDynamic(_resultsSource);
            style.AddDynamic(_querySource);
            Map.Basemap = new GpuBasemap(style);

            Map.CenterPoint = new PointShape(-10778720, 3915154);
            Map.CurrentScale = 202090;

            Map.TrackOverlay.TrackEnded += OnShapeDrawn;

            _mapsQueryCloudClient = new MapsQueryCloudClient
            {
                ClientId = SampleShared.ClientId,
                ClientSecret = SampleShared.ClientSecret,
            };

            ShowQueryShape(new RectangleShape(-10779877.70, 3915441.00, -10779248.97, 3915119.63));
            _ = QueryWorldMapsAsync();
        }

        private async Task QueryWorldMapsAsync()
        {
            var queryLayer = ((ComboBoxItem)CboQueryLayer.SelectedItem).Content.ToString().ToLower();
            var maxResults = (int)MaxResults.Value;
            var options = new CloudMapsQuerySpatialQueryOptions { MaxResults = maxResults };

            CloudMapsQueryResult result = null;
            LoadingImage.Visibility = Visibility.Visible;
            try
            {
                switch (((ComboBoxItem)CboQueryType.SelectedItem).Content.ToString())
                {
                    case "Containing":
                        result = await _mapsQueryCloudClient.GetFeaturesContainingAsync(queryLayer, _queryShape, 3857, options);
                        break;
                    case "Nearest":
                        result = await _mapsQueryCloudClient.GetFeaturesNearestAsync(queryLayer, _queryShape, 3857, maxResults);
                        break;
                    case "Intersecting":
                        result = await _mapsQueryCloudClient.GetFeaturesIntersectingAsync(queryLayer, _queryShape, 3857, options);
                        break;
                    case "Overlapping":
                        result = await _mapsQueryCloudClient.GetFeaturesOverlappingAsync(queryLayer, _queryShape, 3857, options);
                        break;
                    case "Within":
                        result = await _mapsQueryCloudClient.GetFeaturesWithinAsync(queryLayer, _queryShape, 3857, options);
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                // A request the service turns down is thrown as an ArgumentException, the reason in the inner exception.
                Status.Text = ex.InnerException == null ? ex.Message : $"{ex.InnerException.Message} {ex.Message}";
                return;
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
                return;
            }
            finally
            {
                LoadingImage.Visibility = Visibility.Hidden;
            }

            if (result.Features.Count == 0)
            {
                Status.Text = "Nothing was found.";
                return;
            }

            Status.Text = $"Found {result.Features.Count} features.";
            var shapes = new List<BaseShape>();
            _resultsSource.Clear();
            for (var i = 0; i < result.Features.Count; i++)
            {
                var feature = result.Features[i];
                var shape = feature.GetShape();
                shapes.Add(shape);
                var id = "result-" + i;
                switch (shape)
                {
                    case AreaBaseShape: _resultsSource.SetArea(id, feature, new GeoColor(10, GeoColors.OrangeRed), GeoColors.OrangeRed, 1f); break;
                    case LineBaseShape: _resultsSource.SetLine(id, feature, GeoColors.OrangeRed, 2f); break;
                    default: _resultsSource.SetPoint(id, feature, sizeInPixel: 20f, iconName: "star-orange"); break;
                }
            }

            var extent = MapUtil.GetBoundingBoxOfItems(shapes);
            Map.CenterPoint = extent.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, extent, Map.MapWidth, Map.MapHeight);
            await Map.RefreshAsync();
        }

        private async void OnShapeDrawn(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            try
            {
                Map.TrackOverlay.TrackMode = TrackMode.None;
                Map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

                ShowQueryShape(e.TrackShape);
                await Map.RefreshAsync(Map.TrackOverlay);
                await QueryWorldMapsAsync();
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }

        private void DrawPoint_Click(object sender, RoutedEventArgs e)
        {
            Map.TrackOverlay.TrackMode = TrackMode.Point;
            ClearQueryShapes();
        }

        private void DrawLine_Click(object sender, RoutedEventArgs e)
        {
            Map.TrackOverlay.TrackMode = TrackMode.Line;
            ClearQueryShapes();
        }

        private void DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            Map.TrackOverlay.TrackMode = TrackMode.Polygon;
            ClearQueryShapes();
        }

        private void ShowQueryShape(BaseShape shape)
        {
            _queryShape = shape;
            _querySource.Clear();
            var query = new Feature(shape);
            switch (shape)
            {
                case PointShape: _querySource.SetPoint("query", query, sizeInPixel: 20f, iconName: "star-blue"); break;
                case LineBaseShape: _querySource.SetLine("query", query, GeoColors.DodgerBlue, 2f); break;
                case AreaBaseShape: _querySource.SetArea("query", query, new GeoColor(10, GeoColors.DodgerBlue), GeoColors.DodgerBlue, 1f); break;
            }
        }

        private void ClearQueryShapes()
        {
            _queryShape = null;
            _querySource.Clear();
            _resultsSource.Clear();
            Status.Text = string.Empty;
        }
    }
}
