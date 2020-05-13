using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.MapSuite.WpfDesktopEdition;

namespace ThinkGeo.MapSuite.SiteSelection
{
    /// <summary>
    /// Interaction logic for OverlaySwitcher.xaml
    /// </summary>
    public partial class OverlaySwitcher : UserControl
    {
        private static readonly Uri expandImageUri = new Uri("/Image/switcher_minimize.png", UriKind.Relative);
        private static readonly Uri collapseImageUri = new Uri("/Image/switcher_maxmize.png", UriKind.Relative);

        public static readonly DependencyProperty WpfMapProperty = DependencyProperty.Register("WpfMap", typeof(WpfMap), typeof(OverlaySwitcher), new PropertyMetadata(null));

        public event EventHandler<BaseOverlayChangedOverlaySwitcherEventArgs> BaseOverlayChanged;

        public OverlaySwitcher()
        {
            InitializeComponent();
        }

        public WpfMap WpfMap
        {
            get { return (WpfMap)GetValue(WpfMapProperty); }
            set { SetValue(WpfMapProperty, value); }
        }

        protected virtual void OnBaseOverlayChanged(BaseOverlayChangedOverlaySwitcherEventArgs e)
        {
            EventHandler<BaseOverlayChangedOverlaySwitcherEventArgs> handler = BaseOverlayChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void BindingOverlays(Overlay activeOverlay)
        {
            if (WpfMap == null) return;

            Dictionary<Overlay, bool> overlays = WpfMap.Overlays.Where(o => o.IsBase).ToDictionary(o => o, o => o.IsVisible);
            itemsControl.ItemsSource = overlays;
            if (activeOverlay != null && overlays.ContainsKey(activeOverlay))
            {
                Overlay checkedOverlayKey = overlays.FirstOrDefault(o => o.Value).Key;
                overlays[checkedOverlayKey] = false;
                overlays[activeOverlay] = true;
            }
        }

        private void CollaspeExpandOverlaySwitcherClick(object sender, RoutedEventArgs e)
        {
            if (borderPanel.Visibility == Visibility.Visible)
            {
                borderPanel.Visibility = Visibility.Collapsed;
                ((Button)sender).Content = collapseImageUri;
            }
            else
            {
                borderPanel.Visibility = Visibility.Visible;
                ((Button)sender).Content = expandImageUri;
            }
        }

        private void OverlayChecked(object sender, RoutedEventArgs e)
        {
            if (IsLoaded)
            {
                KeyValuePair<Overlay, bool> keyValuePair = (KeyValuePair<Overlay, bool>)((RadioButton)sender).DataContext;
                var overlaySwitchedEventArgs = new BaseOverlayChangedOverlaySwitcherEventArgs(keyValuePair.Key);
                OnBaseOverlayChanged(overlaySwitchedEventArgs);

                if (overlaySwitchedEventArgs.Cancel)
                {
                    BindingOverlays(WpfMap.Overlays.FirstOrDefault(o => o.IsBase && o.IsVisible));
                }
                else
                {
                    foreach (var overlay in WpfMap.Overlays.Where(o => o.IsBase))
                    {
                        overlay.IsVisible = keyValuePair.Key == overlay;
                    }

                    WpfMap.Refresh();
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BindingOverlays(null);
            collapseExpandButton.Content = expandImageUri;
        }
    }
}