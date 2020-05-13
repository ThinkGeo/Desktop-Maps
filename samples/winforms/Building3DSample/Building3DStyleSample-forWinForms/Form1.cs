using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace Building3DStyleSample_forWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            winformsMap1.MapUnit = GeographyUnit.Meter;

            string buildingFilePath = @"..\..\AppData\osm_buildings_900913_min.shp";
            ShapeFileFeatureLayer buildingShapeLayer = new ShapeFileFeatureLayer(buildingFilePath);
            buildingShapeLayer.ZoomLevelSet.ZoomLevel16.CustomStyles.Add(new OsmBuildingAreaStyle());
            buildingShapeLayer.ZoomLevelSet.ZoomLevel16.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            buildingShapeLayer.Open();
            winformsMap1.CurrentExtent = buildingShapeLayer.QueryTools.GetFeatureById("1", ReturningColumnsType.NoColumns).GetBoundingBox();

            LayerOverlay buildingOverlay = new LayerOverlay();
            buildingOverlay.Layers.Add(buildingShapeLayer);
            winformsMap1.Overlays.Add(buildingOverlay);

            winformsMap1.Refresh();
        }
    }
}
