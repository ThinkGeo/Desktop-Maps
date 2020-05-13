using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows;
using System.Xml;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.SiteSelection.Properties;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public static class MapSuiteSampleHelper
    {
        public static string GetBingMapsApplicationId()
        {
            string bingMapsKey = string.Empty;

            string exePath = Application.ResourceAssembly.Location;
            Configuration config = ConfigurationManager.OpenExeConfiguration(exePath);
            KeyValueConfigurationElement appMapKeySetting = config.AppSettings.Settings["BingMapKey"];
            if (appMapKeySetting != null)
            {
                bingMapsKey = appMapKeySetting.Value;
            }
            else
            {
                BingMapsApplicationIdPromptWindow inputBingMapKeyWindow = new BingMapsApplicationIdPromptWindow();
                inputBingMapKeyWindow.Owner = Application.Current.MainWindow;
                inputBingMapKeyWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                if (inputBingMapKeyWindow.ShowDialog().GetValueOrDefault() && IsBingMapsKeyValid(inputBingMapKeyWindow.ApplicationId, BingMapsMapType.Road))
                {
                    SaveBingMapsKey(inputBingMapKeyWindow.ApplicationId);
                    bingMapsKey = inputBingMapKeyWindow.ApplicationId;
                }
            }

            return bingMapsKey;
        }

        public static ServiceAreaAnalysis GetAreaAnalysis(CreateAreasMode createMode)
        {
            switch (createMode)
            {
                case CreateAreasMode.Route:
                    return new RouteAccessibleAreaAnalysis();

                default:
                    return new BufferServiceAreaAnalysis();
            }
        }

        public static string GetDefaultColumnNameByPoiType(string poiType)
        {
            string result = string.Empty;
            if (poiType.Equals(Resources.RestaurantsLayerKey, StringComparison.OrdinalIgnoreCase))
            {
                result = "FoodType";
            }
            else if (poiType.Equals(Resources.MedicalFacilitesLayerKey, StringComparison.OrdinalIgnoreCase)
                || poiType.Equals(Resources.SchoolsLayerKey, StringComparison.OrdinalIgnoreCase))
            {
                result = "TYPE";
            }
            else if (poiType.Equals(Resources.PublicFacilitesLayerKey, StringComparison.OrdinalIgnoreCase))
            {
                result = "AGENCY";
            }
            else if (poiType.Equals(Resources.HotelsLayerKey, StringComparison.OrdinalIgnoreCase))
            {
                result = "Hotels";
            }
            return result;
        }

        private static void SaveBingMapsKey(string bingMapsKey)
        {
            try
            {
                string exePath = Application.ResourceAssembly.Location;
                Configuration config = ConfigurationManager.OpenExeConfiguration(exePath);
                config.AppSettings.Settings.Add("BingMapKey", bingMapsKey);
                config.Save();
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private static bool IsBingMapsKeyValid(string BingMapsKey, BingMapsMapType mapType)
        {
            bool result = false;
            string loginServiceTemplate = "http://dev.virtualearth.net/REST/v1/Imagery/Metadata/{0}?&incl=ImageryProviders&o=xml&key={1}";

            try
            {
                string loginServiceUri = string.Format(CultureInfo.InvariantCulture
                              , loginServiceTemplate, mapType, BingMapsKey);

                WebRequest request = HttpWebRequest.Create(loginServiceUri);
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(stream);
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xDoc.NameTable);
                nsmgr.AddNamespace("bing", "http://schemas.microsoft.com/search/local/ws/rest/v1");

                var root = xDoc.SelectSingleNode("bing:Response", nsmgr);
                var imageUrlElement = root.SelectSingleNode("bing:ResourceSets/bing:ResourceSet/bing:Resources/bing:ImageryMetadata/bing:ImageUrl", nsmgr);
                var subdomainsElement
                    = root.SelectNodes("bing:ResourceSets/bing:ResourceSet/bing:Resources/bing:ImageryMetadata/bing:ImageUrlSubdomains/bing:string", nsmgr);

                if (imageUrlElement != null && subdomainsElement != null)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            return result;
        }
    }
}