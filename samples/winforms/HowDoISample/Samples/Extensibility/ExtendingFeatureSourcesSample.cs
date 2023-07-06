using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class ExtendingFeatureSourcesSample: UserControl 
    {
        public ExtendingFeatureSourcesSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // See the implementation of the new layer and feature source below.
            SimpleCsvFeatureLayer csvLayer = new SimpleCsvFeatureLayer(@"../../../Data/Csv/vehicle-route.csv");

            // Set the points image to an car icon and then apply it to all zoomlevels
            PointStyle vehiclePointStyle = new PointStyle(new GeoImage(@"../../../Resources/vehicle-location.png"));
            vehiclePointStyle.YOffsetInPixel = -12;

            csvLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = vehiclePointStyle;
            csvLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay layerOverlay = new LayerOverlay();
            layerOverlay.TileType = TileType.SingleTile;
            layerOverlay.Layers.Add(csvLayer);
            mapView.Overlays.Add(layerOverlay);

            csvLayer.Open();
            mapView.CurrentExtent = csvLayer.GetBoundingBox();
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
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
            this.mapView.ForeColor = System.Drawing.Color.Black;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapFocusMode = ThinkGeo.Core.MapFocusMode.Default;
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1296, 599);
            this.mapView.TabIndex = 0;
            // 
            // ExtendingFeatureSourcesSample
            // 
            this.Controls.Add(this.mapView);
            this.Name = "ExtendingFeatureSourcesSample";
            this.Size = new System.Drawing.Size(1296, 599);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }
        #endregion Component Designer generated code

        // Here we are creating a simple CVS feature source using the minimum set of overloads.
        // Since CSV doesn't include a way to do spatial queries we only need to return all the features
        // in the method below and the base class will do the rest.  Of course if you had large dataset this
        // would be slow so I recommend you look at other overloads and implement optimized versions of these methods

        public class SimpleCsvFeatureSource : FeatureSource
        {
            public string CsvPathFileName { get; set; }

            private Collection<Feature> features;

            public SimpleCsvFeatureSource(string csvPathFileName)
            {
                this.CsvPathFileName = csvPathFileName;
                features = new Collection<Feature>();
            }

            protected override Collection<Feature> GetAllFeaturesCore(IEnumerable<string> returningColumnNames)
            {
                // If we haven't loaded the CSV then load it and return all the features
                if (features.Count == 0)
                {
                    string[] locations = File.ReadAllLines(CsvPathFileName);

                    foreach (var location in locations)
                    {
                        features.Add(new Feature(double.Parse(location.Split(',')[0]), double.Parse(location.Split(',')[1])));
                    }
                }

                return features;
            }
        }

        // We need to create a layer that wraps the feature source.  FeatureLayer has everything we need we just need
        // to provide a constructor and set the feature source and all of the methods on the feature layer just work.
        public class SimpleCsvFeatureLayer : FeatureLayer
        {
            public SimpleCsvFeatureLayer(string csvPathFileName)
            {
                this.FeatureSource = new SimpleCsvFeatureSource(csvPathFileName);
            }

            public override bool HasBoundingBox => true;
        }

    }
}