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

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public static class MapSuiteSampleHelper
    {
        public static string GetBingMapsKey()
        {
            string exePath = Application.ExecutablePath;
            Configuration config = ConfigurationManager.OpenExeConfiguration(exePath);
            KeyValueConfigurationElement appMapKeySetting = config.AppSettings.Settings["BingMapKey"];
            string bingMapsKey = appMapKeySetting != null ? appMapKeySetting.Value : string.Empty;
            if (string.IsNullOrEmpty(bingMapsKey))
            {
                BingMapsApplicationIdPromptForm inputBingMapKeyWindow = new BingMapsApplicationIdPromptForm();
                inputBingMapKeyWindow.Owner = Application.OpenForms[0];
                if (inputBingMapKeyWindow.ShowDialog() == DialogResult.OK
                    && IsBingMapsKeyValid(inputBingMapKeyWindow.ApplicationId))
                {
                    bingMapsKey = inputBingMapKeyWindow.ApplicationId;
                    config.AppSettings.Settings.Add("BingMapKey", bingMapsKey);
                    config.Save();
                    ConfigurationManager.RefreshSection("appSettings");
                }
            }
            return bingMapsKey;
        }

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

        private static bool IsBingMapsKeyValid(string BingMapsKey)
        {
            bool isValid = false;
            const string loginServiceTemplate = "http://dev.virtualearth.net/REST/v1/Imagery/Metadata/{0}?&incl=ImageryProviders&o=xml&key={1}";

            try
            {
                string loginServiceUri = String.Format(CultureInfo.InvariantCulture
                              , loginServiceTemplate, WinForms.BingMapsMapType.Road, BingMapsKey);

                WebRequest request = WebRequest.Create(loginServiceUri);
                WebResponse response = request.GetResponse();
                Stream stream = response.GetResponseStream();
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(stream);
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xDoc.NameTable);
                nsmgr.AddNamespace("bing", "http://schemas.microsoft.com/search/local/ws/rest/v1");

                XmlNode root = xDoc.SelectSingleNode("bing:Response", nsmgr);
                XmlNode imageUrlElement = root.SelectSingleNode("bing:ResourceSets/bing:ResourceSet/bing:Resources/bing:ImageryMetadata/bing:ImageUrl", nsmgr);
                XmlNodeList subdomainsElement
                    = root.SelectNodes("bing:ResourceSets/bing:ResourceSet/bing:Resources/bing:ImageryMetadata/bing:ImageUrlSubdomains/bing:string", nsmgr);

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