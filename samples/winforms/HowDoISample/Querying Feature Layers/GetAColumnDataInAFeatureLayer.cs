using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class GetAColumnDataInAFeatureLayer : UserControl
    {
        public GetAColumnDataInAFeatureLayer()
        {
            InitializeComponent();
        }

        private void GetAColumnDataInAFeatureLayer_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-116.18203125000002, 77.671875, 143.97421874999998, -60.4921875);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            PopulateTable();

            winformsMap1.Refresh();
        }

        private void PopulateTable()
        {
            FeatureLayer worldLayer = winformsMap1.FindFeatureLayer("WorldLayer");
            worldLayer.Open();
            Collection<FeatureSourceColumn> allColumns = worldLayer.QueryTools.GetColumns();
            worldLayer.Close();

            dgridFeatures.DataSource = GetDataTableFromFeatureSourceColumns(allColumns);
        }

        private static DataTable GetDataTableFromFeatureSourceColumns(Collection<FeatureSourceColumn> featureSourceColumns)
        {
            DataTable dataTable = new DataTable();
            dataTable.Locale = CultureInfo.InvariantCulture;

            // Setup the column.
            dataTable.Columns.Add("ColumnName");
            dataTable.Columns.Add("MaxLength");
            dataTable.Columns.Add("TypeName");

            foreach (FeatureSourceColumn featureSourceColumn in featureSourceColumns)
            {
                // Add a record.
                DataRow dataRow = dataTable.NewRow();

                dataRow[0] = featureSourceColumn.ColumnName;
                dataRow[1] = featureSourceColumn.MaxLength;
                dataRow[2] = featureSourceColumn.TypeName;

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        #region Component Designer generated code

        private DataGridView dgridFeatures;
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
            this.dgridFeatures = new System.Windows.Forms.DataGridView();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            ((System.ComponentModel.ISupportInitialize)(this.dgridFeatures)).BeginInit();
            this.SuspendLayout();
            //
            // dgridFeatures
            //
            this.dgridFeatures.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgridFeatures.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgridFeatures.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgridFeatures.Location = new System.Drawing.Point(0, 389);
            this.dgridFeatures.Name = "dgridFeatures";
            this.dgridFeatures.ReadOnly = true;
            this.dgridFeatures.Size = new System.Drawing.Size(740, 139);
            this.dgridFeatures.TabIndex = 3;
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 393);
            this.winformsMap1.TabIndex = 4;
            this.winformsMap1.Text = "winformsMap1";
            //
            // GetAColumnDataInAFeatureLayer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgridFeatures);
            this.Controls.Add(this.winformsMap1);
            this.Name = "GetAColumnDataInAFeatureLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.GetAColumnDataInAFeatureLayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgridFeatures)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}