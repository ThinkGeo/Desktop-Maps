using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Threading;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace PerformanceSample
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private const int featureRowCount = 25;
        private const int featureColumnCount = 200;
        private const int layerCount = 4;

        private int rate;
        private int updateFeatureCount;
        private Timer featureUpdateTimer;
        private Random featureUpdateRadom;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Rate
        {
            get
            {
                return rate;
            }
            set
            {
                rate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rate"));
            }
        }

        public int UpdateFeatureCount
        {
            get
            {
                return updateFeatureCount;
            }
            set
            {
                updateFeatureCount = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("UpdateFeatureCount"));
            }
        }

        public ObservableCollection<InMemoryFeatureLayer> Layers { get; private set; }

        public MainWindow()
        {
            DataContext = this;
            Rate = 1000;
            UpdateFeatureCount = 1600;
            Layers = new ObservableCollection<InMemoryFeatureLayer>();
            InitializeComponent();

            AllFeatureCountTextBlock.Text = (featureRowCount * featureColumnCount * layerCount).ToString();
            featureUpdateTimer = new Timer()
            {
                AutoReset = true
            };
            featureUpdateTimer.Elapsed += FeatureUpdateTimer_Elapsed;
            featureUpdateRadom = new Random();

            Map.CurrentExtentChanged += Map_CurrentExtentChanged;
        }

        private void MainForm_Loaded(object sender, RoutedEventArgs e)
        {
            // set map properties
            Map.MinimumScale = 0;
            Map.MapUnit = GeographyUnit.DecimalDegree;
            Map.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.StandardColors.AliceBlue);
            Map.MapTools.Logo.IsEnabled = false;

            // create source overlay and layer style
            var sourceOverlay = new LayerOverlay()
            {
                TileType = TileType.SingleTile,
                DrawingQuality = DrawingQuality.HighSpeed
            };

            var layerStyle = new ValueStyle("State", new Collection<ValueItem>()
            {
                new ValueItem("0", new AreaStyle(GeoPens.Black, GeoBrushes.Green)),
                new ValueItem("1", new AreaStyle(GeoPens.Black, GeoBrushes.Yellow)),
                new ValueItem("2", new AreaStyle(GeoPens.Black, GeoBrushes.Red)),
                new ValueItem("3", new AreaStyle(GeoPens.Black, GeoBrushes.Orange))
            });

            for (int layerIndex = 0; layerIndex < layerCount; layerIndex++)
            {
                // create source layer
                var sourceLayer = new InMemoryFeatureLayer()
                {
                    Name = $"Source Layer #{layerIndex}"
                };
                sourceLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(layerStyle);
                sourceLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // add rectangle shape features to source layer, calculate position and size of each rectangle shapes
                double featureInterval = 1.0;
                double featureSize = 1.0;
                for (int row = 0; row < featureRowCount; row++)
                {
                    for (int column = 0; column < featureColumnCount; column++)
                    {
                        double upperLeftX = column * (featureSize + featureInterval);
                        double upperLeftY = (layerIndex * featureRowCount + row) * (featureSize + featureInterval);
                        var feature = new Feature(new PolygonShape(new RingShape(new Collection<Vertex>()
                        {
                            new Vertex(upperLeftX, upperLeftY),
                            new Vertex(upperLeftX, upperLeftY + featureSize),
                            new Vertex(upperLeftX + featureSize, upperLeftY + featureSize),
                            new Vertex(upperLeftX + featureSize, upperLeftY)
                        })))
                        {
                            Id = $"{column}.{row}"
                        };
                        feature.ColumnValues.Add("State", "0");
                        sourceLayer.InternalFeatures.Add(feature.Id, feature);
                    }
                }

                sourceOverlay.Layers.Add(sourceLayer);
                Layers.Add(sourceLayer);
            }

            // refresh the map
            Map.Overlays.Add("SourceOverlay", sourceOverlay);
            Map.CurrentExtent = sourceOverlay.GetBoundingBox();
            Map.Refresh();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Map.Refresh(Map.CurrentExtent, Map.Overlays["SourceOverlay"]);
        }

        private void FeatureUpdateTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // create a new focus layer that use the layer style from any source layers
            var sourceOverlay = Map.Overlays["SourceOverlay"] as LayerOverlay;
            var layerStyle = (sourceOverlay.Layers.First() as InMemoryFeatureLayer).ZoomLevelSet.ZoomLevel01.CustomStyles.First();
            var focusLayer = new InMemoryFeatureLayer();
            focusLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(layerStyle);
            focusLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // change column value of any valid source features randomly, then add the feature to new focus layer
            for (int i = 0; i < UpdateFeatureCount; i++)
            {
                var sourceLayer = sourceOverlay.Layers[featureUpdateRadom.Next(0, layerCount)] as InMemoryFeatureLayer;
                if (sourceLayer.IsVisible)
                {
                    var sourceFeature = sourceLayer.InternalFeatures[$"{featureUpdateRadom.Next(0, featureColumnCount)}.{featureUpdateRadom.Next(0, featureRowCount)}"];
                    sourceFeature.ColumnValues["State"] = ((Convert.ToInt32(sourceFeature.ColumnValues["State"]) + 1) % 5).ToString();
                    focusLayer.InternalFeatures.Add(sourceFeature);
                }
            }

            Dispatcher.Invoke(new Action(() =>
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // save the old overlay as a geoImage, then remove it
                GeoImage focusOverlayBackgroundImage = null;
                if (Map.Overlays.Contains("FocusOverlay"))
                {
                    var dirtyFocusOverlay = Map.Overlays["FocusOverlay"] as CustomLayerOverlay;
                    focusOverlayBackgroundImage = dirtyFocusOverlay.ToGeoImage(Map.CurrentExtent, Map.MapUnit);
                    Map.Overlays.Remove(dirtyFocusOverlay);
                    dirtyFocusOverlay.Dispose();
                }

                // create a new FocusOverlay, add it to map, then refresh it
                var focusOverlay = new CustomLayerOverlay(focusOverlayBackgroundImage)
                {
                    TileType = TileType.SingleTile,
                    DrawingQuality = DrawingQuality.HighSpeed
                };
                focusOverlay.Layers.Add(focusLayer);
                Map.Overlays.Add("FocusOverlay", focusOverlay);
                Map.Refresh(Map.CurrentExtent, focusOverlay);

                stopwatch.Stop();
                InformationTextBlock.Text = $"Refresh Time (ms): {stopwatch.ElapsedMilliseconds}";
            }));
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            featureUpdateTimer.Interval = Rate;
            featureUpdateTimer.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            featureUpdateTimer.Stop();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            featureUpdateTimer.Stop();

            // remove the FocusOverlay
            if (Map.Overlays.Contains("FocusOverlay"))
            {
                var dirtyRenderOverlay = Map.Overlays["FocusOverlay"] as CustomLayerOverlay;
                Map.Overlays.Remove(dirtyRenderOverlay);
                dirtyRenderOverlay.Dispose();
            }

            // reset the column values of all features
            var sourceLayers = (Map.Overlays["SourceOverlay"] as LayerOverlay).Layers;
            foreach (InMemoryFeatureLayer sourceLayer in sourceLayers)
            {
                foreach (var feature in sourceLayer.InternalFeatures)
                {
                    feature.ColumnValues["State"] = "0";
                }
            }

            Map.Refresh();
        }

        private void Map_CurrentExtentChanged(object sender, CurrentExtentChangedWpfMapEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                // remove the FocusOverlay
                if (Map.Overlays.Contains("FocusOverlay"))
                {
                    var dirtyForusOverlay = Map.Overlays["FocusOverlay"] as CustomLayerOverlay;
                    Map.Overlays.Remove(dirtyForusOverlay);
                    dirtyForusOverlay.Dispose();
                }
            }));
        }

        private void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
