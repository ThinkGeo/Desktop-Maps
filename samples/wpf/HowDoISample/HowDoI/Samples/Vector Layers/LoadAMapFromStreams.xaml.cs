using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class LoadAMapFromStreams : UserControl
    {
        public LoadAMapFromStreams()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-14833496, 20037508, 14126965, -20037508);

            ThinkGeoCloudRasterMapsOverlay ThinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(ThinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer shapeFileLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"));
            ((ShapeFileFeatureSource)shapeFileLayer.FeatureSource).StreamLoading += new EventHandler<StreamLoadingEventArgs>(LoadAMapFromStreams_StreamLoading);
            shapeFileLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            shapeFileLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", shapeFileLayer);
            mapView.Overlays.Add(staticOverlay);

            mapView.Refresh();
        }

        private void LoadAMapFromStreams_StreamLoading(object sender, StreamLoadingEventArgs e)
        {
            string fileName = System.IO.Path.GetFileName(e.AlternateStreamName);
            string ext = Path.GetExtension(fileName);
            fileName = SampleHelper.Get(fileName, DataFormat.Shapefile);
            fileName = Path.ChangeExtension(fileName, ext);

            e.AlternateStream = new FileStream(fileName, (FileMode)e.FileMode, (FileAccess)e.ReadWriteMode);
        }
    }
}