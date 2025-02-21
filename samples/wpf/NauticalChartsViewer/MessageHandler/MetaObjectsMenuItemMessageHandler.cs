using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace NauticalChartsViewer
{
    internal class MetaObjectsMenuItemMessageHandler : MenuItemMessageHandler
    {
        private const string chartsOverlayName = "ChartsOverlay";

        public async override void Handle(Window owner, MapView map, MenuItemMessage message)
        {
            Globals.IsMetaObjectsVisible = message.MenuItem.IsChecked;
            if (map.Overlays.Contains(chartsOverlayName))
            {
                LayerOverlay chartsOverlay = map.Overlays[chartsOverlayName] as LayerOverlay;
                foreach (var item in chartsOverlay.Layers)
                {
                    NauticalChartsFeatureLayer maritimeFeatureLayer = item as NauticalChartsFeatureLayer;
                    maritimeFeatureLayer.IsMetaObjectsVisible = Globals.IsMetaObjectsVisible;
                }
                await map.RefreshAsync();
            }
        }

        public override string[] Actions
        {
            get { return new[] { "metaobjects" }; }
        }
    }
}
