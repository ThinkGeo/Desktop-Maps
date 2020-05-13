using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Layers;

namespace WorldMapKitDataExtractor
{
    public class Table
    {
        public string TableName { get; set; }
        public Collection<SqliteColumn> Columns { get; set; }
    }
}
