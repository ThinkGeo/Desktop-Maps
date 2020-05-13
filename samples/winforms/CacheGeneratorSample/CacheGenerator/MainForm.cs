using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;

namespace CacheGenerator
{
    public partial class MainForm : Form
    {
        delegate void InvokeMethod();
        DateTime startTime;
        TileCacheGenerator tileCacheGenerator;
        GeographyUnit mapUnit = GeographyUnit.DecimalDegree;

        public MainForm()
        {
            InitializeComponent();
        }

        private void CachedImagesGenerateroForm_Load(object sender, EventArgs e)
        {
            cmbStartZoomLevel.Text = "ZoomLevel01";
            cmbEndZoomLevel.Text = "ZoomLevel07";
            cmbTileImageType.Text = "PNG";
        }

        private Collection<double> GetScalesToCache()
        {
            Collection<double> scales = LayerProvider.GetScalesToCache();

            if (cmbStartZoomLevel.SelectedIndex > cmbEndZoomLevel.SelectedIndex)
            {
                cmbStartZoomLevel.SelectedIndex = cmbEndZoomLevel.SelectedIndex;
            }
            Collection<double> scalesToCache = new Collection<double>();
            for (int i = cmbStartZoomLevel.SelectedIndex; i <= cmbEndZoomLevel.SelectedIndex; i++)
            {
                scalesToCache.Add(scales[i]);
            }

            return scalesToCache;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            btnGenerate.Enabled = false;

            tileCacheGenerator = new TileCacheGenerator();
            tileCacheGenerator.ProgressChanged += new EventHandler<CacheGeneratorProgressChangedEventArgs>(tileCacheGenerator_ProgressChanged);
            tileCacheGenerator.GenerationCompleted += new EventHandler<EventArgs>(tileCacheGenerator_GenerationCompleted);

            if (cmbTileImageType.Text.ToUpperInvariant() == "PNG")
            {
                tileCacheGenerator.TileImageFormat = TileImageFormat.Png;
            }
            else if (cmbTileImageType.Text.ToUpperInvariant() == "JPEG")
            {
                tileCacheGenerator.TileImageFormat = TileImageFormat.Jpeg;
                tileCacheGenerator.JpegQuality = Int16.Parse(txtJpegQuality.Text);
            }

            tileCacheGenerator.ScalesToCache = GetScalesToCache();
            tileCacheGenerator.LayersToCache = LayerProvider.GetLayersToCache();

            string[] upperLeftPointInString = txtUpperLeftX.Text.Split(',');
            string[] lowerRightPointInString = txtLowerRightX.Text.Split(',');
            tileCacheGenerator.CachingExtent = new RectangleShape(double.Parse(upperLeftPointInString[0]), double.Parse(upperLeftPointInString[1]), double.Parse(lowerRightPointInString[0]), double.Parse(lowerRightPointInString[1]));

            if (checkBoxWatermark.Checked)
            {
                tileCacheGenerator.WatermarkBitmap = new Bitmap(txtWatermarkPath.Text);
            }

            if (checkBoxRestrict.Checked)
            {
                tileCacheGenerator.RestrictShapeFilePathName = txtRestrictionLayerPath.Text;

                int gridSize = Convert.ToInt32(txtGridSize.Text);
                if (gridSize != 0)
                {
                    GridRestrictionLayer(txtRestrictionLayerPath.Text, tileCacheGenerator.CachingExtent, gridSize, txtRestrictionLayerPath.Text.Replace(".shp", "_Grid.shp"));
                    tileCacheGenerator.RestrictShapeFilePathName = txtRestrictionLayerPath.Text.Replace(".shp", "_Grid.shp");
                }
            }

            tileCacheGenerator.MapUnit = mapUnit;
            tileCacheGenerator.CacheFolder = txtCacheFolder.Text;
            tileCacheGenerator.ThreadsCount = Convert.ToInt32(txtThreadsCount.Text);

            startTime = System.DateTime.Now;
            tileCacheGenerator.GenerateTiles();
        }

