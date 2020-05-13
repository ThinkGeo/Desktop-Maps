using System;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows;
using System.Xml;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public static class MapSuiteSampleHelper
    {
        public static Stream GetResourceStream(string virualPath)
        {
            Stream vehicleStream = Application.GetResourceStream(new Uri("/Image/" + virualPath, UriKind.RelativeOrAbsolute)).Stream;
            vehicleStream.Seek(0, SeekOrigin.Begin);
            return vehicleStream;
        }

        public static RectangleShape GetBufferedRectangle(PointShape pointShape, double resolution, double tolerance = 8)
        {
            PointShape clickedPoint = pointShape;
            double offset = resolution * 8;
            return new RectangleShape(clickedPoint.X - offset, clickedPoint.Y + offset, clickedPoint.X + offset, clickedPoint.Y - offset);
        }

        public static string GetBingMapsKey()
        {
            string exePath = Application.ResourceAssembly.Location;
            Configuration config = ConfigurationManager.OpenExeConfiguration(exePath);
            KeyValueConfigurationElement appMapKeySetting = config.AppSettings.Settings["BingMapKey"];
            if (appMapKeySetting != null)
            {
                return appMapKeySetting.Value;
            }

            BingMapsApplicationIdPromptWindow inputBingMapKeyWindow = new BingMapsApplicationIdPromptWindow();
            inputBingMapKeyWindow.Owner = Application.Current.MainWindow;
            inputBingMapKeyWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (inputBingMapKeyWindow.ShowDialog().GetValueOrDefault() && IsBingMapsKeyValid(inputBingMapKeyWindow.ApplicationId))
            {
                SaveBingMapsKey(inputBingMapKeyWindow.ApplicationId);
                return inputBingMapKeyWindow.ApplicationId;
            }
            return string.Empty;
        }

        public static bool IsBingMapsKeyValid(string BingMapsKey)
        {
            bool result = false;
            string loginServiceTemplate = "http://dev.virtualearth.net/REST/v1/Imagery/Metadata/{0}?&incl=ImageryProviders&o=xml&key={1}";

            try
            {
                string loginServiceUri = string.Format(CultureInfo.InvariantCulture, loginServiceTemplate, BingMapsMapType.Road, BingMapsKey);

                WebRequest request = WebRequest.Create(loginServiceUri);
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
