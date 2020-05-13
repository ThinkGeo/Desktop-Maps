using System.Windows;
using ThinkGeo.MapSuite.Wpf;

namespace NauticalChartsViewer
{
    internal class WorldMapShowingMenuItemMessageHandler : MenuItemMessageHandler
    {
        private const string thinkGeoCloudMapsOverlayName = "ThinkGeoCloudMapsOverlay";

        public override void Handle(Window owner, WpfMap map, MenuItemMessage message)
        {
            /*===========================================
               Backgrounds for this sample are powered by ThinkGeo Cloud Maps and require
               a Client ID and Secret. These were sent to you via email when you signed up
               with ThinkGeo, or you can register now at https://cloud.thinkgeo.com.
            ===========================================*/
            ThinkGeoCloudRasterMapsOverlay backgroundOverlay;
            if (map.Overlays.Contains(thinkGeoCloudMapsOverlayName))
            {
                backgroundOverlay = map.Overlays[thinkGeoCloudMapsOverlayName] as ThinkGeoCloudRasterMapsOverlay;
            }
            else
            {
                backgroundOverlay = new ThinkGeoCloudRasterMapsOverlay();
                map.Overlays.Insert(0, backgroundOverlay);
            }

            backgroundOverlay.IsVisible = true;
            switch (message.MenuItem.Action.ToLowerInvariant())
            {
                case "light":
                    backgroundOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Light;
                    break;

                case "aerial":
                    backgroundOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Aerial;
                    break;

                case "hybrid":
                    backgroundOverlay.MapType = ThinkGeoCloudRasterMapsMapType.Hybrid;
                    break;

                case "none":
                    backgroundOverlay.IsVisible = false;
                    break;
            }

            map.Refresh(backgroundOverlay);
        }

        public override string[] Actions
        {
            get { return new[] { "light", "aerial", "hybrid", "none" }; }
        }
    }
}