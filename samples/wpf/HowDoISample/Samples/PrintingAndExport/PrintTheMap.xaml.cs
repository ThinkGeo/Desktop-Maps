using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
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

        private readonly DispatcherTimer _applyTimer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(400) };
        private bool _initialized;
        private PrinterInteractiveOverlay _printerOverlay;
        private MapPrinterLayoutAsyncLayer _mapPrinterLayer;
        private LegendPrinterLayoutAsyncLayer _legendLayer;
        private RectangleShape _cityExtent;

        public PrintTheMap()
        {
            InitializeComponent();
            // Edits land once typing pauses, not on every keystroke.
            _applyTimer.Tick += async (_, _) => { _applyTimer.Stop(); await ApplyStyleAsync(); };
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
            var page = CreatePage(pageLayer, MapLayers(), LegendItems(), _cityExtent, Title);
            foreach (var layer in page.Layers)
            {
                _printerOverlay.PrinterLayoutAsyncLayers.Add(layer);
            }
            _mapPrinterLayer = page.Layers.OfType<MapPrinterLayoutAsyncLayer>().First();
            _legendLayer = page.Layers.OfType<LegendPrinterLayoutAsyncLayer>().First();
            // Click the map frame, then Ctrl+drag pans the map inside it and Ctrl+wheel zooms
            // it; without Ctrl the page itself pans and zooms.
            _mapPrinterLayer.EnableInnerNavigation = true;
            Map.InteractiveOverlays.Add(_printerOverlay);

            var pageExtent = pageLayer.GetPosition().GetBoundingBox();
            Map.CenterPoint = pageExtent.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, pageExtent, Map.MapWidth, Map.MapHeight);

            _ = Map.RefreshAsync();
        }

        private void StyleEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!_initialized) return;

            _applyTimer.Stop();
            _applyTimer.Start();
        }

        // The map frame draws from a cached picture; a new style means a new picture.
        private async Task ApplyStyleAsync()
        {
            try
            {
                // Both read the document first: one that does not parse leaves the page as it is.
                var layers = MapLayers().ToList();
                var items = LegendItems().ToList();
                _mapPrinterLayer.Layers.Clear();
                foreach (var layer in layers)
                {
                    _mapPrinterLayer.Layers.Add(layer);
                }
                _legendLayer.LegendItems.Clear();
                foreach (var item in items)
                {
                    _legendLayer.LegendItems.Add(item);
                }
                _mapPrinterLayer.InvalidateInnerContentCache();
                Status.Text = string.Empty;
                await Map.RefreshAsync();
            }
            catch (Exception ex)
            {
                Status.Foreground = Brushes.Salmon;
                Status.Text = ex.Message.Split('\n')[0].TrimEnd('\r');
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
        /// The map's content: the Frisco shapefiles, drawn by the style document in the editor.
        /// </summary>
        private IEnumerable<LayerBase> MapLayers()
        {
            // The shapefiles are in Texas North Central (2276); the page is in Spherical Mercator (3857).
            var sources = new GeoCollection<FeatureSource>();
            foreach (var name in new[] { "Parks", "Streets", "FriscoCityLimits", "Frisco_Mosquitos" })
            {
                sources.Add(name, new ShapeFileFeatureSource(SampleShared.Shapefile(name + ".shp")) { ProjectionConverter = new ProjectionConverter(2276, 3857) });
            }
            return new LayerBase[] { new MapStyleAsyncLayer(new MapStyle(StyleEditor.Text), sources) };
        }

        /// <summary>
        /// One legend item per fill, line and circle layer of the style document, in its colors. Labels have no swatch.
        /// </summary>
        private IEnumerable<LegendItem> LegendItems()
        {
            using (var document = JsonDocument.Parse(StyleEditor.Text))
            {
                foreach (var layer in document.RootElement.GetProperty("layers").EnumerateArray())
                {
                    var paint = layer.TryGetProperty("paint", out var p) ? p : default;
                    ThinkGeo.Core.Style swatch = layer.GetProperty("type").GetString() switch
                    {
                        "fill" => AreaStyle.CreateSimpleAreaStyle(PaintColor(paint, "fill-color"), PaintColor(paint, "fill-outline-color")),
                        "line" => LineStyle.CreateSimpleLineStyle(PaintColor(paint, "line-color"), PaintNumber(paint, "line-width", 1), true),
                        "circle" => PointStyle.CreateSimpleCircleStyle(PaintColor(paint, "circle-color"), PaintNumber(paint, "circle-radius", 5) * 2, PaintColor(paint, "circle-stroke-color"), PaintNumber(paint, "circle-stroke-width", 0)),
                        _ => null
                    };
                    if (swatch == null) continue;

                    var text = layer.TryGetProperty("id", out var id) ? id.GetString() : layer.GetProperty("source-layer").GetString();
                    yield return CreateLegendItem(text, swatch);
                }
            }
        }

        private static GeoColor PaintColor(JsonElement paint, string name)
        {
            return paint.ValueKind == JsonValueKind.Object && paint.TryGetProperty(name, out var value) && value.ValueKind == JsonValueKind.String
                ? GeoColor.FromHtml(value.GetString())
                : GeoColors.Transparent;
        }

        private static float PaintNumber(JsonElement paint, string name, float fallback)
        {
            return paint.ValueKind == JsonValueKind.Object && paint.TryGetProperty(name, out var value) && value.ValueKind == JsonValueKind.Number
                ? value.GetSingle()
                : fallback;
        }

        /// <summary>
        /// One page: the title, the map of the extent, its legend and scale bar, and the table of trap sites.
        /// Positions are in inches from the centre of the sheet.
        /// </summary>
        private static PrinterLayoutPage CreatePage(PagePrinterLayoutAsyncLayer sheet, IEnumerable<LayerBase> mapLayers, IEnumerable<LegendItem> legendItems, RectangleShape mapExtent, string title)
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

            var legend = CreateLegend(legendItems);
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

        private static LegendPrinterLayoutAsyncLayer CreateLegend(IEnumerable<LegendItem> items)
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

            foreach (var item in items)
            {
                legendAdornment.LegendItems.Add(item);
            }

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
