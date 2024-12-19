﻿using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;

namespace NauticalChartsViewer
{
    internal class TextVisibilibyMenuItemMessageHandler : MenuItemMessageHandler
    {
        private const string chartsOverlayName = "ChartsOverlay";

        public async override void Handle(Window owner, MapView map, MenuItemMessage message)
        {
            switch (message.MenuItem.Action)
            {
                case "contourlabel":
                    Globals.IsDepthContourTextVisible = message.MenuItem.IsChecked;
                    break;

                case "soundinglabel":
                    Globals.IsSoundingTextVisible = message.MenuItem.IsChecked;
                    break;

                case "lightdescription":
                    Globals.IsLightDescriptionVisible = message.MenuItem.IsChecked;
                    break;

                case "textlabel":
                    if (message.MenuItem.IsChecked)
                    {
                        Globals.SymbolTextDisplayMode = NauticalChartsSymbolTextDisplayMode.English;
                    }
                    break;

                case "nationallanguagelabel":

                    if (message.MenuItem.IsChecked)
                    {
                        Globals.SymbolTextDisplayMode = NauticalChartsSymbolTextDisplayMode.NationalLanguage;
                    }
                    break;
                case "notextlabel":

                    if (message.MenuItem.IsChecked)
                    {
                        Globals.SymbolTextDisplayMode = NauticalChartsSymbolTextDisplayMode.None;
                    }
                    break;
            }
            if (map.Overlays.Contains(chartsOverlayName))
            {
                LayerOverlay chartsOverlay = map.Overlays[chartsOverlayName] as LayerOverlay;

                foreach (var item in chartsOverlay.Layers)
                {
                    NauticalChartsFeatureLayer nauticalChartsFeatureLayer = item as NauticalChartsFeatureLayer;
                    nauticalChartsFeatureLayer.IsDepthContourTextVisible = Globals.IsDepthContourTextVisible;
                    nauticalChartsFeatureLayer.IsLightDescriptionVisible = Globals.IsLightDescriptionVisible;
                    nauticalChartsFeatureLayer.IsSoundingTextVisible = Globals.IsSoundingTextVisible;
                    nauticalChartsFeatureLayer.SymbolTextDisplayMode = Globals.SymbolTextDisplayMode;
                }

                await map.RefreshAsync();
            }
        }

        public override string[] Actions
        {
            get { return new[] { "contourlabel", "soundinglabel", "lightdescription", "textlabel", "notextlabel", "nationallanguagelabel" }; }
        }
    }
}