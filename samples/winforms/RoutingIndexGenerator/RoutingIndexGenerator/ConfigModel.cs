using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RoutingIndexGenerator
{
    public class ConfigModel
    {
        private XDocument xdoc;
        private string configFilePath;

        private string fileType;

        private string tableName;
        private string featureIdColumn;
        private string geometryColumnName;
        private Collection<string> extractRequiredColumns;
        private int routingIndexType; // 0: fastest 1: shortest

        // speed option.
        private int speedUnit; // 0: KilometersPerHour 2: MilesPerHour

        private float defaultSpeed;
        private string speedColumnName;
        private Dictionary<string, float> roadSpeedForMPH;
        private Dictionary<string, float> roadSpeedForKPH;

        // oneway option.
        private string identifierColumnName;

        private string identifierColumnValue;
        private string indicatorColumnName;
        private string indicatorStartEndValue;
        private string indicatorEndStartValue;

        // closed road option.
        private string closedColumnName;

        private string closedColumnValue;

        public ConfigModel()
            : this("InitConfig.xml")
        { }

        public ConfigModel(string configFilePath)
            : this(XDocument.Load(configFilePath))
        {
            this.configFilePath = configFilePath;
        }

        public ConfigModel(XDocument xdoc)
        {
            this.xdoc = xdoc;
            roadSpeedForKPH = new Dictionary<string, float>();
            roadSpeedForMPH = new Dictionary<string, float>();
            extractRequiredColumns = new Collection<string>();
            fileType = "Shape File(*shp)| *.shp";
            speedUnit = 0;
            defaultSpeed = 50;
            routingIndexType = 1;

            if (xdoc != null)
            {
                fileType = GetElementValue("Name");
                tableName = GetAttributionValue("DatabaseProvider", "TableName");
                featureIdColumn = GetAttributionValue("DatabaseProvider", "FeatureIdColumn");
                geometryColumnName = GetAttributionValue("DatabaseProvider", "GeometryColumnName");
                string requredColumnsString = GetElementValue("RequiredColumns");
                extractRequiredColumns = new Collection<string>(requredColumnsString.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(col => col.Trim()).ToList());

                routingIndexType = int.Parse(GetElementValue("RoutingIndexType"));

                if (routingIndexType == 0)
                {
                    speedUnit = int.Parse(GetElementValue("SpeedUnit"));
                    defaultSpeed = float.Parse(GetElementValue("DefaultSpeed"));
                    speedColumnName = GetAttributionValue("SpeedColumn", "ColumnName");

                    identifierColumnName = GetAttributionValue("IdentifierColumn", "ColumnName");
                    identifierColumnValue = GetAttributionValue("IdentifierColumn", "ColumnValue");
                    indicatorColumnName = GetAttributionValue("IndicatorColumn", "ColumnName");
                    indicatorStartEndValue = GetAttributionValue("IndicatorColumn", "StartEndColumnValue");
                    indicatorEndStartValue = GetAttributionValue("IndicatorColumn", "EndStartColumnValue");

                    closedColumnName = GetAttributionValue("ClosedRoadColumn", "ColumnName");
                    closedColumnValue = GetAttributionValue("ClosedRoadColumn", "ColumnValue");

                    IEnumerable<XElement> items = xdoc.Descendants("RoadType").Descendants("Item");
                    foreach (XElement item in items)
                    {
                        roadSpeedForMPH.Add(item.Attribute("Name").Value, float.Parse(item.Attribute("Mph").Value));
                        roadSpeedForKPH.Add(item.Attribute("Name").Value, float.Parse(item.Attribute("Kph").Value));
                    }
                }
            }
        }

        public string ConfigFilePath
        {
            get { return configFilePath; }
            set { configFilePath = value; }
        }

        public string FileType { get { return fileType; } }

        public string TableName { get { return tableName; } }

        public string FeatureIdColumn { get { return featureIdColumn; } }

        public string GeometryColumnName { get { return geometryColumnName; } }

        public Collection<string> ExtractRequiredColumns { get { return extractRequiredColumns; } }

        public int RoutingIndexType { get { return routingIndexType; } }

        public int SpeedUnit { get { return speedUnit; } }

        public float DefaultSpeed { get { return defaultSpeed; } }

        public string SpeedColumnName { get { return speedColumnName; } }

        public Dictionary<string, float> RoadSpeedForMPH { get { return roadSpeedForMPH; } }

        public Dictionary<string, float> RoadSpeedForKPH { get { return roadSpeedForKPH; } }

        public string IdentifierColumnName { get { return identifierColumnName; } }

        public string IdentifierColumnValue { get { return identifierColumnValue; } }

        public string IndicatorColumnName { get { return indicatorColumnName; } }

        public string IndicatorStartEndValue { get { return indicatorStartEndValue; } }

        public string IndicatorEndStartValue { get { return indicatorEndStartValue; } }

        public string ClosedColumnName { get { return closedColumnName; } }

        public string ClosedColumnValue { get { return closedColumnValue; } }

        private string GetElementValue(string elementName)
        {
            if (xdoc == null)
            {
                xdoc = XDocument.Load(configFilePath);
            }

            XElement element = xdoc.Descendants(elementName).FirstOrDefault();

            return element == null ? string.Empty : element.Value;
        }

        private string GetAttributionValue(string elementName, string attributionName)
        {
            if (xdoc == null)
            {
                xdoc = XDocument.Load(configFilePath);
            }

            XElement element = xdoc.Descendants(elementName).FirstOrDefault();
            if (element == null)
            {
                return string.Empty;
            }
            else
            {
                return element.Attribute(attributionName).Value;
            }
        }
    }
}