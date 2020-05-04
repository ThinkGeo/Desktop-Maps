using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class SimplyRotateMap : UserControl
    {
        public SimplyRotateMap()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            Map1.ExtentOverlay.RotationMouseButton = MapMouseButton.Right;
            Map1.ExtentOverlay.RotationKey = System.Windows.Input.Key.LeftAlt;

            Map1.MapUnit = GeographyUnit.Meter;
            Map1.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            Map1.CurrentExtent = new RectangleShape(-10775293.1819701, 3866499.57476108, -10774992.2111729, 3866281.90838096);

            var thinkGeoCloudVectorFeatureLayer = new ThinkGeoCloudVectorMapsLayer(SampleHelper.ThinkGeoCloudId, SampleHelper.ThinkGeoCloudSecret, ThinkGeoCloudVectorMapsMapType.Light);
            LayerOverlay layerOverlay = new LayerOverlay
            {
                TileWidth = 512,
                TileHeight = 512
            };
            layerOverlay.Layers.Add(thinkGeoCloudVectorFeatureLayer);
            Map1.Overlays.Add(layerOverlay);
        }

        private void BtnAnticlockwise_Click(object sender, RoutedEventArgs e)
        {
            int degreeStep = GetRotateArguments();
            Map1.RotatedAngle = Map1.RotatedAngle - degreeStep;

            Map1.Refresh();
        }

        private void BtnClockwise_Click(object sender, RoutedEventArgs e)
        {
            int degreeStep = GetRotateArguments();
            Map1.RotatedAngle = Map1.RotatedAngle + degreeStep;

            Map1.Refresh();
        }

        private int GetRotateArguments()
        {
            if (!int.TryParse(txtDegreeStep.Text, out int degreeStep))
            {
                degreeStep = 15;
                txtDegreeStep.Text = degreeStep.ToString();
            }

            return  degreeStep;
        }
    }
}