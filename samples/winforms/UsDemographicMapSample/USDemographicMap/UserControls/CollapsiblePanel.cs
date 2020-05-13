using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ThinkGeo.MapSuite.USDemographicMap.Properties;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    // This Panel is for the collapsible panel on the left. 
    public class CollapsiblePanel : Panel
    {
        private int lineWidth;
        private int panelWidth;
        private bool isCollapsible;
        private Size collapsibleBoxSize;
        private PictureBox pictureCollapsibleBox;

        public event EventHandler<EventArgs> PanelCollapseButtonClick;

        public CollapsiblePanel()
        {
            lineWidth = 5;
            collapsibleBoxSize = new Size(12, 110);

            pictureCollapsibleBox = new PictureBox();
            pictureCollapsibleBox.BackColor = Color.Transparent;
            pictureCollapsibleBox.Location = new Point(Width - lineWidth - collapsibleBoxSize.Width, this.Height / 2 - collapsibleBoxSize.Height / 2);
            pictureCollapsibleBox.Size = collapsibleBoxSize;
            pictureCollapsibleBox.Click += PictureCollapsibleBox_Click;
            pictureCollapsibleBox.MouseEnter += PictureCollapsibleBox_MouseEnter;
            pictureCollapsibleBox.MouseLeave += PictureCollapsibleBox_MouseLeave;
            pictureCollapsibleBox.Image = GetCollapsibleImage();

            Width = lineWidth + panelWidth + pictureCollapsibleBox.Width;
            Controls.Add(pictureCollapsibleBox);
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

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                Size imageSize = Resources.collapse.Size;
                int x = (pictureCollapsibleBox.Width - imageSize.Width) / 2;
                int y = (pictureCollapsibleBox.Height - imageSize.Height) / 2;
                graphics.DrawImage(isCollapsible ? Resources.expand : Resources.collapse, x, y);
            }

            return bitmap;
        }

        private void PictureCollapsibleBox_Click(object sender, EventArgs e)
        {
            isCollapsible = !isCollapsible;
            pictureCollapsibleBox.Image = GetCollapsibleImage();

            foreach (Control item in this.Controls)
            {
                if (item != pictureCollapsibleBox)
                {
                    item.Visible = !isCollapsible;
                }
            }

            Width = isCollapsible ? lineWidth + collapsibleBoxSize.Width : panelWidth + lineWidth + collapsibleBoxSize.Width;
            Refresh();

            OnPanelCollapseButtonClick();
        }

        private void PictureCollapsibleBox_MouseEnter(object sender, EventArgs e)
        {
            pictureCollapsibleBox.BackColor = Color.FromArgb(150, 4, 60, 153);
        }

        private void PictureCollapsibleBox_MouseLeave(object sender, EventArgs e)
        {
            pictureCollapsibleBox.BackColor = Color.Transparent;
        }

        private void OnPanelCollapseButtonClick()
        {
            if (PanelCollapseButtonClick != null)
            {
                PanelCollapseButtonClick(this, new EventArgs());
            }
        }
    }
}
