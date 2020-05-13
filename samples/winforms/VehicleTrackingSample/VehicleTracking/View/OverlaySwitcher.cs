using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ThinkGeo.MapSuite.WinForms;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public partial class OverlaySwitcher : UserControl
    {
        private WinformsMap mapControl;
        private Overlay checkedOverlay;
        private Collection<Overlay> overlays;

        public event EventHandler<OverlayChangedOverlaySwitcherEventArgs> OverlayChanged;

        public OverlaySwitcher()
            : this(null, null)
        { }

        public OverlaySwitcher(IEnumerable<Overlay> overlays, WinformsMap map)
        {
            InitializeComponent();

            mapControl = map;
            this.overlays = new Collection<Overlay>();
            if (overlays != null)
            {
                foreach (var overlay in overlays)
                {
                    this.overlays.Add(overlay);
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
            foreach (Overlay overlay in Overlays.Take(5))
            {
                RadioButton radioButton = new RadioButton();
                radioButton.UseVisualStyleBackColor = true;
                radioButton.AutoSize = true;
                radioButton.TextAlign = ContentAlignment.MiddleLeft;
                radioButton.Tag = overlay;
                radioButton.Text = overlay.Name;
                radioButton.Checked = overlay.IsVisible;
                radioButton.CheckedChanged += OverlayRadioButton_CheckedChanged;
                height = 5 + index * 20;
                radioButton.Location = new Point(15, height);
                if (width < radioButton.Width)
                {
                    width = radioButton.Width + 100;
                }
                height += radioButton.Height + 10;
                Controls.Add(radioButton);
                index++;
            }
            Width = width;
            Height = height;
            Refresh();
        }

        private void OverlayRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                Overlay currentOverlay = (Overlay)radioButton.Tag;
                OverlayChangedOverlaySwitcherEventArgs overlaySwitchedEventArgs = new OverlayChangedOverlaySwitcherEventArgs(currentOverlay);
                OnOverlayChanged(overlaySwitchedEventArgs);
                if (overlaySwitchedEventArgs.Cancel)
                {
                    radioButton.Checked = false;
                    if (checkedOverlay == null)
                    {
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
                        foreach (RadioButton item in Controls.OfType<RadioButton>())
                        {
                            if (item.Tag == checkedOverlay)
                            {
                                item.Checked = true;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    checkedOverlay = currentOverlay;
                    foreach (Overlay overlay in overlays)
                    {
                        overlay.IsVisible = checkedOverlay == overlay;
                    }
                    mapControl.Refresh();
                }
            }
        }

        protected virtual void OnOverlayChanged(OverlayChangedOverlaySwitcherEventArgs e)
        {
            EventHandler<OverlayChangedOverlaySwitcherEventArgs> handler = OverlayChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}