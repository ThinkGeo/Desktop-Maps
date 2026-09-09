using System;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>The two constants every sample shares.</summary>
    public static class SampleShared
    {
        /// <summary>The ThinkGeo Cloud v3 apiKey the gallery demos with (the classic
        /// ClientId/ClientSecret pair is a different credential and does not open v3).</summary>
        public const string CloudApiKey = "SGjwpxF60knqZi3R0A9RRE3GqiuRA1LsnxHMot7rJ58~";

        /// <summary>Shapefile path next to the executable, however the app was launched.</summary>
        public static string Shapefile(string file) =>
            System.IO.Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", file);
    }
}
