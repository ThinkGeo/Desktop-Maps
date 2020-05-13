using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.MbTiles;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace MBTilesExtractor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const long maxZoom = 14;
        private LayerOverlay layerOverlay;
        private Proj4Projection projection;

        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.wpfMap.MapUnit = GeographyUnit.Meter;
            this.wpfMap.ZoomLevelSet = new ThinkGeoCloudMapsZoomLevelSet();
            this.wpfMap.TrackOverlay.TrackEnded += TrackOverlay_TrackEnded;

            projection = new Proj4Projection(3857, 4326);
            projection.Open();

            // Create background world map with vector tile requested from loacl Database. 
            ThinkGeoMBTilesFeatureLayer thinkGeoMBTilesFeatureLayer = new ThinkGeoMBTilesFeatureLayer("Data/tiles_Frisco.mbtiles", new Uri("Data/thinkgeo-world-streets-light.json", UriKind.Relative));
            thinkGeoMBTilesFeatureLayer.BitmapTileCache = null;

            layerOverlay = new LayerOverlay();
            layerOverlay.MaxExtent = thinkGeoMBTilesFeatureLayer.GetTileMatrixBoundingBox();
            layerOverlay.TileWidth = 512;
            layerOverlay.TileHeight = 512;
            layerOverlay.Layers.Add(thinkGeoMBTilesFeatureLayer);

            this.wpfMap.Overlays.Add(layerOverlay);
            this.wpfMap.CurrentExtent = new RectangleShape(-10780508.5162109, 3916643.16078401, -10775922.2945393, 3914213.89649231);
            this.wpfMap.Refresh();
        }

        private void TrackOverlay_TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            InMemoryFeatureLayer trackShapeLayer = wpfMap.TrackOverlay.TrackShapeLayer;

            string localFilePath;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "MB Tiles Database(*.mbtiles)|*.mbtiles";
            if (saveFileDialog.ShowDialog().Value)
            {
                localFilePath = saveFileDialog.FileName.ToString();
                if (File.Exists(localFilePath)) File.Delete(localFilePath);

                RectangleShape extractingBBox = trackShapeLayer.InternalFeatures.First().GetBoundingBox();
                var extent = projection.ConvertToExternalProjection(extractingBBox);
                txtUpperLeftPoint.Text = string.Format("{0:N6},{1:N6}", extent.UpperLeftPoint.X, extent.UpperLeftPoint.Y);
                txtLowerRightPoint.Text = string.Format("{0:N6},{1:N6}", extent.LowerRightPoint.X, extent.LowerRightPoint.Y);

                wpfMap.TrackOverlay.TrackMode = TrackMode.None;
                pbExtractProgress.Visibility = Visibility.Visible;
                pbExtractProgress.IsEnabled = true;
                Task.Factory.StartNew(() =>
                {
                    ExtractTiles(extractingBBox, localFilePath);
                    Dispatcher.Invoke(() =>
                    {
                        pbExtractProgress.IsEnabled = false;
                        pbExtractProgress.Visibility = Visibility.Hidden;
                        trackShapeLayer.InternalFeatures.Clear();
                        wpfMap.Refresh(wpfMap.TrackOverlay);
                        wpfMap.TrackOverlay.TrackMode = TrackMode.Rectangle;

                        ThinkGeoMBTilesFeatureLayer thinkGeoMBTilesFeatureLayer = new ThinkGeoMBTilesFeatureLayer(localFilePath, new Uri("Data/thinkgeo-world-streets-light.json", UriKind.Relative));
                        thinkGeoMBTilesFeatureLayer.BitmapTileCache = null;
                        layerOverlay.Layers.Clear();
                        layerOverlay.Layers.Add(thinkGeoMBTilesFeatureLayer);
                        wpfMap.Refresh(layerOverlay);
                    });
                });
            }
            else
                trackShapeLayer.InternalFeatures.Clear();
        }

        private void ChkExtractingData_Checked(object sender, RoutedEventArgs e)
        {
            wpfMap.TrackOverlay.TrackMode = chkExtractingData.IsChecked.Value ? TrackMode.Rectangle : TrackMode.None;
        }

        private void ExtractTiles(RectangleShape bbox, string targetFilePath)
        {
            List<VectorTileRange> tileRanges = new List<VectorTileRange>();
            for (int zoomLevel = 0; zoomLevel <= maxZoom; zoomLevel++)
            {
                VectorTileRange tileRange = GetTileRange(zoomLevel, bbox);
                tileRanges.Add(tileRange);
            }

            MbtilesDatabase.CreateFileDatabase(targetFilePath);
            MbtilesDatabase targetDB = MbtilesDatabase.OpenDatabase(targetFilePath);

            MbtilesDatabase sourceDB = MbtilesDatabase.OpenDatabase("Data/tiles_Frisco.mbtiles");
            sourceDB.Metadata.NextPage();
            var wgs84BBox = projection.ConvertToExternalProjection(bbox);
            foreach (MetadataEntry entry in sourceDB.Metadata.Entries)
            {
                if (entry.Name.Equals("center"))
                {
                    PointShape centerPoint = wgs84BBox.GetCenterPoint();
                    entry.Value = $"{centerPoint.X},{centerPoint.Y},{maxZoom}";
                }
            }
            sourceDB.Metadata.Entries.Add(new MetadataEntry() { Name = "bounds", Value = $"{wgs84BBox.UpperLeftPoint.X},{wgs84BBox.UpperLeftPoint.Y},{wgs84BBox.LowerRightPoint.X},{wgs84BBox.LowerRightPoint.Y}" });
            targetDB.Metadata.Insert(sourceDB.Metadata.Entries);

            int recordLimit = 1000;
            foreach (var tileRange in tileRanges)
            {
                long offset = 0;
                bool isEnd = false;
                while (!isEnd)
                {
                    string querySql = $"SELECT * FROM {sourceDB.Map.TableName} WHERE " + ConvetToSqlString(tileRange) + $" LIMIT {offset},{recordLimit}";
                    var entries = sourceDB.Map.Query(querySql);
                    targetDB.Map.Insert(entries);

                    if (entries.Count < recordLimit)
                        isEnd = true;

                    querySql = $"SELECT images.tile_data as tile_data, images.tile_id as tile_id FROM {sourceDB.Images.TableName} WHERE images.tile_id IN ( SELECT {Map.TileIdColumnName} FROM {sourceDB.Map.TableName} WHERE " + ConvetToSqlString(tileRange) + $" LIMIT {offset},{recordLimit} )";
                    entries = sourceDB.Images.Query(querySql);
                    targetDB.Images.Insert(entries);

                    offset = offset + recordLimit;
                }
            }
        }

        private string ConvetToSqlString(VectorTileRange tileRange)
        {
            long rowCount = (long)Math.Pow(2, tileRange.Zoom) - 1;

            StringBuilder sqlStringBuilder = new StringBuilder();
            sqlStringBuilder.Append(Map.ZoomLevelColumnName);
            sqlStringBuilder.Append(" = ");
            sqlStringBuilder.Append(tileRange.Zoom);
            sqlStringBuilder.Append(" AND ");
            sqlStringBuilder.Append(Map.TileColColumnName);
            sqlStringBuilder.Append(" >= ");
            sqlStringBuilder.Append(tileRange.MinColumn);
            sqlStringBuilder.Append(" AND ");
            sqlStringBuilder.Append(Map.TileColColumnName);
            sqlStringBuilder.Append(" <= ");
            sqlStringBuilder.Append(tileRange.MaxColumn);
            sqlStringBuilder.Append(" AND ");
            sqlStringBuilder.Append(Map.TileRowColumnName);
            sqlStringBuilder.Append(" >= ");
            sqlStringBuilder.Append(rowCount - tileRange.MaxRow);
            sqlStringBuilder.Append(" AND ");
            sqlStringBuilder.Append(Map.TileRowColumnName);
            sqlStringBuilder.Append(" <= ");
            sqlStringBuilder.Append(rowCount - tileRange.MinRow);

            return sqlStringBuilder.ToString();
        }

        private VectorTileRange GetTileRange(int zoomLevel, RectangleShape extent)
        {
            RectangleShape worldExtent = new RectangleShape(-20037508.2314698, 20037508.2314698, 20037508.2314698, -20037508.2314698);

            extent = worldExtent.GetIntersection(extent);

            long rowCount = (long)Math.Pow(2, zoomLevel);
            double cellWidth = worldExtent.Width / rowCount;
            double cellHeight = worldExtent.Height / rowCount;
            long minX = (long)Math.Floor((extent.UpperLeftPoint.X - worldExtent.UpperLeftPoint.X) / cellWidth);
            long minY = (long)Math.Floor((worldExtent.UpperLeftPoint.Y - extent.UpperLeftPoint.Y) / cellHeight);
            long maxX = (long)Math.Floor((extent.UpperRightPoint.X - worldExtent.UpperLeftPoint.X) / cellWidth);
            long maxY = (long)Math.Floor((worldExtent.UpperLeftPoint.Y - extent.LowerLeftPoint.Y) / cellHeight);

            return new VectorTileRange(zoomLevel, minX, minY, maxX, maxY);
        }
    }

    internal class VectorTileRange
    {
        public VectorTileRange()
            : this(0, 0, 0, 0, 0)
        { }

        public VectorTileRange(long zoom, long minColumn, long minRow, long maxColumn, long maxRow)
        {
            Zoom = zoom;
            MinColumn = minColumn;
            MinRow = minRow;
            MaxColumn = maxColumn;
            MaxRow = maxRow;
        }

        public long Zoom { get; set; }

        public long MinColumn { get; set; }

        public long MaxColumn { get; set; }

        public long MinRow { get; set; }

        public long MaxRow { get; set; }
    }
}
