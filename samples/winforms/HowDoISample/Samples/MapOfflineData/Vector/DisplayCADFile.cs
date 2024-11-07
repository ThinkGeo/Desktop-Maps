using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class DisplayCADFile : UserControl
    {
        private CadFeatureLayer _cadLayer;

        public DisplayCADFile()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            var cadOverlay = new LayerOverlay();
            mapView.Overlays.Add("CAD overlay", cadOverlay);

            // Create the new cad layer
            _cadLayer = new CadFeatureLayer(@"../../../Data/CAD/Zipcodes.DWG");

            // Create a new ProjectionConverter to convert between Lambert Conformal Conic and Spherical Mercator (3857)
            var projectionConverter = new ProjectionConverter(103376, 3857);
            _cadLayer.FeatureSource.ProjectionConverter = projectionConverter;

            // Create an Area style on zoom level 1 and then apply it to all zoom levels up to 20.
            _cadLayer.StylingType = CadStylingType.EmbeddedStyling;

            // Add the layer to the overlay we created earlier.
            cadOverlay.Layers.Add("CAD Drawing", _cadLayer);

            // Set the current extent of the map to the extent of the CAD data
            _cadLayer.Open();
            mapView.CurrentExtent = _cadLayer.GetBoundingBox();

            await mapView.RefreshAsync();
        }

        private async void rbEmbeddedStyling_CheckedChanged(object sender, EventArgs e)
        {
            if (_cadLayer == null) return;
            // Create an Area style on zoom level 1 and then apply it to all zoom levels up to 20.
            _cadLayer.StylingType = CadStylingType.EmbeddedStyling;

            await mapView.RefreshAsync();
        }

        private async void rbProgrammaticStyling_CheckedChanged(object sender, EventArgs e)
        {
            if (_cadLayer == null) return;
            // Create an Area style on zoom level 1 and then apply it to all zoom levels up to 20.
            _cadLayer.StylingType = CadStylingType.StandardStyling;
            _cadLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(new GeoPen(GeoColors.Blue, 2));
            _cadLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            await mapView.RefreshAsync();
        }

        #region Component Designer generated code

        private MapView mapView;
        private Panel panel1;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private Label label1;

        private void InitializeComponent()
        {
            mapView = new MapView();
            panel1 = new Panel();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
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
            panel1.Controls.Add(radioButton1);
            panel1.Controls.Add(radioButton2);
            panel1.Controls.Add(label1);
            panel1.Location = new System.Drawing.Point(965, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(285, 611);
            panel1.TabIndex = 3;
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
            radioButton1.Text = "DWG-Embeded Styling";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += new EventHandler(rbEmbeddedStyling_CheckedChanged);// 
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
            radioButton2.Text = "Standard Programmatic Styling";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += new EventHandler(rbProgrammaticStyling_CheckedChanged);
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
            label1.Text = "Display a CAD File:";
            // 
            // DisplayCADFile
            // 
            Controls.Add(panel1);
            Controls.Add(mapView);
            Name = "DisplayCADFile";
            Size = new System.Drawing.Size(1250, 611);
            Load += new EventHandler(Form_Load);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}
