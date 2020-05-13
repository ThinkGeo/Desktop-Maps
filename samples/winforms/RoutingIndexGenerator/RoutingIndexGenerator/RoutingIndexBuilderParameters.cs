using System.IO;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Routing;
using ThinkGeo.MapSuite.Shapes;

namespace RoutingIndexGenerator
{
    public class RoutingIndexBuilderParameters
    {
        private BuildRoutingDataMode buildRoutingDataMode;
        private GeographyUnit geographyUnit;
        private OneWayRoadOption oneWayRoadOption;
        private ClosedRoadOption closedRoadOption;
        private RoadSpeedOption speedOption;
        private RoutingIndexType routeIndexType;
        private RectangleShape restrictExtent;
        private RoutableFileType routableFileType;
        private string routableFilePathName;
        private string rtgFilePathName;
        private string sourceSqlitePathName;
        private bool includeOnewayOptions;
        private bool overrideRoutableFile;

        public RoutingIndexBuilderParameters()
        {
            RouteIndexType = RoutingIndexType.Fastest;
            GeographyUnit = GeographyUnit.Meter;
            BuildRoutingDataMode = BuildRoutingDataMode.Rebuild;

            OneWayRoadOption = new OneWayRoadOption();
            closedRoadOption = new ClosedRoadOption();
            SpeedOption = new RoadSpeedOption();
            overrideRoutableFile = true;
        }

        public RectangleShape RestrictExtent
        {
            get { return restrictExtent; }
            set { restrictExtent = value; }
        }

        public BuildRoutingDataMode BuildRoutingDataMode
        {
            get { return buildRoutingDataMode; }
            set { buildRoutingDataMode = value; }
        }

        public GeographyUnit GeographyUnit
        {
            get { return geographyUnit; }
            set { geographyUnit = value; }
        }

        public OneWayRoadOption OneWayRoadOption
        {
            get { return oneWayRoadOption; }
            set { oneWayRoadOption = value; }
        }

        public RoadSpeedOption SpeedOption
        {
            get { return speedOption; }
            set { speedOption = value; }
        }

        public ClosedRoadOption ClosedRoadOption
        {
            get { return closedRoadOption; }
            set { closedRoadOption = value; }
        }

        public RoutingIndexType RouteIndexType
        {
            get { return routeIndexType; }
            set { routeIndexType = value; }
        }

        public string RtgFilePathName
        {
            get { return rtgFilePathName; }
            set { rtgFilePathName = value; }
        }

        public RoutableFileType RoutableFileType
        {
            get { return routableFileType; }
            set { routableFileType = value; }
        }

        public string RoutableFilePathName
        {
            get { return routableFilePathName; }
            set { routableFilePathName = value; }
        }

        public string SourceSqlitePathName
        {
            get { return sourceSqlitePathName; }
            set { sourceSqlitePathName = value; }
        }

        public string SourceSegmentsFilePath
        {
            get
            {
                if (routableFileType==RoutableFileType.ShapeFile)
                    return Path.ChangeExtension(sourceSqlitePathName, ".source.shp");
                else
                    return Path.ChangeExtension(sourceSqlitePathName, ".source.sqlite");
            }
        }

        public bool IncludeOnewayOptions
        {
            get { return includeOnewayOptions; }
            set { includeOnewayOptions = value; }
        }

        public bool OverrideRoutableFile
        {
            get { return overrideRoutableFile; }
            set { overrideRoutableFile = value; }
        }
    }
}