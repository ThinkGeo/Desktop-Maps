using System.Windows;
using ThinkGeo.UI.Wpf;

namespace NauticalChartsViewer
{
    internal abstract class MenuItemMessageHandler
    {
        public abstract void Handle(Window owner, MapView map, MenuItemMessage message);

        public abstract string[] Actions { get; }
    }
}
