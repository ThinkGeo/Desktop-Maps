using System;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>What every sample shares: the ThinkGeo Cloud credentials, where the data is, where the tile cache goes.</summary>
    public static class SampleShared
    {
        /// <summary>The ThinkGeo Cloud v3 apiKey the gallery demos with (the classic
        /// ClientId/ClientSecret pair is a different credential and does not open v3).</summary>
        public const string CloudApiKey = "SGjwpxF60knqZi3R0A9RRE3GqiuRA1LsnxHMot7rJ58~";

        /// <summary>The ThinkGeo Cloud ClientId and ClientSecret the Cloud services - geocoding, routing,
        /// elevation, colors - and the classic raster maps layers sign in with.</summary>
        public const string ClientId = "AOf22-EmFgIEeK4qkdx5HhwbkBjiRCmIDbIYuP8jWbc~";
        public const string ClientSecret = "xK0pbuywjaZx4sqauaga8DMlzZprz0qQSjLTow90EhBx5D8gFd2krw~~";

        /// <summary>Shapefile path next to the executable, however the app was launched.</summary>
        public static string Shapefile(string file) =>
            System.IO.Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", file);

        /// <summary>Where the ThinkGeo Cloud vector tiles are kept between runs: a tile source built
        /// in code has no cache unless it is given one, and a world view is dozens of HTTPS round trips.</summary>
        public static string VectorTileCacheFolder =>
            System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ThinkGeo", "HowDoI", "VectorTileCache");
    }
}
