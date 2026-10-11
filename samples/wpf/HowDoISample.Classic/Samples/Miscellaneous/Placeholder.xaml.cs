using System;
using System.Windows;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class Placeholder : IDisposable
    {

        private bool _initialized;
        public Placeholder()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

        _initialized = true;
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Map.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}