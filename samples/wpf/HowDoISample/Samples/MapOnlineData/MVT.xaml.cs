using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for MVT.xaml
    /// </summary>
    public partial class MVT
    {
        public MVT()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MapView.MapUnit = GeographyUnit.Meter;
                LayerOverlay _layerOverlay = new LayerOverlay();
                MapView.Overlays.Add(_layerOverlay);

                var openstackMbtiles = new MvtTilesAsyncLayer("https://demotiles.maplibre.org/style.json");
                //var openstackMbtiles = new MvtTilesAsyncLayer("https://demotiles.maplibre.org/styles/osm-bright-gl-style/style.json");
                //var openstackMbtiles = new MvtTilesAsyncLayer("https://tiles.preludemaps.com/styles/TG_Savannah_Light/style.json");

                _layerOverlay.Layers.Add(openstackMbtiles);
                await openstackMbtiles.OpenAsync();
                MapView.CurrentExtent = openstackMbtiles.GetBoundingBox();
                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }
    }
}
