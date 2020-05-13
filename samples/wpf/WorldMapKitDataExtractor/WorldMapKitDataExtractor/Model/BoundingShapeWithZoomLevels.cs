using ThinkGeo.MapSuite.Shapes;

namespace WorldMapKitDataExtractor
{
    public struct BoundingBoxWithZoomLevels
    {
        public RectangleShape BoundingBox { get; }
        
        public int StartingZoomLevel { get; }
        
        public int EndingZoomLevel { get; } 

        public BoundingBoxWithZoomLevels(RectangleShape boundingBox, int startingZoomLevel, int endingZoomLevel)
        {
            this.BoundingBox = boundingBox;
            this.StartingZoomLevel = startingZoomLevel;
            this.EndingZoomLevel = endingZoomLevel;
        }
    }
}
