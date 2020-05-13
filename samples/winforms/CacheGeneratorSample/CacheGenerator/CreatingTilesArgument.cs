using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;

namespace CacheGenerator
{
    class CreatingTilesArgument
    {
        private FileBitmapTileCache tileCache;
        private RectangleShape extent;
        private int bitmapWidth;
        private int bitmapHeight;

        public CreatingTilesArgument(FileBitmapTileCache tileCache, RectangleShape extent, int bitmapWidth, int bitmapHeight)
        {
            this.tileCache = tileCache;
            this.extent = extent;
            this.bitmapWidth = bitmapWidth;
            this.bitmapHeight = bitmapHeight;
        }

        public FileBitmapTileCache TileCache
        {
            get { return tileCache; }
            set { tileCache = value; }
        }

        public RectangleShape Extent
        {
            get { return extent; }
            set { extent = value; }
        }

        public int BitmapWidth
        {
            get { return bitmapWidth; }
            set { bitmapWidth = value; }
        }

        public int BitmapHeight
        {
            get { return bitmapHeight; }
            set { bitmapHeight = value; }
        }
    }
}
