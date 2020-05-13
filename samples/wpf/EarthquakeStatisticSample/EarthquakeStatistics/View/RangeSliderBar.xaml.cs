using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public partial class RangeSliderBar : UserControl
    {
        private static int frequency = 1;
        private static int sliderHeight = 30;
        private static int maximum = 100;
        private static int minimum = 0;
        private static int sliderWidth = 150;

        private bool isSliderClicked;

        public static readonly DependencyProperty EndValueProperty =
                    DependencyProperty.Register("EndValue", typeof(int), typeof(RangeSliderBar), new PropertyMetadata(maximum));

        public static readonly DependencyProperty MaximumProperty =
                    DependencyProperty.Register("Maximum", typeof(int), typeof(RangeSliderBar), new PropertyMetadata(maximum));

        public static readonly DependencyProperty MinimumProperty =
                    DependencyProperty.Register("Minimum", typeof(int), typeof(RangeSliderBar), new PropertyMetadata(minimum));

        public static readonly DependencyProperty SliderHeightProperty =
                    DependencyProperty.Register("SilderHeight", typeof(int), typeof(RangeSliderBar), new PropertyMetadata(sliderHeight));

        public static readonly DependencyProperty SliderWidthProperty =
                    DependencyProperty.Register("SilderWidth", typeof(int), typeof(RangeSliderBar), new PropertyMetadata(sliderWidth));

        public static readonly DependencyProperty SliderTickFrequencyProperty =
                    DependencyProperty.Register("SliderTickFrequency", typeof(int), typeof(RangeSliderBar), new PropertyMetadata(frequency));

        public static readonly DependencyProperty StartValueProperty =
                    DependencyProperty.Register("StartValue", typeof(int), typeof(RangeSliderBar));

        private static readonly DependencyProperty EndRectProperty =
                    DependencyProperty.Register("EndRect", typeof(Rect), typeof(RangeSliderBar));

        private static readonly DependencyProperty StartRectProperty =
                    DependencyProperty.Register("StartRect", typeof(Rect), typeof(RangeSliderBar));

        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(RangeSliderBar));

        public RangeSliderBar()
        {
            InitializeComponent();
            isSliderClicked = false;
        }

        public event RoutedPropertyChangedEventHandler<double> ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        public int EndValue
        {
            get { return (int)GetValue(EndValueProperty); }
            set { SetValue(EndValueProperty, value); }
        }

        public int Maximum
        {
            get { return (int)GetValue(MaximumProperty); }
            set { SetValue(MaximumProperty, value); }
        }

        public int Minimum
        {
            get { return (int)GetValue(MinimumProperty); }
            set { SetValue(MinimumProperty, value); }
        }

        public int SliderHeight
        {
            get { return (int)GetValue(SliderHeightProperty); }
            set { SetValue(SliderHeightProperty, value); }
        }

        public int SliderWidth
        {
            get { return (int)GetValue(SliderWidthProperty); }
            set { SetValue(SliderWidthProperty, value); }
        }

        public int SliderTickFrequency
        {
            get { return (int)GetValue(SliderTickFrequencyProperty); }
            set { SetValue(SliderTickFrequencyProperty, value); }
        }

        public int StartValue
        {
            get { return (int)GetValue(StartValueProperty); }
            set { SetValue(StartValueProperty, value); }
        }

        private Rect EndRect
        {
            get { return (Rect)GetValue(EndRectProperty); }
            set { SetValue(EndRectProperty, value); }
        }

        private Rect StartRect
        {
            get { return (Rect)GetValue(StartRectProperty); }
            set { SetValue(StartRectProperty, value); }
        }

        protected void OnValueChanged(double oldValue, double newValue)
        {
            RoutedPropertyChangedEventArgs<double> args = new RoutedPropertyChangedEventArgs<double>(oldValue, newValue);
            args.RoutedEvent = ValueChangedEvent;
            RaiseEvent(args);
        }

        private void ClipSilder()
        {
            int selectedValue = EndValue - StartValue;
            int totalValue = Maximum - Minimum;
            double sliderClipWidth = SliderWidth * (StartValue - Minimum + selectedValue * 0.5) / totalValue;
            StartRect = new Rect(0, 0, sliderClipWidth, SliderHeight);
            EndRect = new Rect(sliderClipWidth, 0, SliderWidth, SliderHeight);
        }

        private void SL_Bat1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue > EndValue)
                StartValue = EndValue;
            ClipSilder();
            if (isSliderClicked)
            {
                OnValueChanged(e.OldValue, e.NewValue);
                isSliderClicked = false;
            }
        }

        private void SL_Bat2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (e.NewValue < StartValue)
                EndValue = StartValue;
            ClipSilder();

            if (isSliderClicked)
            {
                OnValueChanged(e.OldValue, e.NewValue);
                isSliderClicked = false;
            }
        }

        private void UC_Arrange_Loaded(object sender, RoutedEventArgs e)
        {
            ClipSilder();
        }

        private void SL_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            OnValueChanged(-9999, ((Slider)sender).Value);
        }

        private void SL_Click(object sender, RoutedEventArgs e)
        {
            isSliderClicked = true;
        }
    }
}