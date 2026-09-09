using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
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

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.ZoomStep = 0.15;

            _style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey));
            Map.Basemap = new GpuBasemap(_style);
            Map.CurrentExtent = new RectangleShape(-8_240_000, 4_975_000, -8_228_000, 4_965_000); // Manhattan

            _ = BuildTogglesAsync();
        }

        private async Task BuildTogglesAsync()
        {
            await Map.RefreshAsync();

            // The style lists its layers in style order once the map has opened it.
            var layers = _style.StyleLayers.ToList();
            LayerPanel.Children.Add(new TextBlock
            {
                Text = FormattableString.Invariant($"{layers.Count} layers"),
                FontWeight = FontWeights.SemiBold,
                Margin = new Thickness(2, 0, 0, 6),
            });

            foreach (var layer in layers)
            {
                var box = new CheckBox
                {
                    IsChecked = true,
                    Margin = new Thickness(2, 1, 0, 1),
                    Content = new TextBlock
                    {
                        Text = FormattableString.Invariant($"{layer.Id}  ({layer.Type})"),
                        TextTrimming = TextTrimming.CharacterEllipsis,
                    },
                };
                var layerId = layer.Id;
                box.Checked += (_, _) => _style.SetLayerVisibility(layerId, true);
                box.Unchecked += (_, _) => _style.SetLayerVisibility(layerId, false);
                LayerPanel.Children.Add(box);
            }
        }
    }
}
