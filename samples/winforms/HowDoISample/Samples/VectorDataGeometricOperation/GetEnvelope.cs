using System;
using System.Linq;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class GetEnvelope : UserControl
    {
        public GetEnvelope()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light
            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            var cityLimits = new ShapeFileFeatureLayer(@"./Data/Shapefile/FriscoCityLimits.shp");
            var envelopeLayer = new InMemoryFeatureLayer();
            var layerOverlay = new LayerOverlay();

            // Project cityLimits layer to Spherical Mercator to match the map projection
            cityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style cityLimits layer
            cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
            cityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style the envelopeLayer
            envelopeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Green), GeoColors.DimGray);
            envelopeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add cityLimits to a LayerOverlay
            layerOverlay.Layers.Add("cityLimits", cityLimits);

            // Add envelopeLayer to the layerOverlay
            layerOverlay.Layers.Add("envelopeLayer", envelopeLayer);

            // Set the map extent to the cityLimits layer bounding box
            cityLimits.Open();
            mapView.CurrentExtent = cityLimits.GetBoundingBox();
            cityLimits.Close();

            // Add LayerOverlay to Map
            mapView.Overlays.Add("layerOverlay", layerOverlay);

            await mapView.RefreshAsync();
        }

        private async void shapeEnvelope_Click(object sender, EventArgs e)
        {
            var layerOverlay = (LayerOverlay)mapView.Overlays["layerOverlay"];

            var cityLimits = (ShapeFileFeatureLayer)layerOverlay.Layers["cityLimits"];
            var envelopeLayer = (InMemoryFeatureLayer)layerOverlay.Layers["envelopeLayer"];

            // Query the cityLimits layer to get the first feature
            cityLimits.Open();
            var feature = cityLimits.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns).First();
            cityLimits.Close();

            // Get the bounding box (or envelope) of the feature
            var envelope = feature.GetBoundingBox();

            // Add the envelope shape into an InMemoryFeatureLayer to display the result.
            envelopeLayer.InternalFeatures.Clear();
            envelopeLayer.InternalFeatures.Add(new Feature(envelope));

            // Redraw the layerOverlay to see the envelope feature on the map
            await layerOverlay.RefreshAsync();
        }

        #region Component Designer generated code
        private Panel panel1;
        private Button shapeEnvelope;
        private Label label1;

        private MapView mapView;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.shapeEnvelope = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
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
            this.mapView.Size = new System.Drawing.Size(853, 575);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.shapeEnvelope);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(851, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(300, 575);
            this.panel1.TabIndex = 1;
            // 
            // shapeEnvelope
            // 
            this.shapeEnvelope.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shapeEnvelope.Location = new System.Drawing.Point(8, 66);
            this.shapeEnvelope.Name = "shapeEnvelope";
            this.shapeEnvelope.Size = new System.Drawing.Size(289, 31);
            this.shapeEnvelope.TabIndex = 1;
            this.shapeEnvelope.Text = "Get Envelope";
            this.shapeEnvelope.UseVisualStyleBackColor = true;
            this.shapeEnvelope.Click += new System.EventHandler(this.shapeEnvelope_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(178, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Envelope Controls:";
            // 
            // GetEnvelope
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "GetEnvelope";
            this.Size = new System.Drawing.Size(1151, 575);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

    }
}