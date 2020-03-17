using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for PrintingMaps.xaml
    /// </summary>
    [Serializable]
    public partial class PrintingMaps : UserControl
    {
        public PrintingMaps()
        {
            InitializeComponent();
        }

        public void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            SetupMapWithBlankPage();
            AddCenteredMapTitles();
            AddMapLayer();

            AddLogoImage();
            AddScaleLineLayer();
            AddLegendLayer1();
            AddLegendLayer2();

            AddGridTitle1();
            AddDataGridLayer1();

            AddGridTitle2();
            AddDataGridLayer2();
            mapView.Refresh();
        }


        private void SetupMapWithBlankPage()
        {
            // Setup the map unit, you want to always use feet or meters for the printer layout map.
            mapView.MapUnit = GeographyUnit.Meter;

            // Here we create the default zoom levels for the sample.  We have pre-created a PrinterZoomLevelSet
            // That pre-defines commonly used zoom levels based on percentages of zoom
            mapView.ZoomLevelSet = new PrinterZoomLevelSet(mapView.MapUnit, PrinterHelper.GetPointsPerGeographyUnit(mapView.MapUnit));

            // Here we set the background color to gray so there is contrast with the white page
            mapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.LightGray);

            // Create the PrinterInteractiveOverlay to contain all of the PrinterLayers.
            // The interactive overlay allows the layers to be interacted with
            PrinterInteractiveOverlay printerOverlay = new PrinterInteractiveOverlay();

            mapView.InteractiveOverlays.Add("PrintPreviewOverlay", printerOverlay);
            mapView.InteractiveOverlays.MoveToBottom("PrintPreviewOverlay");

            // Create the PagePrinterLayer which shows the page boundary and is the area the user will
            // arrange all of the other layer on top of.
            PagePrinterLayer pagePrinterLayer = new PagePrinterLayer(PrinterPageSize.AnsiA, PrinterOrientation.Portrait);
            pagePrinterLayer.Open();
            printerOverlay.PrinterLayers.Add("PageLayer", pagePrinterLayer);

            // Get the pages extent, slightly scale it up, and then set that as the default current extent
            mapView.CurrentExtent = RectangleShape.ScaleUp(pagePrinterLayer.GetPosition(), 10).GetBoundingBox();

            // Set the minimum sscale of the map to the last zoom level
            mapView.MinimumScale = mapView.ZoomLevelSet.ZoomLevel20.Scale;
        }

        private void AddCenteredMapTitles()
        {
            RectangleShape pageBoundingbox = GetPageBoundingBox(PrintingUnit.Inch);

            //For the main title
            LabelPrinterLayer titleLabelPrinterLayer = new LabelPrinterLayer();
            //Setup the text and the font..
            titleLabelPrinterLayer.Text = "Tarrant County Parcels";
            titleLabelPrinterLayer.Font = new GeoFont("Arial", 8, DrawingFontStyles.Bold);
            titleLabelPrinterLayer.TextBrush = new GeoSolidBrush(GeoColors.Black);

            // Set the title position so that is it centered on the page.                     
            PointShape titleCenterPointShape = new PointShape();
            titleCenterPointShape.X = pageBoundingbox.UpperRightPoint.X - pageBoundingbox.Width / 2;
            titleCenterPointShape.Y = pageBoundingbox.UpperLeftPoint.Y - 0.3;
            titleLabelPrinterLayer.PrinterWrapMode = PrinterWrapMode.AutoSizeText;
            titleLabelPrinterLayer.SetPosition(3, 0.5, titleCenterPointShape, PrintingUnit.Inch);

            //For the subtitle
            LabelPrinterLayer subTitleLabelPrinterLayer = new LabelPrinterLayer();
            //Setup the text and the font..
            subTitleLabelPrinterLayer.Text = "Affected Parcels By New Proposal";
            subTitleLabelPrinterLayer.Font = new GeoFont("Arial", 2, DrawingFontStyles.Bold);
            subTitleLabelPrinterLayer.TextBrush = new GeoSolidBrush(GeoColors.Black);

            // Set the sub title position so that is it centered on the page below the title.                       
            PointShape subtitleCenterPointShape = new PointShape();
            subtitleCenterPointShape.X = pageBoundingbox.UpperRightPoint.X - pageBoundingbox.Width / 2;
            subtitleCenterPointShape.Y = pageBoundingbox.UpperLeftPoint.Y - 0.6;
            subTitleLabelPrinterLayer.PrinterWrapMode = PrinterWrapMode.AutoSizeText;
            subTitleLabelPrinterLayer.SetPosition(3, 0.3, subtitleCenterPointShape, PrintingUnit.Inch);

            // Find the PrinterInteractiveOverlay so we can add the new LabelPrinterLayer
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)mapView.InteractiveOverlays["PrintPreviewOverlay"];
            printerInteractiveOverlay.PrinterLayers.Add("titleLabelPrinterLayer", titleLabelPrinterLayer);
            printerInteractiveOverlay.PrinterLayers.Add("subtitleLabelPrinterLayer", subTitleLabelPrinterLayer);

        }

        private void AddMapLayer()
        {
            // Create the MapPrinterLayer and set the position
            MapPrinterLayer mapPrinterLayer = new MapPrinterLayer();
            //Map Printer Layer is in feet (State Plane Texas North Central Feet NAD83)
            mapPrinterLayer.MapUnit = GeographyUnit.Feet;
            //Set the extent of the MapPrinterLayer with world coordinates.
            RectangleShape currentExtent = new RectangleShape(2276456, 6897886, 2280391, 6895437);
            mapPrinterLayer.MapExtent = currentExtent;
            mapPrinterLayer.BackgroundMask = new AreaStyle(new GeoPen(GeoColors.Black, 1));
            mapPrinterLayer.Open();

            // Set the maps position slightly above the page center and 8 inches wide and 7 inches tall
            RectangleShape pageBoundingbox = GetPageBoundingBox(PrintingUnit.Inch);
            mapPrinterLayer.SetPosition(8, 7, pageBoundingbox.GetCenterPoint().X, pageBoundingbox.GetCenterPoint().Y + 1, PrintingUnit.Inch);

            //Setup the styles for the parcels layer.                
            ShapeFileFeatureLayer parcelsLayer = new ShapeFileFeatureLayer(@"..\..\..\Data\Shapefile\parcels.shp", FileAccess.Read);
            //Creates a value style for the parcel type (residential 1, commercial 2, industrial 3).
            ValueStyle typeValueStyle = new ValueStyle();
            typeValueStyle.ColumnName = "PARCELTYPE";
            typeValueStyle.ValueItems.Add(new ValueItem("1", new AreaStyle(new GeoPen(GeoColors.Black), new GeoSolidBrush(GeoColors.LightGreen))));
            typeValueStyle.ValueItems.Add(new ValueItem("2", new AreaStyle(new GeoPen(GeoColors.Black), new GeoSolidBrush(GeoColors.LightPink))));
            typeValueStyle.ValueItems.Add(new ValueItem("3", new AreaStyle(new GeoPen(GeoColors.Black), new GeoSolidBrush(GeoColors.LightBlue))));
            parcelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(typeValueStyle);

            //Creates a value style for affected parcels by new proposal
            ValueStyle affectedValueStyle = new ValueStyle();
            affectedValueStyle.ColumnName = "AFFECTED";
            affectedValueStyle.ValueItems.Add(new ValueItem("YES", new AreaStyle(new GeoPen(GeoColors.Red, 4), new GeoSolidBrush(GeoColors.Transparent))));
            affectedValueStyle.ValueItems.Add(new ValueItem("D", new AreaStyle(new GeoPen(GeoColors.Orange, 4), new GeoSolidBrush(GeoColors.Transparent))));
            parcelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(affectedValueStyle);

            //Creates a text style to display tax id of parcels
            TextStyle textStyle = new TextStyle("TAXPIN", new GeoFont("Arial", 12, DrawingFontStyles.Bold), new GeoSolidBrush(GeoColors.Black));
            textStyle.HaloPen = new GeoPen(GeoColors.White, 2);
            parcelsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textStyle);
            parcelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the new parcels layer to the MapPrinterLayer
            mapPrinterLayer.Layers.Add(parcelsLayer);

            // Add the MapPrinterLayer to the PrinterInteractiveOverlay
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)mapView.InteractiveOverlays["PrintPreviewOverlay"];
            printerInteractiveOverlay.PrinterLayers.Add("MapLayer", mapPrinterLayer);
        }

        private void AddLegendLayer1()
        {
            //Set the legend for the parcel type on top of the map in the upper left corner.
            LegendItem titleLegendItem = new LegendItem();
            titleLegendItem.TextStyle = new TextStyle("Parcel Types", new GeoFont("Arial", 10, DrawingFontStyles.Bold), new GeoSolidBrush(GeoColors.Black));

            LegendItem legendItem1 = new LegendItem();
            legendItem1.ImageStyle = new AreaStyle(new GeoPen(GeoColors.Black), new GeoSolidBrush(GeoColors.LightGreen));
            legendItem1.TextStyle = new TextStyle("Residential", new GeoFont("Arial", 8), new GeoSolidBrush(GeoColors.Black));

            LegendItem legendItem2 = new LegendItem();
            legendItem2.ImageStyle = new AreaStyle(new GeoPen(GeoColors.Black), new GeoSolidBrush(GeoColors.LightPink));
            legendItem2.TextStyle = new TextStyle("Industrial", new GeoFont("Arial", 8), new GeoSolidBrush(GeoColors.Black));

            LegendItem legendItem3 = new LegendItem();
            legendItem3.ImageStyle = new AreaStyle(new GeoPen(GeoColors.Black), new GeoSolidBrush(GeoColors.LightBlue));
            legendItem3.TextStyle = new TextStyle("Industrial", new GeoFont("Arial", 8), new GeoSolidBrush(GeoColors.Black));

            LegendAdornmentLayer legendAdornmentLayer = new LegendAdornmentLayer();
            legendAdornmentLayer.Height = 130;
            legendAdornmentLayer.Width = 140;

            legendAdornmentLayer.Title = titleLegendItem;
            legendAdornmentLayer.LegendItems.Add(legendItem1);
            legendAdornmentLayer.LegendItems.Add(legendItem2);
            legendAdornmentLayer.LegendItems.Add(legendItem3);

            LegendPrinterLayer legendPrinterLayer = new LegendPrinterLayer(legendAdornmentLayer);

            legendPrinterLayer.SetPosition(2, 1, -2.9, 3.9, PrintingUnit.Inch);

            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)mapView.InteractiveOverlays["PrintPreviewOverlay"];
            printerInteractiveOverlay.PrinterLayers.Add("LegendPrinterLayer1", legendPrinterLayer);
        }

        private void AddLegendLayer2()
        {
            //Set the legend for the new proposal on top of the map in the upper right corner.
            LegendItem titleLegendItem = new LegendItem();
            titleLegendItem.TextStyle = new TextStyle("New Proposal", new GeoFont("Arial", 10, DrawingFontStyles.Bold), new GeoSolidBrush(GeoColors.Black));

            LegendItem legendItem1 = new LegendItem();
            legendItem1.ImageStyle = new AreaStyle(new GeoPen(GeoColors.Red, 4), new GeoSolidBrush(GeoColors.Transparent));
            legendItem1.TextStyle = new TextStyle("Approved", new GeoFont("Arial", 8), new GeoSolidBrush(GeoColors.Black));

            LegendItem legendItem2 = new LegendItem();
            legendItem2.ImageStyle = new AreaStyle(new GeoPen(GeoColors.Orange, 4), new GeoSolidBrush(GeoColors.Transparent));
            legendItem2.TextStyle = new TextStyle("In Discussion", new GeoFont("Arial", 8), new GeoSolidBrush(GeoColors.Black));

            LegendAdornmentLayer legendAdornmentLayer = new LegendAdornmentLayer();
            legendAdornmentLayer.Height = 100;
            legendAdornmentLayer.Width = 140;

            legendAdornmentLayer.Title = titleLegendItem;
            legendAdornmentLayer.LegendItems.Add(legendItem1);
            legendAdornmentLayer.LegendItems.Add(legendItem2);

            LegendPrinterLayer legendPrinterLayer = new LegendPrinterLayer(legendAdornmentLayer);

            legendPrinterLayer.SetPosition(2, 1, 2.9, 3.9, PrintingUnit.Inch);

            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)mapView.InteractiveOverlays["PrintPreviewOverlay"];
            printerInteractiveOverlay.PrinterLayers.Add("LegendPrinterLayer2", legendPrinterLayer);
        }

        private void AddGridTitle1()
        {
            //Set the grid tile for approved parcels on top of the left grid.
            RectangleShape pageBoundingbox = GetPageBoundingBox(PrintingUnit.Inch);
            //For the grid title
            LabelPrinterLayer gridTitleLabelPrinterLayer = new LabelPrinterLayer();
            //Setup the text and the font..
            gridTitleLabelPrinterLayer.Text = "Approved Parcels";
            gridTitleLabelPrinterLayer.Font = new GeoFont("Arial", 8, DrawingFontStyles.Bold);
            gridTitleLabelPrinterLayer.TextBrush = new GeoSolidBrush(GeoColors.Black);

            // Set the title position so that is it centered on the page one inch from the top and to the left                       
            gridTitleLabelPrinterLayer.PrinterWrapMode = PrinterWrapMode.AutoSizeText;
            gridTitleLabelPrinterLayer.SetPosition(3, 0.2, new PointShape(-2.25, pageBoundingbox.GetCenterPoint().Y - 2.9), PrintingUnit.Inch);

            //Find the PrinterInteractiveOverlay so we can add the new LabelPrinterLayer
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)mapView.InteractiveOverlays["PrintPreviewOverlay"];
            printerInteractiveOverlay.PrinterLayers.Add("gridtitle1LabelPrinterLayer", gridTitleLabelPrinterLayer);
        }

        private void AddDataGridLayer1()
        {
            // Create the DataGridPrinterLayer for Approved parcels and place it the left.
            DataGridPrinterLayer dataGridPrinterLayer = new DataGridPrinterLayer();
            dataGridPrinterLayer.TextFont = new GeoFont("Arial", 8);
            dataGridPrinterLayer.TextHorizontalAlignment = PrinterTextHorizontalAlignment.Left;

            // Set the data grid position 4.2 inches below the page center to the left and 3.5 inches wide and 2 inches tall
            RectangleShape pageBoundingbox = GetPageBoundingBox(PrintingUnit.Inch);
            dataGridPrinterLayer.SetPosition(3.5, 2, -2.25, pageBoundingbox.GetCenterPoint().Y - 4.2, PrintingUnit.Inch);

            //Create the data table and columns
            dataGridPrinterLayer.DataTable = new DataTable();
            dataGridPrinterLayer.DataTable.Columns.Add("TAXPIN");
            dataGridPrinterLayer.DataTable.Columns.Add("LAST_REVIS");
            dataGridPrinterLayer.DataTable.Columns.Add("ACRES");
            dataGridPrinterLayer.DataTable.Columns.Add("CALCULATED");

            // Find all of the parcels that are approved and add those items to the data table
            ShapeFileFeatureLayer shapefileFeatureLayer = new ShapeFileFeatureLayer(@"..\..\..\Data\Shapefile\Parcels.shp", FileAccess.Read);
            shapefileFeatureLayer.Open();
            Collection<Feature> features = shapefileFeatureLayer.QueryTools.GetFeaturesByColumnValue("AFFECTED", "YES", ReturningColumnsType.AllColumns);
            shapefileFeatureLayer.Close();

            foreach (Feature feature in features)
            {
                dataGridPrinterLayer.DataTable.Rows.Add(new object[4] { feature.ColumnValues["TAXPIN"], feature.ColumnValues["LAST_REVIS"], feature.ColumnValues["ACRES"], feature.ColumnValues["CALCULATED"] });
            }

            // Add the DataGridPrinterLayer to the PrinterInteractiveOverlay
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)mapView.InteractiveOverlays["PrintPreviewOverlay"];
            printerInteractiveOverlay.PrinterLayers.Add("DataGridLayer1", dataGridPrinterLayer);
        }

        private void AddGridTitle2()
        {
            //Set the grid tile for approved parcels on top of the right grid.
            RectangleShape pageBoundingbox = GetPageBoundingBox(PrintingUnit.Inch);
            //For the grid title
            LabelPrinterLayer gridTitleLabelPrinterLayer = new LabelPrinterLayer();
            //Setup the text and the font..
            gridTitleLabelPrinterLayer.Text = "Parcels In Discussion";
            gridTitleLabelPrinterLayer.Font = new GeoFont("Arial", 8, DrawingFontStyles.Bold);
            gridTitleLabelPrinterLayer.TextBrush = new GeoSolidBrush(GeoColors.Black);

            // Set the title position so that is it centered on the page one inch from the top and to the right                       
            gridTitleLabelPrinterLayer.PrinterWrapMode = PrinterWrapMode.AutoSizeText;
            gridTitleLabelPrinterLayer.SetPosition(3, 0.2, new PointShape(2.25, pageBoundingbox.GetCenterPoint().Y - 2.9), PrintingUnit.Inch);
            //titleLabelPrinterLayer.SetPosition(

            // Find the PrinterInteractiveOverlay so we can add the new LabelPrinterLayer
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)mapView.InteractiveOverlays["PrintPreviewOverlay"];
            printerInteractiveOverlay.PrinterLayers.Add("gridtitle2LabelPrinterLayer", gridTitleLabelPrinterLayer);
        }

        private void AddDataGridLayer2()
        {
            // Create the DataGridPrinterLayer for parcels in discussion and place it the right.
            DataGridPrinterLayer dataGridPrinterLayer = new DataGridPrinterLayer();
            dataGridPrinterLayer.TextFont = new GeoFont("Arial", 8);
            dataGridPrinterLayer.TextHorizontalAlignment = PrinterTextHorizontalAlignment.Left;

            // Set the data grid position 4.2 inches below the page center to the right and 3.5 inches wide and 2 inches tall
            RectangleShape pageBoundingbox = GetPageBoundingBox(PrintingUnit.Inch);
            dataGridPrinterLayer.SetPosition(3.5, 2, 2.25, pageBoundingbox.GetCenterPoint().Y - 4.2, PrintingUnit.Inch);

            //Create the data table and columns
            dataGridPrinterLayer.DataTable = new DataTable();
            dataGridPrinterLayer.DataTable.Columns.Add("TAXPIN");
            dataGridPrinterLayer.DataTable.Columns.Add("LAST_REVIS");
            dataGridPrinterLayer.DataTable.Columns.Add("ACRES");
            dataGridPrinterLayer.DataTable.Columns.Add("CALCULATED");

            // Find all of the parcels that are in discussion and add those items to the data table
            ShapeFileFeatureLayer shapefileFeatureLayer = new ShapeFileFeatureLayer(@"..\..\..\Data\Shapefile\Parcels.shp", FileAccess.Read);
            shapefileFeatureLayer.Open();
            Collection<Feature> features = shapefileFeatureLayer.QueryTools.GetFeaturesByColumnValue("AFFECTED", "D", ReturningColumnsType.AllColumns);
            shapefileFeatureLayer.Close();

            foreach (Feature feature in features)
            {
                dataGridPrinterLayer.DataTable.Rows.Add(new object[4] { feature.ColumnValues["TAXPIN"], feature.ColumnValues["LAST_REVIS"], feature.ColumnValues["ACRES"], feature.ColumnValues["CALCULATED"] });
            }

            // Add the DataGridPrinterLayer to the PrinterInteractiveOverlay
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)mapView.InteractiveOverlays["PrintPreviewOverlay"];
            printerInteractiveOverlay.PrinterLayers.Add("DataGridLayer2", dataGridPrinterLayer);
        }

        private void AddLogoImage()
        {
            //Load the image to a GeoImage and then create the ImagePrinterLayer using that image
            GeoImage compassGeoImage = new GeoImage(@"..\..\..\Resources\Compass.png");
            ImagePrinterLayer imagePrinterLayer = new ImagePrinterLayer(compassGeoImage);

            // Set the imagePrinterLayer position offset from the page center and .75 inches wide and .75 inches tall
            RectangleShape pageBoundingbox = GetPageBoundingBox(PrintingUnit.Inch);
            imagePrinterLayer.SetPosition(.75, .75, pageBoundingbox.GetCenterPoint().X + 3.5, pageBoundingbox.GetCenterPoint().Y - 2.0, PrintingUnit.Inch);

            // Add the imagePrinterLayer to the PrinterInteractiveOverlay
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)mapView.InteractiveOverlays["PrintPreviewOverlay"];
            printerInteractiveOverlay.PrinterLayers.Add("ImageLayer", imagePrinterLayer);
        }

        private void AddScaleLineLayer()
        {
            // Get the PrinterInteractiveOverlay
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)mapView.InteractiveOverlays["PrintPreviewOverlay"];

            // Grab the MapPrinterLayer as we need to pass that into the scaleLinePrinterLayer so it
            // can be synced up to the map itself in real-time
            MapPrinterLayer mapPrinterLayer = (MapPrinterLayer)printerInteractiveOverlay.PrinterLayers["MapLayer"];

            //Create the scaleLinePrinterLayer and pass in the MapPrinterLayer
            ScaleLinePrinterLayer scaleLinePrinterLayer = new ScaleLinePrinterLayer(mapPrinterLayer);
            scaleLinePrinterLayer.MapUnit = GeographyUnit.Feet;

            // Set the scale line position offset from the page center and 1.25 inches wide and .25 inches tall
            RectangleShape pageBoundingbox = GetPageBoundingBox(PrintingUnit.Inch);
            scaleLinePrinterLayer.SetPosition(1.25, .25, pageBoundingbox.GetCenterPoint().X - 3.25, pageBoundingbox.GetCenterPoint().Y - 2.25, PrintingUnit.Inch);

            // Add the ScaleLinePrinterLayer to the PrinterInteractiveOverlay
            printerInteractiveOverlay.PrinterLayers.Add("ScaleLineLayer", scaleLinePrinterLayer);
        }

        private RectangleShape GetPageBoundingBox(PrintingUnit unit)
        {
            // This helper method gets the pages bounding box in the unit requested
            PrinterInteractiveOverlay printerInteractiveOverlay = (PrinterInteractiveOverlay)mapView.InteractiveOverlays["PrintPreviewOverlay"];
            PagePrinterLayer pagePrinterLayer = (PagePrinterLayer)printerInteractiveOverlay.PrinterLayers["PageLayer"];
            return pagePrinterLayer.GetPosition(unit); ;
        }
    }
}
