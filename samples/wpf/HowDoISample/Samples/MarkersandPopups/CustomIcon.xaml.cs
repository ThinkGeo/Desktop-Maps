using System.ComponentModel;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Interaction logic for CustomIcon.xaml
    /// </summary>
    public partial class CustomIcon : INotifyPropertyChanged
    {
        private bool _animationStarted;
        public CustomIcon()
        {
            InitializeComponent();
        }

        public bool AnimationStarted
        {
            get => _animationStarted;
            set
            {
                if (_animationStarted != value)
                {
                    _animationStarted = value;
                    OnPropertyChanged(nameof(AnimationStarted));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
