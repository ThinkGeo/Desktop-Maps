using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interpolate soil pH sample points into an ESRI ASCII grid file and show the grid.
    /// </summary>
    public partial class GenerateESRIGridFile : IDisposable
    {
        private bool _initialized;
        private LayerOverlay _pointsOverlay;
        private GridOverlay _gridOverlay;

        public GenerateESRIGridFile()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;
            Map.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColors.Snow);
            Map.Overlays.Add(new ThinkGeoCloudRasterMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudRasterMapsMapType.Hybrid2_V2_X1
            });

            const byte alpha = 180;
            const int symbolSize = 10;
            var phStyle = new ClassBreakStyle("PH");
            phStyle.ClassBreaks.Add(new ClassBreak(double.MinValue, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColors.Transparent))));
            phStyle.ClassBreaks.Add(new ClassBreak(6.2, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 255, 0, 0)))));
            phStyle.ClassBreaks.Add(new ClassBreak(6.83, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 255, 128, 0)))));
            phStyle.ClassBreaks.Add(new ClassBreak(7.0, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 245, 210, 10)))));
            phStyle.ClassBreaks.Add(new ClassBreak(7.08, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 225, 255, 0)))));
            phStyle.ClassBreaks.Add(new ClassBreak(7.15, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 224, 251, 132)))));
            phStyle.ClassBreaks.Add(new ClassBreak(7.21, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 128, 255, 128)))));
            phStyle.ClassBreaks.Add(new ClassBreak(7.54, new PointStyle(PointSymbolType.Circle, symbolSize, new GeoSolidBrush(GeoColor.FromArgb(alpha, 0, 255, 0)))));

            var samplesLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "sample_ph_2.shp"));
            samplesLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(phStyle);
            samplesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _pointsOverlay = new LayerOverlay { TileType = TileType.SingleTile };
            _pointsOverlay.Layers.Add(samplesLayer);
            Map.Overlays.Add(_pointsOverlay);

            samplesLayer.Open();
            Map.CurrentExtent = samplesLayer.GetBoundingBox();
            samplesLayer.Close();

            _ = Map.RefreshAsync();
        }

        private async void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            // The user's own folder, not the application's: an installed application's folder is usually read-only.
            var gridFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ThinkGeo", "HowDoI", "GridFile");
            var gridFile = Path.Combine(gridFolder, "generated.asc");
            GenerateButton.IsEnabled = false;
            Status.Text = "Generating...";

            // The grid overlay keeps its file open, so it goes before the file is written again.
            if (_gridOverlay != null)
            {
                Map.Overlays.Remove(_gridOverlay);
                _gridOverlay.Dispose();
                _gridOverlay = null;
            }

            try
            {
                Directory.CreateDirectory(gridFolder);
                await Task.Run(() => GenerateGrid(gridFile));
                ShowGrid(gridFile);
                Status.Text = "Written to " + gridFile;
            }
            catch (Exception exception)
            {
                Status.Text = "Failed: " + exception.Message;
            }

            GenerateButton.IsEnabled = true;
            _ = Map.RefreshAsync();
        }

        private static void GenerateGrid(string gridFile)
        {
            var pointsLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "sample_ph_2.shp"));
            pointsLayer.Open();
            var gridExtent = pointsLayer.GetBoundingBox();
            gridExtent.ScaleUp(5);
            var features = pointsLayer.FeatureSource.GetAllFeatures(new[] { "PH" });
            pointsLayer.Close();

            var dataPoints = new Dictionary<PointShape, double>();
            foreach (var feature in features)
            {
                dataPoints.Add((PointShape)feature.GetShape(), double.Parse(feature.ColumnValues["PH"], CultureInfo.InvariantCulture));
            }

            // 100 cells across; -9999 marks a cell with no value. See http://en.wikipedia.org/wiki/ASCII_GRID
            var definition = new GridDefinition(gridExtent, gridExtent.Width / 100, -9999, dataPoints);

            // Inverse distance weighting fills each cell from its known neighbors. See http://en.wikipedia.org/wiki/Inverse_distance_weighting
            var interpolationModel = new InverseDistanceWeightedGridInterpolationModel(2, double.MaxValue);

            using (var outputStream = new FileStream(gridFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                GridFeatureSource.GenerateGrid(definition, interpolationModel, outputStream);
            }
        }

        private void ShowGrid(string gridFile)
        {
            _pointsOverlay.IsVisible = false;

            // The pH bands, each color held from its value up to the next; below 6.2 nothing is drawn.
            var gridStyle = new GridFillStyle
            {
                RampMode = ColorRampMode.Classes,
                Opacity = 150 / 255f,
                ColorStops = new[]
                {
                    new ColorStop(0, GeoColors.Transparent),
                    new ColorStop(6.2, GeoColor.FromArgb(255, 255, 0, 0)),
                    new ColorStop(6.83, GeoColor.FromArgb(255, 255, 128, 0)),
                    new ColorStop(7.0, GeoColor.FromArgb(255, 245, 210, 10)),
                    new ColorStop(7.08, GeoColor.FromArgb(255, 225, 255, 0)),
                    new ColorStop(7.15, GeoColor.FromArgb(255, 224, 251, 132)),
                    new ColorStop(7.21, GeoColor.FromArgb(255, 128, 255, 128)),
                    new ColorStop(7.54, GeoColor.FromArgb(255, 0, 255, 0)),
                },
            };

            // Nearest neighbor keeps the cells sharp when the map zooms in.
            _gridOverlay = new GridOverlay(new AscGridSource(gridFile), gridStyle) { ResamplingMode = RasterResamplingMode.NearestNeighbor };
            Map.Overlays.Add(_gridOverlay);
        }

        public void Dispose()
        {
            _gridOverlay?.Dispose();
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
