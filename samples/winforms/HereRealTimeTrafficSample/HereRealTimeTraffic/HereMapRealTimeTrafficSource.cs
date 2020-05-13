using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.Layers
{
    public class HereMapRealTimeTrafficSource : FeatureSource
    {
        private Uri serverUri;
        private string appId;
        private string appCode;

        public HereMapRealTimeTrafficSource()
            : this(null, string.Empty, string.Empty)
        { }

        public HereMapRealTimeTrafficSource(Uri serverUri, string appId, string appCode)
        {
            this.serverUri = serverUri;
            this.appId = appId;
            this.appCode = appCode;
        }

        public Uri ServerUri
        {
            get { return serverUri; }
            set { serverUri = value; }
        }

        public string AppId
        {
            get { return appId; }
            set { appId = value; }
        }

        public string AppCode
        {
            get { return appCode; }
            set { appCode = value; }
        }

        protected override Collection<Feature> GetAllFeaturesCore(IEnumerable<string> returningColumnNames)
        {
            RectangleShape boundingBox = new RectangleShape(-180, 90, 180, -90);
            return GetFeaturesInsideBoundingBox(boundingBox, returningColumnNames);
        }

        protected override Collection<Feature> GetFeaturesForDrawingCore(RectangleShape boundingBox, double screenWidth, double screenHeight, IEnumerable<string> returningColumnNames)
        {
            return GetFeaturesInsideBoundingBox(boundingBox, returningColumnNames);
        }

        private Collection<Feature> GetFeaturesInsideBoundingBox(RectangleShape boundingBox, IEnumerable<string> returningColumnNames)
        {
            Collection<Feature> features = new Collection<Feature>();

            if (serverUri != null && !string.IsNullOrEmpty(appId) && !string.IsNullOrEmpty(appCode))
            {
                var requestUrl = serverUri.AbsoluteUri + $"?app_id={appId}&app_code={appCode}&bbox={boundingBox.UpperLeftPoint.Y},{boundingBox.UpperLeftPoint.X};{boundingBox.LowerRightPoint.Y},{boundingBox.LowerRightPoint.X}&responseattributes=sh,fc";

                var request = WebRequest.Create(new Uri(requestUrl));
                var response = request.GetResponse();

                using (var stream = response.GetResponseStream())
                {
                    var reader = new StreamReader(stream);
                    var content = reader.ReadToEnd();
                    var jsonArray = (JObject)JsonConvert.DeserializeObject(content);
                    var rwsElement = jsonArray["RWS"];
                    foreach (var rwElement in rwsElement)
                    {
                        var rw = rwElement["RW"];
                        foreach (var rwFis in rw)
                        {
                            var fis = rwFis["FIS"];
                            foreach (var fiFis in fis)
                            {
                                var fi = fiFis["FI"];
                                foreach (var fiElement in fi)
                                {
                                    var shapeElement = fiElement["SHP"];
                                    var tmcElement = fiElement["TMC"];

                                    var cfElement = fiElement["CF"];
                                    var cfDictionary = new Dictionary<string, string>();
                                    foreach (var cf in cfElement)
                                    {
                                        var ff = cf["FF"].ToObject<double>();
                                        var sp = cf["SP"].ToObject<double>();
                                        var cn = cf["CN"].ToObject<double>();
                                        var jf = cf["JF"].ToObject<double>();
                                        cfDictionary["TY"] = cf["TY"].ToObject<string>();
                                        cfDictionary["SP"] = cf["SP"].ToObject<double>().ToString();
                                        if (cf["SU"] != null)
                                        {
                                            cfDictionary["SU"] = cf["SU"].ToObject<double>().ToString();
                                        }
                                        cfDictionary["FF"] = cf["FF"].ToObject<double>().ToString();
                                        cfDictionary["JF"] = cf["JF"].ToObject<double>().ToString();
                                        cfDictionary["CN"] = cf["CN"].ToObject<double>().ToString();
                                    }

                                    foreach (var shp in shapeElement)
                                    {
                                        var shpValue = shp["value"];

                                        foreach (var shape in shpValue)
                                        {
                                            var shapeContent = shape.ToObject<string>().Trim();
                                            var lineShapes = shapeContent.Split(' ');
                                            var lineShape = new LineShape();
                                            foreach (var line in lineShapes)
                                            {
                                                var latLong = line.Split(',');
                                                var latValue = double.Parse(latLong[0]);
                                                var longValue = double.Parse(latLong[1]);
                                                lineShape.Vertices.Add(new Vertex(longValue, latValue));
                                            }

                                            var feature = new Feature(lineShape);
                                            feature.ColumnValues.Add("PC", tmcElement["PC"].ToObject<int>().ToString());
                                            feature.ColumnValues.Add("DE", tmcElement["DE"].ToObject<string>());
                                            feature.ColumnValues.Add("QD", tmcElement["QD"].ToObject<string>());
                                            feature.ColumnValues.Add("LE", tmcElement["LE"].ToObject<double>().ToString());

                                            foreach (var cfPair in cfDictionary)
                                            {
                                                feature.ColumnValues.Add(cfPair.Key, cfPair.Value);
                                            }

                                            features.Add(feature);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return features;
        }
    }
}
