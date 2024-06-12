using System;
using System.Linq;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DifferenceShapesSample : UserControl
    {
        public DifferenceShapesSample()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            ShapeFileFeatureLayer cityLimits = new ShapeFileFeatureLayer(@"./Data/ShapeFile/FriscoCityLimits.shp");
            InMemoryFeatureLayer westRegionLayer = new InMemoryFeatureLayer();
            InMemoryFeatureLayer differenceLayer = new InMemoryFeatureLayer();
            LayerOverlay layerOverlay = new LayerOverlay();

            // Project cityLimits layer to Spherical Mercator to match the map projection
            cityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style cityLimits layer
            cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
            cityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style cityLimits layer
            westRegionLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Blue), GeoColors.DimGray);
            westRegionLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style the differenceLayer
            differenceLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(64, GeoColors.Green), GeoColors.DimGray);
            differenceLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add cityLimits to a LayerOverlay
            layerOverlay.Layers.Add("cityLimits", cityLimits);

            // Add cityLimits to a LayerOverlay
            layerOverlay.Layers.Add("westRegionLayer", westRegionLayer);

            // Add differenceLayer to the layerOverlay
            layerOverlay.Layers.Add("differenceLayer", differenceLayer);

            // Set the map extent to the cityLimits layer bounding box
            cityLimits.Open();
            mapView.CurrentExtent = cityLimits.GetBoundingBox();
            cityLimits.Close();

            // Add LayerOverlay to Map
            mapView.Overlays.Add("layerOverlay", layerOverlay);

            // Add west region area to westRegionLayer
            westRegionLayer.InternalFeatures.Add(new Feature("Polygon ((-10780139.10763415694236755 3918539.43726690439507365, -10780206.68015497177839279 3915600.03261143481358886, -10780088.42824354581534863 3914687.80358042707666755, -10780037.7488529346883297 3913978.2921118657104671, -10779767.4587696734815836 3913555.96385676926001906, -10779176.19921253807842731 3913336.35316411964595318, -10778635.61904601566493511 3913049.16995065379887819, -10778280.86331173405051231 3911934.22335720015689731, -10778263.970181530341506 3910684.13172211544588208, -10778382.22209295630455017 3910447.6278992616571486, -10778382.22209295630455017 3909569.1851286618039012, -10778263.970181530341506 3909113.07061315793544054, -10778280.86331173405051231 3907356.18507195776328444, -10785595.58868999965488911 3904045.13155200378969312, -10786034.81007529981434345 3904822.21554138045758009, -10786001.02381489239633083 3908150.16219153860583901, -10786051.70320550352334976 3908690.7423580614849925, -10785933.45129407569766045 3909315.78817560384050012, -10786001.02381489239633083 3911275.39127924991771579, -10785882.77190346457064152 3912204.5134404618293047, -10785832.09251285344362259 3914485.08601798117160797, -10785832.09251285344362259 3917728.56701711937785149, -10785426.65738796070218086 3918117.10901180794462562, -10785460.44364836812019348 3919012.44491261197254062, -10782233.85577943362295628 3918995.55178240826353431, -10781642.59622230008244514 3918623.90291792340576649, -10780139.10763415694236755 3918539.43726690439507365))"));

            await mapView.RefreshAsync();
        }

        private async void shapeDifference_Click(object sender, EventArgs e)
        {
            LayerOverlay layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];

            ShapeFileFeatureLayer cityLimits = (ShapeFileFeatureLayer)layerOverlay.Layers["cityLimits"];
            InMemoryFeatureLayer westRegionLayer = (InMemoryFeatureLayer)layerOverlay.Layers["westRegionLayer"];
            InMemoryFeatureLayer differenceLayer = (InMemoryFeatureLayer)layerOverlay.Layers["differenceLayer"];

            // Query the cityLimits layer to get the first feature
            cityLimits.Open();
            var feature = cityLimits.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns).First();
            cityLimits.Close();

            // Get the westRegion area
            var westRegion = westRegionLayer.InternalFeatures[0];

            // Gets the difference of the feature from the westRegion area
            var difference = feature.GetDifference(westRegion);

            // Add the difference into an InMemoryFeatureLayer to display the result.
            differenceLayer.InternalFeatures.Clear();
            differenceLayer.InternalFeatures.Add(difference);

            // Redraw the layerOverlay to see the difference feature on the map
            await layerOverlay.RefreshAsync();
        }

        #region Component Designer generated code
        private Panel panel1;
        private Button shapeDifference;
        private Label label1;

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.shapeDifference = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
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
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(817, 639);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.shapeDifference);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(815, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 639);
            this.panel1.TabIndex = 1;
            // 
            // shapeDifference
            // 
            this.shapeDifference.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shapeDifference.Location = new System.Drawing.Point(3, 60);
            this.shapeDifference.Name = "shapeDifference";
            this.shapeDifference.Size = new System.Drawing.Size(295, 35);
            this.shapeDifference.TabIndex = 1;
            this.shapeDifference.Text = "Get Difference";
            this.shapeDifference.UseVisualStyleBackColor = true;
            this.shapeDifference.Click += new System.EventHandler(this.shapeDifference_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Difference Controls:";
            // 
            // DifferenceShapesSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "DifferenceShapesSample";
            this.Size = new System.Drawing.Size(1116, 639);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

    }
}