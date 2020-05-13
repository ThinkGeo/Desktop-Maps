using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace NLDASAnalysis
{
    public partial class Form1 : Form
    {
        private Feature polygonShapeFeature;
        private InMemoryFeatureLayer highlightLayer;
        private NldasAsciiGridFeatureLayer gridLayer;
        private SimpleMarkerOverlay markerOverlay;
        private LayerOverlay highlightOverlay;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.ZoomLevelSnapping = ZoomLevelSnappingMode.None;
            winformsMap1.CurrentExtent = new RectangleShape(-84.0233332758852, 28.5066557096906, -81.9576541657225, 27.2381142776906);

            LayerOverlay layerOverlay = new LayerOverlay();
            ShapeFileFeatureLayer boundaryLayer = new ShapeFileFeatureLayer(@"..\..\data\Polygon.shp");
            boundaryLayer.RequireIndex = false;
            boundaryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.StandardColors.LightGreen, GeoColor.StandardColors.Black);
            boundaryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            boundaryLayer.Open();
            polygonShapeFeature = boundaryLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns)[0];
            layerOverlay.Layers.Add(boundaryLayer);

            if(!File.Exists(@"..\..\Data\NLDASmask_UMDunified.idx"))
            {
                NldasAsciiGridFeatureSource.BuildIndexFile(@"..\..\Data\NLDASmask_UMDunified.asc", BuildIndexMode.DoNotRebuild);
            }
            gridLayer = new NldasAsciiGridFeatureLayer(@"..\..\Data\NLDASmask_UMDunified.asc");
            gridLayer.DrawingFeatures += GridLayer_DrawingFeatures;
            gridLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            gridLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColor.StandardColors.Red), GeoColor.SimpleColors.Black);
            gridLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyles.City1("GridNumber");
            layerOverlay.Layers.Add(gridLayer);

            highlightOverlay = new LayerOverlay();
            highlightLayer = new InMemoryFeatureLayer();
            highlightLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            highlightLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(70, GeoColor.StandardColors.Green), GeoColor.SimpleColors.Black);
            highlightOverlay.Layers.Add(highlightLayer);

            markerOverlay = InitializeMarkerOverlay();
            winformsMap1.Overlays.Add(markerOverlay);
            winformsMap1.Overlays.Add(highlightOverlay);
            winformsMap1.Overlays.Add(layerOverlay);

            winformsMap1.Refresh();
        }

        private SimpleMarkerOverlay InitializeMarkerOverlay()
        {
            var markerOverlay = new SimpleMarkerOverlay();
            var marker = new Marker();
            Label content = new Label();
            content.Width = 102;
            content.Height = 60;
            content.Font = new Font("Arial", 10, FontStyle.Bold);
            content.ForeColor = Color.Black;
            content.BackColor = Color.Transparent;
            content.Margin = new Padding(0, -10, 0, 0);
            marker.Controls.Add(content);

            markerOverlay.Markers.Add(marker);
            markerOverlay.MapControl = winformsMap1;

            return markerOverlay;
        }

        private void GridLayer_DrawingFeatures(object sender, DrawingFeaturesEventArgs e)
        {
            if (cbHide.Checked)
            {
                var filterFeatures = gridLayer.FeatureSource.SpatialQuery(polygonShapeFeature, QueryType.Intersects, ReturningColumnsType.AllColumns);
                e.FeaturesToDraw.Clear();
                foreach (var feature in filterFeatures)
                {
                    e.FeaturesToDraw.Add(feature);
                }
            }
        }

        private void winformsMap1_MapClick(object sender, MapClickWinformsMapEventArgs e)
        {
            var marker = markerOverlay.Markers[0];

            var clickPoint = e.WorldLocation;
            var clickedFeatures = gridLayer.FeatureSource.GetFeaturesNearestTo(clickPoint, GeographyUnit.DecimalDegree, 1, ReturningColumnsType.AllColumns);
            highlightLayer.InternalFeatures.Clear();

            if (clickedFeatures != null && clickedFeatures.Count > 0)
            {
                var clickedFeature = clickedFeatures[0];
                highlightLayer.InternalFeatures.Add(clickedFeature);

                var percent = GetCellPercent(clickedFeature);

                marker.Position = e.WorldLocation;

                var markerLabel = (Label)marker.Controls[0];

                markerLabel.Text = $"{percent}%";

                if (percent == 0)
                    markerLabel.Text = "Outside of the shape";
                else if (percent == 100)
                    markerLabel.Text = "The shape contains the cell";
            }

            winformsMap1.Refresh(markerOverlay);
            winformsMap1.Refresh(highlightOverlay);
        }

        private double GetCellPercent(Feature clickedFeature)
        {
            double percent = 0;
            if (polygonShapeFeature.Contains(clickedFeature))
            {
                percent = 100;
            }
            else
            {
                var clickedFeatureArea = ((PolygonShape)clickedFeature.GetShape()).GetArea(GeographyUnit.DecimalDegree, AreaUnit.SquareKilometers);
                var intersectionFeature = polygonShapeFeature.GetIntersection(clickedFeature);
                if (intersectionFeature != null)
                {
                    var intersection = intersectionFeature.GetShape();
                    var intersectionShape = intersection as AreaBaseShape;
                    var intersectionArea = intersectionShape.GetArea(GeographyUnit.DecimalDegree, AreaUnit.SquareKilometers);
                    percent = Math.Round(intersectionArea / clickedFeatureArea * 100, 2);
                }
            }
            return percent;
        }

        private void cbHide_CheckedChanged(object sender, EventArgs e)
        {
            winformsMap1.Refresh();
        }
    }
}
