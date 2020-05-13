using System;
using System.ComponentModel;
using System.Management;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace DisplayIsoLines
{
    /// <summary>
    /// Interaction logic for SampleBanner.xaml
    /// </summary>
    public partial class Banner : UserControl
    {
        DispatcherTimer adsRotatorTimer = new DispatcherTimer();

        public Banner()
        {
            InitializeComponent();

            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                adsRotatorImage.Visibility = Visibility.Collapsed;
                AdRotatorHost.Visibility = Visibility.Visible;
                adsRotatorTimer.Tick += new EventHandler(timer_Tick);
                adsRotatorTimer.Interval = TimeSpan.FromSeconds(20);

                adsRotatorBrowser.Navigate(new Uri("http://gis.thinkgeo.com/Default.aspx?tabid=640"));

                if (IsNetworkAlive())
                {
                    adsRotatorTimer.Start();
                }
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            adsRotatorBrowser.Navigate("http://gis.thinkgeo.com/Default.aspx?tabid=640&random=" + Guid.NewGuid().ToString());
        }

        private static bool IsNetworkAlive()
        {
            ObjectQuery objectQuery = new ObjectQuery("select * from Win32_NetworkAdapter where NetConnectionStatus=2");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(objectQuery);
            return (searcher.Get().Count > 0);
        }
    }
}
