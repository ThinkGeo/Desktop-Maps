using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// This sample shows learn how to work with different shapes across the Date Line.
    /// </summary>
    public partial class WrapTheDateLine
    {
        public WrapTheDateLine()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            var overlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V1_X2,
                WrappingMode = WrappingMode.WrapDateline,
                WrappingExtent = MaxExtents.ThinkGeoMaps
            };

            MapView.MapUnit = GeographyUnit.Meter;
            MapView.Overlays.Add(overlay);
            MapView.CenterPoint = MaxExtents.ThinkGeoMaps.GetCenterPoint();
            var MapScale = MapUtil.GetScale(MapView.MapUnit, MaxExtents.ThinkGeoMaps, MapView.MapWidth, MapView.MapHeight);
            MapView.CurrentScale = MapScale * 1.5; // Multiply the current scale by 1.5 to zoom out 50%.

            MapView.ZoomLevelSet = new SphericalMercatorZoomLevelSet();
            MapView.ZoomLevelSet.CustomZoomLevels.Add(MapView.ZoomLevelSet.ZoomLevel01);
            MapView.MapTools.PanZoomBar.IsEnabled = false;

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

            var layerOverlay = new LayerOverlay
            {
                TileType = TileType.SingleTile,
                WrappingMode = WrappingMode.WrapDateline,
                WrappingExtent = MaxExtents.ThinkGeoMaps
            };

            layerOverlay.Layers.Add(layer);
            MapView.Overlays.Add(layerOverlay);

            _ = MapView.RefreshAsync();
        }

        private static Feature GetExtent(double minX, double maxX, double minY, double maxY, string content)
        {
            var shape = new RectangleShape(minX * MaxExtents.ThinkGeoMaps.Width + MaxExtents.ThinkGeoMaps.MinX,
                maxY * MaxExtents.ThinkGeoMaps.Height + MaxExtents.ThinkGeoMaps.MinY,
                maxX * MaxExtents.ThinkGeoMaps.Width + MaxExtents.ThinkGeoMaps.MinX,
                minY * MaxExtents.ThinkGeoMaps.Height + MaxExtents.ThinkGeoMaps.MinY);

            var feature = new Feature(shape)
            {
                ColumnValues =
                {
                    ["Text"] = content
                }
            };
            return feature;
        }
    }
}