using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to selectively style features based on numerical data using a ClassBreakStyle
    /// </summary>
    public partial class CreateClassBreakStyleSample : UserControl, IDisposable
    {
        public CreateClassBreakStyleSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay. Also, project and style the Frisco 2010 Census Housing Units layer
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            ShapeFileFeatureLayer housingUnitsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco 2010 Census Housing Units.shp");
            LegendAdornmentLayer legend = new LegendAdornmentLayer();

            // Setup the legend adornment
            legend.Title = new LegendItem()
            {
                TextStyle = new TextStyle("Housing Units", new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black)
            };
            legend.Location = AdornmentLocation.LowerRight;
            mapView.AdornmentOverlay.Layers.Add(legend);

            // Project the layer's data to match the projection of the map
            housingUnitsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            AddClassBreakStyle(housingUnitsLayer, legend);

            // Add housingUnitsLayer to a LayerOverlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(housingUnitsLayer);

            // Add layerOverlay to the mapView
            mapView.Overlays.Add(layerOverlay);

            // Set the map extent
            housingUnitsLayer.Open();
            mapView.CurrentExtent = housingUnitsLayer.GetBoundingBox();
            housingUnitsLayer.Close();

            mapView.Refresh();
        }

        /// <summary>
        /// Adds a ClassBreakStyle to the housingUnitsLayer that changes colors based on the numerical value of the H_UNITS column as they fall within the range of a ClassBreak
        /// </summary>
        private void AddClassBreakStyle(ShapeFileFeatureLayer layer, LegendAdornmentLayer legend)
        {
            // Create the ClassBreakStyle based on the H_UNITS numerical column
            var housingUnitsStyle = new ClassBreakStyle("H_UNITS");

            var classBreakIntervals = new double[] { 0, 1000, 2000, 3000, 4000, 5000 };
            var colors = GeoColor.GetColorsInHueFamily(GeoColors.Red, classBreakIntervals.Count()).Reverse().ToList();

            // Create ClassBreaks for each of the classBreakIntervals
            for (int i = 0; i < classBreakIntervals.Count(); i++)
            {
                // Create the classBreak using one of the intervals and colors defined above
                var classBreak = new ClassBreak(classBreakIntervals[i], AreaStyle.CreateSimpleAreaStyle(new GeoColor(192, colors[i]), GeoColors.White));

                // Add the classBreak to the housingUnitsStyle ClassBreaks collection
                housingUnitsStyle.ClassBreaks.Add(classBreak);

                // Add a LegendItem to the legend adornment to represent the classBreak
                var legendItem = new LegendItem()
                {
                    ImageStyle = classBreak.DefaultAreaStyle,
                    TextStyle = new TextStyle($@">{classBreak.Value} units", new GeoFont("Verdana", 10), GeoBrushes.Black)
                };
                legend.LegendItems.Add(legendItem);
            }

            // Add and apply the ClassBreakStyle to the housingUnitsLayer
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(housingUnitsStyle);
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        }
        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
