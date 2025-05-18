using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Windows.Media;
using Newtonsoft.Json;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for MVT.xaml
    /// </summary>
    public partial class MVTtest : IDisposable
    {
        public MVTtest()
        {
            InitializeComponent();
        }

        private string _selectedWvtServer = string.Empty;
        private bool _mapLoaded = false;
        private string _jsonContent = string.Empty;

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;
            _mapLoaded = true;

            _selectedWvtServer = "https://demotiles.maplibre.org/style.json";
            ThinkGeoDebugger.DisplayTileId = true;

            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.MultiTile;

            var mvtLayer = new MvtTilesAsyncLayer(_selectedWvtServer);
            layerOverlay.Layers.Add(mvtLayer);

            await mvtLayer.OpenAsync();
            MapView.CurrentExtent = mvtLayer.GetBoundingBox();
            MapView.Overlays.Add(layerOverlay);

            await MapView.RefreshAsync();
            await LoadAndDisplayJsonContent(_selectedWvtServer);
        }

        private async void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_mapLoaded)
                return;

            _selectedWvtServer = ((ComboBoxItem)e.AddedItems[0])?.Content.ToString();
            await LoadAndDisplayJsonContent(_selectedWvtServer);
            await UpdateMapWithUrl(_selectedWvtServer);
        }

        // Update map via URL
        private async Task UpdateMapWithUrl(string serverUrl)
        {
            try
            {
                StatusTextBlock.Text = "Status: Loading new style....";
                StatusTextBlock.Foreground = Brushes.Orange;

                // Clear existing layers
                MapView.Overlays.Clear();

                // Create new layer and load URL
                var layerOverlay = new LayerOverlay();
                layerOverlay.TileType = TileType.MultiTile;
                var mvtLayer = new MvtTilesAsyncLayer(serverUrl);
                layerOverlay.Layers.Add(mvtLayer);

                await mvtLayer.OpenAsync();
                MapView.CurrentExtent = mvtLayer.GetBoundingBox();
                MapView.Overlays.Add(layerOverlay);
                await MapView.RefreshAsync();

                StatusTextBlock.Text = "Status: Style updated";
                StatusTextBlock.Foreground = Brushes.Green;
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Status: Load failed - {ex.Message}";
                StatusTextBlock.Foreground = Brushes.Red;
            }
        }

        // Fetch and display JSON content
        private async Task LoadAndDisplayJsonContent(string serverUrl)
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string rawJson = await client.DownloadStringTaskAsync(serverUrl);
                    try
                    {
                        // Parse and format JSON
                        JObject jObject = JObject.Parse(rawJson);
                        _jsonContent = jObject.ToString(Formatting.Indented);
                    }
                    catch
                    {
                        // Display raw content if parsing fails
                        _jsonContent = rawJson;
                    }
                    JsonContentTextBox.Text = _jsonContent;
                }
            }
            catch (Exception ex)
            {
                JsonContentTextBox.Text = $"Failed to retrieve JSON content:  {ex.Message}";
            }
        }

        private async void ApplyChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StatusTextBlock.Text = "Status: Applying changes...";
                StatusTextBlock.Foreground = Brushes.Orange;

                // Retrieve modified JSON
                string modifiedJson = JsonContentTextBox.Text;

                // Validate JSON format
                if (!IsValidJson(modifiedJson))
                {
                    StatusTextBlock.Text = "Status: Invalid JSON format!";
                    StatusTextBlock.Foreground = Brushes.Red;
                    return;
                }

                // Create temporary file to save modified JSON
                string tempFilePath = Path.Combine(Path.GetTempPath(), "modified_style.json");
                File.WriteAllText(tempFilePath, modifiedJson);

                // Update map layers
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

        // Update map with modified style
        private async Task UpdateMapWithModifiedStyle(string styleFilePath)
        {
            try
            {
                // Clear existing layers
                MapView.Overlays.Clear();

                // Create new layer overlay
                var layerOverlay = new LayerOverlay();
                layerOverlay.TileType = TileType.MultiTile;

                // Load MVT layer from local file
                var mvtLayer = new MvtTilesAsyncLayer(styleFilePath);
                layerOverlay.Layers.Add(mvtLayer);

                // Add to map and refresh
                MapView.Overlays.Add(layerOverlay);
                await mvtLayer.OpenAsync();
                MapView.CurrentExtent = mvtLayer.GetBoundingBox();
                await MapView.RefreshAsync();
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Status: Map update failed - {ex.Message}";
                StatusTextBlock.Foreground = Brushes.Red;
                throw; // Rethrow exception for caller handling
            }
        }
        
        public void Dispose()
        {
            ThinkGeoDebugger.DisplayTileId = false;
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}