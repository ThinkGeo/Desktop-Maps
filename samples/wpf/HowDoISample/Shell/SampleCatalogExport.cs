using System;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Writes <see cref="SampleCatalog"/> as <c>samples.json</c>, in the tree the
    /// other galleries' catalogs use: one node per group, one child per sample.
    /// </summary>
    internal static class SampleCatalogExport
    {
        /// <returns>The process exit code: 1 when the catalog has drifted from the
        /// code, and then nothing is written but the list of problems.</returns>
        internal static int Write(string galleryFolder)
        {
            var drift = SampleCatalog.FindDrift();
            if (drift.Count > 0)
            {
                File.WriteAllLines(Path.Combine(galleryFolder, "samples.drift.txt"), drift);
                return 1;
            }

            var files = Directory.GetFiles(galleryFolder, "*", SearchOption.AllDirectories)
                .Where(f => (f.EndsWith(".cs", StringComparison.Ordinal) || f.EndsWith(".xaml", StringComparison.Ordinal))
                            && !f.Contains(Path.DirectorySeparatorChar + "obj" + Path.DirectorySeparatorChar)
                            && !f.Contains(Path.DirectorySeparatorChar + "bin" + Path.DirectorySeparatorChar))
                .ToLookup(Path.GetFileName, StringComparer.OrdinalIgnoreCase);

            var groups = SampleCatalog.All.GroupBy(e => e.Category).Select(g => new
            {
                title = g.Key,
                children = g.Select(e => new
                {
                    id = e.SampleType.Name,
                    title = e.Title,
                    description = e.Focus,
                    source = SourcePath(galleryFolder, files[e.SourceFile].Single()),
                    keyApi = e.KeyApi,
                    aliases = e.Aliases,
                    draws = e.DrawsLabel,
                    service = e.Service,
                }),
            });

            var json = JsonSerializer.Serialize(groups, new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            });
            File.WriteAllText(Path.Combine(galleryFolder, "samples.json"), json.Replace("\r\n", "\n").Replace("\n", "\r\n") + "\r\n");
            return 0;
        }

        /// <summary>The path from the gallery folder, forward slashes, no extensions:
        /// "Samples/Styling/HatchStyles" for every file of that sample.</summary>
        private static string SourcePath(string galleryFolder, string file)
        {
            var relative = Path.GetRelativePath(galleryFolder, file).Replace('\\', '/');
            var name = Path.GetFileName(relative);
            return relative.Substring(0, relative.Length - name.Length) + name.Substring(0, name.IndexOf('.'));
        }
    }
}
