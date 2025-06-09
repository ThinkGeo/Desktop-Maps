using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to set the map extent using a variety of different methods.
    /// </summary>
    public partial class DynamicRendering : IDisposable
    {
        private FeatureLayerWpfDrawingOverlay _dynamicOverlay;
        private DynamicPointStyle _customStyle;
        private bool _initialized = false;

        public DynamicRendering()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map and a shapefile with simple data to work with
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;

            MapView.Background = Brushes.White;
            var usStates = new ShapeFileFeatureLayer(@"./Data/Shapefile/USStates_3857.shp");
            usStates.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen = GeoPens.Black;
            usStates.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            layerOverlay.Layers.Add(usStates);
            MapView.Overlays.Add(layerOverlay);

            usStates.Open();
            var allFeatures = usStates.QueryTools.GetAllFeatures(ReturningColumnsType.AllColumns);

            var statePopulationFeatureLayer = new InMemoryFeatureLayer();
            foreach (var feature in allFeatures)
            {
                var centerPoint = feature.GetShape().GetCenterPoint();
                statePopulationFeatureLayer.InternalFeatures.Add(new Feature(centerPoint, feature.ColumnValues));
            }

            _customStyle = new DynamicPointStyle();
            _customStyle.LoadHistoricalData("./Data/historical_state_population_by_year.csv");
            _customStyle.OutlinePen = GeoPens.Red;
            _customStyle.FillBrush = new GeoSolidBrush(GeoColor.FromArgb(128, 255,0,0));
            _customStyle.Year = 1950;
            statePopulationFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = _customStyle;
            statePopulationFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _dynamicOverlay = new FeatureLayerWpfDrawingOverlay();
            _dynamicOverlay.FeatureLayers.Add(statePopulationFeatureLayer);
            MapView.Overlays.Add(_dynamicOverlay);

            // Set the map extent
            MapView.CenterPoint = new PointShape(-10650000, 4770000);
            MapView.CurrentScale = 37000000;

            _ = MapView.RefreshAsync();
            _initialized = true;
        }

        private void PlayAnimation_Click(object sender, RoutedEventArgs e)
        {
            var animation = new DoubleAnimation
            {
                From = MySlider.Minimum,
                To = MySlider.Maximum,
                Duration = TimeSpan.FromSeconds(3),
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
            };

            MySlider.BeginAnimation(Slider.ValueProperty, animation);
        }
        private void MySlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_initialized)
                return;

            _customStyle.Year = (int)e.NewValue;
            _ = _dynamicOverlay.RefreshAsync();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }

    class DynamicPointStyle : PointStyle
    {
        public Dictionary<(string, int), int> HistoricalData;
        public int Year;

        public DynamicPointStyle()
        {
            HistoricalData = new Dictionary<(string, int), int>();
        }

        public void LoadHistoricalData(string dataFilePath)
        {
            using (var reader = new StreamReader(dataFilePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] fields = line.Split(',');

                    var state = fields[0];
                    var year = Convert.ToInt32(fields[1]);
                    var population = Convert.ToInt32(fields[2]);

                    HistoricalData.Add((state, year), population);
                }
            }
        }

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            foreach (var feature in features)
            {
                var state = feature.ColumnValues["STATE_ABBR"];
                var population = HistoricalData[(state, Year)];
                var radius = (population / 300000); // Get the radius based on the population

                var geoColor = GetColorFromValue(radius); // Get the radius based on the radius
                var outlinePen = new GeoPen(geoColor);
                var fillBrush = new GeoSolidBrush(GeoColor.FromArgb(128, geoColor));

                canvas.DrawEllipse(feature, radius, radius, outlinePen, fillBrush, this.DrawingLevel);
            }
        }

        public static GeoColor GetColorFromValue(double value)
        {
            // Clamp between 0 and 100
            value = Math.Max(0, Math.Min(150, value));

            // Interpolate red to green
            byte r = (byte)(255 * (1 - value / 150)); // Red fades out
            byte g = (byte)(255 * (value / 150));     // Green fades in

            return GeoColor.FromArgb(255, r, g, 0); // Blue is 0 throughout
        }
    }
}