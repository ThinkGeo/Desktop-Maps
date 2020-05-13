using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;

namespace CacheGenerator
{
    class CreatingCellsArgument
    {
        private FileBitmapTileCache tileCache;
        private long currentRowIndex = 0;
        private long currentColumnIndex = 0;
        private long rowCount;
        private long columnCount;
        private double cellWidth;
        private double cellHeight;
        private double startRowIndex;
        private double startColumnIndex;

        public CreatingCellsArgument()
        { }

        public FileBitmapTileCache TileCache
        {
            get { return tileCache; }
            set { tileCache = value; }
        }
        public long CurrentRowIndex
        {
            get { return currentRowIndex; }
            set { currentRowIndex = value; }
        }
        public long CurrentColumnIndex
        {
            get { return currentColumnIndex; }
            set { currentColumnIndex = value; }
        }
        public long RowCount
        {
            get { return rowCount; }
            set { rowCount = value; }
        }
        public long ColumnCount
        {
            get { return columnCount; }
            set { columnCount = value; }
        }
        public double CellWidth
        {
            get { return cellWidth; }
            set { cellWidth = value; }
        }
        public double CellHeight
        {
            get { return cellHeight; }
            set { cellHeight = value; }
        }
        public double StartRowIndex
        {
            get { return startRowIndex; }
            set { startRowIndex = value; }
        }
        public double StartColumnIndex
        {
            get { return startColumnIndex; }
            set { startColumnIndex = value; }
        }

        public RectangleShape GetCurrentCellExtent()
        {
            RectangleShape boundingBox = TileCache.TileMatrix.BoundingBox;
            PointShape upperLeftPoint = new PointShape(boundingBox.UpperLeftPoint.X + CurrentColumnIndex * CellWidth + StartColumnIndex * CellWidth, boundingBox.UpperLeftPoint.Y - CurrentRowIndex * CellHeight - StartRowIndex * CellHeight);
            PointShape lowerRightPoint = new PointShape(boundingBox.UpperLeftPoint.X + (CurrentColumnIndex + 1) * CellWidth + StartColumnIndex * CellWidth, boundingBox.UpperLeftPoint.Y - (CurrentRowIndex + 1) * CellHeight - StartRowIndex * CellHeight);
            RectangleShape totalExtent = new RectangleShape(upperLeftPoint, lowerRightPoint);
            return totalExtent;
        }
    }
}
