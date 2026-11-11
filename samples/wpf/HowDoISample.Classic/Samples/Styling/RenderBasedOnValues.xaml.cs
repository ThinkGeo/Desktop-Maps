using System;
using System.IO;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Color crime points by their offense group with a ValueStyle, and list the colors in a legend.
    /// </summary>
    public partial class RenderBasedOnValues : IDisposable
    {
        private bool _initialized;

        public RenderBasedOnValues()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            var friscoCrime = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Frisco_Crime.shp"));
            // The shapefile is in Texas North Central feet (EPSG:2276); the map is in EPSG:3857.
            friscoCrime.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            var legend = new LegendAdornmentLayer
            {
                Title = new LegendItem
                {
                    TextStyle = new TextStyle("Crime Categories", new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black)
                },
                Location = AdornmentLocation.LowerRight
            };
            Map.AdornmentOverlay.Layers.Add(legend);

            friscoCrime.Open();
            var offenseGroups = friscoCrime.FeatureSource.GetDistinctColumnValues("OffenseGro");
            friscoCrime.Close();

            var colors = GeoColor.GetColorsInQualityFamily(GeoColors.Red, offenseGroups.Count);
            var valueStyle = new ValueStyle { ColumnName = "OffenseGro" };
            for (var i = 0; i < offenseGroups.Count; i++)
            {
                var pointStyle = PointStyle.CreateSimpleCircleStyle(colors[i], 10, GeoColors.Black, 2);
                valueStyle.ValueItems.Add(new ValueItem(offenseGroups[i].ColumnValue, pointStyle));

                legend.LegendItems.Add(new LegendItem
                {
                    ImageStyle = pointStyle,
                    TextStyle = new TextStyle(offenseGroups[i].ColumnValue, new GeoFont("Verdana", 10), GeoBrushes.Black)
                });
            }

            friscoCrime.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(valueStyle);
            friscoCrime.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(friscoCrime);
            Map.Overlays.Add(layerOverlay);

            Map.CenterPoint = new PointShape(-10778210, 3914410);
            Map.CurrentScale = 18260;

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
