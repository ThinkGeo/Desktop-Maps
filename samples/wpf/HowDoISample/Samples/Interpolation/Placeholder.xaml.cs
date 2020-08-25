using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Interpolation
{
    /// <summary>
    /// Interaction logic for Placeholder.xaml
    /// </summary>
    public partial class Placeholder : UserControl, IDisposable
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
            mapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}
