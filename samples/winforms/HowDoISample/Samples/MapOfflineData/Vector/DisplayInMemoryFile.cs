﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.Core;


namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class DisplayInMemoryFile : UserControl
    {
        public DisplayInMemoryFile()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light

            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Create a new overlay that will hold our new layer and add it to the map.
            var inMemoryOverlay = new LayerOverlay();
            mapView.Overlays.Add(inMemoryOverlay);

            // Create a new layer that we will pull features from to populate the in memory layer.
            var shapeFileLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco_Mosquitos.shp")
            {
                FeatureSource =
                    {
                        ProjectionConverter = new ProjectionConverter(2276, 3857)
                    }
            };
            shapeFileLayer.Open();

            // Get all the features from the above layer.
            Collection<Feature> features = shapeFileLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);
            shapeFileLayer.Close();

            // Create the in memory layer and add it to the map
            var inMemoryFeatureLayer = new InMemoryFeatureLayer();
            inMemoryOverlay.Layers.Add("Frisco Mosquitos", inMemoryFeatureLayer);

            // Loop through all the features in the first layer and add them to the in memory layer.  We use a shortcut called internal 
            // features that is supported in the in memory layer instead of going through the edit tools
            foreach (var feature in features)
            {
                inMemoryFeatureLayer.InternalFeatures.Add(feature);
            }

            // Create a text style for the label and give it a mask for use below.
            var textStyle = new TextStyle("Trap: [TrapID]", new GeoFont("ariel", 14), GeoBrushes.Black)
            {
                Mask = new AreaStyle(GeoPens.Black, GeoBrushes.White),
                MaskMargin = new DrawingMargin(2, 2, 2, 2),
                YOffsetInPixel = -10
            };

            // Create a point style and add the text style from above on zoom level 1 and then apply it to all zoom levels up to 20.            
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Circle, 12, GeoBrushes.Red, GeoPens.White);
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = textStyle;
            inMemoryFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Open the layer and set the map view current extent to the bounding box of the layer.  
            inMemoryFeatureLayer.Open();
            mapView.CurrentExtent = inMemoryFeatureLayer.GetBoundingBox();

            //Refresh the map.
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
            this.mapView.Size = new System.Drawing.Size(1057, 660);
            this.mapView.TabIndex = 0;
            // 
            // DisplayInMemoryFile
            // 
            this.Controls.Add(this.mapView);
            this.Name = "DisplayInMemoryFile";
            this.Size = new System.Drawing.Size(1057, 660);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}