using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to display a WMTS Layer on the map
    /// </summary>
    public partial class WMTS : IDisposable
    {
        private bool _initialized;

        public WMTS()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add the WMTS layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ThinkGeoDebugger.DisplayTileId = true;
                // It is important to set the map unit first to either feet, meters or decimal degrees.
                MapView.MapUnit = GeographyUnit.Meter;
                var layerOverlay = new WmtsOverlay(new Uri("https://wmts.geo.admin.ch/1.0.0"));
                layerOverlay.ActiveLayerName = "ch.swisstopo.pixelkarte-farbe-pk25.noscale";
                layerOverlay.ActiveStyleName = "default";
                layerOverlay.OutputFormat = "image/png";
                layerOverlay.TileMatrixSetName = "21781_26";
                MapView.Overlays.Add(layerOverlay);

                await layerOverlay.OpenAsync();

                // Apply the wmts matrices to the MapView. 
                var scales = layerOverlay.TileMatrixSet.GetScales();
                MapView.ZoomScales = new Collection<double>(scales.ToList());
                
                var layerOverlayBBox = layerOverlay.GetBoundingBox();
                MapView.CenterPoint = layerOverlayBBox.GetCenterPoint();
                MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit,layerOverlayBBox, MapView.MapWidth, MapView.MapHeight);
                
                _initialized = true;
                await MapView.RefreshAsync();
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
            MapView.Dispose();
            GC.SuppressFinalize(this);
        }

        private void DisplayTileIdCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) 
                return;

            if (!(sender is CheckBox checkBox))
                return;

            if (!checkBox.IsChecked.HasValue)
                return;

            if (ThinkGeoDebugger.DisplayTileId != checkBox.IsChecked.Value)
            {
                ThinkGeoDebugger.DisplayTileId = checkBox.IsChecked.Value;
                _ = MapView.RefreshAsync();
            }
        }
    }
}