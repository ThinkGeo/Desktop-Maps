using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public static class MapSuiteSampleHelper
    {
        public static string GetValueFromConfiguration(string key)
        {
            string exePath = Application.ResourceAssembly.Location;
            Configuration config = ConfigurationManager.OpenExeConfiguration(exePath);
            KeyValueConfigurationElement categoryList = config.AppSettings.Settings[key];
            return categoryList.Value;
        }

        public static T GetDataContext<T>(this object sender) where T : class
        {
            FrameworkElement frameworkElement = sender as FrameworkElement;
            if (frameworkElement != null)
            {
                return frameworkElement.DataContext as T;
            }
            return null;
        }

        public static object GetImageSourceFromNativeImage(object nativeImage)
        {
            object imageSource = nativeImage;
            Bitmap bitmap = nativeImage as Bitmap;
            if (bitmap != null)
            {
                MemoryStream memoryStream = new MemoryStream();
                bitmap.Save(memoryStream, ImageFormat.Png);
                memoryStream.Seek(0, SeekOrigin.Begin);
                imageSource = memoryStream;
            }

            return imageSource;
        }
    }
}
