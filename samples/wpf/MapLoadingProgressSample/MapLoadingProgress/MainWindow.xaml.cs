using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace MapLoadingProgress
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void map_Loaded(object sender, RoutedEventArgs e)
        {
            map.MapUnit = GeographyUnit.Meter;
            map.CurrentExtent = new RectangleShape(2651245, 682873, 2771098, 597111);

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.DrawTilesProgressChanged += DrawTilesProgressChanged;
            CadFeatureLayer cadLayer = new CadFeatureLayer(@"..\..\AppData\MapKitBaseWithHouses.dwg");
            cadLayer.StylingType = CadStylingType.EmbeddedStyling;
            layerOverlay.Layers.Add(cadLayer);
            map.Overlays.Add(layerOverlay);

            map.Refresh();
        }

        private void DrawTilesProgressChanged(object sender, DrawTilesProgressChangedTileOverlayEventArgs e)
        {
            var loadingPercentage = e.ProgressPercentage;
            if (loadingPercentage == 0)
                loadingProgressBar.Visibility = Visibility.Visible;
            else if (loadingPercentage == 100)
                loadingProgressBar.Visibility = Visibility.Hidden;
            loadingProgressTextBlock.Visibility = loadingProgressBar.Visibility;
            loadingProgressTextBlock.Text = $"{loadingPercentage.ToString()}%";
            loadingProgressBar.Value = loadingPercentage;
        }
    }
}
