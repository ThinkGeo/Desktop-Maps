using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeoCloudVectorFeatureLayerQuerySample_Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string cloudServiceClientId = "Your-ThinkGeo-Cloud-Service-Cliend-ID";    // Get it from https://cloud.thinkgeo.com
        private const string cloudServiceClientSecret = "Your-ThinkGeo-Cloud-Service-Cliend-Secret";

        private ThinkGeoCloudVectorMapsFeatureLayer thinkGeoCloudVectorFeatureLayer;
        private SimpleMarkerOverlay searchPointMarkerOverlay;
        private InMemoryFeatureLayer seachRadiusFeatureLayer;
        private SimpleMarkerOverlay nearbysMarkerOverlay;
        private WellKnownType[] searchTypes;
        private PopupOverlay popupOverlay;

        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the search type.
            this.searchTypes = new WellKnownType[] { WellKnownType.Point, WellKnownType.Multipoint };

            this.wpfMap.MapUnit = ThinkGeo.MapSuite.GeographyUnit.Meter;
            this.wpfMap.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            // Add thinkgeoCloudVectorFeatureLayer as background layer and query resource.
            this.thinkGeoCloudVectorFeatureLayer = new ThinkGeoCloudVectorMapsFeatureLayer(cloudServiceClientId, cloudServiceClientSecret);

            // Add layer for searchRadius.
            this.seachRadiusFeatureLayer = new InMemoryFeatureLayer();
            this.seachRadiusFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(new GeoPen(new GeoColor(100, GeoColors.Blue)), new GeoSolidBrush(new GeoColor(10, GeoColors.Blue)));
            this.seachRadiusFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add markerOverlay for search Marker.
            this.searchPointMarkerOverlay = new SimpleMarkerOverlay();

            LayerOverlay layerOverlay = new LayerOverlay
            {
                TileWidth = 512,
                TileHeight = 512
            };
            layerOverlay.MaxExtent = thinkGeoCloudVectorFeatureLayer.GetTileMatrixBoundingBox();
            layerOverlay.Layers.Add(this.thinkGeoCloudVectorFeatureLayer);
            layerOverlay.Layers.Add(this.seachRadiusFeatureLayer);

            // Add markerOverlay for Search Result Markers.
            this.nearbysMarkerOverlay = new SimpleMarkerOverlay();

            this.popupOverlay = new PopupOverlay(new Collection<Popup>());

            this.wpfMap.Overlays.Add(layerOverlay);
            this.wpfMap.Overlays.Add(this.nearbysMarkerOverlay);
            this.wpfMap.Overlays.Add(this.searchPointMarkerOverlay);
            this.wpfMap.Overlays.Add(this.popupOverlay);

            this.wpfMap.CurrentExtent = new RectangleShape(-10775293.1819701, 3866499.57476108, -10774992.2111729, 3866281.90838096);
        }

        private void WpfMap_MapClick(object sender, ThinkGeo.MapSuite.Wpf.MapClickWpfMapEventArgs e)
        {
            if (e.MouseButton == MapMouseButton.Right)
            {
                this.seachRadiusFeatureLayer.InternalFeatures.Clear();
                this.seachRadiusFeatureLayer.InternalFeatures.Add(new Feature(new EllipseShape(e.WorldLocation, int.Parse(this.txtSearchRadius.Text))));
                this.searchPointMarkerOverlay.Markers.Clear();
                this.searchPointMarkerOverlay.Markers.Add(GetMarkerByPlaceRecord(e.WorldLocation, "searchPoint", null));

                this.nearbysMarkerOverlay.Markers.Clear();

                Collection<Feature> selectedFeatures = SearchNearbyFeaturesByClickedLocation(e.WorldLocation, int.Parse(this.txtSearchRadius.Text));

                RenderSelectedFeatures(selectedFeatures);
                this.searchPointMarkerOverlay.IsVisible = true;
                this.nearbysMarkerOverlay.IsVisible = true;
                this.seachRadiusFeatureLayer.IsVisible = true;
            }
            else if (e.MouseButton == MapMouseButton.Left)
            {
                this.seachRadiusFeatureLayer.IsVisible = false;
                this.searchPointMarkerOverlay.IsVisible = false;
                this.nearbysMarkerOverlay.IsVisible = false;
            }

            this.wpfMap.Refresh();
        }

        private Collection<Feature> SearchNearbyFeaturesByClickedLocation(PointShape queryPointShape, int searchDistanceInMeters)
        {
            this.thinkGeoCloudVectorFeatureLayer.Open();
            Collection<Feature> features = this.thinkGeoCloudVectorFeatureLayer.QueryTools.GetFeaturesWithinDistanceOf(queryPointShape, GeographyUnit.Meter, DistanceUnit.Meter, searchDistanceInMeters, ReturningColumnsType.AllColumns);
            this.thinkGeoCloudVectorFeatureLayer.Close();

            return features;
        }

        private void RenderSelectedFeatures(Collection<Feature> selectedFeatures)
        {
            if (selectedFeatures != null)
            {
                foreach (Feature feature in selectedFeatures)
                {
                    WellKnownType featureType = feature.GetWellKnownType();
                    if (featureType == WellKnownType.Point || featureType == WellKnownType.Multipoint)
                    {
                        AddMarkerToMap(feature);
                    }
                }
            }
        }

        private void AddMarkerToMap(Feature feature)
        {
            Marker marker = null;
            string category = string.Empty;
            if (feature.ColumnValues.ContainsKey("class"))
            {
                category = feature.ColumnValues["class"];
            }
            marker = GetMarkerByPlaceRecord(feature.GetBoundingBox().GetCenterPoint(), category, feature.ColumnValues);
            this.nearbysMarkerOverlay.Markers.Add(marker);
        }

        private Marker GetMarkerByPlaceRecord(PointShape pointShape, string category, Dictionary<string, string> columnValues)
        {
            Marker marker = null;
            marker = new Marker(pointShape)
            {
                Width = 24,
                Height = 24,
                Tag = columnValues
            };

            if (!string.IsNullOrEmpty(category))
            {
                if (Properties.Resources.ResourceManager.GetObject(category, Properties.Resources.Culture) != null)
                {
                    string imageUri = string.Format("/Resources/{0}.png", category);
                    marker.ImageSource = new BitmapImage(new Uri(imageUri, UriKind.RelativeOrAbsolute));
                }
                else if (category == "searchPoint")
                {
                    marker.ImageSource = new BitmapImage(new Uri("/Resources/bestMatchingPlace.png", UriKind.RelativeOrAbsolute));
                    marker.Width = 32;
                    marker.Height = 32;
                    marker.YOffset = -16;
                }
                else
                {
                    marker.ImageSource = new BitmapImage(new Uri("/Resources/others.png", UriKind.RelativeOrAbsolute));
                }
            }
            else
            {
                marker.ImageSource = new BitmapImage(new Uri("/Resources/others.png", UriKind.RelativeOrAbsolute));
            }

            marker.MouseEnter += Marker_MouseEnter;
            marker.MouseLeave += Marker_MouseLeave;

            return marker;
        }

        private void Marker_MouseLeave(object sender, MouseEventArgs e)
        {
            this.popupOverlay.Popups.Clear();
            this.popupOverlay.IsVisible = false;
        }

        private void Marker_MouseEnter(object sender, MouseEventArgs e)
        {
            Marker marker = e.Source as Marker;
            if (marker.Tag != null)
            {
                Dictionary<string, string> columnValues = marker.Tag as Dictionary<string, string>;
                Popup popup = new Popup(marker.Position)
                {
                    Content = ConvertToString(columnValues)
                };

                this.popupOverlay.Popups.Add(popup);
                this.popupOverlay.IsVisible = true;
                this.wpfMap.Refresh();
            }
        }

        private object ConvertToString(Dictionary<string, string> columnValues)
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (columnValues != null)
            {
                foreach (KeyValuePair<string, string> columnValue in columnValues)
                {
                    if (string.IsNullOrEmpty(columnValue.Value)) { continue; }
                    stringBuilder.Append(columnValue.Key);
                    stringBuilder.Append(": ");
                    stringBuilder.Append(columnValue.Value);
                    stringBuilder.Append("\n");
                }
            }

            return stringBuilder.ToString().TrimEnd('\n');
        }
    }
}
