using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ThinkGeo.MapSuite.USDemographicMap.Properties;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    // This is the data selector item on the left.
    // It contains StyleSelectorUserControls where user can pick different styles for displaying.
    public partial class DataSelectorUserControl : UserControl
    {
        private int headerHeight;
        private int completeHeight;
        private bool isActive;
        private Panel panel;
        private List<StyleSelectorUserControl> styleSelectors;

        public event EventHandler<StyleUpdatedDataSelectorUserControlEventArgs> StyleUpdated;

        public DataSelectorUserControl()
        {
            InitializeComponent();

            HeaderHeight = Height;
            Panel = new Panel();
            Panel.Location = new Point(0, HeaderHeight);
            Panel.BackColor = Color.White;
            Panel.Width = Width;

            styleSelectors = new List<StyleSelectorUserControl>();

            picPie.Enabled = false;
            picPie.Visible = false;
            isActive = false;

            ToolTip toolTipPie = new ToolTip();
            toolTipPie.SetToolTip(picPie, "Display pie charts on the map representing the breakdown of all data points selected below.");
        }

        protected Panel Panel
        {
            get { return panel; }
            set { panel = value; }
        }

        protected List<StyleSelectorUserControl> StyleSelectors
        {
            get { return styleSelectors; }
        }

        protected int HeaderHeight
        {
            get { return headerHeight; }
            set { headerHeight = value; }
        }

        protected int CompleteHeight
        {
            get { return completeHeight; }
            set { completeHeight = value; }
        }

        public Image Image
        {
            get { return picDataImage.Image; }
            set { picDataImage.Image = value; }
        }

        public bool IsActive
        {
            get { return isActive; }
        }

        public bool PieChartEnabled
        {
            get { return picPie.Enabled; }
            set { picPie.Enabled = value; }
        }

        public string Title
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        public void AddStyleSelector(StyleSelectorUserControl styleSelector)
        {
            this.StyleSelectors.Add(styleSelector);
            styleSelector.StyleUpdated += StyleSelectorUserControl_StyleUpdated;
            styleSelector.Location = new Point(styleSelector.Margin.Left, styleSelector.Margin.Top + CompleteHeight);
            CompleteHeight += styleSelector.Height;
            Panel.Controls.Add(styleSelector);
        }

        public int GetStyleSelectorCount()
        {
            return StyleSelectors.Count;
        }

        public void SetActiveStatus(bool active)
        {
            if (isActive != active)
            {
                picPie.Visible = active;
                isActive = active;

                Panel.Height = CompleteHeight;
                if (isActive)
                {
                    this.Controls.Add(Panel);
                    this.Height = HeaderHeight + Panel.Height;
                }
                else
                {
                    this.Controls.Remove(Panel);
                    this.Height = HeaderHeight;
                }
            }
        }

        protected void RaisePieChartUpdatedEvent()
        {
            StyleUpdatedDataSelectorUserControlEventArgs eventArgs = new StyleUpdatedDataSelectorUserControlEventArgs(DemographicStyleBuilderType.PieChart);
            foreach (StyleSelectorUserControl item in StyleSelectors)
            {
                item.IsVisible = true;
                if (item.WithinPieChart)
                {
                    eventArgs.ActivatedStyleSelectors.Add(item);
                }
            }

            if (eventArgs.ActivatedStyleSelectors.Count >= 2)
            {
                picPie.Enabled = true;
                OnStyleUpdated(eventArgs);
            }
        }

        protected virtual void OnStyleUpdated(StyleUpdatedDataSelectorUserControlEventArgs e)
        {
            EventHandler<StyleUpdatedDataSelectorUserControlEventArgs> handler = StyleUpdated;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void StyleSelectorUserControl_StyleUpdated(object sender, StyleUpdatedStyleSelectorUserControlEventArgs e)
        {
            if (e.DemographicStyleBuilderType != DemographicStyleBuilderType.PieChart)
            {
                StyleUpdatedDataSelectorUserControlEventArgs eventArgs = new StyleUpdatedDataSelectorUserControlEventArgs(e.DemographicStyleBuilderType);
                eventArgs.ActivatedStyleSelectors.Add((StyleSelectorUserControl)sender);
                OnStyleUpdated(eventArgs);
            }
            else
            {
                RaisePieChartUpdatedEvent();
            }
        }

        private void PieChartLegendItem_Click(object sender, EventArgs e)
        {
            RaisePieChartUpdatedEvent();
        }

        private void PictureBoxPie_EnabledChanged(object sender, EventArgs e)
        {
            picPie.Image = picPie.Enabled ? Resources.pie : Resources.pie_Disable;
        }

        private void SubControls_Click(object sender, EventArgs e)
        {
            OnClick(e);
        }

        private void DataCategoryUserControl_MouseEnter(object sender, EventArgs e)
        {
            BackColor = Color.FromArgb(100, Color.FromArgb(180, 180, 180));
            Focus();
        }

        private void DataCategoryUserControl_MouseLeave(object sender, EventArgs e)
        {
            BackColor = Color.Transparent;
        }

        private void PieChartLegendItem_MouseEnter(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.FromArgb(80, Color.Red);
            BackColor = Color.FromArgb(100, Color.FromArgb(180, 180, 180));
            Focus();
        }

        private void PieChartLegendItem_MouseLeave(object sender, EventArgs e)
        {
            ((PictureBox)sender).BackColor = Color.Transparent;
            BackColor = Color.Transparent;
        }
    }
}