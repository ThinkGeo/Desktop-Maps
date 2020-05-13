using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace GetFeaturesFromArcGISServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string serverUri = "http://sampleserver1.arcgisonline.com/ArcGIS/rest/services/Specialty/ESRI_StateCityHighway_USA/MapServer";
        private static List<Feature> cachedFeatures = new List<Feature>();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitailizeWpfMap2();
            InitailizeWpfMap1();
        }

        private void InitailizeWpfMap1()
        {
            wpfMap1.MapUnit = GeographyUnit.DecimalDegree;

            // Uses the WorldMapKit as the background layer.
            WorldStreetsAndImageryRasterLayer worldMapKitLayer = new WorldStreetsAndImageryRasterLayer();
            worldMapKitLayer.Projection = ThinkGeo.MapSuite.Layers.WorldStreetsAndImageryProjection.DecimalDegrees;

            // Creates a new ArcGISServerRestLayer.  This will be used to show counties based on population.
            // documentation for ArcGIS Server REST API can be found at:  http://resources.arcgis.com/en/help/arcgis-rest-api/ , V 9.3 : http://resources.esri.com/help/9.3/arcgisserver/apis/rest/index-9-3.html
            ArcGISServerRestLayer arcGisLayer = new ArcGISServerRestLayer();

            // Specifies the uri of the web service.
            arcGisLayer.ServerUri = new Uri($"{serverUri}/export");

            // Sets our parameters for image format.
            arcGisLayer.Parameters.Add("format", "png");
            //arcGisLayer.ImageFormat = ArcGISServerRestLayerImageFormat.Png; // we can use the property 'ImageFormat' instead of the 'format' in parameters. When they coexist, the property is perferred.

            // Sets our parameters for transparency.
            arcGisLayer.Parameters.Add("transparent", "true");

            // Specifies the layerId(s) you wish to display.  LayerId 2 is the county layer.
            arcGisLayer.Parameters.Add("layers", "show:2");

            // Specifies the layer and field you wish to query(layerid: field).  in this example, the layerid is 2 and the data field to query is POP1999.
            arcGisLayer.Parameters.Add("layerdefs", "2:POP1999>50000");

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(worldMapKitLayer);
            layerOverlay.Layers.Add("ArcGISServerRestLayer", arcGisLayer);

            wpfMap1.Overlays.Add("LayerOverlay", layerOverlay);

            wpfMap1.CurrentExtent = new RectangleShape(-125.4, 43.8, -118.7, 35.2);
        }

        private void InitailizeWpfMap2()
        {
            wpfMap2.MapUnit = GeographyUnit.DecimalDegree;

            WorldStreetsAndImageryRasterLayer worldMapKitLayer = new WorldStreetsAndImageryRasterLayer();
            worldMapKitLayer.Projection = ThinkGeo.MapSuite.Layers.WorldStreetsAndImageryProjection.DecimalDegrees;

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(worldMapKitLayer);

            InMemoryFeatureLayer inMemoryArcGISServerRestLayer = new InMemoryFeatureLayer();
            inMemoryArcGISServerRestLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country1;
            inMemoryArcGISServerRestLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layerOverlay.Layers.Add("InMemoryArcGISServerRestLayer", inMemoryArcGISServerRestLayer);

            wpfMap2.Overlays.Add("LayerOverlay", layerOverlay);

            wpfMap2.CurrentExtent = new RectangleShape(-125.4, 43.8, -118.7, 35.2);
        }

        private void wpfMap1_CurrentExtentChanged(object sender, CurrentExtentChangedWpfMapEventArgs e)
        {
            wpfMap2.CurrentExtent = e.CurrentExtent;

            var featureIds = QueryFeatureIds(e.CurrentExtent);
            var features = GetFeatures(featureIds);

            var inMemoryFeatureLayer = ((LayerOverlay)wpfMap2.Overlays["LayerOverlay"]).Layers["InMemoryArcGISServerRestLayer"] as InMemoryFeatureLayer;
            inMemoryFeatureLayer.InternalFeatures.Clear();
            foreach (var feature in features)
            {
                inMemoryFeatureLayer.InternalFeatures.Add(feature);
            }
            wpfMap2.Refresh();
        }

        private Collection<Feature> GetFeatures(IEnumerable<string> featureIds)
        {
            var features = new Collection<Feature>();
            var tasks = new List<Task>();
            foreach (var id in featureIds)
            {
                var cachedFeature = cachedFeatures.FirstOrDefault(p => p.Id == id);
                if (cachedFeature != null)
                    features.Add(cachedFeature);
                else
                {
                    var task = Task.Factory.StartNew((featureId) =>
                    {
                        WebRequest webRquest = (HttpWebRequest)WebRequest.Create($"{serverUri}/2/{featureId}?f=json");

                        WebResponse response = webRquest.GetResponse();
                        Stream stream = response.GetResponseStream();
                        string output = new StreamReader(stream).ReadToEnd();

                        var json = JObject.Parse(output);
                        var rings = json["feature"]["geometry"]["rings"];
                        foreach (var ring in rings)
                        {
                            var vertices = ring.Select(point => new Vertex(Convert.ToDouble(point[0]), Convert.ToDouble(point[1])));
                            var outerRing = new RingShape(vertices);
                            var polygon = new PolygonShape(outerRing) { Id = json["feature"]["OBJECTID"]?.ToString() };
                            var feature = new Feature(polygon);
                            features.Add(feature);
                            cachedFeatures.Add(feature);
                        }
                    }, id);
                    tasks.Add(task);
                }
            }
            Task.WaitAll(tasks.ToArray());
            return features;
        }
        private string[] QueryFeatureIds(RectangleShape bbox)
        {
            var query = new StringBuilder();
            var param = BuildQueryFeatureIdsParams(bbox);
            foreach (var key in param.AllKeys)
            {
                query.AppendFormat("{0}={1}&", key, param[key]);
            }
            WebRequest webRquest = (HttpWebRequest)WebRequest.Create($"{serverUri}/2/query?{query.ToString()}");

            WebResponse response = webRquest.GetResponse();

            Stream stream = response.GetResponseStream();

            string output = new StreamReader(stream).ReadToEnd();

            var json = JObject.Parse(output);

            return json["objectIds"].Select(p => p.ToString()).ToArray();
        }


        private static NameValueCollection BuildQueryFeatureIdsParams(RectangleShape bbox)
        {
            var query = new NameValueCollection();
            query.Add("text", string.Empty);
            query.Add("geometry", $"{bbox.LowerLeftPoint.X},{bbox.LowerLeftPoint.Y},{bbox.UpperRightPoint.X},{bbox.UpperRightPoint.Y}");
            query.Add("geometryType", "esriGeometryEnvelope");
            query.Add("inSR", "");
            query.Add("spatialRel", "esriSpatialRelIntersects");
            query.Add("relationParam", "");
            query.Add("objectIds", "");
            query.Add("where", "POP1999>50000");
            query.Add("returnCountOnly", "false");
            query.Add("returnIdsOnly", "true");
            query.Add("returnGeometry", "true");
            query.Add("maxAllowableOffset", "");
            query.Add("outSR", "");
            query.Add("outFields", "");
            query.Add("f", "json");
            return query;
        }

    }
}
