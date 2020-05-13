using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.SiteSelection
{
    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    [Serializable]
    public class OverlaySwitcher : MapTool
    {
        private Border borderPanel;
        private Overlay activeOverlay;
        private ItemsControl itemsControl;
        private Button collapseExpandButton;

        public event EventHandler<OverlayChangedOverlaySwitcherEventArgs> OverlayChanged;

        public OverlaySwitcher()
            : base(true)
        {
            DefaultStyleKey = typeof(OverlaySwitcher);

            Width = 230;
            Margin = new Thickness(0, 10, 10, 0);
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Right;
        }

        protected virtual void OnOverlayChanged(OverlayChangedOverlaySwitcherEventArgs e)
        {
            EventHandler<OverlayChangedOverlaySwitcherEventArgs> handler = OverlayChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            itemsControl = (ItemsControl)GetTemplateChild("itemsControl");
            borderPanel = (Border)GetTemplateChild("borderPanel");

            collapseExpandButton = (Button)GetTemplateChild("collapseExpandButton");
            collapseExpandButton.Click -= CollapseExpandButton_Click;
            collapseExpandButton.Click += CollapseExpandButton_Click;

            BindingOverlays();
        }

        private void BindingOverlays(Overlay defaultOverlay = null)
        {
            if (CurrentMap != null)
            {
                Action<OverlayItem> visibleChangedAction = new Action<OverlayItem>(item =>
                {
                    if (IsLoaded)
                    {
                        OverlayChangedOverlaySwitcherEventArgs overlaySwitchedEventArgs = new OverlayChangedOverlaySwitcherEventArgs(item.Overlay);
                        OnOverlayChanged(overlaySwitchedEventArgs);
                        if (overlaySwitchedEventArgs.Cancel)
                        {
                            BindingOverlays(activeOverlay);
                        }
                        else
                        {
                            activeOverlay = item.Overlay;
                            foreach (var overlay in CurrentMap.Overlays.Where(o => o.IsBase))
                            {
                                overlay.IsVisible = activeOverlay == overlay;
                            }
                            CurrentMap.Refresh();
                        }
                    }
                });

                OverlayItem[] overlayItems = CurrentMap.Overlays.Where(o => o.IsBase).Select(o => new OverlayItem(o, o.IsVisible, visibleChangedAction)).ToArray();
                itemsControl.ItemsSource = overlayItems;
                if (defaultOverlay != null)
                {
                    OverlayItem checkedOverlayItem = overlayItems.First(o => o.IsVisible);
                    OverlayItem newOverlayItem = overlayItems.First(o => o.Overlay == defaultOverlay);
                    checkedOverlayItem.IsVisible = false;
                    newOverlayItem.IsVisible = true;
                }
            }
        }

        private void CollapseExpandButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (borderPanel.Visibility == Visibility.Visible)
            {
                borderPanel.Visibility = Visibility.Collapsed;
                button.Content = new Uri("/Image/switcher_maxmize.png", UriKind.RelativeOrAbsolute);
            }
            else
            {
                borderPanel.Visibility = Visibility.Visible;
                button.Content = new Uri("/Image/switcher_minimize.png", UriKind.RelativeOrAbsolute);
            }
        }

        private class OverlayItem
        {
            private Overlay overlay;
            private bool isVisible;
            private Action<OverlayItem> command;

            public OverlayItem()
                : this(null, false, null)
            { }

            public OverlayItem(Overlay overlay, bool isVisible, Action<OverlayItem> command)
            {
                this.overlay = overlay;
                this.isVisible = isVisible;
                this.command = command;
            }

            public Action<OverlayItem> Command
            {
                get { return command; }
                set { command = value; }
            }

            public bool IsVisible
            {
                get { return isVisible; }
                set
                {
                    isVisible = value;
                    if (value && command != null)
                    {
                        command(this);
                    }
                }
            }

            public Overlay Overlay
            {
                get { return overlay; }
                set { overlay = value; }
            }
        }
    }
}