using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Compute a value from a shape: the area of a park, the length of a trail, a center point, the shortest line between two shapes, or part of a line.
    /// </summary>
    public partial class ShapeMeasurements : IDisposable
    {
        private const string RailwayWkt = "LINESTRING(-10776731 3925751, -10778989 3915278, -10781766 3909228, -10782066 3907459, -10781867 3905465)";

        private bool _initialized;
        private string _measurement = "area";
        private Feature _selectedCensusBlock;
        private ShapeFileFeatureLayer _parksLayer;
        private ShapeFileFeatureLayer _trailsLayer;
        private ShapeFileFeatureLayer _censusHousingLayer;
        private InMemoryFeatureLayer _stadiumLayer;
        private InMemoryFeatureLayer _railwayLayer;
        private InMemoryFeatureLayer _resultLayer;
        private LayerOverlay _resultOverlay;

        public ShapeMeasurements()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            _parksLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Parks.shp"));
            _parksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            _parksLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
            _parksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _trailsLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Hike_Bike.shp"));
            _trailsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            _trailsLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Orange, 2, false);
            _trailsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _censusHousingLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Frisco 2010 Census Housing Units.shp"));
            _censusHousingLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            _censusHousingLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
            _censusHousingLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _stadiumLayer = new InMemoryFeatureLayer();
            _stadiumLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 16, GeoColors.White, 4);
            _stadiumLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            _stadiumLayer.InternalFeatures.Add(new Feature(new PointShape(-10779651.5, 3915933.0)));

            _railwayLayer = new InMemoryFeatureLayer();
            _railwayLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Red, 2, false);
            _railwayLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            _railwayLayer.InternalFeatures.Add(new Feature(RailwayWkt));

            var dataOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            dataOverlay.Layers.Add(_parksLayer);
            dataOverlay.Layers.Add(_trailsLayer);
            dataOverlay.Layers.Add(_censusHousingLayer);
            dataOverlay.Layers.Add(_railwayLayer);
            dataOverlay.Layers.Add(_stadiumLayer);
            Map.Overlays.Add(dataOverlay);

            _resultLayer = new InMemoryFeatureLayer();
            _resultLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(64, GeoColors.Green), GeoColors.Black, 2);
            _resultLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Green, 3, false);
            _resultLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Green, 12, GeoColors.White, 4);
            _resultLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            _resultOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            _resultOverlay.Layers.Add(_resultLayer);
            Map.Overlays.Add(_resultOverlay);

            ShowMeasurement();
        }

        private void Measurement_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _measurement = (string)((RadioButton)sender).Tag;
            ShowMeasurement();
        }

        private void ShowMeasurement()
        {
            _parksLayer.IsVisible = _measurement == "area" || _measurement == "shortest";
            _stadiumLayer.IsVisible = _measurement == "shortest";
            _trailsLayer.IsVisible = _measurement == "length";
            _censusHousingLayer.IsVisible = _measurement == "center";
            _railwayLayer.IsVisible = _measurement == "lineOnALine";
            CenterPanel.Visibility = _measurement == "center" ? Visibility.Visible : Visibility.Collapsed;
            LineOnALinePanel.Visibility = _measurement == "lineOnALine" ? Visibility.Visible : Visibility.Collapsed;

            _resultLayer.InternalFeatures.Clear();
            _selectedCensusBlock = null;
            Result.Text = string.Empty;

            switch (_measurement)
            {
                case "center":
                    ZoomToLayer(_censusHousingLayer);
                    Status.Text = "Click a census block on the map.";
                    break;
                case "lineOnALine":
                    ZoomToLayer(_railwayLayer);
                    Status.Text = "Measure along the railway from its first point.";
                    break;
                default:
                    Map.CenterPoint = new PointShape(-10778340, 3915490);
                    Map.CurrentScale = 36110;
                    Status.Text = _measurement == "length" ? "Click a trail on the map." : "Click a park on the map.";
                    break;
            }

            _ = Map.RefreshAsync();
        }

        private void ZoomToLayer(FeatureLayer layer)
        {
            layer.Open();
            var bbox = layer.GetBoundingBox();
            layer.Close();
            Map.CenterPoint = bbox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, bbox, Map.MapWidth, Map.MapHeight) * 1.5;
        }

        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            switch (_measurement)
            {
                case "area":
                    CalculateArea(e.WorldLocation);
                    break;
                case "length":
                    CalculateLength(e.WorldLocation);
                    break;
                case "center":
                    _selectedCensusBlock = NearestFeature(_censusHousingLayer, e.WorldLocation);
                    CalculateCenterPoint();
                    break;
                case "shortest":
                    GetShortestLine(e.WorldLocation);
                    break;
            }
        }

        private static Feature NearestFeature(FeatureLayer layer, PointShape location)
        {
            layer.Open();
            var feature = layer.QueryTools.GetFeaturesNearestTo(location, GeographyUnit.Meter, 1, ReturningColumnsType.NoColumns).FirstOrDefault();
            layer.Close();
            return feature;
        }

        private void CalculateArea(PointShape location)
        {
            var park = NearestFeature(_parksLayer, location);
            if (park == null) return;

            var area = ((AreaBaseShape)park.GetShape()).GetArea(new Projection(3857), AreaUnit.SquareKilometers);
            Result.Text = $"Area: {area:f3} sq km";
            ShowResult(park);
        }

        private void CalculateLength(PointShape location)
        {
            var trail = NearestFeature(_trailsLayer, location);
            if (trail == null) return;

            var length = ((LineBaseShape)trail.GetShape()).GetLength(new Projection(3857), DistanceUnit.Kilometer);
            Result.Text = $"Length: {length:f3} km";
            ShowResult(trail);
        }

        private void CalculateCenterPoint()
        {
            if (_selectedCensusBlock == null) return;

            var centerPoint = CentroidCenter.IsChecked == true
                ? _selectedCensusBlock.GetShape().GetCenterPoint()
                : _selectedCensusBlock.GetBoundingBox().GetCenterPoint();
            Result.Text = $"Center: {centerPoint.X:f0}, {centerPoint.Y:f0}";
            ShowResult(_selectedCensusBlock, new Feature(centerPoint));
        }

        private void CenterKind_Checked(object sender, RoutedEventArgs e)
        {
            if (_initialized) CalculateCenterPoint();
        }

        private void GetShortestLine(PointShape location)
        {
            var park = NearestFeature(_parksLayer, location);
            if (park == null) return;

            var shortestLine = park.GetShape().GetShortestLineTo(_stadiumLayer.InternalFeatures[0], GeographyUnit.Meter);
            Result.Text = $"Distance: {shortestLine.GetLength(new Projection(3857), DistanceUnit.Kilometer):f3} km";
            ShowResult(new Feature(shortestLine));
        }

        private void GetLineOnALine_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(StartingOffset.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var startingOffset)
                || !double.TryParse(Distance.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out var distance))
            {
                Status.Text = "Enter a number.";
                return;
            }

            var railway = (LineShape)_railwayLayer.InternalFeatures[0].GetShape();
            var subLine = railway.GetLineOnALine(StartingPoint.FirstPoint, startingOffset, distance, GeographyUnit.Meter, DistanceUnit.Meter);
            Status.Text = string.Empty;
            ShowResult(new Feature(subLine));
        }

        private void ShowResult(params Feature[] features)
        {
            _resultLayer.InternalFeatures.Clear();
            foreach (var feature in features)
            {
                _resultLayer.InternalFeatures.Add(feature);
            }
            _ = _resultOverlay.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
