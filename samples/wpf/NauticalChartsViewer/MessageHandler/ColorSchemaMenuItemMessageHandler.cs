using System;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace NauticalChartsViewer
{
    internal class ColorSchemaMenuItemMessageHandler : MenuItemMessageHandler
    {
        private const string chartsOverlayName = "ChartsOverlay";

        public async override void Handle(Window owner, MapView map, MenuItemMessage message)
        {
            Globals.CurrentColorSchema = (NauticalChartsDefaultColorSchema)Enum.Parse(typeof(NauticalChartsDefaultColorSchema), message.MenuItem.Action, true);
            if (map.Overlays.Contains(chartsOverlayName))
            {
                LayerOverlay chartsOverlay = map.Overlays[chartsOverlayName] as LayerOverlay;
                foreach (var item in chartsOverlay.Layers)
                {
                    NauticalChartsFeatureLayer maritimeFeatureLayer = item as NauticalChartsFeatureLayer;
                    maritimeFeatureLayer.DefaultColorSchema = Globals.CurrentColorSchema;
                }
                await map.RefreshAsync();
            }
        }

        public override string[] Actions
        {
            get { return new[] { "daybright", "dayblackback", "daywhiteback", "dusk", "night" }; }
        }
    }
}
