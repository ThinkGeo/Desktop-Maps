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
        private MapStyleLoader _loader = new MapStyleLoader();
        private LayerOverlay _layerOverlay = new LayerOverlay();
        private string _selectedStylePath = "./Data/Json/styleJsonfill.json"; // 直接指定默认路径
        private ShapeFileFeatureLayer parksLayer;
        private ShapeFileFeatureLayer streetsLayer;
        private ShapeFileFeatureLayer hotelsLayer;

        public StyleJsonfill()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;

            // 初始化图层（类成员变量）
            parksLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");
            streetsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Streets.shp");
            hotelsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Hotels.shp");

            // 投影转换
            parksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            streetsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            hotelsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // 加载并应用样式
            await _loader.LoadAsync(_selectedStylePath);
            _loader.ApplyStyle("Parks.shp", parksLayer.ZoomLevelSet);
            _loader.ApplyStyle("Streets.shp", streetsLayer.ZoomLevelSet);
            _loader.ApplyStyle("hotels.shp", hotelsLayer.ZoomLevelSet);

            // 添加图层到叠加层
            _layerOverlay = new LayerOverlay();
            _layerOverlay.Layers.Add(parksLayer);
            _layerOverlay.Layers.Add(streetsLayer);
            _layerOverlay.Layers.Add(hotelsLayer);
            _layerOverlay.TileType = TileType.SingleTile;
            MapView.Overlays.Add(_layerOverlay);

            // 设置地图范围和缩放
            MapView.CenterPoint = new PointShape(-10777290, 3908740);
            MapView.CurrentScale = 9000;

            // 读取并显示 JSON 内容
            string jsonContent = File.ReadAllText(_selectedStylePath);
            JsonContentTextBox.Text = JObject.Parse(jsonContent).ToString(Formatting.Indented);

            _ = MapView.RefreshAsync();
        }

        private async void ApplyChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                StatusTextBlock.Text = "Status: Applying changes...";
                StatusTextBlock.Foreground = Brushes.Orange;

                string modifiedJson = JsonContentTextBox.Text;
                bool isJsonValid = IsValidJson(modifiedJson); // 先获取判断结果
                if (!isJsonValid) // 明确判断
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

        private async Task UpdateMapWithModifiedStyle(string styleFilePath)
        {
            try
            {
                StatusTextBlock.Text = "Status: Updating map with modified style...";
                StatusTextBlock.Foreground = Brushes.Orange;

                // 清除现有地图覆盖层
                MapView.Overlays.Clear();
                _layerOverlay.Layers.Clear();

                // 重新初始化图层
                parksLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");
                streetsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Streets.shp");
                hotelsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Hotels.shp");

                // 投影转换
                parksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
                streetsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
                hotelsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

                // 加载并应用样式
                await _loader.LoadAsync(styleFilePath);
                _loader.ApplyStyle("Parks.shp", parksLayer.ZoomLevelSet);
                _loader.ApplyStyle("Streets.shp", streetsLayer.ZoomLevelSet);
                _loader.ApplyStyle("hotels.shp", hotelsLayer.ZoomLevelSet);

                // 重新构建图层覆盖层
                _layerOverlay = new LayerOverlay();
                _layerOverlay.Layers.Add(parksLayer);
                _layerOverlay.Layers.Add(streetsLayer);
                _layerOverlay.Layers.Add(hotelsLayer);
                _layerOverlay.TileType = TileType.SingleTile;
                MapView.Overlays.Add(_layerOverlay);

                // 刷新地图
                await MapView.RefreshAsync();

                StatusTextBlock.Text = "Status: Map updated with modified style";
                StatusTextBlock.Foreground = Brushes.Green;
            }
            catch (Exception ex)
            {
                StatusTextBlock.Text = $"Status: Map update failed - {ex.Message}";
                StatusTextBlock.Foreground = Brushes.Red;
                throw; // 重新抛出异常，以便调用者处理
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