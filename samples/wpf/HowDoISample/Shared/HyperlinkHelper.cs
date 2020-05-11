using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    class HyperlinkHelper
    {
        public static readonly DependencyProperty LaunchBrowserProperty =
            DependencyProperty.RegisterAttached("LaunchBrowser", typeof(bool),
                typeof(HyperlinkHelper), new PropertyMetadata(false,
                    HyperlinkHelper_LaunchBrowserChanged));

        public static bool GetLaunchBrowser(DependencyObject d)
        {
            return (bool)d.GetValue(LaunchBrowserProperty);
        }

        public static void SetLaunchBrowser(DependencyObject d, bool value)
        {
            d.SetValue(LaunchBrowserProperty, value);
        }

        private static void HyperlinkHelper_LaunchBrowserChanged(object
            sender, DependencyPropertyChangedEventArgs e)
        {
            var d = (UIElement)sender;
            if ((bool)e.NewValue)
                d.AddHandler(Hyperlink.RequestNavigateEvent, new RequestNavigateEventHandler(Hyperlink_RequestNavigateEvent));
            else
                d.RemoveHandler(Hyperlink.RequestNavigateEvent, new RequestNavigateEventHandler(Hyperlink_RequestNavigateEvent));
        }

        private static void Hyperlink_RequestNavigateEvent(object sender, RequestNavigateEventArgs e)
        {
            var process = new Process { StartInfo = { UseShellExecute = true, FileName = e.Uri.AbsoluteUri } };
            process.Start();
            e.Handled = true;
        }
    }
}
