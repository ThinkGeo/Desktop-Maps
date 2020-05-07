using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    [Serializable]
    public partial class AddAdditionalCustomPropertiesAndMethods : UserControl
    {
        public AddAdditionalCustomPropertiesAndMethods()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            mapView.CurrentExtent = new RectangleShape(-19747615, 17926782, -5857338, 1637456);
            mapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.ShallowOcean);

            AdministrationShapeFileLayer worldLayer = new AdministrationShapeFileLayer(SampleHelper.Get("Countries02_3857.shp"), SecurityLevel.AverageUsageLevel1);
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 118, 138, 69));

            AdministrationShapeFileLayer statesLayer = new AdministrationShapeFileLayer(SampleHelper.Get("USStates_3857.shp"), SecurityLevel.AverageUsageLevel2);
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(255, 233, 232, 214), GeoColor.FromArgb(255, 156, 155, 154), 2);
            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle.OutlinePen.LineJoin = DrawingLineJoin.Round;
            statesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            worldOverlay.Layers.Add("StatesLayer", statesLayer);
            mapView.Overlays.Add("WorldOverlay", worldOverlay);

            mapView.Refresh();
        }

        public enum SecurityLevel
        {
            AdministrativeLevel = 0,
            AverageUsageLevel1 = 1,
            AverageUsageLevel2 = 2
        }

        /// <summary>
        /// 
        /// </summary>
        [Serializable]
        public class AdministrationShapeFileLayer : ShapeFileFeatureLayer
        {
            private SecurityLevel securityLevel;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="pathFileName"></param>
            /// <param name="securityLevel"></param>
            public AdministrationShapeFileLayer(string pathFileName, SecurityLevel securityLevel)
                : base(pathFileName)
            {
                this.securityLevel = securityLevel;
            }

            /// <summary>
            /// 
            /// </summary>
            public SecurityLevel SecurityLevel
            {
                get
                {
                    return securityLevel;
                }
                set
                {
                    securityLevel = value;
                }
            }
        }

        private void cboSecturityLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mapView.Overlays.Count > 0)
            {
                foreach (Layer layer in ((LayerOverlay)mapView.Overlays[0]).Layers)
                {
                    layer.IsVisible = true;
                    SecurityLevel securityLevel = ((AdministrationShapeFileLayer)layer).SecurityLevel;

                    if (cboSecturityLevel.SelectedItem.ToString().Split(':')[1].Trim() == "AverageUsageLevel1" && securityLevel == SecurityLevel.AverageUsageLevel2)
                    {
                        layer.IsVisible = false;
                    }
                    else if (cboSecturityLevel.SelectedItem.ToString().Split(':')[1].Trim() == "AverageUsageLevel2" && securityLevel == SecurityLevel.AverageUsageLevel1)
                    {
                        layer.IsVisible = false;
                    }
                }

                mapView.Overlays["WorldOverlay"].Refresh();
            }
        }
    }
}