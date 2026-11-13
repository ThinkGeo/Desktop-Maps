using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Show a vector file - Shapefile, GeoJSON, KML, GPX, TAB, TinyGeo, CAD or GeoPDF - or features added in code.
    /// </summary>
    public partial class VectorFileFormats : IDisposable
    {
        private bool _initialized;
        private int _request;
        private LayerOverlay _dataOverlay;

        public VectorFileFormats()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));
            _dataOverlay = new LayerOverlay();
            Map.Overlays.Add(_dataOverlay);
            await ShowChosenAsync();
        }

        private async void Format_Checked(object sender, RoutedEventArgs e)
        {
            if (_initialized) await ShowChosenAsync();
        }

        private async Task ShowChosenAsync()
        {
            var request = ++_request;
            _dataOverlay.Layers.Clear();
            Status.Text = "Opening...";

            try
            {
                var layer = CreateChosenLayer();
                // Some formats parse the whole file when they open, so it runs off the UI thread.
                var extent = await Task.Run(() =>
                {
                    layer.Open();
                    return layer.GetBoundingBox();
                });
                if (request != _request) return;

                _dataOverlay.Layers.Add(layer);
                extent.ScaleUp(20);
                Map.CurrentExtent = extent;
                Status.Text = layer.GetType().Name;
            }
            catch (Exception exception)
            {
                if (request != _request) return;
                Status.Text = "Unavailable: " + exception.Message;
            }

            _ = Map.RefreshAsync();
        }

        private FeatureLayer CreateChosenLayer()
        {
            if (GeoJson.IsChecked == true) return CreateGeoJsonLayer();
            if (Kml.IsChecked == true) return CreateKmlLayer();
            if (Gpx.IsChecked == true) return CreateGpxLayer();
            if (Tab.IsChecked == true) return CreateTabLayer();
            if (TinyGeo.IsChecked == true) return CreateTinyGeoLayer();
            if (CadEmbedded.IsChecked == true) return CreateCadLayer(CadStylingType.EmbeddedStyling);
            if (CadStandard.IsChecked == true) return CreateCadLayer(CadStylingType.StandardStyling);
            if (GeoPdf.IsChecked == true) return CreateGeoPdfLayer();
            if (InMemory.IsChecked == true) return CreateInMemoryLayer();
            return CreateShapefileLayer();
        }

        private static FeatureLayer CreateShapefileLayer()
        {
            var parksLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Parks.shp"));
            parksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            var dashedPen = new GeoPen(GeoColors.Green, 5);
            dashedPen.DashPattern.Add(1);
            dashedPen.DashPattern.Add(1);
            parksLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(dashedPen, new GeoSolidBrush(GeoColors.Transparent));
            parksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return parksLayer;
        }

        private static FeatureLayer CreateGeoJsonLayer()
        {
            var districtsLayer = new InMemoryFeatureLayer
            {
                FeatureSource = new GeoJsonFeatureSource(Path.Combine(AppContext.BaseDirectory, "Data", "GeoJson", "pittsburghpacity-designated-historic-districts.geojson"))
                {
                    ProjectionConverter = new ProjectionConverter(4326, 3857)
                }
            };
            districtsLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(200, GeoColors.LightOrange), GeoColors.Red);
            districtsLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyle.CreateSimpleTextStyle("historic_name", "Noto Sans", 12, DrawingFontStyles.Bold, GeoColors.Black, GeoColors.White, 2);
            districtsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return districtsLayer;
        }

        private static FeatureLayer CreateKmlLayer()
        {
            var kmlLayer = new KmlGdalFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Kml", "Frisco.kml"));
            kmlLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            kmlLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimplePointStyle(PointSymbolType.Diamond, GeoColors.Black, 10);
            kmlLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Red, 4, true);
            kmlLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Blue);
            kmlLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return kmlLayer;
        }

        private static FeatureLayer CreateGpxLayer()
        {
            var trailsLayer = new GpxFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Gpx", "Hike_Bike.gpx"));
            trailsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            trailsLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(GeoPens.Black);
            trailsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return trailsLayer;
        }

        private static FeatureLayer CreateTabLayer()
        {
            var cityBoundaryLayer = new TabFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Tab", "City_ETJ.tab"))
            {
                // Draw with the styles below instead of the styles stored in the file.
                StylingType = TabStylingType.StandardStyling
            };
            cityBoundaryLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            cityBoundaryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(100, GeoColors.Green), GeoColors.Green);
            cityBoundaryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return cityBoundaryLayer;
        }

        private static FeatureLayer CreateTinyGeoLayer()
        {
            var zoningLayer = new TinyGeoFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "TinyGeo", "Zoning.tgeo"));
            zoningLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            zoningLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(GeoPens.Black, new GeoSolidBrush(new GeoColor(75, GeoColors.MediumPurple)));
            zoningLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return zoningLayer;
        }

        private static FeatureLayer CreateCadLayer(CadStylingType stylingType)
        {
            // The drawing is in a Lambert Conformal Conic projection (ESRI 103376).
            var cadLayer = new CadFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "CAD", "Zipcodes.DWG"))
            {
                StylingType = stylingType
            };
            cadLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(103376, 3857);
            cadLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColors.Blue, 2));
            cadLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return cadLayer;
        }

        private static FeatureLayer CreateGeoPdfLayer()
        {
            var geoPdfLayer = new GeoPdfGdalFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "GeoPdf", "bangalore.pdf"));
            geoPdfLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 3857);
            geoPdfLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 5, GeoBrushes.LightGray, GeoPens.Gray);
            geoPdfLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColor.FromArgb(75, 132, 112, 255), 2, true);
            geoPdfLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return geoPdfLayer;
        }

        private static FeatureLayer CreateInMemoryLayer()
        {
            var trapsShapefile = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Frisco_Mosquitos.shp"));
            trapsShapefile.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            trapsShapefile.Open();
            var features = trapsShapefile.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);
            trapsShapefile.Close();

            var trapsLayer = new InMemoryFeatureLayer();
            foreach (var feature in features)
            {
                trapsLayer.InternalFeatures.Add(feature);
            }

            trapsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 12, GeoBrushes.Red, GeoPens.White);
            trapsLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = new TextStyle("Trap: [TrapID]", new GeoFont("Arial", 14), GeoBrushes.Black)
            {
                Mask = new AreaStyle(GeoPens.Black, GeoBrushes.White),
                MaskMargin = new DrawingMargin(2, 2, 2, 2),
                YOffsetInPixel = -10
            };
            trapsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return trapsLayer;
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
