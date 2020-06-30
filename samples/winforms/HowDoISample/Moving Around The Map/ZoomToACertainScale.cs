using System;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.Core; 
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ZoomToACertainScale : UserControl
    {
        public ZoomToACertainScale()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-15612805, 7675440, -5819082, 1746373);

            ThinkGeoCloudVectorMapsOverlay ThinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(ThinkGeoCloudVectorMapsOverlay);
        }

        private void cmbScale_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Zoom to a certain scale.
            string zoomLevelScale = cmbScale.SelectedItem.ToString();
            double scale = Convert.ToDouble(zoomLevelScale.Split(':')[1], CultureInfo.InvariantCulture);
            mapView.ZoomToScale(scale);
        }

        #region Component Designer generated code

        private GroupBox groupBox1;
        private MapView mapView;
        private ComboBox cmbScale;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbScale = new System.Windows.Forms.ComboBox();
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.cmbScale);
            this.groupBox1.Location = new System.Drawing.Point(597, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(140, 53);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Target Scale";
            //
            // cmbScale
            //
            this.cmbScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScale.FormattingEnabled = true;
            this.cmbScale.Items.AddRange(new object[] {
            "1:1,000,000",
            "1:5,000,000",
            "1:10,000,000",
            "1:50,000,000",
            "1:100,000,000",
            "1:500,000,000"});
            this.cmbScale.Location = new System.Drawing.Point(6, 19);
            this.cmbScale.Name = "cmbScale";
            this.cmbScale.Size = new System.Drawing.Size(121, 21);
            this.cmbScale.SelectedIndex = 4;
            this.cmbScale.TabIndex = 0;
            this.cmbScale.SelectedIndexChanged += new System.EventHandler(this.cmbScale_SelectedIndexChanged);
            //
            // mapView
            //
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            //
            // ZoomToACertainScale
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mapView);
            this.Name = "ZoomToACertainScale";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.Form_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}