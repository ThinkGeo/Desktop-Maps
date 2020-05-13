using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace BreadCrumbTrail
{
    public partial class Form1 : Form
    {
        private int count1 = 0;
        private int count2 = 0;
        Collection<PointShape> pointShapes1;
        Collection<PointShape> pointShapes2;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadStreetData();

            InMemoryFeatureLayer inMemoryFeatureLayer = new InMemoryFeatureLayer();
            inMemoryFeatureLayer.Open();
            inMemoryFeatureLayer.Columns.Add(new FeatureSourceColumn("Angle"));
            inMemoryFeatureLayer.Close();

            pointShapes1 = ReadTextFile("../../data/GPSreadings1.txt");
            pointShapes2 = ReadTextFile("../../data/GPSreadings2.txt");

            Feature newFeature1 = new Feature(pointShapes1[count1]) { Id = "Vehicle1" };
            newFeature1.ColumnValues["Angle"] = System.Convert.ToString(pointShapes1[count1].Z);
            inMemoryFeatureLayer.InternalFeatures.Add("Vehicle1", newFeature1);

            Feature newFeature2 = new Feature(pointShapes2[count2]) { Id = "Vehicle2" };
            newFeature2.ColumnValues["Angle"] = System.Convert.ToString(pointShapes2[count2].Z);
            inMemoryFeatureLayer.InternalFeatures.Add("Vehicle2", newFeature2);


            RotatedImageStyle rotatedImageStyle = new RotatedImageStyle(new GeoImage("../../data/vehicle2.png"), "Angle");

            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(rotatedImageStyle);
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            InMemoryFeatureLayer breadCrumbTrailInMemoryFeatureLayer = new InMemoryFeatureLayer();
            breadCrumbTrailInMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyles.CreateSimpleLineStyle(GeoColor.StandardColors.RoyalBlue, 2, true);
            breadCrumbTrailInMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;


            LayerOverlay overLayer = new LayerOverlay();
            overLayer.Layers.Add(inMemoryFeatureLayer);
            overLayer.Layers.Add(breadCrumbTrailInMemoryFeatureLayer);
            winformsMap1.Overlays.Add("Overlay", overLayer);


            timer1.Start();
            winformsMap1.CurrentExtent = new RectangleShape(-66.8581, 10.4995, -66.8456, 10.4915);
            winformsMap1.Refresh();

        }

        //Function to build a LineShape for trailing tail based on a collection of points and the number
        //trailing points. 
        private LineShape BuildLineShape(Collection<PointShape> pointShapes, int index, int number)
        {
            LineShape lineShape = new LineShape();

            int i;
            int sub = index - number;

            for (i = index; i >= sub; i -= 1)
            {
                if (i >= 0)
                {
                    lineShape.Vertices.Add(new Vertex(pointShapes[i]));
                }
            }

            if (lineShape.Vertices.Count <= 1)
            {
                lineShape.Vertices.Add(new Vertex(pointShapes[0]));
            }

            return lineShape;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (count1 == pointShapes1.Count) count1 = 0;
            if (count2 == pointShapes2.Count) count2 = 0;

            try
            {

                winformsMap1.Overlays["Overlay"].Lock.EnterWriteLock();
                LayerOverlay pointOverLay = (LayerOverlay)winformsMap1.Overlays["Overlay"];
                InMemoryFeatureLayer inMemoryFeatureLayer = (InMemoryFeatureLayer)pointOverLay.Layers[0];


                Feature feature = inMemoryFeatureLayer.InternalFeatures["Vehicle1"];
                feature.ColumnValues["Angle"] = System.Convert.ToString(pointShapes1[count1].Z);
                PointShape pointShape1 = feature.GetShape() as PointShape;
                pointShape1.X = pointShapes1[count1].X;
                pointShape1.Y = pointShapes1[count1].Y;
                pointShape1.Id = "Vehicle1";

                Feature feature2 = inMemoryFeatureLayer.InternalFeatures["Vehicle2"];
                feature2.ColumnValues["Angle"] = System.Convert.ToString(pointShapes2[count2].Z);
                PointShape pointShape2 = feature2.GetShape() as PointShape;
                pointShape2.X = pointShapes2[count2].X;
                pointShape2.Y = pointShapes2[count2].Y;
                pointShape2.Id = "Vehicle2";

                inMemoryFeatureLayer.Open();
                inMemoryFeatureLayer.EditTools.BeginTransaction();
                inMemoryFeatureLayer.EditTools.Update(pointShape1);
                inMemoryFeatureLayer.EditTools.Update(pointShape2);
                inMemoryFeatureLayer.EditTools.CommitTransaction();
                inMemoryFeatureLayer.Close();

                //Updates the trailing tail of both vehicles.
                InMemoryFeatureLayer breadCrumbTrailInMemoryFeatureLayer = (InMemoryFeatureLayer)pointOverLay.Layers[1];
                breadCrumbTrailInMemoryFeatureLayer.Open();
                breadCrumbTrailInMemoryFeatureLayer.EditTools.BeginTransaction();
                breadCrumbTrailInMemoryFeatureLayer.InternalFeatures.Clear();
                breadCrumbTrailInMemoryFeatureLayer.EditTools.Add(new Feature(BuildLineShape(pointShapes1, count1, 5)));
                breadCrumbTrailInMemoryFeatureLayer.EditTools.Add(new Feature(BuildLineShape(pointShapes2, count2, 5)));
                breadCrumbTrailInMemoryFeatureLayer.EditTools.CommitTransaction();
                breadCrumbTrailInMemoryFeatureLayer.Close();

            }
            finally
            {
                winformsMap1.Overlays["Overlay"].Lock.ExitWriteLock();
                count1 = count1 + 1;
                count2 = count2 + 1;

            }
            winformsMap1.Refresh();
        }

        private Collection<PointShape> ReadTextFile(string textFile)
        {
            Collection<PointShape> pointShapes = new Collection<PointShape>();
            StreamReader SR = File.OpenText(textFile);
            string S;
            S = SR.ReadLine();
            while (S != null)
            {
                double X = System.Convert.ToDouble(S.Substring(0, 8));
                double Y = System.Convert.ToDouble(S.Substring(9, 7));
                double Angle = System.Convert.ToDouble(S.Substring(17, 3));
                PointShape pointShape = new PointShape(X, Y, Angle);
                pointShapes.Add(pointShape);
                S = SR.ReadLine();

            }
            SR.Close();
            return pointShapes;
        }


        private void LoadStreetData()
        {
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214));

            ShapeFileFeatureLayer ParksShapeLayer = new ShapeFileFeatureLayer("../../data/parks.shp");
            ShapeFileFeatureLayer.BuildIndexFile("../../data/parks.shp", BuildIndexMode.DoNotRebuild);

            ParksShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            ParksShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = WorldStreetsAreaStyles.Park();

            ShapeFileFeatureLayer ParksLabelLayer = new ShapeFileFeatureLayer("../../data/parks.shp");
            ParksLabelLayer.RequireIndex = false;
            ParksLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyles.CreateSimpleTextStyle("Name", "Arial", 10, DrawingFontStyles.Bold, GeoColor.StandardColors.SeaGreen, GeoColor.StandardColors.White, 2);
            ParksLabelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer StreetsShapeLayer = new ShapeFileFeatureLayer("../../data/streets.shp");
            ShapeFileFeatureLayer.BuildIndexFile("../../data/streets.shp", BuildIndexMode.DoNotRebuild);

            StreetsShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            ValueStyle valueStyle = new ValueStyle();
            valueStyle.ColumnName = "Type";
            valueStyle.ValueItems.Add(new ValueItem("A", WorldStreetsLineStyles.RoadFill(8f)));
            valueStyle.ValueItems.Add(new ValueItem("M", LineStyles.CreateSimpleLineStyle(GeoColor.FromArgb(255, 255, 255, 185), 9.2F, GeoColor.StandardColors.LightGray, 12.2F, true)));
            StreetsShapeLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(valueStyle);

            ShapeFileFeatureLayer StreetsLabelLayer = new ShapeFileFeatureLayer("../../data/streets.shp");
            StreetsLabelLayer.RequireIndex = false;
            TextStyle textStyle = new TextStyle();
            textStyle.TextColumnName = "St_name";
            textStyle.TextSolidBrush = new GeoSolidBrush(GeoColor.StandardColors.Black);
            textStyle.Font = new GeoFont("Arial", 7);
            textStyle.TextLineSegmentRatio = 10;
            textStyle.GridSize = 100;
            textStyle.SplineType = SplineType.StandardSplining;
            textStyle.DuplicateRule = LabelDuplicateRule.OneDuplicateLabelPerQuadrant;
            StreetsLabelLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(textStyle);
            StreetsLabelLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.GridSize = 5;
            StreetsLabelLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("Parks", ParksShapeLayer);
            staticOverlay.Layers.Add("Streets", StreetsShapeLayer);
            staticOverlay.Layers.Add("Parks Labels", ParksLabelLayer);
            staticOverlay.Layers.Add("Streets Labels", StreetsLabelLayer);

            winformsMap1.Overlays.Add("Street Overlay", staticOverlay);
        }


    }
}
