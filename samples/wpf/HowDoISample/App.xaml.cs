using System.Windows;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    public partial class App : Application
    {
        /// <summary>
        /// <c>--export-catalog &lt;folder&gt;</c> writes the catalog as <c>samples.json</c>
        /// into the gallery's folder and exits: the file the documentation server
        /// and AI assistants read, so they find a sample by the same titles, API
        /// names and aliases the gallery's own search does.
        /// </summary>
        protected override void OnStartup(StartupEventArgs e)
        {
            if (e.Args.Length == 2 && e.Args[0] == "--export-catalog")
            {
                Shutdown(SampleCatalogExport.Write(e.Args[1]));
                return;
            }

            base.OnStartup(e);
            new MainWindow().Show();
        }
    }
}
