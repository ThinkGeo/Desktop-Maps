using System;
using System.Globalization;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.VehicleTracking.Properties;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public partial class VehicleTrailPopup : UserControl
    {
        public VehicleTrailPopup(Feature feature)
        {
            InitializeComponent();

            double longitude;
            double latitude;
            string longitudeString = "--";
            string latitudeString = "--";
            if (double.TryParse(feature.ColumnValues["Longitude"], out longitude) && double.TryParse(feature.ColumnValues["Latitude"], out latitude))
            {
                Proj4Projection projection = new Proj4Projection();
                projection.InternalProjectionParametersString = Proj4Projection.GetWgs84ParametersString();
                projection.ExternalProjectionParametersString = Proj4Projection.GetSphericalMercatorParametersString();
                projection.Open();
                PointShape pointInWgs84 = (PointShape)projection.ConvertToInternalProjection(new PointShape(longitude, latitude));
                projection.Close();

                longitudeString = pointInWgs84.X.ToString("N6", CultureInfo.InvariantCulture);
                latitudeString = pointInWgs84.Y.ToString("N6", CultureInfo.InvariantCulture);
            }

            NameLabel.Text = feature.ColumnValues["VehicleName"];
            LongitudeLabel.Text = longitudeString;
            LatitudeLabel.Text = latitudeString;
            SpeedLabel.Text = feature.ColumnValues["Speed"] + Resources.MphText;
            TimeLabel.Text = feature.ColumnValues["DateTime"];
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            if (Parent != null)
            {
                Parent.Controls.Remove(this);
            }
        }
    }
}