/*===========================================
    Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
    a Client ID and Secret. These were sent to you via email when you signed up
    with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
===========================================*/

using System;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.WinForms;

namespace CSHowDoISamples
{
    public class DrawUsingFleeBooleanStyle : UserControl
    {
        public DrawUsingFleeBooleanStyle()
        {
            InitializeComponent();
        }

        private void DisplayMap_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.GeographicColors.ShallowOcean);

            // Please input your ThinkGeo Cloud Client ID / Client Secret to enable the background map. 
            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudMapsOverlay = new ThinkGeoCloudRasterMapsOverlay("ThinkGeo Cloud Client ID", "ThinkGeo Cloud Client Secret");
            winformsMap1.Overlays.Add(thinkGeoCloudMapsOverlay);

            // Highlight the countries that are land locked and have a population greater than 10 million
            string expression = "(ToInt32(POP_CNTRY)>10000000) AND (ToChar(LANDLOCKED)='Y')";
            FleeBooleanStyle landLockedCountryStyle = new FleeBooleanStyle(expression);

            // You can access the static methods on these types.  We use this
            // to access the Convert.Toxxx methods to convert variable types
            landLockedCountryStyle.StaticTypes.Add(typeof(System.Convert));
            // The math class might be handy to include but in this sample we do not use it
            //landLockedCountryStyle.StaticTypes.Add(typeof(System.Math));

            landLockedCountryStyle.ColumnVariables.Add("POP_CNTRY");
            landLockedCountryStyle.ColumnVariables.Add("LANDLOCKED");

            landLockedCountryStyle.CustomTrueStyles.Add(new AreaStyle(new GeoPen(GeoColor.SimpleColors.Green, 2), new GeoSolidBrush(GeoColor.FromArgb(100, GeoColor.SimpleColors.Green))));
            landLockedCountryStyle.CustomFalseStyles.Add(AreaStyles.CreateSimpleAreaStyle(GeoColor.SimpleColors.Transparent, GeoColor.FromArgb(100, GeoColor.SimpleColors.Green)));

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(Samples.RootDirectory + @"Data\Countries02.shp", GeoFileReadWriteMode.Read);
            worldLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(landLockedCountryStyle);
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add("WorldOverlay", worldOverlay);

            winformsMap1.CurrentExtent = new RectangleShape(-15495673, 20037508, 13458526, -20037508);
            winformsMap1.Refresh();
        }

        #region Component Designer generated code

        private WinformsMap winformsMap1;
        private Label lblDescription;
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
            this.winformsMap1 = new ThinkGeo.MapSuite.WinForms.WinformsMap();
            this.lblDescription = new System.Windows.Forms.Label();
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
            // lblDescription
            //
            this.lblDescription.BackColor = System.Drawing.Color.LightYellow;
            this.lblDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblDescription.Location = new System.Drawing.Point(3, 4);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(300, 70);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "To use the FleeBooleanStyle functions, you have to reference [Install-Path]\\Devel" +
                "oper Reference\\Spatial Extensions\\FleeStyle Extension\\FleeStyleExtension.dll and" +
                " Ciloci.Flee.dll. And in this sample, the highlight features are the landlocked " +
                "countries whose population is more than 10 million.";
            this.lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // DisplayShapeMap
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.winformsMap1);
            this.Name = "DisplayShapeMap";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DisplayMap_Load);
            this.ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}