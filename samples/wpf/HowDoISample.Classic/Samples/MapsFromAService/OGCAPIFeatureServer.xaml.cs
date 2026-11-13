using System;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Show the features of an OGC API - Features server: named places from the Spanish national geographic institute (IGN).
    /// </summary>
    public partial class OGCAPIFeatureServer : IDisposable
    {
        private bool _initialized;

        public OGCAPIFeatureServer()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            var namedPlacesLayer = new OgcApiAsyncLayer("https://api-features.ign.es", "namedplace")
            {
                FeatureSource =
                {
                    ProjectionConverter = new ProjectionConverter(4326, 3857)
                }
            };
            namedPlacesLayer.ZoomLevelSet.ZoomLevel13.DefaultPointStyle = PointStyle.CreateSimplePointStyle(PointSymbolType.Circle, GeoColors.DarkRed, 10);
            namedPlacesLayer.ZoomLevelSet.ZoomLevel13.DefaultTextStyle = new TextStyle("etiqueta", new GeoFont("Noto Sans", 14), GeoBrushes.DarkRed)
            {
                MaskType = MaskType.RoundedCorners,
                OverlappingRule = LabelOverlappingRule.NoOverlapping,
                Mask = new AreaStyle(GeoBrushes.WhiteSmoke),
                SuppressPartialLabels = true,
                YOffsetInPixel = -12
            };
            namedPlacesLayer.ZoomLevelSet.ZoomLevel13.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            Map.Overlays.Add(new OgcApiFeaturesOverlay
            {
                AsyncLayer = namedPlacesLayer,
                DrawingBulkCount = 100
            });

            Map.CenterPoint = new PointShape(235690, 5057360);
            Map.CurrentScale = 36200;

            _ = Map.RefreshAsync();
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
