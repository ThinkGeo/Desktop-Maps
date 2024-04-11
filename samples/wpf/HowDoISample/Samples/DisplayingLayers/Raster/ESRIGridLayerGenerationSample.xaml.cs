using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to generate and display an ESRI Grid Layer on the map
    /// </summary>
    public partial class ESRIGridLayerGenerationSample : IDisposable
    {
        public ESRIGridLayerGenerationSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the ESRI Grid layer to the map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;
            MapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.Snow);

            // Create background hybrid satellite map requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudRasterMapsOverlay cloudOverlay = new ThinkGeoCloudRasterMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~");
            cloudOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Hybrid_V2_X1;
            MapView.Overlays.Add("Cloud Overlay", cloudOverlay);


            //Applies class break style to show sample points of pH level of a field.
            ClassBreakStyle classBreakStyle = new ClassBreakStyle("PH");
            byte Alpha = 180;
            int symbolSize = 10;
            classBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColors.Transparent))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(6.2, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(Alpha, 255, 0, 0)))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(6.83, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(Alpha, 255, 128, 0)))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(7.0, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(Alpha, 245, 210, 10)))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(7.08, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(Alpha, 225, 255, 0)))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(7.15, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(Alpha, 224, 251, 132)))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(7.21, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(Alpha, 128, 255, 128)))));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(7.54, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(Alpha, 0, 255, 0)))));

            //load the point shapefile containing ph values in different points of a field.
            ShapeFileFeatureLayer samplesLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/sample_ph_2.shp");
            samplesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(classBreakStyle);
            samplesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            //Create an overlay for our points (and later the grid) and add our points layer to it.
            LayerOverlay gridOverlay = new LayerOverlay();
            gridOverlay.TileType = TileType.SingleTile;
            gridOverlay.Layers.Add("GridFeatureLayer", samplesLayer);
            MapView.Overlays.Add("GridFeatureOverlay", gridOverlay);

            //set the map's current extent to the point shapefile location.
            samplesLayer.Open();
            MapView.CurrentExtent = samplesLayer.GetBoundingBox();
            samplesLayer.Close();

            await MapView.RefreshAsync();
        }

        private async void btnGenerateGridFile_Click(object sender, RoutedEventArgs e)
        {
            // call the functions to generate the grid file and render it.
            string filename = @"./Data/GridFile/generated.grd";
            GenerateGrid(filename);
            await LoadGridAsync(filename);
        }

        private void GenerateGrid(string filename)
        {
            //Point based shapefile used to create the GRID (sample points of pH level of a field)
            ShapeFileFeatureLayer pointLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/sample_ph_2.shp");

            //Sets the extent of the grid based on the extent of the sample points shapefile and slightly larger.
            pointLayer.Open();
            RectangleShape gridExtent = pointLayer.GetBoundingBox();
            gridExtent.ScaleUp(5);

            //Gets the data (points with their pH value) to build the GRID.
            Collection<Feature> features = pointLayer.FeatureSource.GetAllFeatures(new string[] { "PH" });
            Dictionary<PointShape, double> dataPoints = new Dictionary<PointShape, double>();
            pointLayer.Close();

            foreach (Feature feature in features)
            {
                PointShape pointShape = (PointShape)feature.GetShape();
                double value = double.Parse(feature.ColumnValues["PH"]);
                dataPoints.Add(pointShape, value);
            }

            //Cell size based on the width of the extent divided by 100.
            double cellSize = gridExtent.Width / 100;
            //Sets the definition of the GRID with its extent, the cell size, the non value, and the data (point locations with their value)
            //For more information on GRID definition see http://en.wikipedia.org/wiki/ASCII_GRID
            GridDefinition definition = new GridDefinition(gridExtent, cellSize, -9999, dataPoints);

            //Inverse Distance Weighted (IDW) is the interpolation model used for the GRID for assigning values to unknown points 
            //based on known neighbor points.
            //http://en.wikipedia.org/wiki/Inverse_distance_weighting
            GridInterpolationModel interpolationModel = new InverseDistanceWeightedGridInterpolationModel(2, double.MaxValue);

            FileStream outputStream = new FileStream(filename, FileMode.Create);
            GridFeatureSource.GenerateGrid(definition, interpolationModel, outputStream);
        }

        private async Task LoadGridAsync(string filename)
        {
            //Shows how to set the class breaks to display grid cell with color according to its value.
            GridFeatureLayer gridFeatureLayer = new GridFeatureLayer(filename);
            ClassBreakStyle gridClassBreakStyle = new ClassBreakStyle("CellValue");
            byte Alpha = 150;
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, new AreaStyle(new GeoSolidBrush(GeoColors.Transparent))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(6.2, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(Alpha, 255, 0, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(6.83, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(Alpha, 255, 128, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.0, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(Alpha, 245, 210, 10)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.08, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(Alpha, 225, 255, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.15, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(Alpha, 224, 251, 132)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.21, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(Alpha, 128, 255, 128)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(7.54, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(Alpha, 0, 255, 0)))));
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(gridClassBreakStyle);
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay layerOverlay = MapView.Overlays["GridFeatureOverlay"] as LayerOverlay;
            if (layerOverlay.Layers.Contains("GridFeatureLayer"))
            {
                layerOverlay.Layers.Remove("GridFeatureLayer");
            }
            layerOverlay.Layers.Add("GridFeatureLayer", gridFeatureLayer);

            await MapView.RefreshAsync(layerOverlay);
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