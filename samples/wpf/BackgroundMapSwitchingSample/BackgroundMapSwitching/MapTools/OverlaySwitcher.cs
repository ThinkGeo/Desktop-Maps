using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.MapSuite.Wpf;

namespace ThinkGeo.MapSuite.BackgroundMapSwitching
{
    [ToolboxItem(false)]
    [DesignTimeVisible(false)]
    [Serializable]
    public class OverlaySwitcher : MapTool
    {
        public event EventHandler<SelectedOverlayEventArgs> SelectedOverlay;

        private Border mainPanel;
        private Overlay activeOverlay;
        private ItemsControl itemsControl;

        public OverlaySwitcher()
            : base(true)
        {
            DefaultStyleKey = typeof(OverlaySwitcher);

            Width = 230;
            Margin = new Thickness(0, 10, 10, 0);
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Right;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            itemsControl = (ItemsControl)GetTemplateChild("itemsControl");
            mainPanel = (Border)GetTemplateChild("borderPanel");

            BindingOverlays();
        }

        private void BindingOverlays(Overlay defaultOverlay = null)
        {
            if (CurrentMap != null)
            {
                OverlayItem[] overlayItems = CurrentMap.Overlays.Where(o => o.IsBase).Select(o => new OverlayItem(o, o.IsVisible, ChangeBaseOverlay)).ToArray();
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

        private void ChangeBaseOverlay(OverlayItem item)
        {
            if (IsLoaded)
            {
                activeOverlay = item.Overlay;
                foreach (Overlay overlay in CurrentMap.Overlays.Where(o => o.IsBase))
                {
                    overlay.IsVisible = activeOverlay == overlay;
                }
                OnChangingOverlay(new SelectedOverlayEventArgs(activeOverlay));
                CurrentMap.Refresh();
            }
        }

        private void OnChangingOverlay(SelectedOverlayEventArgs e)
        {
            EventHandler<SelectedOverlayEventArgs> handler = SelectedOverlay;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private class OverlayItem
        {
            private Overlay overlay;
            private bool isVisible;
            private Action<OverlayItem> command;

            public OverlayItem(Overlay overlay, bool isVisible, Action<OverlayItem> command)
            {
                this.overlay = overlay;
                this.isVisible = isVisible;
                this.command = command;
            }

            public bool IsVisible
            {
                get { return isVisible; }
                set
                {
                    isVisible = value;
                    if (value && command != null) command(this);
                }
            }

            public Overlay Overlay
            {
                get { return overlay; }
            }
        }
    }

    public class SelectedOverlayEventArgs : EventArgs
    {
        public Overlay Overlay { get; }

        public SelectedOverlayEventArgs(Overlay overlay)
        {
            Overlay = overlay;
        }
    }
}