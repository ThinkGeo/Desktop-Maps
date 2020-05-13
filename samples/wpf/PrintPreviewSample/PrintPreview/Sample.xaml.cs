using PdfSharp;
using PdfSharp.Pdf;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace PrintPreview
{
    public partial class Sample : Window
    {
        public Sample() { InitializeComponent(); }

        #region Add PrinterLayer Elements

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            SetupMapWithBlankPage();

            AddCenteredTitleLabel();
            AddMapLayer();
            AddDataGridLayer();
            AddLogoImage();
            AddScaleLineLayer();
            AddLegendLayer();
            SetupComboBoxes();
            SetupContextMenu();

            Map1.Refresh();
        }

        private void SetupMapWithBlankPage()
        {
            // Setup the map unit, you want to always use feet or meters for printer layout maps
            Map1.MapUnit = GeographyUnit.Meter;

            // Here we create the default zoom levels for the sample.  We have pre-created a PrinterZoomLevelSet
            // That pre-defines commonly used zoom levels based on percentages of zoom
            Map1.ZoomLevelSet = new PrinterZoomLevelSet(Map1.MapUnit, PrinterHelper.GetPointsPerGeographyUnit(Map1.MapUnit));

            // Here we set the background color to gray so there is contrast with the while page
            Map1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.StandardColors.LightGray);

            // Create the PrinterInteractiveOverlay to contain all of the PrinterLayers.
            // The interactive overlay allows the layers to be interacted with
            PrinterInteractiveOverlay printerOverlay = new PrinterInteractiveOverlay();

            Map1.InteractiveOverlays.Add("PrintPreviewOverlay", printerOverlay);
            Map1.InteractiveOverlays.MoveToBottom("PrintPreviewOverlay");

            // Create the PagePrinterLayer which shows the page boundary and is the area the user will
            // arrange all of the other layer on top of.
            PagePrinterLayer pagePrinterLayer = new PagePrinterLayer(PrinterPageSize.AnsiA, PrinterOrientation.Portrait);
            pagePrinterLayer.Open();
            printerOverlay.PrinterLayers.Add("PageLayer", pagePrinterLayer);

            // Get the pages extent, slightly scale it up, and then set that as the default current extent
            Map1.CurrentExtent = RectangleShape.ScaleUp(pagePrinterLayer.GetPosition(), 10).GetBoundingBox();

            // Set the minimum sscale of the map to the last zoom level
            Map1.MinimumScale = Map1.ZoomLevelSet.ZoomLevel20.Scale;
        }

        private void AddCenteredTitleLabel()
        {
            LabelPrinterLayer labelPrinterLayer = new LabelPrinterLayer();

            //Setup the text and the font..
            labelPrinterLayer.Text = "Population > 70 Million";
            labelPrinterLayer.Font = new GeoFont("Arial", 10, DrawingFontStyles.Bold);
            labelPrinterLayer.TextBrush = new GeoSolidBrush(GeoColor.StandardColors.Black);

            // Set the labels position so that is it centered on the page one inch from the top                        
            RectangleShape pageBoundingbox = GetPageBoundingBox(PrintingUnit.Inch);
            PointShape labelCenter = new PointShape();
            labelCenter.X = pageBoundingbox.UpperRightPoint.X - pageBoundingbox.Width / 2;
            labelCenter.Y = pageBoundingbox.UpperLeftPoint.Y - .5;

            labelPrinterLayer.PrinterWrapMode = PrinterWrapMode.AutoSizeText;
            labelPrinterLayer.SetPosition(5, 1, labelCenter, PrintingUnit.Inch);

            // Find the PrinterInteractiveOverlay so we can add the new LabelPrinterLayer
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];
            printerInteractiveOverlay.PrinterLayers.Add("LabelLayer", labelPrinterLayer);
        }

        private void AddMapLayer()
        {
            // Create the MapPrinterLayer and set the position
            MapPrinterLayer mapPrinterLayer = new MapPrinterLayer();
            mapPrinterLayer.MapUnit = GeographyUnit.DecimalDegree;
            mapPrinterLayer.MapExtent = new RectangleShape(-180, 90, 180, -90);
            mapPrinterLayer.BackgroundMask = new AreaStyle(new GeoPen(GeoColor.StandardColors.Black, 1));
            mapPrinterLayer.Open();

            // Set the maps position slightly below the pages center and 8 inches wide and 7 inches tall
            RectangleShape pageBoundingbox = GetPageBoundingBox(PrintingUnit.Inch);
            mapPrinterLayer.SetPosition(8, 7, pageBoundingbox.GetCenterPoint().X, pageBoundingbox.GetCenterPoint().Y + 1, PrintingUnit.Inch);

            // Setup the intial extent and ensure they snap to the default ZoomLevel
            ZoomLevelSet zoomLevelSet = new ZoomLevelSet();
            mapPrinterLayer.MapExtent = ExtentHelper.ZoomToScale(zoomLevelSet.ZoomLevel03.Scale, new RectangleShape(-180, 90, 180, -90), mapPrinterLayer.MapUnit, (float)mapPrinterLayer.GetBoundingBox().Width, (float)mapPrinterLayer.GetBoundingBox().Height);

            // Add the World Map Kit layer as the background
            WorldStreetsAndImageryRasterLayer worldMapKitLayer = new WorldStreetsAndImageryRasterLayer();
            worldMapKitLayer.Projection = ThinkGeo.MapSuite.Layers.WorldStreetsAndImageryProjection.DecimalDegrees;
            mapPrinterLayer.Layers.Add(worldMapKitLayer);

            // Setup the Countries mapping layer                
            ShapeFileFeatureLayer shapefileFeatureLayer = new ShapeFileFeatureLayer(@"data/Countries02.shp", GeoFileReadWriteMode.Read);
            shapefileFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            //Create the class break style for countries less than 70 million
            ClassBreakStyle classBreakStyle = new ClassBreakStyle("POP_CNTRY");

            // Create the style and class break for countries with greater and 70 million            
            AreaStyle hatchAreaStyle = new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, GeoColor.StandardColors.Green)));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(70000000, hatchAreaStyle));

            // Add the class break style to the layer
            shapefileFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(classBreakStyle);

            // Add the new country layer to the MapPrinterLayer
            mapPrinterLayer.Layers.Add(shapefileFeatureLayer);

            // Add the MapPrinterLayer to the PrinterInteractiveOverlay
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];
            printerInteractiveOverlay.PrinterLayers.Add("MapLayer", mapPrinterLayer);
        }

        private void AddLegendLayer()
        {
            LegendItem title = new LegendItem();
            title.TextStyle = new TextStyle("Map Legend", new GeoFont("Arial", 10, DrawingFontStyles.Bold), new GeoSolidBrush(GeoColor.SimpleColors.Black));

            LegendItem legendItem1 = new LegendItem();
            legendItem1.ImageStyle = new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(170, GeoColor.StandardColors.Green)));
            legendItem1.TextStyle = new TextStyle("Population > 70 million", new GeoFont("Arial", 8), new GeoSolidBrush(GeoColor.SimpleColors.Black));

            LegendItem legendItem2 = new LegendItem();
            legendItem2.ImageStyle = AreaStyles.Country1;
            legendItem2.TextStyle = new TextStyle("Population < 70 million", new GeoFont("Arial", 8), new GeoSolidBrush(GeoColor.SimpleColors.Black));

            LegendAdornmentLayer legendAdornmentLayer = new LegendAdornmentLayer();
            legendAdornmentLayer.Height = 100;

            legendAdornmentLayer.Title = title;
            legendAdornmentLayer.LegendItems.Add(legendItem1);
            legendAdornmentLayer.LegendItems.Add(legendItem2);

            LegendPrinterLayer legendPrinterLayer = new LegendPrinterLayer(legendAdornmentLayer);
            legendPrinterLayer.SetPosition(2, 1, -2.9, 3.9, PrintingUnit.Inch);
            legendPrinterLayer.BackgroundMask = AreaStyles.CreateSimpleAreaStyle(new GeoColor(255, 230, 230, 230), GeoColor.SimpleColors.Black, 1);
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];
            printerInteractiveOverlay.PrinterLayers.Add("LegendPrinterLayer", legendPrinterLayer);
        }

        private void AddDataGridLayer()
        {
            // Create the DataGridPrinterLayer
            DataGridPrinterLayer dataGridPrinterLayer = new DataGridPrinterLayer();
            dataGridPrinterLayer.TextFont = new GeoFont("Arial", 8);
            //dataGridPrinterLayer.CellTextJustification = LabelTextJustification.Left;
            dataGridPrinterLayer.TextHorizontalAlignment = PrinterTextHorizontalAlignment.Left;

            // Set the data grid position 4 inches below the page center and 8 inches wide and 2.5 inches tall
            RectangleShape pageBoundingbox = GetPageBoundingBox(PrintingUnit.Inch);
            dataGridPrinterLayer.SetPosition(8, 2.5, pageBoundingbox.GetCenterPoint().X, pageBoundingbox.GetCenterPoint().Y - 4, PrintingUnit.Inch);

            //Create the data table and columns
            dataGridPrinterLayer.DataTable = new DataTable();
            dataGridPrinterLayer.DataTable.Columns.Add("Country");
            dataGridPrinterLayer.DataTable.Columns.Add("Population");
            dataGridPrinterLayer.DataTable.Columns.Add("CurrencyCode");
            dataGridPrinterLayer.DataTable.Columns.Add("Area");

            // Find all of the countries with a population greater than 70 million
            // and add those items to the data table
            ShapeFileFeatureLayer shapefileFeatureLayer = new ShapeFileFeatureLayer(@"data/Countries02.shp", GeoFileReadWriteMode.Read);
            shapefileFeatureLayer.Open();
            Collection<Feature> features = shapefileFeatureLayer.QueryTools.GetAllFeatures(ReturningColumnsType.AllColumns);
            shapefileFeatureLayer.Close();

            foreach (Feature feature in features)
            {
                double pop = Convert.ToDouble(feature.ColumnValues["Pop_cntry"].ToString());
                if (pop > 70000000)
                {
                    dataGridPrinterLayer.DataTable.Rows.Add(new object[4] { feature.ColumnValues["Cntry_Name"], feature.ColumnValues["Pop_cntry"], feature.ColumnValues["curr_code"], feature.ColumnValues["sqkm"] });
                }
            }

            // Add the DataGridPrinterLayer to the PrinterInteractiveOverlay
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];
            printerInteractiveOverlay.PrinterLayers.Add("DataGridLayer", dataGridPrinterLayer);
        }

        private void AddLogoImage()
        {
            //Load the image to a GeoImage and then create the ImagePrinterLayer using that image
            GeoImage compassGeoImage = new GeoImage(@"Images\Compass.png");
            ImagePrinterLayer imagePrinterLayer = new ImagePrinterLayer(compassGeoImage);

            // Set the imagePrinterLayer position offset from the page center and .75 inches wide and .75 inches tall
            RectangleShape pageBoundingbox = GetPageBoundingBox(PrintingUnit.Inch);
            imagePrinterLayer.SetPosition(.75, .75, pageBoundingbox.GetCenterPoint().X + 3.5, pageBoundingbox.GetCenterPoint().Y - 2.0, PrintingUnit.Inch);

            // Add the imagePrinterLayer to the PrinterInteractiveOverlay
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];
            printerInteractiveOverlay.PrinterLayers.Add("ImageLayer", imagePrinterLayer);
        }

        private void AddScaleLineLayer()
        {
            // Get the PrinterInteractiveOverlay
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];

            // Grab the MapPrinterLayer as we need to pass that into the scaleLinePrinterLayer so it
            // can be synced up to the map itself in real-time
            MapPrinterLayer mapPrinterLayer = (MapPrinterLayer)printerInteractiveOverlay.PrinterLayers["MapLayer"];

            //Create the scaleLinePrinterLayer and pass in the MapPrinterLayer
            ScaleLinePrinterLayer scaleLinePrinterLayer = new ScaleLinePrinterLayer(mapPrinterLayer);
            scaleLinePrinterLayer.MapUnit = GeographyUnit.DecimalDegree;

            // Set the scale line position offset from the page center and 1.25 inches wide and .25 inches tall
            RectangleShape pageBoundingbox = GetPageBoundingBox(PrintingUnit.Inch);
            scaleLinePrinterLayer.SetPosition(1.25, .25, pageBoundingbox.GetCenterPoint().X - 3.25, pageBoundingbox.GetCenterPoint().Y - 2.25, PrintingUnit.Inch);

            // Add the ScaleLinePrinterLayer to the PrinterInteractiveOverlay
            printerInteractiveOverlay.PrinterLayers.Add("ScaleLineLayer", scaleLinePrinterLayer);
        }

        #endregion

        #region Helper Methods

        private RectangleShape GetPageBoundingBox(PrintingUnit unit)
        {
            // This helper method gets the pages bounding box in the unit requested
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];
            PagePrinterLayer pagePrinterLayer = (PagePrinterLayer)printerInteractiveOverlay.PrinterLayers["PageLayer"];
            return pagePrinterLayer.GetPosition(unit); ;
        }

        private PageSize GetPdfPageSize(PrinterPageSize pageSize)
        {
            PageSize pdfPageSize = PageSize.Letter;
            switch (pageSize)
            {
                case PrinterPageSize.AnsiA:
                    pdfPageSize = PageSize.Letter;
                    break;
                case PrinterPageSize.AnsiB:
                    pdfPageSize = PageSize.Ledger;
                    break;
                case PrinterPageSize.AnsiC:
                    pdfPageSize = PageSize.A2;
                    break;
                case PrinterPageSize.AnsiD:
                    pdfPageSize = PageSize.A1;
                    break;
                case PrinterPageSize.AnsiE:
                    pdfPageSize = PageSize.A0;
                    break;
                case PrinterPageSize.Custom:
                    throw new NotSupportedException();
                default:
                    throw new NotSupportedException();
            }
            return pdfPageSize;
        }

        private PaperSize GetPrintPreviewPaperSize(PagePrinterLayer pagePrinterLayer)
        {
            PaperSize printPreviewPaperSize = new PaperSize("AnsiA", 850, 1100);
            switch (pagePrinterLayer.PageSize)
            {
                case PrinterPageSize.AnsiA:
                    printPreviewPaperSize = new PaperSize("AnsiA", 850, 1100);
                    break;
                case PrinterPageSize.AnsiB:
                    printPreviewPaperSize = new PaperSize("AnsiB", 1100, 1700);
                    break;
                case PrinterPageSize.AnsiC:
                    printPreviewPaperSize = new PaperSize("AnsiC", 1700, 2200);
                    break;
                case PrinterPageSize.AnsiD:
                    printPreviewPaperSize = new PaperSize("AnsiD", 2200, 3400);
                    break;
                case PrinterPageSize.AnsiE:
                    printPreviewPaperSize = new PaperSize("AnsiE", 3400, 4400);
                    break;
                case PrinterPageSize.Custom:
                    printPreviewPaperSize = new PaperSize("Custom Size", (int)pagePrinterLayer.CustomWidth, (int)pagePrinterLayer.CustomHeight);
                    break;
                default:
                    break;
            }

            return printPreviewPaperSize;
        }

        #endregion

        #region Export Buttons
        private void btnToPrintPreview_Click(object sender, RoutedEventArgs e)
        {
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];
            PagePrinterLayer pagePrinterLayer = printerInteractiveOverlay.PrinterLayers["PageLayer"] as PagePrinterLayer;

            PrintDialog printDialog = new PrintDialog();
            PrintDocument printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.Landscape = true;
            if (pagePrinterLayer.Orientation == PrinterOrientation.Portrait)
            {
                printDocument.DefaultPageSettings.Landscape = false;
            }

            printDocument.DefaultPageSettings.PaperSize = GetPrintPreviewPaperSize(pagePrinterLayer);

            PrinterGeoCanvas printerGeoCanvas = new PrinterGeoCanvas();
            printerGeoCanvas.DrawingArea = printDocument.DefaultPageSettings.Bounds;
            printerGeoCanvas.BeginDrawing(printDocument, pagePrinterLayer.GetBoundingBox(), Map1.MapUnit);

            // Loop through all of the PrintingLayer in the PrinterInteractiveOverlay and print all of the
            // except for the PagePrinterLayer                
            Collection<SimpleCandidate> labelsInAllLayers = new Collection<SimpleCandidate>();
            foreach (PrinterLayer printerLayer in printerInteractiveOverlay.PrinterLayers)
            {
                printerLayer.IsDrawing = true;
                if (!(printerLayer is PagePrinterLayer))
                {
                    printerLayer.Draw(printerGeoCanvas, labelsInAllLayers);
                }
                printerLayer.IsDrawing = false;
            }

            printerGeoCanvas.Flush();

            System.Windows.Forms.PrintPreviewDialog printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            printPreviewDialog.Document = printDocument;
            System.Windows.Forms.DialogResult dialogResult = printPreviewDialog.ShowDialog();
        }

        private void btnToBitmap_Click(object sender, RoutedEventArgs e)
        {
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];
            PagePrinterLayer pagePrinterLayer = printerInteractiveOverlay.PrinterLayers["PageLayer"] as PagePrinterLayer;

            // Create a bitmap that is the size of the pages bounding box.  The page by default is in points unit
            // system which is very similar to pixels so that ampping is nice.
            Bitmap bitmap = null;
            try
            {
                bitmap = new Bitmap((int)pagePrinterLayer.GetBoundingBox().Width, (int)pagePrinterLayer.GetBoundingBox().Height);

                // Create a GdiPlusGeoCanvas and start the drawing
                PlatformGeoCanvas gdiPlusGeoCanvas = new PlatformGeoCanvas();
                gdiPlusGeoCanvas.BeginDrawing(bitmap, pagePrinterLayer.GetBoundingBox(), Map1.MapUnit);

                // Loop through all of the PrintingLayer in the PrinterInteractiveOverlay and print all of the
                // except for the PagePrinterLayer                
                Collection<SimpleCandidate> labelsInAllLayers = new Collection<SimpleCandidate>();
                foreach (PrinterLayer printerLayer in printerInteractiveOverlay.PrinterLayers)
                {
                    printerLayer.IsDrawing = true;
                    if (!(printerLayer is PagePrinterLayer))
                    {
                        printerLayer.Draw(gdiPlusGeoCanvas, labelsInAllLayers);
                    }
                    printerLayer.IsDrawing = false;
                }

                // End the drawing
                gdiPlusGeoCanvas.EndDrawing();

                //Save the resulting bitmap to a file and open the file to show the user
                string filename = "PrintingResults.bmp";
                bitmap.Save(filename);
                Process.Start(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.StackTrace, "Exception");
            }
            finally
            {
                if (bitmap != null) { bitmap.Dispose(); }
            }
        }

        private void btnToPDF_Click(object sender, RoutedEventArgs e)
        {
            // Get the PrinterInteractiveOverlay and PagePrinterLayer as we are going to need them below
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];
            PagePrinterLayer pagePrinterLayer = printerInteractiveOverlay.PrinterLayers["PageLayer"] as PagePrinterLayer;

            PdfDocument pdfDocument = null;
            PdfPage pdfPage = null;

            try
            {
                // Create the PDF documents, pages, and copnfigure them
                pdfDocument = new PdfDocument();
                pdfPage = pdfDocument.AddPage();
                pdfPage.Orientation = pagePrinterLayer.Orientation == PrinterOrientation.Portrait ? PageOrientation.Portrait : PageOrientation.Landscape;
                pdfPage.Size = GetPdfPageSize(pagePrinterLayer.PageSize);

                // Create the PdfGeoCanvas and pass tha PdfPage into the BeginDrawing 
                PdfGeoCanvas pdfGeoCanvas = new PdfGeoCanvas();
                pdfGeoCanvas.BeginDrawing(pdfPage, pagePrinterLayer.GetBoundingBox(), Map1.MapUnit);

                // Loop through all of the PrinterLayers and draw them to the canvas except for the
                // PagePrinterLayer which is just used for the visual layout
                Collection<SimpleCandidate> labelsInAllLayers = new Collection<SimpleCandidate>();
                foreach (PrinterLayer printerLayer in printerInteractiveOverlay.PrinterLayers)
                {
                    printerLayer.IsDrawing = true;
                    if (!(printerLayer is PagePrinterLayer))
                    {
                        printerLayer.Draw(pdfGeoCanvas, labelsInAllLayers);
                    }
                    printerLayer.IsDrawing = false;
                }

                // Finish up the drawing
                pdfGeoCanvas.EndDrawing();

                // Dave the PDF document and launch it in the default viewer to show the user
                string filename = "PrintingResults.pdf";
                pdfDocument.Save(filename);
                Process.Start(filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.StackTrace, "Exception");
            }
            finally
            {
                if (pdfDocument != null) { pdfDocument.Dispose(); }
            }
        }

        private void btnToPrinter_Click(object sender, RoutedEventArgs e)
        {
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];
            PagePrinterLayer pagePrinterLayer = printerInteractiveOverlay.PrinterLayers["PageLayer"] as PagePrinterLayer;

            PrintDocument printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.Landscape = true;
            if (pagePrinterLayer.Orientation == PrinterOrientation.Portrait)
            {
                printDocument.DefaultPageSettings.Landscape = false;
            }

            printDocument.DefaultPageSettings.PaperSize = GetPrintPreviewPaperSize(pagePrinterLayer);

            PrinterGeoCanvas printerGeoCanvas = new PrinterGeoCanvas();
            printerGeoCanvas.DrawingArea = new Rectangle(0, 0, Convert.ToInt32(printDocument.DefaultPageSettings.PrintableArea.Width), Convert.ToInt32(printDocument.DefaultPageSettings.PrintableArea.Height));
            printerGeoCanvas.BeginDrawing(printDocument, pagePrinterLayer.GetBoundingBox(), Map1.MapUnit);

            // Loop through all of the PrintingLayer in the PrinterInteractiveOverlay and print all of the
            // except for the PagePrinterLayer                
            Collection<SimpleCandidate> labelsInAllLayers = new Collection<SimpleCandidate>();
            foreach (PrinterLayer printerLayer in printerInteractiveOverlay.PrinterLayers)
            {
                printerLayer.IsDrawing = true;
                if (!(printerLayer is PagePrinterLayer))
                {
                    printerLayer.Draw(printerGeoCanvas, labelsInAllLayers);
                }
                printerLayer.IsDrawing = false;
            }

            printerGeoCanvas.EndDrawing();
        }
        #endregion

        #region Map Events
        private void Map1_CurrentScaleChanged(object sender, CurrentScaleChangedWpfMapEventArgs e)
        {
            // Here we sync up the zoom combox to the map's zoom level.
            PrinterZoomLevelSet printerZoomLevelSet = (PrinterZoomLevelSet)Map1.ZoomLevelSet;
            ZoomLevel currentZoomLevel = printerZoomLevelSet.GetZoomLevel(Map1.CurrentExtent, Map1.ActualWidth, Map1.MapUnit);
            cbxPercentage.SelectedItem = printerZoomLevelSet.GetZoomPercentage(currentZoomLevel) + "%";
        }

        private void Map1_MapClick(object sender, MapClickWpfMapEventArgs e)
        {
            // Here we loop through all of the PrinterLayers to find the one the user right mouse clicked on.
            // Then we show the right click menu to give the user some options
            if (e.MouseButton == MapMouseButton.Right)
            {
                ((MenuItem)Map1.ContextMenu.Items[0]).IsEnabled = false;
                ((MenuItem)Map1.ContextMenu.Items[1]).IsEnabled = false;

                PrinterInteractiveOverlay printerOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];
                for (int i = printerOverlay.PrinterLayers.Count - 1; i >= 0; i--)
                {
                    if (printerOverlay.PrinterLayers[i].GetType() != typeof(PagePrinterLayer))
                    {
                        RectangleShape boundingBox = printerOverlay.PrinterLayers[i].GetPosition();
                        if (boundingBox.Contains(e.WorldLocation))
                        {
                            ((MenuItem)Map1.ContextMenu.Items[0]).Tag = printerOverlay.PrinterLayers[i];
                            ((MenuItem)Map1.ContextMenu.Items[0]).IsEnabled = true;

                            ((MenuItem)Map1.ContextMenu.Items[1]).Tag = printerOverlay.PrinterLayers[i];
                            ((MenuItem)Map1.ContextMenu.Items[1]).IsEnabled = true;

                            break;
                        }
                    }
                }
            }
        }

        #endregion

        #region Pan & Zoom Buttons

        private void btnZoomIn_Click(object sender, RoutedEventArgs e)
        {
            // Grab the MapPrinterLayer and adjust the extent
            PrinterInteractiveOverlay printerOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];
            MapPrinterLayer mapPrinterLayer = ((MapPrinterLayer)(printerOverlay.PrinterLayers["MapLayer"]));

            // Here we snap the current scale to the default zoomLevelSet when we zoom in
            ZoomLevelSet zoomLevelSet = new ZoomLevelSet();
            double newScale = ZoomLevelSet.GetLowerZoomLevelScale(ExtentHelper.GetScale(mapPrinterLayer.MapExtent, (float)mapPrinterLayer.GetBoundingBox().Width, mapPrinterLayer.MapUnit), zoomLevelSet);
            mapPrinterLayer.MapExtent = ExtentHelper.ZoomToScale(newScale, mapPrinterLayer.MapExtent, mapPrinterLayer.MapUnit, (float)mapPrinterLayer.GetBoundingBox().Width, (float)mapPrinterLayer.GetBoundingBox().Height);

            Map1.Refresh();
        }

        private void btnZoomOut_Click(object sender, RoutedEventArgs e)
        {
            // Grab the MapPrinterLayer and adjust the extent
            PrinterInteractiveOverlay printerOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];
            MapPrinterLayer mapPrinterLayer = ((MapPrinterLayer)(printerOverlay.PrinterLayers["MapLayer"]));

            // Here we snap the current scale to the default zoomLevelSet when we zoom in
            ZoomLevelSet zoomLevelSet = new ZoomLevelSet();
            double newScale = ZoomLevelSet.GetHigherZoomLevelScale(ExtentHelper.GetScale(mapPrinterLayer.MapExtent, (float)mapPrinterLayer.GetBoundingBox().Width, mapPrinterLayer.MapUnit), zoomLevelSet);
            mapPrinterLayer.MapExtent = ExtentHelper.ZoomToScale(newScale, mapPrinterLayer.MapExtent, mapPrinterLayer.MapUnit, (float)mapPrinterLayer.GetBoundingBox().Width, (float)mapPrinterLayer.GetBoundingBox().Height);

            Map1.Refresh();
        }

        private void btnPan_Click(object sender, EventArgs e)
        {
            // Grab the MapPrinterLayer and adjust the extent
            PrinterInteractiveOverlay printerOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];

            MapPrinterLayer mapPrinterLayer = ((MapPrinterLayer)(printerOverlay.PrinterLayers["MapLayer"]));

            if (sender == btnPanNorth)
            { mapPrinterLayer.MapExtent = ExtentHelper.Pan(mapPrinterLayer.MapExtent, PanDirection.Up, 30); }
            else if (sender == btnPanSouth)
            { mapPrinterLayer.MapExtent = ExtentHelper.Pan(mapPrinterLayer.MapExtent, PanDirection.Down, 30); }
            else if (sender == btnPanWest)
            { mapPrinterLayer.MapExtent = ExtentHelper.Pan(mapPrinterLayer.MapExtent, PanDirection.Left, 30); }
            else if (sender == btnPanEast)
            { mapPrinterLayer.MapExtent = ExtentHelper.Pan(mapPrinterLayer.MapExtent, PanDirection.Right, 30); }

            Map1.Refresh();
        }

        #endregion

        #region Combo Boxes & ContextMenu

        private void SetupContextMenu()
        {
            Map1.ContextMenu = new System.Windows.Controls.ContextMenu();

            MenuItem removeItem = new MenuItem();
            removeItem.Header = "Remove";
            removeItem.Click += new RoutedEventHandler(RemoveLayer_Click);
            Map1.ContextMenu.Items.Add(removeItem);

            MenuItem editItem = new MenuItem();
            editItem.Header = "Property";
            editItem.Click += new RoutedEventHandler(EditLayer_Click);
            Map1.ContextMenu.Items.Add(editItem);
        }

        private void SetupComboBoxes()
        {
            // Set the default combobox items
            cbxPaperSize.SelectedIndex = 0;
            cbxOrientation.SelectedIndex = 0;

            //Setup the zoom level combobox to relfect the zoom percentages
            foreach (ZoomLevel level in Map1.ZoomLevelSet.GetZoomLevels())
            {
                cbxPercentage.Items.Add(((PrinterZoomLevelSet)Map1.ZoomLevelSet).GetZoomPercentage(level) + "%");
            }
        }

        private void cbxPaperSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded)
            {
                // When the page size combobox is changed then we update the PagePrinterLayer to reflect the changes
                PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];
                PagePrinterLayer pagePrinterLayer = (PagePrinterLayer)printerInteractiveOverlay.PrinterLayers["PageLayer"];

                string selectText = ((ComboBoxItem)e.AddedItems[0]).Content.ToString();
                if (selectText.StartsWith("ANSI A"))
                    pagePrinterLayer.PageSize = PrinterPageSize.AnsiA;
                else if (selectText.StartsWith("ANSI B"))
                    pagePrinterLayer.PageSize = PrinterPageSize.AnsiB;
                else if (selectText.StartsWith("ANSI C"))
                    pagePrinterLayer.PageSize = PrinterPageSize.AnsiC;
                else if (selectText.StartsWith("ANSI D"))
                    pagePrinterLayer.PageSize = PrinterPageSize.AnsiD;
                else if (selectText.StartsWith("ANSI E"))
                    pagePrinterLayer.PageSize = PrinterPageSize.AnsiE;

                Map1.Refresh();
            }
        }

        private void cbxOrientation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // When the orientation combobox is changed then we update the PagePrinterLayer to reflect the changes
            if (this.IsLoaded)
            {
                PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];
                PagePrinterLayer pagePrinterLayer = (PagePrinterLayer)printerInteractiveOverlay.PrinterLayers["PageLayer"];

                string selectText = ((ComboBoxItem)e.AddedItems[0]).Content.ToString();
                if (selectText.ToUpper() == "LANDSCAPE")
                {
                    pagePrinterLayer.Orientation = PrinterOrientation.Landscape;
                }
                else if (selectText.ToUpper() == "PORTRAIT")
                {
                    pagePrinterLayer.Orientation = PrinterOrientation.Portrait;
                }

                Map1.Refresh();
            }
        }

        private void cbxPercentage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                String zoomString = (String)cbxPercentage.SelectedItem;
                double currentZoom = Double.Parse(zoomString.Replace("%", ""));
                Map1.CurrentScale = PrinterHelper.GetPointsPerGeographyUnit(Map1.MapUnit) / (currentZoom / 100);
                Map1.Refresh();
            }
        }

        #endregion

        #region Toolbox

        private void toolBoxItem_Click(object sender, RoutedEventArgs e)
        {
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];
            PrinterLayer printerLayer = null;

            if (sender == btnAddLabel)
            {
                printerLayer = new LabelPrinterLayer();
                printerLayer.SetPosition(2, 1, 0, 0, PrintingUnit.Inch);
            }
            else if (sender == btnAddImage)
            {
                printerLayer = new ImagePrinterLayer();
            }
            else if (sender == btnAddScaleLine)
            {
                MessageBox.Show("NotImplemented");
            }
            else if (sender == btnAddScaleBar)
            {
                MessageBox.Show("NotImplemented");
            }
            else if (sender == btnAddDataGrid)
            {
                printerLayer = new DataGridPrinterLayer();
                printerLayer.SetPosition(1, 1, 0, 0, PrintingUnit.Inch);
            }

            //if (ShowPrinterLayerProperties(printerLayer) == DialogResult.OK)
            if (ShowPrinterLayerProperties(printerLayer) == true)
            {
                printerInteractiveOverlay.PrinterLayers.Add(printerLayer);
                Map1.Refresh();
            }
        }
        #endregion

        #region Right Click and Dialogs

        private void RemoveLayer_Click(object sender, RoutedEventArgs e)
        {
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];
            PrinterLayer printerLayer = (PrinterLayer)((MenuItem)sender).Tag;

            printerInteractiveOverlay.PrinterLayers.Remove(printerLayer);

            Map1.Refresh();
        }

        private void EditLayer_Click(object sender, RoutedEventArgs e)
        {
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)Map1.InteractiveOverlays["PrintPreviewOverlay"];
            PrinterLayer printerLayer = (PrinterLayer)((MenuItem)sender).Tag;

            ShowPrinterLayerProperties(printerLayer);
            Map1.Refresh();
        }

        private bool? ShowPrinterLayerProperties(PrinterLayer printerLayer)
        {
            bool? dialogResult = false;

            if (printerLayer != null)
            {
                if (printerLayer.GetType() == typeof(LabelPrinterLayer))
                {
                    LabelPrinterLayerProperty dialog = new LabelPrinterLayerProperty((LabelPrinterLayer)printerLayer);
                    dialog.Owner = Application.Current.MainWindow;
                    dialogResult = dialog.ShowDialog();
                }
                else if (printerLayer.GetType() == typeof(ScaleBarPrinterLayer))
                {
                    MessageBox.Show("Scalebar (Coming Soon)");
                }
                else if (printerLayer.GetType() == typeof(ScaleLinePrinterLayer))
                {
                    MessageBox.Show("Scaleline (Coming Soon)");
                }
                else if (printerLayer.GetType() == typeof(ImagePrinterLayer))
                {
                    ImagePrinterLayerProperty dialog = new ImagePrinterLayerProperty((ImagePrinterLayer)printerLayer);
                    dialog.Owner = Application.Current.MainWindow;
                    dialogResult = dialog.ShowDialog();
                }
                else if (printerLayer.GetType() == typeof(DataGridPrinterLayer))
                {
                    DataGridPrinterLayerProperty dialog = new DataGridPrinterLayerProperty((DataGridPrinterLayer)printerLayer);
                    dialog.Owner = Application.Current.MainWindow;
                    dialogResult = dialog.ShowDialog();
                }
            }

            return dialogResult;
        }
        #endregion
    }
}

