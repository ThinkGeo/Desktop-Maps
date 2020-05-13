using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public class CustomTextStyle : TextStyle
    {
        public CustomTextStyle(string textColumnName, GeoFont textFont, GeoBrush textBrush)
            : base(textColumnName, textFont, textBrush)
        { }

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            int i = 0;
            foreach (var feature in features)
            {
                base.DrawText(feature, canvas, Font, TextBrush, HaloPen, DrawingLevel, XOffsetInPixel, YOffsetInPixel, Alignment, i++ % 6 * 60, labelsInThisLayer, labelsInAllLayers);
            }
        }
    }

    /// <summary>
    /// Interaction logic for RotatePointWithTextStyle.xaml
    /// </summary>
    public partial class RotatePointWithTextStyle : UserControl
    {
        public RotatePointWithTextStyle()
        {
            InitializeComponent();
        }

        private void mapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudRasterMapsMapType.Dark);
            mapView.Overlays.Add(thinkGeoCloudMapsOverlay);

            ShapeFileFeatureLayer shapeFileFeatureLayer = new ShapeFileFeatureLayer(SampleHelper.Get("FriscoSchools.shp"));
            // CustomColumnFetch event is raised every time the layer is fetching a column which doesn't exist. 
            // In this example as "textColumn" does not exist in the shape file, CustomColumnFetch event 
            // will be raised so give us a way to programmatically filling it in.  
            shapeFileFeatureLayer.FeatureSource.CustomColumnFetch += FeatureSource_CustomColumnFetch;

            // Use a font glyph to render a point. 
            // This font in the sample can be downloaded at https://thinkgeo.com/map-icons-webfont.
            GeoFont geoFont = new GeoFont(SampleHelper.Get("vectormap-icons.ttf"), 20);
            CustomTextStyle textStyle = new CustomTextStyle("textColumn", geoFont, GeoBrushes.Yellow);

            // "textColumn" doesn't exist in the shape file, this is why CustomColumnFetch is raised.
            shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textStyle);
            shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay layerOverlay = new LayerOverlay() { TileType = TileType.SingleTile };
            layerOverlay.Layers.Add(shapeFileFeatureLayer);
            mapView.Overlays.Add(layerOverlay);

            mapView.CurrentExtent = new RectangleShape(-10786168.329407, 3921665.511929, -10765014.955587, 3907241.059012);
        }

        private void FeatureSource_CustomColumnFetch(object sender, CustomColumnFetchEventArgs e)
        {
            // "\ue0aa" is the school zone glyph content in ThinkGeo Web Font. 
            // Visit https://thinkgeo.com/map-icons-webfont and you can find more glyph icons from ThinkGeo. 
            e.ColumnValue = "\ue0aa";
        }
    }
}
