using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to calculate the area of a feature
    /// </summary>
    public partial class CalculateAreaSample : UserControl, IDisposable
    {
        public CalculateAreaSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay. Also, add the friscoParks and selectedAreaLayer layers
        /// into a grouped LayerOverlay and display it on the map.
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a feature layer to hold the Frisco Parks data
            ShapeFileFeatureLayer friscoParksLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Parks.shp");            
            
            // Project friscoParks layer to Spherical Mercator to match the map projection
            friscoParksLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style friscoParks layer
            friscoParksLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
            friscoParksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay friscoParkOverlay = new LayerOverlay();
            friscoParkOverlay.Layers.Add("FriscoParksLayer", friscoParksLayer);
            mapView.Overlays.Add("FriscoParksOverlay", friscoParkOverlay);

            // Style selectedAreaLayer
            InMemoryFeatureLayer selectedAreaLayer = new InMemoryFeatureLayer();
            selectedAreaLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(64, GeoColors.Green), GeoColors.DimGray);
            selectedAreaLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay selectedAreaOverlay = new LayerOverlay();
            selectedAreaOverlay.Layers.Add("SelectedAreaLayer", selectedAreaLayer);
            mapView.Overlays.Add("SelectedAreaOverlay", selectedAreaOverlay);
                      
            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10782307.6877106, 3918904.87378907, -10774377.3460701, 3912073.31442403);
                      
            await mapView.RefreshAsync();
        }

        /// <summary>
        /// Calculates the area of a feature selected on the map and displays it in the areaResult TextBox
        /// </summary>
        private async void MapView_OnMapClick(object sender, MapClickMapViewEventArgs e)
        {           
            LayerOverlay friscoParkOverlay = (LayerOverlay)mapView.Overlays["FriscoParksOverlay"];
            ShapeFileFeatureLayer friscoParksLayer = (ShapeFileFeatureLayer)friscoParkOverlay.Layers["FriscoParksLayer"];

            LayerOverlay selectedAreaOverlay = (LayerOverlay)mapView.Overlays["SelectedAreaOverlay"];
            InMemoryFeatureLayer selectedAreaLayer = (InMemoryFeatureLayer)selectedAreaOverlay.Layers["SelectedAreaLayer"];

            // Query the friscoParks layer to get the first feature closest to the map click event
            var feature = friscoParksLayer.QueryTools.GetFeaturesNearestTo(e.WorldLocation, GeographyUnit.Meter, 1,
                ReturningColumnsType.NoColumns).First();

            // Show the selected feature on the map
            selectedAreaLayer.InternalFeatures.Clear();
            selectedAreaLayer.InternalFeatures.Add(feature);
            await selectedAreaOverlay.RefreshAsync();

            // Get the area of the first feature
            var area = ((AreaBaseShape)feature.GetShape()).GetArea(GeographyUnit.Meter, AreaUnit.SquareKilometers);

            // Display the selectedArea's area in the areaResult TextBox
            areaResult.Text = $"{area:f3} sq km";
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
