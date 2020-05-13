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
    public class BuildIndexFile : UserControl
    {
        public BuildIndexFile()
        {
            InitializeComponent();
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            // Bind the source data to data grid
            BindDataGrid();

            // Setup the map unit and set the USA extent
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.CurrentExtent = new RectangleShape(-13914936, 6446276, -7347086, 2632019);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            winformsMap1.Refresh();
        }

        private void btnCreateIndex_Click(object sender, EventArgs e)
        {
            // Define a string list and add all lines of source data into it
            List<string> cityList = new List<string>();
            cityList.AddRange(File.ReadAllLines(Path.Combine(SampleHelper.GetDataPath("CreateIndexFileData\\citystate.txt"))));

            // Sort the key column To perform the matching high efficiently.
            cityList.Sort(delegate (string lineA, string lineB)
            { return string.Compare(lineA.Split('|')[0], lineB.Split('|')[0], StringComparison.OrdinalIgnoreCase); });

            // Create the MatchDbf object and add records into it
            DbfMatchingPlugin cityDbfMatchingPlugIn = CreateCityDbfMatchingPlugIn();
            try
            {
                cityDbfMatchingPlugIn.Open();

                for (int i = 0; i < cityList.Count; i++)
                {
                    string[] parts = cityList[i].Split('|');
                    string city = parts[0];
                    string state = parts[1];
                    int bb_Cx = int.Parse(parts[2]);
                    int bb_Cy = int.Parse(parts[3]);
                    int bb_Ulx = int.Parse(parts[4]);
                    int bb_Uly = int.Parse(parts[5]);
                    int bb_Lrx = int.Parse(parts[6]);
                    int bb_Lry = int.Parse(parts[7]);

                    cityDbfMatchingPlugIn.AddRecord(new object[] { city, state, bb_Cx, bb_Cy, bb_Ulx, bb_Uly, bb_Lrx, bb_Lry });

                    // Show progress of adding
                    prgBuildIndex.Value = (int)(i / (double)cityList.Count * 100);
                    Application.DoEvents();
                }
            }
            finally
            {
                cityDbfMatchingPlugIn.Close();
            }

            prgBuildIndex.Value = 0;
            MessageBox.Show("Build Index Completed", "How Do I Samples", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        private DbfMatchingPlugin CreateCityDbfMatchingPlugIn()
        {
            // Define a Collection of DbfColumnDefinition
            Collection<DbfMatchingPluginColumn> columns = new Collection<DbfMatchingPluginColumn>();

            // Add necessary columns into the collection
            columns.Add(new DbfMatchingPluginColumn("ID_City", DbfMatchingPluginColumnType.String, 54));
            columns.Add(new DbfMatchingPluginColumn("DT_State", DbfMatchingPluginColumnType.String, 2));
            columns.Add(new DbfMatchingPluginColumn("BB_CX", DbfMatchingPluginColumnType.Long, 4));
            columns.Add(new DbfMatchingPluginColumn("BB_CY", DbfMatchingPluginColumnType.Long, 4));
            columns.Add(new DbfMatchingPluginColumn("BB_ULX", DbfMatchingPluginColumnType.Long, 4));
            columns.Add(new DbfMatchingPluginColumn("BB_ULY", DbfMatchingPluginColumnType.Long, 4));
            columns.Add(new DbfMatchingPluginColumn("BB_LRX", DbfMatchingPluginColumnType.Long, 4));
            columns.Add(new DbfMatchingPluginColumn("BB_LRY", DbfMatchingPluginColumnType.Long, 4));

            // Create the MatchDbf object according to the path and the collection of DbfColumnDefinition
            string filePath = Path.Combine(SampleHelper.GetDataPath("CreateIndexFileData\\citystate.dbf"));
            DbfMatchingPlugin.CreateDbf(filePath, columns);
            DbfMatchingPlugin matchingPlugIn = new DbfMatchingPlugin(filePath, DbfMatchingPluginReadWriteMode.ReadWrite);
            return matchingPlugIn;
        }

        private void BindDataGrid()
        {
            dataGridViewDetail.Rows.Clear();
            string sourceDataFile = Path.Combine(SampleHelper.GetDataPath("CreateIndexFileData\\citystate.txt"));
            string[] allLines = File.ReadAllLines(sourceDataFile);
            foreach (string item in allLines)
            {
                dataGridViewDetail.Rows.Add(new object[] { item.Split('|')[0], item.Split('|')[1] });
            }
        }

        #region Component Designer generated code

        private WinformsMap winformsMap1;
        private Panel panel1;
        private OpenFileDialog openFileDialog1;
        private Label lblCoordinate;
        private Button btnCreateIdxFile;
        private GroupBox groupBox1;
        private Label label3;
        private DataGridView dataGridViewDetail;
        private DataGridViewTextBoxColumn columnCity;
        private DataGridViewTextBoxColumn columnState;
        private StatusStrip statusStrip1;
        private ToolStripProgressBar prgBuildIndex;

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
            lblCoordinate = new System.Windows.Forms.Label();
            btnCreateIdxFile = new System.Windows.Forms.Button();
            groupBox1 = new System.Windows.Forms.GroupBox();
            panel1 = new System.Windows.Forms.Panel();
            label3 = new System.Windows.Forms.Label();
            openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            dataGridViewDetail = new System.Windows.Forms.DataGridView();
            columnCity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            columnState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            statusStrip1 = new System.Windows.Forms.StatusStrip();
            prgBuildIndex = new System.Windows.Forms.ToolStripProgressBar();
            groupBox1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGridViewDetail)).BeginInit();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // lblCoordinate
            // 
            lblCoordinate.AutoSize = true;
            lblCoordinate.Location = new System.Drawing.Point(204, 234);
            lblCoordinate.Name = "lblCoordinate";
            lblCoordinate.Size = new System.Drawing.Size(0, 13);
            lblCoordinate.TabIndex = 6;
            lblCoordinate.Visible = false;
            // 
            // btnCreateIdxFile
            // 
            btnCreateIdxFile.Location = new System.Drawing.Point(46, 54);
            btnCreateIdxFile.Name = "btnCreateIdxFile";
            btnCreateIdxFile.Size = new System.Drawing.Size(112, 23);
            btnCreateIdxFile.TabIndex = 3;
            btnCreateIdxFile.Text = "Build Index File";
            btnCreateIdxFile.UseVisualStyleBackColor = true;
            btnCreateIdxFile.Click += new System.EventHandler(btnCreateIndex_Click);
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(panel1);
            groupBox1.Location = new System.Drawing.Point(514, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(223, 116);
            groupBox1.TabIndex = 8;
            groupBox1.TabStop = false;
            groupBox1.Text = "Instructions";
            // 
            // panel1
            // 
            panel1.Controls.Add(label3);
            panel1.Controls.Add(btnCreateIdxFile);
            panel1.Location = new System.Drawing.Point(7, 20);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(213, 90);
            panel1.TabIndex = 0;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(2, 8);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(208, 26);
            label3.TabIndex = 15;
            label3.Text = "Click the button below to build an index file\r\n based on the data in the grid bel" +
                "ow.";
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // winformsMap1
            // 
            winformsMap1.BackColor = System.Drawing.Color.White;
            winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            winformsMap1.CurrentScale = 590591790;
            winformsMap1.DrawingQuality = DrawingQuality.Default;
            winformsMap1.Location = new System.Drawing.Point(0, 0);
            winformsMap1.MapFocusMode = MapFocusMode.Default;
            winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.MaximumScale = 80000000000000;
            winformsMap1.MinimumScale = 200;
            winformsMap1.Name = "winformsMap1";
            winformsMap1.Size = new System.Drawing.Size(508, 503);
            winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            winformsMap1.TabIndex = 9;
            winformsMap1.Text = "winformsMap1";
            winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            winformsMap1.ThreadingMode = MapThreadingMode.Default;
            winformsMap1.ZoomLevelSnapping = ZoomLevelSnappingMode.Default;
            // 
            // dataGridViewDetail
            // 
            dataGridViewDetail.AllowUserToAddRows = false;
            dataGridViewDetail.AllowUserToDeleteRows = false;
            dataGridViewDetail.AllowUserToResizeRows = false;
            dataGridViewDetail.BackgroundColor = System.Drawing.SystemColors.Control;
            dataGridViewDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDetail.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            columnCity,
            columnState});
            dataGridViewDetail.Location = new System.Drawing.Point(514, 122);
            dataGridViewDetail.Name = "dataGridViewDetail";
            dataGridViewDetail.ReadOnly = true;
            dataGridViewDetail.RowHeadersVisible = false;
            dataGridViewDetail.Size = new System.Drawing.Size(223, 381);
            dataGridViewDetail.TabIndex = 12;
            // 
            // columnCity
            // 
            columnCity.HeaderText = "ID_City";
            columnCity.Name = "columnCity";
            columnCity.ReadOnly = true;
            // 
            // columnState
            // 
            columnState.HeaderText = "DT_State";
            columnState.Name = "columnState";
            columnState.ReadOnly = true;
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            prgBuildIndex});
            statusStrip1.Location = new System.Drawing.Point(0, 506);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new System.Drawing.Size(740, 22);
            statusStrip1.TabIndex = 31;
            statusStrip1.Text = "statusStrip1";
            // 
            // prgBuildIndex
            // 
            prgBuildIndex.Name = "prgBuildIndex";
            prgBuildIndex.Size = new System.Drawing.Size(100, 16);
            // 
            // BuildIndexFile
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(statusStrip1);
            Controls.Add(dataGridViewDetail);
            Controls.Add(groupBox1);
            Controls.Add(lblCoordinate);
            Controls.Add(winformsMap1);
            Name = "BuildIndexFile";
            Size = new System.Drawing.Size(740, 528);
            Load += new System.EventHandler(UserControl_Load);
            groupBox1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(dataGridViewDetail)).EndInit();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion


    }
}
