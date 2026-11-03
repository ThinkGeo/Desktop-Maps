using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DisplayESRIGridFile : UserControl
    {
        public DisplayESRIGridFile()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            var thinkGeoVectorTileOverlay = new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey);
            mapView.Overlays.Add(thinkGeoVectorTileOverlay);

            // Load the grid file; the data is in srid 2276 and our background is srid 3857 (spherical mercator).
            var gridSource = new AscGridSource(@"..\..\..\data\GridFile\Mosquitos.grd", new Projection(2276));

            // Create an overlay that draws the grid in srid 3857 and add it to the map.
            var gridOverlay = new GridOverlay(gridSource, GridStyle(), new Projection(3857))
            {
                ResamplingMode = RasterResamplingMode.NearestNeighbor
            };
            mapView.Overlays.Add(gridOverlay);

            // Set the map view current extent to the bounding box of the grid, in srid 3857.
            var projectionConverter = new ProjectionConverter(2276, 3857);
            projectionConverter.Open();
            var gridBBox = projectionConverter.ConvertToExternalProjection(gridSource.Extent);
            projectionConverter.Close();
            mapView.CenterPoint = gridBBox.GetCenterPoint();
            var MapScale = MapUtil.GetScale(mapView.MapUnit, gridBBox, mapView.MapWidth, mapView.MapHeight);
            mapView.CurrentScale = MapScale * 1.5; // Multiply the current scale by 1.5 to zoom out 50%.

            // Refresh the map.
            await mapView.RefreshAsync();
        }

        // The mosquito count bands, each colour held from its value up to the next.
        private static GridFillStyle GridStyle() => new GridFillStyle
        {
            RampMode = ColorRampMode.Classes,
            Opacity = 100 / 255f,
            ColorStops = new[]
            {
                new ColorStop(0, GeoColor.FromArgb(255, 0, 255, 0)),
                new ColorStop(12, GeoColor.FromArgb(255, 128, 255, 128)),
                new ColorStop(24, GeoColor.FromArgb(255, 224, 251, 132)),
                new ColorStop(36, GeoColor.FromArgb(255, 225, 255, 0)),
                new ColorStop(48, GeoColor.FromArgb(255, 245, 210, 10)),
                new ColorStop(60, GeoColor.FromArgb(255, 255, 128, 0)),
                new ColorStop(72, GeoColor.FromArgb(255, 255, 0, 0)),
            },
        };

        #region Create Sample Data

        //var mos = new ShapeFileFeatureSource(@"./Data/Frisco_Mosquitos.shp");
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
        #endregion

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotationAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1047, 568);
            this.mapView.TabIndex = 0;
            // 
            // DisplayESRIGridFile
            // 
            this.Controls.Add(this.mapView);
            this.Name = "DisplayESRIGridFile";
            this.Size = new System.Drawing.Size(1047, 568);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}