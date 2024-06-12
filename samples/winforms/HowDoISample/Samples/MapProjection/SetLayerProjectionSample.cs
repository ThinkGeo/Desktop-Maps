﻿using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class SetLayerProjectionSample : UserControl
    {
        public SetLayerProjectionSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>

        private async void Form_Load(object sender, EventArgs e)
        {
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~", "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the Map Unit to meters (Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Create an overlay that we can add feature layers to, and add it to the MapView
            LayerOverlay subdivisionsOverlay = new LayerOverlay();
            mapView.Overlays.Add("Frisco Subdivisions Overlay", subdivisionsOverlay);

            // Reproject a shapefile and set the extent
            await ReprojectFeaturesFromShapefileAsync();
        }

        /// <summary>
        /// Use the ProjectionConverter class to reproject features in a ShapeFileFeatureLayer
        /// </summary>
        private async Task ReprojectFeaturesFromShapefileAsync()
        {
            // Create a feature layer to hold the Frisco subdivisions data
            ShapeFileFeatureLayer subdivisionsLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Subdivisions.shp");

            // Create a new ProjectionConverter to convert between Texas North Central (2276) and Spherical Mercator (3857)
            ProjectionConverter projectionConverter = new ProjectionConverter(2276, 3857);
            subdivisionsLayer.FeatureSource.ProjectionConverter = projectionConverter;

            // Add a style to use to draw the Frisco subdivions polygons
            subdivisionsLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(50, GeoColors.MediumPurple), GeoColors.MediumPurple, 2);

            // Apply the styles across all zoom levels
            subdivisionsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Get the overlay we prepared from the MapView, and add the subdivisions ShapeFileFeatureLayer to it
            LayerOverlay subdivisionsOverlay = (LayerOverlay)mapView.Overlays["Frisco Subdivisions Overlay"];
            subdivisionsOverlay.Layers.Clear();
            subdivisionsOverlay.Layers.Add("Frisco Subdivisions", subdivisionsLayer);

            // Set the map to the extent of the subdivisions features and refresh the map
            subdivisionsLayer.Open();
            mapView.CurrentExtent = subdivisionsLayer.GetBoundingBox();
            subdivisionsLayer.Close();
            await mapView.RefreshAsync();
        }

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
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
            this.mapView.Size = new System.Drawing.Size(1212, 647);
            this.mapView.TabIndex = 0;
            // 
            // SetLayerProjectionSample
            // 
            this.Controls.Add(this.mapView);
            this.Name = "SetLayerProjectionSample";
            this.Size = new System.Drawing.Size(1212, 647);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}