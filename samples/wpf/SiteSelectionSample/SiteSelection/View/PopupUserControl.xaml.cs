using System.Windows.Controls;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.SiteSelection
{
    /// <summary>
    /// Interaction logic for PopupUserControl.xaml
    /// </summary>
    public partial class PopupUserControl : UserControl
    {
        public PopupUserControl(Feature feature)
        {
            InitializeComponent();
            DataContext = new PopupUserControlViewModel(feature);
        }
    }
}