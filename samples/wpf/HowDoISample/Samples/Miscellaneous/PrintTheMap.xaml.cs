using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to print maps using the PrintOverlay.
    /// </summary>
    public partial class PrintTheMap : IDisposable
    {
        public PrintTheMap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the mapView to display a print preview
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            SetupMapForPrinting();
            AddPageTitleLabel();
            AddMapLayers();
            //AddMosquitoDataGrid();

            // Create a legend adornment to display class breaks
            var legend = new LegendAdornmentLayer
            {
                // Set up the legend adornment
                Title = new LegendItem()
                {
                    TextStyle = new TextStyle("Housing Unit Counts Title", new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black)
                },
                Location = AdornmentLocation.Center,
                // Set up the legend adornment
                Footer = new LegendItem()
                {
                    TextStyle = new TextStyle("Housing Unit Counts Foot", new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black)
                },
            };

            // Add a LegendItems to the legend adornment for each ClassBreak
            var legendItem1 = new LegendItem()
            {
                TextStyle = new TextStyle("Housing Unit Counts Item1", new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black)
            };
            legend.LegendItems.Add(legendItem1);
            var legendItem2 = new LegendItem()
            {
                TextStyle = new TextStyle("Housing Unit Counts Item2", new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black)
            };
            legend.LegendItems.Add(legendItem2);

            var printerOverlay = (PrinterInteractiveOverlay)MapView.InteractiveOverlays["printerOverlay"];
            var pageLayer = (PagePrinterLayer)printerOverlay.PrinterLayers["pageLayer"];

            // Set the position of the map using the pageLayer's centerPoint
            var pageCenter = pageLayer.GetPosition().GetCenterPoint();
            var legendPrinterLayer = new LegendPrinterLayer(legend);

            legendPrinterLayer.SetPosition(7.5, 2, pageCenter.X, pageCenter.Y - 3, PrintingUnit.Inch);

            //var overlay = new LayerOverlay();
            //overlay.Layers.Add(printerLayer);
            //MapView.Overlays.Add(overlay);
            //   MapView.AdornmentOverlay.Layers.Add(legend);

            //// Set up the legend adornment
            //legend.Title = null;
            ////legend.Title = new LegendItem()
            ////{
            ////    TextStyle = new TextStyle("Crime Categories", new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black)
            ////};
            //legend.Location = AdornmentLocation.Center;
            //MapView.AdornmentOverlay.Layers.Add(legend);

            //var scaleLine = new ScaleLineAdornmentLayer();
            //scaleLine.TextStyle.Font = new GeoFont("Arial", 10);
            //MapView.AdornmentOverlay.Layers.Add(scaleLine);

            // Add the dataGridLayer to the PrinterLayers collection to print later
            printerOverlay.PrinterLayers.Add(legendPrinterLayer);

            _ = MapView.RefreshAsync();
        }

        /// <summary>
        /// Creates a PrintDocument and draws all the layers for it to print onto.
        /// </summary>
        private void PrintMap_OnClick(object sender, RoutedEventArgs e)
        {
            var printerOverlay = (PrinterInteractiveOverlay)MapView.InteractiveOverlays["printerOverlay"];
            var pageLayer = (PagePrinterLayer)printerOverlay.PrinterLayers["pageLayer"];

            // Create a printDocument that matches the size of our pageLayer
            var printDocument = new PrintDocument
            {
                DefaultPageSettings =
                {
                    Landscape = pageLayer.Orientation == PrinterOrientation.Landscape,
                    PaperSize = new PaperSize("AnsiA", 850, 1100)
                }
            };

            // Create a printerGeoCanvas that will allow us to print Layers onto the printDocument
            var printerGeoCanvas = new PrinterGeoCanvas
            {
                DrawingArea = new Rectangle(0, 0, (int)printDocument.DefaultPageSettings.PrintableArea.Width, (int)printDocument.DefaultPageSettings.PrintableArea.Height)
            };

            // Start drawing on the printDocument
            printerGeoCanvas.BeginDrawing(printDocument, pageLayer.GetBoundingBox(), MapView.MapUnit);

            // Draw each layer in the PrinterLayers collection except for the background PagePrinterLayer
            foreach (var printerLayer in printerOverlay.PrinterLayers.Reverse())
            {
                printerLayer.IsDrawing = true;
                if (!(printerLayer is PagePrinterLayer))
                {
                    // Draw the layer
                    printerLayer.Draw(printerGeoCanvas, new Collection<SimpleCandidate>());
                }

                printerLayer.IsDrawing = false;
            }

            // Finish drawing and send the print commands to the printer
            printerGeoCanvas.EndDrawing();
        }

        /// <summary>
        /// Set up the mapView for a print preview and add a printerOverlay to hold various print layers
        /// </summary>
        private void SetupMapForPrinting()
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Set the map's ZoomLevelSet to a set of common printer zoom settings
            MapView.ZoomLevelSet =
                new PrinterZoomLevelSet(GeographyUnit.Meter, PrinterHelper.GetPointsPerGeographyUnit(GeographyUnit.Meter));
            MapView.MinimumScale = MapView.ZoomLevelSet.ZoomLevel20.Scale;

            var printerOverlay = new PrinterInteractiveOverlay();
            printerOverlay.IsEditable = true;

            var pageLayer = new PagePrinterLayer(PrinterPageSize.AnsiA, PrinterOrientation.Portrait)
            {
                // Style the pageLayer to appear to look like a piece of paper
                BackgroundMask = AreaStyle.CreateSimpleAreaStyle(GeoColors.White, GeoColors.Black)
            };

            // Add the pageLayer to the printerOverlay
            printerOverlay.PrinterLayers.Add("pageLayer", pageLayer);
            // Add the printerOverlay to the map
            MapView.InteractiveOverlays.Add("printerOverlay", printerOverlay);

            // Set the map extent
            var pageLayerBBox = pageLayer.GetPosition().GetBoundingBox();
            MapView.CenterPoint = pageLayerBBox.GetCenterPoint();
            MapView.CurrentScale = MapUtil.GetScale(pageLayerBBox, MapView.ActualWidth, MapView.MapUnit) * 1.5; // Multiply the current scale by a factor like 1.5 (50% increase) to zoom out and expand the map extent.
        }

        /// <summary>
        /// Adds a title to the top of the page
        /// </summary>
        private void AddPageTitleLabel()
        {
            var printerOverlay = (PrinterInteractiveOverlay)MapView.InteractiveOverlays["printerOverlay"];

            var titleLabel = new LabelPrinterLayer("Frisco Mosquito Report - 5/5/2020", new GeoFont("Verdana", 8), GeoBrushes.Black)
            {
                PrinterWrapMode = PrinterWrapMode.AutoSizeText
            };
            titleLabel.SetPosition(7.5, .5, 0, 4.75, PrintingUnit.Inch);

            printerOverlay.PrinterLayers.Add(titleLabel);

        }

        /// <summary>
        /// Creates various layers from shapefile data and adds them to a mapPrinterLayer that will be able to translate the layers into print commands
        /// </summary>
        private void AddMapLayers()
        {
            var printerOverlay = (PrinterInteractiveOverlay)MapView.InteractiveOverlays["printerOverlay"];
            var pageLayer = (PagePrinterLayer)printerOverlay.PrinterLayers["pageLayer"];

            /***************************
             * Create cityLimits layer *
             ***************************/
            var cityLimits = new ShapeFileFeatureLayer(@"./Data/Shapefile/FriscoCityLimits.shp");

            // Style cityLimits layer
            cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.Black, 2);
            cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.DrawingLevel = DrawingLevel.LevelFour;
            cityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Project cityLimits layer to Spherical Mercator to match the mapLayer projection
            cityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            /************************
             * Create streets layer *
             ************************/
            var streets = new ShapeFileFeatureLayer(@"./Data/Shapefile/Streets.shp");

            // Style streets layer
            var majorStreetsStyle = new FilterStyle();
            majorStreetsStyle.Conditions.Add(new FilterCondition("SUBTYPE", "<= 6"));
            majorStreetsStyle.Styles.Add(LineStyle.CreateSimpleLineStyle(GeoColors.DarkGray, 1, true));
            streets.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(majorStreetsStyle);
            streets.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Project streets layer to Spherical Mercator to match the mapLayer projection
            streets.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            /**********************
             * Create parks layer *
             **********************/
            var parks = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");

            // Style parks layer
            parks.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.LightGray, GeoColors.Transparent);
            parks.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Project parks layer to Spherical Mercator to match the mapLayer projection
            parks.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            /*************************
             * Create mosquitoSightings layer *
             *************************/
            var mosquitoSightings = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco_Mosquitos.shp");

            // Style parks layer
            var mayFifthSightings = new FilterStyle();
            mayFifthSightings.Conditions.Add(new FilterCondition("DateCollec", "20200505"));
            mayFifthSightings.Styles.Add(PointStyle.CreateSimpleCircleStyle(GeoColors.Black, 10, GeoColors.White, 2));
            mayFifthSightings.Styles.Add(TextStyle.CreateSimpleTextStyle("TrapSite", "Verdana", 8, DrawingFontStyles.Bold, GeoColors.Black, GeoColors.White, 2, 0, 6));
            mosquitoSightings.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(mayFifthSightings);
            mosquitoSightings.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.DrawingLevel = DrawingLevel.LevelFour;
            mosquitoSightings.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Project parks layer to Spherical Mercator to match the mapLayer projection
            mosquitoSightings.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            /********************************
             * Create mapPrinterLayer layer *
             *******************************/
            // Open cityLimits layer to get the bounding box for the mapLayer
            cityLimits.Open();
            var mapExtent = cityLimits.GetBoundingBox();
            cityLimits.Close();

            // Create the mapPrinterLayer, adding the FeatureLayers that we want to print
            var mapPrinterLayer = new MapPrinterLayer(new LayerBase[] { cityLimits, streets, parks, mosquitoSightings }, mapExtent, GeographyUnit.Meter);

            // Set the position of the map using the pageLayer's centerPoint
            var pageCenter = pageLayer.GetPosition().GetCenterPoint();
            mapPrinterLayer.SetPosition(7.5, 5, pageCenter.X, pageCenter.Y + 1.75, PrintingUnit.Inch);

            printerOverlay.PrinterLayers.Add(mapPrinterLayer);
        }

        /// <summary>
        /// Creates a DataGridLayer containing mosquito trap data collected on 5/5/2020
        /// </summary>
        private void AddMosquitoDataGrid()
        {
            var printerOverlay = (PrinterInteractiveOverlay)MapView.InteractiveOverlays["printerOverlay"];
            var pageLayer = (PagePrinterLayer)printerOverlay.PrinterLayers["pageLayer"];

            // Create a table with columns
            var table = new DataTable();
            table.Columns.Add("TRAP SITE");
            table.Columns.Add("ADDRESS");
            table.Columns.Add("MALES");
            table.Columns.Add("FEMALES");
            table.Columns.Add("WEST NILE DETECTED");

            // Load in the mosquito feature source data
            var mosquitoFeatureSource = new ShapeFileFeatureSource(@"./Data/Shapefile/Frisco_Mosquitos.shp");

            // Query the data for all features whose data was collected on 5/5/2020
            mosquitoFeatureSource.Open();
            var trapSites = mosquitoFeatureSource.GetFeaturesByColumnValue("DateCollec", "20200505", ReturningColumnsType.AllColumns);
            mosquitoFeatureSource.Close();

            // Add a row to the table for every feature
            foreach (var trapSite in trapSites)
            {
                table.Rows.Add(trapSite.ColumnValues["TrapSite"], trapSite.ColumnValues["Address"], trapSite.ColumnValues["Male"], trapSite.ColumnValues["Female"], trapSite.ColumnValues["WestNileMo"]);
            }

            // Create the dataGridLayer that will display the mosquito data
            var dataGridLayer = new DataGridPrinterLayer(table, new GeoFont("Verdana", 8), new GeoFont("Verdana", 8, DrawingFontStyles.Bold));

            // Set the position of the map using the pageLayer's centerPoint
            var pageCenter = pageLayer.GetPosition().GetCenterPoint();
            dataGridLayer.SetPosition(7.5, 4, pageCenter.X, pageCenter.Y - 3, PrintingUnit.Inch);

            // Add the dataGridLayer to the PrinterLayers collection to print later
            printerOverlay.PrinterLayers.Add(dataGridLayer);
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}