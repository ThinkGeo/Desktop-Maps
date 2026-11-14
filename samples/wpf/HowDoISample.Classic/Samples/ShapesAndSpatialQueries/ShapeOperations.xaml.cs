using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Make a new shape from an existing one: union, buffer, simplify, rotate, scale, translate, clip, difference, convex hull or envelope.
    /// </summary>
    public partial class ShapeOperations : IDisposable
    {
        private const string WestRegionWkt =
            "POLYGON((-10780139 3918539, -10780207 3915600, -10780088 3914688, -10780038 3913978, -10779767 3913556, " +
            "-10779176 3913336, -10778636 3913049, -10778281 3911934, -10778264 3910684, -10778382 3910448, " +
            "-10778382 3909569, -10778264 3909113, -10778281 3907356, -10785596 3904045, -10786035 3904822, " +
            "-10786001 3908150, -10786052 3908691, -10785933 3909316, -10786001 3911275, -10785883 3912205, " +
            "-10785832 3914485, -10785832 3917729, -10785427 3918117, -10785460 3919012, -10782234 3918996, " +
            "-10781643 3918624, -10780139 3918539))";

        private bool _initialized;
        private string _operation = "buffer";
        private Feature _cityLimits;
        private Collection<Feature> _cityLimitsPieces;
        private ShapeFileFeatureLayer _cityLimitsLayer;
        private ShapeFileFeatureLayer _dividedCityLimitsLayer;
        private InMemoryFeatureLayer _westRegionLayer;
        private InMemoryFeatureLayer _resultLayer;
        private LayerOverlay _layerOverlay;

        public ShapeOperations()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            _cityLimitsLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "FriscoCityLimits.shp"));
            _cityLimitsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            _cityLimitsLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
            _cityLimitsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _dividedCityLimitsLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "FriscoCityLimitsDivided.shp"));
            _dividedCityLimitsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            _dividedCityLimitsLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(128, GeoColors.LightOrange), GeoColors.DimGray, 2);
            _dividedCityLimitsLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyle.CreateSimpleTextStyle("NAME", "Noto Sans", 12, DrawingFontStyles.Bold, GeoColors.Black, GeoColors.White, 2);
            _dividedCityLimitsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _westRegionLayer = new InMemoryFeatureLayer();
            _westRegionLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Blue), GeoColors.DimGray);
            _westRegionLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            _westRegionLayer.InternalFeatures.Add(new Feature(WestRegionWkt));

            _resultLayer = new InMemoryFeatureLayer();
            _resultLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(64, GeoColors.Green), GeoColors.DimGray);
            _resultLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _layerOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            _layerOverlay.Layers.Add(_cityLimitsLayer);
            _layerOverlay.Layers.Add(_dividedCityLimitsLayer);
            _layerOverlay.Layers.Add(_westRegionLayer);
            _layerOverlay.Layers.Add(_resultLayer);
            Map.Overlays.Add(_layerOverlay);

            _cityLimitsLayer.Open();
            _cityLimits = _cityLimitsLayer.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns)[0];
            var cityLimitsBBox = _cityLimitsLayer.GetBoundingBox();
            _cityLimitsLayer.Close();

            _dividedCityLimitsLayer.Open();
            _cityLimitsPieces = _dividedCityLimitsLayer.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns);
            _dividedCityLimitsLayer.Close();

            Map.CenterPoint = cityLimitsBBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, cityLimitsBBox, Map.MapWidth, Map.MapHeight) * 1.5;

            ShowOperation();
        }

        private void Operation_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            _operation = (string)((RadioButton)sender).Tag;
            ShowOperation();
        }

        private void ShowOperation()
        {
            BufferPanel.Visibility = _operation == "buffer" ? Visibility.Visible : Visibility.Collapsed;
            SimplifyPanel.Visibility = _operation == "simplify" ? Visibility.Visible : Visibility.Collapsed;
            RotatePanel.Visibility = _operation == "rotate" ? Visibility.Visible : Visibility.Collapsed;
            ScalePanel.Visibility = _operation == "scale" ? Visibility.Visible : Visibility.Collapsed;
            OffsetPanel.Visibility = _operation == "offset" ? Visibility.Visible : Visibility.Collapsed;
            DegreePanel.Visibility = _operation == "degree" ? Visibility.Visible : Visibility.Collapsed;

            _dividedCityLimitsLayer.IsVisible = _operation == "union";
            _cityLimitsLayer.IsVisible = _operation != "union";
            _westRegionLayer.IsVisible = _operation == "clip" || _operation == "difference";
            _resultLayer.InternalFeatures.Clear();
            Status.Text = _operation == "union" ? "Each piece of the city limits is its own shape until you run the union." : string.Empty;

            _ = Map.RefreshAsync();
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            var shape = _cityLimits.GetShape();
            Feature result;
            double first;
            double second;

            switch (_operation)
            {
                case "union":
                    result = new Feature(AreaBaseShape.Union(_cityLimitsPieces));
                    _dividedCityLimitsLayer.IsVisible = false;
                    break;
                case "buffer":
                    if (!double.TryParse(BufferDistance.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out first)) { Status.Text = "Enter a number."; return; }
                    result = new Feature(shape.Buffer(first, GeographyUnit.Meter, DistanceUnit.Meter));
                    break;
                case "simplify":
                    if (!double.TryParse(Tolerance.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out first)) { Status.Text = "Enter a number."; return; }
                    result = new Feature(AreaBaseShape.Simplify((AreaBaseShape)shape, first, SimplificationType.DouglasPeucker));
                    break;
                case "rotate":
                    if (!double.TryParse(RotateDegree.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out first)) { Status.Text = "Enter a number."; return; }
                    result = new Feature(BaseShape.Rotate(shape, shape.GetCenterPoint(), (float)first));
                    break;
                case "scale":
                    if (!double.TryParse(ScaleFactor.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out first)) { Status.Text = "Enter a number."; return; }
                    result = new Feature(BaseShape.ScaleTo(shape, first));
                    break;
                case "offset":
                    if (!double.TryParse(TranslateX.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out first)
                        || !double.TryParse(TranslateY.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out second)) { Status.Text = "Enter a number."; return; }
                    result = new Feature(BaseShape.TranslateByOffset(shape, first, second, GeographyUnit.Meter, DistanceUnit.Meter));
                    break;
                case "degree":
                    if (!double.TryParse(TranslateDistance.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out first)
                        || !double.TryParse(TranslateAngle.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out second)) { Status.Text = "Enter a number."; return; }
                    result = new Feature(BaseShape.TranslateByDegree(shape, first, second, GeographyUnit.Meter, DistanceUnit.Meter));
                    break;
                case "clip":
                    result = _cityLimits.GetIntersection(_westRegionLayer.InternalFeatures[0]);
                    break;
                case "difference":
                    result = _cityLimits.GetDifference(_westRegionLayer.InternalFeatures[0]);
                    break;
                case "convexHull":
                    result = _cityLimits.GetConvexHull();
                    break;
                default:
                    result = new Feature(_cityLimits.GetBoundingBox());
                    break;
            }

            Status.Text = string.Empty;
            _resultLayer.InternalFeatures.Clear();
            _resultLayer.InternalFeatures.Add(result);
            _ = _layerOverlay.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
