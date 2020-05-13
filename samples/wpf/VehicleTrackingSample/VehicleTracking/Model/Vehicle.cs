using System.Collections.ObjectModel;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    /// <summary>
    /// This class stands for a vehicle.
    /// </summary>
    public class Vehicle
    {
        private int id;
        private string name;
        private string iconPath;
        private Location location;
        private Collection<Location> historyLocations;

        public Vehicle()
            : this(0)
        { }

        public Vehicle(int id)
        {
            Id = id;
            Name = string.Empty;
            Location = new Location();
            historyLocations = new Collection<Location>();
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public Location Location
        {
            get { return location; }
            set { location = value; }
        }

        public Collection<Location> HistoryLocations
        {
            get { return historyLocations; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string IconPath
        {
            get { return iconPath; }
            set { iconPath = value; }
        }

        public int GetSpeedDuration()
        {
            int speedDuration = 0;
            double lastSpeed = Location.Speed;
            foreach (Location tempLocation in HistoryLocations)
            {
                if (tempLocation.Speed == lastSpeed) speedDuration++;
                else break;
            }

            return speedDuration;
        }

        /// <summary>
        /// If the Vehicle's speed is not 0 in the passed 4 minutes, we say it is in Motion. 
        /// </summary>
        /// <returns>State of current vehicle.</returns>
        public VehicleMotionState GetCurrentState()
        {
            VehicleMotionState vehicleState = VehicleMotionState.Idle;

            if (Location.Speed != 0)
            {
                vehicleState = VehicleMotionState.Motion;
            }
            else
            {
                int locationIndex = 0;
                foreach (Location historyLocation in HistoryLocations)
                {
                    if (locationIndex > 3)
                    {
                        break;
                    }
                    if (historyLocation.Speed != 0)
                    {
                        vehicleState = VehicleMotionState.Motion;
                        break;
                    }
                    locationIndex++;
                }
            }

            return vehicleState;
        }
    }
}
