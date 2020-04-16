using System.Diagnostics;
using System.Windows;
using ThinkGeo.UI.Wpf;

namespace NauticalChartsViewer
{
    internal class HomePageMenuItemMessageHandler : MenuItemMessageHandler
    {
        public override void Handle(Window owner, MapView map, MenuItemMessage message)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "http://wiki.thinkgeo.com/",
                UseShellExecute = true
            };
            Process.Start(psi);
        }

        public override string[] Actions
        {
            get { return new[] { "thinkgeohomepage" }; }
        }
    }
}
