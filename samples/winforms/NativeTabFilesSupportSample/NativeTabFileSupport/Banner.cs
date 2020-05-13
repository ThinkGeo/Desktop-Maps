using System;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Windows.Forms;

public partial class Banner : UserControl
{
    public Banner()
    {
        InitializeComponent();
        SetupBannerAd();
    }

    public void SetupBannerAd()
    {
        if (Process.GetCurrentProcess().ProcessName != "devenv")
        {
            adRotator.Url = new Uri(new DirectoryInfo(@"..\..\Resources\bannerad_offline.html").FullName);
            if (IsNetworkAlive())
            {
                adsRotatorTimer.Start();
            }
        }
    }

    private static bool IsNetworkAlive()
    {
        ObjectQuery objectQuery = new ObjectQuery("select * from Win32_NetworkAdapter where NetConnectionStatus=2");
        ManagementObjectSearcher searcher = null;
        try
        {
            searcher = new ManagementObjectSearcher(objectQuery);

            return (searcher.Get().Count > 0);
        }
        finally
        {
            if (searcher != null)
            {
                searcher.Dispose();
            }
        }
    }

    private void adsRotatorTimer_Tick(object sender, EventArgs e)
    {
        adRotator.Navigate("http://gis.thinkgeo.com/Default.aspx?tabid=640&random=" + Guid.NewGuid().ToString());
    }
}
