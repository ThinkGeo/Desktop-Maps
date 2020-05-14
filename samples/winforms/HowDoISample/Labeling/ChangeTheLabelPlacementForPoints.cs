using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ChangeTheLabelPlacementForPoints : UserControl
    {
        public ChangeTheLabelPlacementForPoints()
        {
            InitializeComponent();
        }

        private void ChangeTheLabelPlacementForPoints_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));

            ShapeFileFeatureLayer statesLayer = new ShapeFileFeatureLayer(SampleHelper.Get("USStates.shp"));
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 156, 155, 154), 2);
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.LineJoin = DrawingLineJoin.Round;
            statesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer majorCitiesShapeLayer = new ShapeFileFeatureLayer(SampleHelper.Get("MajorCities.shp"));
            majorCitiesShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateCompoundCircleStyle(GeoColors.White, 10F, GeoColors.Black, 1F, GeoColors.Black, 7F);
            majorCitiesShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyle.CreateSimpleTextStyle("AREANAME", "Arial", 10, DrawingFontStyles.Regular, GeoColors.Black, 0, -8);
            majorCitiesShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.OverlappingRule = LabelOverlappingRule.AllowOverlapping;
            majorCitiesShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay statesOverlay = new LayerOverlay();
            statesOverlay.Layers.Add("World", worldLayer);
            statesOverlay.Layers.Add("States", statesLayer);
            statesOverlay.Layers.Add("MajorCitiesShapes", majorCitiesShapeLayer);
            winformsMap1.Overlays.Add("StatesOverlay", statesOverlay);
            winformsMap1.CurrentExtent = new RectangleShape(-126.4, 48.8, -67.0, 19.0);

            winformsMap1.Refresh();
        }

        private void cbxPointPlacement_SelectedIndexChanged(object sender, EventArgs e)
        {
            TextPlacement placement;
            switch (cbxPointPlacement.Text.Trim())
            {
                case "Center":
                    placement = TextPlacement.Center;
                    break;

                case "CenterLeft":
                    placement = TextPlacement.Left;
                    break;

                case "CenterRight":
                    placement = TextPlacement.Right;
                    break;

                case "LowerCenter":
                    placement = TextPlacement.Lower;
                    break;

                case "LowerLeft":
                    placement = TextPlacement.LowerLeft;
                    break;

                case "LowerRight":
                    placement = TextPlacement.LowerRight;
                    break;

                case "UpperCenter":
                    placement = TextPlacement.Upper;
                    break;

                case "UpperLeft":
                    placement = TextPlacement.UpperLeft;
                    break;

                case "UpperRight":
                    placement = TextPlacement.UpperRight;
                    break;

                default:
                    placement = TextPlacement.Right;
                    break;
            }

            FeatureLayer labelPlacementLayer = winformsMap1.FindFeatureLayer("MajorCitiesShapes");
            labelPlacementLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.TextPlacement = placement;

            winformsMap1.Refresh(winformsMap1.Overlays["StatesOverlay"]);
        }

        #region Component Designer generated code

        private GroupBox gbxFunctions;
        private ComboBox cbxPointPlacement;
        private MapView winformsMap1;

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
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
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