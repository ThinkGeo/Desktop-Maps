/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace DisplayASimpleMap
{
    public partial class Sample : Window
    {
        public Sample()
        {
            InitializeComponent();
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            Map1.MapUnit = GeographyUnit.Meter;
            Map1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            Map1.MapTools.Logo.IsEnabled = true;
            Map1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay baseOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            Map1.Overlays.Add(baseOverlay);

            BuildLegend();

            Map1.CurrentExtent = new RectangleShape(-13914936.3491592, 5160979.44404978, -12245143.9872601, 4163881.14406429);
            Map1.Refresh();
        }

        private void BuildLegend()
        {
            //****************************************
            // Note:  Hard-coded styles were used in the LegendItems below to simplify the sample.  
            //        Typically, a LegendItem's ImageStyle property will be set to a style that is already 
            //        set on a layer in the map.
            // Example:
            //        legendItem.ImageStyle = Map1.FindFeatureLayer("myLayerName").ZoomLevelSet.ZoomLevel01.DefaultAreaStyle;
            //
            //****************************************

            // Create a legend item for the title.
            LegendItem title = new LegendItem();
            title.TextStyle = new TextStyle("Map Legend", new GeoFont("Arial", 10, DrawingFontStyles.Bold), new GeoSolidBrush(GeoColor.SimpleColors.Black));

            // Create a legend item for the state borders.  This example uses a modified LineStyle.
            LegendItem legendItem1 = new LegendItem();
            LineStyle stateBorderStyle = new LineStyle();
            stateBorderStyle.OuterPen.DashStyle = LineDashStyle.Dash;
            stateBorderStyle.OuterPen.Width = 2;
            stateBorderStyle.OuterPen.Color = GeoColor.FromArgb(255, 156, 155, 154);
            legendItem1.ImageStyle = stateBorderStyle;
            legendItem1.TextStyle = new TextStyle("State Borders", new GeoFont("Arial", 8), new GeoSolidBrush(GeoColor.SimpleColors.Black));

            // Create a legend item for the state borders.  This example uses a simple AreaStyle.
            LegendItem legendItem2 = new LegendItem();
            legendItem2.ImageStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 167, 204, 149));
            legendItem2.TextStyle = new TextStyle("Forests", new GeoFont("Arial", 8), new GeoSolidBrush(GeoColor.SimpleColors.Black));

            // Create A legend item for the airports.  This example uses a .png file.
            LegendItem legendItem3 = new LegendItem();
            legendItem3.ImageStyle = new PointStyle(new GeoImage(@"..\..\Resources\airport_small_size3.png"));
            legendItem3.TextStyle = new TextStyle("Airports", new GeoFont("Arial", 8), new GeoSolidBrush(GeoColor.SimpleColors.Black));

            // Create the LegendAdornmentLayer and add the LegendItems.
            LegendAdornmentLayer legendLayer = new LegendAdornmentLayer();
            legendLayer.BackgroundMask = AreaStyles.CreateLinearGradientStyle(new GeoColor(255, 255, 255, 255), new GeoColor(255, 230, 230, 230), 90, GeoColor.SimpleColors.Black);
            legendLayer.LegendItems.Add(legendItem1);
            legendLayer.LegendItems.Add(legendItem2);
            legendLayer.LegendItems.Add(legendItem3);
            legendLayer.Height = 125;
            legendLayer.Title = title;
            legendLayer.Location = AdornmentLocation.LowerLeft;

            AdornmentOverlay adornmentOverlay = new AdornmentOverlay();
            //adornmentOverlay.IsBase = true;

            adornmentOverlay.Layers.Add("LengendLayer", legendLayer);
            Map1.Overlays.Add(adornmentOverlay);
        }
    }
}

