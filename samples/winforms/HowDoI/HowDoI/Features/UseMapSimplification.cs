using System;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class UseMapSimplification : UserControl
    {
        private AreaBaseShape areaBaseShape;

        public UseMapSimplification()
        {
            InitializeComponent();
        }

        private void UseMapSimplification_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\Countries02.shp");

            worldLayer.Open();
            Feature feature = worldLayer.QueryTools.GetFeatureById("135", new string[0]);
            areaBaseShape = (AreaBaseShape)feature.GetShape();
            worldLayer.Close();

            InMemoryFeatureLayer simplificationLayer = new InMemoryFeatureLayer();
            simplificationLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            simplificationLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country1;
            simplificationLayer.InternalFeatures.Add(feature);

            LayerOverlay simplificationOverlay = new LayerOverlay();
            simplificationOverlay.Layers.Add("SimplificationLayer", simplificationLayer);
            winformsMap1.Overlays.Add("SimplificationOverlay", simplificationOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-19747615, 17926782, -5857338, 1637456);

            winformsMap1.Refresh();
        }

        private void btnSimplify_Click(object sender, EventArgs e)
        {
            InMemoryFeatureLayer simplificationLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("SimplificationLayer");

            double tolerance = Convert.ToDouble(cmbTolerance.Text, CultureInfo.InvariantCulture);
            SimplificationType simplificationType = (SimplificationType)cmbSimplificationType.SelectedIndex;

            MultipolygonShape multipolygonShape = areaBaseShape.Simplify(tolerance, simplificationType);
            simplificationLayer.InternalFeatures.Clear();
            simplificationLayer.InternalFeatures.Add(new Feature(multipolygonShape));

            winformsMap1.Refresh(winformsMap1.Overlays["SimplificationOverlay"]);
        }

        private WinformsMap winformsMap1;

        #region Component Designer generated code

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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbSimplificationType = new System.Windows.Forms.ComboBox();
            this.btnSimplify = new System.Windows.Forms.Button();
            this.cmbTolerance = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.cmbSimplificationType);
            this.groupBox1.Controls.Add(this.btnSimplify);
            this.groupBox1.Controls.Add(this.cmbTolerance);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(533, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 111);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // cmbSimplificationType
            //
            this.cmbSimplificationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSimplificationType.FormattingEnabled = true;
            this.cmbSimplificationType.Items.AddRange(new object[] {
            "TopologyPreserving",
            "DouglasPeucker"});
            this.cmbSimplificationType.Location = new System.Drawing.Point(80, 45);
            this.cmbSimplificationType.Name = "cmbSimplificationType";
            this.cmbSimplificationType.Size = new System.Drawing.Size(120, 21);
            this.cmbSimplificationType.TabIndex = 4;
            this.cmbSimplificationType.Text = "TopologyPreserving";
            //
            // btnSimplify
            //
            this.btnSimplify.Location = new System.Drawing.Point(146, 77);
            this.btnSimplify.Name = "btnPan";
            this.btnSimplify.Size = new System.Drawing.Size(50, 20);
            this.btnSimplify.TabIndex = 24;
            this.btnSimplify.Text = "Simplify";
            this.btnSimplify.UseVisualStyleBackColor = true;
            this.btnSimplify.Click += new System.EventHandler(this.btnSimplify_Click);
            //
            // cmbTolerance
            //
            this.cmbTolerance.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTolerance.FormattingEnabled = true;
            this.cmbTolerance.Items.AddRange(new object[] {
            "75000",
            "150000",
            "300000"});
            this.cmbTolerance.Location = new System.Drawing.Point(10, 45);
            this.cmbTolerance.Name = "cmbTolerance";
            this.cmbTolerance.Size = new System.Drawing.Size(50, 21);
            this.cmbTolerance.TabIndex = 3;
            this.cmbTolerance.Text = "75000";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(80, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "SimplificationType:";
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Tolerance:";
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
            // PanAroundTheMap
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "PanAroundTheMap";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.UseMapSimplification_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        private GroupBox groupBox1;
        private Button btnSimplify;
        private Label label2;
        private Label label1;
        private ComboBox cmbTolerance;
        private ComboBox cmbSimplificationType;

        #endregion Component Designer generated code
    }
}