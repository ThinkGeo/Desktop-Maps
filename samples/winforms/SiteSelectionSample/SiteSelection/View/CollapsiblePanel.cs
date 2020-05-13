using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using ThinkGeo.MapSuite.SiteSelection.Properties;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public class CollapsiblePanel : Panel
    {
        private int lineWidth;
        private int panelWidth;
        private bool isCollapsible;
        private Size collapsibleBoxSize;
        private PictureBox collapsibleBoxPictureBox;

        public event EventHandler ToggleExpandedButtonClick;

        public CollapsiblePanel()
        {
            lineWidth = 5;
            isCollapsible = false;
            collapsibleBoxSize = new Size(12, 110);
            collapsibleBoxPictureBox = new PictureBox();
            collapsibleBoxPictureBox.Cursor = Cursors.Hand;
            collapsibleBoxPictureBox.BackColor = Color.Transparent;
            collapsibleBoxPictureBox.Location = new Point(Width - lineWidth - collapsibleBoxSize.Width, Height / 2 - collapsibleBoxSize.Height / 2);
            collapsibleBoxPictureBox.Size = collapsibleBoxSize;
            collapsibleBoxPictureBox.Click += CollapsibleBoxPictureBox_Click;
            collapsibleBoxPictureBox.MouseEnter += CollapsibleBoxPictureBox_MouseEnter;
            collapsibleBoxPictureBox.MouseLeave += CollapsibleBoxPictureBox_MouseLeave;
            collapsibleBoxPictureBox.Image = GetCollapsibleImage();
            Controls.Add(collapsibleBoxPictureBox);

            Width = lineWidth + panelWidth + collapsibleBoxPictureBox.Width;
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

            Rectangle drawingRectangle = new Rectangle(Width - lineWidth, 0, lineWidth, Height);
            LinearGradientBrush myBrush = new LinearGradientBrush(drawingRectangle, Color.Gray, Color.White, LinearGradientMode.Horizontal);
            e.Graphics.FillRectangle(myBrush, drawingRectangle);
        }

        protected void OnToggleExpandedButtonClick(EventArgs e)
        {
            EventHandler handler = ToggleExpandedButtonClick;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void CollapsiblePanel_Resize(object sender, EventArgs e)
        {
            collapsibleBoxPictureBox.Location = new Point(Width - lineWidth - collapsibleBoxSize.Width, Height / 2 - collapsibleBoxSize.Height / 2);
        }

        private Bitmap GetCollapsibleImage()
        {
            if (collapsibleBoxPictureBox.Image != null) collapsibleBoxPictureBox.Image.Dispose();

            Bitmap bitmap = new Bitmap(collapsibleBoxPictureBox.Size.Width, collapsibleBoxPictureBox.Size.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                Size imageSize = Resources.collapse.Size;
                int x = (collapsibleBoxPictureBox.Width - imageSize.Width) / 2;
                int y = (collapsibleBoxPictureBox.Height - imageSize.Height) / 2;
                g.DrawImage(isCollapsible ? Resources.expand : Resources.collapse, x, y);
            }

            return bitmap;
        }

        private void CollapsibleBoxPictureBox_Click(object sender, EventArgs e)
        {
            isCollapsible = !isCollapsible;
            collapsibleBoxPictureBox.Image = GetCollapsibleImage();

            foreach (var item in Controls.OfType<Control>().Where(c => c != collapsibleBoxPictureBox))
            {
                item.Visible = !isCollapsible;
            }

            Width = isCollapsible ? lineWidth + collapsibleBoxSize.Width : panelWidth + lineWidth + collapsibleBoxSize.Width;
            OnToggleExpandedButtonClick(e);

            Refresh();
        }

        private void CollapsibleBoxPictureBox_MouseEnter(object sender, EventArgs e)
        {
            collapsibleBoxPictureBox.BackColor = Color.FromArgb(150, 4, 60, 153);
        }

        private void CollapsibleBoxPictureBox_MouseLeave(object sender, EventArgs e)
        {
            collapsibleBoxPictureBox.BackColor = Color.Transparent;
        }
    }
}