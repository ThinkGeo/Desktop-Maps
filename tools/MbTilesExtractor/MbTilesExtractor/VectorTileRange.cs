namespace MBTilesExtractor
{
    class VectorTileRange
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
