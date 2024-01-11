using System.Windows;
using System.Windows.Controls;
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
            overlay.WrappingExtent = MaxExtents.ThinkGeoMaps;
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.Overlays.Add(overlay);
            mapView.CurrentExtent = MaxExtents.ThinkGeoMaps;

            mapView.ZoomLevelSet = new SphericalMercatorZoomLevelSet();
            mapView.ZoomLevelSet.CustomZoomLevels.Add(mapView.ZoomLevelSet.ZoomLevel01);
            mapView.MapTools.PanZoomBar.IsEnabled = false;

            var layer = new InMemoryFeatureLayer();
            layer.InternalFeatures.Add(GetExtent(-0.6, -0.4, 0.9, 0.99, "-0.6 ~ -0.4"));
            layer.InternalFeatures.Add(GetExtent(-0.6, 0, 0.8, 0.89, "-0.6 ~ 0"));
            layer.InternalFeatures.Add(GetExtent(-0.3, 0.3, 0.7, 0.79, "-0.3 ~ 0.3"));
            layer.InternalFeatures.Add(GetExtent(-0.6, 1, 0.6, 0.69, "-0.6 ~ 1"));
            layer.InternalFeatures.Add(GetExtent(-0.6, 1.8, 0.5, 0.59, "0.6 ~ 1.8,"));
            layer.InternalFeatures.Add(GetExtent(-1.5, -0.5, 0.4, 0.49, "-1.5 ~ -0.5"));
            layer.InternalFeatures.Add(GetExtent(0.4, 0.8, 0.3, 0.39, "0.4 ~ 0.8"));
            layer.InternalFeatures.Add(GetExtent(0.4, 1.8, 0.2, 0.29, "0.4 ~ 1.8"));
            layer.InternalFeatures.Add(GetExtent(1, 1.8, 0.1, 0.19, "1 ~ 1.8"));
            layer.InternalFeatures.Add(GetExtent(1.4, 1.8, 0.0, 0.09, "1.4 ~ 1.8"));

            layer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(100, GeoColors.Brown), GeoColors.LightRed);
            layer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = new TextStyle("Text", new GeoFont("Arial", 16), GeoBrushes.Yellow);
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            layerOverlay.WrappingMode = WrappingMode.WrapDateline;
            layerOverlay.WrappingExtent = MaxExtents.ThinkGeoMaps;

            layerOverlay.Layers.Add(layer);
            mapView.Overlays.Add(layerOverlay);

            await mapView.RefreshAsync();
        }

        private Feature GetExtent(double minX, double maxX, double minY, double maxY, string content)
        {
            var shape = new RectangleShape(minX * MaxExtents.ThinkGeoMaps.Width + MaxExtents.ThinkGeoMaps.MinX,
                maxY * MaxExtents.ThinkGeoMaps.Height + MaxExtents.ThinkGeoMaps.MinY,
                maxX * MaxExtents.ThinkGeoMaps.Width + MaxExtents.ThinkGeoMaps.MinX,
                minY * MaxExtents.ThinkGeoMaps.Height + MaxExtents.ThinkGeoMaps.MinY);
            
            var feature = new Feature(shape);
            feature.ColumnValues["Text"] = content;
            return feature;
        }
    }
}