using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class DisplayAndReprojectWmtsMap : UserControl
    {
        private WmtsLayer wmtsLayer;

        public DisplayAndReprojectWmtsMap()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            wmtsLayer = new WmtsLayer();
            wmtsLayer.WmtsSeverEncodingType = WmtsSeverEncodingType.Kvp;
            wmtsLayer.ServerUris.Add(new Uri("https://basemap.nationalmap.gov/arcgis/rest/services/USGSImageryOnly/MapServer/WMTS"));
            wmtsLayer.ActiveLayerName = "USGSImageryOnly";
            wmtsLayer.ActiveStyleName = "default";
            wmtsLayer.TileMatrixSetName = "GoogleMapsCompatible";
            wmtsLayer.OutputFormat = "image/png";

            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(wmtsLayer);

            mapView.Overlays.Add(layerOverlay);
            mapView.CurrentExtent = new RectangleShape(-9805806.7384, 3642647.7077, -9797096.4156, 3626504.5761);
        }

        private void RadioButtonLight_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBoxProjection.Text == "3857 (Spherical Mercator)")
            {
                mapView.MapUnit = GeographyUnit.Meter;
                mapView.CurrentExtent = new RectangleShape(-9805806.7384, 3642647.7077, -9797096.4156, 3626504.5761); 
                wmtsLayer.ProjectionConverter = null;

            }
            else if(comboBoxProjection.Text == "4326 (Decimal Degrees)")
            {
                mapView.MapUnit = GeographyUnit.DecimalDegree;
                mapView.CurrentExtent = new RectangleShape(-88.1825201012961, 31.1103487417779, -87.9133550622336, 30.9177446768365);
                wmtsLayer.ProjectionConverter = new UnmanagedProjectionConverter(3857, 4326);
                wmtsLayer.ProjectionConverter.Open();

            }
            else if (comboBoxProjection.Text == "26916 (UTM 16N)")
            {
                mapView.MapUnit = GeographyUnit.Meter;
                mapView.CurrentExtent = new RectangleShape(396167.595904146, 3438553.46153289, 403766.768311506, 3424708.06524118);
                wmtsLayer.ProjectionConverter.Open();
            }
            mapView.Refresh();
        }


        #region Component Designer generated code

        private GroupBox groupBox1;
        private ComboBox comboBoxProjection;
        private MapView mapView;


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayThinkGeoCloudVectorMaps));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.comboBoxProjection = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.comboBoxProjection);
            this.groupBox1.Location = new System.Drawing.Point(585, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 43);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Projection";
            //
            // mapView
            //
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.Controls.Add(this.mapView);
            // 
            // comboBoxProjection
            // 
            this.comboBoxProjection.AutoSize = true;
            this.comboBoxProjection.Items.Add("3857 (Spherical Mercator)");
            this.comboBoxProjection.Items.Add("4326 (Decimal Degrees)");
            this.comboBoxProjection.Items.Add("26916 (UTM 16N)");
            this.comboBoxProjection.Location = new System.Drawing.Point(2, 19);
            this.comboBoxProjection.Name = "comboBoxProjection";
            this.comboBoxProjection.Size = new System.Drawing.Size(148, 17);
            this.comboBoxProjection.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxProjection.TabIndex = 14;
            this.comboBoxProjection.TabStop = true;
            this.comboBoxProjection.Text = "3857 (Spherical Mercator)";
            this.comboBoxProjection.SelectedValueChanged += RadioButtonLight_SelectedValueChanged;
            // 
            // DisplayThinkGeoCloudRasterMaps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mapView);
            this.Name = "DisplayThinkGeoCloudRasterMaps";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.Form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion Component Designer generated code
    }
}