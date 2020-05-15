using System;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class DisplayThinkGeoCloudVectorMaps : UserControl
    {
        private ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay;

        public DisplayThinkGeoCloudVectorMaps()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            mapView.CurrentExtent = new RectangleShape(-12922411, 8734539, -8568181, 687275);
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonLight.Checked)
            {
                thinkGeoCloudVectorMapsOverlay.MapType = ThinkGeoCloudVectorMapsMapType.Light;
            }
            else if (radioButtonDark.Checked)
            {
                thinkGeoCloudVectorMapsOverlay.MapType = ThinkGeoCloudVectorMapsMapType.Dark;
            }
            else if (radioButtonCustomized.Checked)
            {
                thinkGeoCloudVectorMapsOverlay.MapType = ThinkGeoCloudVectorMapsMapType.CustomizedByStyleJson;
                thinkGeoCloudVectorMapsOverlay.StyleJsonUri = new Uri(Environment.CurrentDirectory + @"\SampleData\thinkgeo-world-streets-cobalt.json");
            }
            thinkGeoCloudVectorMapsOverlay.Refresh();
        }

        #region Component Designer generated code

        private GroupBox groupBox1;
        private RadioButton radioButtonCustomized;
        private RadioButton radioButtonDark;
        private RadioButton radioButtonLight;
        private MapView mapView;


        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayThinkGeoCloudVectorMaps));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.radioButtonLight = new System.Windows.Forms.RadioButton();
            this.radioButtonDark = new System.Windows.Forms.RadioButton();
            this.radioButtonCustomized = new System.Windows.Forms.RadioButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioButtonCustomized);
            this.groupBox1.Controls.Add(this.radioButtonDark);
            this.groupBox1.Controls.Add(this.radioButtonLight);
            this.groupBox1.Location = new System.Drawing.Point(585, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(152, 83);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Styles";
            //
            // mapView
            //
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.Controls.Add(this.mapView);
            // 
            // radioButtonLight
            // 
            this.radioButtonLight.AutoSize = true;
            this.radioButtonLight.Checked = true;
            this.radioButtonLight.Location = new System.Drawing.Point(13, 19);
            this.radioButtonLight.Name = "radioButtonLight";
            this.radioButtonLight.Size = new System.Drawing.Size(48, 17);
            this.radioButtonLight.TabIndex = 14;
            this.radioButtonLight.TabStop = true;
            this.radioButtonLight.Text = "Light";
            this.radioButtonLight.UseVisualStyleBackColor = true;
            this.radioButtonLight.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButtonDark
            // 
            this.radioButtonDark.AutoSize = true;
            this.radioButtonDark.Location = new System.Drawing.Point(12, 38);
            this.radioButtonDark.Name = "radioButtonDark";
            this.radioButtonDark.Size = new System.Drawing.Size(48, 17);
            this.radioButtonDark.TabIndex = 14;
            this.radioButtonDark.Text = "Dark";
            this.radioButtonDark.UseVisualStyleBackColor = true;
            this.radioButtonDark.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButtonCustomized.AutoSize = true;
            this.radioButtonCustomized.Location = new System.Drawing.Point(12, 57);
            this.radioButtonCustomized.Name = "radioButton3";
            this.radioButtonCustomized.Size = new System.Drawing.Size(142, 17);
            this.radioButtonCustomized.TabIndex = 14;
            this.radioButtonCustomized.Text = "CustomizedByStyleJSON";
            this.radioButtonCustomized.UseVisualStyleBackColor = true;
            this.radioButtonCustomized.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // DisplayThinkGeoCloudRasterMaps
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mapView);
            this.Name = "DisplayThinkGeoCloudRasterMaps";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.Form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}