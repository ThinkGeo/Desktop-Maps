using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using ThinkGeo.MapSuite.EarthquakeStatistics.Properties;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public class CollapsiblePanel : Panel
    {
        private int lineWidth;
        private int panelWidth;
        private bool isCollapsible;
        private Size collapsibleBoxSize;
        private PictureBox pictureCollapsibleBox;

        public event EventHandler PanelCollapseButtonClick;

        public CollapsiblePanel()
        {
            lineWidth = 5;
            isCollapsible = false;
            collapsibleBoxSize = new Size(12, 110);

            pictureCollapsibleBox = new PictureBox();
            pictureCollapsibleBox.Cursor = Cursors.Hand;
            pictureCollapsibleBox.BackColor = Color.Transparent;
            pictureCollapsibleBox.Location = new Point(Width - lineWidth - collapsibleBoxSize.Width, Height / 2 - collapsibleBoxSize.Height / 2);
            pictureCollapsibleBox.Size = collapsibleBoxSize;
            pictureCollapsibleBox.Image = GetCollapsibleImage();
            pictureCollapsibleBox.Click += PictureCollapsibleBox_Click;
            pictureCollapsibleBox.MouseEnter += PictureCollapsibleBox_MouseEnter;
            pictureCollapsibleBox.MouseLeave += PictureCollapsibleBox_MouseLeave;
            Controls.Add(pictureCollapsibleBox);

            Width = lineWidth + panelWidth + pictureCollapsibleBox.Width;
            Resize += CollapsiblePanel_Resize;
        }

        public int LineWidth
        {
            get { return lineWidth; }
            set { lineWidth = value; }
        }

        public int PanelWidth
        {
            get { return panelWidth; }
            set { panelWidth = value; }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Height > 0)
            {
                Rectangle drawingRectangle = new Rectangle(Width - lineWidth, 0, lineWidth, Height);
                LinearGradientBrush myBrush = new LinearGradientBrush(drawingRectangle, Color.Gray, Color.White, LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(myBrush, drawingRectangle);
            }
        }

        protected void OnPanelCollapseButtonClick(EventArgs e)
        {
            EventHandler handler = PanelCollapseButtonClick;
            if (handler != null) handler(this, e);
        }

        private void CollapsiblePanel_Resize(object sender, EventArgs e)
        {
            pictureCollapsibleBox.Location = new Point(Width - lineWidth - collapsibleBoxSize.Width, Height / 2 - collapsibleBoxSize.Height / 2);
        }

        private Bitmap GetCollapsibleImage()
        {
            if (pictureCollapsibleBox.Image != null)
            {
                pictureCollapsibleBox.Image.Dispose();
            }

            Bitmap bitmap = new Bitmap(pictureCollapsibleBox.Size.Width, pictureCollapsibleBox.Size.Height);

            using (Graphics g = Graphics.FromImage(bitmap))
            {
                Size imageSize = Resources.collapse.Size;
                int x = (pictureCollapsibleBox.Width - imageSize.Width) / 2;
                int y = (pictureCollapsibleBox.Height - imageSize.Height) / 2;
                g.DrawImage(isCollapsible ? Resources.expand : Resources.collapse, x, y);
            }

            return bitmap;
        }

        private void PictureCollapsibleBox_Click(object sender, EventArgs e)
        {
            isCollapsible = !isCollapsible;
            pictureCollapsibleBox.Image = GetCollapsibleImage();

            foreach (Control item in Controls.OfType<Control>().Where(c => c != pictureCollapsibleBox))
            {
                item.Visible = !isCollapsible;
            }

            Width = isCollapsible ? lineWidth + collapsibleBoxSize.Width : panelWidth + lineWidth + collapsibleBoxSize.Width;
            OnPanelCollapseButtonClick(e);

            Refresh();
        }

        private void PictureCollapsibleBox_MouseEnter(object sender, EventArgs e)
        {
            pictureCollapsibleBox.BackColor = Color.FromArgb(150, 4, 60, 153);
        }

        private void PictureCollapsibleBox_MouseLeave(object sender, EventArgs e)
        {
            pictureCollapsibleBox.BackColor = Color.Transparent;
        }
    }
}