using System;
using System.Windows.Forms;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
#if NET6_0_OR_GREATER
            // only exists on WinForms .NET 6+
            Application.SetHighDpiMode(HighDpiMode.PerMonitorV2);
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Samples());
        }
    }
}