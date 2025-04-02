using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// This samples shows how to refresh points on the map based on some outside event
    /// </summary>
    public partial class PerfTestRefreshShapes : IDisposable
    {
        private DispatcherTimer _timer;

        public PerfTestRefreshShapes()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Creating a rectangle area we will use to generate the polygons and also start the map there.
            var currentExtent = new RectangleShape(-10810995.245624, 3939081.90719325, -10747552.5124997, 3884429.43227297);

            //Do all the things we need to set up the polygon layer and overlay such as creating all the polygons etc.
            AddPolygonOverlay(AreaBaseShape.ScaleDown(currentExtent.GetBoundingBox(), 80).GetBoundingBox());

            //Set the maps current extent so we start there
            MapView.CenterPoint = currentExtent.GetCenterPoint();
            MapView.CurrentScale = 288900;

            _ = MapView.RefreshAsync();
        }

        private void AddPolygonOverlay(BaseShape boundingRectangle)
        {
            var polygonLayer = new InMemoryFeatureLayer();
            var features = GetGeneratedPolygons(boundingRectangle.GetBoundingBox());

            //Add all the polygons to the layer
            foreach (var feature in features)
            {
                polygonLayer.InternalFeatures.Add(feature);
            }

            //Create a value-based style to render features according to the value of column DataPoint1
            var valueStyle = new ValueStyle
            {
                ColumnName = "DataPoint1"
            };

            //Here we add all the different sub styles so for example "1" is going to be a red semitransparent fill with a black border etc.
            valueStyle.ValueItems.Add(new ValueItem("1", new AreaStyle(new GeoPen(GeoColors.Black, 1f), new GeoSolidBrush(new GeoColor(50, GeoColors.Red)))));
            valueStyle.ValueItems.Add(new ValueItem("2", new AreaStyle(new GeoPen(GeoColors.Black, 1f), new GeoSolidBrush(new GeoColor(50, GeoColors.Blue)))));
            valueStyle.ValueItems.Add(new ValueItem("3", new AreaStyle(new GeoPen(GeoColors.Black, 1f), new GeoSolidBrush(new GeoColor(50, GeoColors.Green)))));
            valueStyle.ValueItems.Add(new ValueItem("4", new AreaStyle(new GeoPen(GeoColors.Black, 1f), new GeoSolidBrush(new GeoColor(50, GeoColors.White)))));

            //We add the style we just created to the custom styles of the first zoom level.  Zoom level on is the highest
            // on as such you can see the whole globe.  Zoom level 20 is the lowest street level.
            polygonLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(valueStyle);

            // We can define a style that applies from zoom level 1 all the way to level 20. However, if you only want this style to be visible at lower zoom levels
            // you can adjust the starting level. For example, by setting the style to start at zoom level 15 and apply it up to level 20, the style will no longer be applied once you zoom out beyond level 15.
            polygonLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Create the overlay to house the layer and add it to the map.
            var polygonOverlay = new LayerOverlay
            {
                // Here, we set the overlay to draw as a single tile. Alternatively, we could draw it as multiple threaded tiles, but a single tile is slightly faster.
                // We prefer using multiple tiles for slow data sources or very complex ones that take time to render. This allows you to see the data gradually as it comes in.
                TileType = TileType.SingleTile
            };

            polygonOverlay.Layers.Add("PolygonLayer", polygonLayer);
            MapView.Overlays.Add("PolygonOverlay", polygonOverlay);
        }

        private static Collection<Feature> GetGeneratedPolygons(RectangleShape boundingRectangle)
        {
            // We just created approximately 20,000 rectangles around the bounding box area and assigned them random data values between 1 and 4.
            var random = new Random();

            boundingRectangle.ScaleTo(10);

            var features = new Collection<Feature>();

            for (var x = 1; x < 150; x++)
            {
                for (var y = 1; y < 150; y++)
                {
                    var upperLeftX = boundingRectangle.UpperLeftPoint.X + x * boundingRectangle.Width / 150;
                    var upperLeftY = boundingRectangle.UpperLeftPoint.Y - y * boundingRectangle.Height / 150;

                    var lowerRightX = upperLeftX + boundingRectangle.Width / 150;
                    var lowerRightY = upperLeftY - boundingRectangle.Height / 150;

                    var feature = new Feature(new RectangleShape(new PointShape(upperLeftX, upperLeftY), new PointShape(lowerRightX, lowerRightY)));
                    feature.ColumnValues.Add("DataPoint1", random.Next(1, 5).ToString());

                    features.Add(feature);
                }
            }

            return features;
        }

        private void BtnStartRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (_timer != null) return;
            //When you click this I start a timer that ticks every second
            _timer = new DispatcherTimer
            {
                Interval = new TimeSpan(0, 0, 1)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //I go to find the layer and then loop through all the features and assign them new
            // random colors and refresh just the overlay that we are using to draw the polygons
            var polygonLayer = (InMemoryFeatureLayer)MapView.FindFeatureLayer("PolygonLayer");

            var random = new Random();

            foreach (var feature in polygonLayer.InternalFeatures)
            {
                feature.ColumnValues["DataPoint1"] = random.Next(1, 5).ToString();
            }

            // We are only going to refresh the one overlay that draws the polygons.  This saves us having toe refresh the background data.            
            _ = MapView.RefreshAsync(MapView.Overlays["PolygonOverlay"]);
        }

        private void BtnRotate_Click(object sender, RoutedEventArgs e)
        {
            //I go to find the layer and then loop through all the features and rotate them
            var polygonLayer = (InMemoryFeatureLayer)MapView.FindFeatureLayer("PolygonLayer");

            var newFeatures = new Collection<Feature>();

            var center = polygonLayer.GetBoundingBox().GetCenterPoint();

            foreach (var feature in polygonLayer.InternalFeatures)
            {
                // Here we need to clone the features and add them back to the layer
                var shape = (PolygonShape)feature.GetShape();

                shape.Rotate(center, 10);
                shape.Id = feature.Id;

                var newFeature = new Feature(shape);
                newFeature.ColumnValues.Add("DataPoint1", feature.ColumnValues["DataPoint1"]);
                newFeatures.Add(newFeature);
            }
            polygonLayer.InternalFeatures.Clear();

            foreach (var feature in newFeatures)
            {
                polygonLayer.InternalFeatures.Add(feature);
            }

            // We are only going to refresh the one overlay that draws the polygons.  This saves us having toe refresh the background data.
            _ = MapView.RefreshAsync(MapView.Overlays["PolygonOverlay"]);
        }

        private void BtnOffset_Click(object sender, RoutedEventArgs e)
        {
            // We go to find the layer and then loop through all the features and rotate them
            var polygonLayer = (InMemoryFeatureLayer)MapView.FindFeatureLayer("PolygonLayer");

            var newFeatures = new Collection<Feature>();

            foreach (var feature in polygonLayer.InternalFeatures)
            {
                // Here we need to clone the features and add them back to the layer
                var shape = (PolygonShape)feature.GetShape();

                shape.TranslateByOffset(2000, 2000);
                shape.Id = feature.Id;

                var newFeature = new Feature(shape);
                newFeature.ColumnValues.Add("DataPoint1", feature.ColumnValues["DataPoint1"]);
                newFeatures.Add(newFeature);
            }
            polygonLayer.InternalFeatures.Clear();

            foreach (var feature in newFeatures)
            {
                polygonLayer.InternalFeatures.Add(feature);
            }

            // We are only going to refresh the one overlay that draws the polygons.  This saves us having toe refresh the background data.
            _ = MapView.RefreshAsync(MapView.Overlays["PolygonOverlay"]);
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