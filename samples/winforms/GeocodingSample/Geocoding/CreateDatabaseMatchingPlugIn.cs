/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
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
    public partial class CreateDatabaseMatchingPlugIn : UserControl
    {
        public CreateDatabaseMatchingPlugIn()
        {
            InitializeComponent();
        }       

        private void AccessMatchingPlugInSample_Load(object sender, EventArgs e)
        {
            // Setting up the map unit and set the USA extent
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
            string dataPath = Path.Combine(SampleHelper.GetDataPath("AccessMatchingPlugin\\County.mdb"));
            
            // Create a generic geocoder that will house the match plugin
            Geocoder geoCoder = new Geocoder(Path.Combine(SampleHelper.GetDataPath("AccessMatchingPlugin")));

            // Create our Access matching plugin and add it to the geocoder
            AccessMatchingPlugIn accessMatchingPlugin = new AccessMatchingPlugIn("county", dataPath);
            geoCoder.MatchingPlugIns.Add(accessMatchingPlugin);

            //Open the geocoder and call the match method then close it
            Collection<GeocoderMatch> matchResult;
            try
            {                
                geoCoder.Open();
                matchResult = geoCoder.Match(cboSourceText.Text);
            }
            finally
            {
                geoCoder.Close();
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
                    lstResult.Items.Add(new ListItem(matchItem, new string[]{"County","State"}));
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
            dataGridViewDetail = new System.Windows.Forms.DataGridView();
            lstResult = new System.Windows.Forms.ListBox();
            btnSearch = new System.Windows.Forms.Button();
            cboSourceText = new System.Windows.Forms.ComboBox();
            label3 = new System.Windows.Forms.Label();
            gbInstructions = new System.Windows.Forms.GroupBox();
            label4 = new System.Windows.Forms.Label();
            fieldName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            fieldValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            winformsMap1.Size = new System.Drawing.Size(540, 522);
            winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            winformsMap1.TabIndex = 6;
            winformsMap1.Text = "winformsMap1";
            winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            winformsMap1.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
            winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
            // 
            // dataGridViewDetail
            // 
            dataGridViewDetail.AllowUserToAddRows = false;
            dataGridViewDetail.AllowUserToDeleteRows = false;
            dataGridViewDetail.AllowUserToResizeRows = false;
            dataGridViewDetail.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            fieldName,
            fieldValue});
            dataGridViewDetail.Location = new System.Drawing.Point(546, 350);
            dataGridViewDetail.Name = "dataGridViewDetail";
            dataGridViewDetail.ReadOnly = true;
            dataGridViewDetail.RowHeadersVisible = false;
            dataGridViewDetail.Size = new System.Drawing.Size(191, 175);
            dataGridViewDetail.TabIndex = 13;
            // 
            // lstResult
            // 
            lstResult.FormattingEnabled = true;
            lstResult.HorizontalScrollbar = true;
            lstResult.Location = new System.Drawing.Point(546, 110);
            lstResult.Name = "lstResult";
            lstResult.ScrollAlwaysVisible = true;
            lstResult.Size = new System.Drawing.Size(191, 238);
            lstResult.TabIndex = 17;
            lstResult.SelectedIndexChanged += new System.EventHandler(lstResult_SelectedIndexChanged);
            // 
            // btnSearch
            // 
            btnSearch.Location = new System.Drawing.Point(105, 65);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new System.Drawing.Size(66, 23);
            btnSearch.TabIndex = 7;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += new System.EventHandler(btnSearch_Click);
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
            cboSourceText.Location = new System.Drawing.Point(5, 67);
            cboSourceText.Name = "cboSourceText";
            cboSourceText.Size = new System.Drawing.Size(89, 21);
            cboSourceText.TabIndex = 12;
            cboSourceText.Text = "Union County";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(2, 16);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(0, 13);
            label3.TabIndex = 16;
            // 
            // gbInstructions
            // 
            gbInstructions.Controls.Add(label4);
            gbInstructions.Controls.Add(label3);
            gbInstructions.Controls.Add(cboSourceText);
            gbInstructions.Controls.Add(btnSearch);
            gbInstructions.Location = new System.Drawing.Point(546, 4);
            gbInstructions.Name = "gbInstructions";
            gbInstructions.Size = new System.Drawing.Size(191, 104);
            gbInstructions.TabIndex = 18;
            gbInstructions.TabStop = false;
            gbInstructions.Text = "Instructions";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(6, 16);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(178, 39);
            label4.TabIndex = 17;
            label4.Text = "Select or type in a USA county. We\r\nwill match that county name in a text\r\nfile u" +
                "sing a custom MatchPlugin.";
            // 
            // fieldName
            // 
            fieldName.HeaderText = "Name";
            fieldName.Name = "fieldName";
            fieldName.ReadOnly = true;
            // 
            // fieldValue
            // 
            fieldValue.HeaderText = "Value";
            fieldValue.Name = "fieldValue";
            fieldValue.ReadOnly = true;
            // 
            // CreateDatabaseMatchingPlugIn
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(gbInstructions);
            Controls.Add(lstResult);
            Controls.Add(dataGridViewDetail);
            Controls.Add(winformsMap1);
            Name = "CreateDatabaseMatchingPlugIn";
            Size = new System.Drawing.Size(740, 528);
            Load += new System.EventHandler(AccessMatchingPlugInSample_Load);
            ((System.ComponentModel.ISupportInitialize)(dataGridViewDetail)).EndInit();
            gbInstructions.ResumeLayout(false);
            gbInstructions.PerformLayout();
            ResumeLayout(false);

        }

        #endregion

        private ThinkGeo.MapSuite.WinForms.WinformsMap winformsMap1;
        private DataGridView dataGridViewDetail;
        private ListBox lstResult;
        private Button btnSearch;
        private ComboBox cboSourceText;
        private Label label3;
        private GroupBox gbInstructions;
        private Label label4;
        private DataGridViewTextBoxColumn fieldName;
        private DataGridViewTextBoxColumn fieldValue;

        #endregion
    }

    public class AccessMatchingPlugIn : MatchingPlugin, IDisposable
    {
        private string pathAndFileName;
        private string tableName;
        private string connectionString;
        private OleDbConnection oleDbConnection;
       
        public AccessMatchingPlugIn() :
            this(string.Empty, string.Empty)
        {
        }

        public AccessMatchingPlugIn(string tableName, string pathFileName)
        {
            pathAndFileName = pathFileName;
            this.tableName = tableName;
            connectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathFileName + @";User Id=admin;Password=;";
           
        }

        public string TableName
        {
            get
            {
                return tableName;
            }
            set
            {
                tableName = value;
            }
        }

        public string AccessConnectionString
        {
            get
            {
                return connectionString;
            }
            set
            {
                connectionString = value;
            }
        }

        public string PathFileName
        {
            get
            {
                return pathAndFileName;
            }
            set
            {
                pathAndFileName = value;
            }
        }

        protected override void OpenCore()
        {
            oleDbConnection = new OleDbConnection(connectionString);
            oleDbConnection.Open();
        }

        protected override void CloseCore()
        {
            oleDbConnection.Close();
        }

        protected override Collection<GeocoderMatch> MatchCore(string sourceText)
        {
            string str = "select * from " + tableName + " where county ='" + sourceText + "'";
            if (oleDbConnection.State == ConnectionState.Closed)
            {
                oleDbConnection.Open();
            }
            Collection<GeocoderMatch> matchItems = new Collection<GeocoderMatch>();
            OleDbCommand oleCmd = new OleDbCommand(str, oleDbConnection);
            oleCmd.CommandTimeout = int.MaxValue;
            OleDbDataReader oleReader = oleCmd.ExecuteReader();
            while (oleReader.Read())
            {               
                Dictionary<string, string> values = new Dictionary<string, string>();
                values.Add("County", oleReader["county"].ToString());
                values.Add("State", oleReader["State"].ToString());
                values.Add("CentroidPoint", oleReader["CentroidPoint"].ToString());
                values.Add("BoundingBox", oleReader["BoundingBox"].ToString());
                matchItems.Add(new GeocoderMatch(values));
            }
            return matchItems;
        }


        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (oleDbConnection != null)
                {
                    oleDbConnection.Dispose();
                }
            }
        }

        #endregion
    }
}
