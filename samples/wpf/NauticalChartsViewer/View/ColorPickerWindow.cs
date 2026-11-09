using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NauticalChartsViewer
{
    /// <summary>
    /// A small WPF RGB color picker dialog (replaces the WinForms ColorDialog so the sample
    /// stays pure WPF). Exposes the chosen R/G/B (0-255) and returns true on OK.
    /// </summary>
    public class ColorPickerWindow : Window
    {
        private readonly Slider redSlider;
        private readonly Slider greenSlider;
        private readonly Slider blueSlider;
        private readonly Border preview;

        public int R => (int)redSlider.Value;
        public int G => (int)greenSlider.Value;
        public int B => (int)blueSlider.Value;

        public ColorPickerWindow(int r, int g, int b)
        {
            Title = "Pick Color";
            Width = 340;
            SizeToContent = SizeToContent.Height;
            ResizeMode = ResizeMode.NoResize;
            WindowStyle = WindowStyle.ToolWindow;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            preview = new Border
            {
                Height = 40,
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(0, 0, 0, 10)
            };

            redSlider = CreateSlider(r);
            greenSlider = CreateSlider(g);
            blueSlider = CreateSlider(b);

            var panel = new StackPanel { Margin = new Thickness(12) };
            panel.Children.Add(preview);
            panel.Children.Add(CreateSliderRow("R", redSlider));
            panel.Children.Add(CreateSliderRow("G", greenSlider));
            panel.Children.Add(CreateSliderRow("B", blueSlider));

            var okButton = new Button { Content = "OK", Width = 72, Margin = new Thickness(0, 8, 8, 0), IsDefault = true };
            okButton.Click += (s, e) => { DialogResult = true; };
            var cancelButton = new Button { Content = "Cancel", Width = 72, Margin = new Thickness(0, 8, 0, 0), IsCancel = true };

            var buttons = new StackPanel { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };
            buttons.Children.Add(okButton);
            buttons.Children.Add(cancelButton);
            panel.Children.Add(buttons);

            Content = panel;
            UpdatePreview();
        }

        private Slider CreateSlider(int value)
        {
            var slider = new Slider
            {
                Minimum = 0,
                Maximum = 255,
                Value = Clamp(value),
                SmallChange = 1,
                LargeChange = 16,
                IsSnapToTickEnabled = true,
                TickFrequency = 1,
                VerticalAlignment = VerticalAlignment.Center
            };
            slider.ValueChanged += (s, e) => UpdatePreview();
            return slider;
        }

        private Grid CreateSliderRow(string label, Slider slider)
        {
            var grid = new Grid { Margin = new Thickness(0, 2, 0, 2) };
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(18) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(34) });

            var name = new TextBlock { Text = label, VerticalAlignment = VerticalAlignment.Center };
            Grid.SetColumn(name, 0);
            Grid.SetColumn(slider, 1);

            var valueText = new TextBlock { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Right };
            valueText.SetBinding(TextBlock.TextProperty, new System.Windows.Data.Binding("Value") { Source = slider, StringFormat = "0" });
            Grid.SetColumn(valueText, 2);

            grid.Children.Add(name);
            grid.Children.Add(slider);
            grid.Children.Add(valueText);
            return grid;
        }

        private void UpdatePreview()
        {
            if (preview != null)
            {
                preview.Background = new SolidColorBrush(Color.FromRgb((byte)R, (byte)G, (byte)B));
            }
        }

        private static double Clamp(int value)
        {
            if (value < 0) return 0;
            if (value > 255) return 255;
            return value;
        }
    }
}
