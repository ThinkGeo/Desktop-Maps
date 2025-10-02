using System;

namespace MBTilesExtractor
{
    public class TilesEntry
    {
        public long ZoomLevel { get; set; }
        public long TileColumn { get; set; }
        public long TileRow { get; set; }
        public byte[] TileData { get; set; } = Array.Empty<byte>();
    }
}