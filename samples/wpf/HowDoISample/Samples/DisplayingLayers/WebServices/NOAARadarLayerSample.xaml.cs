﻿using System;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// TODO
    /// </summary>
    public partial class NOAARadarLayerSample : UserControl, IDisposable
    {
        public NOAARadarLayerSample()
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