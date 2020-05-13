using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using ThinkGeo.Cloud;

namespace ThinkGeoCloudReverseGeocoding.UserControls
{

    public partial class IntersectionViewItem : UserControl
    {
        public IntersectionViewItem()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var record = DataContext as ReverseGeocodingLocation;
            if (record.LocationType == "Intersection")
            {
                string iconUri = string.Format("/Resources/{0}.png", record.LocationType);
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
                txbDistanceAndDirection.Text = string.Format("{0}", record.DistanceFromQueryFeature);
                txbNamee.Text = string.Format("{0} {1}", Math.Round(decimal.Parse(distance), 1), "meters away" );
            }
           
        }
    }
}
