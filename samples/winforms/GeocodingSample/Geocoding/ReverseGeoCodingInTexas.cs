/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Geocoding;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace Geocoding
{
    public partial class ReverseGeocodingInTexas : UserControl
    {
        public ReverseGeocodingInTexas()
        {
            InitializeComponent();
        }

        private void ReverseGeoCoder_Load(object sender, EventArgs e)
        {
            // Setup the map unit and set the Texas extent
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.CurrentExtent = new RectangleShape(-9833185, 5250775, -9675419, 5036402);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            // Setup the marker overlay and add it to the map            
            LayerOverlay markerOverlay = new LayerOverlay();
            winformsMap1.Overlays.Add("MarkerOverlay", markerOverlay);

            // Create and add the marker layer to the marker overlay
            MarkerLayer markerLayer = new MarkerLayer();
            markerOverlay.Layers.Add("MarkerLayer", markerLayer);

            winformsMap1.Refresh();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Get the path to the data files and create the Geocoder
            string dataPath = Path.Combine(SampleHelper.GetDataPath("CitystateGeocoding"));
            UsaGeocoder usaGeocoder = new UsaGeocoder(dataPath);

            // Open the geocoder, get any matches and close it
            Collection<GeocoderMatch> matchResult;
            try
            {
                usaGeocoder.Open();
                matchResult = usaGeocoder.Match(cboSourceText.Text);
            }
            finally
            {
                usaGeocoder.Close();
            }

            PopulateAddressResultList(matchResult);
        }

        private void PopulateAddressResultList(Collection<GeocoderMatch> matchResult)
        {
            // Clear the results
            dataGridViewDetail.Rows.Clear();

            // If we find addresses then Load the matching items into the grid and
            // select the first one to zoom in, 
            // if not then say we did not find any
            if (matchResult.Count > 0)
            {
                GeocoderMatch matchItem = matchResult[0];
                foreach (KeyValuePair<string, string> item in matchItem.NameValuePairs)
                {
                    dataGridViewDetail.Rows.Add(new object[] { item.Key, item.Value });
                }

                // Set the marker location to the address selected
                LayerOverlay markerOverlay = winformsMap1.Overlays["MarkerOverlay"] as LayerOverlay;
                MarkerLayer markerLayer = markerOverlay.Layers["MarkerLayer"] as MarkerLayer;

                Proj4Projection proj4Projection = new Proj4Projection(4326, 3857);
                proj4Projection.Open();
                // Get the centroid point from the WKT string in the MatchPair
                if (!string.IsNullOrEmpty(matchItem.NameValuePairs["CentroidPoint"]))
                {
                    var point = new PointShape(matchItem.NameValuePairs["CentroidPoint"]);
                    markerLayer.MarkerLocation = (PointShape)proj4Projection.ConvertToExternalProjection(point);
                }

                // Set the extent that is from the WKT string in the MatchPair and refresh the map
                if (!string.IsNullOrEmpty(matchItem.NameValuePairs["BoundingBox"]))
                {
                    var rectangle = new RectangleShape(matchItem.NameValuePairs["BoundingBox"]);
                    winformsMap1.CurrentExtent = proj4Projection.ConvertToExternalProjection(rectangle);
                }
                winformsMap1.Refresh();

            }
            else
            {
                MessageBox.Show("Address can not be found!", "How Do I Samples", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private WinformsMap winformsMap1;


        #region Component Designer generated code

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cboSourceText = new System.Windows.Forms.ComboBox();
            btnSearch = new System.Windows.Forms.Button();
            dataGridViewDetail = new System.Windows.Forms.DataGridView();
            columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            columnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            ((System.ComponentModel.ISupportInitialize)(dataGridViewDetail)).BeginInit();
            SuspendLayout();
            // 
            // cboSourceText
            // 
            cboSourceText.FormattingEnabled = true;
            cboSourceText.Items.AddRange(new object[] {
            "33.020431 -96.666757",
            "33.017069 -96.672102",
            "33.016106 -96.668558",
            "33.005451 -96.664937",
            "33.011431 -96.678457",
            "33.013912 -96.699847",
            "33.010031 -96.686357",
            "32.999531 -96.686557",
            "32.986931 -96.690957",
            "32.992631 -96.659456",
            "32.995831 -96.672257",
            "32.986831 -96.673356",
            "32.987531 -96.658456",
            "32.980931 -96.670056",
            "32.971931 -96.671256",
            "32.965644 -96.658961",
            "32.958632 -96.653255",
            "32.979431 -96.704057",
            "32.962231 -96.693656"});
            cboSourceText.Location = new System.Drawing.Point(528, 3);
            cboSourceText.Name = "cboSourceText";
            cboSourceText.Size = new System.Drawing.Size(137, 21);
            cboSourceText.TabIndex = 13;
            cboSourceText.Text = "33.020431 -96.666757";
            // 
            // btnSearch
            // 
            btnSearch.Location = new System.Drawing.Point(671, 3);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new System.Drawing.Size(62, 23);
            btnSearch.TabIndex = 7;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += new System.EventHandler(btnSearch_Click);
            // 
            // dataGridViewDetail
            // 
            dataGridViewDetail.AllowUserToAddRows = false;
            dataGridViewDetail.AllowUserToDeleteRows = false;
            dataGridViewDetail.AllowUserToResizeRows = false;
            dataGridViewDetail.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            columnName,
            columnValue});
            dataGridViewDetail.Location = new System.Drawing.Point(528, 32);
            dataGridViewDetail.Name = "dataGridViewDetail";
            dataGridViewDetail.ReadOnly = true;
            dataGridViewDetail.RowHeadersVisible = false;
            dataGridViewDetail.Size = new System.Drawing.Size(205, 496);
            dataGridViewDetail.TabIndex = 14;
            // 
            // columnName
            // 
            columnName.HeaderText = "Name";
            columnName.Name = "columnName";
            columnName.ReadOnly = true;
            // 
            // columnValue
            // 
            columnValue.HeaderText = "Value";
            columnValue.Name = "columnValue";
            columnValue.ReadOnly = true;
            // 
            // winformsMap1
            // 
            winformsMap1.BackColor = System.Drawing.Color.White;
            winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            winformsMap1.CurrentScale = 590591790;
            winformsMap1.DrawingQuality = DrawingQuality.Default;
            winformsMap1.Location = new System.Drawing.Point(3, 3);
            winformsMap1.MapFocusMode = ThinkGeo.MapSuite.WinForms.MapFocusMode.Default;
            winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.MaximumScale = 80000000000000;
            winformsMap1.MinimumScale = 200;
            winformsMap1.Name = "winformsMap1";
            winformsMap1.Size = new System.Drawing.Size(519, 522);
            winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            winformsMap1.TabIndex = 15;
            winformsMap1.Text = "winformsMap1";
            winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            winformsMap1.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
            winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
            // 
            // ReverseGeocodingInTexas
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(winformsMap1);
            Controls.Add(dataGridViewDetail);
            Controls.Add(btnSearch);
            Controls.Add(cboSourceText);
            Name = "ReverseGeocodingInTexas";
            Size = new System.Drawing.Size(740, 528);
            Load += new System.EventHandler(ReverseGeoCoder_Load);
            ((System.ComponentModel.ISupportInitialize)(dataGridViewDetail)).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cboSourceText;
        private DataGridView dataGridViewDetail;
        private DataGridViewTextBoxColumn columnName;
        private DataGridViewTextBoxColumn columnValue;

        #endregion
    }
}
