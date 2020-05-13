using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    /// <summary>
    /// Interaction logic for PopupUserControl.xaml
    /// </summary>
    public partial class PopupUserControl : UserControl
    {
        private PopupOverlay popupOverlay;

        public PopupUserControl()
            : this(string.Empty)
        { }

        public PopupUserControl(Feature feature)
        {
            InitializeComponent();

            string speed = feature.ColumnValues["Speed"];
            string name = feature.ColumnValues["VehicleName"];
            string latitude = feature.ColumnValues["Latitude"];
            string dateTime = feature.ColumnValues["DateTime"];
            string longitude = feature.ColumnValues["Longitude"];

            txtName.Text = name;
            double x, y;
            if (double.TryParse(longitude, out x) && double.TryParse(latitude, out y))
            {
                Proj4Projection proj4 = new Proj4Projection();
                proj4.InternalProjectionParametersString = Proj4Projection.GetDecimalDegreesParametersString();
                proj4.ExternalProjectionParametersString = Proj4Projection.GetGoogleMapParametersString();
                proj4.Open();

                Vertex vertex = proj4.ConvertToInternalProjection(x, y);
                txtLongitude.Text = vertex.X.ToString("N6", CultureInfo.InvariantCulture);
                txtLatitude.Text = vertex.Y.ToString("N6", CultureInfo.InvariantCulture);

                proj4.Close();
            }
            else
            {
                txtLongitude.Text = longitude;
                txtLatitude.Text = latitude;
            }

            txtSpeed.Text = speed + " mph";
            txtTime.Text = dateTime;
        }

        public PopupUserControl(string content)
        {
            InitializeComponent();

            txtContent.Text = content;
            txtContent.Visibility = Visibility.Visible;
            vehiclePanel.Visibility = Visibility.Collapsed;
            txtName.Visibility = Visibility.Collapsed;
        }

        public PopupOverlay PopupOverlay
        {
            get { return popupOverlay; }
            set { popupOverlay = value; }
        }

        private void ClosePopupClick(object sender, RoutedEventArgs e)
        {
            if (PopupOverlay != null)
            {
                PopupOverlay.Popups.Remove((Popup) Parent);
                PopupOverlay.Refresh();
            }
        }
    }
}
