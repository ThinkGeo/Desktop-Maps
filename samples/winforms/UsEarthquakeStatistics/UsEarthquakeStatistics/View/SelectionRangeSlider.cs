using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.VisualStyles;

namespace ThinkGeo.MapSuite.EarthquakeStatistics
{
    public sealed class SelectionRangeSlider : Control
    {
        public event EventHandler LeftValueChanged;
        public event EventHandler RightValueChanged;
        public event EventHandler ValueChanged;

        private int maximum;
        private int minimum;
        private int tickNum;
        private int valueLeft;
        private int valueRight;
        private int smallChange;
        private bool draggingLeft;
        private bool draggingRight;
        private Thumbs selectedThumb;
        private Orientation orientation;
        private TrackBarThumbState leftThumbState;
        private TrackBarThumbState rightThumbState;

        public SelectionRangeSlider()
        {
            tickNum = 1;
            DoubleBuffered = true;
            SetDefaults();
        }

        [Description("The maximum value.")]
        public int Maximum
        {
            get { return maximum; }
            set
            {
                if (value <= Minimum)
                {
                    throw new ArgumentException(string.Format("Value of '{0}' is not valid for 'Maximum'. 'Maximum' should be greater than 'Minimum'.", value.ToString(CultureInfo.InvariantCulture)), "Maximum");
                }
                maximum = value;
                Invalidate();
            }
        }

        [Description("The minimum value.")]
        public int Minimum
        {
            get { return minimum; }
            set
            {
                if (value >= Maximum)
                {
                    throw new ArgumentException(string.Format("Value of '{0}' is not valid for 'Minimum'. 'Minimum' should be less than 'Maximum'.", value.ToString(CultureInfo.InvariantCulture)), "Minimum");
                }
                minimum = value;
                Invalidate();
            }
        }

        [Description("The orientation of the control.")]
        public Orientation Orientation
        {
            get { return orientation; }
            set { orientation = value; }
        }

        [Description("The thumb that had focus last.")]
        public Thumbs SelectedThumb
        {
            get { return selectedThumb; }
            private set { selectedThumb = value; }
        }

        [Description("The amount of positions the closest slider moves when the control is clicked.")]
        public int SmallChange
        {
            get { return smallChange; }
            set { smallChange = value; }
        }

        public int TickNum
        {
            get { return tickNum; }
            set { tickNum = value; }
        }

        [Description("The position of the left slider.")]
        public int ValueLeft
        {
            get { return valueLeft; }
            set
            {
                valueLeft = value;

                OnValueChanged(EventArgs.Empty);
                OnLeftValueChanged(EventArgs.Empty);
                Invalidate();
            }
        }

        [Description("The position of the right slider.")]
        public int ValueRight
        {
            get { return valueRight; }
            set
            {
                valueRight = value;

                OnValueChanged(EventArgs.Empty);
                OnRightValueChanged(EventArgs.Empty);
                Invalidate();
            }
        }

        private double RelativeValueLeft
        {
            get
            {
                double difference = Maximum - Minimum;
                return difference == 0 ? ValueLeft : (ValueLeft - Minimum) / difference;
            }
        }

        private double RelativeValueRight
        {
            get
            {
                double difference = Maximum - Minimum;
                return difference == 0 ? ValueLeft : (ValueRight - Minimum) / difference;
            }
        }

        public void DecrementLeft()
        {
            int newValue = Math.Max(ValueLeft - 1, Minimum);
            if (IsValidValueLeft(newValue)) ValueLeft = newValue;
            Invalidate();
        }

        public void DecrementRight()
        {
            int newValue = Math.Max(ValueRight - 1, Minimum);
            if (IsValidValueRight(newValue)) ValueRight = newValue;
            Invalidate();
        }

        public void IncrementLeft()
        {
            int newValue = Math.Min(ValueLeft + 1, Maximum);
            if (IsValidValueLeft(newValue)) ValueLeft = newValue;
            Invalidate();
        }

