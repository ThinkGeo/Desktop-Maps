using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.SiteSelection.Properties;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public class QueryResultItem
    {
        private string name;
        private string location;
        private Feature feature;
        private DataGridViewImageCell imageButtonCell;
        private DataGridViewTextBoxCell textCell;
        private DataGridViewTextBoxCell locationCell;

        public QueryResultItem()
            : this(null)
        { }

        public QueryResultItem(Feature feature)
            : base()
        {
            this.feature = feature;
            this.name = feature.ColumnValues.FirstOrDefault(item => item.Key.Equals("NAME", StringComparison.InvariantCultureIgnoreCase)).Value;
            this.location = feature.GetWellKnownText();
        }

        public Feature Feature
        {
            get { return feature; }
        }

        public DataGridViewImageCell ImageButtonCell
        {
            get
            {
                if (imageButtonCell == null)
                {
                    imageButtonCell = new DataGridViewImageCell();
                    imageButtonCell.Style.SelectionBackColor = Color.Transparent;
                    imageButtonCell.Value = Icon.FromHandle(Resources.find.GetHicon());
                }

                return imageButtonCell;
            }
        }

        public DataGridViewTextBoxCell TextCell
        {
            get { return textCell ?? (textCell = new DataGridViewTextBoxCell { Value = name }); }
        }

        public DataGridViewTextBoxCell LocationCell
        {
            get { return locationCell ?? (locationCell = new DataGridViewTextBoxCell { Value = location }); }
        }
    }
}