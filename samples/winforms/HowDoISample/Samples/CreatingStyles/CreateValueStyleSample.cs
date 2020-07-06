using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;
using System.Collections.ObjectModel;


namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CreateValueStyleSample : UserControl
    {
        private readonly ShapeFileFeatureLayer friscoCrime = new ShapeFileFeatureLayer(@"../../../Data/Shapefile/Frisco_Crime.shp");
        private readonly LegendAdornmentLayer legend = new LegendAdornmentLayer();

        public CreateValueStyleSample()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Project the layer's data to match the projection of the map
            friscoCrime.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add friscoCrimeLayer to a LayerOverlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(friscoCrime);

            // Setup the legend adornment
            legend.Title = new LegendItem()
            {
                TextStyle = new TextStyle("Crime Categories", new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black)
            };
            legend.Height = 600;
            legend.Location = AdornmentLocation.LowerRight;
            mapView.AdornmentOverlay.Layers.Add(legend);

            AddValueStyle();

            // Add layerOverlay to the mapView
            mapView.Overlays.Add(layerOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10780196.9469504, 3916119.49665258, -10776231.7761301, 3912703.71697007);
        }


        /// <summary>
        /// Adds a ValueStyle to the friscoCrime layer that represents each OffenseGroup as a different color
        /// </summary>
        private void AddValueStyle()
        {
            // Get all the distinct OffenseGroups in the friscoCrime data
            friscoCrime.Open();
            var offenseGroups = friscoCrime.FeatureSource.GetDistinctColumnValues("OffenseGro");
            friscoCrime.Close();

            // Create a set of colors to represent each OffenseGroup using a spectrum starting from red
            var colors = GeoColor.GetColorsInQualityFamily(GeoColors.Red, offenseGroups.Count);

            // Create a ValueItem styled with a PointStyle to represent each instance of an OffenseGroup
            var valueItems = new Collection<ValueItem>();
            foreach (var offenseGroup in offenseGroups)
            {
                // Create a PointStyle to represent the OffenseGroup by selecting a color using the index of the OffenseGroup
                var style = PointStyle.CreateSimpleCircleStyle(colors[offenseGroups.IndexOf(offenseGroup)], 10,
                    GeoColors.Black, 2);

                // Create a ValueItem that will house the pointStyle for the OffenseGroup
                valueItems.Add(new ValueItem(offenseGroup.ColumnValue, style));

                // Add a LegendItem to the legend adornment
                var legendItem = new LegendItem()
                {
                    ImageStyle = style,
                    TextStyle = new TextStyle(offenseGroup.ColumnValue, new GeoFont("Verdana", 10), GeoBrushes.Black)
                };
                legend.LegendItems.Add(legendItem);
            }

            // Create the ValueStyle that will use the previously created valueItems to style the data using the OffenseGroup column values
            var valueStyle = new ValueStyle("OffenseGro", valueItems);

            // Add the valueStyle to the friscoCrime layer's CustomStyles and apply the style to all ZoomLevels
            friscoCrime.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(valueStyle);
            friscoCrime.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
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
            this.mapView.BackColor = System.Drawing.Color.White;
            this.mapView.CurrentScale = 0D;
            this.mapView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapView.Location = new System.Drawing.Point(0, 0);
            this.mapView.MapResizeMode = ThinkGeo.Core.MapResizeMode.PreserveScale;
            this.mapView.MaximumScale = 1.7976931348623157E+308D;
            this.mapView.MinimumScale = 200D;
            this.mapView.Name = "mapView";
            this.mapView.RestrictExtent = null;
            this.mapView.RotatedAngle = 0F;
            this.mapView.Size = new System.Drawing.Size(1195, 556);
            this.mapView.TabIndex = 0;
            // 
            // CreateValueStyleSample
            // 
            this.Controls.Add(this.mapView);
            this.Name = "CreateValueStyleSample";
            this.Size = new System.Drawing.Size(1195, 556);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}