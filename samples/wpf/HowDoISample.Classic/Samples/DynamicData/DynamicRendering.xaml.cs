using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Redraw a circle per state, sized by its population in the year on the slider, with a custom PointStyle.
    /// </summary>
    public partial class DynamicRendering : IDisposable
    {
        private bool _initialized;
        private FeatureLayerWpfDrawingOverlay _populationOverlay;
        private PopulationPointStyle _populationStyle;

        public DynamicRendering()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            Map.MapUnit = GeographyUnit.Meter;
            Map.Background = Brushes.White;

            var usStates = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "USStates_3857.shp"));
            usStates.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen = GeoPens.Black;
            usStates.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var statesOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            statesOverlay.Layers.Add(usStates);
            Map.Overlays.Add(statesOverlay);

            var stateCenters = new InMemoryFeatureLayer();
            usStates.Open();
            foreach (var state in usStates.QueryTools.GetAllFeatures(ReturningColumnsType.AllColumns))
            {
                stateCenters.InternalFeatures.Add(new Feature(state.GetShape().GetCenterPoint(), state.ColumnValues));
            }
            usStates.Close();

            _populationStyle = new PopulationPointStyle { Year = (int)YearSlider.Value };
            // Each line is state,year,population.
            foreach (var line in File.ReadLines(Path.Combine(AppContext.BaseDirectory, "Data", "historical_state_population_by_year.csv")))
            {
                var fields = line.Split(',');
                if (fields.Length < 3
                    || !int.TryParse(fields[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out var year)
                    || !int.TryParse(fields[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out var population))
                {
                    continue;
                }
                _populationStyle.Populations[(fields[0], year)] = population;
            }
            stateCenters.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = _populationStyle;
            stateCenters.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _populationOverlay = new FeatureLayerWpfDrawingOverlay();
            _populationOverlay.FeatureLayers.Add(stateCenters);
            Map.Overlays.Add(_populationOverlay);

            Map.CenterPoint = new PointShape(-10650000, 4770000);
            Map.CurrentScale = 37000000;

            _initialized = true;
            _ = Map.RefreshAsync();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            // FillBehavior.Stop hands the slider back to the user when the animation ends.
            var animation = new DoubleAnimation
            {
                From = YearSlider.Minimum,
                To = YearSlider.Maximum,
                Duration = TimeSpan.FromSeconds(3),
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut },
                FillBehavior = FillBehavior.Stop
            };
            animation.Completed += (s, args) => YearSlider.Value = YearSlider.Maximum;
            YearSlider.BeginAnimation(Slider.ValueProperty, animation);
        }

        private void YearSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_initialized) return;

            _populationStyle.Year = (int)Math.Round(e.NewValue);
            _ = _populationOverlay.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    // A circle whose radius and color follow the state's population in Year: red when small, green when large.
    internal class PopulationPointStyle : PointStyle
    {
        public Dictionary<(string State, int Year), int> Populations { get; } = new Dictionary<(string State, int Year), int>();

        public int Year { get; set; }

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            foreach (var feature in features)
            {
                if (!Populations.TryGetValue((feature.ColumnValues["STATE_ABBR"], Year), out var population)) continue;

                var radius = population / 300000.0;
                var share = Math.Max(0, Math.Min(150, radius)) / 150;
                var color = GeoColor.FromArgb(255, (byte)(255 * (1 - share)), (byte)(255 * share), 0);

                canvas.DrawEllipse(feature, (float)radius, (float)radius, new GeoPen(color), new GeoSolidBrush(GeoColor.FromArgb(128, color)), DrawingLevel);
            }
        }

        protected override Collection<string> GetRequiredColumnNamesCore()
        {
            return new Collection<string> { "STATE_ABBR" };
        }
    }
}
