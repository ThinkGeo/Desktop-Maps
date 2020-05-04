using System.ComponentModel.Composition;
using System.Windows;
using ThinkGeo.UI.Wpf;

namespace NauticalChartsViewer
{
    [InheritedExport]
    internal abstract class MenuItemMessageHandler
    {
        public abstract void Handle(Window owner, MapView map, MenuItemMessage message);

        public abstract string[] Actions { get; }
    }
}