        private void GridRestrictionLayer(string restrictShapePathFileName, RectangleShape extentToGrid, int gridSize, string gridedRestrictShapePathFileName)
        {
            ShapeFileFeatureSource.BuildIndexFile(restrictShapePathFileName, BuildIndexMode.DoNotRebuild);

            ShapeFileFeatureLayer restrictionLayer = new ShapeFileFeatureLayer(restrictShapePathFileName);
            restrictionLayer.Open();

            if (restrictionLayer.GetShapeFileType() == ShapeFileType.Polygon)
            {
                ShapeFileFeatureSource.CreateShapeFile(ShapeFileType.Polygon, gridedRestrictShapePathFileName, new Collection<DbfColumn>() { new DbfColumn("RECID", DbfColumnType.Numeric, 10, 0) }, Encoding.Default, OverwriteMode.Overwrite);

                ShapeFileFeatureSource gridedRestrictionLayer = new ShapeFileFeatureSource(gridedRestrictShapePathFileName, GeoFileReadWriteMode.ReadWrite);
                gridedRestrictionLayer.Open();
                gridedRestrictionLayer.BeginTransaction();

                double startX = extentToGrid.UpperLeftPoint.X;
                double startY = extentToGrid.UpperLeftPoint.Y;

                double cellWidth = extentToGrid.Width / gridSize;
                double cellHeight = extentToGrid.Height / gridSize;

                for (int x = 0; x < gridSize; x++)
                {
                    for (int y = 0; y < gridSize; y++)
                    {
                        RectangleShape boundingBox = new RectangleShape(startX + x * cellWidth, startY - y * cellHeight, startX + x * cellWidth + cellWidth, startY - y * cellHeight - cellHeight);
                        Collection<Feature> fearures = restrictionLayer.QueryTools.GetFeaturesInsideBoundingBox(boundingBox, new Collection<string>() { "RECID" });

                        foreach (Feature feature in fearures)
                        {
                            MultipolygonShape polygon = (MultipolygonShape)feature.GetShape();
                            BaseShape areaIntersection = polygon.GetIntersection(boundingBox);

                            if (areaIntersection != null)
                            {
                                Feature newFeature = new Feature(areaIntersection);
                                newFeature.ColumnValues.Add("RECID", feature.ColumnValues["RECID"].ToString());
                                gridedRestrictionLayer.AddFeature(newFeature);
                            }

                            lblTotalTime.Text = string.Format("Griding the restrict file: Row {0} of {1}, Column {2} of {3}", x, gridSize, y, gridSize);
                            lblCurrentImageCount.Text = string.Empty;
                            Application.DoEvents();
                        }
                    }
                }
                gridedRestrictionLayer.CommitTransaction();
                gridedRestrictionLayer.Close();
            }
        }

        void tileCacheGenerator_ProgressChanged(object sender, CacheGeneratorProgressChangedEventArgs e)
        {
            // Make the label update every 100 tiles. 
            if (e.CurrentTileIndex % 100 != 0 && e.CurrentTileIndex < e.TotalTilesCount)
            {
                return;
            }
            TimeSpan timeSpan = DateTime.Now - startTime;

            lblCurrentImageCount.Invoke((InvokeMethod)delegate()
            {
                lblCurrentImageCount.Text = string.Format("Current Progress: {0} %", e.ProgressPercentage);
            });

            lblTileStatus.Invoke((InvokeMethod)delegate
            {
                lblTileStatus.Text = String.Format("{0:N0} Tiles Generated. ", e.CurrentTileIndex);
            });

            double totalMilliseconds = timeSpan.TotalMilliseconds;
            double averageMilliseconds = totalMilliseconds / e.CurrentTileIndex;
            double remainingTime = (e.TotalTilesCount - e.CurrentTileIndex) * averageMilliseconds;
            if (remainingTime < 0)
            {
                remainingTime = 0;
            }

            lblTotalTime.Invoke((InvokeMethod)delegate()
            {
                lblTotalTime.Text = String.Format("TotalTime: {0}; RemainingTime: {1}; AverageTime: {2} ms/Tile", FormattedTimeString(timeSpan), FormattedTimeString(TimeSpan.FromMilliseconds(remainingTime)), averageMilliseconds.ToString("N1"));
            });
        }

