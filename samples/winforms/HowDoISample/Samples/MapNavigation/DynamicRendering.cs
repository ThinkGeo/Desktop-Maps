using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using System.Windows.Media.Animation;
using ThinkGeo.Core;
using Button = System.Windows.Forms.Button;
using Font = System.Drawing.Font;
using Label = System.Windows.Forms.Label;
using Point = System.Drawing.Point;
using Size = System.Drawing.Size;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class DynamicRendering : System.Windows.Forms.UserControl
    {
        private FeatureLayerWpfDrawingOverlay _dynamicOverlay;
        private DynamicPointStyle _customStyle;
        private bool _initialized = false;

        public DynamicRendering()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            mapView.BackColor = System.Drawing.Color.White;
            var usStates = new ShapeFileFeatureLayer(@"./Data/Shapefile/USStates_3857.shp");
            usStates.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen = GeoPens.Black;
            usStates.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            layerOverlay.Layers.Add(usStates);
            mapView.Overlays.Add(layerOverlay);

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
            _customStyle.FillBrush = new GeoSolidBrush(GeoColor.FromArgb(128, 255, 0, 0));
            _customStyle.Year = 1950;
            statePopulationFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = _customStyle;
            statePopulationFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _dynamicOverlay = new FeatureLayerWpfDrawingOverlay();
            _dynamicOverlay.FeatureLayers.Add(statePopulationFeatureLayer);
            mapView.Overlays.Add(_dynamicOverlay);

            // Set the map extent
            mapView.CenterPoint = new PointShape(-10650000, 4770000);
            mapView.CurrentScale = 37000000;

            mapView.MapResizeMode = MapResizeMode.PreserveScaleAndCenter;
            _initialized = true;
            _ = mapView.RefreshAsync();

            // Initialize the animation with the default starting value
            var fakeArgs = new RoutedPropertyChangedEventArgs<double>(
                           playSlider.Minimum, playSlider.Value);
            PlaySlider_ValueChanged(playSlider, fakeArgs);
        }

        private void PlayAnimation_Click(object sender, EventArgs e)
        {
            var animation = new DoubleAnimation
            {
                From = playSlider.Minimum,
                To = playSlider.Maximum,
                Duration = TimeSpan.FromSeconds(3),
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
            };

            playSlider.BeginAnimation(Slider.ValueProperty, animation);
        }

        private void PlaySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (!_initialized)
                return;

            _customStyle.Year = (int)e.NewValue;
            _ = _dynamicOverlay.RefreshAsync();
            titileLabel.Text = $"State Population in {(int)e.NewValue:F0}";
        }


        #region Component Designer generated code

        private MapView mapView;
        private Label titileLabel;
        private Button playButton;
        private Slider playSlider;
        private ElementHost sliderHost;

        private void InitializeComponent()
        {
            mapView = new ThinkGeo.UI.WinForms.MapView();
            titileLabel = new Label();
            playButton = new Button();
            playSlider = new Slider();
            sliderHost = new ElementHost();
            SuspendLayout();
            // 
            // mapView
            // 
            mapView.Dock = DockStyle.Fill;
            mapView.BackColor = System.Drawing.Color.White;
            mapView.CurrentScale = 0D;
            mapView.Location = new System.Drawing.Point(0, 0);
            mapView.MapResizeMode = MapResizeMode.PreserveScale;
            mapView.MaximumScale = 1.7976931348623157E+308D;
            mapView.MinimumScale = 200D;
            mapView.Name = "mapView";
            mapView.RestrictExtent = null;
            mapView.RotationAngle = 0F;
            // 
            // titileLabel
            // 
            titileLabel.Font = new Font("Microsoft Sans Serif", 20F);
            titileLabel.Name = "titileLabel";
            titileLabel.Size = new Size(500, 50);
            titileLabel.TabIndex = 0;
            titileLabel.Text = "titileLabel";
            titileLabel.Anchor = AnchorStyles.Top;
            titileLabel.Top = 30;
            titileLabel.Left = mapView.Width / 2 + 60;
            // 
            // playButton
            // 
            int dpiScale = DpiHelper.GetDpiScalePercentage(mapView);
            int verticalOffset = dpiScale >= 150 ? 17 : 9;
            playButton.AutoSize = true;
            playButton.Name = "playButton";
            playButton.TabIndex = 2;
            playButton.Text = "Play";
            playButton.UseVisualStyleBackColor = true;
            playButton.Anchor = AnchorStyles.Bottom;
            playButton.Left = mapView.Width / 2 + 310;
            playButton.Top = mapView.Bottom + verticalOffset;
            playButton.Click += PlayAnimation_Click;
            // 
            // playSlider
            // 
            playSlider.Minimum = 1950;
            playSlider.Maximum = 2024;
            playSlider.Width = 200;
            playSlider.Height = 30;
            playSlider.Background = System.Windows.Media.Brushes.LightBlue;
            playSlider.Orientation = System.Windows.Controls.Orientation.Horizontal;
            playSlider.Name = "playTrackBar";
            playSlider.ValueChanged += PlaySlider_ValueChanged;
            // 
            // sliderHost
            // 
            sliderHost.Size = new Size(200, 30);
            sliderHost.Child = playSlider;
            sliderHost.BackColorTransparent = true;
            sliderHost.Anchor = AnchorStyles.Bottom;
            sliderHost.Left = mapView.Width / 2 + 100;
            sliderHost.Top = mapView.Bottom + 10;
            // 
            // DynamicRendering
            // 
            Controls.Add(mapView);
            Controls.Add(titileLabel);
            Controls.Add(playButton);
            Controls.Add(sliderHost);
            Name = "DynamicRendering";
            Load += Form_Load;
            Size = new Size(1314, 710);
            ResumeLayout(false);

            titileLabel.BringToFront();
            sliderHost.BringToFront();
            playButton.BringToFront();
        }

        #endregion Component Designer generated code

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
