using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to generate and display an ESRI Grid Layer on the map
    /// We start with a shapefile of points with each point containing a soil PH value.
    /// Clicking the Generate button will build a .grd file using an Inverse Distance Weighted (IDW) interpolation model and then displays the grid.
    /// Although this sample is a soil map, this same technique can be used to translate any other point-based data to a grid
    /// </summary>
    public partial class GenerateESRIGridFile : IDisposable
    {
        public GenerateESRIGridFile()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the ESRI Grid layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MapView.MapUnit = GeographyUnit.Meter;
                MapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.Snow);

                // Create background hybrid satellite map requested from ThinkGeo Cloud Service. 
                var cloudOverlay = new ThinkGeoCloudRasterMapsOverlay
                {
                    ClientId = SampleKeys.ClientId,
                    ClientSecret = SampleKeys.ClientSecret,
                    MapType = ThinkGeoCloudRasterMapsMapType.Hybrid_V2_X1
                };
                MapView.Overlays.Add("Cloud Overlay", cloudOverlay);

                //Applies class break style to show sample points of pH level of a field.
                var classBreakStyle = new ClassBreakStyle("PH");
                const byte alpha = 180;
                const int symbolSize = 10;
                classBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColors.Transparent))));
                classBreakStyle.ClassBreaks.Add(new ClassBreak(6.2, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 255, 0, 0)))));
                classBreakStyle.ClassBreaks.Add(new ClassBreak(6.83, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 255, 128, 0)))));
                classBreakStyle.ClassBreaks.Add(new ClassBreak(7.0, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 245, 210, 10)))));
                classBreakStyle.ClassBreaks.Add(new ClassBreak(7.08, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 225, 255, 0)))));
                classBreakStyle.ClassBreaks.Add(new ClassBreak(7.15, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 224, 251, 132)))));
                classBreakStyle.ClassBreaks.Add(new ClassBreak(7.21, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 128, 255, 128)))));
                classBreakStyle.ClassBreaks.Add(new ClassBreak(7.54, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 0, 255, 0)))));

                //load the point shapefile containing ph values in different points of a field.
                var samplesLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/sample_ph_2.shp");
                samplesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(classBreakStyle);
                samplesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                //Create an overlay for our points (and later the grid) and add our points layer to it.
                var gridOverlay = new LayerOverlay
                {
                    TileType = TileType.SingleTile
                };
                gridOverlay.Layers.Add("GridFeatureLayer", samplesLayer);
                MapView.Overlays.Add("GridFeatureOverlay", gridOverlay);

                //set the map's current extent to the point shapefile location.
                samplesLayer.Open();
                var samplesLayerBBox = samplesLayer.GetBoundingBox();
                MapView.CenterPoint = samplesLayerBBox.GetCenterPoint();
                MapView.CurrentScale = MapUtil.GetScale(samplesLayerBBox, MapView.ActualWidth, MapView.MapUnit);
                samplesLayer.Close();

                await MapView.RefreshAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private async void BtnGenerateGridFile_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                // call the functions to generate the grid file and render it.
                const string filename = @"./Data/GridFile/generated.grd";
                GenerateGrid(filename);
                await LoadGridAsync(filename);
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private static void GenerateGrid(string filename)
        {
            //Point based shapefile used to create the GRID (sample points of pH level of a field)
            var pointLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/sample_ph_2.shp");

            //Sets the extent of the grid based on the extent of the sample points shapefile and slightly larger.
            pointLayer.Open();
            var gridExtent = pointLayer.GetBoundingBox();
            gridExtent.ScaleUp(5);

            //Gets the data (points with their pH value) to build the GRID.
            var features = pointLayer.FeatureSource.GetAllFeatures(new[] { "PH" });
            var dataPoints = new Dictionary<PointShape, double>();
            pointLayer.Close();

            foreach (var feature in features)
            {
                var pointShape = (PointShape)feature.GetShape();
                var value = double.Parse(feature.ColumnValues["PH"]);
                dataPoints.Add(pointShape, value);
            }

            //Cell size based on the width of the extent divided by 100.
            var cellSize = gridExtent.Width / 100;
            //Sets the definition of the GRID with its extent, the cell size, the non value, and the data (point locations with their value)
            //For more information on GRID definition see http://en.wikipedia.org/wiki/ASCII_GRID
            var definition = new GridDefinition(gridExtent, cellSize, -9999, dataPoints);

            //Inverse Distance Weighted (IDW) is the interpolation model used for the GRID for assigning values to unknown points 
            //based on known neighbor points.
            //http://en.wikipedia.org/wiki/Inverse_distance_weighting
            GridInterpolationModel interpolationModel = new InverseDistanceWeightedGridInterpolationModel(2, double.MaxValue);

            var outputStream = new FileStream(filename, FileMode.Create);
            GridFeatureSource.GenerateGrid(definition, interpolationModel, outputStream);
        }

        private async Task LoadGridAsync(string filename)
        {
            //Shows how to set the class breaks to display grid cell with color according to its value.
            var gridFeatureLayer = new GridFeatureLayer(filename);
            var gridClassBreakStyle = new ClassBreakStyle("CellValue");
            const byte alpha = 150;
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, new AreaStyle(new GeoSolidBrush(GeoColors.Transparent))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(6.2, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(alpha, 255, 0, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(6.83, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(alpha, 255, 128, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.0, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(alpha, 245, 210, 10)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.08, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(alpha, 225, 255, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.15, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(alpha, 224, 251, 132)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.21, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(alpha, 128, 255, 128)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.54, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(alpha, 0, 255, 0)))));
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(gridClassBreakStyle);
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = MapView.Overlays["GridFeatureOverlay"] as LayerOverlay;
            if (layerOverlay != null && layerOverlay.Layers.Contains("GridFeatureLayer"))
            {
                layerOverlay.Layers.Remove("GridFeatureLayer");
            }

            if (layerOverlay != null)
            {
                layerOverlay.Layers.Add("GridFeatureLayer", gridFeatureLayer);

                await MapView.RefreshAsync(layerOverlay);
            }
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