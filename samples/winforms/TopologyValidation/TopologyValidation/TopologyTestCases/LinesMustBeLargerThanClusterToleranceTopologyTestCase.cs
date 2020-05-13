using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace TopologyValidation
{
    public class LinesMustBeLargerThanClusterToleranceTopologyTestCase : OneInputLayerTopologyTestCase
    {
        private double tolerance = 30;
        private InMemoryFeatureLayer assistantLayer;

        public InMemoryFeatureLayer AssistantLayer
        {
            get { return assistantLayer; }
        }

        public LinesMustBeLargerThanClusterToleranceTopologyTestCase()
            : this("LinesMustBeLargerThanClusterToleranceTopologyTestCase")
        { }

        public LinesMustBeLargerThanClusterToleranceTopologyTestCase(string name)
            : base(name)
        {
            TestCaseInputType = TestCaseInputType.Line;
            InputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.FromArgb(70, 0, 0, 255), 2, true);
            InputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(20, 0, 0, 255), GeoColor.FromArgb(255, 0, 0, 255), 2);
            OutputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.FromArgb(200, GeoColor.StandardColors.Green), 2, true);
            OutputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(102, 0, 0, 255), GeoColor.FromArgb(255, 0, 0, 255), 2);
            OutputFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimpleCircleStyle(GeoColor.SimpleColors.Transparent, 10);

            assistantLayer = new InMemoryFeatureLayer();
            assistantLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(20, 255, 255, 255), GeoColor.FromArgb(70, 0, 0, 0));
            assistantLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.CreateSimplePointStyle(PointSymbolType.Circle, GeoColor.SimpleColors.Red, 5);
            assistantLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            this.Layers.Add(assistantLayer);
        }

        public double Tolerance
        {
            get
            {
                return tolerance;
            }
            set
            {
                tolerance = value;
            }
        }

        protected override void GenerateTestResultCore()
        {
            Collection<Feature> results = TopologyValidator.LinesMustBeLargerThanClusterTolerance(InputFeatureLayer.InternalFeatures, tolerance);
            
            OutputFeatureLayer.InternalFeatures.Clear();
            assistantLayer.InternalFeatures.Clear();
            foreach (Feature result in results)
            {
                OutputFeatureLayer.InternalFeatures.Add(result);
                if (result.GetShape() is LineBaseShape)
                {
                    foreach (Vertex vertex in ((LineShape)result.GetShape()).Vertices)
                    {
                        EllipseShape ellipseShape = new EllipseShape(new PointShape(vertex), tolerance);
                        assistantLayer.InternalFeatures.Add(new Feature(ellipseShape));

                        Feature pointFeature = new Feature(vertex);
                        assistantLayer.InternalFeatures.Add(pointFeature);
                    }
                }
                else
                {
                    EllipseShape ellipseShape = new EllipseShape(result, tolerance);
                    assistantLayer.InternalFeatures.Add(new Feature(ellipseShape));
                    assistantLayer.InternalFeatures.Add(result);
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
