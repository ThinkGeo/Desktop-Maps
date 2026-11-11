using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Ask the ThinkGeo Cloud for a family of colors and shade Frisco's census blocks by housing units with it.
    /// </summary>
    public partial class ColorUtilities
    {
        private bool _initialized;
        private ColorCloudClient _colorCloudClient;
        private ShapeFileFeatureLayer _housingUnitsLayer;
        private LayerOverlay _housingUnitsOverlay;
        private LegendAdornmentLayer _legend;

        public ColorUtilities()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            try
            {
                Map.Basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Dark, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) }));
                Map.MapUnit = GeographyUnit.Meter;

                // The census data is in Texas North Central (2276); the map is in Spherical Mercator (3857).
                _housingUnitsLayer = new ShapeFileFeatureLayer(SampleShared.Shapefile("Frisco 2010 Census Housing Units.shp"));
                _housingUnitsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);
                _housingUnitsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                _housingUnitsOverlay = new LayerOverlay();
                _housingUnitsOverlay.Layers.Add(_housingUnitsLayer);
                Map.Overlays.Add(_housingUnitsOverlay);

                _legend = new LegendAdornmentLayer
                {
                    Title = new LegendItem
                    {
                        TextStyle = new TextStyle("Housing Unit Counts", new GeoFont("Verdana", 10, DrawingFontStyles.Bold), GeoBrushes.Black)
                    },
                    Location = AdornmentLocation.LowerRight
                };
                Map.AdornmentOverlay.Layers.Add(_legend);

                _housingUnitsLayer.Open();
                var extent = _housingUnitsLayer.GetBoundingBox();
                _housingUnitsLayer.Close();
                Map.CenterPoint = extent.GetCenterPoint();
                Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, extent, Map.MapWidth, Map.MapHeight);
                await Map.ZoomOutAsync();

                _colorCloudClient = new ColorCloudClient
                {
                    ClientId = SampleShared.ClientId,
                    ClientSecret = SampleShared.ClientSecret,
                };

                await GenerateAndApplyColorsAsync();
            }
            catch (Exception ex)
            {
                LoadingImage.Visibility = Visibility.Hidden;
                Status.Text = ex.Message;
            }
        }

        private async void GenerateColors_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await GenerateAndApplyColorsAsync();
            }
            catch (Exception ex)
            {
                LoadingImage.Visibility = Visibility.Hidden;
                Status.Text = ex.Message;
            }
        }

        private async Task GenerateAndApplyColorsAsync()
        {
            LoadingImage.Visibility = Visibility.Visible;
            var colors = await GetColorsAsync((CboColorType.SelectedItem as ComboBoxItem)?.Content.ToString(), 6);
            LoadingImage.Visibility = Visibility.Hidden;

            if (colors.Count == 0) return;
            Status.Text = $"The service returned {colors.Count} colors.";

            // One class per color the service returned - some families return more colors
            // than were asked for - spread over the range of the H_UNITS column (0 to 6,000).
            var step = Math.Ceiling(6000d / colors.Count / 100) * 100;
            var classBreakStyle = new ClassBreakStyle { ColumnName = "H_UNITS" };
            _legend.LegendItems.Clear();
            for (var i = 0; i < colors.Count; i++)
            {
                var areaStyle = new AreaStyle(new GeoSolidBrush(colors[colors.Count - i - 1]));
                classBreakStyle.ClassBreaks.Add(new ClassBreak(i * step, areaStyle));
                _legend.LegendItems.Add(new LegendItem
                {
                    ImageStyle = areaStyle,
                    TextStyle = new TextStyle($">{i * step} units", new GeoFont("Verdana", 10), GeoBrushes.Black)
                });
            }

            _housingUnitsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Clear();
            _housingUnitsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(classBreakStyle);

            await _housingUnitsOverlay.RefreshAsync();
            await Map.AdornmentOverlay.RefreshAsync();
        }

        /// <summary>
        /// Hue and Quality answer one family; the other families come back as one list per
        /// base color the service chose, and all of them are used.
        /// </summary>
        private async Task<Collection<GeoColor>> GetColorsAsync(string family, int numberOfColors)
        {
            GeoColor baseColor = null;
            if (RdoSpecificColor.IsChecked == true)
            {
                try
                {
                    baseColor = GeoColor.FromHtml(TxtSpecificColor.Text);
                }
                catch
                {
                    Status.Text = "That is not an HTML color code, such as #AB2567.";
                    return new Collection<GeoColor>();
                }
            }
            else if (RdoDefaultColor.IsChecked == true)
            {
                baseColor = GetGeoColorFromDefaultColors();
            }

            Dictionary<GeoColor, Collection<GeoColor>> families;
            switch (family)
            {
                case "Hue":
                    return baseColor == null
                        ? await _colorCloudClient.GetColorsInHueFamilyAsync(numberOfColors)
                        : await _colorCloudClient.GetColorsInHueFamilyAsync(baseColor, numberOfColors);
                case "Quality":
                    return baseColor == null
                        ? await _colorCloudClient.GetColorsInQualityFamilyAsync(numberOfColors)
                        : await _colorCloudClient.GetColorsInQualityFamilyAsync(baseColor, numberOfColors);
                case "Analogous":
                    families = baseColor == null
                        ? await _colorCloudClient.GetColorsInAnalogousFamilyAsync(numberOfColors)
                        : await _colorCloudClient.GetColorsInAnalogousFamilyAsync(baseColor, numberOfColors);
                    break;
                case "Complementary":
                    families = baseColor == null
                        ? await _colorCloudClient.GetColorsInComplementaryFamilyAsync(numberOfColors)
                        : await _colorCloudClient.GetColorsInComplementaryFamilyAsync(baseColor, numberOfColors);
                    break;
                case "Contrasting":
                    families = baseColor == null
                        ? await _colorCloudClient.GetColorsInContrastingFamilyAsync(numberOfColors)
                        : await _colorCloudClient.GetColorsInContrastingFamilyAsync(baseColor, numberOfColors);
                    break;
                case "Tetrad":
                    families = baseColor == null
                        ? await _colorCloudClient.GetColorsInTetradFamilyAsync(numberOfColors)
                        : await _colorCloudClient.GetColorsInTetradFamilyAsync(baseColor, numberOfColors);
                    break;
                case "Triad":
                    families = baseColor == null
                        ? await _colorCloudClient.GetColorsInTriadFamilyAsync(numberOfColors)
                        : await _colorCloudClient.GetColorsInTriadFamilyAsync(baseColor, numberOfColors);
                    break;
                default:
                    return new Collection<GeoColor>();
            }

            return new Collection<GeoColor>(families.Values.SelectMany(colors => colors).ToList());
        }

        private GeoColor GetGeoColorFromDefaultColors()
        {
            switch ((CboDefaultColor.SelectedItem as ComboBoxItem)?.Content.ToString())
            {
                case "Orange": return GeoColors.Orange;
                case "Yellow": return GeoColors.Yellow;
                case "Green": return GeoColors.Green;
                case "Blue": return GeoColors.Blue;
                case "Purple": return GeoColors.Purple;
                default: return GeoColors.Red;
            }
        }

        private void CboColorType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;

            switch ((CboColorType.SelectedItem as ComboBoxItem)?.Content.ToString())
            {
                case "Hue":
                    TxtColorCategoryDescription.Text = "Get a family of colors with the same hue and sequential variances in lightness and saturation";
                    break;
                case "Analogous":
                    TxtColorCategoryDescription.Text = "Get a family of colors based on analogous hues";
                    break;
                case "Complementary":
                    TxtColorCategoryDescription.Text = "Get a family of colors based on complementary hues";
                    break;
                case "Contrasting":
                    TxtColorCategoryDescription.Text = "Get a family of colors based on contrasting hues";
                    break;
                case "Quality":
                    TxtColorCategoryDescription.Text = "Get a family of colors with qualitative variances in hue, but similar lightness and saturation";
                    break;
                case "Tetrad":
                    TxtColorCategoryDescription.Text = "Get a family of colors based on a harmonious tetrad of hues";
                    break;
                case "Triad":
                    TxtColorCategoryDescription.Text = "Get a family of colors based on a harmonious triad of hues";
                    break;
            }
        }
    }
}
