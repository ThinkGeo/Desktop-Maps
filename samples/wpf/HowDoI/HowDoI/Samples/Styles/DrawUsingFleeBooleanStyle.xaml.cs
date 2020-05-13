using System.IO;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class DrawUsingFleeBooleanStyle : UserControl
    {
        public DrawUsingFleeBooleanStyle()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;
            mapView.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();

            ThinkGeoCloudRasterMapsOverlay worldMapKitDesktopOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret);
            mapView.Overlays.Add(worldMapKitDesktopOverlay);

            // Highlight the countries that are land locked and have a population greater than 10 million
            string expression = "(POP_CNTRY>10000000) && (LANDLOCKED=='Y')";
            FleeBooleanStyle landLockedCountryStyle = new FleeBooleanStyle(expression);

            // You can access the static methods on these types.  We use this
            // to access the Convert.Toxxx methods to convert variable types
            landLockedCountryStyle.StaticTypes.Add(typeof(System.Convert));
            // The math class might be handy to include but in this sample we do not use it
            //landLockedCountryStyle.StaticTypes.Add(typeof(System.Math));

            landLockedCountryStyle.ColumnVariables.Add("POP_CNTRY");
            landLockedCountryStyle.ColumnVariables.Add("LANDLOCKED");

            landLockedCountryStyle.CustomTrueStyles.Add(new AreaStyle(new GeoPen(GeoColors.Green, 2), new GeoSolidBrush(GeoColor.FromArgb(100, GeoColors.Green))));
            landLockedCountryStyle.CustomFalseStyles.Add(AreaStyle.CreateSimpleAreaStyle(GeoColors.Transparent, GeoColor.FromArgb(100, GeoColors.Green)));

            ShapeFileFeatureLayer worldLayer = new ShapeFileFeatureLayer(SampleHelper.Get("Countries02_3857.shp"), FileAccess.Read);
            worldLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(landLockedCountryStyle);
            worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            LayerOverlay worldOverlay = new LayerOverlay();
            worldOverlay.TileType = TileType.SingleTile;
            worldOverlay.Layers.Add("WorldLayer", worldLayer);
            mapView.Overlays.Add("WorldOverlay", worldOverlay);

            mapView.Refresh();
        }
    }
}