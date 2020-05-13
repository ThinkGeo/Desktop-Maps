using System;
using System.Drawing;
using System.Windows.Forms;
using ThinkGeo.MapSuite.USDemographicMap.Properties;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    // This is a StyleSelector within each DataSelector, user can pick different style from here. 
    public partial class StyleSelectorUserControl : UserControl
    {
        private string columnName;
        private string legendTitle;

        public event EventHandler<StyleUpdatedStyleSelectorUserControlEventArgs> StyleUpdated;

        public StyleSelectorUserControl()
            : base()
        {
            InitializeComponent();

            ToolTip legendItemToolTip = new ToolTip();
            legendItemToolTip.SetToolTip(picDotDensity, "Present the data with Dot Density.");
            legendItemToolTip.SetToolTip(picValueCircle, "Present the data in Value Circles.");
            legendItemToolTip.SetToolTip(picThematic, "Present the data in Thematic Colors.");

            ToolTip isSelectedToolTip = new ToolTip();
            isSelectedToolTip.SetToolTip(chkIsSelected, "Select to include this data point in the map's pie charts.");

            picDotDensity.Image = Resources.DotDensity;
            picThematic.Image = Resources.Thematic;
            picValueCircle.Image = Resources.ValueCircle;
            picDotDensity.Cursor = Cursors.Hand;
            picThematic.Cursor = Cursors.Hand;
            picValueCircle.Cursor = Cursors.Hand;
            chkIsSelected.Checked = true;
            chkIsSelected.Visible = false;
        }

        public string Alias
        {
            get { return lblTitle.Text; }
            set { lblTitle.Text = value; }
        }

        public bool IsVisible
        {
            get { return chkIsSelected.Visible; }
            set { chkIsSelected.Visible = value; }
        }

        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }

        public bool WithinPieChart
        {
            get { return chkIsSelected.Checked; }
            set { chkIsSelected.Checked = value; }
        }

        public string LegendTitle
        {
            get { return legendTitle; }
            set { legendTitle = value; }
        }

        private void SelectedCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            OnStyleUpdated(new StyleUpdatedStyleSelectorUserControlEventArgs(DemographicStyleBuilderType.PieChart));
        }

        private void pictureBox_Click(object sender, System.EventArgs e)
        {
            DemographicStyleBuilderType StyleType = DemographicStyleBuilderType.DotDensity;
            if (sender == picDotDensity)
            {
                StyleType = DemographicStyleBuilderType.DotDensity;
            }
            else if (sender == picThematic)
            {
                StyleType = DemographicStyleBuilderType.Thematic;
            }
            else if (sender == picValueCircle)
            {
                StyleType = DemographicStyleBuilderType.ValueCircle;
            }

            StyleUpdatedStyleSelectorUserControlEventArgs eventArgs = new StyleUpdatedStyleSelectorUserControlEventArgs(StyleType);
            chkIsSelected.Checked = true;
            OnStyleUpdated(eventArgs);
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox != null)
            {
                pictureBox.BackColor = Color.FromArgb(50, Color.Red);
            }
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            if (pictureBox != null)
            {
                pictureBox.BackColor = Color.Transparent;
            }
        }

        private void OnStyleUpdated(StyleUpdatedStyleSelectorUserControlEventArgs e)
        {
            EventHandler<StyleUpdatedStyleSelectorUserControlEventArgs> handler = StyleUpdated;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}