using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// This samples shows how to refresh points on the map based on some outside event
    /// </summary>
    public partial class RefreshDynamicItems : UserControl, IDisposable
    {
        DispatcherTimer timer;

        public RefreshDynamicItems()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
            thinkGeoCloudVectorMapsOverlay.TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light");
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Creating a rectangle area we will use to generate the polygons and also start the map there.
            RectangleShape currentExtent = new RectangleShape(-10810995.245624, 3939081.90719325, -10747552.5124997, 3884429.43227297);

            //Do all the things we need to setup the polygon layer and overlay such as creating all the polygons etc.
            AddPolygonOverlay(AreaBaseShape.ScaleDown(currentExtent.GetBoundingBox(), 80).GetBoundingBox());

            //Set the maps current extent so we start there
            mapView.CurrentExtent = currentExtent;

            mapView.Refresh();
        }

        private void AddPolygonOverlay(RectangleShape boundingRectangle)
        {
            //We are going to store all of the polygons in an in memory layer
            InMemoryFeatureLayer polygonLayer = new InMemoryFeatureLayer();

            //Here we generate all of our make believe polygons
            Collection<Feature> features = GetGeneratedPolygons(boundingRectangle.GetBoundingBox());

            //Add all of the polygons to the layer
            foreach (var feature in features)
            {
                polygonLayer.InternalFeatures.Add(feature);
            }

            //We are going to style them based on their values we randomly added using the column DataPoint1
            ValueStyle valueStyle = new ValueStyle();
            valueStyle.ColumnName = "DataPoint1";

            //Here we add all of the different sub styles so for example "1" is going to be a red semitransparent fill with a black border etc.
            valueStyle.ValueItems.Add(new ValueItem("1", new AreaStyle(new GeoPen(GeoColors.Black, 1f), new GeoSolidBrush(new GeoColor(50, GeoColors.Red)))));
            valueStyle.ValueItems.Add(new ValueItem("2", new AreaStyle(new GeoPen(GeoColors.Black, 1f), new GeoSolidBrush(new GeoColor(50, GeoColors.Blue)))));
            valueStyle.ValueItems.Add(new ValueItem("3", new AreaStyle(new GeoPen(GeoColors.Black, 1f), new GeoSolidBrush(new GeoColor(50, GeoColors.Green)))));
            valueStyle.ValueItems.Add(new ValueItem("4", new AreaStyle(new GeoPen(GeoColors.Black, 1f), new GeoSolidBrush(new GeoColor(50, GeoColors.White)))));

            //We add the style we just created to the custom styles of the first zoom level.  Zoom level on is the highest
            // on as such you can see the whole globe.  Zoom level 20 is the lowest street level.
            polygonLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(valueStyle);

            // He we say that whatever is one zoom level 1 is applied all the way to zoom level 20.  If you only wanted to see this style at lower levels
            // you would make the above line start at ZoomLevel15 say and them apply it to 20.  That way once you zoomed out further than 15 the style would no longer apply.
            polygonLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create the overlay to house the layer and add it to the map.
            LayerOverlay polygonOverlay = new LayerOverlay();

            // Here we set the overlay to draw as a single tile.  Alternatively we could draw it as multiples tiles all threaded but single tile is a bit faster
            //We like to use multi tile for slow data sources or very complex ones that may take sime to render as you can start to see data as it comes in.
            polygonOverlay.TileType = TileType.SingleTile;

            polygonOverlay.Layers.Add("PolygonLayer", polygonLayer);
            mapView.Overlays.Add("PolygonOverlay", polygonOverlay);
        }

        private Collection<Feature> GetGeneratedPolygons(RectangleShape boundingRectangle)
        {
            //Here i just created about 20,000 rectangles around the bounding box area and generated random number from 1-4 for their data

            Random random = new Random();

            boundingRectangle.ScaleTo(10);

            Collection<Feature> features = new Collection<Feature>();

            for (int x = 1; x < 150; x++)
            {
                for (int y = 1; y < 150; y++)
                {
                    double upperLeftX = boundingRectangle.UpperLeftPoint.X + x * boundingRectangle.Width / 150;
                    double upperLeftY = boundingRectangle.UpperLeftPoint.Y - y * boundingRectangle.Height / 150;

                    double lowerRightX = upperLeftX + boundingRectangle.Width / 150;
                    double lowerRightY = upperLeftY - boundingRectangle.Height / 150;

                    Feature feature = new Feature(new RectangleShape(new PointShape(upperLeftX, upperLeftY), new PointShape(lowerRightX, lowerRightY)));
                    feature.ColumnValues.Add("DataPoint1", random.Next(1, 5).ToString());

                    features.Add(feature);
                }
            }

            return features;
        }

        private void btnStartRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (timer != null) return;
            //When you click this I start a timer that ticks every second
            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //I go to find the layer and then loop through all of the features and assign them new
            // random colors and refresh just the overlay that we are using to draw the polygons

            var polygonLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("PolygonLayer");

            Random random = new Random();

            foreach (var feature in polygonLayer.InternalFeatures)
            {
                feature.ColumnValues["DataPoint1"] = random.Next(1, 5).ToString();
            }

            // We are only going to refresh the one overlay that draws the polygons.  This saves us having toe refresh the background data.            
            mapView.Refresh(mapView.Overlays["PolygonOverlay"]);
        }

        private void btnRotate_Click(object sender, RoutedEventArgs e)
        {
            //I go to find the layer and then loop through all of the features and rotate them
            var polygonLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("PolygonLayer");

            Collection<Feature> newFeatures = new Collection<Feature>();

            PointShape center = polygonLayer.GetBoundingBox().GetCenterPoint();

            foreach (var feature in polygonLayer.InternalFeatures)
            {
                // Here we need to clone the features and add them back to the layer
                PolygonShape shape = (PolygonShape)feature.GetShape();

                shape.Rotate(center, 10);
                shape.Id = feature.Id;

                Feature newFeature = new Feature(shape);
                newFeature.ColumnValues.Add("DataPoint1", feature.ColumnValues["DataPoint1"]);
                newFeatures.Add(newFeature);
            }

            polygonLayer.InternalFeatures.Clear();

            foreach (var feature in newFeatures)
            {
                polygonLayer.InternalFeatures.Add(feature);
            }

            // We are only going to refresh the one overlay that draws the polygons.  This saves us having toe refresh the background data.
            mapView.Refresh(mapView.Overlays["PolygonOverlay"]);
        }

        private void btnOffset_Click(object sender, RoutedEventArgs e)
        {
            //I go to find the layer and then loop through all of the features and rotate them
            var polygonLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("PolygonLayer");

            Collection<Feature> newFeatures = new Collection<Feature>();

            PointShape center = polygonLayer.GetBoundingBox().GetCenterPoint();

            foreach (var feature in polygonLayer.InternalFeatures)
            {
                // Here we need to clone the features and add them back to the layer
                PolygonShape shape = (PolygonShape)feature.GetShape();

                shape.TranslateByOffset(2000, 2000);
                shape.Id = feature.Id;

                Feature newFeature = new Feature(shape);
                newFeature.ColumnValues.Add("DataPoint1", feature.ColumnValues["DataPoint1"]);
                newFeatures.Add(newFeature);
            }

            polygonLayer.InternalFeatures.Clear();

            foreach (var feature in newFeatures)
            {
                polygonLayer.InternalFeatures.Add(feature);
            }

            // We are only going to refresh the one overlay that draws the polygons.  This saves us having toe refresh the background data.
            mapView.Refresh(mapView.Overlays["PolygonOverlay"]);
        }

        private void mapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            //I go to find the layer and then loop through all of the features and rotate them
            var polygonLayer = (InMemoryFeatureLayer)mapView.FindFeatureLayer("PolygonLayer");

            var features = polygonLayer.QueryTools.GetFeaturesContaining(new PointShape(e.WorldX, e.WorldY), ReturningColumnsType.AllColumns);

            if (features.Count > 0)
            {
                MessageBox.Show($"Feature: {features[0].Id} DataPoint1: {features[0].ColumnValues["DataPoint1"]}");
            }
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
