using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace TopologyValidation
{
    public class PolygonsMustBeLargerThanClusterToleranceTopologyTestCase : OneInputLayerTopologyTestCase
    {
        private double tolerance = 30;
        private InMemoryFeatureLayer assistantLayer;

        public PolygonsMustBeLargerThanClusterToleranceTopologyTestCase()
            : this("PolygonsMustBeLargerThanClusterToleranceTopologyTestCase")
        { }

        public PolygonsMustBeLargerThanClusterToleranceTopologyTestCase(string name)
            : base(name)
        {
            TestCaseInputType = TopologyValidation.TestCaseInputType.Polygon;
            InputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.FromArgb(70, 0, 0, 255), 2, true);
            InputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(20, 0, 0, 255), GeoColor.FromArgb(255, 0, 0, 255), 2);
            OutputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.FromArgb(200, GeoColor.StandardColors.Green), 2, true);
            OutputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(102, 0, 255, 0), GeoColor.FromArgb(255, 0, 0, 255), 2);
            OutputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.SimpleColors.Transparent, 10);

            assistantLayer = new InMemoryFeatureLayer();
            assistantLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(20, 255, 255, 255), GeoColor.FromArgb(70, 0, 0, 0));
            assistantLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimplePointStyle(PointSymbolType.Circle, GeoColor.SimpleColors.Red, 5);
            assistantLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            this.Layers.Add(assistantLayer);
        }

        public double Tolerance
        {
            get { return tolerance; }
            set { tolerance = value; }
        }

        public InMemoryFeatureLayer AssistantLayer
        {
            get { return assistantLayer; }
        }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = null;

            if (!LoadingFromShapeFile)
            {
                results = TopologyValidator.PolygonsMustBeLargerThanClusterTolerance(InputFeatureLayer.InternalFeatures, Tolerance);
            }

            OutputFeatureLayer.InternalFeatures.Clear();
            assistantLayer.InternalFeatures.Clear();
            foreach (Feature polygonShape in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(polygonShape);
                foreach (Vertex vertex in (polygonShape.GetShape() as PolygonShape).OuterRing.Vertices)
                {
                    EllipseShape ellipseShape = new EllipseShape(new PointShape(vertex), Tolerance);
                    AssistantLayer.InternalFeatures.Add(new Feature(ellipseShape));

                    Feature pointFeature = new Feature(vertex);
                    AssistantLayer.InternalFeatures.Add(pointFeature);
                }

                foreach (var item in (polygonShape.GetShape() as PolygonShape).InnerRings)
                {
                    foreach (var vertex in item.Vertices)
                    {
                        EllipseShape ellipseShape = new EllipseShape(new PointShape(vertex), Tolerance);
                        AssistantLayer.InternalFeatures.Add(new Feature(ellipseShape));

                        Feature pointFeature = new Feature(vertex);
                        AssistantLayer.InternalFeatures.Add(pointFeature);
                    }
                }
            }
        }

        protected override void ClearCore()
        {
            base.ClearCore();
            assistantLayer.InternalFeatures.Clear();
        }
    }
}
