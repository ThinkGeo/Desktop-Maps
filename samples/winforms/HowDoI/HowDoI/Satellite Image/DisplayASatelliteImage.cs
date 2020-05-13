using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class DisplayASatelliteImage : UserControl
    {
        private WinformsMap winformsMap1;

        public DisplayASatelliteImage()
        {
            InitializeComponent();
        }

        private void DisplaySatelliteImage_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-118.098, 84.3, 118.098, -84.3);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            EcwRasterLayer ecwImageLayer = new EcwRasterLayer(Samples.RootDirectory + @"Data\World.ecw");
            ecwImageLayer.UpperThreshold = double.MaxValue;
            ecwImageLayer.LowerThreshold = 0;

            LayerOverlay ImageOverlay = new LayerOverlay();
            ImageOverlay.Layers.Add("EcwImageLayer", ecwImageLayer);
            winformsMap1.Overlays.Add(ImageOverlay);

            winformsMap1.Refresh();
        }

        #region Component Designer generated code

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
            // DisplayASatelliteImage
            //
            this.Controls.Add(this.winformsMap1);
            this.Name = "DisplayASatelliteImage";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DisplaySatelliteImage_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}