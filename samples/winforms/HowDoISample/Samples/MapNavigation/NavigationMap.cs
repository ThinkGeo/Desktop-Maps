using System;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class NavigationMap : UserControl
    {
        public NavigationMap()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light
            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);
            txtCurrentAngle.DataBindings.Add("Text", rotateAngle, "Value");

            _ = mapView.RefreshAsync();
        }

        private void zoomIn_Click(object sender, EventArgs e)
        {
            _ = mapView.ZoomInAsync();
        }

        private void zoomOut_Click(object sender, EventArgs e)
        {
            _ = mapView.ZoomOutAsync();
        }

        private void panNorth_Click(object sender, EventArgs e)
        {
            _ = mapView.PanByDirectionAndScreenDistanceAsync(PanDirection.Up, 200);
        }

        private void panWest_Click(object sender, EventArgs e)
        {
            _ = mapView.PanByDirectionAndScreenDistanceAsync(PanDirection.Left, 200);
        }

        private void panEast_Click(object sender, EventArgs e)
        {
            _ = mapView.PanByDirectionAndScreenDistanceAsync(PanDirection.Right, 200);
        }

        private void panSouth_Click(object sender, EventArgs e)
        {
            _ = mapView.PanByDirectionAndScreenDistanceAsync(PanDirection.Down, 200);
        }


        private void rotateAngle_ValueChanged(object sender, EventArgs e)
        {
            var value = rotateAngle.Value;
            _ = mapView.ZoomToAsync(mapView.CenterPoint, mapView.CurrentScale, rotateAngle.Value);
        }



        #region Component Designer generated code
        private Panel panel1;
        private Label label1;
        private TextBox txtCurrentAngle;
        private Label label4;
        private Button zoomOut;
        private Button zoomIn;
        private TrackBar rotateAngle;
        private Label label3;
        private Button panWest;
        private Button panSouth;
        private Button panEast;
        private Button panNorth;


        private MapView mapView;

        private void InitializeComponent()
        {
            mapView = new ThinkGeo.UI.WinForms.MapView();
            panel1 = new Panel();
            txtCurrentAngle = new TextBox();
            label4 = new Label();
            zoomOut = new Button();
            zoomIn = new Button();
            rotateAngle = new TrackBar();
            label3 = new Label();
            panWest = new Button();
            panSouth = new Button();
            panEast = new Button();
            panNorth = new Button();
            label1 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)rotateAngle).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            panel1.BackColor = System.Drawing.Color.Gray;
            panel1.Controls.Add(txtCurrentAngle);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(zoomOut);
            panel1.Controls.Add(zoomIn);
            panel1.Controls.Add(rotateAngle);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(panWest);
            panel1.Controls.Add(panSouth);
            panel1.Controls.Add(panEast);
            panel1.Controls.Add(panNorth);
            panel1.Controls.Add(label1);
            panel1.Location = new System.Drawing.Point(894, 0);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(300, 560);
            panel1.TabIndex = 1;  
            // 
            // mapView
            // 
            this.mapView.Dock = DockStyle.Fill;
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotationAngle = 0F;
            this.mapView.TabIndex = 0;
            // 
            // txtCurrentAngle
            // 
            txtCurrentAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            txtCurrentAngle.Location = new System.Drawing.Point(250, 371);
            txtCurrentAngle.Name = "txtCurrentAngle";
            txtCurrentAngle.ReadOnly = true;
            txtCurrentAngle.Size = new System.Drawing.Size(46, 31);
            txtCurrentAngle.TabIndex = 14;
            txtCurrentAngle.Text = "0";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            label4.ForeColor = System.Drawing.Color.White;
            label4.Location = new System.Drawing.Point(18, 96);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(107, 29);
            label4.TabIndex = 12;
            label4.Text = "Panning:";
            // 
            // zoomOut
            // 
            zoomOut.Image = Properties.Resources.ZoomOut;
            zoomOut.Location = new System.Drawing.Point(150, 57);
            zoomOut.Name = "zoomOut";
            zoomOut.Size = new System.Drawing.Size(147, 36);
            zoomOut.TabIndex = 11;
            zoomOut.UseVisualStyleBackColor = true;
            zoomOut.Click += zoomOut_Click;
            // 
            // zoomIn
            // 
            zoomIn.Image = Properties.Resources.ZoomIn;
            zoomIn.Location = new System.Drawing.Point(3, 57);
            zoomIn.Name = "zoomIn";
            zoomIn.Size = new System.Drawing.Size(141, 36);
            zoomIn.TabIndex = 10;
            zoomIn.UseVisualStyleBackColor = true;
            zoomIn.Click += zoomIn_Click;
            // 
            // rotateAngle
            // 
            rotateAngle.LargeChange = 15;
            rotateAngle.Location = new System.Drawing.Point(0, 371);
            rotateAngle.Maximum = 360;
            rotateAngle.Name = "rotateAngle";
            rotateAngle.Size = new System.Drawing.Size(248, 69);
            rotateAngle.TabIndex = 8;
            rotateAngle.TickStyle = TickStyle.None;
            rotateAngle.ValueChanged += rotateAngle_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            label3.ForeColor = System.Drawing.Color.White;
            label3.Location = new System.Drawing.Point(5, 343);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(139, 25);
            label3.TabIndex = 7;
            label3.Text = "Current Angle:";
            // 
            // panWest
            // 
            panWest.Image = Properties.Resources.West;
            panWest.Location = new System.Drawing.Point(76, 178);
            panWest.Name = "panWest";
            panWest.Size = new System.Drawing.Size(42, 34);
            panWest.TabIndex = 4;
            panWest.UseVisualStyleBackColor = true;
            panWest.Click += panWest_Click;
            // 
            // panSouth
            // 
            panSouth.Image = Properties.Resources.South;
            panSouth.Location = new System.Drawing.Point(124, 214);
            panSouth.Name = "panSouth";
            panSouth.Size = new System.Drawing.Size(42, 34);
            panSouth.TabIndex = 3;
            panSouth.UseVisualStyleBackColor = true;
            panSouth.Click += panSouth_Click;
            // 
            // panEast
            // 
            panEast.Image = Properties.Resources.East;
            panEast.Location = new System.Drawing.Point(172, 178);
            panEast.Name = "panEast";
            panEast.Size = new System.Drawing.Size(42, 34);
            panEast.TabIndex = 2;
            panEast.UseVisualStyleBackColor = true;
            panEast.Click += panEast_Click;
            // 
            // panNorth
            // 
            panNorth.Image = Properties.Resources.North;
            panNorth.Location = new System.Drawing.Point(124, 141);
            panNorth.Name = "panNorth";
            panNorth.Size = new System.Drawing.Size(42, 34);
            panNorth.TabIndex = 1;
            panNorth.UseVisualStyleBackColor = true;
            panNorth.Click += panNorth_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            label1.ForeColor = System.Drawing.Color.White;
            label1.Location = new System.Drawing.Point(18, 20);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(114, 29);
            label1.TabIndex = 0;
            label1.Text = "Zooming:";
            // 
            // NavigationMap
            // 
            Controls.Add(panel1);
            this.Controls.Add(this.mapView);
            Name = "NavigationMap";
            Size = new System.Drawing.Size(1194, 560);
            Load += Form_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)rotateAngle).EndInit();
            ResumeLayout(false);
        }

        #endregion Component Designer generated code

    }
}