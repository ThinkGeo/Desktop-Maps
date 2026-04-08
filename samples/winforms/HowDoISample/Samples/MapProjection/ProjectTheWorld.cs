using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ProjectTheWorld : UserControl
    {
        private bool _initialized;
        private MvtTilesAsyncLayer _worldLayer;

        public ProjectTheWorld()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            _initialized = true;
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.DecimalDegree;

            // Set the background of the map to a water color
            mapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromHtml("#42B2EE"));

            // Create a new overlay that will hold our new layer and add it to the map.
            var layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            mapView.Overlays.Add("world overlay", layerOverlay);

            var wvtServerUri = "https://tiles.preludemaps.com/styles/WorldStreets_Light/style.json";
            _worldLayer = new MvtTilesAsyncLayer(wvtServerUri);
            _worldLayer.ProjectionConverter = new ProjectionConverter(3857, 4326);

            // Add the layer to the overlay we created earlier.
            layerOverlay.Layers.Add("world layer", _worldLayer);

            mapView.CenterPoint = new PointShape(5.92651, 14.58364);
            mapView.CurrentScale = 147648000;

            _initialized = true;
            _ = mapView.RefreshAsync();
        }

        private async void Radial_CheckChanged(object sender, EventArgs e)
        {
            if (!_initialized)
                return;

            var radioButton = (RadioButton)sender;

            switch (radioButton.Text)
            {
                // Set the new projection converter and open it. Next, set the map to the correct map unit and lastly, set the new extent.

                case "Decimal Degrees":
                    _worldLayer.ProjectionConverter = new ProjectionConverter(3857, 4326);
                    _worldLayer.ProjectionConverter.Open();
                    mapView.MapUnit = GeographyUnit.DecimalDegree;
                    mapView.CenterPoint = new PointShape(5.92651, 14.58364);
                    mapView.CurrentScale = 147648000;
                    break;
                case "MGA Zone 55":
                    _worldLayer.ProjectionConverter = new ProjectionConverter(3857, SampleKeys.ProjString1);
                    _worldLayer.ProjectionConverter.Open();
                    mapView.MapUnit = GeographyUnit.Meter;
                    mapView.CenterPoint = new PointShape(-945060, 7010600);
                    mapView.CurrentScale = 18489350;
                    break;
                case "Equal Area - Albers Conic":
                    _worldLayer.ProjectionConverter = new ProjectionConverter(3857, SampleKeys.ProjString2);
                    _worldLayer.ProjectionConverter.Open();
                    mapView.MapUnit = GeographyUnit.Meter;
                    mapView.CenterPoint = new PointShape(-107570, -246560);
                    mapView.CurrentScale = 36978690;
                    break;
                case "Polar Stereographic":
                    _worldLayer.ProjectionConverter = new ProjectionConverter(3857, SampleKeys.ProjString3);
                    _worldLayer.ProjectionConverter.Open();
                    mapView.MapUnit = GeographyUnit.Meter;
                    mapView.CenterPoint = new PointShape(176160, -818530);
                    mapView.CurrentScale = 73957380;
                    break;
                case "Equal Area - Cylindrical":
                    _worldLayer.ProjectionConverter = new ProjectionConverter(3857, SampleKeys.ProjString4);
                    _worldLayer.ProjectionConverter.Open();
                    mapView.MapUnit = GeographyUnit.Meter;
                    mapView.CenterPoint = new PointShape(-1152760, 796980);
                    mapView.CurrentScale = 147648000;
                    break;
            }

            _ = mapView.RefreshAsync();
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
            this.rdoCylindrical.Text = "Equal Area - Cylindrical";
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
            this.rdoAlbers.Text = "Equal Area - Albers Conic";
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
            this.rdoDecimalDegrees.Checked = true;
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