using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public class DataCategoryViewModel : ViewModelBase
    {
        private bool canUsePieView;
        private string title;
        private BitmapImage categoryImage;
        private ObservableCollection<ColumnViewModel> columns;

        public DataCategoryViewModel()
        {
            columns = new ObservableCollection<ColumnViewModel>();
        }

        public bool CanUsePieView
        {
            get { return canUsePieView; }
            set
            {
                canUsePieView = value;
                RaisePropertyChanged(() => CanUsePieView);
            }
        }

        public BitmapImage CategoryImage
        {
            get { return categoryImage; }
            set
            {
                categoryImage = value;
                RaisePropertyChanged(() => CategoryImage);
            }
        }

        public ObservableCollection<ColumnViewModel> Columns
        {
            get { return columns; }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                RaisePropertyChanged(() => Title);
            }
        }
    }
}