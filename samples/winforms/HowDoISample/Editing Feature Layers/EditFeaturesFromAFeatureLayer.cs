using System;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class EditFeaturesFromAFeatureLayer : UserControl
    {
        public EditFeaturesFromAFeatureLayer()
        {
            InitializeComponent();
        }

        private void ModifyFeature_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;

            winformsMap1.CurrentExtent = new RectangleShape(0, 100, 100, 0);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.White);

            InMemoryFeatureLayer inMemoryLayer = new InMemoryFeatureLayer();
            inMemoryLayer.InternalFeatures.Add("Polygon", new Feature(BaseShape.CreateShapeFromWellKnownData("POLYGON((10 60,40 70,30 85, 10 60))")));
            inMemoryLayer.InternalFeatures.Add("Multipoint", new Feature(BaseShape.CreateShapeFromWellKnownData("MULTIPOINT(10 20, 30 20,40 20, 10 30, 30 30, 40 30)")));
            inMemoryLayer.InternalFeatures.Add("Line", new Feature(BaseShape.CreateShapeFromWellKnownData("LINESTRING(60 60, 70 70,75 60, 80 70, 85 60,95 80)")));
            inMemoryLayer.InternalFeatures.Add("Rectangle", new Feature(new RectangleShape(65, 30, 95, 15)));

            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillBrush = new GeoSolidBrush(GeoColor.FromArgb(100, GeoColors.RoyalBlue));
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.Blue;
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle.OuterPen = new GeoPen(GeoColor.FromArgb(200, GeoColors.Red), 5);
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle.OutlinePen = new GeoPen(GeoColor.FromArgb(255, GeoColors.Green), 8);
            inMemoryLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("InMemoryFeatureLayer", inMemoryLayer);
            winformsMap1.Overlays.Add("InmemoryOverlay", staticOverlay);

            winformsMap1.Refresh();
        }

        private void EditAFeatureButton_Click(object sender, EventArgs e)
        {
            InMemoryFeatureLayer inMemoryLayer = (InMemoryFeatureLayer)winformsMap1.FindFeatureLayer("InMemoryFeatureLayer");

            inMemoryLayer.Open();
            inMemoryLayer.EditTools.BeginTransaction();
            inMemoryLayer.EditTools.Update(new Feature("POLYGON((10 60,40 70,30 85,20 90,10 60))", "Polygon"));
            inMemoryLayer.EditTools.CommitTransaction();
            inMemoryLayer.Close();

            winformsMap1.Refresh(winformsMap1.Overlays["InmemoryOverlay"]);
        }

        #region Component Designer generated code

        private GroupBox groupBox1;
        private MapView winformsMap1;
        private Button EditAFeatureButton;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EditAFeatureButton = new System.Windows.Forms.Button();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.EditAFeatureButton);
            this.groupBox1.Location = new System.Drawing.Point(590, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(147, 58);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // EditAFeatureButton
            //
            this.EditAFeatureButton.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.EditAFeatureButton.Location = new System.Drawing.Point(16, 19);
            this.EditAFeatureButton.Name = "EditAFeatureButton";
            this.EditAFeatureButton.Size = new System.Drawing.Size(115, 23);
            this.EditAFeatureButton.TabIndex = 1;
            this.EditAFeatureButton.Text = "Edit A Feature";
            this.EditAFeatureButton.UseVisualStyleBackColor = true;
            this.EditAFeatureButton.Click += new System.EventHandler(this.EditAFeatureButton_Click);
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 13;
            this.winformsMap1.Text = "winformsMap1";
            //
            // EditFeaturesFromAFeatureLayer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "EditFeaturesFromAFeatureLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.ModifyFeature_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        /// <summary>
        /// Required designer variable.
        /// </summary>
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

        #endregion Component Designer generated code
    }
}