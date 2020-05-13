
namespace ThinkGeo.MapSuite.USDemographicMap
{
    public class CustomDataCategoryViewModel : DataCategoryViewModel
    {
        private string customDataFilePathName;

        public CustomDataCategoryViewModel()
            : base()
        {
            CanUsePieView = false;
        }

        public string CustomDataFilePathName
        {
            get { return customDataFilePathName; }
            set
            {
                customDataFilePathName = value;
                RaisePropertyChanged(() => CustomDataFilePathName);
            }
        }
    }
}