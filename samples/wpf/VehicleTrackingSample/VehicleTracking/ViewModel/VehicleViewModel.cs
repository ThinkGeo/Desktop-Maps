using System.Globalization;
using System.Linq;
using ThinkGeo.MapSuite.Layers;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public class VehicleViewModel : ViewModelBase
    {
        private static string vehicleImageFormat = "/Image/{0}";

        private string area;
        private string speed;
        private string status;
        private string iconUri;
        private string duration;
        private string ownerName;
        private MapModel mapModel;
        private Vehicle vehicle;
        private CommandBase zoomToVehicleCommand;

        public VehicleViewModel()
            : this(null, null)
        { }

        public VehicleViewModel(Vehicle vehicle, MapModel mapModel)
            : base()
        {
            this.vehicle = vehicle;
            this.ownerName = vehicle.Name;
            this.mapModel = mapModel;
            this.iconUri = string.Format(CultureInfo.InvariantCulture, vehicleImageFormat, vehicle.IconPath);

            this.Load();
        }

        public Vehicle Vehicle
        {
            get { return vehicle; }
            set { vehicle = value; }
        }

        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                RaisePropertyChanged(() => Status);
            }
        }

        public string IconUri
        {
            get { return iconUri; }
            set
            {
                iconUri = value;
                RaisePropertyChanged(() => IconUri);
            }
        }

        public string Area
        {
            get { return area; }
            set
            {
                area = value;
                RaisePropertyChanged(() => Area);
            }
        }

        public string OwnerName
        {
            get { return ownerName; }
            set
            {
                ownerName = value;
                RaisePropertyChanged(() => OwnerName);
            }
        }

        public string Speed
        {
            get { return speed; }
            set
            {
                speed = value;
                RaisePropertyChanged(() => Speed);
            }
        }

        public string Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                RaisePropertyChanged(() => Duration);
            }
        }

        public CommandBase ZoomToVehicleCommand
        {
            get
            {
                return zoomToVehicleCommand ?? (zoomToVehicleCommand = new CommandBase(() =>
                {
                    InMemoryFeatureLayer vehicleFeatureLayer = mapModel.TraceOverlay.Layers[OwnerName] as InMemoryFeatureLayer;
                    if (vehicleFeatureLayer != null)
                    {
                        mapModel.MapControl.ZoomIntoCenter(60, vehicleFeatureLayer.InternalFeatures.LastOrDefault());
                    }
                }));
            }
        }

        public void Load()
        {
            Status = vehicle.GetCurrentState() == VehicleMotionState.Idle ? "Idle" : "In Motion";
            Speed = vehicle.Location.Speed.ToString(CultureInfo.InvariantCulture) + " mph";
            Duration = vehicle.GetSpeedDuration().ToString(CultureInfo.InvariantCulture) + " min";
        }
    }
}