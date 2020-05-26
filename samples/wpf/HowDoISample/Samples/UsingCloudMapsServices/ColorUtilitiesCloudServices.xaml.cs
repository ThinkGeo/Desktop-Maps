using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI.UsingCloudMapsServices
{
    /// <summary>
    /// Interaction logic for ColorUtilitiesCloudServices.xaml
    /// </summary>
    public partial class ColorUtilitiesCloudServices : UserControl
    {
        private ColorCloudClient colorCloudClient;
        private Collection<RadioButton> baseColorRadioButtons;

        public ColorUtilitiesCloudServices()
        {
            InitializeComponent();
            txtColorCategoryDescription.Text = "Get a family of colors with the same hue and sequential variances in lightness and saturation";

            baseColorRadioButtons = new Collection<RadioButton>() { rdoDefaultColor, rdoRandomColor, rdoSpecificColor };
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            mapView.MapUnit = GeographyUnit.Meter;

            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
            ThinkGeoCloudVectorMapsOverlay thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay("itZGOI8oafZwmtxP-XGiMvfWJPPc-dX35DmESmLlQIU~", "bcaCzPpmOG6le2pUz5EAaEKYI-KSMny_WxEAe7gMNQgGeN9sqL12OA~~", ThinkGeoCloudVectorMapsMapType.Light);
            mapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            ShapeFileFeatureLayer parksLayer = new ShapeFileFeatureLayer(@"../../../Data/FriscoPOI/Parks.shp");
            parksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            //parksLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.BrightBlue, GeoColors.Black, 2);

            ProjectionConverter projectionConverter = new ProjectionConverter(2276, 3857);
            parksLayer.FeatureSource.ProjectionConverter = projectionConverter;

            LayerOverlay parksOverlay = new LayerOverlay();
            parksOverlay.Layers.Add("Frisco Parks", parksLayer);
            parksOverlay.TileType = TileType.MultiTile;
            mapView.Overlays.Add("Frisco Parks Overlay", parksOverlay);

            parksLayer.Open();
            RectangleShape parksBoundingBox = parksLayer.GetBoundingBox();
            parksLayer.Close();

            mapView.CurrentExtent = parksBoundingBox;
            mapView.Refresh();

            colorCloudClient = new ColorCloudClient("FSDgWMuqGhZCmZnbnxh-Yl1HOaDQcQ6mMaZZ1VkQNYw~", "IoOZkBJie0K9pz10jTRmrUclX6UYssZBeed401oAfbxb9ufF1WVUvg~~");
        }

        private GeoColor GetGeoColorFromString(string htmlColorString)
        {
            GeoColor color = null;

            try
            {
                color = GeoColor.FromHtml(htmlColorString);
            }
            catch 
            {
                MessageBox.Show("Invalid HTML color string");
            }

            return color;
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            int numberOfColors = 5;
            Collection<GeoColor> colors = new Collection<GeoColor>();

            switch((cboColorType.SelectedItem as ComboBoxItem).Content.ToString())
            {
                case "Hue":
                    // Get a family of colors with the same hue and sequential variances in lightness and saturation
                    colors = GetColorsByHue(numberOfColors);
                    break;
                case "Analogous":
                    // Get a family of colors based on analogous hues
                    colors = GetAnalogousColors(numberOfColors);
                    break;
                case "Complementary":
                    // Get a family of colors based on complementary hues
                    colors = GetComplementaryColors(numberOfColors);
                    break;
                case "Contrasting":
                    // Get a family of colors based on contrasting hues
                    colors = GetContrastingColors(numberOfColors);
                    break;
                case "Quality":
                    // Get a family of colors with qualitative variances in hue, but similar lightness and saturation
                    colors = GetQualityColors(numberOfColors);
                    break;
                case "Tetrad":
                    // Get a family of colors based on a harmonious tetrad of hues
                    colors = GetTetradColors(numberOfColors);
                    break;
                case "Triad":
                    // Get a family of colors based on a harmonious tried of hues
                    colors = GetTriadColors(numberOfColors);
                    break;
                default:
                    break;
            }

            if(colors.Count > 0)
            {
                LayerOverlay parksOverlay = (LayerOverlay)mapView.Overlays["Frisco Parks Overlay"];
                ShapeFileFeatureLayer parksLayer = (ShapeFileFeatureLayer)parksOverlay.Layers["Frisco Parks"];

                parksLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();

                ClassBreakStyle classBreakStyle = new ClassBreakStyle();
                classBreakStyle.ColumnName = "ACRES";
                double size = 1;
                foreach (var color in colors)
                {
                    AreaStyle areaStyle = new AreaStyle(new GeoSolidBrush(color));
                    size *= 2;
                    classBreakStyle.ClassBreaks.Add(new ClassBreak(size, areaStyle));
                }
                parksLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(classBreakStyle);
                parksLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                parksOverlay.Refresh();
            }
        }

        private GeoColor GetGeoColorFromDefaultColors()
        {
            GeoColor color = GeoColors.White;
            var selectedColorItem = cboDefaultColor.SelectedItem as ComboBoxItem;

            switch (selectedColorItem.Content.ToString())
            {
                case "Red":
                    color = GeoColors.Red;
                    break;
                case "Orange":
                    color = GeoColors.Orange;
                    break;
                case "Yellow":
                    color = GeoColors.Yellow;
                    break;
                case "Green":
                    color = GeoColors.Green;
                    break;
                case "Blue":
                    color = GeoColors.Blue;
                    break;
                case "Purple":
                    color = GeoColors.Purple;
                    break;
                default:
                    break;
            }

            return color;
        }

        private Collection<GeoColor> GetColorsByHue(int numberOfColors)
        {
            Collection<GeoColor> hueColors = new Collection<GeoColor>();

            switch (baseColorRadioButtons.FirstOrDefault(btn => (bool)btn.IsChecked).Name)
            {
                case "rdoRandomColor":
                    hueColors = colorCloudClient.GetColorsInHueFamily(numberOfColors);
                    break;
                case "rdoSpecificColor":
                    GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        hueColors = colorCloudClient.GetColorsInHueFamily(colorFromInputString, numberOfColors);
                    }
                    break;
                case "rdoDefaultColor":
                    hueColors = colorCloudClient.GetColorsInHueFamily(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
                default:
                    break;
            }

            return hueColors;
        }

        private Collection<GeoColor> GetAnalogousColors(int numberOfColors)
        {
            Collection<GeoColor> analogousColors = new Collection<GeoColor>();
            Dictionary<GeoColor, Collection<GeoColor>> colorsDictionary = new Dictionary<GeoColor, Collection<GeoColor>>();

            switch (baseColorRadioButtons.FirstOrDefault(btn => (bool)btn.IsChecked).Name)
            {
                case "rdoRandomColor":
                    colorsDictionary = colorCloudClient.GetColorsInAnalogousFamily(numberOfColors);
                    break;
                case "rdoSpecificColor":
                    GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        colorsDictionary = colorCloudClient.GetColorsInAnalogousFamily(colorFromInputString, numberOfColors);
                    }
                    break;
                case "rdoDefaultColor":
                    colorsDictionary = colorCloudClient.GetColorsInAnalogousFamily(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
                default:
                    break;
            }

            foreach (var colors in colorsDictionary.Values)
            {
                foreach (var color in colors)
                {
                    analogousColors.Add(color);
                }
            }

            return analogousColors;
        }

        private Collection<GeoColor> GetComplementaryColors(int numberOfColors)
        {
            Collection<GeoColor> complementaryColors = new Collection<GeoColor>();
            Dictionary<GeoColor, Collection<GeoColor>> colorsDictionary = new Dictionary<GeoColor, Collection<GeoColor>>();

            switch (baseColorRadioButtons.FirstOrDefault(btn => (bool)btn.IsChecked).Name)
            {
                case "rdoRandomColor":
                    colorsDictionary = colorCloudClient.GetColorsInComplementaryFamily(numberOfColors);
                    break;
                case "rdoSpecificColor":
                    GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        colorsDictionary = colorCloudClient.GetColorsInComplementaryFamily(colorFromInputString, numberOfColors);
                    }
                    break;
                case "rdoDefaultColor":
                    colorsDictionary = colorCloudClient.GetColorsInComplementaryFamily(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
                default:
                    break;
            }

            foreach (var colors in colorsDictionary.Values)
            {
                foreach (var color in colors)
                {
                    complementaryColors.Add(color);
                }
            }

            return complementaryColors;
        }

        private Collection<GeoColor> GetContrastingColors(int numberOfColors)
        {
            Collection<GeoColor> contrastingColors = new Collection<GeoColor>();
            Dictionary<GeoColor, Collection<GeoColor>> colorsDictionary = new Dictionary<GeoColor, Collection<GeoColor>>();

            switch (baseColorRadioButtons.FirstOrDefault(btn => (bool)btn.IsChecked).Name)
            {
                case "rdoRandomColor":
                    colorsDictionary = colorCloudClient.GetColorsInContrastingFamily(numberOfColors);
                    break;
                case "rdoSpecificColor":
                    GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        colorsDictionary = colorCloudClient.GetColorsInContrastingFamily(colorFromInputString, numberOfColors);
                    }
                    break;
                case "rdoDefaultColor":
                    colorsDictionary = colorCloudClient.GetColorsInContrastingFamily(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
                default:
                    break;
            }

            foreach (var colors in colorsDictionary.Values)
            {
                foreach (var color in colors)
                {
                    contrastingColors.Add(color);
                }
            }

            return contrastingColors;
        }

        private Collection<GeoColor> GetQualityColors(int numberOfColors)
        {
            Collection<GeoColor> qualityColors = new Collection<GeoColor>();

            switch (baseColorRadioButtons.FirstOrDefault(btn => (bool)btn.IsChecked).Name)
            {
                case "rdoRandomColor":
                    qualityColors = colorCloudClient.GetColorsInQualityFamily(numberOfColors);
                    break;
                case "rdoSpecificColor":
                    GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        qualityColors = colorCloudClient.GetColorsInQualityFamily(colorFromInputString, numberOfColors);
                    }
                    break;
                case "rdoDefaultColor":
                    qualityColors = colorCloudClient.GetColorsInQualityFamily(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
                default:
                    break;
            }

            return qualityColors;
        }

        private Collection<GeoColor> GetTetradColors(int numberOfColors)
        {
            Collection<GeoColor> tetradColors = new Collection<GeoColor>();
            Dictionary<GeoColor, Collection<GeoColor>> colorsDictionary = new Dictionary<GeoColor, Collection<GeoColor>>();

            switch (baseColorRadioButtons.FirstOrDefault(btn => (bool)btn.IsChecked).Name)
            {
                case "rdoRandomColor":
                    colorsDictionary = colorCloudClient.GetColorsInTetradFamily(numberOfColors);
                    break;
                case "rdoSpecificColor":
                    GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        colorsDictionary = colorCloudClient.GetColorsInTetradFamily(colorFromInputString, numberOfColors);
                    }
                    break;
                case "rdoDefaultColor":
                    colorsDictionary = colorCloudClient.GetColorsInTetradFamily(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
                default:
                    break;
            }

            foreach (var colors in colorsDictionary.Values)
            {
                foreach (var color in colors)
                {
                    tetradColors.Add(color);
                }
            }

            return tetradColors;
        }

        private Collection<GeoColor> GetTriadColors(int numberOfColors)
        {
            Collection<GeoColor> triadColors = new Collection<GeoColor>();
            Dictionary<GeoColor, Collection<GeoColor>> colorsDictionary = new Dictionary<GeoColor, Collection<GeoColor>>();


            switch (baseColorRadioButtons.FirstOrDefault(btn => (bool)btn.IsChecked).Name)
            {
                case "rdoRandomColor":
                    colorsDictionary = colorCloudClient.GetColorsInTriadFamily(numberOfColors);
                    break;
                case "rdoSpecificColor":
                    GeoColor colorFromInputString = GetGeoColorFromString(txtSpecificColor.Text);
                    if (colorFromInputString != null)
                    {
                        colorsDictionary = colorCloudClient.GetColorsInTriadFamily(colorFromInputString, numberOfColors);
                    }
                    break;
                case "rdoDefaultColor":
                    colorsDictionary = colorCloudClient.GetColorsInTriadFamily(GetGeoColorFromDefaultColors(), numberOfColors);
                    break;
                default:
                    break;
            }

            foreach (var colors in colorsDictionary.Values)
            {
                foreach (var color in colors)
                {
                    triadColors.Add(color);
                }
            }

            return triadColors;
        }

        private void cboColorType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxContent = (cboColorType.SelectedItem as ComboBoxItem).Content;

            if (comboBoxContent != null)
            {
                switch (comboBoxContent.ToString())
                {
                    case "Hue":
                        txtColorCategoryDescription.Text = "Get a family of colors with the same hue and sequential variances in lightness and saturation";
                        break;
                    case "Analogous":
                        txtColorCategoryDescription.Text = "Get a family of colors based on analogous hues";
                        break;
                    case "Complementary":
                        txtColorCategoryDescription.Text = "Get a family of colors based on complementary hues";
                        break;
                    case "Contrasting":
                        txtColorCategoryDescription.Text = "Get a family of colors based on contrasting hues";
                        break;
                    case "Quality":
                        txtColorCategoryDescription.Text = "Get a family of colors with qualitative variances in hue, but similar lightness and saturation";
                        break;
                    case "Tetrad":
                        txtColorCategoryDescription.Text = "Get a family of colors based on a harmonious tetrad of hues";
                        break;
                    case "Triad":
                        txtColorCategoryDescription.Text = "Get a family of colors based on a harmonious tried of hues";
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
