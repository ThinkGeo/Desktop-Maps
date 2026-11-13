using System;
using System.Collections.ObjectModel;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Wrap the map and its shapes across the date line, so a shape wider than the world or past its edge repeats in the next world.
    /// </summary>
    public partial class WrapTheDateLine : IDisposable
    {
        private bool _initialized;

        public WrapTheDateLine()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V2_X2,
                WrappingMode = WrappingMode.WrapDateline,
                WrappingExtent = MaxExtents.ThinkGeoMaps
            });

            Map.CurrentScale = 300000000;
            Map.ZoomScales = new Collection<double> { Map.CurrentScale };
            Map.MapTools.PanZoomBar.IsEnabled = false;

            // Each band runs from minX to maxX in world widths: 0 is the left edge of the world, 1 the right.
            var layer = new InMemoryFeatureLayer();
            layer.InternalFeatures.Add(GetBand(-0.6, -0.4, 0.9, 0.99, "-0.6 ~ -0.4"));
            layer.InternalFeatures.Add(GetBand(-0.6, 0, 0.8, 0.89, "-0.6 ~ 0"));
            layer.InternalFeatures.Add(GetBand(-0.3, 0.3, 0.7, 0.79, "-0.3 ~ 0.3"));
            layer.InternalFeatures.Add(GetBand(-0.6, 1, 0.6, 0.69, "-0.6 ~ 1"));
            layer.InternalFeatures.Add(GetBand(-0.6, 1.8, 0.5, 0.59, "-0.6 ~ 1.8"));
            layer.InternalFeatures.Add(GetBand(-1.5, -0.5, 0.4, 0.49, "-1.5 ~ -0.5"));
            layer.InternalFeatures.Add(GetBand(0.4, 0.8, 0.3, 0.39, "0.4 ~ 0.8"));
            layer.InternalFeatures.Add(GetBand(0.4, 1.8, 0.2, 0.29, "0.4 ~ 1.8"));
            layer.InternalFeatures.Add(GetBand(1, 1.8, 0.1, 0.19, "1 ~ 1.8"));
            layer.InternalFeatures.Add(GetBand(1.4, 1.8, 0.0, 0.09, "1.4 ~ 1.8"));
            layer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(100, GeoColors.Brown), GeoColors.LightRed);
            layer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = new TextStyle("Text", new GeoFont("Noto Sans", 16), GeoBrushes.Yellow);
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay
            {
                TileType = TileType.SingleTile,
                WrappingMode = WrappingMode.WrapDateline,
                WrappingExtent = MaxExtents.ThinkGeoMaps
            };
            layerOverlay.Layers.Add(layer);
            Map.Overlays.Add(layerOverlay);

            _ = Map.RefreshAsync();
        }

        private static Feature GetBand(double minX, double maxX, double minY, double maxY, string text)
        {
            var world = MaxExtents.ThinkGeoMaps;
            var band = new RectangleShape(minX * world.Width + world.MinX, maxY * world.Height + world.MinY,
                maxX * world.Width + world.MinX, minY * world.Height + world.MinY);

            var feature = new Feature(band);
            feature.ColumnValues["Text"] = text;
            return feature;
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
