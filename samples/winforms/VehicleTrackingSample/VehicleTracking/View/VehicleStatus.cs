using System;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.MapSuite.VehicleTracking.Properties;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public partial class VehicleStatus : UserControl
    {
        public event EventHandler<VehicleStatusClickVehicleStatusEventArgs> VehicleClick;

        public VehicleStatus(Vehicle vehicle)
        {
            InitializeComponent();
            LoadStatus(vehicle);
        }

        public void LoadStatus(Vehicle vehicle)
        {
            VehiclePictureBox.Image = vehicle.Icon;
            NameLinkLabel.Text = vehicle.Name;
            StatusLabel.Text = vehicle.GetCurrentState() == VehicleMotionState.Idle ? Resources.IdleText : Resources.InMotionText;
            AreaLabel.Text = vehicle.RestrictedAreaText;
            DurationLabel.Text = vehicle.GetSpeedDuration().ToString(CultureInfo.InvariantCulture) + Resources.VehicleStatus_LoadStatus__min;
            SpeedLabel.Text = vehicle.Location.Speed.ToString(CultureInfo.InvariantCulture) + Resources.VehicleStatus_LoadStatus__mph;
        }

        private void VehiclePictureBox_Click(object sender, EventArgs e)
        {
            OnVehicleClick(new VehicleStatusClickVehicleStatusEventArgs(NameLinkLabel.Text));
        }

        protected virtual void OnVehicleClick(VehicleStatusClickVehicleStatusEventArgs e)
        {
            EventHandler<VehicleStatusClickVehicleStatusEventArgs> handler = VehicleClick;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
