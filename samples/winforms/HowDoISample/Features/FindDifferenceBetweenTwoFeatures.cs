using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class FindDifferenceBetweenTwoFeatures : UserControl
    {
        public FindDifferenceBetweenTwoFeatures()
        {
            InitializeComponent();
        }

        private void FindDifferenceBetweenTwoFeatures_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-139.2, 92.4, 120.9, -93.2);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.White);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            // Setup the inMemoryLayer.
            InMemoryFeatureLayer inMemoryLayer = new InMemoryFeatureLayer();
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoSolidBrush(new GeoColor(50, 100, 100, 200)));
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.RoyalBlue;
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            inMemoryLayer.InternalFeatures.Add("AreaShape1", new Feature(new RectangleShape(10, 50, 50, 10).GetWellKnownBinary(), "AreaShape1"));
            inMemoryLayer.InternalFeatures.Add("AreaShape2", new Feature(new RectangleShape(30, 80, 80, 30).GetWellKnownBinary(), "AreaShape2"));

            LayerOverlay inmemoryOverlay = new LayerOverlay();
            inmemoryOverlay.Layers.Add("InMemoryFeatureLayer", inMemoryLayer);
            winformsMap1.Overlays.Add("InmemoryOverlay", inmemoryOverlay);

            winformsMap1.Refresh();
        }

        private void btnDifference_Click(object sender, EventArgs e)
        {
            InMemoryFeatureLayer inMemoryLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("InMemoryFeatureLayer");

            if (inMemoryLayer.InternalFeatures.Count > 1)
            {
                AreaBaseShape sourceShape = (AreaBaseShape)inMemoryLayer.InternalFeatures["AreaShape2"].GetShape();
                AreaBaseShape targetShape = (AreaBaseShape)inMemoryLayer.InternalFeatures["AreaShape1"].GetShape();

                AreaBaseShape resultShape = sourceShape.GetDifference(targetShape);

                inMemoryLayer.InternalFeatures.Clear();
                inMemoryLayer.InternalFeatures.Add("ResultFeature", new Feature(resultShape.GetWellKnownBinary(), "ResultFeature"));
                inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillBrush = new GeoSolidBrush(GeoColor.FromArgb(100, GeoColors.Blue));

                winformsMap1.Refresh(winformsMap1.Overlays["InmemoryOverlay"]);
            }
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;
        private GroupBox groupBox1;
        private MapView winformsMap1;
        private Button btnDifference;

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
            this.btnDifference = new System.Windows.Forms.Button();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.btnDifference);
            this.groupBox1.Location = new System.Drawing.Point(598, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(137, 59);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // btnDifference
            //
            this.btnDifference.Location = new System.Drawing.Point(13, 19);
            this.btnDifference.Name = "btnDifference";
            this.btnDifference.Size = new System.Drawing.Size(118, 23);
            this.btnDifference.TabIndex = 1;
            this.btnDifference.Text = "Difference";
            this.btnDifference.UseVisualStyleBackColor = true;
            this.btnDifference.Click += new System.EventHandler(this.btnDifference_Click);
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 8;
            this.winformsMap1.Text = "winformsMap1";
            //
            // FindDifferenceBetweenTwoFeatures
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "FindDifferenceBetweenTwoFeatures";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.FindDifferenceBetweenTwoFeatures_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}