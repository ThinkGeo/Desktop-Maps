using System;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public class VehicleStatusClickVehicleStatusEventArgs : EventArgs
    {
        public VehicleStatusClickVehicleStatusEventArgs()
            : this(string.Empty)
        { }

        public VehicleStatusClickVehicleStatusEventArgs(string vehicleName)
        {
            VehicleName = vehicleName;
        }

        public string VehicleName { get; set; }
    }
}
