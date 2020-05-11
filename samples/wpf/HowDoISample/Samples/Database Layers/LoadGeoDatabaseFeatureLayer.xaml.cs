using System.IO;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class LoadGeoDatabaseFeatureLayer : UserControl
    {
        public LoadGeoDatabaseFeatureLayer()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                mapView.MapUnit = GeographyUnit.Meter;
                mapView.CurrentExtent = new RectangleShape(2149408.38465815, 246471.365609125, 2204046.63635703, 213231.081162168);

                PersonalGeoDatabaseFeatureLayer worldLayer = new PersonalGeoDatabaseFeatureLayer(SampleHelper.Get("wsb57.mdb"));
                worldLayer.EnableEmbeddedStyle = false;
                worldLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.LightGray);
                worldLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColor.FromArgb(255, 169, 195, 221), 2F, GeoColors.Black, 4F, false);
                worldLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateCompoundPointStyle(PointSymbolType.Square, GeoColors.White, GeoColors.Black, 1F, 6F, PointSymbolType.Square, GeoColors.Maroon, GeoColors.Transparent, 0F, 2F);
                worldLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                worldLayer.Open();

                LayerOverlay worldOverlay = new LayerOverlay();
                worldOverlay.DrawingExceptionMode = DrawingExceptionMode.DrawException;
                worldOverlay.Layers.Add(new BackgroundLayer(new GeoSolidBrush(GeoColor.FromArgb(255, 233, 232, 214))));
                worldOverlay.Layers.Add("WorldLayer", worldLayer);
                mapView.Overlays.Add("WorldOverlay", worldOverlay);

                mapView.CurrentExtent = worldOverlay.GetBoundingBox();
                mapView.Refresh();
            }
            catch (FileNotFoundException ex)
            {
                string message = "You have to reference ThinkGeo.PersonalGeoDatabase.nupkg\r\n\r\n" + ex.Message;
                MessageBox.Show(message, "FileNotFound", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK, (MessageBoxOptions)0);
            }
        }
    }
}