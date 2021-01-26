using System;
using System.Windows;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class WorldProjectionSample : UserControl
    {
        public WorldProjectionSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            // Set the background of the map to a water color
            mapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#42B2EE"));

            // Create a new overlay that will hold our new layer and add it to the map.
            LayerOverlay layerOverlay = new LayerOverlay();
            mapView.Overlays.Add("world overlay", layerOverlay);

            // Create the world layer, it will be decimal degrees at first but we will be able to change it
            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Countries02.shp");

            // Setup the styles for the countries for zoom level 1 and then apply it until zoom level 20
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.DarkSlateGray;
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillBrush = new GeoSolidBrush(GeoColor.FromHtml("#C9E1BE"));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the layer to the overlay we created earlier.
            layerOverlay.Layers.Add("world layer", worldLayer);

            mapView.CurrentExtent = new RectangleShape(-176.885988320039, 121.810205234135, 168.699949179961, -92.642919765865);

            // Refresh the map.
            mapView.Refresh();
        }

        private void Radial_CheckChanged(object sender, EventArgs e)
        {

            RadioButton radioButton = (RadioButton)sender;
            FeatureLayer layer = mapView.FindFeatureLayer("world layer");

            if (layer != null)
            {
                switch (radioButton.Text)
                {
                    case "Decimal Degrees":
                        // Set the new projection converter and open it.  Next set the map to the correct map unit and lastley set the new extent
                        layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, 4326);
                        layer.FeatureSource.ProjectionConverter.Open();
                        mapView.MapUnit = GeographyUnit.DecimalDegree;
                        mapView.CurrentExtent = new RectangleShape(-176.885988320039, 121.810205234135, 168.699949179961, -92.642919765865);
                        break;
                    case "MGA Zone 55":
                        // Set the new projection converter and open it.  Next set the map to the correct map unit and lastley set the new extent
                        layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, @"+proj=utm +zone=55 +south +ellps=GRS80 +towgs84=0,0,0,0,0,0,0 +units=m +no_defs");
                        layer.FeatureSource.ProjectionConverter.Open();
                        mapView.MapUnit = GeographyUnit.Meter;
                        mapView.CurrentExtent = new RectangleShape(-5255863.3178402, 10345432.4700869, 2850158.42260337, 3362534.22380121);
                        break;
                    case "Albers Equal Area Conic":
                        // Set the new projection converter and open it.  Next set the map to the correct map unit and lastley set the new extent
                        layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, @"+proj=aea +lat_1=20 +lat_2=60 +lat_0=40 +lon_0=-96 +x_0=0 +y_0=0 +ellps=GRS80 +datum=NAD83 +units=m no_defs");
                        layer.FeatureSource.ProjectionConverter.Open();
                        mapView.MapUnit = GeographyUnit.Meter;
                        mapView.CurrentExtent = new RectangleShape(-4167908.28780145, 3251198.24423971, 3952761.55210086, -3744318.54555566);
                        break;
                    case "Polar Stereographic":
                        // Set the new projection converter and open it.  Next set the map to the correct map unit and lastley set the new extent
                        layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, @"+proj=stere +lat_0=90 +lat_ts=70 +lon_0=-45 +k=1 +x_0=0 +y_0=0 +a=6378273 +b=6356889.449 +units=m +no_defs s");
                        layer.FeatureSource.ProjectionConverter.Open();
                        mapView.MapUnit = GeographyUnit.Meter;
                        mapView.CurrentExtent = new RectangleShape(-7944508.96033433, 6176987.61570341, 8296830.7194703, -7814045.96388732);
                        break;
                    case "Equal Area Cylindrical":
                        // Set the new projection converter and open it.  Next set the map to the correct map unit and lastley set the new extent
                        layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, @"+proj=cea +lon_0=0 +x_0=0 +y_0=0 +lat_ts=45 +ellps=WGS84 +datum=WGS84 +units=m +no_defs");
                        layer.FeatureSource.ProjectionConverter.Open();
                        mapView.MapUnit = GeographyUnit.Meter;
                        mapView.CurrentExtent = new RectangleShape(-13262961.8178162, 11618032.522095, 25138095.3911286, -12211718.1365584);
                        break;
                    default:
                        break;
                }

                mapView.Refresh();
            }


        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        private Panel panel1;
        private Label label1;
        private RadioButton radioButton4;
        private RadioButton radioButton3;
        private RadioButton radioButton2;
        private RadioButton radioButton1;
        private RadioButton radioButton5;

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.ForeColor = System.Drawing.Color.White;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapFocusMode = ThinkGeo.Core.MapFocusMode.Default;
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1254, 667);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.radioButton5);
            this.panel1.Controls.Add(this.radioButton4);
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(952, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 667);
            this.panel1.TabIndex = 2;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radioButton5.ForeColor = System.Drawing.Color.White;
            this.radioButton5.Location = new System.Drawing.Point(21, 158);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(164, 21);
            this.radioButton5.TabIndex = 7;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "Equal Area Cylindrical";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.Radial_CheckChanged);
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radioButton4.ForeColor = System.Drawing.Color.White;
            this.radioButton4.Location = new System.Drawing.Point(21, 131);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(152, 21);
            this.radioButton4.TabIndex = 6;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "Polar Stereographic";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.Radial_CheckChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radioButton3.ForeColor = System.Drawing.Color.White;
            this.radioButton3.Location = new System.Drawing.Point(20, 104);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(179, 21);
            this.radioButton3.TabIndex = 5;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Albers Equal Area Conic";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.Radial_CheckChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radioButton2.ForeColor = System.Drawing.Color.White;
            this.radioButton2.Location = new System.Drawing.Point(20, 77);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(114, 21);
            this.radioButton2.TabIndex = 4;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "MGA Zone 55";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.Radial_CheckChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.radioButton1.ForeColor = System.Drawing.Color.White;
            this.radioButton1.Location = new System.Drawing.Point(20, 50);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(134, 21);
            this.radioButton1.TabIndex = 3;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Decimal Degrees";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.Radial_CheckChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data Feed Controls:";
            // 
            // WorldProjectionSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "WorldProjectionSample";
            this.Size = new System.Drawing.Size(1254, 667);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

       
    }
}