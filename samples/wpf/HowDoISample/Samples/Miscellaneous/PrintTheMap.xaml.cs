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

        private bool _initialized;
        public PrintTheMap()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map to display a print preview
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            SetupMapForPrinting();
            AddPageTitleLabel();
            AddMapLayers();
            AddMosquitoDataGrid();

            _ = Map.RefreshAsync();
        }

        /// <summary>
        /// Creates a PrintDocument and draws all the layers for it to print onto.
        /// </summary>
        private void PrintMap_OnClick(object sender, RoutedEventArgs e)
        {
            var printerOverlay = (PrinterInteractiveOverlay)Map.InteractiveOverlays["printerOverlay"];
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
            printerGeoCanvas.BeginDrawing(printDocument, pageLayer.GetBoundingBox(), Map.MapUnit);

            // Draw each layer in collection order (same order as preview rendering).
            foreach (var printerLayer in printerOverlay.PrinterLayers)
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
        /// Set up the map for a print preview and add a printerOverlay to hold various print layers
        /// </summary>
        private void SetupMapForPrinting()
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            Map.MapUnit = GeographyUnit.Meter;

            // Set the map's ZoomLevelSet to a set of common printer zoom settings
            Map.ZoomScales =
                new PrinterZoomLevelSet(GeographyUnit.Meter, PrinterHelper.GetPointsPerGeographyUnit(GeographyUnit.Meter)).GetScales();
            Map.MinimumScale = Map.ZoomScales[Map.ZoomScales.Count - 1];

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
            Map.InteractiveOverlays.Add("printerOverlay", printerOverlay);

            // Set the map extent
            var pageLayerBBox = pageLayer.GetPosition().GetBoundingBox();
            Map.CenterPoint = pageLayerBBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, pageLayerBBox, Map.MapWidth, Map.MapHeight);
        }

        /// <summary>
        /// Adds a title to the top of the page
        /// </summary>
        private void AddPageTitleLabel()
        {
            var printerOverlay = (PrinterInteractiveOverlay)Map.InteractiveOverlays["printerOverlay"];

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
            var printerOverlay = (PrinterInteractiveOverlay)Map.InteractiveOverlays["printerOverlay"];
            var pageLayer = (PagePrinterLayer)printerOverlay.PrinterLayers["pageLayer"];

            /***************************
             * Create cityLimits layer *
             ***************************/
            var cityLimits = new ShapeFileFeatureLayer(@"./Data/Shapefile/FriscoCityLimits.shp");

            // Style cityLimits layer
            var cityLimitsStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.Black, 2);
            cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = cityLimitsStyle;
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
            var majorStreetsLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.DarkGray, 1, true);
            majorStreetsStyle.Conditions.Add(new FilterCondition("SUBTYPE", "<= 6"));
            majorStreetsStyle.Styles.Add(majorStreetsLineStyle);
            streets.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(majorStreetsStyle);
            streets.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Project streets layer to Spherical Mercator to match the mapLayer projection
            streets.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            /**********************
             * Create parks layer *
             **********************/
            var parks = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");

            // Style parks layer
            var parksStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.LightGray, GeoColors.Transparent);
            parks.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = parksStyle;
            parks.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Project parks layer to Spherical Mercator to match the mapLayer projection
            parks.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            /*************************
             * Create mosquitoSightings layer *
             *************************/
            var mosquitoSightings = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco_Mosquitos.shp");

            // Style mosquitoSightings layer
            var mayFifthSightings = new FilterStyle();
            var mosquitoSightingsPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Black, 10, GeoColors.White, 2);
            mayFifthSightings.Conditions.Add(new FilterCondition("DateCollec", "20200505"));
            mayFifthSightings.Styles.Add(mosquitoSightingsPointStyle);
            mayFifthSightings.Styles.Add(TextStyle.CreateSimpleTextStyle("TrapSite", "Verdana", 8, DrawingFontStyles.Bold, GeoColors.Black, GeoColors.White, 2, 0, 6));
            mosquitoSightings.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(mayFifthSightings);
            mosquitoSightings.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.DrawingLevel = DrawingLevel.LevelFour;
            mosquitoSightings.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Project mosquitoSightings layer to Spherical Mercator to match the mapLayer projection
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

            // Add a legend describing the map symbology.
            var legendPrinterLayer = CreateLegendPrinterLayer(cityLimitsStyle, majorStreetsLineStyle, parksStyle, mosquitoSightingsPointStyle);
            // Place a compact legend near the bottom-right corner of the printed map.
            legendPrinterLayer.SetPosition(1.40, 1.05, pageCenter.X + 2.95, pageCenter.Y - 0.12, PrintingUnit.Inch);
            printerOverlay.PrinterLayers.Add(legendPrinterLayer);

            // Add a scale bar that reflects the map printer layer extent and scale.
            var scaleBarPrinterLayer = new ScaleBarPrinterLayer(mapPrinterLayer)
            {
                MapUnit = GeographyUnit.Meter,
                UnitFamily = UnitSystem.Imperial,
                HasMask = true,
                DynamicBoundingBox = true
            };
            scaleBarPrinterLayer.SetPosition(2.15, 0.45, pageCenter.X - 2.35, pageCenter.Y - 0.45, PrintingUnit.Inch);
            printerOverlay.PrinterLayers.Add(scaleBarPrinterLayer);
        }

        private static LegendPrinterLayer CreateLegendPrinterLayer(AreaStyle cityLimitsStyle, LineStyle majorStreetsLineStyle, AreaStyle parksStyle, PointStyle mosquitoSightingsPointStyle)
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

            legendAdornment.LegendItems.Add(CreateLegendItem("City Limits", cityLimitsStyle));
            legendAdornment.LegendItems.Add(CreateLegendItem("Major Streets", majorStreetsLineStyle));
            legendAdornment.LegendItems.Add(CreateLegendItem("Parks", parksStyle));
            legendAdornment.LegendItems.Add(CreateLegendItem("Mosquito Site", mosquitoSightingsPointStyle));

            return new LegendPrinterLayer(legendAdornment);
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
        /// Creates a DataGridLayer containing mosquito trap data collected on 5/5/2020
        /// </summary>
        private void AddMosquitoDataGrid()
        {
            var printerOverlay = (PrinterInteractiveOverlay)Map.InteractiveOverlays["printerOverlay"];
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
            Map.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
