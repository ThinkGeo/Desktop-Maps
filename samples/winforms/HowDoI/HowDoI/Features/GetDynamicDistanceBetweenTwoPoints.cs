/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public partial class GetDynamicDistanceBetweenTwoPoints : UserControl
    {
        private bool isDrawing;
        private PointShape startPoint;

        public GetDynamicDistanceBetweenTwoPoints()
        {
            InitializeComponent();
        }

        private void GetDynamicDistanceBetweenTwoPoints_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            LayerOverlay lineOverlay = new LayerOverlay();
            winformsMap1.Overlays.Add("LineOverlay", lineOverlay);

            InMemoryFeatureLayer shadowLayer = new InMemoryFeatureLayer();
            shadowLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.FromArgb(150, GeoColor.FromHtml("#F2ABB1")), 2, LineDashStyle.Dash, true);
            shadowLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            lineOverlay.Layers.Add("Shadow", shadowLayer);

            InMemoryFeatureLayer shapeLayer = new InMemoryFeatureLayer();
            shapeLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.FromHtml("#ED775B"), 10, GeoColor.FromHtml("#CA5B48"));
            shapeLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.FromHtml("#CA5B48"), 3, true);
            shapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            lineOverlay.Layers.Add("Shape", shapeLayer);

            winformsMap1.CurrentExtent = new RectangleShape(-14694173, 7361866, -7792364, 2273031);
            winformsMap1.Refresh();
        }

        private void winformsMap1_MouseClick(object sender, MouseEventArgs e)
        {
            LayerOverlay lineOverlay = (LayerOverlay)winformsMap1.Overlays["LineOverlay"];
            InMemoryFeatureLayer shapeLayer = (InMemoryFeatureLayer)lineOverlay.Layers["Shape"];
            InMemoryFeatureLayer shadowLayer = (InMemoryFeatureLayer)lineOverlay.Layers["Shadow"];

            if (isDrawing)
            {
                isDrawing = false;
            }
            else
            {
                Point screenPointStart = e.Location;
                RectangleShape extent = winformsMap1.CurrentExtent;
                ScreenPointF currentPoint = new ScreenPointF(screenPointStart.X, screenPointStart.Y);
                startPoint = ExtentHelper.ToWorldCoordinate(extent, currentPoint, winformsMap1.Width, winformsMap1.Height);
                label2.Text = "";

                shapeLayer.InternalFeatures.Clear();
                shadowLayer.InternalFeatures.Clear();
                shapeLayer.InternalFeatures.Add(new Feature(startPoint));
                isDrawing = true;
            }
        }

        private void winformsMap1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                LayerOverlay lineOverlay = (LayerOverlay)winformsMap1.Overlays["LineOverlay"];
                InMemoryFeatureLayer shapeLayer = (InMemoryFeatureLayer)lineOverlay.Layers["Shape"];
                InMemoryFeatureLayer shadowLayer = (InMemoryFeatureLayer)lineOverlay.Layers["Shadow"];

                try
                {
                    shapeLayer.InternalFeatures.Clear();
                    shadowLayer.InternalFeatures.Clear();

                    Point screenPointEnd = e.Location;
                    RectangleShape extent = winformsMap1.CurrentExtent;
                    ScreenPointF currentPoint = new ScreenPointF(screenPointEnd.X, screenPointEnd.Y);
                    PointShape endPoint = ExtentHelper.ToWorldCoordinate(extent, currentPoint, winformsMap1.Width, winformsMap1.Height);

                    shapeLayer.InternalFeatures.Add(new Feature(startPoint));
                    shapeLayer.InternalFeatures.Add(new Feature(endPoint));
                    MultilineShape line = startPoint.GetShortestLineTo(endPoint, GeographyUnit.Meter);
                    if (line != null)
                    {
                        shapeLayer.InternalFeatures.Add(new Feature(line));
                        LineShape shadow = new LineShape(new Collection<Vertex>() { new Vertex(startPoint.X, startPoint.Y), new Vertex(endPoint.X, endPoint.Y) });
                        shadowLayer.InternalFeatures.Add(new Feature(shadow));
                    }

                    label2.Text = startPoint.GetDistanceTo(endPoint, GeographyUnit.Meter, DistanceUnit.Kilometer).ToString("N2") + " km.";
                    winformsMap1.Refresh();
                }
                catch
                {
                    return;
                }
            }
        }
    }
}