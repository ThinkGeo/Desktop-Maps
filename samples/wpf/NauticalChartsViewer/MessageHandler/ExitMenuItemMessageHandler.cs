using System;
using System.Windows;
using ThinkGeo.UI.Wpf;

namespace NauticalChartsViewer
{
    internal class ExitMenuItemMessageHandler : MenuItemMessageHandler
    {
        public override void Handle(Window owner, MapView map, MenuItemMessage message)
        {
            Environment.Exit(0);
        }

        public override string[] Actions
        {
            get { return new[] { "exit" }; }
        }
    }
}
