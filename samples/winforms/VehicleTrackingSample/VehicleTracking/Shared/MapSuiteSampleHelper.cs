using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.VehicleTracking.Properties;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public static class MapSuiteSampleHelper
    {
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

        public static string GetBingMapsApplicationId()
        {
            string bingMapsKey = string.Empty;

            string exePath = Assembly.GetExecutingAssembly().Location;
            Configuration config = ConfigurationManager.OpenExeConfiguration(exePath);
            KeyValueConfigurationElement appMapKeySetting = config.AppSettings.Settings["BingMapKey"];
            if (appMapKeySetting != null)
            {
                bingMapsKey = appMapKeySetting.Value;
            }
            else
            {
                BingMapsApplicationIdPromptForm inputBingMapKeyWindow = new BingMapsApplicationIdPromptForm();
                inputBingMapKeyWindow.StartPosition = FormStartPosition.CenterParent;
                if (inputBingMapKeyWindow.ShowDialog() == DialogResult.OK && Validate(inputBingMapKeyWindow.ApplicationId))
                {
                    SaveBingMapsKey(inputBingMapKeyWindow.ApplicationId);
                    bingMapsKey = inputBingMapKeyWindow.ApplicationId;
                }
            }

            return bingMapsKey;
        }

        private static void SaveBingMapsKey(string bingMapsKey)
        {
            string exePath = Assembly.GetExecutingAssembly().Location;
            Configuration config = ConfigurationManager.OpenExeConfiguration(exePath);
            config.AppSettings.Settings.Add("BingMapKey", bingMapsKey);
            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }

        private static bool Validate(string BingMapsKey)
        {
            bool result = false;
            const string loginServiceTemplate = "http://dev.virtualearth.net/REST/v1/Imagery/Metadata/{0}?&incl=ImageryProviders&o=xml&key={1}";

            try
            {
                string loginServiceUri = string.Format(CultureInfo.InvariantCulture, loginServiceTemplate, WinForms.BingMapsMapType.Road, BingMapsKey);

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
                    if (root != null)
                    {
                        XmlNode imageUrlElement = root.SelectSingleNode("bing:ResourceSets/bing:ResourceSet/bing:Resources/bing:ImageryMetadata/bing:ImageUrl", nsmgr);
                        XmlNodeList subdomainsElement = root.SelectNodes("bing:ResourceSets/bing:ResourceSet/bing:Resources/bing:ImageryMetadata/bing:ImageUrlSubdomains/bing:string", nsmgr);
                        if (imageUrlElement != null && subdomainsElement != null)
                        {
                            result = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Resources.WarningText, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return result;
        }

        internal static Bitmap GetVehicleBitmap(string vehicleIconVirtualPath)
        {
            string fileName = Path.GetFileNameWithoutExtension(vehicleIconVirtualPath);
            switch (fileName)
            {
                case "Robert_vehicle":
                    return Resources.Robert_vehicle;

                case "Bob_vehicle":
                    return Resources.Bob_vehicle;

                case "Jim_vehicle":
                    return Resources.Jim_vehicle;

                case "Micheal_vehicle":
                    return Resources.Micheal_vehicle;

                case "Jenny_vehicle":
                    return Resources.Jenny_vehicle;

                case "Jeremy_vehicle":
                default:
                    return Resources.Jeremy_vehicle;
            }
        }
    }
}