using System.Windows;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Core;

namespace NauticalChartsViewer
{
    internal class WorldMapShowingMenuItemMessageHandler : MenuItemMessageHandler
    {
        private const string thinkGeoCloudMapsOverlayName = "ThinkGeoCloudMapsOverlay";
        private const string thinkGeoCloudClientId = "USlbIyO5uIMja2y0qoM21RRM6NBXUad4hjK3NBD6pD0~";
        private const string thinkGeoCloudClientSecret = "f6OJsvCDDzmccnevX55nL7nXpPDXXKANe5cN6czVjCH0s8jhpCH-2A~~";

        public async override void Handle(Window owner, MapView map, MenuItemMessage message)
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
                backgroundOverlay = new ThinkGeoCloudRasterMapsOverlay(thinkGeoCloudClientId, thinkGeoCloudClientSecret);
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

            await map.RefreshAsync(backgroundOverlay);
        }

        public override string[] Actions
        {
            get { return new[] { "light", "aerial", "hybrid", "none" }; }
        }
    }
}