using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace NauticalChartsViewer
{
    // Toggles the additional S-52 display options that NauticalChartsFeatureLayer supports:
    // full light lines, shallow-water pattern, isolated dangers in shallow water,
    // SCAMIN (minimum-scale decluttering) and two- vs four-color depth shades.
    internal class S52OptionsMenuItemMessageHandler : MenuItemMessageHandler
    {
        private const string chartsOverlayName = "ChartsOverlay";

        public async override void Handle(Window owner, MapView map, MenuItemMessage message)
        {
            bool isChecked = message.MenuItem.IsChecked;
            switch (message.MenuItem.Action)
            {
                case "fulllightline":
                    Globals.IsFullLightLineVisible = isChecked;
                    break;
                case "shallowwaterpattern":
                    Globals.IsShallowWaterPatternVisible = isChecked;
                    break;
                case "isolateddanger":
                    Globals.IsIsolatedDangerVisible = isChecked;
                    break;
                case "minimumscale":
                    Globals.IsMinimumScaleEnabled = isChecked;
                    break;
                case "fourcolordepths":
                    Globals.CurrentDepthShades = isChecked ? NauticalChartsDepthShades.FourColor : NauticalChartsDepthShades.TwoColor;
                    break;
                case "s52styling":
                    // Checked = full S-52 presentation library; unchecked = plain feature styling.
                    Globals.CurrentStylingType = isChecked ? NauticalChartsStylingType.EmbeddedStyling : NauticalChartsStylingType.StandardStyling;
                    break;
            }

            if (map.Overlays.Contains(chartsOverlayName))
            {
                LayerOverlay chartsOverlay = map.Overlays[chartsOverlayName] as LayerOverlay;
                foreach (var item in chartsOverlay.Layers)
                {
                    if (item is NauticalChartsFeatureLayer layer)
                    {
                        layer.IsFullLightLineVisible = Globals.IsFullLightLineVisible;
                        layer.IsShallowWaterPatternVisible = Globals.IsShallowWaterPatternVisible;
                        layer.IsIsolatedDangerInShallowWaterVisible = Globals.IsIsolatedDangerVisible;
                        layer.IsMinimumScaleEnabled = Globals.IsMinimumScaleEnabled;
                        layer.DepthShades = Globals.CurrentDepthShades;
                        layer.StylingType = Globals.CurrentStylingType;
                    }
                }
                await map.RefreshAsync();
            }
        }

        public override string[] Actions
        {
            get { return new[] { "fulllightline", "shallowwaterpattern", "isolateddanger", "minimumscale", "fourcolordepths", "s52styling" }; }
        }
    }
}
