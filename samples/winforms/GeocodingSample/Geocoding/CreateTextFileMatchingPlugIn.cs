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

    public partial class CreateTextFileMatchingPlugIn : UserControl
    {
        public CreateTextFileMatchingPlugIn()
        {
            InitializeComponent();
        }

        private void CreateTextMatchingPlugIn_Load(object sender, EventArgs e)
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
            string dataPath = Path.Combine(SampleHelper.GetDataPath("TextMatchingPlugin\\county.txt"));

            // Create a generic geocoder that will house the match plugin
            Geocoder geocoder = new Geocoder(Path.Combine(SampleHelper.GetDataPath("TextMatchingPlugin")));

            // Create our Text matching plugin and add it to the geocoder
            TextMatchingPlugIn txtMatchingPlugin = new TextMatchingPlugIn(dataPath);
            geocoder.MatchingPlugIns.Add(txtMatchingPlugin);

            //Open the geocoder and call the match method then close it
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
            dataGridViewDetail.Rows.Clear();

            // Load the matching items into the grid
            foreach (GeocoderMatch matchItem in matchResult)
            {
                if (matchItem.NameValuePairs.ContainsKey("County") && matchItem.NameValuePairs.ContainsKey("State"))
                {
                    lstResult.Items.Add(new ListItem(matchItem, new string[] { "County", "State" }));
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
            winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            cboSourceText = new System.Windows.Forms.ComboBox();
            btnSearch = new System.Windows.Forms.Button();
            dataGridViewDetail = new System.Windows.Forms.DataGridView();
            ColumnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ColumnValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            lstResult = new System.Windows.Forms.ListBox();
            gbInstructions = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(dataGridViewDetail)).BeginInit();
            gbInstructions.SuspendLayout();
            SuspendLayout();
            // 
            // winformsMap1
            // 
            winformsMap1.BackColor = System.Drawing.Color.White;
            winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            winformsMap1.CurrentScale = 590591790;
            winformsMap1.DrawingQuality = DrawingQuality.Default;
            winformsMap1.Location = new System.Drawing.Point(0, 3);
            winformsMap1.MapFocusMode = ThinkGeo.MapSuite.WinForms.MapFocusMode.Default;
            winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.MaximumScale = 80000000000000;
            winformsMap1.MinimumScale = 200;
            winformsMap1.Name = "winformsMap1";
            winformsMap1.Size = new System.Drawing.Size(537, 522);
            winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            winformsMap1.TabIndex = 6;
            winformsMap1.Text = "winformsMap1";
            winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            winformsMap1.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
            winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
            // 
            // cboSourceText
            // 
            cboSourceText.FormattingEnabled = true;
            cboSourceText.Items.AddRange(new object[] {
            "Union County",
            "Van Buren County",
            "Kiowa County",
            "Logan County",
            "Anderson County",
            "Pawnee County",
            "Wallace County",
            "Saline County",
            "Ballard County",
            "Breckinridge County",
            "Fulton County",
            "Linn County",
            "Daviess County",
            "Grant County",
            "Green County",
            "Hancock County",
            "Graves County",
            "Hickman County",
            "Harrison County",
            "Jackson County",
            "Johnson County",
            "Larue County",
            "Laurel County",
            "Lee County",
            "Lawrence County",
            "Kenton County",
            "Lincoln County",
            "Owsley County",
            "Magoffin County",
            "Robertson County",
            "Marion County",
            "Pendleton County",
            "Perry County",
            "Rockcastle County",
            "Taylor County",
            "Todd County",
            "Scott County",
            "Tensas County",
            "Clarke County",
            "Ionia County"});
            cboSourceText.Location = new System.Drawing.Point(17, 68);
            cboSourceText.Name = "cboSourceText";
            cboSourceText.Size = new System.Drawing.Size(96, 21);
            cboSourceText.TabIndex = 11;
            cboSourceText.Text = "Union County";
            // 
            // btnSearch
            // 
            btnSearch.Location = new System.Drawing.Point(119, 66);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new System.Drawing.Size(64, 23);
            btnSearch.TabIndex = 10;
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
            ColumnName,
            ColumnValue});
            dataGridViewDetail.Location = new System.Drawing.Point(543, 359);
            dataGridViewDetail.Name = "dataGridViewDetail";
            dataGridViewDetail.ReadOnly = true;
            dataGridViewDetail.RowHeadersVisible = false;
            dataGridViewDetail.Size = new System.Drawing.Size(191, 166);
            dataGridViewDetail.TabIndex = 15;
            // 
            // ColumnName
            // 
            ColumnName.HeaderText = "Name";
            ColumnName.Name = "ColumnName";
            ColumnName.ReadOnly = true;
            // 
            // ColumnValue
            // 
            ColumnValue.HeaderText = "Value";
            ColumnValue.Name = "ColumnValue";
            ColumnValue.ReadOnly = true;
            // 
            // lstResult
            // 
            lstResult.FormattingEnabled = true;
            lstResult.HorizontalScrollbar = true;
            lstResult.Location = new System.Drawing.Point(543, 115);
            lstResult.Name = "lstResult";
            lstResult.ScrollAlwaysVisible = true;
            lstResult.Size = new System.Drawing.Size(191, 238);
            lstResult.TabIndex = 16;
            lstResult.SelectedIndexChanged += new System.EventHandler(lstResult_SelectedIndexChanged);
            // 
            // gbInstructions
            // 
            gbInstructions.Controls.Add(label4);
            gbInstructions.Controls.Add(cboSourceText);
            gbInstructions.Controls.Add(btnSearch);
            gbInstructions.Location = new System.Drawing.Point(543, 5);
            gbInstructions.Name = "gbInstructions";
            gbInstructions.Size = new System.Drawing.Size(191, 104);
            gbInstructions.TabIndex = 19;
            gbInstructions.TabStop = false;
            gbInstructions.Text = "Instructions";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(5, 15);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(178, 39);
            label4.TabIndex = 18;
            label4.Text = "Select or type in a USA county. We\r\nwill match that county name in a text\r\nfile u" +
                "sing a custom MatchPlugin.";
            // 
            // CreateTextFileMatchingPlugIn
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gbInstructions);
            Controls.Add(lstResult);
            Controls.Add(dataGridViewDetail);
            Controls.Add(winformsMap1);
            Name = "CreateTextFileMatchingPlugIn";
            Size = new System.Drawing.Size(740, 528);
            Load += new System.EventHandler(CreateTextMatchingPlugIn_Load);
            ((System.ComponentModel.ISupportInitialize)(dataGridViewDetail)).EndInit();
            gbInstructions.ResumeLayout(false);
            gbInstructions.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private ThinkGeo.MapSuite.WinForms.WinformsMap winformsMap1;
        private System.Windows.Forms.ComboBox cboSourceText;
        private System.Windows.Forms.Button btnSearch;
        private DataGridView dataGridViewDetail;
        private ListBox lstResult;
        private GroupBox gbInstructions;
        private Label label4;

        private DataGridViewTextBoxColumn ColumnName;
        private DataGridViewTextBoxColumn ColumnValue;


        #endregion
    }

    public class TextMatchingPlugIn : MatchingPlugin
    {
        private string pathFileName;

        public TextMatchingPlugIn()
            : this(string.Empty)
        {
        }

        public TextMatchingPlugIn(string pathFileName)
        {
            this.pathFileName = pathFileName;
        }

        protected override Collection<GeocoderMatch> MatchCore(string sourceText)
        {
            Collection<GeocoderMatch> matchItems = new Collection<GeocoderMatch>();
            string[] allLines = File.ReadAllLines(pathFileName);
            foreach (string item in allLines)
            {
                if (string.Compare(sourceText, item.Split('|')[0], StringComparison.CurrentCultureIgnoreCase) == 0)
                {
                    GeocoderMatch matchItem = new GeocoderMatch();
                    matchItem.NameValuePairs.Add("County", item.Split('|')[0]);
                    matchItem.NameValuePairs.Add("State", item.Split('|')[1]);
                    matchItem.NameValuePairs.Add("CentroidPoint", item.Split('|')[2]);
                    matchItem.NameValuePairs.Add("BoundingBox", item.Split('|')[3]);
                    matchItems.Add(matchItem);
                }
            }
            return matchItems;
        }
    }
}
