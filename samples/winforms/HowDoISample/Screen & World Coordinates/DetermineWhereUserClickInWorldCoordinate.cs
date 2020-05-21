﻿using System;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.Core; using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class DetermineWhereUserClickInWorldCoordinate : UserControl
    {
        public DetermineWhereUserClickInWorldCoordinate()
        {
            InitializeComponent();
        }

        private void DetermineWhereInWorldCoordinate_Load(object sender, EventArgs e)
        {
            winformsMap1.MapUnit = GeographyUnit.Meter;
            winformsMap1.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            ThinkGeoCloudRasterMapsOverlay thinkGeoCloudRasterMapsOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            winformsMap1.Overlays.Add(thinkGeoCloudRasterMapsOverlay);

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02.shp"));
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green));
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            ProjectionConverter proj = new ProjectionConverter(4326, 3857);
            worldLayer.FeatureSource.ProjectionConverter = proj;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.TileCache = new FileRasterTileCache(@"c:\share\wmk\", "wmk3");
            worldOverlay.TileCache.TileAccessMode = TileAccessMode.ReadOnly;
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            winformsMap1.Overlays.Add(worldOverlay);

            winformsMap1.MapClick += new EventHandler<MapClickMapViewEventArgs>(winformsMap1_MapClick);

            winformsMap1.CurrentExtent = new RectangleShape(-13939426.6371, 6701997.4056, -7812401.86, 2626987.38696);
            winformsMap1.Refresh();
        }

        private void winformsMap1_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            txtXCoordinate.Text = e.WorldX.ToString("N4", CultureInfo.InvariantCulture);
            txtYCoordinate.Text = e.WorldY.ToString("N4", CultureInfo.InvariantCulture);
        }

        private MapView winformsMap1;

        #region Component Designer generated code

        /// <summary>
        /// Required designer variable.
        /// </summary>
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtYCoordinate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtXCoordinate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.winformsMap1 = new ThinkGeo.UI.WinForms.MapView();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            //
            // groupBox1
            //
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtYCoordinate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtXCoordinate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(551, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(186, 44);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "World Coordinates";
            //
            // txtYCoordinate
            //
            this.txtYCoordinate.Location = new System.Drawing.Point(115, 16);
            this.txtYCoordinate.Name = "txtYCoordinate";
            this.txtYCoordinate.ReadOnly = true;
            this.txtYCoordinate.Size = new System.Drawing.Size(55, 20);
            this.txtYCoordinate.TabIndex = 10;
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Y:";
            //
            // txtXCoordinate
            //
            this.txtXCoordinate.Location = new System.Drawing.Point(27, 16);
            this.txtXCoordinate.Name = "txtXCoordinate";
            this.txtXCoordinate.ReadOnly = true;
            this.txtXCoordinate.Size = new System.Drawing.Size(55, 20);
            this.txtXCoordinate.TabIndex = 11;
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "X:";
            //
            // winformsMap1
            //
            this.winformsMap1.BackColor = System.Drawing.Color.White;
            this.winformsMap1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.winformsMap1.Location = new System.Drawing.Point(0, 0);
            this.winformsMap1.Name = "winformsMap1";
            this.winformsMap1.Size = new System.Drawing.Size(740, 528);
            this.winformsMap1.TabIndex = 8;
            this.winformsMap1.Text = "winformsMap1";
            //
            // DetermineWhereUserClickInWorldCoordinate
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.winformsMap1);
            this.Name = "DetermineWhereUserClickInWorldCoordinate";
            this.Size = new System.Drawing.Size(740, 528);
            this.Load += new System.EventHandler(this.DetermineWhereInWorldCoordinate_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
        }

        private GroupBox groupBox1;
        private TextBox txtYCoordinate;
        private Label label2;
        private TextBox txtXCoordinate;
        private Label label1;

        #endregion Component Designer generated code
    }
}