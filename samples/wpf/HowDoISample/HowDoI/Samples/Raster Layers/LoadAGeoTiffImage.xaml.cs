using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for LoadAGeoTiff.xaml
    /// </summary>
    public partial class LoadAGeoTiffImage : UserControl
    {
        public LoadAGeoTiffImage()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.DecimalDegree;
            LayerOverlay staticOverlay = new LayerOverlay();
            //GeoTiffRasterLayer layer = new GeoTiffRasterLayer(SampleHelper.Get("World.tif"));

            UnmanagedGeoTiffRasterLayer layer = new UnmanagedGeoTiffRasterLayer(SampleHelper.Get("World.tif"));

            staticOverlay.Layers.Add(layer);
            mapView.Overlays.Add(staticOverlay);

            layer.Open();
            RectangleShape extent = layer.GetBoundingBox();
            mapView.CurrentExtent = extent;
            mapView.Refresh();
        }
    }
}
