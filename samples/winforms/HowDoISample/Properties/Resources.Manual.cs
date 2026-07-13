using System.Drawing;
using System.IO;

namespace ThinkGeo.UI.WinForms.HowDoI.Properties
{
    // File-based loaders for assets that used to be ResXFileRef entries in
    // Resources.resx. Removed from .resx so the project builds clean under
    // dotnet SDK 10 (which errors on net48 + non-string .resx). The PNG
    // files are CopyToOutputDirectory in HowDoI.csproj.
    internal partial class Resources
    {
        private static string ResourcePath(string name) =>
            Path.Combine(System.AppContext.BaseDirectory, "Resources", name);

        internal static Bitmap icon_globe_black => new Bitmap(ResourcePath("icon_globe_black.png"));
        internal static Bitmap icon_north_arrow => new Bitmap(ResourcePath("icon_north_arrow.png"));
        internal static Bitmap icon_north_arrow1 => new Bitmap(ResourcePath("icon_north_arrow.png"));
        internal static Bitmap logo => new Bitmap(ResourcePath("logo.png"));
    }
}
