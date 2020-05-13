using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public class LegendPanel : Panel
    {
        private string title;
        private FeatureLayer featureLayer;
        private ObservableCollection<MyLegendItem> legendItems;

        public LegendPanel()
        {
            legendItems = new ObservableCollection<MyLegendItem>();
        }

        public FeatureLayer FeatureLayer
        {
            get { return featureLayer; }
            set { featureLayer = value; }
        }

        public ObservableCollection<MyLegendItem> LegendItems
        {
            get { return legendItems; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public void Draw(double mapScale)
        {
            LegendItems.Clear();
            if (featureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Count > 0)
            {
                Style style = featureLayer.ZoomLevelSet.ZoomLevel01.CustomStyles[0];

                if (style is CustomDotDensityStyle)
                {
                    LegendItems.Add(new DotDensityLegendItem(5));
                    LegendItems.Add(new DotDensityLegendItem(10));
                    LegendItems.Add(new DotDensityLegendItem(20));
                    LegendItems.Add(new DotDensityLegendItem(40));
                }
                else if (style is PieZedGraphStyle)
                {
                    PieZedGraphStyle zedGraphStyle = (PieZedGraphStyle)style;

                    foreach (KeyValuePair<string, GeoColor> item in zedGraphStyle.PieSlices)
                    {
                        AreaStyle fillStyle = new AreaStyle(new GeoSolidBrush(item.Value));
                        PieChartLegendItem legendItem = new PieChartLegendItem(fillStyle, item.Key);
                        this.LegendItems.Add(legendItem);
                    }
                }
                else if (style is ValueCircleStyle)
                {
                    LegendItems.Add(new ValueCircleLegendItem(80));
                    LegendItems.Add(new ValueCircleLegendItem(16));
                    LegendItems.Add(new ValueCircleLegendItem(32));
                    LegendItems.Add(new ValueCircleLegendItem(64));
                    LegendItems.Add(new ValueCircleLegendItem(1280));
                }
                else if (style is ClassBreakStyle)
                {
                    ClassBreakStyle thematicStyle = (ClassBreakStyle)style;

                    for (int i = 0; i < thematicStyle.ClassBreaks.Count; i++)
                    {
                        ThematicLegendItem thematicLegendItem;
                        if (i < thematicStyle.ClassBreaks.Count - 1)
                        {
                            thematicLegendItem = new ThematicLegendItem(thematicStyle.ClassBreaks[i], thematicStyle.ClassBreaks[i + 1]);
                        }
                        else
                        {
                            thematicLegendItem = new ThematicLegendItem(thematicStyle.ClassBreaks[i], null);
                        }
                        LegendItems.Add(thematicLegendItem);
                    }
                }

                foreach (var legendItem in LegendItems)
                {
                    legendItem.Draw(style);
                }
            }
        }

        public void Refresh(FeatureLayer displayLayer, WinformsMap map)
        {
            FeatureLayer = displayLayer;
            Draw(map.CurrentScale);

            Controls.Clear();

            Label LegendTitleLabel = new Label();
            LegendTitleLabel.AutoSize = true;
            LegendTitleLabel.TextAlign = ContentAlignment.MiddleLeft;
            LegendTitleLabel.Location = new Point(5, 15);
            LegendTitleLabel.Text = Title;
            LegendTitleLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            Size textSize = TextRenderer.MeasureText(LegendTitleLabel.Text, LegendTitleLabel.Font);
            int maxPanelWidth = LegendTitleLabel.Location.X + textSize.Width;
            Controls.Add(LegendTitleLabel);

            int legendItemHeight = 55;
            foreach (var item in LegendItems.Where(i => i.Image != null && !string.IsNullOrEmpty(i.Title)))
            {
                PictureBox ItemImagePicture = new PictureBox();
                ItemImagePicture.Location = new Point(10, legendItemHeight);
                ItemImagePicture.Size = item.Image.Size;
                ItemImagePicture.Image = item.Image;

                Label TextLabel = new Label();
                TextLabel.AutoSize = true;
                TextLabel.Text = item.Title;
                TextLabel.Location = new Point(ItemImagePicture.Location.X + ItemImagePicture.Width + 10, legendItemHeight + ItemImagePicture.Height / 2 - 6);

                textSize = TextRenderer.MeasureText(TextLabel.Text, TextLabel.Font);
                maxPanelWidth = maxPanelWidth < TextLabel.Location.X + textSize.Width ? TextLabel.Location.X + textSize.Width : maxPanelWidth;
                legendItemHeight += ItemImagePicture.Height + 5;

                Controls.Add(ItemImagePicture);
                Controls.Add(TextLabel);
            }

            Location = new Point(Location.X, map.Height + map.Top - legendItemHeight - 9);
            Height = legendItemHeight + 10;
            Width = maxPanelWidth + 20;
        }
    }
}
