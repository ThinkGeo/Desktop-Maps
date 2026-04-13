using System;
using System.Collections.ObjectModel;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Render points based on their values using a ValueStyle.
    /// </summary>
    public partial class RenderBasedOnValues : IDisposable
    {

        private bool _initialized;
        public RenderBasedOnValues()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, project and style the friscoCrime layer
        /// </summary>
        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // Set the map's unit of measurement to meters(Spherical Mercator)
            Map.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            Map.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            var friscoCrime = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco_Crime.shp");
            var legend = new LegendAdornmentLayer();

            // Project the layer's data to match the projection of the map
            friscoCrime.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add friscoCrimeLayer to a LayerOverlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(friscoCrime);

            // Set up the legend adornment
            legend.Title = new LegendItem()
            {
                TextStyle = new TextStyle("Crime Categories", new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black)
            };
            legend.Location = AdornmentLocation.LowerRight;
            Map.AdornmentOverlay.Layers.Add(legend);

            AddValueStyle(friscoCrime, legend);

            // Add layerOverlay to the map
            Map.Overlays.Add(layerOverlay);

            // Set the map extent
            Map.CenterPoint = new PointShape(-10778210, 3914410);
            Map.CurrentScale = 18260;

            _ = Map.RefreshAsync();
        }

        /// <summary>
        /// Adds a ValueStyle to the friscoCrime layer that represents each OffenseGroup as a different color
        /// </summary>
        private static void AddValueStyle(FeatureLayer friscoCrime, LegendAdornmentLayer legend)
        {
            // Get all the distinct OffenseGroups in the friscoCrime data
            friscoCrime.Open();
            var offenseGroups = friscoCrime.FeatureSource.GetDistinctColumnValues("OffenseGro");
            friscoCrime.Close();

            // Create a set of colors to represent each OffenseGroup using a spectrum starting from red
            var colors = GeoColor.GetColorsInQualityFamily(GeoColors.Red, offenseGroups.Count);

            // Create a ValueItem styled with a PointStyle to represent each instance of an OffenseGroup
            var valueItems = new Collection<ValueItem>();
            foreach (var offenseGroup in offenseGroups)
            {
                // Create a PointStyle to represent the OffenseGroup by selecting a color using the index of the OffenseGroup
                var style = PointStyle.CreateSimpleCircleStyle(colors[offenseGroups.IndexOf(offenseGroup)], 10,
                    GeoColors.Black, 2);

                // Create a ValueItem that will house the pointStyle for the OffenseGroup
                valueItems.Add(new ValueItem(offenseGroup.ColumnValue, style));

                // Add a LegendItem to the legend adornment
                var legendItem = new LegendItem()
                {
                    ImageStyle = style,
                    TextStyle = new TextStyle(offenseGroup.ColumnValue, new GeoFont("Verdana", 10), GeoBrushes.Black)
                };
                legend.LegendItems.Add(legendItem);
            }

            // Create the ValueStyle that will use the previously created valueItems to style the data using the OffenseGroup column values
            var valueStyle = new ValueStyle("OffenseGro", valueItems);

            // Add the valueStyle to the friscoCrime layer's CustomStyles and apply the style to all ZoomLevels
            friscoCrime.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(valueStyle);
            friscoCrime.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
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
