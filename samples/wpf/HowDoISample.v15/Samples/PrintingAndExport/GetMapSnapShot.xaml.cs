using System;
using System.Globalization;
using System.IO;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Take a snapshot of the map - as a plain image, or with the world file and
    /// projection file that make it open as a placed raster in any GIS.
    /// </summary>
    public partial class GetMapSnapShot
    {

        private bool _initialized;
        public GetMapSnapShot()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service and add it to the map.
            // Set up the tile cache for the GpuBasemap, passing in the location and an ID to distinguish the cache. 
            var thinkGeoCloudVectorMapsOverlay = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey)));
            Map.Basemap = thinkGeoCloudVectorMapsOverlay;

            var simpleMarkerOverlay = new SimpleMarkerOverlay();
            Map.Overlays.Add(simpleMarkerOverlay);

            // set the map extent to Frisco, TX
            Map.CenterPoint = new PointShape(-10779270, 3911750);
            Map.CurrentScale = 288900;

            // Add a marker in the center of the map. 
            var marker = new Marker(Map.CenterPoint);
            simpleMarkerOverlay.Markers.Add(marker);

            _ = Map.RefreshAsync();
        }
        private void btnGetSnapshot_Click(object sender, RoutedEventArgs e)
        {
            var snapShot = Map.GetSnapshot();
            snapShot.Save(@".\snapshot.png");

            var fullPath = Path.GetFullPath(@".\snapshot.png");
            MessageBox.Show($"The snapshot image was saved at this path: {fullPath}");
        }

        private void btnGetGeoreferencedSnapshot_Click(object sender, RoutedEventArgs e)
        {
            var snapShot = Map.GetSnapshot();
            snapShot.Save(@".\snapshot.png");

            // The world file georeferences the pixels: map units per pixel, then the
            // world position of the top-left pixel's CENTER. With it and the .prj
            // naming the projection, the png opens as a placed raster in any GIS.
            var extent = Map.CurrentExtent;
            var pixelWidth = extent.Width / snapShot.Width;
            var pixelHeight = extent.Height / snapShot.Height;
            File.WriteAllLines(@".\snapshot.pgw", new[]
            {
                pixelWidth.ToString(CultureInfo.InvariantCulture),
                "0",
                "0",
                (-pixelHeight).ToString(CultureInfo.InvariantCulture),
                (extent.UpperLeftPoint.X + (pixelWidth / 2)).ToString(CultureInfo.InvariantCulture),
                (extent.UpperLeftPoint.Y - (pixelHeight / 2)).ToString(CultureInfo.InvariantCulture),
            });
            File.WriteAllText(@".\snapshot.prj", Projection.ConvertEpsgToWkt(3857));

            var fullPath = Path.GetFullPath(@".\snapshot.png");
            MessageBox.Show($"Saved snapshot.png with snapshot.pgw and snapshot.prj at: {fullPath}");
        }
    }
}