using OSGeo.GDAL;
using OSGeo.OSR;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    internal enum NdfdMapProjectionMode
    {
        Lambert,
        WebMercator
    }

    public partial class PerfTestRefreshNdfdSkyGrid : IDisposable
    {
        private const string DefaultDataPath = @".\Data\Ndfd\ds.sky.bin";

        private bool isPlaying;
        private readonly Stopwatch frameStopwatch = new Stopwatch();
        private NdfdSkyFrameSequence frameSequence;
        private GridOverlay<byte> bitmapOverlay;
        private int currentFrameIndex;
        private NdfdMapProjectionMode currentProjectionMode = NdfdMapProjectionMode.Lambert;

        public PerfTestRefreshNdfdSkyGrid()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;
            MapView.BackgroundOverlay.BackgroundBrush = new GeoSolidBrush(GeoColor.FromArgb(255, 19, 24, 32));

            DataPathText.Text = DefaultDataPath;
            currentProjectionMode = GetSelectedProjectionMode();
            LoadSequence(DefaultDataPath);
        }

        private void AnimationStep()
        {
            if (!isPlaying)
                return;

            ShowFrame(currentFrameIndex + 1);
            Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(AnimationStep));
        }

        private void ResamplingMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyResamplingSelection();
        }

        private void ProjectionMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded)
                return;

            NdfdMapProjectionMode selectedMode = GetSelectedProjectionMode();
            if (selectedMode == currentProjectionMode && frameSequence != null)
                return;

            currentProjectionMode = selectedMode;
            LoadSequence(DefaultDataPath);
        }

        private void AddStateOutlineOverlay(Projection targetProjection)
        {
            if (MapView.Overlays.Contains("StateOverlay"))
                MapView.Overlays.Remove("StateOverlay");

            LayerOverlay stateOverlay = new LayerOverlay();
            ShapeFileFeatureLayer statesLayer = new ShapeFileFeatureLayer(@"./Data/Shapefile/USStates_3857.shp");
            if (currentProjectionMode == NdfdMapProjectionMode.Lambert && targetProjection != null)
            {
                statesLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(3857, targetProjection.ProjString)
                {
                    SuppressException = true
                };
            }

            statesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(
                GeoColor.FromArgb(18, 255, 255, 255),
                GeoColor.FromArgb(110, 200, 210, 220),
                1);
            statesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            stateOverlay.Layers.Add("States", statesLayer);
            MapView.Overlays.Add("StateOverlay", stateOverlay);
        }

        private void LoadSequence(string filePath)
        {
            isPlaying = false;
            frameStopwatch.Reset();

            frameSequence?.Dispose();
            bitmapOverlay?.Dispose();
            frameSequence = null;
            bitmapOverlay = null;
            currentFrameIndex = 0;

            try
            {
                frameSequence = new NdfdSkyFrameSequence(filePath, currentProjectionMode);
                AddStateOutlineOverlay(frameSequence.MapProjection);

                bitmapOverlay = new GridOverlay<byte>(frameSequence, CreateSkyStyleFunc(GetSelectedColorStyle()), frameSequence.SourceProjection, frameSequence.MapProjection);
                ApplyResamplingSelection();

                if (MapView.Overlays.Contains("NdfdSkyOverlay"))
                    MapView.Overlays.Remove("NdfdSkyOverlay");

                MapView.Overlays.Add("NdfdSkyOverlay", bitmapOverlay);

                RectangleShape displayExtent = ProjectExtent(frameSequence.Extent, frameSequence.SourceProjection, frameSequence.MapProjection);
                if (displayExtent != null)
                {
                    MapView.CenterPoint = displayExtent.GetCenterPoint();
                    MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, displayExtent, MapView.MapWidth, MapView.MapHeight) * 1.05;
                }

                ShowFrame(0);
                _ = MapView.RefreshAsync();

                isPlaying = true;
                frameStopwatch.Restart();
                Dispatcher.BeginInvoke(DispatcherPriority.Background, new Action(AnimationStep));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Failed to load NDFD sky grid");
            }
        }

        private NdfdMapProjectionMode GetSelectedProjectionMode()
        {
            ComboBoxItem selectedItem = ProjectionMode?.SelectedItem as ComboBoxItem;
            if (selectedItem != null && string.Equals(selectedItem.Tag as string, "WebMercator", StringComparison.Ordinal))
                return NdfdMapProjectionMode.WebMercator;

            return NdfdMapProjectionMode.Lambert;
        }

        private void ShowFrame(int frameIndex)
        {
            if (frameSequence == null || frameSequence.FrameCount == 0 || bitmapOverlay == null)
                return;

            int boundedIndex = frameIndex % frameSequence.FrameCount;
            if (boundedIndex < 0)
                boundedIndex += frameSequence.FrameCount;

            long intervalMs = frameStopwatch.ElapsedMilliseconds;
            frameStopwatch.Restart();

            currentFrameIndex = boundedIndex;
            frameSequence.LoadFrame(currentFrameIndex);
            bitmapOverlay.InvalidateValues();

            long renderMs = frameStopwatch.ElapsedMilliseconds;

            DateTime? frameTime = frameSequence.GetFrameTime(currentFrameIndex);
            string timeLabel = frameTime.HasValue
                ? frameTime.Value.ToString("yyyy-MM-dd HH:mm") + " UTC"
                : "(no time)";

            string timingLine = isPlaying && intervalMs > 0
                ? string.Format("Render: {0} ms  |  Interval: {1} ms  |  FPS: {2:F1}", renderMs, intervalMs, 1000.0 / intervalMs)
                : string.Empty;

            FrameStatusText.Text = string.Format(
                "Frame {0}/{1}\r\n{2}\r\nGrid: {3} x {4}\r\nMap: {5}\r\n{6}\r\n{7}",
                currentFrameIndex + 1,
                frameSequence.FrameCount,
                timeLabel,
                frameSequence.Width,
                frameSequence.Height,
                currentProjectionMode == NdfdMapProjectionMode.Lambert ? "Lambert (source)" : "3857",
                frameSequence.LookupDescription,
                timingLine).TrimEnd();
        }

        private void ApplyResamplingSelection()
        {
            if (bitmapOverlay == null || ResamplingMode == null)
                return;

            ComboBoxItem selectedItem = ResamplingMode.SelectedItem as ComboBoxItem;
            if (selectedItem == null)
                return;

            switch (selectedItem.Tag as string)
            {
                case "Auto":
                    bitmapOverlay.ResamplingMode = RasterResamplingMode.Auto;
                    break;
                case "CartographicAuto":
                    bitmapOverlay.ResamplingMode = RasterResamplingMode.CartographicAuto;
                    break;
                case "Bicubic":
                    bitmapOverlay.ResamplingMode = RasterResamplingMode.Bicubic;
                    break;
                case "Trilinear":
                    bitmapOverlay.ResamplingMode = RasterResamplingMode.Trilinear;
                    break;
                case "Bilinear":
                    bitmapOverlay.ResamplingMode = RasterResamplingMode.Bilinear;
                    break;
                case "NearestNeighbor":
                default:
                    bitmapOverlay.ResamplingMode = RasterResamplingMode.NearestNeighbor;
                    break;
            }
        }

        private void StyleRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            ApplyColorStyleSelection();
        }

        private void ApplyColorStyleSelection()
        {
            if (bitmapOverlay == null)
                return;

            bitmapOverlay.Style = CreateSkyStyleFunc(GetSelectedColorStyle());
        }

        private SkyColorStyle GetSelectedColorStyle()
        {
            if (StyleCloudGray.IsChecked == true) return SkyColorStyle.CloudGray;
            if (StyleWarmTones.IsChecked == true) return SkyColorStyle.WarmTones;
            if (StyleCoolTones.IsChecked == true) return SkyColorStyle.CoolTones;
            if (StyleViridis.IsChecked == true)   return SkyColorStyle.Viridis;
            return SkyColorStyle.WeatherRainbow;
        }

        private static Func<byte, GeoColor> CreateSkyStyleFunc(SkyColorStyle style)
        {
            GeoColor[] table = new GeoColor[101];
            for (int v = 1; v <= 100; v++)
            {
                double ratio = v / 100d;
                GetColorForSkyStyle(style, ratio, out System.Windows.Media.Color color, out byte alpha);
                table[v] = GeoColor.FromArgb(alpha, color.R, color.G, color.B);
            }
            return value => (value == 0 || value > 100) ? GeoColors.Transparent : table[value];
        }

        private static void GetColorForSkyStyle(SkyColorStyle style, double ratio, out System.Windows.Media.Color color, out byte alpha)
        {
            switch (style)
            {
                case SkyColorStyle.CloudGray:
                    color = SkyLerp(new[] { SkyRgb(200,215,230), SkyRgb(130,145,160) }, ratio);
                    alpha = (byte)(ratio * 210); break;
                case SkyColorStyle.WarmTones:
                    color = SkyLerp(new[] { SkyRgb(255,245,180), SkyRgb(255,200,0), SkyRgb(240,100,0), SkyRgb(180,0,0) }, ratio);
                    alpha = (byte)(40 + ratio * 195); break;
                case SkyColorStyle.CoolTones:
                    color = SkyLerp(new[] { SkyRgb(190,230,255), SkyRgb(0,190,240), SkyRgb(0,80,200), SkyRgb(0,10,110) }, ratio);
                    alpha = (byte)(40 + ratio * 195); break;
                case SkyColorStyle.Viridis:
                    color = SkyLerp(new[] { SkyRgb(68,1,84), SkyRgb(59,82,139), SkyRgb(33,145,140), SkyRgb(94,201,98), SkyRgb(253,231,37) }, ratio);
                    alpha = (byte)(50 + ratio * 185); break;
                default:
                    color = SkyLerp(new[] { SkyRgb(24,64,196), SkyRgb(0,174,239), SkyRgb(0,186,124), SkyRgb(246,224,52), SkyRgb(246,143,31), SkyRgb(214,37,35) }, ratio);
                    alpha = (byte)(48 + ratio * 180); break;
            }
        }

        private static System.Windows.Media.Color SkyRgb(byte r, byte g, byte b)
            => System.Windows.Media.Color.FromRgb(r, g, b);

        private static System.Windows.Media.Color SkyLerp(System.Windows.Media.Color[] stops, double ratio)
        {
            double scaled = ratio * (stops.Length - 1);
            int lo = Math.Max(0, Math.Min(stops.Length - 1, (int)Math.Floor(scaled)));
            int hi = Math.Min(stops.Length - 1, lo + 1);
            double t = scaled - lo;
            if (hi == lo) return stops[lo];
            return System.Windows.Media.Color.FromRgb(
                (byte)(stops[lo].R + t * (stops[hi].R - stops[lo].R)),
                (byte)(stops[lo].G + t * (stops[hi].G - stops[lo].G)),
                (byte)(stops[lo].B + t * (stops[hi].B - stops[lo].B)));
        }

        private static RectangleShape ProjectExtent(RectangleShape sourceExtent, Projection sourceProjection, Projection mapProjection)
        {
            if (sourceExtent == null)
                return null;

            if (sourceProjection == null || mapProjection == null ||
                string.Equals(sourceProjection.ProjString, mapProjection.ProjString, StringComparison.Ordinal))
            {
                return sourceExtent;
            }

            ProjectionConverter projectionConverter = new ProjectionConverter(sourceProjection.ProjString, mapProjection.ProjString);
            projectionConverter.Open();

            try
            {
                return projectionConverter.ConvertToExternalProjection(sourceExtent).GetBoundingBox();
            }
            finally
            {
                projectionConverter.Close();
            }
        }

        public void Dispose()
        {
            isPlaying = false;
            frameSequence?.Dispose();
            bitmapOverlay?.Dispose();
            MapView.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    internal sealed class NdfdSkyFrameSequence : GridSource<byte>, IDisposable
    {
        private readonly Dataset dataset;
        private readonly DateTime?[] frameTimes;
        private readonly string lookupDescription;
        private readonly int frameCount;
        private int loadedFrameIndex;
        private byte[] currentValues;

        public NdfdSkyFrameSequence(string filePath, NdfdMapProjectionMode mapProjectionMode)
            : this(Parse(filePath, mapProjectionMode))
        { }

        public Projection SourceProjection { get; }
        public Projection MapProjection { get; }

        private NdfdSkyFrameSequence(ParseResult result)
            : base(result.Width, result.Height, result.SourceExtent, value => value > 100)
        {
            dataset = result.Dataset;
            SourceProjection = result.SourceProjection;
            MapProjection = result.MapProjection;
            frameTimes = result.FrameTimes;
            lookupDescription = result.LookupDescription;
            frameCount = result.FrameCount;
            currentValues = new byte[result.Width * result.Height];
            loadedFrameIndex = -1;
        }

        public int FrameCount => frameCount;
        public string LookupDescription => lookupDescription;

        public DateTime? GetFrameTime(int frameIndex) => frameTimes?[frameIndex];

        public void LoadFrame(int frameIndex)
        {
            if (frameIndex == loadedFrameIndex && currentValues != null)
                return;

            Band band = dataset.GetRasterBand(frameIndex + 1);
            try
            {
                band.ReadRaster(0, 0, Width, Height, currentValues, Width, Height, 0, 0);
                loadedFrameIndex = frameIndex;
            }
            finally
            {
                band.Dispose();
            }
        }

        protected override void FillValuesCore(GridSlot<byte>[] samples)
        {
            if (currentValues == null) return;
            for (int i = 0; i < samples.Length; i++)
            {
                if (samples[i].X < 0) continue;
                int idx = samples[i].Y * Width + samples[i].X;
                samples[i].Value = (idx < currentValues.Length) ? currentValues[idx] : (byte)0;
            }
        }

        public void Dispose()
        {
            dataset.Dispose();
        }

        private static ParseResult Parse(string filePath, NdfdMapProjectionMode mapProjectionMode)
        {
            GdalManager.ConfigureGdal();

            Dataset dataset = Gdal.Open(filePath, Access.GA_ReadOnly);
            if (dataset == null)
                throw new InvalidOperationException("GDAL could not open the NDFD sky file.");

            try
            {
                int width = dataset.RasterXSize;
                int height = dataset.RasterYSize;
                int frameCount = dataset.RasterCount;

                if (width <= 0 || height <= 0 || frameCount <= 0)
                    throw new InvalidOperationException("The NDFD sky file did not expose any raster bands.");

                DateTime?[] frameTimes = new DateTime?[frameCount];
                for (int i = 0; i < frameCount; i++)
                {
                    Band band = dataset.GetRasterBand(i + 1);
                    try
                    {
                        frameTimes[i] = ParseGribValidTime(band.GetMetadataItem("GRIB_VALID_TIME", string.Empty));
                    }
                    finally
                    {
                        band.Dispose();
                    }
                }

                string sourceProjectionProj4 = GetProjectionProj4(dataset);
                if (string.IsNullOrEmpty(sourceProjectionProj4))
                    throw new InvalidOperationException("The dataset projection could not be converted to proj4.");

                RectangleShape sourceExtent = GetSourceExtent(dataset);
                if (sourceExtent == null)
                    throw new InvalidOperationException("The dataset geotransform could not be converted to a valid source extent.");

                bool usesProjectedLookup = mapProjectionMode == NdfdMapProjectionMode.WebMercator;
                string mapProjectionProj4 = usesProjectedLookup
                    ? Projection.GetSphericalMercatorProjString()
                    : sourceProjectionProj4;

                Projection sourceProjection = new Projection(sourceProjectionProj4);
                Projection mapProjection    = new Projection(mapProjectionProj4);

                return new ParseResult(
                    dataset,
                    width,
                    height,
                    frameCount,
                    sourceExtent,
                    frameTimes,
                    sourceProjection,
                    mapProjection,
                    usesProjectedLookup
                        ? "Projected lookup cached per view"
                        : "Source projection direct lookup");
            }
            catch
            {
                dataset.Dispose();
                throw;
            }
        }

        private static DateTime? ParseGribValidTime(string metadataValue)
        {
            if (string.IsNullOrEmpty(metadataValue)) return null;
            string numPart = metadataValue.Split(' ')[0].Trim();
            if (long.TryParse(numPart, out long unixSeconds))
                return DateTimeOffset.FromUnixTimeSeconds(unixSeconds).UtcDateTime;
            return null;
        }

        private static string GetProjectionProj4(Dataset dataset)
        {
            string projectionWkt = dataset.GetProjection();
            if (string.IsNullOrEmpty(projectionWkt))
                projectionWkt = dataset.GetProjectionRef();
            if (string.IsNullOrEmpty(projectionWkt))
                return string.Empty;

            SpatialReference sr = new SpatialReference(string.Empty);
            string wkt = projectionWkt;
            if (sr.ImportFromWkt(ref wkt) != 0) { sr.Dispose(); return string.Empty; }
            sr.ExportToProj4(out string proj4);
            sr.Dispose();
            return proj4 ?? string.Empty;
        }

        private static RectangleShape GetSourceExtent(Dataset dataset)
        {
            double[] gt = new double[6];
            dataset.GetGeoTransform(gt);
            if (Math.Abs(gt[1] * gt[5] - gt[2] * gt[4]) < 1e-12) return null;

            double[] xs = new double[4], ys = new double[4];
            GetProjectedPoint(gt, 0, 0, out xs[0], out ys[0]);
            GetProjectedPoint(gt, dataset.RasterXSize, 0, out xs[1], out ys[1]);
            GetProjectedPoint(gt, 0, dataset.RasterYSize, out xs[2], out ys[2]);
            GetProjectedPoint(gt, dataset.RasterXSize, dataset.RasterYSize, out xs[3], out ys[3]);

            double minX = xs[0], maxX = xs[0], minY = ys[0], maxY = ys[0];
            for (int i = 1; i < 4; i++)
            {
                if (xs[i] < minX) minX = xs[i]; if (xs[i] > maxX) maxX = xs[i];
                if (ys[i] < minY) minY = ys[i]; if (ys[i] > maxY) maxY = ys[i];
            }
            return new RectangleShape(minX, maxY, maxX, minY);
        }

        private static void GetProjectedPoint(double[] gt, int px, int py, out double x, out double y)
        {
            x = gt[0] + px * gt[1] + py * gt[2];
            y = gt[3] + px * gt[4] + py * gt[5];
        }

        private sealed class ParseResult
        {
            public ParseResult(
                Dataset dataset,
                int width,
                int height,
                int frameCount,
                RectangleShape sourceExtent,
                DateTime?[] frameTimes,
                Projection sourceProjection,
                Projection mapProjection,
                string lookupDescription)
            {
                Dataset = dataset;
                Width = width;
                Height = height;
                FrameCount = frameCount;
                SourceExtent = sourceExtent;
                FrameTimes = frameTimes;
                SourceProjection = sourceProjection;
                MapProjection = mapProjection;
                LookupDescription = lookupDescription;
            }

            public Dataset Dataset { get; }
            public int Width { get; }
            public int Height { get; }
            public int FrameCount { get; }
            public RectangleShape SourceExtent { get; }
            public DateTime?[] FrameTimes { get; }
            public Projection SourceProjection { get; }
            public Projection MapProjection { get; }
            public string LookupDescription { get; }
        }
    }

    internal enum SkyColorStyle { WeatherRainbow, CloudGray, WarmTones, CoolTones, Viridis }
}
