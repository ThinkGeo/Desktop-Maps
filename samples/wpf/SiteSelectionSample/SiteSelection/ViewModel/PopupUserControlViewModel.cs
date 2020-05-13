using System.Collections.ObjectModel;
using System.Globalization;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public class PopupUserControlViewModel : ViewModelBase
    {
        private string name;
        private string address;
        private Feature feature;
        private ObservableCollection<string> informations;

        public PopupUserControlViewModel()
            : this(null)
        { }

        public PopupUserControlViewModel(Feature feature)
            : base()
        {
            informations = new ObservableCollection<string>();
            Feature = feature;
        }

        public Feature Feature
        {
            get { return feature; }
            set
            {
                feature = value;
                LoadInformation();
            }
        }

        public string Address
        {
            get { return string.Format(CultureInfo.InvariantCulture, "Addr: {0} ,TX", address); }
        }

        public string Header
        {
            get { return name; }
        }

        public ObservableCollection<string> Informations
        {
            get { return informations; }
        }

        private void LoadInformation()
        {
            if (feature == null) return;

            name = string.Empty;
            address = string.Empty;
            Informations.Clear();

            foreach (var item in feature.ColumnValues)
            {
                string key = item.Key.ToUpperInvariant();
                if (key == "NAME")
                {
                    name = item.Value;
                }
                else if (key == "ADDRESS")
                {
                    address = item.Value;
                }
                else
                {
                    Informations.Add(string.Format(CultureInfo.InvariantCulture, "{0} : {1}", item.Key, item.Value));
                }
            }

            RaisePropertyChanged(() => Header);
            RaisePropertyChanged(() => Address);
            RaisePropertyChanged(() => Informations);
        }
    }
}