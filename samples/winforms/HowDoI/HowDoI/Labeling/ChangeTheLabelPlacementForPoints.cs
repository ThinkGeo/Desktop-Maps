using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class ChangeTheLabelPlacementForPoints : UserControl
    {
        public ChangeTheLabelPlacementForPoints()
        {
            InitializeComponent();
        }

        private void ChangeTheLabelPlacementForPoints_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\Countries02.shp");
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country1;

            ShapeFileFeatureLayer statesLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\USStates.shp");
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.State1;
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.LineJoin = DrawingLineJoin.Round;
            statesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer majorCitiesShapeLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\MajorCities.shp");
            majorCitiesShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.City1;
            majorCitiesShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyles.City1("AREANAME");
            majorCitiesShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.OverlappingRule = LabelOverlappingRule.AllowOverlapping;
            majorCitiesShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay statesOverlay = new LayerOverlay();
            statesOverlay.Layers.Add("World", worldLayer);
            statesOverlay.Layers.Add("States", statesLayer);
            statesOverlay.Layers.Add("MajorCitiesShapes", majorCitiesShapeLayer);
            winformsMap1.Overlays.Add("StatesOverlay", statesOverlay);
            winformsMap1.CurrentExtent = new RectangleShape(-14070784, 6240993, -7458406, 2154936);

            winformsMap1.Refresh();
        }

        private void cbxPointPlacement_SelectedIndexChanged(object sender, EventArgs e)
        {
            PointPlacement placement;
            switch (cbxPointPlacement.Text.Trim())
            {
                case "Center":
                    placement = PointPlacement.Center;
                    break;

                case "CenterLeft":
                    placement = PointPlacement.CenterLeft;
                    break;

                case "CenterRight":
                    placement = PointPlacement.CenterRight;
                    break;

                case "LowerCenter":
                    placement = PointPlacement.LowerCenter;
                    break;

                case "LowerLeft":
                    placement = PointPlacement.LowerLeft;
                    break;

                case "LowerRight":
                    placement = PointPlacement.LowerRight;
                    break;

                case "UpperCenter":
                    placement = PointPlacement.UpperCenter;
                    break;

                case "UpperLeft":
                    placement = PointPlacement.UpperLeft;
                    break;

                case "UpperRight":
                    placement = PointPlacement.UpperRight;
                    break;

                default:
                    placement = PointPlacement.CenterRight;
                    break;
            }

            FeatureLayer labelPlacementLayer = winformsMap1.FindFeatureLayer("MajorCitiesShapes");
            labelPlacementLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.PointPlacement = placement;

            winformsMap1.Refresh(winformsMap1.Overlays["StatesOverlay"]);
        }

        #region Component Designer generated code

        private GroupBox gbxFunctions;
        private ComboBox cbxPointPlacement;
        private WinformsMap winformsMap1;

        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbxFunctions = new System.Windows.Forms.GroupBox();
            this.cbxPointPlacement = new System.Windows.Forms.ComboBox();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.gbxFunctions.SuspendLayout();
            this.SuspendLayout();
            //
            // gbxFunctions
            //
            this.gbxFunctions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gbxFunctions.Controls.Add(this.cbxPointPlacement);
            this.gbxFunctions.Location = new System.Drawing.Point(595, 3);
            this.gbxFunctions.Name = "gbxFunctions";
            this.gbxFunctions.Size = new System.Drawing.Size(142, 60);
            this.gbxFunctions.TabIndex = 2;
            this.gbxFunctions.TabStop = false;
            this.gbxFunctions.Text = "PointPlacement";
            //
            // cbxPointPlacement
            //
            this.cbxPointPlacement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxPointPlacement.FormattingEnabled = true;
            this.cbxPointPlacement.Items.AddRange(new object[] {
            "UpperLeft",
            "UpperCenter",
            "UpperRight",
            "CenterRight",
            "Center",
            "CenterLeft",
            "LowerLeft",
            "LowerCenter",
            "LowerRight"});
            this.cbxPointPlacement.Location = new System.Drawing.Point(15, 19);
            this.cbxPointPlacement.Name = "cbxPointPlacement";
            this.cbxPointPlacement.Size = new System.Drawing.Size(121, 21);
            this.cbxPointPlacement.TabIndex = 0;
            this.cbxPointPlacement.Text = "CenterRight";
            this.cbxPointPlacement.SelectedIndexChanged += new System.EventHandler(this.cbxPointPlacement_SelectedIndexChanged);
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 3;
            this.winformsMap1.Text = "winformsMap1";
            //
            // ChangeTheLabelPlacementForPoints
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbxFunctions);
            this.Controls.Add(this.winformsMap1);
            this.Name = "ChangeTheLabelPlacementForPoints";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.ChangeTheLabelPlacementForPoints_Load);
            this.gbxFunctions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}