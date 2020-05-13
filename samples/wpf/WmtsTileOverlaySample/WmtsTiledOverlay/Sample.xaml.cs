using System;
using System.Windows;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;
using System.IO;

namespace WmtsTiledOverlaySample
{
    public partial class Sample : Window
    {
        public Sample()
        {
            InitializeComponent();
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            btnVisitKVPServer_Click(null, null);
        }


        private void btnVisitKVPServer_Click(object sender, RoutedEventArgs e)
        {
            Map1.MapUnit = GeographyUnit.Meter;
            Map1.CurrentExtent = new RectangleShape(33707536, 18538567, 52962281, 7013115);
            WmtsTiledOverlay wmtsTiledOverlay = new WmtsTiledOverlay(new Collection<Uri> { new Uri("http://opencache.statkart.no/gatekeeper/gk/gk.open_wmts/") });
            wmtsTiledOverlay.WmtsServerEncodingType = WmtsSeverEncodingType.Kvp;
            wmtsTiledOverlay.Parameters.Add("LAYER", "matrikkel_bakgrunn");
            wmtsTiledOverlay.Parameters.Add("STYLE", "default");
            wmtsTiledOverlay.Parameters.Add("FORMAT", "image/png");
            wmtsTiledOverlay.Parameters.Add("TileMatrixSet", "EPSG:3857");
            wmtsTiledOverlay.InitializeConnection();

            Map1.Overlays.Clear();
            Map1.Overlays.Add(wmtsTiledOverlay);
            Map1.Refresh();
        }

        private void btnVisitRESTfulServer_Click(object sender, RoutedEventArgs e)
        {
            Map1.CurrentExtent = new RectangleShape(4950653.58289573, 23129180.9399217, 81969633.6420015, -22972627.2458641);
            WmtsTiledOverlay wmtsTiledOverlay = new WmtsTiledOverlay(new Collection<Uri> { new Uri("http://server.caris.com/spatialfusionserver/services/ows/wmts/NaturalEarth/") });
            wmtsTiledOverlay.WmtsServerEncodingType = WmtsSeverEncodingType.Restful;
            wmtsTiledOverlay.Parameters.Add("LAYER", "NaturalEarth.newworld");
            wmtsTiledOverlay.Parameters.Add("STYLE", "default");
            wmtsTiledOverlay.Parameters.Add("FORMAT", "image/png");
            wmtsTiledOverlay.Parameters.Add("TileMatrixSet", "GoogleMapsCompatible");
            wmtsTiledOverlay.InitializeConnection();

            Map1.Overlays.Clear();
            Map1.Overlays.Add(wmtsTiledOverlay);
            Map1.Refresh();
        }
    }
}

