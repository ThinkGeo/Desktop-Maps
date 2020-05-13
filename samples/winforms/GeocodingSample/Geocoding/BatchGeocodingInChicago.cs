using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Geocoding;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace Geocoding
{
    public partial class BatchGeocodingInChicago : UserControl
    {
        private string[] dataSource;

        public BatchGeocodingInChicago()
        {
            InitializeComponent();
        }

        private void BatchGeoCoder_Load(object sender, EventArgs e)
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

            // Load data source.
            LoadDataSource();

            winformsMap1.Refresh();
        }

        private void btnBatchGeoCode_Click(object sender, EventArgs e)
        {
            // Define a Stopwatch to measure the batch duration
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            // Define a int to record the success count of batching Geocoding
            int successCount = 0;

            // Get the path to the data files and create the Geocoder
            string dataPath = Path.Combine(SampleHelper.GetDataPath("ChicagoData"));
            UsaGeocoder usaGeoCoder = new UsaGeocoder(dataPath);

            //Open the geocoder and call the match method then close it
            try
            {
                usaGeoCoder.Open();
                for (int rowIndex = 0; rowIndex < dataSource.Length; rowIndex++)
                {
                    string[] parts = dataSource[rowIndex].Split(',');
                    string address = parts[0];
                    string zip = parts[1];


                    Collection<GeocoderMatch> result = usaGeoCoder.Match(address, zip);
                    if (result.Count > 0)
                    {
                        // If the NameValuePairs contains the key Street it means we found match on street,
                        // then we add it to the result DataGrid.
                        if (result[0].NameValuePairs.ContainsKey("Street"))
                        {
                            successCount++;
                            dataGridViewDetail.Rows[rowIndex].Cells[1].Value = new ListItem(result[0], new string[] { "CentroidPoint" });
                        }
                    }

                    // Show progress of batch geocoding
                    prgBatchGeocoder.Value = (int)((double)rowIndex / (double)dataSource.Length * 100);
                }
            }
            finally
            {
                usaGeoCoder.Close();
            }

            stopWatch.Stop();
            prgBatchGeocoder.Value = 0;

            PopulateBatchResults(successCount, stopWatch.ElapsedMilliseconds);
        }

        private void PopulateBatchResults(int successCount, double duration)
        {
            txtSuccessRate.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.0%}", (double)successCount / (double)dataSource.Length);
            txtTotalTime.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.00 sec}", duration / (double)1000);
            txtTimePerRec.Text = string.Format(CultureInfo.InvariantCulture, "{0:0.00 ms}", duration / (double)successCount);
            txtRecPerSecond.Text = string.Format(CultureInfo.InvariantCulture, "{0:0}", (double)successCount / duration * 1000);

            dataGridViewDetail.Rows[0].Cells[0].Selected = true;
            dataGridViewDetail_CellClick(this, new DataGridViewCellEventArgs(0, 0));
        }

        private void LoadDataSource()
        {
            string dataPath = Path.Combine(SampleHelper.GetDataPath("ChicagoData"));
            dataSource = File.ReadAllLines(Path.Combine(dataPath, "BatchGeocoderData.txt"));
            txtTotalRecordCount.Text = string.Format(CultureInfo.InvariantCulture, "{0:0,0}", dataSource.Length, CultureInfo.InvariantCulture);
            dataGridViewDetail.Rows.Clear();
            foreach (string item in dataSource)
            {
                string address = item.Split(',')[0];
                dataGridViewDetail.Rows.Add(new object[] { address, string.Empty });
            }
            dataGridViewDetail.ClearSelection();
        }

        private void dataGridViewDetail_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Populate the address grid based on the selected address
            if (e.RowIndex == -1)
            {
                return;
            }
            ListItem listItem = dataGridViewDetail.Rows[e.RowIndex].Cells[1].Value as ListItem;
            if (listItem == null)
            {
                return;
            }
            GeocoderMatch matchItem = listItem.MatchItem;

            // Set the marker location to the address selected
            LayerOverlay markerOverlay = winformsMap1.Overlays["MarkerOverlay"] as LayerOverlay;
            MarkerLayer markerLayer = markerOverlay.Layers["MarkerLayer"] as MarkerLayer;
            if (matchItem.NameValuePairs.Count > 0)
            {
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



        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            winformsMap1 = new WinformsMap();
            btnBatchGeoCode = new System.Windows.Forms.Button();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            txtTotalRecordCount = new System.Windows.Forms.TextBox();
            txtSuccessRate = new System.Windows.Forms.TextBox();
            txtTotalTime = new System.Windows.Forms.TextBox();
            txtTimePerRec = new System.Windows.Forms.TextBox();
            txtRecPerSecond = new System.Windows.Forms.TextBox();
            dataGridViewDetail = new System.Windows.Forms.DataGridView();
            columnAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            columnLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            prgBatchGeocoder = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(dataGridViewDetail)).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // winformsMap1
            // 
            winformsMap1.BackColor = System.Drawing.Color.White;
            winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            winformsMap1.CurrentScale = 590591790;
            winformsMap1.DrawingQuality = DrawingQuality.Default;
            winformsMap1.Location = new System.Drawing.Point(3, -22);
            winformsMap1.MapFocusMode = ThinkGeo.MapSuite.WinForms.MapFocusMode.Default;
            winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.MaximumScale = 80000000000000;
            winformsMap1.MinimumScale = 200;
            winformsMap1.Name = "winformsMap1";
            winformsMap1.Size = new System.Drawing.Size(495, 525);
            winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            winformsMap1.TabIndex = 12;
            winformsMap1.Text = "winformsMap1";
            winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            winformsMap1.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
            winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
            // 
            // btnBatchGeoCode
            // 
            btnBatchGeoCode.Location = new System.Drawing.Point(626, 180);
            btnBatchGeoCode.Name = "btnBatchGeoCode";
            btnBatchGeoCode.Size = new System.Drawing.Size(111, 23);
            btnBatchGeoCode.TabIndex = 0;
            btnBatchGeoCode.Text = "Batch GeoCode";
            btnBatchGeoCode.UseVisualStyleBackColor = true;
            btnBatchGeoCode.Click += new System.EventHandler(btnBatchGeoCode_Click);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(506, 16);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(100, 13);
            label1.TabIndex = 20;
            label1.Text = "Total Record Count";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(506, 49);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(74, 13);
            label2.TabIndex = 21;
            label2.Text = "Success Rate";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(506, 82);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(57, 13);
            label3.TabIndex = 22;
            label3.Text = "Total Time";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(506, 115);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(72, 13);
            label4.TabIndex = 23;
            label4.Text = "Time Per Rec";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(506, 148);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(106, 13);
            label5.TabIndex = 24;
            label5.Text = "Records Per Second";
            // 
            // txtTotalRecordCount
            // 
            txtTotalRecordCount.Location = new System.Drawing.Point(626, 16);
            txtTotalRecordCount.Name = "txtTotalRecordCount";
            txtTotalRecordCount.ReadOnly = true;
            txtTotalRecordCount.Size = new System.Drawing.Size(111, 20);
            txtTotalRecordCount.TabIndex = 25;
            txtTotalRecordCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtSuccessRate
            // 
            txtSuccessRate.Location = new System.Drawing.Point(626, 49);
            txtSuccessRate.Name = "txtSuccessRate";
            txtSuccessRate.ReadOnly = true;
            txtSuccessRate.Size = new System.Drawing.Size(111, 20);
            txtSuccessRate.TabIndex = 25;
            txtSuccessRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalTime
            // 
            txtTotalTime.Location = new System.Drawing.Point(626, 82);
            txtTotalTime.Name = "txtTotalTime";
            txtTotalTime.ReadOnly = true;
            txtTotalTime.Size = new System.Drawing.Size(111, 20);
            txtTotalTime.TabIndex = 25;
            txtTotalTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTimePerRec
            // 
            txtTimePerRec.Location = new System.Drawing.Point(626, 115);
            txtTimePerRec.Name = "txtTimePerRec";
            txtTimePerRec.ReadOnly = true;
            txtTimePerRec.Size = new System.Drawing.Size(111, 20);
            txtTimePerRec.TabIndex = 25;
            txtTimePerRec.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtRecPerSecond
            // 
            txtRecPerSecond.Location = new System.Drawing.Point(626, 148);
            txtRecPerSecond.Name = "txtRecPerSecond";
            txtRecPerSecond.ReadOnly = true;
            txtRecPerSecond.Size = new System.Drawing.Size(111, 20);
            txtRecPerSecond.TabIndex = 25;
            txtRecPerSecond.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // dataGridViewDetail
            // 
            dataGridViewDetail.AllowUserToAddRows = false;
            dataGridViewDetail.AllowUserToDeleteRows = false;
            dataGridViewDetail.AllowUserToResizeRows = false;
            dataGridViewDetail.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            columnAddress,
            columnLocation});
            dataGridViewDetail.Location = new System.Drawing.Point(501, 209);
            dataGridViewDetail.Name = "dataGridViewDetail";
            dataGridViewDetail.ReadOnly = true;
            dataGridViewDetail.RowHeadersVisible = false;
            dataGridViewDetail.Size = new System.Drawing.Size(236, 294);
            dataGridViewDetail.TabIndex = 26;
            dataGridViewDetail.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridViewDetail_CellClick);
            // 
            // columnAddress
            // 
            columnAddress.HeaderText = "Address";
            columnAddress.Name = "columnAddress";
            columnAddress.ReadOnly = true;
            // 
            // columnLocation
            // 
            columnLocation.HeaderText = "Location";
            columnLocation.Name = "columnLocation";
            columnLocation.ReadOnly = true;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            prgBatchGeocoder});
            statusStrip1.Location = new System.Drawing.Point(0, 506);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(740, 22);
            statusStrip1.TabIndex = 30;
            statusStrip1.Text = "statusStrip1";
            // 
            // prgBatchGeocoder
            // 
            prgBatchGeocoder.Name = "prgBatchGeocoder";
            prgBatchGeocoder.Size = new System.Drawing.Size(100, 16);
            // 
            // BatchGeocodingInChicago
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(statusStrip1);
            Controls.Add(dataGridViewDetail);
            Controls.Add(txtRecPerSecond);
            Controls.Add(txtTimePerRec);
            Controls.Add(txtTotalTime);
            Controls.Add(txtSuccessRate);
            Controls.Add(txtTotalRecordCount);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnBatchGeoCode);
            Controls.Add(winformsMap1);
            Name = "BatchGeocodingInChicago";
            Size = new System.Drawing.Size(740, 528);
            Load += new System.EventHandler(BatchGeoCoder_Load);
            ((System.ComponentModel.ISupportInitialize)(dataGridViewDetail)).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }
        private ThinkGeo.MapSuite.WinForms.WinformsMap winformsMap1;
        private Button btnBatchGeoCode;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox txtTotalRecordCount;
        private TextBox txtSuccessRate;
        private TextBox txtTotalTime;
        private TextBox txtTimePerRec;
        private TextBox txtRecPerSecond;
        private DataGridView dataGridViewDetail;
        private DataGridViewTextBoxColumn columnAddress;
        private DataGridViewTextBoxColumn columnLocation;
        private StatusStrip statusStrip1;
        private ToolStripProgressBar prgBatchGeocoder;

        #endregion

    }
}
