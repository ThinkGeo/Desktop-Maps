using System;
using System.Windows.Forms;
using ThinkGeo.Core; 
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ZoomToACertainZoomLevel : UserControl
    {
        public ZoomToACertainZoomLevel()
        {
            InitializeComponent();
        }

        private void ZoomToACertainZoomLevel_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.CurrentExtent = new RectangleShape(-15612805, 7675440, -5819082, 1746373);

            ThinkGeoCloudVectorMapsOverlay ThinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(ThinkGeoCloudVectorMapsOverlay);
        }

        private void cmbZoomLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            double scale = GetScaleFromZoomLevelSet(cmbZoomLevel.SelectedIndex);
            mapView.ZoomToScale(scale);
        }

        private static double GetScaleFromZoomLevelSet(int comboboxIndex)
        {
            double scale = 0;
            ZoomLevelSet zoomLevelSet = new ZoomLevelSet();

            switch (comboboxIndex)
            {
                case 0:
                    scale = zoomLevelSet.ZoomLevel10.Scale;
                    break;
                case 1:
                    scale = zoomLevelSet.ZoomLevel09.Scale;
                    break;
                case 2:
                    scale = zoomLevelSet.ZoomLevel08.Scale;
                    break;
                case 3:
                    scale = zoomLevelSet.ZoomLevel07.Scale;
                    break;
                case 4:
                    scale = zoomLevelSet.ZoomLevel06.Scale;
                    break;
                case 5:
                    scale = zoomLevelSet.ZoomLevel05.Scale;
                    break;
                case 6:
                    scale = zoomLevelSet.ZoomLevel04.Scale;
                    break;
                case 7:
                    scale = zoomLevelSet.ZoomLevel03.Scale;
                    break;
                case 8:
                    scale = zoomLevelSet.ZoomLevel02.Scale;
                    break;
                default:
                    break;
            }

            return scale;
        }

        #region Component Designer generated code

        private GroupBox groupBox1;
        private MapView mapView;
        private ComboBox cmbZoomLevel;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbZoomLevel = new System.Windows.Forms.ComboBox();
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cmbZoomLevel);
            this.groupBox1.Location = new System.Drawing.Point(605, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(132, 48);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ZoomLevel";
            //
            // cmbZoomLevel
            //
            this.cmbZoomLevel.AllowDrop = true;
            this.cmbZoomLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbZoomLevel.FormattingEnabled = true;
            this.cmbZoomLevel.Items.AddRange(new object[] {
            "Zoom Level 10",
            "Zoom Level 09",
            "Zoom Level 08",
            "Zoom Level 07",
            "Zoom Level 06",
            "Zoom Level 05",
            "Zoom Level 04",
            "Zoom Level 03",
            "Zoom Level 02"});
            this.cmbZoomLevel.Location = new System.Drawing.Point(6, 19);
            this.cmbZoomLevel.Name = "cmbZoomLevel";
            this.cmbZoomLevel.Size = new System.Drawing.Size(121, 21);
            this.cmbZoomLevel.TabIndex = 0;
            cmbZoomLevel.SelectedIndex = 5;
            this.cmbZoomLevel.SelectedIndexChanged += new System.EventHandler(this.cmbZoomLevel_SelectedIndexChanged);
            //
            // mapView
            //
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            //
            // ZoomToACertainZoomLevel
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mapView);
            this.Name = "ZoomToACertainZoomLevel";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.ZoomToACertainZoomLevel_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}