        public void IncrementRight()
        {
            int newValue = Math.Min(ValueRight + 1, Maximum);
            if (IsValidValueRight(newValue)) ValueRight = newValue;
            Invalidate();
        }

        private void OnLeftValueChanged(EventArgs e)
        {
            EventHandler handler = LeftValueChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Focus();
            SetThumbState(e.Location, TrackBarThumbState.Pressed);

            draggingLeft = (leftThumbState == TrackBarThumbState.Pressed);
            if (!draggingLeft)
                draggingRight = (rightThumbState == TrackBarThumbState.Pressed);

            if (draggingLeft)
            {
                SelectedThumb = Thumbs.Left;
            }
            else if (draggingRight)
            {
                SelectedThumb = Thumbs.Right;
            }

            if (!draggingLeft && !draggingRight)
            {
                if (GetClosestSlider(e.Location) == Thumbs.Left)
                {
                    if (e.X < GetLeftThumbRectangle().X) DecrementLeft();
                    else IncrementLeft();

                    SelectedThumb = Thumbs.Left;
                }
                else
                {
                    if (e.X < GetRightThumbRectangle().X) DecrementRight();
                    else IncrementRight();
                    SelectedThumb = Thumbs.Right;
                }
            }

            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (draggingLeft || draggingRight)
            {
                SetThumbState(e.Location, TrackBarThumbState.Hot);

                int offset = Convert.ToInt32(e.X / (double)(Width) * (Maximum - Minimum));

                int newValue = Minimum + offset;
                if (draggingLeft && IsValidValueLeft(newValue)) ValueLeft = newValue;
                else if (draggingRight && IsValidValueRight(newValue)) ValueRight = newValue;

                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            draggingLeft = false;
            draggingRight = false;
            Invalidate();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            if (e.Delta == 0) return;

            switch (SelectedThumb)
            {
                case Thumbs.Left:
                    if (e.Delta > 0) IncrementLeft();
                    else DecrementLeft();
                    break;
                case Thumbs.Right:
                    if (e.Delta > 0) IncrementRight();
                    else DecrementRight();
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Size thumbSize = GetThumbRectangle(0, e.Graphics).Size;
            Rectangle trackRect = GetTrackRectangle(Convert.ToInt32(thumbSize.Width / 2));
            Rectangle ticksRect = trackRect;
            ticksRect.Offset(0, 15);

            Rectangle leftRectangle = GetLeftThumbRectangle(e.Graphics);
            Rectangle rightRectangle = GetRightThumbRectangle(e.Graphics);
            Rectangle highlightRectangle = new Rectangle((leftRectangle.X + leftRectangle.Width / 2), trackRect.Y, (rightRectangle.X + rightRectangle.Width / 2) - (leftRectangle.X + leftRectangle.Width / 2), trackRect.Height);

            TrackBarRenderer.DrawVerticalTrack(e.Graphics, trackRect);
            TrackBarRenderer.DrawHorizontalTicks(e.Graphics, ticksRect, (Maximum - Minimum) / TickNum + 1, EdgeStyle.Etched);

            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(64, 141, 219)), highlightRectangle);

            TrackBarRenderer.DrawBottomPointingThumb(e.Graphics, GetLeftThumbRectangle(e.Graphics), leftThumbState);
            TrackBarRenderer.DrawBottomPointingThumb(e.Graphics, GetRightThumbRectangle(e.Graphics), rightThumbState);
        }

