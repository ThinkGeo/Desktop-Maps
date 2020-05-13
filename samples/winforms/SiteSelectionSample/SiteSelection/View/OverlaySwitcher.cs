using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ThinkGeo.MapSuite.WinForms;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public partial class OverlaySwitcher : UserControl
    {
        public event EventHandler<OverlayChangedOverlaySwitcherEventArgs> OverlayChanged;

        private WinformsMap mapControl;
        private Collection<Overlay> overlays;
        private Overlay checkedOverlay;

        public OverlaySwitcher()
            : this(null, null)
        { }

        public OverlaySwitcher(IEnumerable<Overlay> overlays, WinformsMap map)
        {
            InitializeComponent();

            this.mapControl = map;
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

            int index = 0;
            int width = 0;
            int height = 0;
            foreach (var item in Overlays)
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
                if (width < radioButton.Width) width = radioButton.Width + 80;
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
                var overlaySwitchedEventArgs = new OverlayChangedOverlaySwitcherEventArgs((Overlay)radioButton.Tag);
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
                        foreach (RadioButton item in Controls.OfType<RadioButton>().Where(item => item.Tag == checkedOverlay))
                        {
                            item.Checked = true;
                            break;
                        }
                    }
                }
                else
                {
                    checkedOverlay = currentOverlay;
                    foreach (var overlay in Overlays)
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