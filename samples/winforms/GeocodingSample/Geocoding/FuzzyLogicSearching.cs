/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
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
    public partial class FuzzyLogicSearching : UserControl
    {
        public FuzzyLogicSearching()
        {
            InitializeComponent();
        }

        private void FuzzyCitySearch_Load(object sender, EventArgs e)
        {
            // Setup the map unit and set the USA extent
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.CurrentExtent = new RectangleShape(-13914936, 6446276, -7347086, 2632019);

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
            Geocoder geocoder = new Geocoder(dataPath);

            // Create a SoundExDbfMatchingPlugIn and add it to the geocoder
            geocoder.MatchingPlugIns.Add(new SoundexDbfMatchingPlugin(Path.Combine(dataPath, "SoundExCityState.dbf"), "DT_City"));

            // Open the geocoder, get any matches and close it
            Collection<GeocoderMatch> matchResult;
            try
            {
                geocoder.Open();
                matchResult = geocoder.Match(cboSourceText.Text);
            }
            finally
            {
                geocoder.Close();
            }

            PopulateAddressResultList(matchResult);
        }

        private void PopulateAddressResultList(Collection<GeocoderMatch> matchResult)
        {
            // Clear the results
            lstResult.Items.Clear();

            // Load the matching items into the grid
            foreach (GeocoderMatch matchItem in matchResult)
            {
                lstResult.Items.Add(new ListItem(matchItem, new string[] { "DT_City", "DT_State" }));
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

            // Set the marker location to the address selected
            LayerOverlay mainOverlay = winformsMap1.Overlays["MarkerOverlay"] as LayerOverlay;
            MarkerLayer markerLayer = mainOverlay.Layers["MarkerLayer"] as MarkerLayer;

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
            lstResult = new System.Windows.Forms.ListBox();
            btnSearch = new System.Windows.Forms.Button();
            cboSourceText = new System.Windows.Forms.ComboBox();
            winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            SuspendLayout();
            // 
            // lstResult
            // 
            lstResult.FormattingEnabled = true;
            lstResult.HorizontalScrollbar = true;
            lstResult.Location = new System.Drawing.Point(565, 28);
            lstResult.Name = "lstResult";
            lstResult.ScrollAlwaysVisible = true;
            lstResult.Size = new System.Drawing.Size(172, 498);
            lstResult.TabIndex = 4;
            lstResult.SelectedIndexChanged += new System.EventHandler(lstResult_SelectedIndexChanged);
            // 
            // btnSearch
            // 
            btnSearch.Location = new System.Drawing.Point(662, 3);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new System.Drawing.Size(75, 23);
            btnSearch.TabIndex = 8;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += new System.EventHandler(btnSearch_Click);
            // 
            // cboSourceText
            // 
            cboSourceText.FormattingEnabled = true;
            cboSourceText.Items.AddRange(new object[] {
            "New York",
            "New Yorke",
            "Philadelphia",
            "Philadelphi",
            "Chicago",
            "Chicagoe",
            "Houston",
            "Hoston",
            "San Diego",
            "San Digo",
            "Dallas",
            "Dalas",
            "Miami",
            "Miamii",
            "Detroit",
            "Detrot",
            "Buffalo",
            "Bufalo",
            "Cleveland",
            "Clevland",
            "Naples",
            "Napls",
            "Pittsburgh",
            "Pitsburgh",
            "Seattle",
            "Seatle",
            "Los Angeles",
            "Los Angles",
            "Baltimore",
            "Baltimor",
            "Brooklyn",
            "Broklyn",
            "Memphis",
            "Menphis",
            "Phoenix",
            "Phonix",
            "Columbus",
            "Colunbus",
            "Boston",
            "Bostn",
            "Rochester",
            "Roechester",
            "San Jose",
            "San Jos",
            "Jacksonville",
            "Jacksonvile"});
            cboSourceText.Location = new System.Drawing.Point(565, 5);
            cboSourceText.Name = "cboSourceText";
            cboSourceText.Size = new System.Drawing.Size(91, 21);
            cboSourceText.TabIndex = 9;
            cboSourceText.Text = "New York";
            // 
            // winformsMap1
            // 
            winformsMap1.BackColor = System.Drawing.Color.White;
            winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            winformsMap1.CurrentScale = 590591790;
            winformsMap1.DrawingQuality = DrawingQuality.Default;
            winformsMap1.Location = new System.Drawing.Point(0, 5);
            winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.MaximumScale = 80000000000000;
            winformsMap1.MinimumScale = 200;
            winformsMap1.Name = "winformsMap1";
            winformsMap1.Size = new System.Drawing.Size(559, 520);
            winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            winformsMap1.TabIndex = 10;
            winformsMap1.Text = "winformsMap1";
            winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            winformsMap1.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
            winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
            // 
            // FuzzyCitySearch
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(winformsMap1);
            Controls.Add(btnSearch);
            Controls.Add(cboSourceText);
            Controls.Add(lstResult);
            Name = "FuzzyCitySearch";
            Size = new System.Drawing.Size(740, 528);
            Load += new System.EventHandler(FuzzyCitySearch_Load);
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstResult;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cboSourceText;
        private WinformsMap winformsMap1;

        #endregion
    }
}
