using System.Collections.Generic;

namespace RoutingIndexGenerator
{
    public class RoadSpeedOption
    {
        private SpeedUnit speedUnit;
        private float defaultSpeed;
        private RoadSpeedSourceType roadSpeedSourceType;
        private string roadSpeedColumnName;
        private string roadTypeColumnName;
        private Dictionary<string, double> roadSpeeds;

        public RoadSpeedOption()
        {
            DefaultSpeed = 50;
            SpeedUnit = SpeedUnit.KilometersPerHour;
        }

        public SpeedUnit SpeedUnit
        {
            get { return speedUnit; }
            set { speedUnit = value; }
        }

        public float DefaultSpeed
        {
            get { return defaultSpeed; }
            set { defaultSpeed = value; }
        }

        public RoadSpeedSourceType SpeedSourceType
        {
            get { return roadSpeedSourceType; }
            set { roadSpeedSourceType = value; }
        }

        public string SpeedColumnName
        {
            get { return roadSpeedColumnName; }
            set { roadSpeedColumnName = value; }
        }

        public string RoadTypeColumnName
        {
            get { return roadTypeColumnName; }
            set { roadTypeColumnName = value; }
        }

        public Dictionary<string, double> RoadSpeeds
        {
            get
            {
                if (roadSpeeds == null)
                {
                    roadSpeeds = new Dictionary<string, double>();
                }
                return roadSpeeds;
            }
        }
    }
}
