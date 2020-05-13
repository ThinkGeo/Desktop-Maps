using System;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;

namespace HowDoISamples
{
    public class GenerateRoutableShapeFile : UserControl
    {
        public GenerateRoutableShapeFile()
        {
            InitializeComponent();
        }

        private void UserControl_Load(object sender, EventArgs e)
        {
            RenderMap();
        }

        private void btnGenerateRoadData_Click(object sender, EventArgs e)
        {
            // Please uncomment the code below to disassemble the shapefile
            // RoutingHelper.GenerateRoutableShapeFile(@"..\..\SampleData\Edmonton.shp", @"..\..\SampleData\RoutableEdmonton.shp");

            ShapeFileFeatureSource disassembledFeatureSource = new ShapeFileFeatureSource(@"..\..\SampleData\RoutableEdmonton.shp");
            disassembledFeatureSource.Open();
            lbDisassebledCount.Text = disassembledFeatureSource.GetCount().ToString(CultureInfo.InvariantCulture);
            disassembledFeatureSource.Close();

            MessageBox.Show("Finish building routing data!");
        }

        private void RenderMap()
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#e6e5d1"));
            winformsMap1.CurrentExtent = new RectangleShape(-113.61883135497,54.0464761546714,-111.586360651845,52.5962808421714);

            ShapeFileFeatureLayer shapeFileFeatureLayer = new ShapeFileFeatureLayer(@"..\..\SampleData\Edmonton.shp");
            shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColor.FromHtml("#b8ac9c"), 7), new GeoPen(GeoColor.FromHtml("#FFFFFF"), 4));
            shapeFileFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            LayerOverlay shapeFileFeatureOverlay = new LayerOverlay();
            shapeFileFeatureOverlay.Layers.Add("shapeFileFeatureLayer", shapeFileFeatureLayer);
            winformsMap1.Overlays.Add("shapeFileFeatureOverlay", shapeFileFeatureOverlay);

            shapeFileFeatureLayer.Open();
            lbRawCount.Text = shapeFileFeatureLayer.FeatureSource.GetCount().ToString(CultureInfo.InvariantCulture);
            shapeFileFeatureLayer.Close();

            winformsMap1.Refresh();
        }

        #region Component Designer generated code

        private Label lblCoordinate;
        private GroupBox groupBox1;
        private WinformsMap winformsMap1;
        private Label label3;
        private Button btnGenerateRoadData;
        private Label label2;
        private Label label1;
        private GroupBox groupBox2;
        private Label lbRawCount;
        private Label lbDisassebledCount;
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
            this.lblCoordinate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbDisassebledCount = new System.Windows.Forms.Label();
            this.lbRawCount = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGenerateRoadData = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.MapSuite.DesktopEdition.WinformsMap();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCoordinate
            // 
            this.lblCoordinate.AutoSize = true;
            this.lblCoordinate.Location = new System.Drawing.Point(204, 234);
            this.lblCoordinate.Name = "lblCoordinate";
            this.lblCoordinate.Size = new System.Drawing.Size(0, 13);
            this.lblCoordinate.TabIndex = 6;
            this.lblCoordinate.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.btnGenerateRoadData);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(514, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(226, 185);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbDisassebledCount);
            this.groupBox2.Controls.Add(this.lbRawCount);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(5, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(216, 85);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Record Count";
            // 
            // lbDisassebledCount
            // 
            this.lbDisassebledCount.AutoSize = true;
            this.lbDisassebledCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDisassebledCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbDisassebledCount.Location = new System.Drawing.Point(132, 57);
            this.lbDisassebledCount.Name = "lbDisassebledCount";
            this.lbDisassebledCount.Size = new System.Drawing.Size(14, 13);
            this.lbDisassebledCount.TabIndex = 19;
            this.lbDisassebledCount.Text = "";
            // 
            // lbRawCount
            // 
            this.lbRawCount.AutoSize = true;
            this.lbRawCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRawCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbRawCount.Location = new System.Drawing.Point(132, 32);
            this.lbRawCount.Name = "lbRawCount";
            this.lbRawCount.Size = new System.Drawing.Size(14, 13);
            this.lbRawCount.TabIndex = 18;
            this.lbRawCount.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 16;
            this.label1.Text = "Raw ShapeFile: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Disassembled ShapeFile: ";
            // 
            // btnGenerateRoadData
            // 
            this.btnGenerateRoadData.Location = new System.Drawing.Point(70, 57);
            this.btnGenerateRoadData.Name = "btnGenerateRoadData";
            this.btnGenerateRoadData.Size = new System.Drawing.Size(144, 23);
            this.btnGenerateRoadData.TabIndex = 15;
            this.btnGenerateRoadData.Text = "Disassemble ShapeFile";
            this.btnGenerateRoadData.UseVisualStyleBackColor = true;
            this.btnGenerateRoadData.Click += new System.EventHandler(this.btnGenerateRoadData_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(205, 26);
            this.label3.TabIndex = 14;
            this.label3.Text = "Click the button below to disassemble the \r\nLineShapes in raw ShapeFile.";
            // 
            // winformsMap1
            // 
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.DrawingQuality = ThinkGeo.MapSuite.Core.DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapResizeMode = ThinkGeo.MapSuite.Core.MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = ThinkGeo.MapSuite.Core.GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000;
            this.winformsMap1.MinimumScale = 200;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.winformsMap1.TabIndex = 9;
            this.winformsMap1.Text = "winformsMap1";
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.DesktopEdition.MapThreadingMode.Default;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.DesktopEdition.ZoomLevelSnappingMode.Default;
            // 
            // GenerateRoutableShapeFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCoordinate);
            this.Controls.Add(this.winformsMap1);
            this.Name = "GenerateRoutableShapeFile";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.UserControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}