        void tileCacheGenerator_GenerationCompleted(object sender, EventArgs e)
        {
            btnGenerate.Invoke((InvokeMethod)delegate()
            {
                btnGenerate.Enabled = true;
            });

            lblCurrentImageCount.Invoke((InvokeMethod)delegate()
            {
                lblCurrentImageCount.Text = "Current Progress: 100%";
            });

            MessageBox.Show("Generation Completed!");
        }

        public static string FormattedTimeString(TimeSpan ts)
        {
            if (ts.Days > 0)
            {
                return String.Format("{0} Days {1} Hours", ts.Days.ToString(), ts.Hours.ToString());
            }
            else if (ts.Hours > 0)
            {
                return String.Format("{0} Hours {1} Minutes", ts.Hours.ToString(), ts.Minutes.ToString());
            }
            else if (ts.Minutes > 0)
            {
                return String.Format("{0} Minutes {1} Seconds", ts.Minutes.ToString(), ts.Seconds.ToString());
            }
            else
            {
                return String.Format("{0} Seconds", ts.Seconds.ToString());
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            TileCacheGenerator tileCacheGenerator = new TileCacheGenerator();
            tileCacheGenerator.MapUnit = mapUnit;
            tileCacheGenerator.ScalesToCache = GetScalesToCache();

            string[] upperLeftPointInString = txtUpperLeftX.Text.Split(',');
            string[] lowerRightPointInString = txtLowerRightX.Text.Split(',');
            tileCacheGenerator.CachingExtent = new RectangleShape(double.Parse(upperLeftPointInString[0]), double.Parse(upperLeftPointInString[1]), double.Parse(lowerRightPointInString[0]), double.Parse(lowerRightPointInString[1]));

            if (checkBoxRestrict.Checked)
            {
                tileCacheGenerator.RestrictShapeFilePathName = txtRestrictionLayerPath.Text;
            }

            Collection<long> tilesCount = tileCacheGenerator.GetTilesCountsForScales();
            long totalTilesCount = 0;

            int startIndex = cmbStartZoomLevel.SelectedIndex;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine();
            sb.AppendLine();

            for (int i = 0; i < tilesCount.Count; i++)
            {
                totalTilesCount += tilesCount[i];
                sb.AppendFormat("Tiles Count In {0}: {1:N0}", cmbStartZoomLevel.Items[startIndex++], tilesCount[i]);
                sb.AppendLine();
            }

            sb.Insert(0, string.Format("Total Tiles Count: {0:N0}", totalTilesCount));
            tileCacheGenerator.Close();

            MessageBox.Show(sb.ToString());
        }

        private void btnSelectCacheFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtCacheFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void btnSelectWatermark_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Application.StartupPath;
            openFileDialog.Filter = "Image File(*.jpg,*.gif,*.bmp,*.png)|*.jpg;*.gif;*.bmp;*.png|All File(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtWatermarkPath.Text = openFileDialog.FileName;
            }
        }

        private void btnSelectRestrictionLayer_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Application.StartupPath;
            openFileDialog.Filter = "Shape File(*.shp)|*.shp|All File(*.*)|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtRestrictionLayerPath.Text = openFileDialog.FileName;
            }
        }

        private void cmbTileImageType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtJpegQuality.Enabled = (cmbTileImageType.Text == "JPEG");
            lblJpegQuality.Enabled = txtJpegQuality.Enabled;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tileCacheGenerator != null)
            {
                tileCacheGenerator.Close();
            }
        }

        private void checkBoxRestrict_CheckedChanged(object sender, EventArgs e)
        {
            txtRestrictionLayerPath.ReadOnly = !checkBoxRestrict.Checked;
            txtGridSize.ReadOnly = !checkBoxRestrict.Checked;
        }

        private void checkBoxWatermark_CheckedChanged(object sender, EventArgs e)
        {
            txtWatermarkPath.ReadOnly = !checkBoxWatermark.Checked;
        }
    }
}
