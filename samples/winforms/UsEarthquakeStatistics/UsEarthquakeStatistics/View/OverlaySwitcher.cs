using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ThinkGeo.MapSuite.WinForms;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public partial class OverlaySwitcher : UserControl
    {
        public event EventHandler<OverlayChangedOverlaySwitcherEventArgs> OverlayChanged;

        private WinformsMap mapControl;
        private Collection<Overlay> overlays;

        public OverlaySwitcher()
            : this(null, null)
        { }

        public OverlaySwitcher(IEnumerable<Overlay> overlays, WinformsMap map)
        {
            this.InitializeComponent();

            this.mapControl = map;
            this.overlays = new Collection<Overlay>();

            if (overlays != null)
            {
                foreach (var item in overlays)
                {
                    this.overlays.Add(item);
                }
            }

            LoadUIs();
        }

        public WinformsMap MapControl
        {
            get { return mapControl; }
            set { mapControl = value; }
        }

        public Collection<Overlay> Overlays
        {
            get { return overlays; }
        }

        public void LoadUIs()
        {
            if (mapControl == null && overlays.Count == 0) return;

            int width = 0;
            int height = 0;
            int index = 0;
            foreach (Overlay item in Overlays)
            {
                RadioButton radioButton = new RadioButton();
                radioButton.UseVisualStyleBackColor = true;
                radioButton.AutoSize = true;
                radioButton.TextAlign = ContentAlignment.MiddleLeft;
                radioButton.Tag = item;
                radioButton.Text = item.Name;
                radioButton.Checked = item.IsVisible;
                radioButton.CheckedChanged += RadioButton_CheckedChanged;
                height = 5 + index * 20;
                radioButton.Location = new Point(15, height);
                if (width < radioButton.Width)
                {
                    width = radioButton.Width + 80;
                }
                height += radioButton.Height + 10;
                Controls.Add(radioButton);
                index++;
            }
            Width = width;
            Height = height;
            Refresh();
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                Overlay currentOverlay = (Overlay)radioButton.Tag;
                OverlayChangedOverlaySwitcherEventArgs overlaySwitchedEventArgs = new OverlayChangedOverlaySwitcherEventArgs((Overlay)radioButton.Tag);
                OnOverlaySwitched(overlaySwitchedEventArgs);

                if (overlaySwitchedEventArgs.Cancel)
                {
                    radioButton.Checked = false;
                    foreach (RadioButton item in Controls.OfType<RadioButton>())
                    {
                        Overlay tempOverlay = item.Tag as Overlay;
                        if (tempOverlay != null && tempOverlay.IsVisible)
                        {
                            item.Checked = true;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (Overlay overlay in Overlays)
                    {
                        overlay.IsVisible = currentOverlay == overlay;
                    }
                    mapControl.Refresh();
                }
            }
        }

        protected virtual void OnOverlaySwitched(OverlayChangedOverlaySwitcherEventArgs e)
        {
            EventHandler<OverlayChangedOverlaySwitcherEventArgs> handler = OverlayChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}