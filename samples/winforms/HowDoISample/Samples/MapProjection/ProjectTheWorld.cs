using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ProjectTheWorld : UserControl
    {
        public ProjectTheWorld()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            // Set the background of the map to a water color
            mapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#42B2EE"));

            // Create a new overlay that will hold our new layer and add it to the map.
            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            mapView.Overlays.Add("world overlay", layerOverlay);

            // Create the world layer, it will be decimal degrees at first, but we will be able to change it
            var worldLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Countries02.shp");

            // Set up the styles for the countries for zoom level 1 and then apply it until zoom level 20
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.Color = GeoColors.DarkSlateGray;
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.FillBrush = new GeoSolidBrush(GeoColor.FromHtml("#C9E1BE"));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the layer to the overlay we created earlier.
            layerOverlay.Layers.Add("world layer", worldLayer);

            mapView.CurrentExtent = new RectangleShape(-176.885988320039, 121.810205234135, 168.699949179961, -92.642919765865);

            rdoPolar.Checked = true;

            await mapView.RefreshAsync();
        }

        private async void Radial_CheckChanged(object sender, EventArgs e)
        {
            var radioButton = (RadioButton)sender;
            if (radioButton == null)
                return;

            if (!radioButton.Checked)
                return;

            var layer = mapView.FindFeatureLayer("world layer");

            if (layer != null)
            {
                switch (radioButton.Text)
                {
                    case "Decimal Degrees":
                        // Set the new projection converter and open it.  Next set the map to the correct map unit and lastly set the new extent
                        layer.FeatureSource.ProjectionConverter = null;
                        mapView.MapUnit = GeographyUnit.DecimalDegree;
                        mapView.CurrentExtent = new RectangleShape(-76.885988320039, 21.810205234135, 68.699949179961, -72.642919765865);
                        break;
                    case "MGA Zone 55":
                        // Set the new projection converter and open it.  Next set the map to the correct map unit and lastly set the new extent
                        layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, SampleKeys.ProjString1);
                        layer.FeatureSource.ProjectionConverter.Open();
                        mapView.MapUnit = GeographyUnit.Meter;
                        mapView.CurrentExtent = new RectangleShape(-4415962.270035205, 10196887.263572674, 3690059.470408367, 3223755.308540492);
                        break;
                    case "Albers Equal Area Conic":
                        // Set the new projection converter and open it.  Next set the map to the correct map unit and lastly set the new extent
                        layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, SampleKeys.ProjString2);
                        layer.FeatureSource.ProjectionConverter.Open();
                        mapView.MapUnit = GeographyUnit.Meter;
                        mapView.CurrentExtent = new RectangleShape(-4167908.28780145, 3251198.24423971, 3952761.55210086, -3744318.54555566);
                        break;
                    case "Polar Stereographic":
                        // Set the new projection converter and open it.  Next set the map to the correct map unit and lastly set the new extent
                        layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, SampleKeys.ProjString3);
                        layer.FeatureSource.ProjectionConverter.Open();
                        mapView.MapUnit = GeographyUnit.Meter;
                        mapView.CurrentExtent = new RectangleShape(-7944508.96033433, 6176987.61570341, 8296830.7194703, -7814045.96388732);
                        break;
                    case "Equal Area Cylindrical":
                        // Set the new projection converter and open it.  Next set the map to the correct map unit and lastly set the new extent
                        layer.FeatureSource.ProjectionConverter = new ProjectionConverter(4326, SampleKeys.ProjString4);
                        layer.FeatureSource.ProjectionConverter.Open();
                        mapView.MapUnit = GeographyUnit.Meter;
                        mapView.CurrentExtent = new RectangleShape(-13262961.8178162, 11618032.522095, 25138095.3911286, -12211718.1365584);
                        break;
                }

                await mapView.RefreshAsync();
            }
        }

        protected override void Dispose(bool disposing)
        {
            mapView.Dispose();
            GC.SuppressFinalize(this);
        }

        #region Component Designer generated code
        private Panel panel1;
        private Label label1;
        private RadioButton rdoPolar;
        private RadioButton rdoAlbers;
        private RadioButton rdoMGAZone;
        private RadioButton rdoDecimalDegrees;
        private RadioButton rdoCylindrical;
        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdoCylindrical = new System.Windows.Forms.RadioButton();
            this.rdoPolar = new System.Windows.Forms.RadioButton();
            this.rdoAlbers = new System.Windows.Forms.RadioButton();
            this.rdoMGAZone = new System.Windows.Forms.RadioButton();
            this.rdoDecimalDegrees = new System.Windows.Forms.RadioButton();
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
            this.mapView.MapFocusMode = MapFocusMode.Default;
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotationAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1254, 667);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.rdoCylindrical);
            this.panel1.Controls.Add(this.rdoPolar);
            this.panel1.Controls.Add(this.rdoAlbers);
            this.panel1.Controls.Add(this.rdoMGAZone);
            this.panel1.Controls.Add(this.rdoDecimalDegrees);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(952, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(302, 667);
            this.panel1.TabIndex = 2;
            // 
            // rdoCylindrical
            // 
            this.rdoCylindrical.AutoSize = true;
            this.rdoCylindrical.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rdoCylindrical.ForeColor = System.Drawing.Color.White;
            this.rdoCylindrical.Location = new System.Drawing.Point(22, 129);
            this.rdoCylindrical.Name = "rdoCylindrical";
            this.rdoCylindrical.Size = new System.Drawing.Size(164, 21);
            this.rdoCylindrical.TabIndex = 7;
            this.rdoCylindrical.Text = "Equal Area Cylindrical";
            this.rdoCylindrical.UseVisualStyleBackColor = true;
            this.rdoCylindrical.CheckedChanged += new System.EventHandler(this.Radial_CheckChanged);
            // 
            // rdoPolar
            // 
            this.rdoPolar.AutoSize = true;
            this.rdoPolar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rdoPolar.ForeColor = System.Drawing.Color.White;
            this.rdoPolar.Location = new System.Drawing.Point(22, 102);
            this.rdoPolar.Name = "rdoPolar";
            this.rdoPolar.Size = new System.Drawing.Size(152, 21);
            this.rdoPolar.TabIndex = 6;
            this.rdoPolar.Text = "Polar Stereographic";
            this.rdoPolar.UseVisualStyleBackColor = true;
            this.rdoPolar.CheckedChanged += new System.EventHandler(this.Radial_CheckChanged);
            // 
            // rdoAlbers
            // 
            this.rdoAlbers.AutoSize = true;
            this.rdoAlbers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rdoAlbers.ForeColor = System.Drawing.Color.White;
            this.rdoAlbers.Location = new System.Drawing.Point(21, 75);
            this.rdoAlbers.Name = "rdoAlbers";
            this.rdoAlbers.Size = new System.Drawing.Size(179, 21);
            this.rdoAlbers.TabIndex = 5;
            this.rdoAlbers.Text = "Albers Equal Area Conic";
            this.rdoAlbers.UseVisualStyleBackColor = true;
            this.rdoAlbers.CheckedChanged += new System.EventHandler(this.Radial_CheckChanged);
            // 
            // rdoMGAZone
            // 
            this.rdoMGAZone.AutoSize = true;
            this.rdoMGAZone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rdoMGAZone.ForeColor = System.Drawing.Color.White;
            this.rdoMGAZone.Location = new System.Drawing.Point(21, 48);
            this.rdoMGAZone.Name = "rdoMGAZone";
            this.rdoMGAZone.Size = new System.Drawing.Size(114, 21);
            this.rdoMGAZone.TabIndex = 4;
            this.rdoMGAZone.Text = "MGA Zone 55";
            this.rdoMGAZone.UseVisualStyleBackColor = true;
            this.rdoMGAZone.CheckedChanged += new System.EventHandler(this.Radial_CheckChanged);
            // 
            // rdoDecimalDegrees
            // 
            this.rdoDecimalDegrees.AutoSize = true;
            this.rdoDecimalDegrees.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rdoDecimalDegrees.ForeColor = System.Drawing.Color.White;
            this.rdoDecimalDegrees.Location = new System.Drawing.Point(22, 156);
            this.rdoDecimalDegrees.Name = "rdoDecimalDegrees";
            this.rdoDecimalDegrees.Size = new System.Drawing.Size(134, 21);
            this.rdoDecimalDegrees.TabIndex = 3;
            this.rdoDecimalDegrees.Text = "Decimal Degrees";
            this.rdoDecimalDegrees.UseVisualStyleBackColor = true;
            this.rdoDecimalDegrees.CheckedChanged += new System.EventHandler(this.Radial_CheckChanged);
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
            // ProjectTheWorld
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "ProjectTheWorld";
            this.Size = new System.Drawing.Size(1254, 667);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}