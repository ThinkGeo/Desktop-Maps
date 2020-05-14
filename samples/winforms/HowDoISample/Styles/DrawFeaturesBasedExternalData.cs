using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DrawFeaturesBasedExternalData : UserControl
    {
        public DrawFeaturesBasedExternalData()
        {
            InitializeComponent();
        }

        private void DrawFeaturesBasedExternalData_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            InMemoryFeatureLayer inMemoryLayer = new InMemoryFeatureLayer();
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoSolidBrush(new GeoColor(50, 100, 100, 200)));
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.DarkBlue;
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            inMemoryLayer.InternalFeatures.Add("MapShape", new Feature("POLYGON((-50 -20,-50 20,50 20,50 -20,-50 -20))", "MapShape"));

            LayerOverlay inMemoryOverlay = new LayerOverlay();
            inMemoryOverlay.Layers.Add("InMemoryFeatureLayer", inMemoryLayer);

            winformsMap1.Overlays.Add("InMemoryOverlay", inMemoryOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-139.2, 92.4, 120.9, -93.2);
            winformsMap1.Refresh();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            InMemoryFeatureLayer inMemoryLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("InMemoryFeatureLayer");

            inMemoryLayer.Open();
            inMemoryLayer.EditTools.BeginTransaction();
            inMemoryLayer.EditTools.Update(new Feature(txtWktString.Text, "MapShape"));
            inMemoryLayer.EditTools.CommitTransaction();
            inMemoryLayer.Close();

            winformsMap1.Refresh(winformsMap1.Overlays["InMemoryOverlay"]);
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;
        private GroupBox groupBox1;
        private Button btnChange;
        private MapView winformsMap1;
        private TextBox txtWktString;

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
            this.btnChange = new System.Windows.Forms.Button();
            this.txtWktString = new System.Windows.Forms.TextBox();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnChange);
            this.groupBox1.Controls.Add(this.txtWktString);
            this.groupBox1.Location = new System.Drawing.Point(462, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 78);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Draw Features Based On Wkt";
            //
            // btnChange
            //
            this.btnChange.Location = new System.Drawing.Point(212, 45);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(57, 23);
            this.btnChange.TabIndex = 24;
            this.btnChange.Text = "Change";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            //
            // txtWktString
            //
            this.txtWktString.Location = new System.Drawing.Point(6, 19);
            this.txtWktString.Name = "txtWktString";
            this.txtWktString.ReadOnly = true;
            this.txtWktString.Size = new System.Drawing.Size(263, 20);
            this.txtWktString.TabIndex = 20;
            this.txtWktString.Text = "POLYGON((-50 -20,-50 20,50 20,50 -10,-50 -20))";
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 5;
            this.winformsMap1.Text = "winformsMap1";
            //
            // DrawFeaturesBasedExternalData
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "DrawFeaturesBasedExternalData";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DrawFeaturesBasedExternalData_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}