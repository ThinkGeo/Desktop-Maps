using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class BufferShapeSample: UserControl 
    {
        private readonly ShapeFileFeatureLayer cityLimits = new ShapeFileFeatureLayer(@"./Data/Shapefile/FriscoCityLimits.shp");
        private readonly InMemoryFeatureLayer bufferLayer = new InMemoryFeatureLayer();
        private readonly LayerOverlay layerOverlay = new LayerOverlay();

        public BufferShapeSample()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Setup the map with the ThinkGeo Cloud Maps overlay. Also, add the cityLimits and bufferLayer layers into a grouped LayerOverlay and display them on the map.
        /// </summary>
        private void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Project cityLimits layer to Spherical Mercator to match the map projection
            cityLimits.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Style cityLimits layer
            cityLimits.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Orange), GeoColors.DimGray);
            cityLimits.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Style the bufferLayer
            bufferLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(new GeoColor(32, GeoColors.Green), GeoColors.DimGray);
            bufferLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add cityLimits to a LayerOverlay
            layerOverlay.Layers.Add(cityLimits);

            // Add bufferLayer to the layerOverlay
            layerOverlay.Layers.Add(bufferLayer);

            // Set the map extent to the cityLimits layer bounding box
            cityLimits.Open();
            mapView.CurrentExtent = cityLimits.GetBoundingBox();
            cityLimits.Close();

            // Add LayerOverlay to Map
            mapView.Overlays.Add(layerOverlay);
        }

        private void bufferShape_Click(object sender, EventArgs e)
        {
            // Query the cityLimits layer to get all the features
            cityLimits.Open();
            var features = cityLimits.QueryTools.GetAllFeatures(ReturningColumnsType.NoColumns);
            cityLimits.Close();

            // Buffer the first feature by the amount of the bufferDistance TextBox
            var buffer = features[0].Buffer(Convert.ToInt32(bufferDistance.Text), GeographyUnit.Meter, DistanceUnit.Meter);

            // Add the buffer shape into an InMemoryFeatureLayer to display the result.
            // If this were to be a permanent change to the cityLimits FeatureSource, you would modify the underlying data using BeginTransaction and CommitTransaction instead.
            bufferLayer.InternalFeatures.Clear();
            bufferLayer.InternalFeatures.Add(buffer);

            // Redraw the layerOverlay to see the buffered features on the map
            layerOverlay.Refresh();
        }

        #region Component Designer generated code

        private MapView mapView;
        private Panel panel1;
        private Button bufferShape;
        private TextBox bufferDistance;
        private Label lblbufferDistance;
        private Label label1;

        private void InitializeComponent()
        {
            this.mapView = new ThinkGeo.UI.WinForms.MapView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblbufferDistance = new System.Windows.Forms.Label();
            this.bufferDistance = new System.Windows.Forms.TextBox();
            this.bufferShape = new System.Windows.Forms.Button();
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
            this.mapView.MapUnit = ThinkGeo.Core.GeographyUnit.Meter;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1036, 751);
            this.mapView.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gray;
            this.panel1.Controls.Add(this.bufferShape);
            this.panel1.Controls.Add(this.bufferDistance);
            this.panel1.Controls.Add(this.lblbufferDistance);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1033, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(291, 751);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Buffer Shape Contorls:";
            // 
            // lblbufferDistance
            // 
            this.lblbufferDistance.AutoSize = true;
            this.lblbufferDistance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbufferDistance.ForeColor = System.Drawing.Color.White;
            this.lblbufferDistance.Location = new System.Drawing.Point(11, 50);
            this.lblbufferDistance.Name = "lblbufferDistance";
            this.lblbufferDistance.Size = new System.Drawing.Size(163, 20);
            this.lblbufferDistance.TabIndex = 1;
            this.lblbufferDistance.Text = "Buffer Distance (m):";
            // 
            // bufferDistance
            // 
            this.bufferDistance.Location = new System.Drawing.Point(180, 50);
            this.bufferDistance.Name = "bufferDistance";
            this.bufferDistance.Size = new System.Drawing.Size(97, 22);
            this.bufferDistance.TabIndex = 2;
            this.bufferDistance.Text = "1000";
            // 
            // bufferShape
            // 
            this.bufferShape.Location = new System.Drawing.Point(15, 88);
            this.bufferShape.Name = "bufferShape";
            this.bufferShape.Size = new System.Drawing.Size(262, 34);
            this.bufferShape.TabIndex = 3;
            this.bufferShape.Text = "Buffer";
            this.bufferShape.UseVisualStyleBackColor = true;
            this.bufferShape.Click += new System.EventHandler(this.bufferShape_Click);
            // 
            // BufferShapeSample
            // 
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.mapView);
            this.Name = "BufferShapeSample";
            this.Size = new System.Drawing.Size(1324, 751);
            this.Load += new System.EventHandler(this.Form_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code

    }
}