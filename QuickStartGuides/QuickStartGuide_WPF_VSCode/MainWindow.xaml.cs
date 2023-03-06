using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ThinkGeo.Core;

namespace MapDemo
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
    
    private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the Map's Unit to Meter.
            mapView.MapUnit = GeographyUnit.Meter;
            // Set the Current Extent to the Max Extent of ThinkGeo Map.
            mapView.CurrentExtent = MaxExtents.ThinkGeoMaps;
                            
            // Add a base map overlay.
            var baseOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", 
                "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the base overlay, passing in the location and an ID to distinguish the cache.     
            baseOverlay.TileCache = new FileRasterTileCache(@".\cache", "basemap");

            // Add the newly created overlay to mapView.
            mapView.Overlays.Add(baseOverlay);
                            
            // Refresh the Map
            mapView.Refresh();
        }
    }
}
