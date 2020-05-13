using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Resources;
using System.Xml;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public static class MapSuiteSampleHelper
    {
        public static Stream GetImageStream(string imageName)
        {
            StreamResourceInfo streamInfo = Application.GetResourceStream(new Uri(imageName, UriKind.RelativeOrAbsolute));
            return streamInfo != null ? streamInfo.Stream : null;
        }

        public static string GetBingMapsKey()
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
                if (inputBingMapKeyWindow.ShowDialog().GetValueOrDefault() && Validate(inputBingMapKeyWindow.ApplicationId, BingMapsMapType.Road))
                {
                    SaveBingMapsKey(inputBingMapKeyWindow.ApplicationId);
                    bingMapsKey = inputBingMapKeyWindow.ApplicationId;
                }
            }

            return bingMapsKey;
        }

        private static void SaveBingMapsKey(string bingMapsKey)
        {
            string exePath = Application.ResourceAssembly.Location;
            Configuration config = ConfigurationManager.OpenExeConfiguration(exePath);
            config.AppSettings.Settings.Add("BingMapKey", bingMapsKey);
            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        private static bool Validate(string BingMapsKey, BingMapsMapType mapType)
        {
            bool result = false;
            string loginServiceTemplate = "http://dev.virtualearth.net/REST/v1/Imagery/Metadata/{0}?&incl=ImageryProviders&o=xml&key={1}";

            try
            {
                string loginServiceUri = string.Format(CultureInfo.InvariantCulture
                              , loginServiceTemplate, mapType, BingMapsKey);

                WebRequest request = WebRequest.Create(loginServiceUri);
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();

                if (stream != null)
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(stream);
                    XmlNamespaceManager nsmgr = new XmlNamespaceManager(xDoc.NameTable);
                    nsmgr.AddNamespace("bing", "http://schemas.microsoft.com/search/local/ws/rest/v1");

                    XmlNode root = xDoc.SelectSingleNode("bing:Response", nsmgr);
                    XmlNode imageUrlElement = root.SelectSingleNode("bing:ResourceSets/bing:ResourceSet/bing:Resources/bing:ImageryMetadata/bing:ImageUrl", nsmgr);
                    XmlNodeList subdomainsElement = root.SelectNodes("bing:ResourceSets/bing:ResourceSet/bing:Resources/bing:ImageryMetadata/bing:ImageUrlSubdomains/bing:string", nsmgr);
                    if (imageUrlElement != null && subdomainsElement != null)
                    {
                        result = true;
                    }
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