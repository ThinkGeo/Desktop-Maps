using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class BasicNavigationSample: UserControl
    {
        public BasicNavigationSample()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);
            txtPanPercent.DataBindings.Add("Text", panPercentage, "Value");
            txtCurrentAngle.DataBindings.Add("Text", rotateAngle, "Value");

            await mapView.RefreshAsync();
        }


        private async void zoomIn_Click(object sender, EventArgs e)
        {
            await mapView.ZoomInAsync();
        }

        private async void zoomOut_Click(object sender, EventArgs e)
        {
            await mapView.ZoomOutAsync();
        }

        private async void panNorth_Click(object sender, EventArgs e)
        {
            var percentage = (int)panPercentage.Value;
            switch (((Button)sender).Name)
            {
                case "panNorth":
                    await mapView.PanAsync(PanDirection.Up, percentage);
                    break;
                case "panEast":
                    await mapView.PanAsync(PanDirection.Right, percentage);
                    break;
                case "panWest":
                    await mapView.PanAsync(PanDirection.Left, percentage);
                    break;
                case "panSouth":
                    await mapView.PanAsync(PanDirection.Down, percentage);
                    break;
            }
        }

        private async void panWest_Click(object sender, EventArgs e)
        {
            var percentage = (int)panPercentage.Value;
            switch (((Button)sender).Name)
            {
                case "panNorth":
                    await mapView.PanAsync(PanDirection.Up, percentage);
                    break;
                case "panEast":
                    await mapView.PanAsync(PanDirection.Right, percentage);
                    break;
                case "panWest":
                    await mapView.PanAsync(PanDirection.Left, percentage);
                    break;
                case "panSouth":
                    await mapView.PanAsync(PanDirection.Down, percentage);
                    break;
            }
        }

        private async void panEast_Click(object sender, EventArgs e)
        {
            var percentage = (int)panPercentage.Value;
            switch (((Button)sender).Name)
            {
                case "panNorth":
                    await mapView.PanAsync(PanDirection.Up, percentage);
                    break;
                case "panEast":
                    await mapView.PanAsync(PanDirection.Right, percentage);
                    break;
                case "panWest":
                    await mapView.PanAsync(PanDirection.Left, percentage);
                    break;
                case "panSouth":
                    await mapView.PanAsync(PanDirection.Down, percentage);
                    break;
            }
        }

        private async void panSouth_Click(object sender, EventArgs e)
        {
            var percentage = (int)panPercentage.Value;
            switch (((Button)sender).Name)
            {
                case "panNorth":
                    await mapView.PanAsync(PanDirection.Up, percentage);
                    break;
                case "panEast":
                    await mapView.PanAsync(PanDirection.Right, percentage);
                    break;
                case "panWest":
                    await mapView.PanAsync(PanDirection.Left, percentage);
                    break;
                case "panSouth":
                    await mapView.PanAsync(PanDirection.Down, percentage);
                    break;
            }
        }

        private async void rotate_Click(object sender, EventArgs e)
        {
            mapView.RotatedAngle = (float)rotateAngle.Value;
            await mapView.RefreshAsync();
        }

        #region Component Designer generated code
        private Panel panel1;
        private Label label1;
        private TextBox txtCurrentAngle;
        private TextBox txtPanPercent;
        private Label label4;
        private Button zoomOut;
        private Button zoomIn;
        private Button rotate;
        private TrackBar rotateAngle;
        private Label label3;
        private TrackBar panPercentage;
        private Label label2;
        private Button panWest;
        private Button panSouth;
        private Button panEast;
        private Button panNorth;


        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCurrentAngle = new System.Windows.Forms.TextBox();
            this.txtPanPercent = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.zoomOut = new System.Windows.Forms.Button();
            this.zoomIn = new System.Windows.Forms.Button();
            this.rotate = new System.Windows.Forms.Button();
            this.rotateAngle = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.panPercentage = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.panWest = new System.Windows.Forms.Button();
            this.panSouth = new System.Windows.Forms.Button();
            this.panEast = new System.Windows.Forms.Button();
            this.panNorth = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rotateAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panPercentage)).BeginInit();
            this.SuspendLayout();
            // 
            // mapView
            // 
            this.mapView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(891, 560);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.txtCurrentAngle);
            this.panel1.Controls.Add(this.txtPanPercent);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.zoomOut);
            this.panel1.Controls.Add(this.zoomIn);
            this.panel1.Controls.Add(this.rotate);
            this.panel1.Controls.Add(this.rotateAngle);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.panPercentage);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.panWest);
            this.panel1.Controls.Add(this.panSouth);
            this.panel1.Controls.Add(this.panEast);
            this.panel1.Controls.Add(this.panNorth);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(894, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 560);
            this.panel1.TabIndex = 1;
            // 
            // txtCurrentAngle
            // 
            this.txtCurrentAngle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCurrentAngle.Location = new System.Drawing.Point(250, 371);
            this.txtCurrentAngle.Name = "txtCurrentAngle";
            this.txtCurrentAngle.ReadOnly = true;
            this.txtCurrentAngle.Size = new System.Drawing.Size(46, 27);
            this.txtCurrentAngle.TabIndex = 14;
            this.txtCurrentAngle.Text = "0";
            // 
            // txtPanPercent
            // 
            this.txtPanPercent.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPanPercent.Location = new System.Drawing.Point(250, 285);
            this.txtPanPercent.Name = "txtPanPercent";
            this.txtPanPercent.ReadOnly = true;
            this.txtPanPercent.Size = new System.Drawing.Size(46, 27);
            this.txtPanPercent.TabIndex = 13;
            this.txtPanPercent.Text = "50";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(18, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 25);
            this.label4.TabIndex = 12;
            this.label4.Text = "Panning:";
            // 
            // zoomOut
            // 
            this.zoomOut.Image = global::ThinkGeo.UI.WinForms.HowDoI.Properties.Resources.ZoomOut;
            this.zoomOut.Location = new System.Drawing.Point(150, 57);
            this.zoomOut.Name = "zoomOut";
            this.zoomOut.Size = new System.Drawing.Size(147, 36);
            this.zoomOut.TabIndex = 11;
            this.zoomOut.UseVisualStyleBackColor = true;
            this.zoomOut.Click += new System.EventHandler(this.zoomOut_Click);
            // 
            // zoomIn
            // 
            this.zoomIn.Image = global::ThinkGeo.UI.WinForms.HowDoI.Properties.Resources.ZoomIn;
            this.zoomIn.Location = new System.Drawing.Point(3, 57);
            this.zoomIn.Name = "zoomIn";
            this.zoomIn.Size = new System.Drawing.Size(141, 36);
            this.zoomIn.TabIndex = 10;
            this.zoomIn.UseVisualStyleBackColor = true;
            this.zoomIn.Click += new System.EventHandler(this.zoomIn_Click);
            // 
            // rotate
            // 
            this.rotate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.rotate.ForeColor = System.Drawing.Color.Black;
            this.rotate.Location = new System.Drawing.Point(3, 416);
            this.rotate.Name = "rotate";
            this.rotate.Size = new System.Drawing.Size(293, 35);
            this.rotate.TabIndex = 9;
            this.rotate.Text = "Update";
            this.rotate.UseVisualStyleBackColor = true;
            this.rotate.Click += new System.EventHandler(this.rotate_Click);
            // 
            // rotateAngle
            // 
            this.rotateAngle.Location = new System.Drawing.Point(33, 371);
            this.rotateAngle.Maximum = 360;
            this.rotateAngle.Name = "rotateAngle";
            this.rotateAngle.Size = new System.Drawing.Size(215, 56);
            this.rotateAngle.TabIndex = 8;
            this.rotateAngle.TickStyle = System.Windows.Forms.TickStyle.None;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(29, 348);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Current Angle:";
            // 
            // panPercentage
            // 
            this.panPercentage.Location = new System.Drawing.Point(29, 285);
            this.panPercentage.Maximum = 100;
            this.panPercentage.Name = "panPercentage";
            this.panPercentage.Size = new System.Drawing.Size(215, 56);
            this.panPercentage.TabIndex = 6;
            this.panPercentage.TickStyle = System.Windows.Forms.TickStyle.None;
            this.panPercentage.Value = 50;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(25, 251);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(164, 20);
            this.label2.TabIndex = 5;
            this.label2.Text = "Panning Percentage:";
            // 
            // panWest
            // 
            this.panWest.Image = global::ThinkGeo.UI.WinForms.HowDoI.Properties.Resources.West;
            this.panWest.Location = new System.Drawing.Point(76, 178);
            this.panWest.Name = "panWest";
            this.panWest.Size = new System.Drawing.Size(42, 34);
            this.panWest.TabIndex = 4;
            this.panWest.UseVisualStyleBackColor = true;
            this.panWest.Click += new System.EventHandler(this.panWest_Click);
            // 
            // panSouth
            // 
            this.panSouth.Image = global::ThinkGeo.UI.WinForms.HowDoI.Properties.Resources.South;
            this.panSouth.Location = new System.Drawing.Point(124, 214);
            this.panSouth.Name = "panSouth";
            this.panSouth.Size = new System.Drawing.Size(42, 34);
            this.panSouth.TabIndex = 3;
            this.panSouth.UseVisualStyleBackColor = true;
            this.panSouth.Click += new System.EventHandler(this.panSouth_Click);
            // 
            // panEast
            // 
            this.panEast.Image = global::ThinkGeo.UI.WinForms.HowDoI.Properties.Resources.East;
            this.panEast.Location = new System.Drawing.Point(172, 178);
            this.panEast.Name = "panEast";
            this.panEast.Size = new System.Drawing.Size(42, 34);
            this.panEast.TabIndex = 2;
            this.panEast.UseVisualStyleBackColor = true;
            this.panEast.Click += new System.EventHandler(this.panEast_Click);
            // 
            // panNorth
            // 
            this.panNorth.Image = global::ThinkGeo.UI.WinForms.HowDoI.Properties.Resources.North;
            this.panNorth.Location = new System.Drawing.Point(124, 141);
            this.panNorth.Name = "panNorth";
            this.panNorth.Size = new System.Drawing.Size(42, 34);
            this.panNorth.TabIndex = 1;
            this.panNorth.UseVisualStyleBackColor = true;
            this.panNorth.Click += new System.EventHandler(this.panNorth_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(18, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Zooming:";
            // 
            // BasicNavigationSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "BasicNavigationSample";
            this.Size = new System.Drawing.Size(1194, 560);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.rotateAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panPercentage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

    }
}