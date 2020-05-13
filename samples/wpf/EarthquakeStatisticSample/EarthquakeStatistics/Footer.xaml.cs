using System;
using System.Diagnostics;
using System.Net;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    /// <summary>
    /// Interaction logic for Bottom.xaml
    /// </summary>
    public partial class Footer : UserControl
    {
        public Footer()
        {
            InitializeComponent();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            //I intend to make use of this error message at a later stage, for now it is informational only
            string errMsg = "";

            try
            {
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
                e.Handled = true;
            }
            catch (UnauthorizedAccessException uae)
            {
                errMsg = uae.Message;
            }
            catch (WebException we)
            {
                errMsg = we.Message;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
        }
    }
}