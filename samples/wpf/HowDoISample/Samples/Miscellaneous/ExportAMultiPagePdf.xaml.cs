using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to export a map book: one PDF with a page per sheet of a grid.
    /// </summary>
    public partial class ExportAMultiPagePdf : IDisposable
    {
        private bool _initialized;

        public ExportAMultiPagePdf()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map to preview the first sheet of the map book
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;
            Map.ZoomScales = new PrinterZoomLevelSet(GeographyUnit.Meter,
                PrinterLayoutHelper.GetPointsPerGeographyUnit(GeographyUnit.Meter)).GetScales();
            Map.MinimumScale = Map.ZoomScales[Map.ZoomScales.Count - 1];

            var printerOverlay = new PrinterInteractiveOverlay { IsEditable = false };
            Map.InteractiveOverlays.Add("printerOverlay", printerOverlay);

            ShowFirstSheet();
        }

        /// <summary>
        /// Writes every sheet of the grid into a single PDF and opens it
        /// </summary>
        private async void ExportPdf_OnClick(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "PDF Document|*.pdf",
                FileName = "FriscoMapBook.pdf"
            };
            if (saveFileDialog.ShowDialog() != true) return;

            var document = BuildMapBook(GetGridSize());
            var firstSheet = document.Pages[0].Page;

            using (var stream = File.Create(saveFileDialog.FileName))
            {
                var pdfGeoCanvas = new PdfGeoCanvas();
                pdfGeoCanvas.SetPageSize(firstSheet);

                // DrawAsync walks the pages, calling AddPage on the canvas between them.
                pdfGeoCanvas.BeginDrawing(stream, firstSheet.GetPosition(), Map.MapUnit);
                await document.DrawAsync(pdfGeoCanvas);
                pdfGeoCanvas.EndDrawing();
            }

            Process.Start(new ProcessStartInfo(saveFileDialog.FileName) { UseShellExecute = true });
        }

        private void GridSize_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!_initialized) return;

            ShowFirstSheet();
        }

        /// <summary>
        /// Previews the first sheet of the map book on screen
        /// </summary>
        private void ShowFirstSheet()
        {
            var printerOverlay = (PrinterInteractiveOverlay)Map.InteractiveOverlays["printerOverlay"];
            printerOverlay.PrinterLayoutAsyncLayers.Clear();

            var firstPage = BuildMapBook(GetGridSize()).Pages[0];
            printerOverlay.PrinterLayoutAsyncLayers.Add("pageLayer", firstPage.Page);
            foreach (var layer in firstPage.Layers)
            {
                printerOverlay.PrinterLayoutAsyncLayers.Add(layer);
            }

            var pageBoundingBox = firstPage.Page.GetPosition().GetBoundingBox();
            Map.CenterPoint = pageBoundingBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, pageBoundingBox, Map.MapWidth, Map.MapHeight);

            _ = Map.RefreshAsync();
        }

        /// <summary>
        /// Splits the city into a grid and turns each cell into one page of a document
        /// </summary>
        private static PrinterLayoutDocument BuildMapBook(int gridSize)
        {
            // Take the area to cover from the data rather than hard-coding it.
            var cityLimitsLayer = CreateCityLimitsLayer();
            cityLimitsLayer.Open();
            RectangleShape cityLimits = cityLimitsLayer.GetBoundingBox();
            cityLimitsLayer.Close();

            double cellWidth = cityLimits.Width / gridSize;
            double cellHeight = cityLimits.Height / gridSize;

            var document = new PrinterLayoutDocument();
            for (int row = 0; row < gridSize; row++)
            {
                for (int column = 0; column < gridSize; column++)
                {
                    var cell = new RectangleShape(
                        cityLimits.UpperLeftPoint.X + column * cellWidth,
                        cityLimits.UpperLeftPoint.Y - row * cellHeight,
                        cityLimits.UpperLeftPoint.X + (column + 1) * cellWidth,
                        cityLimits.UpperLeftPoint.Y - (row + 1) * cellHeight);

                    int sheetNumber = row * gridSize + column + 1;
                    document.Pages.Add(BuildSheet(cell, sheetNumber, gridSize * gridSize));
                }
            }

            return document;
        }

        /// <summary>
        /// One sheet: a title, the map at this cell's extent, and a scale bar
        /// </summary>
        private static PrinterLayoutPage BuildSheet(RectangleShape cellExtent, int sheetNumber, int sheetCount)
        {
            var page = new PrinterLayoutPage(
                new PagePrinterLayoutAsyncLayer(PrinterPageSize.AnsiA, PrinterOrientation.Landscape)
                {
                    BackgroundMask = AreaStyle.CreateSimpleAreaStyle(GeoColors.White, GeoColors.Black)
                });

            var title = new LabelPrinterLayoutAsyncLayer(
                $"Frisco Map Book - Sheet {sheetNumber} of {sheetCount}",
                new GeoFont("Verdana", 10),
                GeoBrushes.Black);
            title.SetPosition(10, 0.4, 0, 3.5, PrintingUnit.Inch);
            page.Layers.Add(title);

            var mapLayer = new MapPrinterLayoutAsyncLayer(new[] { CreateCityLimitsLayer(), CreateStreetsLayer() }, cellExtent, GeographyUnit.Meter)
            {
                BackgroundMask = AreaStyle.CreateSimpleAreaStyle(GeoColors.White, GeoColors.Black)
            };
            mapLayer.SetPosition(10, 6.5, 0, -0.2, PrintingUnit.Inch);
            page.Layers.Add(mapLayer);

            var scaleBar = new ScaleBarPrinterLayoutAsyncLayer(mapLayer) { MapUnit = GeographyUnit.Meter };
            scaleBar.SetPosition(2.5, 0.4, -3.5, -3.5, PrintingUnit.Inch);
            page.Layers.Add(scaleBar);

            return page;
        }

        private static ShapeFileFeatureLayer CreateCityLimitsLayer()
        {
            var cityLimits = new ShapeFileFeatureLayer(@"./Data/Shapefile/FriscoCityLimits.shp");
            cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.Black, 2);
            cityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            cityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            return cityLimits;
        }

        private static ShapeFileFeatureLayer CreateStreetsLayer()
        {
            var streets = new ShapeFileFeatureLayer(@"./Data/Shapefile/Streets.shp");
            streets.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.DarkGray, 1, true);
            streets.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            streets.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
            return streets;
        }

        private int GetGridSize()
        {
            return GridSize.SelectedIndex + 1;
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
