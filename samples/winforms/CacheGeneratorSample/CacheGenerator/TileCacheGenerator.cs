using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Threading;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace CacheGenerator
{
    class TileCacheGenerator
    {
        private int tileWidth = 256;
        private int tileHeight = 256;
        // We have this maxTileCountInOneBitmap for optimization. we would rather draw a 4096*4096 bitmap once and split it to 256 tiles, 
        // than draw a 256*256 tile for 256 times. 
        private int maxTileCountInOneBitmap = 16;

        private int threadsCount;
        private short jpegQuality;
        private TileImageFormat tileImageFormat;
        Collection<Layer> layersToCache;
        GeographyUnit mapUnit;
        Collection<double> scales;
        RectangleShape cachingExtent;
        string cacheFolder;
        string restrictShapeFilePathName;
        Bitmap watermarkBitmap;
        ShapeFileFeatureLayer restrictionLayer;

        int currentTileIndex = 0;
        long totalTilesCount = 0;
        Collection<Thread> workingThreads;
        int workingThreadCount;

        public event EventHandler<CacheGeneratorProgressChangedEventArgs> ProgressChanged;
        public event EventHandler<EventArgs> GenerationCompleted;

        Collection<CreatingCellsArgument> cellsArguments = new Collection<CreatingCellsArgument>();

        public TileCacheGenerator()
        {
            ThreadsCount = 4;
        }

        public int ThreadsCount
        {
            get { return threadsCount; }
            set { threadsCount = value; }
        }

        public int TileWidth
        {
            get { return tileWidth; }
            set { tileWidth = value; }
        }

        public int TileHeight
        {
            get { return tileHeight; }
            set { tileHeight = value; }
        }

        public TileImageFormat TileImageFormat
        {
            get { return tileImageFormat; }
            set { tileImageFormat = value; }
        }

        public short JpegQuality
        {
            get { return jpegQuality; }
            set { jpegQuality = value; }
        }

        public Collection<Layer> LayersToCache
        {
            get { return layersToCache; }
            set { layersToCache = value; }
        }

        public GeographyUnit MapUnit
        {
            get { return mapUnit; }
            set { mapUnit = value; }
        }

        public Collection<double> ScalesToCache
        {
            get { return scales; }
            set { scales = value; }
        }

        public RectangleShape CachingExtent
        {
            get { return cachingExtent; }
            set { cachingExtent = value; }
        }

        public string CacheFolder
        {
            get { return cacheFolder; }
            set { cacheFolder = value; }
        }

        public string RestrictShapeFilePathName
        {
            get { return restrictShapeFilePathName; }
            set { restrictShapeFilePathName = value; }
        }

        public Bitmap WatermarkBitmap
        {
            get { return watermarkBitmap; }
            set { watermarkBitmap = value; }
        }

        public void GenerateTiles()
        {
            if (!string.IsNullOrEmpty(restrictShapeFilePathName))
            {
                restrictionLayer = new ShapeFileFeatureLayer(restrictShapeFilePathName, GeoFileReadWriteMode.Read);
                restrictionLayer.Open();
            }

            currentTileIndex = 0;
            totalTilesCount = GetTilesCount();

            workingThreads = new Collection<Thread>();
            Thread thread = new Thread(new ParameterizedThreadStart(GenerateTiles));
            thread.Start(scales);
            workingThreads.Add(thread);
        }

        private void GenerateTiles(object scales)
        {
            workingThreadCount = this.ThreadsCount;

            Collection<double> doubleScales = (Collection<double>)scales;
            foreach (double scale in doubleScales)
            {
                GenerateTilesForOneScale(scale);
            }

            ManualResetEvent manualResetEvent = new ManualResetEvent(false);
            for (int i = 0; i < this.ThreadsCount; i++)
            {
                Thread thread = new Thread(new ParameterizedThreadStart(CreateTiles));
                thread.Start(manualResetEvent);
                workingThreads.Add(thread);
            }

            manualResetEvent.WaitOne();

            foreach (Layer layer in layersToCache)
            {
                if (layer.IsOpen)
                {
                    layer.Close();
                }
            }

            if (restrictionLayer != null && restrictionLayer.IsOpen)
            {
                restrictionLayer.Close();
            }

            OnGenerationCompleted(new EventArgs());
        }

        private void GenerateTilesForOneScale(object scale)
        {
            MapSuiteTileMatrix tileMatrix = new MapSuiteTileMatrix((double)scale, tileWidth, tileHeight, mapUnit);
            tileMatrix.BoundingBoxUnit = mapUnit;
            FileBitmapTileCache tileCache = new FileBitmapTileCache(cacheFolder, string.Empty, TileImageFormat, tileMatrix);
            tileCache.JpegQuality = JpegQuality;

            RectangleShape matrixBoundingBox = tileCache.TileMatrix.BoundingBox;

            RowColumnRange rowColumnRange = tileCache.TileMatrix.GetIntersectingRowColumnRange(matrixBoundingBox);
            int deltaColumnIndex = (int)(rowColumnRange.MaxColumnIndex - rowColumnRange.MinColumnIndex);
            int deltaRowIndex = (int)(rowColumnRange.MaxRowIndex - rowColumnRange.MinRowIndex);
            int deltaIndex = Math.Max(deltaColumnIndex, deltaRowIndex);

            double cellWidth = tileCache.TileMatrix.CellWidth;
            double cellHeight = tileCache.TileMatrix.CellHeight;

            if (deltaIndex < maxTileCountInOneBitmap)
            {
                int bitMapWidth = tileWidth * (deltaColumnIndex + 1);
                int bitMapHeight = tileHeight * (deltaRowIndex + 1);

                CreateTiles(tileCache, matrixBoundingBox, bitMapWidth, bitMapHeight, watermarkBitmap, restrictionLayer);
            }
            else
            {

                CreatingCellsArgument cellArgument = new CreatingCellsArgument();

                cellArgument.CellWidth = cellWidth * maxTileCountInOneBitmap;
                cellArgument.CellHeight = cellHeight * maxTileCountInOneBitmap;

                int startColumnIndex = (int)((cachingExtent.UpperLeftPoint.X - matrixBoundingBox.UpperLeftPoint.X) / cellArgument.CellWidth);
                int startRowIndex = (int)((matrixBoundingBox.UpperLeftPoint.Y - cachingExtent.UpperLeftPoint.Y) / cellArgument.CellHeight);

                cellArgument.StartRowIndex = startRowIndex;
                cellArgument.StartColumnIndex = startColumnIndex;

                int endColumnIndex = (int)Math.Ceiling((cachingExtent.UpperRightPoint.X - matrixBoundingBox.UpperLeftPoint.X) / cellArgument.CellWidth);
                int endRowIndex = (int)Math.Ceiling((matrixBoundingBox.UpperLeftPoint.Y - cachingExtent.LowerLeftPoint.Y) / cellArgument.CellHeight);

                cellArgument.ColumnCount = endColumnIndex - startColumnIndex;
                cellArgument.RowCount = endRowIndex - startRowIndex;

                cellArgument.TileCache = tileCache;
                cellsArguments.Add(cellArgument);
            }
        }

        private void CreateTiles(object manualResetEvent)
        {
            ManualResetEvent resetEvent = (ManualResetEvent)manualResetEvent;

            Bitmap clonedWatermarkBitmap = GetWatermarkBitmap();
            ShapeFileFeatureLayer clonedRestrictedLayer = GetRestrictionLayer();

            while (true)
            {
                CreatingTilesArgument currentArgument = GetTilesCreatingArgument();
                if (currentArgument != null)
                {
                    CreateTiles(currentArgument.TileCache, currentArgument.Extent, currentArgument.BitmapWidth, currentArgument.BitmapHeight, clonedWatermarkBitmap, clonedRestrictedLayer);
                }
                if (cellsArguments.Count == 0)
                {
                    break;
                }
            }
            if (clonedRestrictedLayer != null)
            {
                clonedRestrictedLayer.Close();
            }

            if (Interlocked.Decrement(ref workingThreadCount) == 0)
            {
                resetEvent.Set();
            }
        }

        private CreatingTilesArgument GetTilesCreatingArgument()
        {
            CreatingTilesArgument tilesCreatingArgument = null;
            lock (cellsArguments)
            {
                if (cellsArguments.Count == 0)
                {
                    return null;
                }

                CreatingCellsArgument tileArgument = cellsArguments[0];

                if (tileArgument.CurrentRowIndex == tileArgument.RowCount && tileArgument.CurrentColumnIndex == tileArgument.ColumnCount)
                {
                    cellsArguments.RemoveAt(0);
                    tilesCreatingArgument = GetTilesCreatingArgument();
                }
                else
                {
                    RectangleShape totalExtent = tileArgument.GetCurrentCellExtent();

                    // This is a workaround of an issue or RectangleShape. 
                    MultipolygonShape intersection = ((AreaBaseShape)cachingExtent).GetIntersection(totalExtent);

                    if (intersection != null)
                    {
                        tilesCreatingArgument = new CreatingTilesArgument(tileArgument.TileCache, totalExtent, maxTileCountInOneBitmap * tileWidth, maxTileCountInOneBitmap * tileHeight);
                    }
                    tileArgument.CurrentColumnIndex++;
                }


                if (tileArgument.CurrentColumnIndex == tileArgument.ColumnCount
                    && tileArgument.CurrentRowIndex != tileArgument.RowCount)
                {
                    tileArgument.CurrentRowIndex++;
                    tileArgument.CurrentColumnIndex = 0;
                }
            }
            return tilesCreatingArgument;
        }

        private void CreateTiles(FileBitmapTileCache tileCache, RectangleShape extent, int bitmapWidth, int bitmapHeight, Bitmap watermarkBitmap, ShapeFileFeatureLayer restrictionLayer)
        {
            if (restrictionLayer == null || restrictionLayer.QueryTools.GetFeaturesIntersecting(extent, ReturningColumnsType.NoColumns).Count > 0)
            {
                Bitmap bitmap = new Bitmap(bitmapWidth, bitmapHeight);
                DrawOnBitmap(LayersToCache, mapUnit, extent, bitmap);
                SaveBitmapToTiles(tileCache, bitmap, extent, cachingExtent, watermarkBitmap, restrictionLayer, tileWidth, tileHeight);
                bitmap.Dispose();
            }
        }

        public void DrawOnBitmap(IEnumerable<Layer> layersTmp, GeographyUnit mapUnit, RectangleShape extent, Bitmap bitmap)
        {
            GeoCanvas canvas = new PlatformGeoCanvas();
            canvas.BeginDrawing(bitmap, extent, mapUnit);
            Collection<Layer> layers = LayerProvider.GetLayersToCache();
            foreach (Layer layer in layers)
            {
                if (!layer.IsOpen)
                {
                    layer.Open();
                }
                layer.Draw(canvas, new Collection<SimpleCandidate>());
            }
            canvas.EndDrawing();
        }

        private void SaveBitmapToTiles(FileBitmapTileCache tileCache, Bitmap bitmap, RectangleShape bitmapExtent, RectangleShape cachingExtent, Bitmap watermarkBitmap, ShapeFileFeatureLayer restrictionLayer, int tileWidth, int tileHeight)
        {
            MultipolygonShape resultShapes = ((AreaBaseShape)cachingExtent).GetIntersection(bitmapExtent);

            if (resultShapes != null)
            {
                RectangleShape cachingExtentInBitmapExtent = resultShapes.Polygons[0].GetBoundingBox();
                Collection<TileMatrixCell> cells = tileCache.TileMatrix.GetIntersectingCells(cachingExtentInBitmapExtent);

                foreach (TileMatrixCell cell in cells)
                {
                    if (restrictionLayer == null || restrictionLayer.QueryTools.GetFeaturesIntersecting(cell.BoundingBox, ReturningColumnsType.NoColumns).Count > 0)
                    {
                        // If the tile exists, do nothing.  
                        String tileImageFileName = tileCache.GetTileImageFileName(cell.Row, cell.Column);
                        bool isTileExisting = File.Exists(tileImageFileName);
                        if (!isTileExisting)
                        {
                            Bitmap cellBitmap = GetCellFromBitmap(bitmap, bitmapExtent, cell.BoundingBox, tileWidth, tileHeight);

                            BitmapTile savingBitmapTile = new BitmapTile(new GeoImage(cellBitmap), cell.BoundingBox, tileCache.TileMatrix.Scale);

                            if (watermarkBitmap != null && savingBitmapTile != null)
                            {
                                using (Graphics graphics = Graphics.FromImage((Image)savingBitmapTile.Bitmap.NativeImage))
                                {
                                    graphics.DrawImageUnscaled(watermarkBitmap, 0, 0);
                                }
                            }

                            tileCache.SaveTile(savingBitmapTile);
                            cellBitmap.Dispose();
                            savingBitmapTile.Dispose();
                        }
                        lock (this)
                        {
                            currentTileIndex++;
                            int percentage = (int)(currentTileIndex * 100 / totalTilesCount);
                            if (percentage > 100)
                            {
                                percentage = 100;
                            }
                            OnProgressChanged(new CacheGeneratorProgressChangedEventArgs(percentage, null, currentTileIndex, totalTilesCount));
                        }
                    }
                }
            }
        }

        public Bitmap GetCellFromBitmap(Bitmap sourceBitmap, RectangleShape sourceExtent, RectangleShape cellExtent, int tileWidth, int tileHeight)
        {
            Bitmap cellBitmap = null;

            if (sourceBitmap != null)
            {
                Graphics graphics = null;

                try
                {
                    ScreenPointF upperLeftPoint = ExtentHelper.ToScreenCoordinate(sourceExtent, cellExtent.UpperLeftPoint, Convert.ToSingle(sourceBitmap.Width), Convert.ToSingle(sourceBitmap.Height));
                    cellBitmap = new Bitmap(tileWidth, tileHeight);

                    graphics = Graphics.FromImage(cellBitmap);
                    graphics.DrawImageUnscaled(sourceBitmap, -(int)Math.Round(upperLeftPoint.X), -(int)Math.Round(upperLeftPoint.Y));
                }
                finally
                {
                    if (graphics != null) { graphics.Dispose(); }
                }
            }

            return cellBitmap;
        }

        public long GetTilesCount()
        {
            long tilesCount = 0;
            Collection<long> cells = GetTilesCountsForScales();
            foreach (long cellCount in cells)
            {
                tilesCount += cellCount;
            }
            return tilesCount;
        }

        public Collection<long> GetTilesCountsForScales()
        {
            if (!string.IsNullOrEmpty(restrictShapeFilePathName))
            {
                restrictionLayer = new ShapeFileFeatureLayer(restrictShapeFilePathName, GeoFileReadWriteMode.Read);
                restrictionLayer.Open();
            }

            Collection<long> cells = new Collection<long>();
            foreach (double scale in scales)
            {
                MapSuiteTileMatrix tileMatrix = new MapSuiteTileMatrix(scale, tileWidth, tileHeight, mapUnit);
                RowColumnRange rowColumnRange = tileMatrix.GetIntersectingRowColumnRange(cachingExtent);
                long deltaColumn = rowColumnRange.MaxColumnIndex - rowColumnRange.MinColumnIndex + 1;
                long deltaRow = rowColumnRange.MaxRowIndex - rowColumnRange.MinRowIndex + 1;

                long cellsCountUnderScale = deltaColumn * deltaRow;
                if (restrictionLayer != null)
                {
                    Collection<Feature> features = restrictionLayer.QueryTools.GetFeaturesInsideBoundingBox(cachingExtent, ReturningColumnsType.NoColumns);
                    double totalArea = cachingExtent.Width * cachingExtent.Height;
                    double area = 0;
                    foreach (Feature feature in features)
                    {
                        // Use the same unit here (Meter and SquarMeter) to just make the calculation easy.
                        area += ((AreaBaseShape)feature.GetShape()).GetIntersection(cachingExtent).GetArea(GeographyUnit.Meter, AreaUnit.SquareMeters);
                    }
                    cellsCountUnderScale = (long)Math.Ceiling(cellsCountUnderScale * area / totalArea);
                }
                cells.Add(cellsCountUnderScale);
            }
            return cells;
        }

        public void Close()
        {
            if (workingThreads != null)
            {
                foreach (Thread workingThread in workingThreads)
                {
                    if (workingThread != null && workingThread.IsAlive)
                    {
                        workingThread.Abort();
                    }

                }
            }
            if (layersToCache != null)
            {
                foreach (Layer layer in layersToCache)
                {
                    if (layer.IsOpen)
                    {
                        layer.Close();
                    }
                }
            }

            if (restrictionLayer != null && restrictionLayer.IsOpen)
            {
                restrictionLayer.Close();
            }
        }

        protected virtual void OnProgressChanged(CacheGeneratorProgressChangedEventArgs e)
        {
            EventHandler<CacheGeneratorProgressChangedEventArgs> handler = ProgressChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnGenerationCompleted(EventArgs e)
        {
            EventHandler<EventArgs> handler = GenerationCompleted;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private Bitmap GetWatermarkBitmap()
        {
            if (watermarkBitmap == null)
            {
                return null;
            }

            Bitmap clonedWatermarkBitmap = null;
            lock (watermarkBitmap)
            {
                clonedWatermarkBitmap = (Bitmap)watermarkBitmap.Clone();
            }
            return clonedWatermarkBitmap;
        }

        private ShapeFileFeatureLayer GetRestrictionLayer()
        {
            if (restrictionLayer == null)
            {
                return null;
            }

            ShapeFileFeatureLayer clonedRestrictionLayer = new ShapeFileFeatureLayer(restrictionLayer.ShapePathFilename, GeoFileReadWriteMode.Read);
            clonedRestrictionLayer.Open();

            return clonedRestrictionLayer;
        }
    }
}