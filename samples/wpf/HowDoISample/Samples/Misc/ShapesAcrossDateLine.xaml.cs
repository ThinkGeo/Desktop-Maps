using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.Misc
{
    /// <summary>
    /// This sample shows learn how to work with different shapes across the Date Line.
    /// </summary>
    public partial class ShapesAcrossDateLine : UserControl
    {
        public ShapesAcrossDateLine()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            var overlay = new ThinkGeoCloudRasterMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudRasterMapsMapType.Light_V1_X2);

            overlay.WrappingMode = WrappingMode.WrapDateline;
            mapView.OverlaysDrawing += MapView_OverlaysDrawing;
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.Overlays.Add(overlay);
            mapView.CurrentExtent = MaxExtents.ThinkGeoMaps;

            var zoomLevelSet = new SphericalMercatorZoomLevelSet();
            zoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(zoomLevelSet.ZoomLevel01.Scale * 8));
            zoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(zoomLevelSet.ZoomLevel01.Scale * 4));
            zoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(zoomLevelSet.ZoomLevel01.Scale * 2));
            zoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(zoomLevelSet.ZoomLevel01.Scale));
            zoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(zoomLevelSet.ZoomLevel02.Scale));
            zoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(zoomLevelSet.ZoomLevel03.Scale));
            zoomLevelSet.CustomZoomLevels.Add(new ZoomLevel(zoomLevelSet.ZoomLevel04.Scale));

            mapView.ZoomLevelSet = zoomLevelSet;

            var layer = new InMemoryFeatureLayer();
            var wkt9 = GetExtent(-0.6, -0.4, 0.9, 0.99);
            var wkt8 = GetExtent(-0.6, 0, 0.8, 0.89);
            var wkt7 = GetExtent(-0.3, 0.3, 0.7, 0.79);
            var wkt6 = GetExtent(-0.6, 1, 0.6, 0.69);
            var wkt5 = GetExtent(-0.6, 1.8, 0.5, 0.59);
            var wkt4 = GetExtent(-1.5, -0.5, 0.4, 0.49);
            var wkt3 = GetExtent(0.4, 0.8, 0.3, 0.39);
            var wkt2 = GetExtent(0.4, 1.8, 0.2, 0.29);
            var wkt1 = GetExtent(1, 1.8, 0.1, 0.19);
            var wkt0 = GetExtent(1.4, 1.8, 0.0, 0.09);

            layer.InternalFeatures.Add(new Feature(wkt9));
            layer.InternalFeatures.Add(new Feature(wkt8));
            layer.InternalFeatures.Add(new Feature(wkt7));
            layer.InternalFeatures.Add(new Feature(wkt6));
            layer.InternalFeatures.Add(new Feature(wkt5));
            layer.InternalFeatures.Add(new Feature(wkt4));
            layer.InternalFeatures.Add(new Feature(wkt3));
            layer.InternalFeatures.Add(new Feature(wkt2));
            layer.InternalFeatures.Add(new Feature(wkt1));
            layer.InternalFeatures.Add(new Feature(wkt0));

            layer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle =
                AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(100, 153, 76, 0), GeoColors.LightRed);
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            layerOverlay.WrappingMode = WrappingMode.WrapDateline;
            layerOverlay.WrappingExtent = MaxExtents.ThinkGeoMaps;

            layer.DrawingFeatures += Layer_DrawingFeatures;
            layerOverlay.Layers.Add(layer);
            mapView.Overlays.Add(layerOverlay);

            await mapView.RefreshAsync();
        }

        private RectangleShape GetExtent(double minX, double maxX, double minY, double maxY)
        {
            return new RectangleShape(minX * MaxExtents.ThinkGeoMaps.Width + MaxExtents.ThinkGeoMaps.MinX,
                maxY * MaxExtents.ThinkGeoMaps.Height + MaxExtents.ThinkGeoMaps.MinY,
                maxX * MaxExtents.ThinkGeoMaps.Width + MaxExtents.ThinkGeoMaps.MinX,
                minY * MaxExtents.ThinkGeoMaps.Height + MaxExtents.ThinkGeoMaps.MinY);
        }

        private void Layer_DrawingFeatures(object sender, DrawingFeaturesEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Layer_DrawingFeatures: " + e.FeaturesToDraw.Count);
        }

        private void MapView_OverlaysDrawing(object sender, OverlaysDrawingMapViewEventArgs e)
        {
            Debug.WriteLine(mapView.RotatedAngle);
            ((RotateTransform)NorthImage.RenderTransform).Angle = mapView.RotatedAngle;
        }

        private void NorthImage_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            mapView.RotatedAngle = 0;
            mapView.Refresh();
        }
    }
}