using Microsoft.Data.Sqlite;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace MBTilesExtractor
{
    public partial class MainWindow : Window
    {
        private VectorMbTilesAsyncLayer _mvtLayer;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MapView.MapUnit = GeographyUnit.Meter;

            var overlay = new LayerOverlay();
            MapView.Overlays.Add(overlay);

            MapView.TrackOverlay.TrackEnded += TrackOverlay_TrackEnded;
        }

        private void MapView_DragOver(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.None;
            }
            else
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                e.Effects = files.Length == 1 ? DragDropEffects.Copy : DragDropEffects.None;
            }
            e.Handled = true;
        }

        private void MapView_Drop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length != 1)
            {
                MessageBox.Show("Please drag and drop only one file.", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Instruction.Visibility = Visibility.Collapsed;
            _ = LoadFileOnMap(files[0]);
        }

        private async void TrackOverlay_TrackEnded(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            if (_mvtLayer == null) return;

            var trackShapeLayer = MapView.TrackOverlay.TrackShapeLayer;

            var saveFileDialog = new SaveFileDialog { Filter = "MBTiles (*.mbtiles)|*.mbtiles" };
            if (saveFileDialog.ShowDialog() != true)
            {
                trackShapeLayer.InternalFeatures.Clear();
                return;
            }

            string targetFilePath = saveFileDialog.FileName;
            if (File.Exists(targetFilePath)) File.Delete(targetFilePath);

            var polygon = new PolygonShape(trackShapeLayer.InternalFeatures.First().GetWellKnownBinary());

            try
            {
                MapView.IsEnabled = false;
                Mouse.OverrideCursor = Cursors.Wait;
                pbExtractProgress.Visibility = Visibility.Visible;
                pbExtractProgress.IsIndeterminate = true; // indeterminate until we start the copy phase
                SetProgress(0);

                MapView.TrackOverlay.TrackMode = TrackMode.None;

                // Do the extraction with accurate progress
                int total = await ExtractTilesAsync(polygon, targetFilePath);

                MessageBox.Show($"Extraction complete.\nTiles processed: {total}", "Done",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Extraction Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                pbExtractProgress.Visibility = Visibility.Hidden;
                pbExtractProgress.IsIndeterminate = false;
                SetProgress(0);

                Mouse.OverrideCursor = null;
                MapView.IsEnabled = true;

                trackShapeLayer.InternalFeatures.Clear();
                MapView.TrackOverlay.TrackMode = TrackMode.Rectangle;
                await MapView.TrackOverlay.RefreshAsync();
                chkExtractingData.IsChecked = false;
            }
        }

        private void ChkExtractingData_Checked(object sender, RoutedEventArgs e)
        {
            MapView.TrackOverlay.TrackMode =
                chkExtractingData.IsChecked == true ? TrackMode.Rectangle : TrackMode.None;
        }

        // Create an empty MBTiles file (no ThinkGeo dependency)
        private static void CreateEmptyMbtilesDatabase(string filePath)
        {
            if (File.Exists(filePath))
                File.Delete(filePath);

            using var conn = new SqliteConnection($"Data Source={filePath}");
            conn.Open();

            using var cmd = conn.CreateCommand();
            // In CreateEmptyMbtilesDatabase(...)
            cmd.CommandText = @"
    CREATE TABLE metadata (
        name  TEXT PRIMARY KEY,        -- or: UNIQUE(name)
        value TEXT
    );
    CREATE TABLE tiles (
        zoom_level  INTEGER,
        tile_column INTEGER,
        tile_row    INTEGER,
        tile_data   BLOB
    );
    CREATE UNIQUE INDEX tile_index 
        ON tiles (zoom_level, tile_column, tile_row);
";

            cmd.ExecuteNonQuery();
        }

        // Async extractor with accurate progress
        private async Task<int> ExtractTilesAsync(PolygonShape polygonShape, string targetFilePath)
        {
            var bbox = polygonShape.GetBoundingBox();
            var tileRanges = Enumerable.Range(0, _mvtLayer.MaxZoomOfData + 1)
                                       .Select(z => GetTileRange(z, bbox))
                                       .ToList();

            CreateEmptyMbtilesDatabase(targetFilePath);

            var sourceFilePath = _mvtLayer.FilePath;
            var srcCs = $"Data Source=file:{sourceFilePath}?immutable=1;Mode=ReadOnly;Cache=Private";

            // Disable pooling on target so handles are released deterministically
            var tgtCs = new SqliteConnectionStringBuilder
            {
                DataSource = targetFilePath,
                Cache = SqliteCacheMode.Private,
                Pooling = false
            }.ToString();

            using var sourceConn = new SqliteConnection(srcCs);
            using var targetConn = new SqliteConnection(tgtCs);

            sourceConn.Open();
            targetConn.Open();
            ConfigureSqliteForWriter(targetConn, isNewDb: false);

            int totalProcessed = 0;

            // Wrap DB work off the UI thread
            await Task.Run(() =>
            {
                // Build helpers inside the background task
                var targetMap = new TilesTable(targetConn);
                var targetMetadata = new MetadataTable(targetConn);
                var sourceMap = new TilesTable(sourceConn);
                var sourceMetadata = new MetadataTable(sourceConn);
                {
                    // ----- Phase 1: accurate total count (indeterminate while counting) -----
                    Dispatcher.Invoke(() => pbExtractProgress.IsIndeterminate = true);

                    long totalToCopy = 0;
                    foreach (var range in tileRanges)
                    {
                        string countSql = $"SELECT COUNT(*) FROM {sourceMap.TableName} WHERE {ToSqlWhere(range)}";
                        //totalToCopy += sourceMap.ScalarCount(countSql); // assume TilesTable exposes a COUNT helper; if not, add one similar to Query()

                        totalToCopy += ExecuteScalarInt64(sourceConn, countSql);
                    }

                    // Handle empty result set
                    if (totalToCopy == 0)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            pbExtractProgress.IsIndeterminate = false;
                            SetProgress(100);
                        });
                        return; // nothing to insert
                    }

                    // ----- Phase 2: copy with true percentage -----
                    Dispatcher.Invoke(() =>
                    {
                        pbExtractProgress.IsIndeterminate = false;
                        SetProgress(0);
                    });

                    const int recordLimit = 5000;

                    foreach (var tileRange in tileRanges)
                    {
                        long offset = 0;
                        while (true)
                        {
                            string querySql =
                                $"SELECT {TilesTable.ZoomLevelColumnName}, {TilesTable.TileColColumnName}, {TilesTable.TileRowColumnName}, {TilesTable.TileDataColumnName} " +
                                $"FROM {sourceMap.TableName} WHERE {ToSqlWhere(tileRange)} " +
                                $"ORDER BY {TilesTable.ZoomLevelColumnName}, {TilesTable.TileRowColumnName}, {TilesTable.TileColColumnName} " +
                                $"LIMIT {recordLimit} OFFSET {offset}";

                            var entries = sourceMap.Query(querySql);
                            if (entries.Count == 0) break;

                            // If you want geometry filtering, do it here before insert.
                            targetMap.Insert(entries);

                            totalProcessed += entries.Count;
                            double percent = (totalProcessed * 100.0) / totalToCopy;
                            Dispatcher.Invoke(() => SetProgress(percent));

                            if (entries.Count < recordLimit) break;
                            offset += recordLimit;
                        }
                    }
                }

                // Build from source + bbox
                var meta = BuildMetadata(sourceMetadata.Entries, bbox, _mvtLayer.MaxZoomOfData, targetFilePath);

                // Write in its own transaction
                UpsertMetadata(targetConn, meta);

                // ANALYZE + finalize after wrappers are disposed
                using (var cmd = targetConn.CreateCommand())
                {
                    cmd.CommandText = "ANALYZE;";
                    cmd.ExecuteNonQuery();
                }

                FinalizeSqliteWriter(targetConn);
                targetConn.Close();
                targetConn.Dispose();
                TryDeleteSidecars(targetFilePath);

            });

            return totalProcessed;
        }

        public static long ExecuteScalarInt64(SqliteConnection conn, string sql)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            var obj = cmd.ExecuteScalar();
            return (obj == null || obj == DBNull.Value) ? 0 : Convert.ToInt64(obj);
        }

        private static void ConfigureSqliteForWriter(SqliteConnection conn, bool isNewDb)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
        PRAGMA journal_mode=WAL;      -- fast concurrent writes
        PRAGMA synchronous=OFF;       -- you’re already in an app-controlled bulk op
        PRAGMA temp_store=MEMORY;
        PRAGMA cache_size=-200000;    -- ~200MB
        PRAGMA page_size=4096;
        PRAGMA mmap_size=268435456;   -- 256MB
    ";
            cmd.ExecuteNonQuery();

            if (isNewDb)
            {
                cmd.CommandText = "VACUUM;";  // ensure page_size takes effect
                cmd.ExecuteNonQuery();
            }
        }

        private static void FinalizeSqliteWriter(SqliteConnection conn)
        {
            using var cmd = conn.CreateCommand();
            cmd.CommandText = @"
        PRAGMA wal_checkpoint(TRUNCATE); -- flush, merge, and truncate WAL
        PRAGMA journal_mode=DELETE;      -- exit WAL; unlink -wal/-shm on close
        VACUUM;                          -- optional: compact
    ";
            cmd.ExecuteNonQuery();
        }

        private static void TryDeleteSidecars(string dbPath)
        {
            // Delete leftover sidecars if any handles are still around
            foreach (var path in new[] { dbPath + "-wal", dbPath + "-shm" })
            {
                try { if (File.Exists(path)) File.Delete(path); } catch { /* ignore */ }
            }
        }

        // Convert tile range to SQL WHERE clause
        private static string ToSqlWhere(VectorTileRange range)
        {
            long rowCount = (long)Math.Pow(2, range.Zoom) - 1;

            return $"{TilesTable.ZoomLevelColumnName}={range.Zoom} " +
                   $"AND {TilesTable.TileColColumnName} BETWEEN {range.MinColumn} AND {range.MaxColumn} " +
                   $"AND {TilesTable.TileRowColumnName} BETWEEN {rowCount - range.MaxRow} AND {rowCount - range.MinRow}";
        }

        // Compute tile range covering extent at given zoom level
        private VectorTileRange GetTileRange(int zoom, RectangleShape extent)
        {
            var world = MaxExtents.SphericalMercator;
            extent = world.GetIntersection(extent);

            long rowCount = 1L << zoom;
            double cellWidth = world.Width / rowCount;
            double cellHeight = world.Height / rowCount;

            long minX = (long)Math.Floor((extent.UpperLeftPoint.X - world.UpperLeftPoint.X) / cellWidth);
            long maxX = (long)Math.Floor((extent.UpperRightPoint.X - world.UpperLeftPoint.X) / cellWidth);
            long minY = (long)Math.Floor((world.UpperLeftPoint.Y - extent.UpperLeftPoint.Y) / cellHeight);
            long maxY = (long)Math.Floor((world.UpperLeftPoint.Y - extent.LowerLeftPoint.Y) / cellHeight);

            return new VectorTileRange(zoom, minX, minY, maxX, maxY);
        }

        // Helper: safely update the WPF progress bar (0–100)
        private void SetProgress(double percent)
        {
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => pbExtractProgress.Value = Math.Max(0, Math.Min(100, percent)));
            }
            else
            {
                pbExtractProgress.Value = Math.Max(0, Math.Min(100, percent));
            }
        }

        // Load MBTiles file onto map
        private async Task LoadFileOnMap(string filePath)
        {
            if (!File.Exists(filePath)) return;

            string ext = Path.GetExtension(filePath).ToLowerInvariant();
            if (ext != ".mbtiles")
            {
                MessageBox.Show($"Unsupported file type: {ext}", "Info",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _mvtLayer = new VectorMbTilesAsyncLayer(filePath);
            await _mvtLayer.OpenAsync();

            MapView.CurrentExtent = _mvtLayer.GetBoundingBox();
            MapView.Overlays.Clear();

            var overlay = new LayerOverlay();
            overlay.Layers.Add(_mvtLayer);
            MapView.Overlays.Add(overlay);

            await MapView.RefreshAsync();
        }

        // Build a safe metadata set from source (if any) + your bbox/maxZoom.
        private static List<(string Name, string Value)> BuildMetadata(
            IEnumerable<MetadataEntry> source,
            RectangleShape bbox3857,
            int maxZoom,
            string targetPath)
        {
            // Project bbox to WGS84
            var proj = new ProjectionConverter(3857, 4326);
            proj.Open();
            var wgs = proj.ConvertToExternalProjection(bbox3857);
            proj.Close();

            var center = wgs.GetCenterPoint();

            // Start from source (if any)
            var dict = (source ?? Enumerable.Empty<MetadataEntry>())
                .GroupBy(e => e.Name, StringComparer.OrdinalIgnoreCase)
                .ToDictionary(g => g.Key, g => g.Last().Value, StringComparer.OrdinalIgnoreCase);

            // Ensure core keys exist / are sane
            dict["name"] = dict.TryGetValue("name", out var n) ? n : Path.GetFileNameWithoutExtension(targetPath);
            dict["type"] = dict.TryGetValue("type", out var t) ? t : "baselayer";  // or "overlay"
            dict["format"] = dict.TryGetValue("format", out var f) ? f : "pbf";
            dict["minzoom"] = dict.TryGetValue("minzoom", out var mz) ? mz : "0";
            dict["maxzoom"] = maxZoom.ToString();
            dict["bounds"] = $"{wgs.MinX},{wgs.MinY},{wgs.MaxX},{wgs.MaxY}";
            dict["center"] = $"{center.X},{center.Y},{maxZoom}";

            return dict.Select(kv => (kv.Key, kv.Value)).ToList();
        }

        // Write metadata with UPSERT in its own short transaction.
        private static void UpsertMetadata(SqliteConnection conn, IEnumerable<(string Name, string Value)> entries)
        {
            using var tx = conn.BeginTransaction();
            using var cmd = conn.CreateCommand();

            cmd.CommandText = "CREATE UNIQUE INDEX IF NOT EXISTS metadata_name_idx ON metadata(name);";
            cmd.ExecuteNonQuery();

            cmd.Transaction = tx;
            cmd.CommandText =
                "INSERT INTO metadata(name, value) VALUES($n, $v) " +
                "ON CONFLICT(name) DO UPDATE SET value = excluded.value;";

            var pName = cmd.Parameters.Add("$n", SqliteType.Text);
            var pVal = cmd.Parameters.Add("$v", SqliteType.Text);

            foreach (var (name, value) in entries)
            {
                pName.Value = name;
                pVal.Value = value;
                cmd.ExecuteNonQuery();
            }
            tx.Commit();
        }

    }
}
