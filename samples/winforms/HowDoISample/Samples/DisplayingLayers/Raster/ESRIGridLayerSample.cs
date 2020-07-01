using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ESRIGridLayerSample: UserControl
    {
        public ESRIGridLayerSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay staticOverlay = new LayerOverlay();
            mapView.Overlays.Add(staticOverlay);

            // Create the new layer and set the projection as the data is in srid 2276 and our background is srid 3857 (spherical mercator).
            GridFeatureLayer gridFeatureLayer = new GridFeatureLayer(@"..\..\..\data\GridFile\Mosquitos.grd");
            gridFeatureLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add the layer to the overlay we created earlier.
            staticOverlay.Layers.Add("GridFeatureLayer", gridFeatureLayer);

            // Create a class break style based on the cell values and set area styles based on the values
            ClassBreakStyle gridClassBreakStyle = new ClassBreakStyle("CellValue");
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 0, 255, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(12, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 128, 255, 128)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(24, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 224, 251, 132)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(36, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 225, 255, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(48, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 245, 210, 10)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(60, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 255, 128, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(72, new AreaStyle(new GeoSolidBrush(GeoColor.FromArgb(100, 255, 0, 0)))));
            gridClassBreakStyle.ClassBreaks.Add(new ClassBreak(double.MaxValue, new AreaStyle(new GeoSolidBrush(GeoColors.Transparent))));

            // Take the class break style we created above and set it on zoom level 1 and then apply it to all zoom levels up to 20.
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(gridClassBreakStyle);
            gridFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Open the layer and set the map view current extent to the bounding box of the layer.  
            gridFeatureLayer.Open();
            mapView.CurrentExtent = gridFeatureLayer.GetBoundingBox();

            // Refresh the map.
            mapView.Refresh();
        }

        //var mos = new ShapeFileFeatureSource(@"../../../data/Frisco_Mosquitos.shp");
        //mos.Open();
        //var features = mos.GetAllFeatures(ReturningColumnsType.AllColumns);

        //Dictionary<PointShape, double> points = new Dictionary<PointShape, double>();

        //foreach (var feature in features)
        //{
        //    if (feature.ColumnValues["DateCollec"] == "20190806")
        //    {
        //        int male = 0;
        //        int female = 0;

        //        if (feature.ColumnValues["Male"] != "")
        //            male = int.Parse(feature.ColumnValues["Male"]);

        //        if (feature.ColumnValues["Female"] != "")
        //            female = int.Parse(feature.ColumnValues["Female"]);

        //        points.Add((PointShape)feature.GetShape(), male + female);
        //    }
        //}
        //FileStream stream = new FileStream(@"C:\temp\Mosquitos.grd", FileMode.Create, FileAccess.ReadWrite);
        //GridFeatureSource.GenerateGrid(new GridDefinition(RectangleShape.ScaleUp(mos.GetBoundingBox(),30).GetBoundingBox(), 300, -999, points), new InverseDistanceWeightedGridInterpolationModel(), stream);
        //stream.Close();

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.SuspendLayout();
            //
            // mapView
            //
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.Controls.Add(this.mapView);
            //
            // UserControl
            //
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}