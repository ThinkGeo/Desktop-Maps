using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.SiteSelection.Properties;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public static class MapSuiteSampleHelper
    {
        private static readonly string bingLoginServiceTemplate = "http://dev.virtualearth.net/REST/v1/Imagery/Metadata/{0}?&incl=ImageryProviders&o=xml&key={1}";

        public static GraphicsPath CreateRoundPath(Rectangle rect, int arcRadius)
        {
            GraphicsPath path = new GraphicsPath();

            if (rect.Width == 0 || rect.Height == 0)
            {
                return path;
            }

            if (arcRadius > 0)
            {
                path.AddArc(rect.Left, rect.Top, arcRadius, arcRadius, 180, 90);
                path.AddArc(rect.Right - arcRadius, rect.Top, arcRadius, arcRadius, -90, 90);
                path.AddArc(rect.Right - arcRadius, rect.Bottom - arcRadius, arcRadius, arcRadius, 0, 90);
                path.AddArc(rect.Left, rect.Bottom - arcRadius, arcRadius, arcRadius, 90, 90);
            }

            path.CloseFigure();
            return path;
        }

        public static ServiceAreaAnalysis GetAreaAnalysis(AreaCreateMode createAreaMode)
        {
            ServiceAreaAnalysis serviceAreaAnalysis;
            switch (createAreaMode)
            {
                case AreaCreateMode.Route:
                    serviceAreaAnalysis = new RouteAccessibleAreaAnalysis();
                    break;

                default:
                    serviceAreaAnalysis = new BufferServiceAreaAnalysis();
                    break;
            }

            return serviceAreaAnalysis;
        }

        public static string GetBingMapsApplicationId(WinForms.BingMapsMapType mapType)
        {
            string exePath = Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(exePath);
            KeyValueConfigurationElement appMapKeySetting = config.AppSettings.Settings["BingMapKey"];
            string bingMapsKey = appMapKeySetting != null ? appMapKeySetting.Value : string.Empty;
            if (string.IsNullOrEmpty(bingMapsKey))
            {
                BingMapsApplicationIdPromptForm inputBingMapKeyWindow = new BingMapsApplicationIdPromptForm();
                inputBingMapKeyWindow.StartPosition = FormStartPosition.CenterParent;
                if (inputBingMapKeyWindow.ShowDialog() == DialogResult.OK
                    && IsBingMapsKeyValid(inputBingMapKeyWindow.ApplicationId, mapType))
                {
                    bingMapsKey = inputBingMapKeyWindow.ApplicationId;
                    config.AppSettings.Settings.Add("BingMapKey", bingMapsKey);
                    config.Save();
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
            return bingMapsKey;
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

        private static bool IsBingMapsKeyValid(string bingMapsKey, WinForms.BingMapsMapType mapType)
        {
            bool isValid = false;

            try
            {
                string loginServiceUri = String.Format(CultureInfo.InvariantCulture
                              , bingLoginServiceTemplate, mapType, bingMapsKey);

                WebRequest request = WebRequest.Create(loginServiceUri);
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(stream);
                XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xDoc.NameTable);
                xmlNamespaceManager.AddNamespace("bing", "http://schemas.microsoft.com/search/local/ws/rest/v1");

                XmlNode root = xDoc.SelectSingleNode("bing:Response", xmlNamespaceManager);
                XmlNode imageUrlElement = root.SelectSingleNode("bing:ResourceSets/bing:ResourceSet/bing:Resources/bing:ImageryMetadata/bing:ImageUrl", xmlNamespaceManager);
                XmlNodeList subdomainsElement
                    = root.SelectNodes("bing:ResourceSets/bing:ResourceSet/bing:Resources/bing:ImageryMetadata/bing:ImageUrlSubdomains/bing:string", xmlNamespaceManager);

                if (imageUrlElement != null && subdomainsElement != null)
                {
                    isValid = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return isValid;
        }
    }
}