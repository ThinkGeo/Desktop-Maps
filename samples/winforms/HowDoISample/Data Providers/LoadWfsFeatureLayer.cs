using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class LoadWfsFeatureLayer : UserControl
    {
        public LoadWfsFeatureLayer()
        {
            InitializeComponent();
        }

        private void LoadWfsFeatureLayer_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            //WfsFeatureLayer wfsFeatureLayer = new WfsFeatureLayer(@"http://giswebservices.massgis.state.ma.us/geoserver/wfs?request=GetFeature", "massgis:TOWNS_POLYM");
            WfsFeatureLayer wfsFeatureLayer = new WfsFeatureLayer(@"http://demo.boundlessgeo.com/geoserver/wfs", "topp:states");
            wfsFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 156, 155, 154), 2);
            wfsFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WfsFeatureLayer", wfsFeatureLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.CurrentExtent = MapUtil.GetDrawingExtent(new RectangleShape(-124.731, 49.3717, -66.9698, 24.956), winformsMap1.Width, winformsMap1.Height);
            winformsMap1.Refresh();
        }

        private void btnGetAllFeatures_Click(object sender, EventArgs e)
        {
            Collection<string> returningColumns = new Collection<string>();

            FeatureLayer wfsFeatureLayer = winformsMap1.FindFeatureLayer("WfsFeatureLayer");
            wfsFeatureLayer.Open();
            Collection<FeatureSourceColumn> columns = wfsFeatureLayer.FeatureSource.GetColumns();
            foreach (FeatureSourceColumn item in columns)
            {
                returningColumns.Add(item.ColumnName);
            }
            Collection<Feature> allFeaturs = wfsFeatureLayer.FeatureSource.GetAllFeatures(returningColumns);
            wfsFeatureLayer.Close();

            dgridFeatures.DataSource = (DataTable)FeatureSource.ConvertToDataTable(allFeaturs, returningColumns);
        }

        #region Component Designer generated code

        private DataGridView dgridFeatures;
        private Button btnGetAllFeatures;
        private MapView winformsMap1;
        private System.ComponentModel.IContainer components = null;
        private Label lblDescription;

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
            this.dgridFeatures = new System.Windows.Forms.DataGridView();
            this.btnGetAllFeatures = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            ((System.ComponentModel.ISupportInitialize)(this.dgridFeatures)).BeginInit();
            this.SuspendLayout();
            // 
            // dgridFeatures
            // 
            this.dgridFeatures.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgridFeatures.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridFeatures.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgridFeatures.Location = new System.Drawing.Point(0, 334);
            this.dgridFeatures.Name = "dgridFeatures";
            this.dgridFeatures.ReadOnly = true;
            this.dgridFeatures.Size = new System.Drawing.Size(740, 194);
            this.dgridFeatures.TabIndex = 2;
            // 
            // btnGetAllFeatures
            // 
            this.btnGetAllFeatures.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGetAllFeatures.Location = new System.Drawing.Point(621, 305);
            this.btnGetAllFeatures.Name = "btnGetAllFeatures";
            this.btnGetAllFeatures.Size = new System.Drawing.Size(116, 23);
            this.btnGetAllFeatures.TabIndex = 34;
            this.btnGetAllFeatures.Text = "Get All  Features";
            this.btnGetAllFeatures.UseVisualStyleBackColor = true;
            this.btnGetAllFeatures.Click += new System.EventHandler(this.btnGetAllFeatures_Click);
            // 
            // lblDescription
            // 
            this.lblDescription.BackColor = System.Drawing.Color.LightYellow;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblDescription.Location = new System.Drawing.Point(3, 5);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(277, 67);
            this.lblDescription.TabIndex = 35;
            this.lblDescription.Text = "The current service is from http://demo.boundlessgeo.com. You can use the GetAllF" +
    "eatures button to retrive data from the website.";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // winformsMap1
            // 
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CurrentScale = 590591790D;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000D;
            this.winformsMap1.MinimumScale = 100D;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 301);
            this.winformsMap1.TabIndex = 35;
            this.winformsMap1.Text = "winformsMap1";
            // 
            // LoadWfsFeatureLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.winformsMap1);
            this.Controls.Add(this.btnGetAllFeatures);
            this.Controls.Add(this.dgridFeatures);
            this.Name = "LoadWfsFeatureLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.LoadWfsFeatureLayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridFeatures)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}