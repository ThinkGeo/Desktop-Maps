using System;
using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace WmtsLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            map.MapUnit = GeographyUnit.Meter;
            map.CurrentExtent = new RectangleShape(-13900994, 21923801, 16781363, -31303);

            ThinkGeo.MapSuite.Layers.WmtsLayer wmtsLayer = new ThinkGeo.MapSuite.Layers.WmtsLayer();
            wmtsLayer.DrawingExceptionMode = DrawingExceptionMode.DrawException;
            wmtsLayer.WmtsSeverEncodingType = ThinkGeo.MapSuite.Layers.WmtsSeverEncodingType.Kvp;
            wmtsLayer.ServerUris.Add(new Uri("http://opencache.statkart.no/gatekeeper/gk/gk.open_wmts/"));
            wmtsLayer.Open();
            wmtsLayer.ActiveLayerName = "matrikkel_bakgrunn";
            wmtsLayer.ActiveStyleName = "default";
            wmtsLayer.TileMatrixSetName = "EPSG:3857";
            wmtsLayer.OutputFormat = "image/png";

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(wmtsLayer);
            map.Overlays.Add(layerOverlay);
        }
    }
}
