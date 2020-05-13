using System;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    //NotImplemented
    public class LoadGdalRasterLayer : UserControl
    {
        public LoadGdalRasterLayer()
        {
            InitializeComponent();
        }

        private void LoadGdalRasterLayer_Load(object sender, EventArgs e)
        {
            try
            {
                winformsMap1.MapUnit = GeographyUnit.DecimalDegree;

                GdalRasterLayer worldLayer = new GdalRasterLayer();//Samples.RootDirectory + @"Data\bw_sample.jpg");
                worldLayer.LowerThreshold = 0;
                worldLayer.UpperThreshold = double.MaxValue;

                worldLayer.Open();
                winformsMap1.CurrentExtent = worldLayer.GetBoundingBox();
                worldLayer.Close();

                LayerOverlay staticOverlay = new LayerOverlay();
                staticOverlay.DrawingExceptionMode = DrawingExceptionMode.DrawException;

                staticOverlay.Layers.Add("WorldLayer", worldLayer);
                winformsMap1.Overlays.Add(staticOverlay);

                winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

                winformsMap1.Refresh();
            }
            catch (FileNotFoundException ex)
            {
                string message = "You should get Fdo dependencies from [Install-Path]\\Developer Reference\\System32, and put MapSuiteFdoExtensionx86 folder to System32 folder.\r\n\r\n" + ex.Message;
                MessageBox.Show(message, "FileNotFound", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0);
            }
        }

        private WinformsMap winformsMap1;

        #region Component Designer generated code

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
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.SuspendLayout();
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
            // DisplayShapeMap
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.winformsMap1);
            this.Name = "DisplayShapeMap";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.LoadGdalRasterLayer_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}