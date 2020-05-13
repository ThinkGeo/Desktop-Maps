using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using ThinkGeo.MapSuite.WorldReverseGeocoding;

namespace ThinkGeo.MapSuite.PlaceSearchWorldReverseGeocodingSamples.UserControls
{
    /// <summary>
    /// Interaction logic for NearbyPlaceViewItem.xaml
    /// </summary>
    public partial class NearbyPlaceViewItem : UserControl
    {
        public NearbyPlaceViewItem()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var record = DataContext as Place;
            string category = null;
            if (record.PlaceCategory.ToString().Contains(","))
            {
                category = record.PlaceCategory.ToString().Split(',')[1].ToLower().Trim();
            }
            else
            {
                category = record.PlaceCategory.ToString().ToLower();
            }
            string iconUri = string.Format("/Resources/{0}.png", category);
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

            txbDistanceAndDirection.Text = string.Format("{0}{1}{2}", distance, string.IsNullOrEmpty(direction) ? "m" : "m, ", direction);

            txbAddressWithType.Text = string.Format("{0} ({1})", record.Address, string.IsNullOrEmpty(record.PlaceType) ? "-" : record.PlaceType);
        }
    }
}
