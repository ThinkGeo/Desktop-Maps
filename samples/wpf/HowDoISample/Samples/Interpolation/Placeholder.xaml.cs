using System;
using System.Windows;

namespace ThinkGeo.UI.Wpf.HowDoI.Interpolation
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class Placeholder : IDisposable
    {
        public Placeholder()
        {
            InitializeComponent();
        }

        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}