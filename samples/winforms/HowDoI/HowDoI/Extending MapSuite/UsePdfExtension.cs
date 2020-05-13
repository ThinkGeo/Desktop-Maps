using PdfSharp;
using PdfSharp.Pdf;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class UsePdfExtension : UserControl
    {
        public UsePdfExtension()
        {
            InitializeComponent();
        }

        private void CreateASpatialIndexForAShapeFileLayer_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-111.7875, 92.0859375, 148.36875, -93.5390625);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            EcwRasterLayer worldImageLayer = new EcwRasterLayer(Samples.RootDirectory + @"Data\World.ecw");

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\Countries02.shp");
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.Country1;
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.StartCap = DrawingLineCap.Round;
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.EndCap = DrawingLineCap.Round;
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer usStatesLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\USStates.shp");
            usStatesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.State2;
            usStatesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.StartCap = DrawingLineCap.Round;
            usStatesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer worldCapitalsLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\WorldCapitals.shp");
            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyles.Capital3;
            worldCapitalsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ShapeFileFeatureLayer worldCapitalsLabelsLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\WorldCapitals.shp");
            worldCapitalsLabelsLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = TextStyles.Capital3("city_name");
            worldCapitalsLabelsLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.HaloPen = new GeoPen(GeoColor.StandardColors.White, 2);
            worldCapitalsLabelsLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.HaloPen.StartCap = DrawingLineCap.Round;
            worldCapitalsLabelsLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.HaloPen.EndCap = DrawingLineCap.Round;
            worldCapitalsLabelsLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle.SuppressPartialLabels = true;
            worldCapitalsLabelsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay staticOverlay = new LayerOverlay();
            staticOverlay.Layers.Add("WorldImageLayer", worldImageLayer);
            staticOverlay.Layers.Add("WorldLayer", worldLayer);
            staticOverlay.Layers.Add("USStatesLayer", usStatesLayer);
            staticOverlay.Layers.Add("WorldCapitals", worldCapitalsLayer);
            staticOverlay.Layers.Add("WorldCapitalsLabels", worldCapitalsLabelsLayer);
            winformsMap1.Overlays.Add(staticOverlay);

            winformsMap1.Refresh();
        }

        // Here we setup the PDF page and then create our PDFGeoCanvas.
        // We loop through all the layers to draw and then save & pop the
        // PDF
        private void btnToPdf_Click(object sender, EventArgs e)
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            if (pageOrientationLandscape.Checked == true)
            {
                page.Orientation = PageOrientation.Landscape;
            }

            PdfGeoCanvas pdfGeoCanvas = new PdfGeoCanvas();

            // This allows you to control the area in which you want the
            // map to draw in.  Leaving this commented out uses the whole page
            //pdfGeoCanvas.DrawingArea = new Rectangle(200, 50, 400, 400);
            Collection<SimpleCandidate> labelsInLayers = new Collection<SimpleCandidate>();
            foreach (Layer layer in ((LayerOverlay)winformsMap1.Overlays[0]).Layers)
            {
                pdfGeoCanvas.BeginDrawing(page, winformsMap1.CurrentExtent, GeographyUnit.DecimalDegree);
                layer.Open();
                layer.Draw(pdfGeoCanvas, labelsInLayers);
                layer.Close();
                pdfGeoCanvas.EndDrawing();
            }

            string filename = GetTemporaryFolder() + "\\MapSuite PDF Map.pdf";
            try
            {
                document.Save(filename);
                OpenPdfFile(filename);
            }
            catch (Exception ex)
            {
                string message = "You have no permission to write file to disk." + ex.Message;
                MessageBox.Show(message, "Not permitted", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
            }
        }

        private static string GetTemporaryFolder()
        {
            string returnValue = string.Empty;
            if (string.IsNullOrEmpty(returnValue))
            {
                returnValue = Environment.GetEnvironmentVariable("Temp");
            }

            if (string.IsNullOrEmpty(returnValue))
            {
                returnValue = Environment.GetEnvironmentVariable("Tmp");
            }

            if (string.IsNullOrEmpty(returnValue))
            {
                returnValue = @"c:\MapSuiteTemp";
            }
            else
            {
                returnValue = returnValue + "\\" + "MapSuite";
            }

            return returnValue;
        }

        private static void OpenPdfFile(string filename)
        {
            try
            {
                Process.Start(filename);
            }
            catch (Win32Exception ex)
            {
                if (ex.Message == "No application is associated with the specified file for this operation")
                {
                    //string message = "You can't open Pdf file, the reason maybe you don't install Adobe Reader\r\n\r\n" + ex.ToString();
                    //MessageBox.Show(message, "Open Pdf file failed", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
                    ProcessStartInfo psi = new ProcessStartInfo(@"C:\WINDOWS\system32\rundll32.exe");
                    psi.Arguments = @" C:\WINDOWS\system32\shell32.dll, OpenAs_RunDLL " + filename;
                    Process.Start(psi);
                }
            }
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;
        private Button btnToPdf;
        private WinformsMap winformsMap1;
        private RadioButton pageOrientationLandscape;
        private RadioButton pageOrientationPortrait;
        private GroupBox groupBox1;

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
            this.btnToPdf = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pageOrientationLandscape = new System.Windows.Forms.RadioButton();
            this.pageOrientationPortrait = new System.Windows.Forms.RadioButton();
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // btnToPdf
            //
            this.btnToPdf.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnToPdf.Location = new System.Drawing.Point(16, 59);
            this.btnToPdf.Name = "btnToPdf";
            this.btnToPdf.Size = new System.Drawing.Size(115, 23);
            this.btnToPdf.TabIndex = 1;
            this.btnToPdf.Text = "To Pdf";
            this.btnToPdf.UseVisualStyleBackColor = true;
            this.btnToPdf.Click += new System.EventHandler(this.btnToPdf_Click);
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.pageOrientationLandscape);
            this.groupBox1.Controls.Add(this.pageOrientationPortrait);
            this.groupBox1.Controls.Add(this.btnToPdf);
            this.groupBox1.Location = new System.Drawing.Point(585, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 88);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Functions";
            //
            // pageOrientationLandscape
            //
            this.pageOrientationLandscape.AutoSize = true;
            this.pageOrientationLandscape.Checked = true;
            this.pageOrientationLandscape.Location = new System.Drawing.Point(5, 21);
            this.pageOrientationLandscape.Name = "pageOrientationLandscape";
            this.pageOrientationLandscape.Size = new System.Drawing.Size(78, 17);
            this.pageOrientationLandscape.TabIndex = 48;
            this.pageOrientationLandscape.TabStop = true;
            this.pageOrientationLandscape.Text = "Landscape";
            this.pageOrientationLandscape.UseVisualStyleBackColor = true;
            //
            // pageOrientationPortrait
            //
            this.pageOrientationPortrait.AutoSize = true;
            this.pageOrientationPortrait.Location = new System.Drawing.Point(89, 21);
            this.pageOrientationPortrait.Name = "pageOrientationPortrait";
            this.pageOrientationPortrait.Size = new System.Drawing.Size(58, 17);
            this.pageOrientationPortrait.TabIndex = 47;
            this.pageOrientationPortrait.Text = "Portrait";
            this.pageOrientationPortrait.UseVisualStyleBackColor = true;
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
            this.winformsMap1.CurrentScale = 590591790D;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.DrawingQuality = DrawingQuality.Default;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.MapFocusMode = ThinkGeo.MapSuite.WinForms.MapFocusMode.Default;
            this.winformsMap1.MapResizeMode = MapResizeMode.PreserveScale;
            this.winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            this.winformsMap1.MaximumScale = 80000000000000D;
            this.winformsMap1.MinimumScale = 200D;
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            this.winformsMap1.TabIndex = 3;
            this.winformsMap1.Text = "winformsMap1";
            this.winformsMap1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.winformsMap1.ThreadingMode = ThinkGeo.MapSuite.WinForms.MapThreadingMode.Default;
            this.winformsMap1.ZoomLevelSnapping = ThinkGeo.MapSuite.WinForms.ZoomLevelSnappingMode.Default;
            //
            // UsePdfExtension
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "UsePdfExtension";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.CreateASpatialIndexForAShapeFileLayer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}