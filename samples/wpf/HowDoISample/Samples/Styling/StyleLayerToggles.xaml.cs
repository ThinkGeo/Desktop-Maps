using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Every layer of the style, listed with a checkbox: SetLayerVisibility toggles
    /// any of them at runtime, with no style rebuild.
    /// </summary>
    public partial class StyleLayerToggles
    {
        private MapStyle _style;
        private bool _initialized;

        public StyleLayerToggles()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            _style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });
            Map.Basemap = new GpuBasemap(_style);
            Map.CurrentExtent = new RectangleShape(-8_240_000, 4_975_000, -8_228_000, 4_965_000); // Manhattan

            await Map.RefreshAsync();

            // The style lists its layers in style order once the map has opened it.
            var layers = _style.StyleLayers.ToList();
            LayerCount.Text = FormattableString.Invariant($"{layers.Count} layers");
            LayerList.ItemsSource = layers;
        }

        private void Layer_Changed(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox { DataContext: StyleLayer layer } box)
            {
                _style.SetLayerVisibility(layer.Id, box.IsChecked == true);
            }
        }
    }
}
