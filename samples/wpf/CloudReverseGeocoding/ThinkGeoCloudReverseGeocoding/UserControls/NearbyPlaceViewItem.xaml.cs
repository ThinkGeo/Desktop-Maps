using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ThinkGeo.Cloud;

namespace ThinkGeoCloudReverseGeocoding.UserControls
{

    public partial class NearbyPlaceViewItem : UserControl
    {
        public NearbyPlaceViewItem()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var record = DataContext as ReverseGeocodingLocation;

            string iconUri = string.Format("/Resources/{0}.png", record.LocationCategory);
            if (!string.IsNullOrEmpty(iconUri))
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(iconUri, UriKind.RelativeOrAbsolute);
                bitmap.EndInit();
                imgIcon.Source = bitmap;
            }

            string distance = string.Empty, direction = string.Empty;
            if (record.Properties.ContainsKey("distance"))
            {
                distance = record.Properties["distance"];
            }

            if (record.Properties.ContainsKey("direction"))
            {
                direction = record.Properties["direction"];
            }

            if (record.Properties.ContainsKey("index"))
            {
                counter.Text = record.Properties["index"];
            }

            if (record.LocationType == "Address")
            {
                txbDistanceAndDirection.Text = string.Format("{0} ({1})", (record.Address).Split(',')[0], string.IsNullOrEmpty(record.LocationType) ? "-" : (record.LocationType).Replace('_', ' '));
            }
            else
            {
                txbDistanceAndDirection.Text = string.Format("{0} ({1})", record.LocationName, string.IsNullOrEmpty(record.LocationType) ? "-" : (record.LocationType).Replace('_', ' '));
            }
            txbAddressWithType.Text = string.Format("{0} ({1} {2})", record.Address, Math.Round(decimal.Parse(distance), 1), "meters away");
        }
    }
}
