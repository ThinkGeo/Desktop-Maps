using System;
using System.Linq;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to selectively style features based on numerical data using a ClassBreakStyle
    /// </summary>
    public partial class CreateClassBreakStyleSample : IDisposable
    {
        public CreateClassBreakStyleSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, project and style the Frisco 2010 Census Housing Units layer
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            var housingUnitsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco 2010 Census Housing Units.shp");
            var legend = new LegendAdornmentLayer
            {
                // Set up the legend adornment
                Title = new LegendItem()
                {
                    TextStyle = new TextStyle("Housing Units", new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black)
                },
                Location = AdornmentLocation.LowerRight
            };

            MapView.AdornmentOverlay.Layers.Add(legend);

            // Project the layer's data to match the projection of the map
            housingUnitsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            AddClassBreakStyle(housingUnitsLayer, legend);

            // Add housingUnitsLayer to a LayerOverlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(housingUnitsLayer);

            // Add layerOverlay to the mapView
            MapView.Overlays.Add(layerOverlay);

            // Set the map extent
            housingUnitsLayer.Open();
            MapView.CurrentExtent = housingUnitsLayer.GetBoundingBox();
            housingUnitsLayer.Close();

            await MapView.RefreshAsync();
        }

        /// <summary>
        /// Adds a ClassBreakStyle to the housingUnitsLayer that changes colors based on the numerical value of the H_UNITS column as they fall within the range of a ClassBreak
        /// </summary>
        private static void AddClassBreakStyle(FeatureLayer layer, LegendAdornmentLayer legend)
        {
            // Create the ClassBreakStyle based on the H_UNITS numerical column
            var housingUnitsStyle = new ClassBreakStyle("H_UNITS");

            var classBreakIntervals = new double[] { 0, 1000, 2000, 3000, 4000, 5000 };
            var colors = GeoColor.GetColorsInHueFamily(GeoColors.Red, classBreakIntervals.Length).Reverse().ToList();

            // Create ClassBreaks for each of the classBreakIntervals
            for (var i = 0; i < classBreakIntervals.Length; i++)
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
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}