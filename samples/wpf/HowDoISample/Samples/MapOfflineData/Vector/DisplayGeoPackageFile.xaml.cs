using System;
using System.Collections.Generic;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for DisplayGeoPackageFile.xaml
    /// </summary>
    public partial class DisplayGeoPackageFile 
    {
        public DisplayGeoPackageFile()
        {
            InitializeComponent();
        }

        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MapView.MapUnit = GeographyUnit.Meter;

                // Create the Cloud Aerial Overlay as the base overlay 
                var cloudOverlay = new ThinkGeoCloudRasterMapsOverlay
                {
                    ClientId = SampleKeys.ClientId,
                    ClientSecret = SampleKeys.ClientSecret,
                    MapType = ThinkGeoCloudRasterMapsMapType.Aerial_V2_X1
                };
                MapView.Overlays.Add(cloudOverlay);

                // Creat a new layerOverlay to hold the gdalFeatureLayers
                var layerOverlay = new LayerOverlay();
                var projectionConverter = new ProjectionConverter(26910, 3857);
                projectionConverter.Open();

                // Create the gdalFeatureLayers
                var gdalFeatureLayer = new GdalFeatureLayer(@"./Data/GeoPackage/mora_surficial_geology.gpkg");
                gdalFeatureLayer.FeatureSource.ProjectionConverter = projectionConverter;
                gdalFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimplePointStyle(PointSymbolType.Circle, GeoColors.LightRed, 4);
                gdalFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColor.FromArgb(128, GeoColors.LightSteelBlue), 2F, GeoColors.Black, 2F, false);
                gdalFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColor.FromArgb(64, GeoColors.LightGreen), GeoColors.Black, 1);
                gdalFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
                layerOverlay.Layers.Add(gdalFeatureLayer);

                MapView.Overlays.Add(layerOverlay);

                gdalFeatureLayer.Open();
                MapView.CurrentExtent = gdalFeatureLayer.GetBoundingBox();

                await MapView.RefreshAsync();

                projectionConverter.Close();
                gdalFeatureLayer.Close();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }
    }
}
