using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Drawing;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    // This is the color picking combobox.
    public class ColorComboBox : ComboBox
    {
        private Dictionary<string, Color> simpleColors;

        public ColorComboBox()
        {
            simpleColors = new Dictionary<string, Color>();
            DrawMode = DrawMode.OwnerDrawFixed;
            DropDownStyle = ComboBoxStyle.DropDownList;
            ItemHeight = 25;

            Type type = GeoColor.SimpleColors.GetType();
            PropertyInfo[] propertyInfos = type.GetProperties();

            foreach (PropertyInfo propertypInfo in propertyInfos.Where(p => !p.Name.Equals("TRANSPARENT", StringComparison.InvariantCultureIgnoreCase)))
            {
                string name = propertypInfo.Name;
                object value = propertypInfo.GetValue(GeoColor.SimpleColors, null);
                if (value.GetType() == typeof(GeoColor))
                {
                    GeoColor geoColor = (GeoColor)value;
                    simpleColors.Add(name, Color.FromArgb(geoColor.RedComponent, geoColor.GreenComponent, geoColor.BlueComponent));
                    if (!Items.Contains(name))
                    {
                        Items.Add(name);
                    }
                }
            }
            Text = "Black";
        }

        public Color SelectedColor
        {
            get { return simpleColors[Text]; }
        }

        public Dictionary<string, Color> SimpleColors
        {
            get { return simpleColors; }
            set { simpleColors = value; }
        }

        public void SetSelectedColor(string key)
        {
            if (simpleColors.ContainsKey(key))
            {
                SelectedItem = key;
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Rectangle rect = e.Bounds;

            if (e.Index >= 0)
            {
                string colorName = Items[e.Index].ToString();
                Color c = simpleColors[colorName];
                using (Brush brush = new SolidBrush(c))
                {
                    Brush whiteBrush = new SolidBrush(Color.White);
                    e.Graphics.DrawRectangle(new Pen(Color.Black, 1), rect.X + 5, rect.Y + 2, rect.Width / 4, rect.Height - 4);
                    e.Graphics.FillRectangle(brush, new Rectangle(rect.X + 7, rect.Y + 4, rect.Width / 4 - 3, rect.Height - 7));
                    e.Graphics.DrawString(colorName, new Font("Segoe UI", 10), new SolidBrush(Color.Black), new PointF(rect.X + rect.Width / 4 + 10, rect.Y + 4));

                    whiteBrush.Dispose();
                }
            }
        }
    }
}