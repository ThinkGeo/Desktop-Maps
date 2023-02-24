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

        // private void Map_Loaded(object sender, RoutedEventArgs e)
        // {
        //     // Set the Map's Unit to Meter.
        //     map.MapUnit = GeographyUnit.Meter;
        //     // Set the Current Extent to the Max Extent of ThinkGeo Map.
        //     map.CurrentExtent = MaxExtents.ThinkGeoMaps;
        
        //     // Create a new ThinkGeo Map Overlay. Here I'm using a demo Key, every registered user gets an eval key which is good for 30 days
        //     var backgroundOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
        
        //     // Add the newly created overlay to mapView.
        //     map.Overlays.Add(backgroundOverlay);
        
        //     // Refresh the Map
        //     map.Refresh();
        // }
    }
}