        private void OnRightValueChanged(EventArgs e)
        {
            EventHandler handler = RightValueChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void OnValueChanged(EventArgs e)
        {
            EventHandler handler = ValueChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private Thumbs GetClosestSlider(Point point)
        {
            Rectangle leftThumbRect = GetLeftThumbRectangle();
            Rectangle rightThumbRect = GetRightThumbRectangle();
            if (Orientation == Orientation.Horizontal)
            {
                if (Math.Abs(leftThumbRect.X - point.X) > Math.Abs(rightThumbRect.X - point.X) && Math.Abs(leftThumbRect.Right - point.X) > Math.Abs(rightThumbRect.Right - point.X))
                {
                    return Thumbs.Right;
                }
                return Thumbs.Left;
            }
            if (Math.Abs(leftThumbRect.Y - point.Y) > Math.Abs(rightThumbRect.Y - point.Y) && Math.Abs(leftThumbRect.Bottom - point.Y) > Math.Abs(rightThumbRect.Bottom - point.Y))
            {
                return Thumbs.Right;
            }
            return Thumbs.Left;
        }

        private Rectangle GetLeftThumbRectangle(Graphics g = null)
        {
            bool shouldDispose = (g == null);
            if (shouldDispose)
                g = CreateGraphics();

            Rectangle rect = GetThumbRectangle(RelativeValueLeft, g);
            if (shouldDispose)
                g.Dispose();

            return rect;
        }

        private Rectangle GetRightThumbRectangle(Graphics g = null)
        {
            bool shouldDispose = (g == null);
            if (shouldDispose) g = CreateGraphics();

            Rectangle rect = GetThumbRectangle(RelativeValueRight, g);
            if (shouldDispose) g.Dispose();

            return rect;
        }

        private Rectangle GetThumbRectangle(double relativeValue, Graphics g)
        {
            Size size = TrackBarRenderer.GetBottomPointingThumbSize(g, TrackBarThumbState.Normal);
            int border = Convert.ToInt32(size.Width / 2);
            int w = GetTrackRectangle(border).Width;
            int x = Convert.ToInt32(0 / (Maximum - Minimum) * w + relativeValue * w);

            int y = Convert.ToInt32((Height - size.Height) / 2);
            return new Rectangle(new Point(x, y), size);
        }

        private Rectangle GetTrackRectangle(int border)
        {
            return new Rectangle(border, Convert.ToInt32(Height / 2) - 3, Width - 2 * border - 1, 4);
        }

        private bool IsValidValueLeft(int value)
        {
            return (value >= Minimum && value <= Maximum && value < ValueRight);
        }

        private bool IsValidValueRight(int value)
        {
            return (value >= Minimum && value <= Maximum && value > ValueLeft);
        }

        private void SetDefaults()
        {
            Orientation = Orientation.Horizontal;
            SmallChange = 1;
            Maximum = 10;
            Minimum = 0;
            ValueLeft = 0;
            ValueRight = 7;
        }

        private void SetThumbState(Point location, TrackBarThumbState newState)
        {
            Rectangle leftThumbRect = GetLeftThumbRectangle();
            Rectangle rightThumbRect = GetRightThumbRectangle();

            if (leftThumbRect.Contains(location))
            {
                leftThumbState = newState;
            }
            else
            {
                leftThumbState = SelectedThumb == Thumbs.Left ? TrackBarThumbState.Hot : TrackBarThumbState.Normal;
            }

            if (rightThumbRect.Contains(location))
            {
                rightThumbState = newState;
            }
            else
            {
                rightThumbState = SelectedThumb == Thumbs.Right ? TrackBarThumbState.Hot : TrackBarThumbState.Normal;
            }
        }

        internal class DoubleTrackBarDesigner : ControlDesigner
        {
            private SelectionRangeSlider TrackBar
            {
                get { return (SelectionRangeSlider)Control; }
            }

            protected override bool GetHitTest(Point point)
            {
                Point pt = TrackBar.PointToClient(point);
                if (TrackBar.GetLeftThumbRectangle().Contains(pt) || TrackBar.GetRightThumbRectangle().Contains(pt))
                {
                    return true;
                }
                return base.GetHitTest(point);
            }
        }
    }

    public enum Thumbs
    {
        None = 0,
        Left = 1,
        Right = 2
    }
}