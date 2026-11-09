using System.Windows.Controls;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// The renderer decision guide: when the GPU tile path wins, when the classic
    /// per-frame overlays do, and why - each rule anchored to the sample that
    /// demonstrates it. Static content; the knowledge lives in the XAML.
    /// </summary>
    public partial class RendererGuidePage : UserControl
    {
        public RendererGuidePage()
        {
            InitializeComponent();
        }
    }
}
