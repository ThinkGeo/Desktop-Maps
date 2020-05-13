using System.Configuration;
using System.IO;
using ThinkGeo.MapSuite.Portable;

namespace Geocoding
{
    internal static class SampleHelper
    {
        private static string dataRootPath = Path.GetFullPath(ConfigurationManager.AppSettings["RootDirectory"]);

        public static string GetDataPath(string path)
        {
            return Path.Combine(dataRootPath, path);
        }
    }
}



