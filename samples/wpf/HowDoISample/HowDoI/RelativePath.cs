using System.IO;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public static class SampleHelper
    {
        // This ThinkGeo Cloud test key is exclusively for demonstration purposes and is limited to requesting base map 
        // tiles only. Do not use it in your own applications, as it may be restricted or disabled without prior notice. 
        // Please visit https://cloud.thinkgeo.com to create a free key for your own use.
        public static string ThinkGeoCloudId { get; } = "USlbIyO5uIMja2y0qoM21RRM6NBXUad4hjK3NBD6pD0~";
        public static string ThinkGeoCloudSecret { get; } = "f6OJsvCDDzmccnevX55nL7nXpPDXXKANe5cN6czVjCH0s8jhpCH-2A~~";
        static SampleHelper()
        {
            Root = @"../../../Data";
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
}
