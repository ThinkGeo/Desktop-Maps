using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace ProjectionSupportForWmts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WmtsLayer wmtsLayer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            wmtsLayer = new WmtsLayer();
            wmtsLayer.WmtsSeverEncodingType = ThinkGeo.MapSuite.Layers.WmtsSeverEncodingType.Kvp;
            wmtsLayer.ServerUris.Add(new Uri("https://basemap.nationalmap.gov/arcgis/rest/services/USGSImageryOnly/MapServer/WMTS"));
            wmtsLayer.ActiveLayerName = "USGSImageryOnly";
            wmtsLayer.ActiveStyleName = "default";
            wmtsLayer.TileMatrixSetName = "GoogleMapsCompatible";
            wmtsLayer.OutputFormat = "image/png";
            wmtsLayer.Projection = new Proj4Projection(3857, 4326);
            wmtsLayer.ProjectedTileCache = new FileBitmapTileCache("WmtsProjectedTileCache", "USGSImageryOnly-4326");
            wmtsLayer.TileCache = new FileBitmapTileCache("WmtsTileCache", "USGSImageryOnly-3857");
            wmtsLayer.Open();

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(wmtsLayer);

            mapView.Overlays.Add(layerOverlay);
            mapView.CurrentExtent = new RectangleShape(-88.1825201012961, 31.1103487417779, -87.9133550622336, 30.9177446768365);
        }

        private void projectionsCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (wmtsLayer != null)
            {
                var srid = int.Parse(((ComboBoxItem)((ComboBox)e.Source).SelectedItem).Content.ToString());
                switch (srid)
                {
                    case 4326:
                        mapView.MapUnit = GeographyUnit.DecimalDegree;
                        mapView.CurrentExtent = new RectangleShape(-88.1825201012961, 31.1103487417779, -87.9133550622336, 30.9177446768365);
                        break;
                    case 102003:
                        mapView.MapUnit = GeographyUnit.Meter;
                        mapView.CurrentExtent = new RectangleShape(751845.883866561, -685904.954985099, 760423.862159907, -700300.974970186);
                        break;
                    case 26916:
                        mapView.MapUnit = GeographyUnit.Meter;
                        mapView.CurrentExtent = new RectangleShape(396167.595904146, 3438553.46153289, 403766.768311506, 3424708.06524118);
                        break;
                }

                if (wmtsLayer.ProjectedTileCache != null)
                    wmtsLayer.ProjectedTileCache.CacheId = $"{wmtsLayer.ActiveLayerName.Replace(" ", "")}-{srid}";

                wmtsLayer.Projection.Close();
                wmtsLayer.Projection = new Proj4Projection(3857, srid);
                wmtsLayer.Projection.Open();

                mapView.Refresh();
            }
        }

        private void projectionCkb_Checked(object sender, RoutedEventArgs e)
        {
            if (projectionsCmb != null)
                projectionsCmb.IsEnabled = true;

            if (wmtsLayer != null)
            {
                var srid = int.Parse(((ComboBoxItem)projectionsCmb.SelectedItem).Content.ToString());
                wmtsLayer.Projection = new Proj4Projection(3857, srid);
                wmtsLayer.Projection.Open();

                switch (srid)
                {
                    case 4326:
                        mapView.MapUnit = GeographyUnit.DecimalDegree;
                        mapView.CurrentExtent = new RectangleShape(-88.1825201012961, 31.1103487417779, -87.9133550622336, 30.9177446768365);
                        break;
                    case 102003:
                        mapView.MapUnit = GeographyUnit.Meter;
                        mapView.CurrentExtent = new RectangleShape(751845.883866561, -685904.954985099, 760423.862159907, -700300.974970186);
                        break;
                    case 26916:
                        mapView.MapUnit = GeographyUnit.Meter;
                        mapView.CurrentExtent = new RectangleShape(396167.595904146, 3438553.46153289, 403766.768311506, 3424708.06524118);
                        break;
                }

                if (wmtsLayer.ProjectedTileCache != null)
                    wmtsLayer.ProjectedTileCache.CacheId = $"{wmtsLayer.ActiveLayerName}-{srid}";

                mapView.Refresh();
            }
        }

        private void projectionCkb_Unchecked(object sender, RoutedEventArgs e)
        {
            if (projectionsCmb != null)
                projectionsCmb.IsEnabled = false;

            if (wmtsLayer != null)
            {
                wmtsLayer.Projection.Close();
                wmtsLayer.Projection = null;

                if (wmtsLayer.ProjectedTileCache != null)
                    wmtsLayer.ProjectedTileCache.CacheId = $"{wmtsLayer.ActiveLayerName}-3857";

                mapView.MapUnit = GeographyUnit.Meter;
                mapView.CurrentExtent = new RectangleShape(-9805806.7384, 3642647.7077, -9797096.4156, 3626504.5761);

                mapView.Refresh();
            }
        }
    }
}
