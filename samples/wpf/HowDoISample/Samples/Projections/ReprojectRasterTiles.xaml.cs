using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// One lesson, two servers: the same LayerOverlay + GdalProjectionConverter recipe
    /// reprojects any XYZ raster service - only the tile source differs, so switching
    /// server never moves the view. Classic rendering on purpose: reprojection lives in
    /// the layer's DRAW path (tiles are fetched on the native grid, merged, and warped
    /// as they draw), while the GPU source path serves native-grid tiles only - so this
    /// row is the classic path's own ability. Caching needs no wiring here either: each
    /// layer carries its data-level tile cache by default.
    /// </summary>
    public partial class ReprojectRasterTiles
    {
        private bool _initialized;
        private LayerOverlay _overlay;
        private RasterXyzTileAsyncLayer _layer;
        private int _epsg = 3857;

        public ReprojectRasterTiles()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            _overlay = new LayerOverlay { TileType = TileType.SingleTile };
            Map.Overlays.Add(_overlay);
            ApplyServer();

            // A neighborhood in Frisco Texas.
            Map.CenterPoint = new PointShape(-10777600, 3910900);
            Map.CurrentScale = 1000000;
            _ = Map.RefreshAsync();
        }

        private void ApplyServer()
        {
            RasterXyzTileAsyncLayer layer = ServerCombo.SelectedIndex == 1
                ? new OpenStreetMapAsyncLayer("ThinkGeo Samples")
                : new ThinkGeoRasterMapsAsyncLayer(SampleKeys.ClientId, SampleKeys.ClientSecret,
                    ThinkGeoCloudRasterMapsMapType.Light_V2_X1);
            if (_epsg == 4326)
            {
                layer.ProjectionConverter = new GdalProjectionConverter(3857, 4326);
            }

            _layer = layer;
            _overlay.Layers.Clear();
            _overlay.Layers.Add(layer);
        }

        private void Server_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_initialized) return;

            // Same view, different tiles: swapping the server must not move the map.
            ApplyServer();
            _ = Map.RefreshAsync();
        }

        private async void Projection_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            try
            {
                var target = (string)((RadioButton)sender).Tag == "4326" ? 4326 : 3857;
                if (target == _epsg) return;

                // Carry the view across: convert the current extent into the target
                // projection so the map keeps looking at the same ground.
                var view = new ProjectionConverter(_epsg, target);
                view.Open();
                var extent = view.ConvertToExternalProjection(Map.CurrentExtent);
                view.Close();

                _epsg = target;
                Map.MapUnit = target == 4326 ? GeographyUnit.DecimalDegree : GeographyUnit.Meter;
                ApplyServer();
                Map.CenterPoint = extent.GetCenterPoint();
                Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, extent, Map.MapWidth, Map.MapHeight);
                await Map.RefreshAsync();
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }
    }
}
