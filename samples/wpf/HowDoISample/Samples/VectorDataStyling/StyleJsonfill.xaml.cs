using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class StyleJsonfill : IDisposable
    {
        private StyleJsonLoader _loader = new StyleJsonLoader();
        private LayerOverlay _layerOverlay = new LayerOverlay();
        private ShapeFileFeatureLayer parksLayer;
        private ShapeFileFeatureLayer streetsLayer;
        private ShapeFileFeatureLayer hotelsLayer;
        private string _allPropertiesh = "./Data/Json/Fill_AllProperties.json";
        private string _supported = "./Data/Json/Fill_Supported.json";
        private string _unsupported = "./Data/Json/Fill_Unsupported.json";
        private bool _isUnsupportedMode = false; 

        public StyleJsonfill()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;

            parksLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");
            streetsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Streets.shp");
            hotelsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Hotels.shp");

            parksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            streetsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            hotelsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            await _loader.LoadAsync(_allPropertiesh);
            _loader.ApplyStyle("Parks.shp", parksLayer.ZoomLevelSet);
            _loader.ApplyStyle("Streets.shp", streetsLayer.ZoomLevelSet);
            _loader.ApplyStyle("hotels.shp", hotelsLayer.ZoomLevelSet);

            _layerOverlay = new LayerOverlay();
            _layerOverlay.Layers.Add(parksLayer);
            _layerOverlay.Layers.Add(streetsLayer);
            _layerOverlay.Layers.Add(hotelsLayer);
            _layerOverlay.TileType = TileType.SingleTile;
            MapView.Overlays.Add(_layerOverlay);

            MapView.CenterPoint = new PointShape(-10777290, 3908740);
            MapView.CurrentScale = 9000;

            string jsonContent = File.ReadAllText(_allPropertiesh);
            JsonContentTextBox.Text = JObject.Parse(jsonContent).ToString(Formatting.Indented);

            _ = MapView.RefreshAsync();
        }

        private async void ApplyChangesButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isUnsupportedMode)
            {
                StatusTextBlock.Text = "Status: Cannot apply unsupported properties!";
                StatusTextBlock.Foreground = Brushes.Red;
                return;
            }
            try
            {
                StatusTextBlock.Text = "Status: Applying changes...";
                StatusTextBlock.Foreground = Brushes.Orange;

                string modifiedJson = JsonContentTextBox.Text;
                bool isJsonValid = IsValidJson(modifiedJson); 
                if (!isJsonValid) 
                {
                    StatusTextBlock.Text = "Status: Invalid JSON format!";
                    StatusTextBlock.Foreground = Brushes.Red;
                    return;
                }

                string tempFilePath = Path.Combine(Path.GetTempPath(), "modified_style.json");
                File.WriteAllText(tempFilePath, modifiedJson);

                await UpdateMapWithModifiedStyle(tempFilePath);

                StatusTextBlock.Text = "Status: Changes applied";
                StatusTextBlock.Foreground = Brushes.Green;
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Status: Error - {ex.Message}";
                StatusTextBlock.Foreground = Brushes.Red;
            }
        }

        private async void AllProperties_Click(object sender, RoutedEventArgs e)
        {
            _isUnsupportedMode = false; 
            await LoadAndApplyStyle(_allPropertiesh); 
        }

        private async void SupportedProperties_Click(object sender, RoutedEventArgs e)
        {
            _isUnsupportedMode = false; 
            await LoadAndApplyStyle(_supported);
        }

        private async void UnsupportedProperties_Click(object sender, RoutedEventArgs e)
        {
            _isUnsupportedMode = true; 
            await LoadJsonToTextBox(_unsupported);
        }

        private async Task UpdateMapWithModifiedStyle(string styleFilePath)
        {
            try
            {
                StatusTextBlock.Text = "Status: Updating map with modified style...";
                StatusTextBlock.Foreground = Brushes.Orange;

                MapView.Overlays.Clear();
                _layerOverlay.Layers.Clear();

                parksLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");
                streetsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Streets.shp");
                hotelsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Hotels.shp");

                parksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
                streetsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
                hotelsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

                await _loader.LoadAsync(styleFilePath);
                _loader.ApplyStyle("Parks.shp", parksLayer.ZoomLevelSet);
                _loader.ApplyStyle("Streets.shp", streetsLayer.ZoomLevelSet);
                _loader.ApplyStyle("hotels.shp", hotelsLayer.ZoomLevelSet);

                _layerOverlay = new LayerOverlay();
                _layerOverlay.Layers.Add(parksLayer);
                _layerOverlay.Layers.Add(streetsLayer);
                _layerOverlay.Layers.Add(hotelsLayer);
                _layerOverlay.TileType = TileType.SingleTile;
                MapView.Overlays.Add(_layerOverlay);

                await MapView.RefreshAsync();

                StatusTextBlock.Text = "Status: Map updated with modified style";
                StatusTextBlock.Foreground = Brushes.Green;
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Status: Map update failed - {ex.Message}";
                StatusTextBlock.Foreground = Brushes.Red;
                throw; 
            }
        }

        private async Task LoadAndApplyStyle(string styleFilePath)
        {
            try
            {
                StatusTextBlock.Text = $"Status: Loading {Path.GetFileName(styleFilePath)}...";
                StatusTextBlock.Foreground = Brushes.Orange;

                string jsonContent = File.ReadAllText(styleFilePath);
                JsonContentTextBox.Text = JObject.Parse(jsonContent).ToString(Formatting.Indented);

                await UpdateMapWithModifiedStyle(styleFilePath);

                StatusTextBlock.Text = $"Status: {Path.GetFileName(styleFilePath)} applied";
                StatusTextBlock.Foreground = Brushes.Green;
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Status: Error - {ex.Message}";
                StatusTextBlock.Foreground = Brushes.Red;
            }
        }

        private async Task LoadJsonToTextBox(string styleFilePath)
        {
            try
            {
                StatusTextBlock.Text = $"Status: Loading {Path.GetFileName(styleFilePath)}...";
                StatusTextBlock.Foreground = Brushes.Orange;

                string jsonContent = File.ReadAllText(styleFilePath);
                JsonContentTextBox.Text = JObject.Parse(jsonContent).ToString(Formatting.Indented);

                StatusTextBlock.Text = $"Status: {Path.GetFileName(styleFilePath)} loaded (map unchanged)";
                StatusTextBlock.Foreground = Brushes.Green;
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Status: Error - {ex.Message}";
                StatusTextBlock.Foreground = Brushes.Red;
            }
        }


        private bool IsValidJson(string json)
        {
            try
            {
                JToken.Parse(json);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            ThinkGeoDebugger.DisplayTileId = false;
            MapView.Dispose();
            GC.SuppressFinalize(this);
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}