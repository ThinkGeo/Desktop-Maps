using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Geocoding;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace Geocoding
{
    public partial class ReverseGeocodingInChicago : UserControl
    {
        public ReverseGeocodingInChicago()
        {
            InitializeComponent();
        }

        private void ReverseGeoCoder_Load(object sender, EventArgs e)
        {
            // Setup the map unit and set the Chicago extent
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-88.3330001640625, 42.5966329101563, -86.9157638359375, 41.1629170898438);

            // Setup the World Map Kit WMS Overlay
            WorldMapKitWmsDesktopOverlay worldMapKitOverlay = new WorldMapKitWmsDesktopOverlay();
            winformsMap1.Overlays.Add("WorldMapKitOverlay", worldMapKitOverlay);

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
            string dataPath = Path.Combine(SampleHelper.GetDataPath("ChicagoData"));
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
                // Get the centroid point from the WKT string in the MatchPair
                if (!string.IsNullOrEmpty(matchItem.NameValuePairs["CentroidPoint"]))
                {
                    markerLayer.MarkerLocation = new PointShape(matchItem.NameValuePairs["CentroidPoint"]);
                }

                // Set the extent that is from the WKT string in the MatchPair and refresh the map
                if (!string.IsNullOrEmpty(matchItem.NameValuePairs["BoundingBox"]))
                {
                    winformsMap1.CurrentExtent = new RectangleShape(matchItem.NameValuePairs["BoundingBox"]);
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
            "42.020431 -87.666757",
            "42.017069 -87.672102",
            "42.016106 -87.668558",
            "42.005451 -87.664937",
            "42.011431 -87.678457",
            "42.013912 -87.699847",
            "42.010031 -87.686357",
            "41.999531 -87.686557",
            "41.986931 -87.690957",
            "41.992631 -87.659456",
            "41.995831 -87.672257",
            "41.986831 -87.673356",
            "41.987531 -87.658456",
            "41.980931 -87.670056",
            "41.971931 -87.671256",
            "41.965644 -87.658961",
            "41.958632 -87.653255",
            "41.979431 -87.704057",
            "41.962231 -87.693656"});
            cboSourceText.Location = new System.Drawing.Point(528, 3);
            cboSourceText.Name = "cboSourceText";
            cboSourceText.Size = new System.Drawing.Size(137, 21);
            cboSourceText.TabIndex = 13;
            cboSourceText.Text = "42.020431 -87.666757";
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
            // ReverseGeocodingInChicago
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(winformsMap1);
            Controls.Add(dataGridViewDetail);
            Controls.Add(btnSearch);
            Controls.Add(cboSourceText);
            Name = "ReverseGeocodingInChicago";
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
