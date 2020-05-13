using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RedisenceSearch
{
    public class SearchResult : INotifyPropertyChanged
    {
        private string lowHouseNo = "N/A";
        private string highHouseNo = "N/A";
        private string dirPrefix = "N/A";
        private string streetName = "N/A";
        private string streetType = "N/A";
        private string dirSuffix = "N/A";
        private string lowCrossStreet = "N/A";
        private string highCrossStreet = "N/A";

        public string LowHouseNo
        {
            get { return lowHouseNo; }
            set
            {
                if(lowHouseNo != value)
                {
                    lowHouseNo = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string HighHouseNo
        {
            get { return highHouseNo; }
            set
            {
                if (highHouseNo != value)
                {
                    highHouseNo = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string DirPrefix
        {
            get { return dirPrefix; }
            set
            {
                if (dirPrefix != value)
                {
                    dirPrefix = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string StreetName
        {
            get { return streetName; }
            set
            {
                if (streetName != value)
                {
                    streetName = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string StreetType
        {
            get { return streetType; }
            set
            {
                if (streetType != value)
                {
                    streetType = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string DirSuffix
        {
            get { return dirSuffix; }
            set
            {
                if (dirSuffix != value)
                {
                    dirSuffix = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string LowCrossStreet
        {
            get { return lowCrossStreet; }
            set
            {
                if (lowCrossStreet != value)
                {
                    lowCrossStreet = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string HighCrossStreet
        {
            get { return highCrossStreet; }
            set
            {
                if (highCrossStreet != value)
                {
                    highCrossStreet = value;
                    RaisePropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
