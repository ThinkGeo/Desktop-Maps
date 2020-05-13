using System.Windows;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public class ColumnViewModel : ViewModelBase
    {
        private bool isSelected;
        private string alias;
        private string columnName;
        private string legendTitle;
        private Visibility checkBoxVisiblity;
        private DataCategoryViewModel parent;

        public ColumnViewModel()
            : this(string.Empty, string.Empty)
        { }

        public ColumnViewModel(string columnName, string displayName)
        {
            this.columnName = columnName;
            alias = displayName;
            IsSelected = true;
            CheckBoxVisiblity = Visibility.Hidden;
        }

        public string Alias
        {
            get { return alias; }
            set
            {
                alias = value;
                RaisePropertyChanged(() => Alias);
            }
        }

        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }

        public string LegendTitle
        {
            get { return legendTitle; }
            set { legendTitle = value; }
        }

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                RaisePropertyChanged(() => IsSelected);
            }
        }

        public Visibility CheckBoxVisiblity
        {
            get { return checkBoxVisiblity; }
            set
            {
                checkBoxVisiblity = value;
                RaisePropertyChanged(() => CheckBoxVisiblity);
            }
        }

        public DataCategoryViewModel Parent
        {
            get { return parent; }
            set { parent = value; }
        }
    }
}