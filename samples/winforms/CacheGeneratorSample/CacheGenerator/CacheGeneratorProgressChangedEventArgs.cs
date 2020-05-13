using System.ComponentModel;

namespace CacheGenerator
{
    class CacheGeneratorProgressChangedEventArgs : ProgressChangedEventArgs
    {
        private int currentTileIndex;
        private long totalTileCount;

        public int CurrentTileIndex
        {
            get { return currentTileIndex; }
            set { currentTileIndex = value; }
        }

        public long TotalTilesCount
        {
            get { return totalTileCount; }
            set { totalTileCount = value; }
        }

        private CacheGeneratorProgressChangedEventArgs()
            : this(0, null, 0, 0)
        { }

        public CacheGeneratorProgressChangedEventArgs(int progressPercentage, object userState, int currentTileIndex, long totalTilesCount)
            : base(progressPercentage, userState)
        {
            this.currentTileIndex = currentTileIndex;
            this.totalTileCount = totalTilesCount;
        }
    }
}
