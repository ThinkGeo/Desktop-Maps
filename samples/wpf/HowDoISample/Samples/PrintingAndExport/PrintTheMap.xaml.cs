using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Compose a page of map, legend, scale bar, title and data grid on the map, then export it as a PDF.
    /// </summary>
    public partial class PrintTheMap
    {
        private const string Title = "Frisco Mosquito Report - 5/5/2020";

        private static readonly AreaStyle CityLimitsStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.Black, 2);
        private static readonly LineStyle MajorStreetsStyle = LineStyle.CreateSimpleLineStyle(GeoColors.DarkGray, 1, true);
        private static readonly AreaStyle ParksStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.LightGray, GeoColors.Transparent);
        private static readonly PointStyle TrapSiteStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Black, 10, GeoColors.White, 2);

        private bool _initialized;
        private bool _useStyleDocument;
        private PrinterInteractiveOverlay _printerOverlay;
        private MapPrinterLayoutAsyncLayer _mapPrinterLayer;
        private RectangleShape _cityExtent;

        public PrintTheMap()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            // The map shows a sheet of paper, and zooms in the steps a printed page is read at.
            Map.MapUnit = GeographyUnit.Meter;
            Map.ZoomScales = new PrinterZoomLevelSet(GeographyUnit.Meter, PrinterLayoutHelper.GetPointsPerGeographyUnit(GeographyUnit.Meter)).GetScales();
            Map.MinimumScale = Map.ZoomScales[Map.ZoomScales.Count - 1];

            var cityLimits = new ShapeFileFeatureSource(SampleShared.Shapefile("FriscoCityLimits.shp")) { ProjectionConverter = new ProjectionConverter(2276, 3857) };
            cityLimits.Open();
            _cityExtent = cityLimits.GetBoundingBox();
            cityLimits.Close();

            // The sheet is drawn on screen to show where the paper is; it is not printed.
            var pageLayer = new PagePrinterLayoutAsyncLayer(PrinterPageSize.AnsiA, PrinterOrientation.Portrait)
            {
                BackgroundMask = AreaStyle.CreateSimpleAreaStyle(GeoColors.White, GeoColors.Black)
            };
            _printerOverlay = new PrinterInteractiveOverlay { IsEditable = true };
            _printerOverlay.PrinterLayoutAsyncLayers.Add(pageLayer);
            var page = CreatePage(pageLayer, CreateMapLayers(), _cityExtent, Title);
            foreach (var layer in page.Layers)
            {
                _printerOverlay.PrinterLayoutAsyncLayers.Add(layer);
            }
            _mapPrinterLayer = page.Layers.OfType<MapPrinterLayoutAsyncLayer>().First();
            Map.InteractiveOverlays.Add(_printerOverlay);

            var pageExtent = pageLayer.GetPosition().GetBoundingBox();
            Map.CenterPoint = pageExtent.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, pageExtent, Map.MapWidth, Map.MapHeight);

            _ = Map.RefreshAsync();
        }

        private async void MapContent_Checked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            try
            {
                _useStyleDocument = (string)((RadioButton)sender).Tag == "style";
                _mapPrinterLayer.Layers.Clear();
                foreach (var layer in CreateMapLayers())
                {
                    _mapPrinterLayer.Layers.Add(layer);
                }
                _mapPrinterLayer.InvalidateInnerContentCache();
                await Map.RefreshAsync();
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }

        private async void ExportPdf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var document = new PrinterLayoutDocument();
                document.Pages.Add(PageOnScreen());
                await ExportAsync(document, "FriscoMosquitoReport.pdf");
            }
            catch (Exception ex)
            {
                Status.Text = ex.Message;
            }
        }

        private async Task ExportAsync(PrinterLayoutDocument document, string fileName)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "PDF Document|*.pdf",
                FileName = fileName,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (saveFileDialog.ShowDialog() != true) return;

            // Each PDF page takes the size of its page's sheet.
            Status.Text = $"Writing {document.Pages.Count} page(s)...";
            using (var stream = File.Create(saveFileDialog.FileName))
            {
                await document.ExportPdfAsync(stream);
            }
            Status.Text = "Saved " + saveFileDialog.FileName;

            Process.Start(new ProcessStartInfo(saveFileDialog.FileName) { UseShellExecute = true });
        }

        /// <summary>
        /// The page as it is on screen, with whatever was moved or resized there.
        /// </summary>
        private PrinterLayoutPage PageOnScreen()
        {
            var sheet = _printerOverlay.PrinterLayoutAsyncLayers.OfType<PagePrinterLayoutAsyncLayer>().First();
            var page = new PrinterLayoutPage(sheet);
            foreach (var layer in _printerOverlay.PrinterLayoutAsyncLayers.Where(layer => layer != sheet))
            {
                page.Layers.Add(layer);
            }
            return page;
        }

        /// <summary>
        /// The map's content: the Frisco shapefiles, styled in code or by the style document.
        /// </summary>
        private IEnumerable<LayerBase> CreateMapLayers()
        {
            // The shapefiles are in Texas North Central (2276); the page is in Spherical Mercator (3857).
            if (_useStyleDocument)
            {
                var sources = new GeoCollection<FeatureSource>();
                foreach (var name in new[] { "Parks", "Streets", "FriscoCityLimits", "Frisco_Mosquitos" })
                {
                    sources.Add(name, new ShapeFileFeatureSource(SampleShared.Shapefile(name + ".shp")) { ProjectionConverter = new ProjectionConverter(2276, 3857) });
                }
                return new LayerBase[] { new MapStyleAsyncLayer(new MapStyle((string)FindResource("StyleJson")), sources) };
            }

            var cityLimits = new ShapeFileFeatureLayer(SampleShared.Shapefile("FriscoCityLimits.shp"));
            cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = CityLimitsStyle;
            cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.DrawingLevel = DrawingLevel.LevelFour;

            var streets = new ShapeFileFeatureLayer(SampleShared.Shapefile("Streets.shp"));
            var majorStreets = new FilterStyle();
            majorStreets.Conditions.Add(new FilterCondition("SUBTYPE", "<= 6"));
            majorStreets.Styles.Add(MajorStreetsStyle);
            streets.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(majorStreets);

            var parks = new ShapeFileFeatureLayer(SampleShared.Shapefile("Parks.shp"));
            parks.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = ParksStyle;

            var trapSites = new ShapeFileFeatureLayer(SampleShared.Shapefile("Frisco_Mosquitos.shp"));
            var mayFifth = new FilterStyle();
            mayFifth.Conditions.Add(new FilterCondition("DateCollec", "20200505"));
            mayFifth.Styles.Add(TrapSiteStyle);
            mayFifth.Styles.Add(TextStyle.CreateSimpleTextStyle("TrapSite", "Verdana", 8, DrawingFontStyles.Bold, GeoColors.Black, GeoColors.White, 2, 0, 6));
            trapSites.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(mayFifth);
            trapSites.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.DrawingLevel = DrawingLevel.LevelFour;

            var layers = new[] { cityLimits, streets, parks, trapSites };
            foreach (var layer in layers)
            {
                layer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
                layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            }
            return layers;
        }

        /// <summary>
        /// One page: the title, the map of the extent, its legend and scale bar, and the table of trap sites.
        /// Positions are in inches from the centre of the sheet.
        /// </summary>
        private static PrinterLayoutPage CreatePage(PagePrinterLayoutAsyncLayer sheet, IEnumerable<LayerBase> mapLayers, RectangleShape mapExtent, string title)
        {
            var center = sheet.GetPosition().GetCenterPoint();
            var page = new PrinterLayoutPage(sheet);

            var titleLabel = new LabelPrinterLayoutAsyncLayer(title, new GeoFont("Verdana", 8), GeoBrushes.Black)
            {
                PrinterWrapMode = PrinterWrapMode.AutoSizeText
            };
            titleLabel.SetPosition(7.5, .5, center.X, center.Y + 4.75, PrintingUnit.Inch);
            page.Layers.Add(titleLabel);

            var mapPrinterLayer = new MapPrinterLayoutAsyncLayer(mapLayers, mapExtent, GeographyUnit.Meter)
            {
                DrawingMode = MapPrinterDrawingMode.Vector
            };
            mapPrinterLayer.SetPosition(7.5, 5, center.X, center.Y + 1.75, PrintingUnit.Inch);
            page.Layers.Add(mapPrinterLayer);

            var legend = CreateLegend();
            legend.SetPosition(1.40, 1.05, center.X + 2.95, center.Y - 0.12, PrintingUnit.Inch);
            page.Layers.Add(legend);

            var scaleBar = new ScaleBarPrinterLayoutAsyncLayer(mapPrinterLayer)
            {
                UnitFamily = UnitSystem.Imperial,
                HasMask = true,
                DynamicBoundingBox = true,
                Projection = new Projection(3857)
            };
            scaleBar.SetPosition(2.15, 0.45, center.X - 2.35, center.Y - 0.45, PrintingUnit.Inch);
            page.Layers.Add(scaleBar);

            var dataGrid = CreateTrapSiteTable();
            dataGrid.SetPosition(7.5, 4, center.X, center.Y - 3, PrintingUnit.Inch);
            page.Layers.Add(dataGrid);

            return page;
        }

        private static LegendPrinterLayoutAsyncLayer CreateLegend()
        {
            var legendAdornment = new LegendAdornmentLayer
            {
                Title = new LegendItem
                {
                    Width = 98,
                    Height = 16,
                    LeftPadding = 4,
                    RightPadding = 4,
                    TopPadding = 2,
                    BottomPadding = 2,
                    TextLeftPadding = 0,
                    TextTopPadding = 0,
                    TextStyle = new TextStyle("Map Legend", new GeoFont("Verdana", 8, DrawingFontStyles.Bold), GeoBrushes.Black)
                }
            };

            legendAdornment.LegendItems.Add(CreateLegendItem("City Limits", CityLimitsStyle));
            legendAdornment.LegendItems.Add(CreateLegendItem("Major Streets", MajorStreetsStyle));
            legendAdornment.LegendItems.Add(CreateLegendItem("Parks", ParksStyle));
            legendAdornment.LegendItems.Add(CreateLegendItem("Mosquito Site", TrapSiteStyle));

            return new LegendPrinterLayoutAsyncLayer(legendAdornment);
        }

        private static LegendItem CreateLegendItem(string text, ThinkGeo.Core.Style imageStyle)
        {
            return new LegendItem
            {
                Width = 98,
                Height = 13,
                LeftPadding = 2,
                RightPadding = 2,
                TopPadding = 1,
                BottomPadding = 1,
                ImageWidth = 10,
                ImageHeight = 10,
                ImageLeftPadding = 2,
                ImageRightPadding = 3,
                ImageTopPadding = 1,
                ImageBottomPadding = 1,
                TextLeftPadding = 3,
                TextRightPadding = 1,
                TextTopPadding = 1,
                TextBottomPadding = 1,
                ImageStyle = imageStyle,
                TextStyle = new TextStyle(text, new GeoFont("Verdana", 8), GeoBrushes.Black)
            };
        }

        /// <summary>
        /// The trap sites sampled on 5/5/2020, one row each.
        /// </summary>
        private static DataGridPrinterLayoutAsyncLayer CreateTrapSiteTable()
        {
            var table = new DataTable();
            table.Columns.Add("TRAP SITE");
            table.Columns.Add("ADDRESS");
            table.Columns.Add("MALES");
            table.Columns.Add("FEMALES");
            table.Columns.Add("WEST NILE DETECTED");

            var mosquitoFeatureSource = new ShapeFileFeatureSource(SampleShared.Shapefile("Frisco_Mosquitos.shp"));
            mosquitoFeatureSource.Open();
            var trapSites = mosquitoFeatureSource.GetFeaturesByColumnValue("DateCollec", "20200505", ReturningColumnsType.AllColumns);
            mosquitoFeatureSource.Close();

            foreach (var trapSite in trapSites)
            {
                table.Rows.Add(trapSite.ColumnValues["TrapSite"], trapSite.ColumnValues["Address"], trapSite.ColumnValues["Male"], trapSite.ColumnValues["Female"], trapSite.ColumnValues["WestNileMo"]);
            }

            return new DataGridPrinterLayoutAsyncLayer(table, new GeoFont("Verdana", 8), new GeoFont("Verdana", 8, DrawingFontStyles.Bold));
        }
    }
}
