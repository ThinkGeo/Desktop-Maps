using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Contour lines interpolated from scattered point values, labelled, with optional filled bands.
    /// </summary>
    public partial class Isolines
    {
        private readonly GpuBasemap _basemap;
        private readonly MapStyle _style;
        private readonly Dictionary<PointShape, double> _points;
        private readonly RectangleShape _dataBounds;

        private PointValueSource _isolineSource;
        private bool _initialized;
        private bool _fillBands;
        private bool _showLabels = true;
        private double _searchRadius;   // 0 = every point
        private int _levelCount = 25;
        private float _lineWidth = 3f;
        private double _power = 2.0;

        public Isolines()
        {
            InitializeComponent();

            Map.MapUnit = GeographyUnit.Meter;
            Map.IsTiltEnabled = true;

            _points = LoadCsv(Path.Combine(AppContext.BaseDirectory, "Data", "Csv", "Frisco_Mosquitos.csv"));
            var xs = _points.Keys.Select(p => p.X).ToList();
            var ys = _points.Keys.Select(p => p.Y).ToList();
            _dataBounds = new RectangleShape(xs.Min(), ys.Max(), xs.Max(), ys.Min());

            var pointTiles = new FeatureSourceVectorTileSource();
            pointTiles.FeatureSources.Add("points", new InMemoryFeatureSource(
                Array.Empty<FeatureSourceColumn>(), _points.Keys.Select(point => new Feature(point)).ToList()));

            _style = new MapStyle(ThinkGeoVectorStyles.Dark, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) })
                .AddStyle((string)FindResource("DataPointsStyle"), pointTiles);
            _basemap = new GpuBasemap(_style);
            Map.Basemap = _basemap;
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0)
            {
                return;
            }

            _initialized = true;
            var start = (RectangleShape)_dataBounds.CloneDeep();
            start.ScaleUp(60);
            Map.CenterPoint = start.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, start, Map.MapWidth, Map.MapHeight);
            ApplyIsolines();
            await Map.RefreshAsync();
        }

        private void FillToggle_Changed(object sender, RoutedEventArgs e)
        {
            _fillBands = (sender as CheckBox)?.IsChecked == true;
            ApplyIsolines();
        }

        private void LabelToggle_Changed(object sender, RoutedEventArgs e)
        {
            _showLabels = (sender as CheckBox)?.IsChecked == true;
            ApplyIsolines();
        }

        private void PointToggle_Changed(object sender, RoutedEventArgs e)
        {
            if (_initialized)
            {
                _style.SetLayerVisibility("data-points", PointToggle.IsChecked == true);
            }
        }

        private void LevelSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _levelCount = (int)e.NewValue;
            ApplyIsolines();
        }

        private void WidthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _lineWidth = (float)e.NewValue;
            ApplyIsolines();
        }

        private void PowerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _power = e.NewValue;
            ApplyIsolines();
        }

        private void RadiusSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _searchRadius = e.NewValue * (_dataBounds?.Width ?? 0);
            ApplyIsolines();
        }

        private void ApplyIsolines()
        {
            if (!_initialized)
            {
                return;
            }

            // Breaks over the data, colored light blue to red.
            var levels = IsoLineLayer.GetIsoLineLevels(_points, _levelCount);
            var colors = GeoColor.GetColorsInQualityFamily(
                GeoColor.FromHtml("#4EA8FF"), GeoColors.Red, levels.Count, ColorWheelDirection.Clockwise);

            var display = new IsolineStyle
            {
                Levels = levels.Select((level, i) => new ColorStop(level, colors[i])).ToList(),
                LineWidthInPixel = _lineWidth,
                FillOpacity = _fillBands ? 0.45f : 0f,
                ShowLabels = _showLabels,
                LabelEveryNthLevel = 2,
                LabelColor = GeoColor.FromHtml("#E7E9EC"),
                LabelHaloColor = GeoColor.FromHtml("#1E2329"),
            };

            // The document did not change, so SetStyleAsync only re-sends the isoline
            // registration; no tile is rebuilt.
            if (_isolineSource != null)
            {
                _style.RemoveIsolines(_isolineSource);
            }

            _isolineSource = new PointValueSource(_points) { Power = _power, SearchRadius = _searchRadius };
            _style.AddIsolines(_isolineSource, display);
            _ = _basemap.SetStyleAsync(_style);

            Status.Text = string.Format(
                CultureInfo.InvariantCulture,
                "{0:N0} points, {1} levels",
                _points.Count, levels.Count);
        }

        private static Dictionary<PointShape, double> LoadCsv(string path)
        {
            var points = new Dictionary<PointShape, double>();
            foreach (var line in File.ReadLines(path))
            {
                var parts = line.Split(',');
                if (parts.Length < 3)
                {
                    continue;
                }

                points[new PointShape(
                    double.Parse(parts[0], CultureInfo.InvariantCulture),
                    double.Parse(parts[1], CultureInfo.InvariantCulture))] =
                    double.Parse(parts[2], CultureInfo.InvariantCulture);
            }

            return points;
        }
    }
}
