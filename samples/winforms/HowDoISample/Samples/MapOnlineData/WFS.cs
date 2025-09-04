using System;
using System.Linq;
using System.Windows.Forms;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    public partial class WFS : UserControl
    {
        public WFS()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            // It is important to set the map unit first to either feet, meters or decimal degrees.
            mapView.MapUnit = GeographyUnit.Meter;

            // Create background world map with vector tile requested from ThinkGeo Cloud Service. 
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            var helsinkiParcelsLayer = CreateHelsinkiParcelsLayer();

            var layerOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            layerOverlay.Layers.Add(helsinkiParcelsLayer);
            mapView.Overlays.Add(layerOverlay);

            mapView.CenterPoint = new PointShape(2777730, 8435220);
            mapView.CurrentScale = 20520;

            _ = mapView.RefreshAsync();
        }

        private WfsV2FeatureLayer CreateHelsinkiParcelsLayer()
        {
            var layer = new WfsV2FeatureLayer(
                "https://inspire-wfs.maanmittauslaitos.fi/inspire-wfs/cp/ows",
                "cp:CadastralParcel")
            {
                TimeoutInSeconds = 500
            };

            layer.ZoomLevelSet.ZoomLevel13.DefaultAreaStyle =
                AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColors.OrangeRed, 4);
            layer.ZoomLevelSet.ZoomLevel13.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            layer.FeatureSource.ProjectionConverter = new ProjectionConverter(3067, 3857);

            // Attach event handlers here so they’re always included
            layer.SendingWebRequest += HelsinkiParcelsLayer_SendingWebRequest;

            var featureSource = (WfsV2FeatureSource)layer.FeatureSource;
            featureSource.RequestingData += WFS_RequestingData;

            return layer;
        }

        private void rbOverlayType_CheckedChanged(object sender, EventArgs e)
        {
            if (!(sender is RadioButton radio) || radio.Checked == false)
                return;

            // Keep background overlays
            var backgroundOverlays = mapView.Overlays
                                            .Where(o => o.Name != "WfsOverlay" && o.Name != "LayerOverlay")
                                            .ToList();
            mapView.Overlays.Clear();
            foreach (var overlay in backgroundOverlays)
                mapView.Overlays.Add(overlay);

            switch (radio.Text.ToString())
            {
                case "WfsV2Overlay":
                    AddWfsV2Overlay();
                    break;

                case "LayerOverlay":
                    AddLayerOverlay();
                    break;
            }

            _ = mapView.RefreshAsync();
        }

        private void AddLayerOverlay()
        {
            // Try to get an existing overlay with the key
            LayerOverlay overlay;
            if (mapView.Overlays.Contains("LayerOverlay"))
            {
                overlay = mapView.Overlays["LayerOverlay"] as LayerOverlay;
                overlay.Layers.Clear(); // clear existing layers
            }
            else
            {
                overlay = new LayerOverlay { TileType = TileType.SingleTile };
            }

            // Add the WFS layer
            var helsinkiParcelsLayer = CreateHelsinkiParcelsLayer();
            overlay.Layers.Add(helsinkiParcelsLayer);

            // If it’s a new overlay, add to MapView
            if (!mapView.Overlays.Contains("LayerOverlay"))
            {
                mapView.Overlays.Add("LayerOverlay", overlay);
            }
        }

        private void AddWfsV2Overlay()
        {
            // Try to get an existing overlay with the key
            WfsV2Overlay overlay;
            if (mapView.Overlays.Contains("WfsOverlay"))
            {
                overlay = mapView.Overlays["WfsOverlay"] as WfsV2Overlay;
                overlay.FeatureLayer = null; // clear existing layer
            }
            else
            {
                overlay = new WfsV2Overlay()
                {
                    DrawingBulkCount = 500
                };
            }

            // Add the WFS layer
            var helsinkiParcelsLayer = CreateHelsinkiParcelsLayer();
            overlay.FeatureLayer = helsinkiParcelsLayer;

            // If it’s a new overlay, add to MapView
            if (!mapView.Overlays.Contains("WfsOverlay"))
            {
                mapView.Overlays.Add("WfsOverlay", overlay);
            }
        }

        private void HelsinkiParcelsLayer_SendingWebRequest(object sender, SendingWebRequestEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"[WFS v1] Request: {e.WebRequest.RequestUri}");
        }

        private void WFS_RequestingData(object sender, RequestingDataWfsFeatureSourceEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"[WFS v2] Request: {e.ServiceUrl}");
        }


        #region Component Designer generated code

        private MapView mapView;
        private Panel controlPanel;
        private RadioButton wfsV2OverlayRadioButton;
        private RadioButton layerOverlayRadioButton;
        private Label overlayTypelabel;

        private void InitializeComponent()
        {
            mapView = new MapView();
            controlPanel = new Panel();
            wfsV2OverlayRadioButton = new RadioButton();
            layerOverlayRadioButton = new RadioButton();
            overlayTypelabel = new Label();
            controlPanel.SuspendLayout();
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
            // controlPanel
            // 
            controlPanel.Dock = DockStyle.Right;
            controlPanel.Width = 185;
            controlPanel.BackColor = System.Drawing.Color.Gray;
            controlPanel.Controls.Add(wfsV2OverlayRadioButton);
            controlPanel.Controls.Add(layerOverlayRadioButton);
            controlPanel.Controls.Add(overlayTypelabel);
            controlPanel.Name = "controlPanel";
            controlPanel.TabIndex = 1;
            // 
            // overlayTypelabel
            // 
            overlayTypelabel.AutoSize = true;
            overlayTypelabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            overlayTypelabel.ForeColor = System.Drawing.Color.White;
            overlayTypelabel.Location = new System.Drawing.Point(15, 10);
            overlayTypelabel.Name = "overlayTypelabel";
            overlayTypelabel.Size = new System.Drawing.Size(162, 25);
            overlayTypelabel.Text = "Overlay Type:";
            overlayTypelabel.TabIndex = 2;
            // 
            // wfsV2OverlayRadioButton
            // 
            wfsV2OverlayRadioButton.AutoSize = true;
            wfsV2OverlayRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            wfsV2OverlayRadioButton.ForeColor = System.Drawing.Color.White;
            wfsV2OverlayRadioButton.Location = new System.Drawing.Point(20, 48);
            wfsV2OverlayRadioButton.Name = "wfsV2OverlayRadioButton";
            wfsV2OverlayRadioButton.Size = new System.Drawing.Size(161, 24);
            wfsV2OverlayRadioButton.TabStop = true;
            wfsV2OverlayRadioButton.Text = "WfsV2Overlay";
            wfsV2OverlayRadioButton.UseVisualStyleBackColor = true;
            wfsV2OverlayRadioButton.CheckedChanged += new EventHandler(rbOverlayType_CheckedChanged);
            wfsV2OverlayRadioButton.TabIndex = 3;
            // 
            // layerOverlayRadioButton
            // 
            layerOverlayRadioButton.AutoSize = true;
            layerOverlayRadioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
            layerOverlayRadioButton.ForeColor = System.Drawing.Color.White;
            layerOverlayRadioButton.Location = new System.Drawing.Point(20, 85);
            layerOverlayRadioButton.Name = "layerOverlayRadioButton";
            layerOverlayRadioButton.Size = new System.Drawing.Size(196, 24);
            layerOverlayRadioButton.Text = "LayerOverlay";
            layerOverlayRadioButton.UseVisualStyleBackColor = true;
            layerOverlayRadioButton.Checked = true;
            layerOverlayRadioButton.CheckedChanged += new EventHandler(rbOverlayType_CheckedChanged);
            layerOverlayRadioButton.TabIndex = 4;
            // 
            // WFS
            // 
            Controls.Add(mapView);
            Controls.Add(controlPanel);
            Name = "WFS";
            Size = new System.Drawing.Size(1377, 743);
            Load += new EventHandler(Form_Load);
            controlPanel.ResumeLayout(false);
            controlPanel.PerformLayout();
            ResumeLayout(false);
            
            // Make sure panel stays on top of the map
            controlPanel.BringToFront();
        }

        #endregion Component Designer generated code
    }
}
