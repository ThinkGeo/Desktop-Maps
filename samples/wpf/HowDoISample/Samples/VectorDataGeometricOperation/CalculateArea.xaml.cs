using System;
using System.Linq;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to calculate the area of a feature
    /// </summary>
    public partial class CalculateArea : IDisposable
    {

        private bool _initialized;
        public CalculateArea()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay. Also, add the friscoParks and selectedAreaLayer layers
        /// into a grouped LayerOverlay and display it on the map.
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

            // Create a feature layer to hold the Frisco Parks data
            // Project friscoParks layer to Spherical Mercator to match the map projection
            var friscoParksLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp")
            {
                FeatureSource =
                {
                    ProjectionConverter = new ProjectionConverter(2276, 3857)
                }
            };

            // Style friscoParks layer
            friscoParksLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
            friscoParksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var friscoParkOverlay = new LayerOverlay();
            friscoParkOverlay.Layers.Add("FriscoParksLayer", friscoParksLayer);
            Map.Overlays.Add("FriscoParksOverlay", friscoParkOverlay);

            // Style selectedAreaLayer
            var selectedAreaLayer = new InMemoryFeatureLayer();
            selectedAreaLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(64, GeoColors.Green), GeoColors.DimGray);
            selectedAreaLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var selectedAreaOverlay = new LayerOverlay();
            selectedAreaOverlay.TileType = TileType.SingleTile;
            selectedAreaOverlay.Layers.Add("SelectedAreaLayer", selectedAreaLayer);
            Map.Overlays.Add("SelectedAreaOverlay", selectedAreaOverlay);

            // Set the map extent
            Map.CenterPoint = new PointShape(-10778340, 3915490);
            Map.CurrentScale = 36110;

            _ = Map.RefreshAsync();
        }

        /// <summary>
        /// Calculates the area of a feature selected on the map and displays it in the areaResult TextBox
        /// </summary>
        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            var friscoParkOverlay = (LayerOverlay)Map.Overlays["FriscoParksOverlay"];
            var friscoParksLayer = (ShapeFileFeatureLayer)friscoParkOverlay.Layers["FriscoParksLayer"];

            var selectedAreaOverlay = (LayerOverlay)Map.Overlays["SelectedAreaOverlay"];
            var selectedAreaLayer = (InMemoryFeatureLayer)selectedAreaOverlay.Layers["SelectedAreaLayer"];

            // Query the friscoParks layer to get the first feature closest to the map click event
            var feature = friscoParksLayer.QueryTools.GetFeaturesNearestTo(e.WorldLocation, GeographyUnit.Meter, 1,
                ReturningColumnsType.NoColumns).First();

            // Show the selected feature on the map
            selectedAreaLayer.InternalFeatures.Clear();
            selectedAreaLayer.InternalFeatures.Add(feature);
            _ = selectedAreaOverlay.RefreshAsync();

            // Get the area of the first feature
            var area = ((AreaBaseShape)feature.GetShape()).GetArea(GeographyUnit.Meter, AreaUnit.SquareKilometers);

            // Display the selectedArea's area in the areaResult TextBox
            AreaResult.Text = $"{area:f3} sq km";
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
