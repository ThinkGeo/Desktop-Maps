using System;
using System.Windows.Forms;
using ThinkGeo.Core;
using ThinkGeo.UI.WinForms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public class CreateFilterStyleSample : UserControl
    {
        private readonly ShapeFileFeatureLayer friscoCrimeLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco_Crime.shp");

        public CreateFilterStyleSample()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            mapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            mapView.CurrentExtent = new RectangleShape(-10780196.9469504, 3916119.49665258, -10776231.7761301, 3912703.71697007);

            ShapeFileFeatureLayer friscoCrimeLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/Frisco_Crime.shp");

            // Project the layer's data to match the projection of the map
            friscoCrimeLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            // Add friscoCrimeLayer to a LayerOverlay
            var layerOverlay = new LayerOverlay();
            layerOverlay.Layers.Add(friscoCrimeLayer);

            AddFilterStyle(friscoCrimeLayer);

            // Add layerOverlay to the mapView
            mapView.Overlays.Add(layerOverlay);

            await mapView.RefreshAsync();
        }


        /// <summary>
        /// Adds a filter style to various categories of the Frisco Crime layer
        /// </summary>
        private void AddFilterStyle(ShapeFileFeatureLayer layer)
        {
            // Create a filter style based on the "Drugs" Offense Group 
            var drugFilterStyle = new FilterStyle()
            {
                Conditions = { new FilterCondition("OffenseGro", "Drugs") },
                Styles = {
                    new PointStyle(PointSymbolType.Circle, 28, GeoBrushes.White,GeoPens.Red),
                    new PointStyle(new GeoImage(@"../../../Resources/drugs_icon.png")) { ImageScale = .60 }
                }
            };

            // Create a filter style based on the "Weapons" Offense Group 
            var weaponFilterStyle = new FilterStyle()
            {
                Conditions = { new FilterCondition("OffenseGro", "Weapons") },
                Styles = {
                    new PointStyle(PointSymbolType.Circle, 28, GeoBrushes.White,GeoPens.Red),
                    new PointStyle(new GeoImage(@"../../../Resources/weapon_icon.png")) { ImageScale = .25 }
                }
            };

            // Create a filter style based on the "Vandalism" Offense Group 
            var vandalismFilterStyle = new FilterStyle()
            {
                Conditions = { new FilterCondition("OffenseGro", "Vandalism") },
                Styles = {
                    new PointStyle(PointSymbolType.Circle, 28, GeoBrushes.White,GeoPens.Red),
                    new PointStyle(new GeoImage(@"../../../Resources/vandalism_icon.png")) { ImageScale = .25 }
                }
            };

            // Add the filter styles to the CustomStyles collection
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(drugFilterStyle);
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(weaponFilterStyle);
            layer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(vandalismFilterStyle);
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
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
            this.mapView.Size = new System.Drawing.Size(1175, 567);
            this.mapView.TabIndex = 0;
            // 
            // CreateFilterStyleSample
            // 
            this.Controls.Add(this.mapView);
            this.Name = "CreateFilterStyleSample";
            this.Size = new System.Drawing.Size(1175, 567);
            this.Load += new System.EventHandler(this.Form_Load);
            this.ResumeLayout(false);

        }

        #endregion Component Designer generated code
    }
}