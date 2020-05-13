using System;
using System.Diagnostics;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class LoadAWmsImage : UserControl
    {
        private WinformsMap winformsMap1;

        public LoadAWmsImage()
        {
            InitializeComponent();
        }

        private void DisplaySatelliteImage_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-143.4, 109.3, 116.7, -76.3);
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            WmsRasterLayer wmsImageLayer = new WmsRasterLayer(new Uri("http://howdoiwms.thinkgeo.com/WmsServer.aspx"));

            // 1.3.0 server
            //WmsRasterLayer wmsImageLayer = new WmsRasterLayer(new Uri("http://sampleserver1.arcgisonline.com/ArcGIS/services/Specialty/ESRI_StatesCitiesRivers_USA/MapServer/WMSServer"));
            wmsImageLayer.UpperThreshold = double.MaxValue;
            wmsImageLayer.LowerThreshold = 0;

            wmsImageLayer.Open();
            wmsImageLayer.ActiveStyleNames.Add("Simple");
            foreach (string layerName in wmsImageLayer.GetServerLayerNames())
            {
                wmsImageLayer.ActiveLayerNames.Add(layerName);
            }
            // this parameter is just optional.
            wmsImageLayer.Exceptions = "application/vnd.ogc.se_xml";

            LayerOverlay imageOverlay = new LayerOverlay();
            imageOverlay.Layers.Add("wmsImageLayer", wmsImageLayer);
            winformsMap1.Overlays.Add(imageOverlay);

            // GetRequestUrl for debug
            Debug.WriteLine(wmsImageLayer.GetRequestUrl(winformsMap1.CurrentExtent, winformsMap1.Width, winformsMap1.Height));
            wmsImageLayer.Close();

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
            // LoadAWmsImage
            //
            this.Controls.Add(this.winformsMap1);
            this.Name = "LoadAWmsImage";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DisplaySatelliteImage_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}