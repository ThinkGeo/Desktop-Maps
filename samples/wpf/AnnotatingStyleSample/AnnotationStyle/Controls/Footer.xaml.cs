using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace ThinkGeo.Samples
{
    /// <summary>
    /// Interaction logic for Footer.xaml
    /// </summary>
    public partial class Footer : UserControl
    {
        public Footer()
        {
            InitializeComponent();
        }

        private void Products_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", "https://thinkgeo.com/gis-ui-controls");
        }

        private void Products_MouseEnter(object sender, MouseEventArgs e)
        {
            Product.Source = new BitmapImage(new Uri("/Resources/btn_active_map_suite_products.png", UriKind.RelativeOrAbsolute));
        }

        private void Products_MouseLeave(object sender, MouseEventArgs e)
        {
            Product.Source = new BitmapImage(new Uri("/Resources/btn_inactive_map_suite_products.png", UriKind.RelativeOrAbsolute));
        }

        private void Support_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", "https://helpdesk.thinkgeo.com");
        }

        private void Support_MouseEnter(object sender, MouseEventArgs e)
        {
            Support.Source = new BitmapImage(new Uri("/Resources/btn_active_support_center.png", UriKind.RelativeOrAbsolute));
        }

        private void Support_MouseLeave(object sender, MouseEventArgs e)
        {
            Support.Source = new BitmapImage(new Uri("/Resources/btn_inactive_support_center.png", UriKind.RelativeOrAbsolute));
        }

        private void Discussion_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", "https://community.thinkgeo.com");
        }

        private void Discussion_MouseEnter(object sender, MouseEventArgs e)
        {
            Discussion.Source = new BitmapImage(new Uri("/Resources/btn_active_discussion_forums.png", UriKind.RelativeOrAbsolute));
        }

        private void Discussion_MouseLeave(object sender, MouseEventArgs e)
        {
            Discussion.Source = new BitmapImage(new Uri("/Resources/btn_inactive_discussion_forums.png", UriKind.RelativeOrAbsolute));
        }

        private void Wiki_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", "https://wiki.thinkgeo.com/wiki/thinkgeo_desktop_for_wpf");
        }

        private void Wiki_MouseEnter(object sender, MouseEventArgs e)
        {
            Wiki.Source = new BitmapImage(new Uri("/Resources/btn_active_thinkgeo_wiki.png", UriKind.RelativeOrAbsolute));
        }

        private void Wiki_MouseLeave(object sender, MouseEventArgs e)
        {
            Wiki.Source = new BitmapImage(new Uri("/Resources/btn_inactive_thinkgeo_wiki.png", UriKind.RelativeOrAbsolute));
        }

        private void Contact_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("explorer.exe", "https://thinkgeo.com/about-us#contact");
        }

        private void Contact_MouseEnter(object sender, MouseEventArgs e)
        {
            Contact.Source = new BitmapImage(new Uri("/Resources/btn_active_contact_us.png", UriKind.RelativeOrAbsolute));
        }

        private void Contact_MouseLeave(object sender, MouseEventArgs e)
        {
            Contact.Source = new BitmapImage(new Uri("/Resources/btn_inactive_contact_us.png", UriKind.RelativeOrAbsolute));
        }
    }
}