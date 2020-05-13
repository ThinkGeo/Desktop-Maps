using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace ThinkGeo.MapSuite.SiteSelection
{
    /// <summary>
    /// Interaction logic for Loading.xaml
    /// </summary>
    public partial class LoadingImageUserControl : UserControl
    {
        private Thread runLoadingThreading;
        private bool isActive;

        public LoadingImageUserControl()
        {
            InitializeComponent();
        }

        private void HandleLoaded(object sender, RoutedEventArgs e)
        {
            const double offset = Math.PI;
            const double step = Math.PI * 2 / 10.0;

            SetPosition(C0, offset, 0.0, step);
            SetPosition(C1, offset, 1.0, step);
            SetPosition(C2, offset, 2.0, step);
            SetPosition(C3, offset, 3.0, step);
            SetPosition(C4, offset, 4.0, step);
            SetPosition(C5, offset, 5.0, step);
            SetPosition(C6, offset, 6.0, step);
            SetPosition(C7, offset, 7.0, step);
            SetPosition(C8, offset, 8.0, step);
        }

        private void HandleVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            bool isVisible = (bool)e.NewValue;
            if (isVisible) Start();
            else Stop();

        }

        private void Start()
        {
            runLoadingThreading = new Thread(LoadingActive);
            runLoadingThreading.Start();
            isActive = true;
        }

        private void Stop()
        {
            isActive = false;
        }

        private void LoadingActive()
        {
            while (isActive)
            {
                SpinnerRotate.Dispatcher.BeginInvoke(new Action(() =>
                {
                    SpinnerRotate.Angle = (SpinnerRotate.Angle + 36) % 360;
                }));
                Thread.Sleep(80);
            }
        }

        private static void SetPosition(Ellipse ellipse, double offset,
            double posOffSet, double step)
        {
            ellipse.SetValue(Canvas.LeftProperty, 50.0 + Math.Sin(offset + posOffSet * step) * 50.0);
            ellipse.SetValue(Canvas.TopProperty, 50.0 + Math.Cos(offset + posOffSet * step) * 50.0);
        }
    }
}