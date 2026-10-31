using System;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>The three things every sample shares.</summary>
    public static class SampleShared
    {
        /// <summary>The ThinkGeo Cloud v3 apiKey the gallery demos with (the classic
        /// ClientId/ClientSecret pair is a different credential and does not open v3).</summary>
        public const string CloudApiKey = "SGjwpxF60knqZi3R0A9RRE3GqiuRA1LsnxHMot7rJ58~";

        /// <summary>Shapefile path next to the executable, however the app was launched.</summary>
        public static string Shapefile(string file) =>
            System.IO.Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", file);

        /// <summary>Where the ThinkGeo Cloud vector tiles are kept between runs: a tile source built
        /// in code has no cache unless it is given one, and a world view is dozens of HTTPS round trips.</summary>
        public static string VectorTileCacheFolder =>
            System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ThinkGeo", "HowDoI", "VectorTileCache");
    }
}
