using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.MapSuite.WpfDesktopEdition;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    /// <summary>
    /// Interaction logic for OverlaySwitcher.xaml
    /// </summary>
    public partial class OverlaySwitcher : UserControl
    {
        public event EventHandler<OverlaySwitchedEventArgs> OverlaySwitched;

        private WpfMap wpfMap;

        private Overlay checkedOverlay;

        public OverlaySwitcher()
            : this(null)
        {
        }

        public OverlaySwitcher(WpfMap wpfMap)
        {
            InitializeComponent();
            this.wpfMap = wpfMap;
        }

        public WpfMap WpfMap
        {
            get { return wpfMap; }
            set { wpfMap = value; }
        }

        public string CollapseImageUri { get; set; }

        public string ExpandImageUri { get; set; }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BindingOverlays();
            collapseExpandButton.Content = new Uri(ExpandImageUri, UriKind.RelativeOrAbsolute);
        }

        private void BindingOverlays(Overlay defaultOverlay = null)
        {
            Dictionary<Overlay, bool> overlays = new Dictionary<Overlay, bool>();
            if (wpfMap != null)
            {
                overlays = wpfMap.Overlays.Where(o => o.IsBase).ToDictionary(o => o, o => o.IsVisible);
                itemsControl.ItemsSource = overlays;
                if (defaultOverlay != null)
                {
                    Overlay checkedOverlayKey = overlays.FirstOrDefault(o => o.Value).Key;
                    overlays[checkedOverlayKey] = false;
                    overlays[defaultOverlay] = true;
                }
            }
        }

        private void OverlayChecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                KeyValuePair<Overlay, bool> keyValuePair = (KeyValuePair<Overlay, bool>)((RadioButton)sender).DataContext;
                var overlaySwitchedEventArgs = new OverlaySwitchedEventArgs(keyValuePair.Key);
                OnOverlaySwitched(overlaySwitchedEventArgs);
                if (overlaySwitchedEventArgs.IsCancel)
                {
                    BindingOverlays(checkedOverlay);
                }
                else
                {
                    checkedOverlay = keyValuePair.Key;
                    foreach (var overlay in wpfMap.Overlays)
                    {
                        if (overlay.IsBase)
                        {
                            overlay.IsVisible = checkedOverlay == overlay;
                        }
                    }
                    wpfMap.Refresh();
                }
            }
        }

        protected virtual void OnOverlaySwitched(OverlaySwitchedEventArgs e)
        {
            EventHandler<OverlaySwitchedEventArgs> handler = OverlaySwitched;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void CollaspeExpandOverlaySwitcherClick(object sender, RoutedEventArgs e)
        {
            if (borderPanel.Visibility == Visibility.Visible)
            {
                borderPanel.Visibility = Visibility.Collapsed;
                ((Button)sender).Content = new Uri(CollapseImageUri, UriKind.RelativeOrAbsolute);
            }
            else
            {
                borderPanel.Visibility = Visibility.Visible;
                ((Button)sender).Content = new Uri(ExpandImageUri, UriKind.RelativeOrAbsolute);
            }
        }
    }
}
