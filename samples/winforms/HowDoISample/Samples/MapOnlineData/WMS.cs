using System;
using System.Windows.Forms;
using ThinkGeo.Core;


namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class WMS : UserControl
    {
        public WMS()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            // This code sets up the sample to use the overlay versus the layer.
            UseOverlay();

            // Set the current extent to a local area.
            mapView.CurrentExtent = new RectangleShape(-96.8538765269409, 33.1618647290098, -96.7987487018851, 33.1054126590461);

            // Refresh the map.
            await mapView.RefreshAsync();
        }

        private async void rbLayerOrOverlay_CheckedChanged(object sender, EventArgs e)
        {
            // Based on the radio buttons we switch between using the overlay and layer.
            var button = (RadioButton)sender;
            if (button.Text != null && button.Checked)
            {
                switch (button.Text)
                {
                    case "Use WmsOverlay":
                        UseOverlay();
                        break;
                    case "Use WmsRasterLayer":
                        UseLayer();
                        break;
                }
                await mapView.RefreshAsync();
            }
        }

        private void UseOverlay()
        {
            // Clear out the overlays so we start fresh
            mapView.Overlays.Clear();

            // Create a WMS overlay using the WMS parameters below.
            // This is a public service and is very slow most of the time.
            var wmsOverlay = new WmsOverlay();
            wmsOverlay.Uri = new Uri("http://ows.mundialis.de/services/service");
            wmsOverlay.Parameters.Add("layers", "OSM-WMS");
            wmsOverlay.Parameters.Add("STYLES", "default");

            // Add the overlay to the map.
            mapView.Overlays.Add(wmsOverlay);
        }

        private void UseLayer()
        {
            // Clear out the overlays so we start fresh
            mapView.Overlays.Clear();

            // Create an overlay that we will add the layer to.
            var staticOverlay = new LayerOverlay();
            mapView.Overlays.Add(staticOverlay);

            // Create the WMS layer using the parameters below.
            // This is a public service and is very slow most of the time.
            //Core.Async.WmsRasterLayer wmsImageLayer = new Core.Async.WmsRasterLayer(new Uri("http://ows.mundialis.de/services/service"));
            var wmsImageLayer = new Core.WmsAsyncLayer(new Uri("http://ows.mundialis.de/services/service"));

            //wmsImageLayer.UpperThreshold = double.MaxValue;
            //wmsImageLayer.LowerThreshold = 0;
            wmsImageLayer.ActiveLayerNames.Add("OSM-WMS");
            wmsImageLayer.ActiveStyleNames.Add("default");
            wmsImageLayer.Exceptions = "application/vnd.ogc.se_xml";

            // Add the layer to the overlay.
            staticOverlay.Layers.Add("wmsImageLayer", wmsImageLayer);
        }

        #region Component Designer generated code

        private MapView mapView;
        private Panel panel1;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private Label label1;

        private void InitializeComponent()
        {
            mapView = new MapView();
            panel1 = new Panel();
            radioButton2 = new RadioButton();
            radioButton1 = new RadioButton();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // mapView
            // 
            mapView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
            | AnchorStyles.Left
            | AnchorStyles.Right;
            mapView.BackColor = System.Drawing.Color.White;
            mapView.CurrentScale = 0D;
            mapView.Location = new System.Drawing.Point(4, 0);
            mapView.MapResizeMode = MapResizeMode.PreserveScale;
            mapView.MaximumScale = 1.7976931348623157E+308D;
            mapView.MinimumScale = 200D;
            mapView.Name = "mapView";
            mapView.RestrictExtent = null;
            mapView.RotatedAngle = 0F;
            mapView.Size = new System.Drawing.Size(962, 611);
            mapView.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
            | AnchorStyles.Right;
            panel1.BackColor = System.Drawing.Color.Gray;
            panel1.Controls.Add(radioButton2);
            panel1.Controls.Add(radioButton1);
            panel1.Controls.Add(label1);
            panel1.Location = new System.Drawing.Point(965, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(285, 611);
            panel1.TabIndex = 1;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            radioButton2.ForeColor = System.Drawing.Color.White;
            radioButton2.Location = new System.Drawing.Point(20, 85);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new System.Drawing.Size(196, 24);
            radioButton2.TabIndex = 2;
            radioButton2.Text = "Use WmsRasterLayer";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += new EventHandler(rbLayerOrOverlay_CheckedChanged);
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Checked = true;
            radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            radioButton1.ForeColor = System.Drawing.Color.White;
            radioButton1.Location = new System.Drawing.Point(20, 48);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new System.Drawing.Size(161, 24);
            radioButton1.TabIndex = 1;
            radioButton1.TabStop = true;
            radioButton1.Text = "Use WmsOverlay";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += new EventHandler(rbLayerOrOverlay_CheckedChanged);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(15, 10);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(162, 25);
            label1.TabIndex = 0;
            label1.Text = "Layer or Overlay:";
            // 
            // WMS
            // 
            Controls.Add(panel1);
            Controls.Add(mapView);
            Name = "WMS";
            Size = new System.Drawing.Size(1250, 611);
            Load += new EventHandler(Form_Load);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}