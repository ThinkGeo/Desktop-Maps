using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    /// <summary>
    /// This sample shows learn how to work with different shapes across the Date Line.
    /// </summary>
    public partial class WrapTheDateLine : UserControl
    {
        public WrapTheDateLine()
        {
            InitializeComponent();
        }

        private async void Form_Load(object sender, EventArgs e)
        {
            var overlay = new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Light_V1_X2,
                WrappingMode = WrappingMode.WrapDateline,
                WrappingExtent = MaxExtents.ThinkGeoMaps
            };

            mapView.MapUnit = GeographyUnit.Meter;
            mapView.Overlays.Add(overlay);
            mapView.CurrentExtent = MaxExtents.ThinkGeoMaps;

            var zoomLevelSet = new SphericalMercatorZoomLevelSet();
            var selectedScales = new List<double>
            {
                zoomLevelSet.ZoomLevel01.Scale
            };
            mapView.ZoomScales = new Collection<double>(selectedScales);
            mapView.MapTools.PanZoomBar.IsEnabled = false;

            var layer = new InMemoryFeatureLayer();
            layer.InternalFeatures.Add(GetExtent(-0.6, -0.4, 0.9, 0.99, "-0.6 ~ -0.4"));
            layer.InternalFeatures.Add(GetExtent(-0.6, 0, 0.8, 0.89, "-0.6 ~ 0"));
            layer.InternalFeatures.Add(GetExtent(-0.3, 0.3, 0.7, 0.79, "-0.3 ~ 0.3"));
            layer.InternalFeatures.Add(GetExtent(-0.6, 1, 0.6, 0.69, "-0.6 ~ 1"));
            layer.InternalFeatures.Add(GetExtent(-0.6, 1.8, 0.5, 0.59, "0.6 ~ 1.8,"));
            layer.InternalFeatures.Add(GetExtent(-1.5, -0.5, 0.4, 0.49, "-1.5 ~ -0.5"));
            layer.InternalFeatures.Add(GetExtent(0.4, 0.8, 0.3, 0.39, "0.4 ~ 0.8"));
            layer.InternalFeatures.Add(GetExtent(0.4, 1.8, 0.2, 0.29, "0.4 ~ 1.8"));
            layer.InternalFeatures.Add(GetExtent(1, 1.8, 0.1, 0.19, "1 ~ 1.8"));
            layer.InternalFeatures.Add(GetExtent(1.4, 1.8, 0.0, 0.09, "1.4 ~ 1.8"));

            layer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(100, GeoColors.Brown), GeoColors.LightRed);
            layer.ZoomLevelSet.ZoomLevel01.DefaultTextStyle = new TextStyle("Text", new GeoFont("Arial", 16), GeoBrushes.Yellow);
            layer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            var layerOverlay = new LayerOverlay
            {
                TileType = TileType.SingleTile,
                WrappingMode = WrappingMode.WrapDateline,
                WrappingExtent = MaxExtents.ThinkGeoMaps
            };

            layerOverlay.Layers.Add(layer);
            mapView.Overlays.Add(layerOverlay);

            await mapView.RefreshAsync();
        }

        private static Feature GetExtent(double minX, double maxX, double minY, double maxY, string content)
        {
            var shape = new RectangleShape(minX * MaxExtents.ThinkGeoMaps.Width + MaxExtents.ThinkGeoMaps.MinX,
                maxY * MaxExtents.ThinkGeoMaps.Height + MaxExtents.ThinkGeoMaps.MinY,
                maxX * MaxExtents.ThinkGeoMaps.Width + MaxExtents.ThinkGeoMaps.MinX,
                minY * MaxExtents.ThinkGeoMaps.Height + MaxExtents.ThinkGeoMaps.MinY);

            var feature = new Feature(shape)
            {
                ColumnValues =
                {
                    ["Text"] = content
                }
            };
            return feature;
        }

        #region Component Designer generated code

        private MapView mapView;

        private void InitializeComponent()
        {
            mapView = new MapView();
            SuspendLayout();
            // 
            // mapView
            // 
            mapView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
            | AnchorStyles.Left
            | AnchorStyles.Right;
            mapView.BackColor = System.Drawing.Color.White;
            mapView.CurrentScale = 0D;
            mapView.Location = new System.Drawing.Point(0, 0);
            mapView.MapResizeMode = MapResizeMode.PreserveScale;
            mapView.MaximumScale = 1.7976931348623157E+308D;
            mapView.MinimumScale = 200D;
            mapView.Name = "mapView";
            mapView.RestrictExtent = null;
            mapView.RotationAngle = 0F;
            mapView.Size = new System.Drawing.Size(1377, 743);
            mapView.TabIndex = 0;
            // 
            // ResizeTheMap
            // 
            Controls.Add(mapView);
            Name = "WrapTheDateLine";
            Size = new System.Drawing.Size(1377, 743);
            Load += new EventHandler(Form_Load);
            ResumeLayout(false);
        }

        #endregion Component Designer generated code
    }
}
