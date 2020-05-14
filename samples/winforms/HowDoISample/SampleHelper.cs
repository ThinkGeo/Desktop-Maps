using System.Collections.Generic;
using System.IO;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    internal class SampleHelper
    {// This ThinkGeo Cloud test key is exclusively for demonstration purposes and is limited to requesting base map 
        // tiles only. Do not use it in your own applications, as it may be restricted or disabled without prior notice. 
        // Please visit https://cloud.thinkgeo.com to create a free key for your own use.
        public static string ThinkGeoCloudId { get; } = "9ap16imkD_V7fsvDW9I8r8ULxgAB50BX_BnafMEBcKg~";
        public static string ThinkGeoCloudSecret { get; } = "vtVao9zAcOj00UlGcK7U-efLANfeJKzlPuDB9nw7Bp4K4UxU_PdRDg~~";
        static SampleHelper()
        {
            Root = @"../../../../../SampleData";
        }

        public static string Root { get; set; }

        public static string Get(string relPath)
        {
            DataFormat format = DataFormat.Unknown;
            switch (Path.GetExtension(relPath).ToLowerInvariant())
            {
                case ".tif":
                    format = DataFormat.GeoTiff;
                    break;
                case ".shx":
                case ".idx":
                case ".ids":
                case ".prj":
                case ".dbf":
                case ".shp":
                    format = DataFormat.Shapefile;
                    break;
                case ".png":
                case ".jpg":
                    format = DataFormat.Images;
                    break;
                case ".mdb":
                    format = DataFormat.PersonalGeoDatabase;
                    break;
                case ".sdf":
                    format = DataFormat.Sdf;
                    break;
                case ".sid":
                    format = DataFormat.MrSid;
                    break;
                case ".tab":
                    format = DataFormat.Tab;
                    break;
                case ".ecw":
                    format = DataFormat.Ecw;
                    break;
                case ".grd":
                    format = DataFormat.GridFile;
                    break;
                case ".mbtiles":
                    format = DataFormat.Mbtiles;
                    break;
                case ".json":
                    format = DataFormat.Json;
                    break;
                case ".ttf":
                    format = DataFormat.Font;
                    break;
                case ".csv":
                    format = DataFormat.Csv;
                    break;
                case ".gpx":
                    format = DataFormat.Gpx;
                    break;
                case ".tgeo":
                    format = DataFormat.TinyGeo;
                    break;
                case ".dem":
                    format = DataFormat.UsgsDem;
                    break;
            }

            return Get(relPath, format);
        }

        public static string Get(string relPath, DataFormat dataFormat)
        {
            return $"{Root}/{dataFormat.ToString()}/{relPath}";
        }
    }

    public enum DataFormat
    {
        Unknown,
        FileGeoDatabase,
        GeoCoding,
        GeoTiff,
        Gif,
        Gpx,
        Jpg,
        MrSid,
        S57,
        Shapefile,
        Tab,
        TinyGeo,
        Images,
        PersonalGeoDatabase,
        Sdf,
        Ecw,
        GridFile,
        Mbtiles,
        Json,
        Font,
        Csv,
        UsgsDem
    }


    public class MenuModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public List<MenuModel> Children { get; set; }
    }
}
