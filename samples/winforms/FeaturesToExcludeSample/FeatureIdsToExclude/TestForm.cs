/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;


namespace FeatureIdsToExclude
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
            winformsMap1.CurrentExtent = new RectangleShape(-10777085, 4492137, -10726423, 4456740);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 198, 255, 255));

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            //InMemoryFeatureLayer for the dams.
            InMemoryFeatureLayer inMemoryFeatureLayer = new InMemoryFeatureLayer();
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(new GeoImage(@"../../data/dam_L_32x32.png"));
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(-10738101, 4464925, "Dam 21"));
            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(-10749344, 4465344, "Dam 22"));
            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(-10750123, 4468139, "Dam 23"));
            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(-10748787, 4469397, "Dam 24"));
            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(-10739548, 4473312, "Dam 26"));
            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(-10736208, 4473592, "Dam 27"));
            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(-10750457, 4473731, "Dam 31"));
            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(-10737099, 4477508, "Dam 28"));
            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(-10773871, 4478441, "Elk City Lake"));
            inMemoryFeatureLayer.InternalFeatures.Add(new Feature(-10741552, 4481985, "Dam 32"));

            //InMemoryFeatureLayer for the searching point.
            InMemoryFeatureLayer inMemoryFeatureLayer2 = new InMemoryFeatureLayer();
            inMemoryFeatureLayer2.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimplePointStyle(PointSymbolType.Circle, GeoColor.SimpleColors.Red, 12);
            inMemoryFeatureLayer2.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            inMemoryFeatureLayer2.InternalFeatures.Add(new Feature(-10748632, 4477046));

            //InMemoryFeatureLayer for the selected dams.
            InMemoryFeatureLayer highlightInMemoryFeatureLayer = new InMemoryFeatureLayer();
            highlightInMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimplePointStyle(PointSymbolType.Circle, GeoColor.FromArgb(100, GeoColor.SimpleColors.Red), 14);
            highlightInMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(inMemoryFeatureLayer);
            layerOverlay.Layers.Add(inMemoryFeatureLayer2);
            layerOverlay.Layers.Add(highlightInMemoryFeatureLayer);

            winformsMap1.Overlays.Add(layerOverlay);

            winformsMap1.Refresh();
        }

        //Finds the five nearest dams using FeatureIdsToExclude.
        private void radioButtonWith_CheckedChanged(object sender, EventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)winformsMap1.Overlays[1];
            InMemoryFeatureLayer inMemoryFeatureLayer = (InMemoryFeatureLayer)layerOverlay.Layers[0];

            //Adds the features to exlude using the feature id.
            //Will exclude the dam 24 and dam 31 from the search.
            inMemoryFeatureLayer.FeatureIdsToExclude.Add("Dam 24");
            inMemoryFeatureLayer.FeatureIdsToExclude.Add("Dam 31");

            InMemoryFeatureLayer inMemoryFeatureLayer2 = (InMemoryFeatureLayer)layerOverlay.Layers[1];
            PointShape pointShape = (PointShape)inMemoryFeatureLayer2.InternalFeatures[0].GetShape();

            inMemoryFeatureLayer.Open();
            int numberToFind = 5;
            Collection<Feature> nearestFeatures = inMemoryFeatureLayer.QueryTools.GetFeaturesNearestTo(pointShape, GeographyUnit.Meter, numberToFind, ReturningColumnsType.NoColumns);

            //We may have to adjust numberOfItemsToFind parameter to find the nearest 5 features.
            if (nearestFeatures.Count < numberToFind)
            {
                numberToFind = numberToFind + (numberToFind - nearestFeatures.Count);
                nearestFeatures = inMemoryFeatureLayer.QueryTools.GetFeaturesNearestTo(pointShape, GeographyUnit.Meter, numberToFind, ReturningColumnsType.NoColumns);
            }

            //Before refreshing the map, we clear the FeatureIdsToExclude so that all the features of the InMemoryFeatureLayer will be drawn.
            inMemoryFeatureLayer.FeatureIdsToExclude.Clear();
            inMemoryFeatureLayer.Close();

            InMemoryFeatureLayer highllightInMemoryFeatureLayer = (InMemoryFeatureLayer)layerOverlay.Layers[2];
            highllightInMemoryFeatureLayer.InternalFeatures.Clear();

            //Adds the result features to the InMemoryFeatureLayer for displaying.
            foreach (Feature feature in nearestFeatures)
            {
                highllightInMemoryFeatureLayer.InternalFeatures.Add(feature);
            }

            winformsMap1.Refresh(layerOverlay);

        }

        private void radioButtonWithout_CheckedChanged(object sender, EventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)winformsMap1.Overlays[1];
            InMemoryFeatureLayer inMemoryFeatureLayer = (InMemoryFeatureLayer)layerOverlay.Layers[0];
            inMemoryFeatureLayer.FeatureIdsToExclude.Clear();

            InMemoryFeatureLayer inMemoryFeatureLayer2 = (InMemoryFeatureLayer)layerOverlay.Layers[1];
            PointShape pointShape = (PointShape)inMemoryFeatureLayer2.InternalFeatures[0].GetShape();

            inMemoryFeatureLayer.Open();
            int numberToFind = 5;
            Collection<Feature> nearestFeatures = inMemoryFeatureLayer.QueryTools.GetFeaturesNearestTo(pointShape, GeographyUnit.Meter, numberToFind, ReturningColumnsType.NoColumns);
            inMemoryFeatureLayer.Close();

            InMemoryFeatureLayer highllightFeatureLayer = (InMemoryFeatureLayer)layerOverlay.Layers[2];
            highllightFeatureLayer.InternalFeatures.Clear();

            foreach (Feature feature in nearestFeatures)
            {
                highllightFeatureLayer.InternalFeatures.Add(feature);
            }

            winformsMap1.Refresh(layerOverlay);
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
