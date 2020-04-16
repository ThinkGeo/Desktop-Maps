using System.Windows;
using ThinkGeo.UI.Wpf;

namespace NauticalChartsViewer
{
    internal class SafeWaterDepthMenuItemMessageHandler : MenuItemMessageHandler
    {
        public override void Handle(Window owner, MapView map, MenuItemMessage message)
        {
            var window = new SafeWaterDepthSettingsWindow();
            window.Owner = owner;
            window.ShowDialog();
        }

        public override string[] Actions
        {
            get { return new[] { "safewaterdepth" }; }
        }
    }
}
