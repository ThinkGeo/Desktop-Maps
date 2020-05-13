/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class LoadSdfFeatureLayer : UserControl
    {
        public LoadSdfFeatureLayer()
        {
            InitializeComponent();
        }

        private void LoadSdfFeatureLayer_Load(object sender, EventArgs e)
        {
            try
            {
                winformsMap1.MapUnit = GeographyUnit.Meter;
                winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
                winformsMap1.ThreadingMode = MapThreadingMode.SingleThreaded;

                // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map.
                ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
                winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

                winformsMap1.CurrentExtent = new RectangleShape(-9769954, 5434161, -9762221, 5417809);
                winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

                SdfFeatureLayer worldLayer = new SdfFeatureLayer(Samples.RootDirectory + @"Data\Sheboygan_CityLimits.sdf", null);
                worldLayer.FeatureSource.Projection = new Proj4Projection(4326, 3857);
                worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyles.CreateSimpleAreaStyle(GeoColor.FromArgb(100, GeoColor.SimpleColors.Green), GeoColor.SimpleColors.Green);
                worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                LayerOverlay staticOverlay = new LayerOverlay();
                staticOverlay.Layers.Add("WorldLayer", worldLayer);
                winformsMap1.Overlays.Add(staticOverlay);

                winformsMap1.Refresh();
            }
            catch (FileNotFoundException ex)
            {
                string message = "Could not find MapSuiteFdoExtension.dll assembly.\r\n" +
                                 "This dll is expected to be found in the MapSuiteFDOExtensionX86(X64) folder so you need to make this folder available within the System32 folder of your computer. Simply copy the MapSuiteFDOExtensionX86 or MapSuiteFDOExtensionX64 folder from [Install-Path}\\Developer Reference\\System32 to the ‘System32’ (‘System’ for x64) folder of your computer.\r\n" +
                                 "Additionally you need to add the FdoExtension.dll to this sample. You can reference this DLL from [Install-Path]\\Developer Reference\\Spatial Extensions\\Fdo Extension\\.\r\n\r\n" + ex.Message;
                MessageBox.Show(message, "FileNotFound", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
            }
        }

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;
        private WinformsMap winformsMap1;

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
            this.lblDescription = new System.Windows.Forms.Label();
            this.winformsMap1 = new WinformsMap();
            this.SuspendLayout();
            //
            // lblDescription
            //
            this.lblDescription.BackColor = System.Drawing.Color.LightYellow;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblDescription.Location = new System.Drawing.Point(5, 5);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(300, 80);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = @"To use the SdfFeatureLayer functions, you have to reference [Install-Path]\Developer Reference\Spatial Extensions\Fdo Extension\FdoExtension.dll, And copy MapSuiteFdoExtensionx86/MapSuiteFdoExtensionx64 folder from [Install-Path]\\Developer Reference\\System32 to System32 folder for x86/x64 platform.";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 0;
            this.winformsMap1.Text = "winformsMap1";
            //
            // LoadAPostgreSqlFeatureLayer
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.winformsMap1);
            this.Name = "LoadAPostgreSqlFeatureLayer";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.LoadSdfFeatureLayer_Load);
            this.ResumeLayout(false);
        }

        private Label lblDescription;

        #endregion Component Designer generated code
    }
}