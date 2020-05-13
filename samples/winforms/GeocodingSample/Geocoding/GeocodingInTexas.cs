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
    public partial class GeocodingInTexas : UserControl
    {
        public GeocodingInTexas()
        {
            InitializeComponent();
        }

        private void SearchStreetsInTexas_Load(object sender, EventArgs e)
        {
            // Setup the map unit and set the Texas extent
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.CurrentExtent = new RectangleShape(-9833185, 5250775, -9675419, 5036402);

            // Add ThinkGeoCloudRasterMapsOverlay as basemap
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
            UsaGeocoder usaGeocoder = new UsaGeocoder(dataPath, MatchMode.ExactMatch);
            
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
            lstResult.Items.Clear();
            dataGridViewDetail.Rows.Clear();

            // Load the matching items into the grid
            foreach (GeocoderMatch matchItem in matchResult)
            {
                if (matchItem.NameValuePairs.ContainsKey("Street"))
                {
                    lstResult.Items.Add(new ListItem(matchItem, new string[] { "Street", "Zip", "State" }));
                }
            }

            // If we find addresses then select the first one to zoom in, if not then say we did not find any
            if (lstResult.Items.Count > 0)
            {
                lstResult.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Address can not be found!", "How Do I Samples", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void lstResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Populate the address grid based on the selected address
            GeocoderMatch matchItem = ((ListItem)lstResult.SelectedItem).MatchItem;
            dataGridViewDetail.Rows.Clear();

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
            dataGridViewDetail = new System.Windows.Forms.DataGridView();
            columnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            columnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            cboSourceText = new System.Windows.Forms.ComboBox();
            lstResult = new System.Windows.Forms.ListBox();
            btnSearch = new System.Windows.Forms.Button();
            winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            ((System.ComponentModel.ISupportInitialize)(dataGridViewDetail)).BeginInit();
            SuspendLayout();
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
            dataGridViewDetail.Location = new System.Drawing.Point(522, 141);
            dataGridViewDetail.Name = "dataGridViewDetail";
            dataGridViewDetail.ReadOnly = true;
            dataGridViewDetail.RowHeadersVisible = false;
            dataGridViewDetail.Size = new System.Drawing.Size(215, 384);
            dataGridViewDetail.TabIndex = 10;
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
            // cboSourceText
            // 
            cboSourceText.FormattingEnabled = true;
            cboSourceText.Items.AddRange(new object[] {
            "2500 N Winthrop Ave",
            "1401 W Savannah St",
            "1300 N Finley Rd",
            "8600 W 7th St",
            "7400 W Jefferson St",
            "1101 W Commerce St",
            "3551 Rigsby Ave",
            "1650 Fair Ave",
            "2500 E Sandy Lake Rd",
            "4201 N Claremont Dr",
            "4598 W Fargo Dr",
            "5600 N Bell Ave",
            "2833 North Rd",
            "3051 W Hood St",
            "8130 W Rosemont Dr",
            "2001 N Claremont Dr",
            "8900 Alpha Ave",
            "800 W Small St"});
            cboSourceText.Location = new System.Drawing.Point(522, 2);
            cboSourceText.Name = "cboSourceText";
            cboSourceText.Size = new System.Drawing.Size(134, 21);
            cboSourceText.TabIndex = 9;
            cboSourceText.Text = "2500 N Winthrop Ave";
            // 
            // lstResult
            // 
            lstResult.FormattingEnabled = true;
            lstResult.HorizontalScrollbar = true;
            lstResult.Location = new System.Drawing.Point(522, 28);
            lstResult.Name = "lstResult";
            lstResult.ScrollAlwaysVisible = true;
            lstResult.Size = new System.Drawing.Size(215, 108);
            lstResult.TabIndex = 8;
            lstResult.SelectedIndexChanged += new System.EventHandler(lstResult_SelectedIndexChanged);
            // 
            // btnSearch
            // 
            btnSearch.Location = new System.Drawing.Point(662, 2);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new System.Drawing.Size(75, 23);
            btnSearch.TabIndex = 7;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += new System.EventHandler(btnSearch_Click);
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
            winformsMap1.Size = new System.Drawing.Size(513, 522);
            winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            winformsMap1.TabIndex = 13;
            winformsMap1.Text = "winformsMap1";
            winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            winformsMap1.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
            winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
            // 
            // GeocodingInTexas
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(winformsMap1);
            Controls.Add(dataGridViewDetail);
            Controls.Add(cboSourceText);
            Controls.Add(lstResult);
            Controls.Add(btnSearch);
            Name = "GeocodingInTexas";
            Size = new System.Drawing.Size(740, 528);
            Load += new System.EventHandler(SearchStreetsInTexas_Load);
            ((System.ComponentModel.ISupportInitialize)(dataGridViewDetail)).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private DataGridView dataGridViewDetail;
        private ComboBox cboSourceText;
        private ListBox lstResult;
        private Button btnSearch;

        private DataGridViewTextBoxColumn columnName;
        private DataGridViewTextBoxColumn columnValue;
      

        #endregion
    }
}
