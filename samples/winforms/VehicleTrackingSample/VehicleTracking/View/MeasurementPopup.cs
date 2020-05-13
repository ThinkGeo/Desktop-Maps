using System;
using System.Windows.Forms;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public sealed partial class MeasurementPopup : UserControl
    {
        public MeasurementPopup()
            : this(string.Empty)
        { }

        public MeasurementPopup(string content)
        {
            InitializeComponent();

            ContentLabel.Text = content;
            contentPanel.Width = ContentLabel.Width + 20;
            contentPanel.Height = ContentLabel.Height + 30;
            contentPanel.Top = 0;
            contentPanel.Left = 0;
            Width = contentPanel.Width;
            Height = contentPanel.Height + TrianglePictureBox.Height;
            ContentLabel.Left = (int)(contentPanel.Width * 0.5 - ContentLabel.Width * 0.5);
            ContentLabel.Top = (int)(contentPanel.Height * 0.5 - ContentLabel.Height * 0.5);

            TrianglePictureBox.Left = (int)(contentPanel.Width * 0.5 - TrianglePictureBox.Width * 0.5);
            TrianglePictureBox.Top = contentPanel.Height;

            ClosePictureBox.Left = contentPanel.Width - ClosePictureBox.Width - 2;
            ClosePictureBox.Top = 5;

            Refresh();
        }

        private void ClosingPopup(object sender, EventArgs e)
        {
            if (Parent != null)
            {
                Parent.Controls.Remove(this);
            }
        }
    }
}
