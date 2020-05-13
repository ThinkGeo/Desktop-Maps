using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;
using ThinkGeo.MapSuite.Routing;
using System.Collections.ObjectModel;

namespace HowDoISamples
{
    public partial class CustomRoutingSourceForm : Form
    {
        public CustomRoutingSourceForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.StandardColors.LightSteelBlue);

            ShapeFileFeatureLayer austinstreetsLayer = new ShapeFileFeatureLayer(@"..\..\App_Data\Austinstreets.shp");
            austinstreetsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(GetRoadStyle());
            austinstreetsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay austinstreetsOverlay = new LayerOverlay();
            austinstreetsOverlay.Layers.Add("AustinstreetsLayer", austinstreetsLayer);
            winformsMap1.Overlays.Add("AustinstreetsOverlay", austinstreetsOverlay);

            austinstreetsLayer.Open();
            winformsMap1.CurrentExtent = austinstreetsLayer.GetBoundingBox();
            austinstreetsLayer.Close();

            InMemoryFeatureLayer routingLayer = new InMemoryFeatureLayer();
            routingLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle.OuterPen.Color = GeoColor.StandardColors.Red;
            routingLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay routingOverlay = new LayerOverlay();
            routingOverlay.Layers.Add("RoutingLayer", routingLayer);
            winformsMap1.Overlays.Add("RoutingOverlay", routingOverlay);

            winformsMap1.Refresh();
        }

        private Style GetRoadStyle()
        {
            string expression = "CFCC.CompareTo(\"A4\")<0";

            FleeBooleanStyle roadCFCCStyle = new FleeBooleanStyle(expression);

            // You can access the static methods on these types.  We use this
            // to access the Convert.Toxxx methods to convert variable types
            roadCFCCStyle.StaticTypes.Add(typeof(System.Convert));
            // The math class might be handy to include but in this sample we do not use it
            //landLockedCountryStyle.StaticTypes.Add(typeof(System.Math));

            roadCFCCStyle.ColumnVariables.Add("CFCC");

            roadCFCCStyle.CustomTrueStyles.Add(new LineStyle(new GeoPen(GeoColor.StandardColors.Goldenrod, 2)));
            roadCFCCStyle.CustomFalseStyles.Add(new LineStyle(new GeoPen(GeoColor.StandardColors.DodgerBlue)));

            return roadCFCCStyle;
        }

        private void btnRoute_Click(object sender, EventArgs e)
        {
            Algorithm algorithm = new AStarAlgorithm();
            RoutingSource routingSource = new CustomRoutingSource(new ShapeFileFeatureSource(@"..\..\App_Data\Austinstreets.shp"));
            Collection<Road> pathIDs = algorithm.GetShortestPath(routingSource, txtStartId.Text, txtEndId.Text);

            Collection<Feature> features = new Collection<Feature>();
            Dictionary<string, ShapeFileFeatureSource> featureSources = new Dictionary<string, ShapeFileFeatureSource>();
            for (int i = 0; i < pathIDs.Count; i++)
            {
                FeatureSource featureSource = routingSource.GetFeatureSourceByroadId(pathIDs[i].Id);
                featureSource.Open();
                features.Add(featureSource.GetFeatureById(routingSource.GetFeatureIdByroadId(pathIDs[i].Id).ToString(), ReturningColumnsType.NoColumns));
                featureSource.Close();
            }

            winformsMap1.Overlays["RoutingOverlay"].Lock.EnterWriteLock();
            try
            {
                InMemoryFeatureLayer inmemoryLayer = (InMemoryFeatureLayer)((LayerOverlay)winformsMap1.Overlays["RoutingOverlay"]).Layers["RoutingLayer"];
                inmemoryLayer.InternalFeatures.Clear();

                foreach (Feature feature in features)
                {
                    inmemoryLayer.InternalFeatures.Add(feature);
                }

                inmemoryLayer.Open();
                winformsMap1.CurrentExtent = inmemoryLayer.GetBoundingBox();
                inmemoryLayer.Close();
            }
            finally
            {
                winformsMap1.Overlays["RoutingOverlay"].Lock.ExitWriteLock();
            }

            winformsMap1.Refresh();
        }
    }
}
