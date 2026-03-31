using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Show the Standard MBTiles File (v1.3)
    /// </summary>
    public partial class DisplayMbTilesFile : IDisposable
    {
        private bool _initialized;
        private VectorMbTilesAsyncLayer _mvtLayer;

        public DisplayMbTilesFile()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            _mvtLayer = new VectorMbTilesAsyncLayer(@".\Data\Mbtiles\maplibre.mbtiles");
            _mvtLayer.StyleJsonUri = @".\Data\Mbtiles\style.json";
       
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(_mvtLayer);

            Map.Overlays.Clear();
            layerOverlay.TileType = TileType.SingleTile;
            Map.Overlays.Add(layerOverlay);

            await _mvtLayer.OpenAsync();
            Map.CurrentExtent = _mvtLayer.GetBoundingBox();

            _initialized = true;
            await Map.RefreshAsync();
        }
    
        private async void SwitchStyleJson_OnCheckedChanged(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;
            var selectedType = ((RadioButton)sender).Content?.ToString();

            _mvtLayer.StyleJsonUri = selectedType == "With StyleJson" 
                ? @".\Data\Mbtiles\style.json" 
                : null;

            await _mvtLayer.CloseAsync();
            await _mvtLayer.OpenAsync();

            _ = Map.RefreshAsync();
        }

        private async void Projection_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            try
            {
                if (_mvtLayer == null) return;

                var radioButton = sender as RadioButton;
                if (radioButton?.Tag == null) return;

                var centerPoint = Map.CenterPoint;

                switch (radioButton.Tag.ToString())
                {
                    case "3857":
                        Map.MapUnit = GeographyUnit.Meter;
                        _mvtLayer.ProjectionConverter = null;
                        centerPoint = ProjectionConverter.Convert(4326, 3857, centerPoint);
                        break;

                    case "4326":
                        Map.MapUnit = GeographyUnit.DecimalDegree;
                        _mvtLayer.ProjectionConverter = new GdalProjectionConverter(3857, 4326);
                        centerPoint = ProjectionConverter.Convert(3857, 4326, centerPoint);
                        break;

                    default:
                        return;
                }

                await _mvtLayer.CloseAsync();
                await _mvtLayer.OpenAsync();

                Map.CenterPoint = centerPoint;
                await Map.RefreshAsync();
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        public void Dispose()
        {
            ThinkGeoDebugger.DisplayTileId = false;
            // Dispose of unmanaged resources.
            Map.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}