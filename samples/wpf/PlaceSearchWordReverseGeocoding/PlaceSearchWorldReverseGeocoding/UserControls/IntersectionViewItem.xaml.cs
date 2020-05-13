using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using ThinkGeo.MapSuite.WorldReverseGeocoding;

namespace ThinkGeo.MapSuite.PlaceSearchWorldReverseGeocodingSamples.UserControls
{
    /// <summary>
    /// Interaction logic for IntersectionViewItem.xaml
    /// </summary>
    public partial class IntersectionViewItem : UserControl
    {
        public IntersectionViewItem()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var record = DataContext as Intersection;
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri("/Resources/intersection.png", UriKind.RelativeOrAbsolute);
            bitmap.EndInit();
            imgIcon.Source = bitmap;

            txbDistanceAndDirection.Text = string.Format("{0}{1}{2}", record.OptionalNames["distance"], string.IsNullOrEmpty(record.OptionalNames["direction"]) ? "m" : "m, ", record.OptionalNames["direction"]);

            List<string> roadNames = record.Description.Split(',').ToList();
            txbName.Inlines.Clear();
            txbName.Inlines.Add(new Run("Intersection between "));
            for (int i = 0; i < roadNames.Count - 1; i++)
            {
                txbName.Inlines.Add(new Run(roadNames[i]) { FontSize = 13, FontWeight = FontWeights.SemiBold });
                txbName.Inlines.Add(new Run(" and "));
            }
            txbName.Inlines.Add(new Run(roadNames[roadNames.Count - 1]) { FontSize = 13, FontWeight = FontWeights.SemiBold });

            if (record.OptionalNames.ContainsKey("index"))
            {
                counter.Text = record.OptionalNames["index"];
            }

            txbAddress.Text = string.Format("in {0}", record.Address);
        }
    }
}
