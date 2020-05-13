using LiveCharts;
using LiveCharts.Wpf;
using System.ComponentModel;

namespace ThinkGeo.MapSuite.DebugSamples
{
    /// <summary>
    /// Interaction logic for CustomerVm.xaml
    /// </summary>
    public partial class CustomersTooltip : IChartTooltip
    {
        private TooltipData _data;

        public CustomersTooltip()
        {
            InitializeComponent();

            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public TooltipData Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged("Data");
            }
        }

        public TooltipSelectionMode? SelectionMode { get; set; }

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
