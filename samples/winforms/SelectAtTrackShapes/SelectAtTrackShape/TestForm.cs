/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.WinForms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;

namespace SelectAtTrackShape
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.CurrentExtent = new RectangleShape(-12819569, 7708233, 13244845, -8220019);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 198, 255, 255));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            //Layer to do the spatial query on.
            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(@"..\..\Data\Countries02.shp");
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.SimpleColors.Transparent, GeoColor.FromArgb(100, GeoColor.SimpleColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add("WorldOverlay", worldOverlay);

            //Using the rectangle from TrackOverlay to do the spatial query
            winformsMap1.TrackOverlay.TrackMode = TrackMode.Rectangle;

            //Event for getting the rectangle shape at the end of tracking the rectangle on the map.
            winformsMap1.TrackOverlay.TrackEnded += new EventHandler<TrackEndedTrackInteractiveOverlayEventArgs>(winformsMap1_TrackEnded);

            //InMemoryfeatureLayer to show the selected features from the spatial query.
            InMemoryFeatureLayer spatialQueryResultLayer = new InMemoryFeatureLayer();
            spatialQueryResultLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(200, GeoColor.SimpleColors.PastelRed)));
            spatialQueryResultLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColor.StandardColors.Red;
            spatialQueryResultLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay spatialQueryResultOverlay = new LayerOverlay();
            spatialQueryResultOverlay.Layers.Add("SpatialQueryResultLayer", spatialQueryResultLayer);
            winformsMap1.Overlays.Add("SpatialQueryResultOverlay", spatialQueryResultOverlay);

            winformsMap1.Refresh();
        }

        void winformsMap1_TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            RectangleShape rectangleShape = (RectangleShape)e.TrackShape;

            ShapeFileFeatureLayer worldLayer = (ShapeFileFeatureLayer)winformsMap1.FindFeatureLayer("WorldLayer");
            InMemoryFeatureLayer spatialQueryResultLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("SpatialQueryResultLayer");

            //Spatial query to find the features intersecting the rectangle from the track rectangle.
            Collection<Feature> spatialQueryResults;
            worldLayer.Open();
            spatialQueryResults = worldLayer.QueryTools.GetFeaturesIntersecting(rectangleShape, ReturningColumnsType.NoColumns);
            worldLayer.Close();

            //Adds the selected features to the InMemoryfeatureLayer
            spatialQueryResultLayer.Open();
            spatialQueryResultLayer.EditTools.BeginTransaction();
            spatialQueryResultLayer.InternalFeatures.Clear();
            foreach (Feature feature in spatialQueryResults)
            {
                spatialQueryResultLayer.EditTools.Add(feature);
            }
            spatialQueryResultLayer.EditTools.CommitTransaction();
            spatialQueryResultLayer.Close();

            //Refreshes the layers to show new result.
            winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();
            winformsMap1.Refresh(winformsMap1.TrackOverlay);
            winformsMap1.Refresh(winformsMap1.Overlays["SpatialQueryResultOverlay"]);
        }


        private void winformsMap1_MouseMove(object sender, MouseEventArgs e)
        {
            //Displays the X and Y in screen coordinates.
            statusStrip1.Items["toolStripStatusLabelScreen"].Text = "X:" + e.X + " Y:" + e.Y;

            //Gets the PointShape in world coordinates from screen coordinates.
            PointShape pointShape = ExtentHelper.ToWorldCoordinate(winformsMap1.CurrentExtent, new ScreenPointF(e.X, e.Y), winformsMap1.Width, winformsMap1.Height);

            //Displays world coordinates.
            statusStrip1.Items["toolStripStatusLabelWorld"].Text = "(world) X:" + Math.Round(pointShape.X, 4) + " Y:" + Math.Round(pointShape.Y, 4);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
