using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public class ImageButton : Button
    {
        public static readonly DependencyProperty DisableImageSourceProperty =
        DependencyProperty.Register("DisableImageSource", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));

        public static readonly DependencyProperty ImageSourceProperty =
        DependencyProperty.Register("ImageSource", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));

        public ImageButton()
            : base()
        { }

        public ImageSource DisableImageSource
        {
            get { return (ImageSource)GetValue(DisableImageSourceProperty); }
            set { SetValue(DisableImageSourceProperty, value); }
        }

        public ImageSource ImageSource
        {
            get { return (ImageSource)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }
    }
}