using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Ask the ThinkGeo Cloud for a family of colors and shade Frisco's census blocks by housing units with it.
    /// </summary>
    public partial class ColorUtilities : IDisposable
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
                Map.MapUnit = GeographyUnit.Meter;
                Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

                // The census data is in Texas North Central (2276); the map is in Spherical Mercator (3857).
                _housingUnitsLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Frisco 2010 Census Housing Units.shp"));
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
                    ClientId = SampleKeys.ClientId2,
                    ClientSecret = SampleKeys.ClientSecret2,
                };

                await GenerateAndApplyColorsAsync();
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
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
                Status.Text = ex.Message;
            }
        }

        private async Task GenerateAndApplyColorsAsync()
        {
            GeoColor baseColor = null;
            if (SpecificColor.IsChecked == true)
            {
                try
                {
                    baseColor = GeoColor.FromHtml(TxtSpecificColor.Text);
                }
                catch
                {
                    Status.Text = "That is not an HTML color code, such as #AB2567.";
                    return;
                }
            }

            Collection<GeoColor> colors;
            LoadingImage.Visibility = Visibility.Visible;
            try
            {
                colors = await GetColorsAsync(baseColor, 6);
            }
            finally
            {
                LoadingImage.Visibility = Visibility.Hidden;
            }

            if (colors.Count == 0)
            {
                Status.Text = "The service returned no colors.";
                return;
            }
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
        /// base color the service chose, and all of them are used. With no base color the
        /// service picks one at random.
        /// </summary>
        private async Task<Collection<GeoColor>> GetColorsAsync(GeoColor baseColor, int numberOfColors)
        {
            if (FamilyHue.IsChecked == true)
            {
                return baseColor == null
                    ? await _colorCloudClient.GetColorsInHueFamilyAsync(numberOfColors)
                    : await _colorCloudClient.GetColorsInHueFamilyAsync(baseColor, numberOfColors);
            }
            if (FamilyQuality.IsChecked == true)
            {
                return baseColor == null
                    ? await _colorCloudClient.GetColorsInQualityFamilyAsync(numberOfColors)
                    : await _colorCloudClient.GetColorsInQualityFamilyAsync(baseColor, numberOfColors);
            }

            Dictionary<GeoColor, Collection<GeoColor>> families;
            if (FamilyAnalogous.IsChecked == true)
            {
                families = baseColor == null
                    ? await _colorCloudClient.GetColorsInAnalogousFamilyAsync(numberOfColors)
                    : await _colorCloudClient.GetColorsInAnalogousFamilyAsync(baseColor, numberOfColors);
            }
            else if (FamilyComplementary.IsChecked == true)
            {
                families = baseColor == null
                    ? await _colorCloudClient.GetColorsInComplementaryFamilyAsync(numberOfColors)
                    : await _colorCloudClient.GetColorsInComplementaryFamilyAsync(baseColor, numberOfColors);
            }
            else if (FamilyContrasting.IsChecked == true)
            {
                families = baseColor == null
                    ? await _colorCloudClient.GetColorsInContrastingFamilyAsync(numberOfColors)
                    : await _colorCloudClient.GetColorsInContrastingFamilyAsync(baseColor, numberOfColors);
            }
            else if (FamilyTriad.IsChecked == true)
            {
                families = baseColor == null
                    ? await _colorCloudClient.GetColorsInTriadFamilyAsync(numberOfColors)
                    : await _colorCloudClient.GetColorsInTriadFamilyAsync(baseColor, numberOfColors);
            }
            else
            {
                families = baseColor == null
                    ? await _colorCloudClient.GetColorsInTetradFamilyAsync(numberOfColors)
                    : await _colorCloudClient.GetColorsInTetradFamilyAsync(baseColor, numberOfColors);
            }

            return new Collection<GeoColor>(families.Values.SelectMany(colors => colors).ToList());
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
