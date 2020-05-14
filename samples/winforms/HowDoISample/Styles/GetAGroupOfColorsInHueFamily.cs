using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class GetAGroupOfColorsInHueFamily : UserControl
    {
        public GetAGroupOfColorsInHueFamily()
        {
            InitializeComponent();
        }

        private void DisplayMap_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green)));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Draw a feature based on a value and hue family colors.
            Collection<GeoColor> colorsInFamily = GeoColor.GetColorsInHueFamily(GeoColors.Red, 5);
            ValueStyle valueStyle = new ValueStyle();
            valueStyle.ColumnName = "CNTRY_NAME";
            valueStyle.ValueItems.Add(new ValueItem("United States", new AreaStyle(new GeoSolidBrush(colorsInFamily[0]))));
            valueStyle.ValueItems.Add(new ValueItem("China", new AreaStyle(new GeoSolidBrush(colorsInFamily[1]))));
            valueStyle.ValueItems.Add(new ValueItem("Brazil", new AreaStyle(new GeoSolidBrush(colorsInFamily[2]))));
            valueStyle.ValueItems.Add(new ValueItem("Australia", new AreaStyle(new GeoSolidBrush(colorsInFamily[3]))));
            valueStyle.ValueItems.Add(new ValueItem("South Africa", new AreaStyle(new GeoSolidBrush(colorsInFamily[4]))));
            worldLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(valueStyle);

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add(worldOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-139.2, 92.4, 120.9, -93.2);
            winformsMap1.Refresh();
        }

        private MapView winformsMap1;

        #region Component Designer generated code

        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.SuspendLayout();
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 0;
            this.winformsMap1.Text = "winformsMap1";
            //
            // GetAGroupOfColorsInHueFamily
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.winformsMap1);
            this.Name = "GetAGroupOfColorsInHueFamily";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DisplayMap_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}