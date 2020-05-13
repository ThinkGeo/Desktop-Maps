using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

public partial class Sample : Form
{
    Collection<Feature> editFeatures = new Collection<Feature>();
    Collection<Feature> addFeatures = new Collection<Feature>();
    Collection<Feature> deleteFeatures = new Collection<Feature>();

    //original tab files.
    private const string fileName = @"..\..\Data\USA.TAB";

    public Sample()
    {
        InitializeComponent();
    }

    private void Sample_Load(object sender, EventArgs e)
    {
        winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
        winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.DeepOcean);

        //load the tab file and set the styles.
        TabFeatureLayer tabFeatureLayer = new TabFeatureLayer(fileName, GeoFileReadWriteMode.ReadWrite);
        tabFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.StandardColors.White, GeoColor.FromArgb(100, GeoColor.SimpleColors.Green));
        tabFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        tabFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.City1;
        tabFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.Canal1;
        tabFeatureLayer.RequireIndex = false;

        //create an InMemoryFeatureLayer to hold the selected feature.  Style the layer so it appears to be highlighted.
        InMemoryFeatureLayer highLightFeatureLayer = new InMemoryFeatureLayer();
        highLightFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.SimpleColors.PaleGreen, GeoColor.FromArgb(100, GeoColor.SimpleColors.Green));
        highLightFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
        highLightFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.City3;
        highLightFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.Interstate3;

        //Create our overlays, add our layers to the overlays, and add the overlays to the map.
        LayerOverlay staticOverlay = new LayerOverlay();
        staticOverlay.Layers.Add("TabFeatureLayer", tabFeatureLayer);

        LayerOverlay highLightOverlay = new LayerOverlay();
        highLightOverlay.Layers.Add("HighLightFeatureLayer", highLightFeatureLayer);

        winformsMap1.Overlays.Add("StaticOverlay", staticOverlay);
        winformsMap1.Overlays.Add("HighLightOverlay", highLightOverlay);

        //zoom the map to the extent of the TabFeatureLayer.
        tabFeatureLayer.Open();
        winformsMap1.CurrentExtent = tabFeatureLayer.GetBoundingBox();
        tabFeatureLayer.Close();

        winformsMap1.Refresh();
    }

    private void BtnClick_Handle(object sender, EventArgs e)
    {
        Button button = sender as Button;

        if (null != button)
        {
            switch (button.Name)
            {
                case "Mouse_Btn":
                    winformsMap1.TrackOverlay.TrackMode = TrackMode.None;
                    break;
                case "Point_Btn":
                    winformsMap1.TrackOverlay.TrackMode = TrackMode.Point;
                    break;
                case "Line_Btn":
                    winformsMap1.TrackOverlay.TrackMode = TrackMode.Line;
                    break;
                case "Rectangle_Btn":
                    winformsMap1.TrackOverlay.TrackMode = TrackMode.Rectangle;
                    break;
                case "Square_Btn":
                    winformsMap1.TrackOverlay.TrackMode = TrackMode.Square;
                    break;
                case "Polygon_Btn":
                    winformsMap1.TrackOverlay.TrackMode = TrackMode.Polygon;
                    break;
                case "Circle_Btn":
                    winformsMap1.TrackOverlay.TrackMode = TrackMode.Circle;
                    break;
                case "Ellipse_Btn":
                    winformsMap1.TrackOverlay.TrackMode = TrackMode.Ellipse;
                    break;
                case "Edit_Btn":
                    {
                        //add the selected feature into the EditOverlay and clear it from the highlightLayer.
                        InMemoryFeatureLayer highlightLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("HighLightFeatureLayer");
                        FeatureLayer tabFeatureLayer = winformsMap1.FindFeatureLayer("TabFeatureLayer");

                        foreach (Feature feature in highlightLayer.InternalFeatures)
                        {
                            winformsMap1.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature);
                            tabFeatureLayer.FeatureIdsToExclude.Add(feature.Id);
                        }
                        winformsMap1.EditOverlay.CalculateAllControlPoints();
                        highlightLayer.InternalFeatures.Clear();
                        winformsMap1.Refresh();
                    }
                    break;
                case "Delete_Btn":
                    {
                        //delete the selected features from the TabFileLayer.  
                        InMemoryFeatureLayer highlightLayer = winformsMap1.FindFeatureLayer("HighLightFeatureLayer") as InMemoryFeatureLayer;
                        highlightLayer.Open();
                        Collection<Feature> deleteFeatures = highlightLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.NoColumns);

                        FeatureLayer tabFeatureLayer = winformsMap1.FindFeatureLayer("TabFeatureLayer");
                        tabFeatureLayer.Open();
                        tabFeatureLayer.EditTools.BeginTransaction();
                        if (deleteFeatures.Count > 0)
                        {
                            foreach (Feature deleteFeature in deleteFeatures)
                            {
                                tabFeatureLayer.EditTools.Delete(deleteFeature.Id);
                            }
                        }

                        tabFeatureLayer.EditTools.CommitTransaction();
                        highlightLayer.InternalFeatures.Clear();
                        tabFeatureLayer.Close();
                        highlightLayer.Close();

                        winformsMap1.Refresh();
                    }
                    break;
                default:
                    winformsMap1.TrackOverlay.TrackMode = TrackMode.None;
                    break;
            }
        }
    }

    private void Save_Btn_Click(object sender, EventArgs e)
    {
        //query the EditOverlay for edited features and query the TrackOverlay for new features.
        Collection<Feature> editFeatures = winformsMap1.EditOverlay.EditShapesLayer.InternalFeatures;
        Collection<Feature> newFeatures = winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures;

        FeatureLayer tabFeatureLayer = winformsMap1.FindFeatureLayer("TabFeatureLayer");
        tabFeatureLayer.FeatureIdsToExclude.Clear();
        tabFeatureLayer.Open();
        tabFeatureLayer.EditTools.BeginTransaction();

        //update all features that were in the EditOverlay.
        if (editFeatures.Count > 0)
        {
            foreach (Feature editFeature in editFeatures)
            {
                tabFeatureLayer.EditTools.Update(editFeature);
            }
        }

        //create all features that were in the TrackOverlayl.
        if (newFeatures.Count > 0)
        {
            foreach (Feature newFeature in newFeatures)
            {
                tabFeatureLayer.EditTools.Add(newFeature);
            }
        }

        tabFeatureLayer.EditTools.CommitTransaction();

        tabFeatureLayer.Close();

        //set the map back to normal mode.  Clear all features from the EditOverlay and TrackOverlay.
        winformsMap1.TrackOverlay.TrackMode = TrackMode.None;
        winformsMap1.EditOverlay.EditShapesLayer.InternalFeatures.Clear();
        winformsMap1.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

        winformsMap1.Refresh();
    }

    private void winformsMap1_MapClick(object sender, MapClickWinformsMapEventArgs e)
    {
        if (winformsMap1.TrackOverlay.TrackMode == TrackMode.None)
        {
            FeatureLayer tabFeatureLayer = winformsMap1.FindFeatureLayer("TabFeatureLayer");

            if (null != tabFeatureLayer)
            {
                //Open the TabFeatureLayer and do a spatial query to get the features that contained the point you clicked.
                tabFeatureLayer.Open();
                Collection<Feature> selectedFeatures = tabFeatureLayer.QueryTools.GetFeaturesContaining(e.WorldLocation, ReturningColumnsType.NoColumns);
                tabFeatureLayer.Close();

                InMemoryFeatureLayer highlightLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("HighLightFeatureLayer");
                highlightLayer.InternalFeatures.Clear();

                //insert each selected feature into the highlightLayer.
                foreach (Feature selectedFeature in selectedFeatures)
                {
                    highlightLayer.InternalFeatures.Add(selectedFeature);
                }

                winformsMap1.Refresh(winformsMap1.Overlays["HighLightOverlay"]);
            }
        }
    }
}
