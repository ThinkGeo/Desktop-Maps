using System;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Drawing;

namespace EditGridLayer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            GridFeatureLayer gridFeatureLayer = new GridFeatureLayer(@"..\..\data\testdata.asc");

            ClassBreakStyle classBreakStyle = new ClassBreakStyle("CellValue");

            AreaStyle areaStyleOne = new AreaStyle(GeoPens.White, GeoBrushes.Red);
            AreaStyle areaStyleTwo = new AreaStyle(GeoPens.White, GeoBrushes.Orange);
            AreaStyle areaStyleThree = new AreaStyle(GeoPens.White, GeoBrushes.Yellow);
            AreaStyle areaStyleFour = new AreaStyle(GeoPens.White, GeoBrushes.Green);
            AreaStyle areaStyleFive = new AreaStyle(GeoPens.White, GeoBrushes.Blue);
            AreaStyle areaStyleSix = new AreaStyle(GeoPens.White, GeoBrushes.Indigo);
            AreaStyle areaStyleSeven = new AreaStyle(GeoPens.White, GeoBrushes.Violet);

            classBreakStyle.ClassBreaks.Add(new ClassBreak(1, areaStyleOne));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(2, areaStyleTwo));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(3, areaStyleThree));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(4, areaStyleFour));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(5, areaStyleFive));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(6, areaStyleSix));
            classBreakStyle.ClassBreaks.Add(new ClassBreak(7, areaStyleSeven));

            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(classBreakStyle);
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            gridFeatureLayer.Open();
            winformsMap1.CurrentExtent = gridFeatureLayer.GetBoundingBox();

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("Grid View", gridFeatureLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.TrackOverlay.TrackEnded += TrackOverlay_TrackEnded;

            winformsMap1.TrackOverlay.TrackMode = TrackMode.Polygon;
            winformsMap1.Refresh();
        }

        private void btnIncrementValue_Click(object sender, EventArgs e)
        {
            LayerOverlay staticOverlay = (LayerOverlay)winformsMap1.Overlays[0];
            GridFeatureLayer gridFeatureLayer = (GridFeatureLayer)staticOverlay.Layers["Grid View"];

            winformsMap1.TrackOverlay.TrackShapeLayer.FeatureSource.Open();
            Feature selectionFeature = winformsMap1.TrackOverlay.TrackShapeLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.NoColumns)?[0];

            Collection<Feature> featuresToUpdate = gridFeatureLayer.QueryTools.GetFeaturesWithin(selectionFeature, new string[0]);

            var test = gridFeatureLayer.FeatureSource.IsEditable;
            gridFeatureLayer.EditTools.BeginTransaction();

            foreach (Feature featureToUpdate in featuresToUpdate)
            {
                featureToUpdate.ColumnValues["CellValue"] = (int.Parse(featureToUpdate.ColumnValues["CellValue"]) + 1).ToString();
                gridFeatureLayer.EditTools.Update(featureToUpdate);
            }

            gridFeatureLayer.EditTools.CommitTransaction();

            gridFeatureLayer.Close();
            gridFeatureLayer.Open();

            winformsMap1.TrackOverlay.TrackShapeLayer.Clear();

            winformsMap1.Refresh(staticOverlay);

            winformsMap1.TrackOverlay.TrackMode = TrackMode.Polygon;
        }

        private void TrackOverlay_TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            winformsMap1.TrackOverlay.TrackMode = TrackMode.None;
        }
    }
}
