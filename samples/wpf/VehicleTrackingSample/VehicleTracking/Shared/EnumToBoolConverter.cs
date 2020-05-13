using System;
using System.Globalization;
using System.Windows.Data;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public class EnumToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = false;
            if (value is MeasureMode)
            {
                result = GetMeasureModeResult(value, parameter);
            }
            else if (value is DrawFenceMode)
            {
                result = GetDrawFenceModeResult(value, parameter);
            }
            else if (value is ControlMapMode)
            {
                result = GetMapModeResult(value, parameter);
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string temp = parameter.ToString();
            if (temp == "MeasureLine" || temp == "MeasurePolygon")
            {
                MeasureMode measureMode = MeasureMode.Line;
                bool isChecked = (bool)value;

                if (isChecked)
                {
                    switch (parameter.ToString())
                    {
                        case "MeasureLine":
                            measureMode = MeasureMode.Line;
                            break;
                        case "MeasurePolygon":
                            measureMode = MeasureMode.Polygon;
                            break;
                    }
                }
                else
                {
                    switch (parameter.ToString())
                    {
                        case "MeasureLine":
                            measureMode = MeasureMode.Polygon;
                            break;
                        case "MeasurePolygon":
                            measureMode = MeasureMode.Line;
                            break;
                    }
                }
                return measureMode;
            }

            if (temp == "Pan" || temp == "DrawFence" || temp == "Measure")
            {
                ControlMapMode mapMode = ControlMapMode.Pan;
                bool isChecked = (bool)value;

                if (isChecked)
                {
                    switch (parameter.ToString())
                    {
                        case "Pan":
                            mapMode = ControlMapMode.Pan;
                            break;
                        case "DrawFence":
                            mapMode = ControlMapMode.DrawFence;
                            break;
                        case "Measure":
                            mapMode = ControlMapMode.Measure;
                            break;
                    }
                }
                else
                {
                    switch (parameter.ToString())
                    {
                        case "Pan":
                            mapMode = ControlMapMode.DrawFence;
                            break;
                        case "DrawFence":
                            mapMode = ControlMapMode.Measure;
                            break;
                        case "Measure":
                            mapMode = ControlMapMode.Pan;
                            break;
                    }
                }
                return mapMode;
            }

            if (temp == "NewFence" || temp == "EditFence")
            {
                DrawFenceMode drawFenceMode = DrawFenceMode.DrawNewFence;
                bool isChecked = (bool)value;

                if (isChecked)
                {
                    switch (parameter.ToString())
                    {
                        case "NewFence":
                            drawFenceMode = DrawFenceMode.DrawNewFence;
                            break;
                        case "EditFence":
                            drawFenceMode = DrawFenceMode.EditFence;
                            break;
                    }
                }
                else
                {
                    switch (parameter.ToString())
                    {
                        case "NewFence":
                            drawFenceMode = DrawFenceMode.EditFence;
                            break;
                        case "EditFence":
                            drawFenceMode = DrawFenceMode.DrawNewFence;
                            break;
                    }
                }
                return drawFenceMode;
            }

            return value;
        }

        private bool GetMapModeResult(object value, object parameter)
        {
            bool result = false;
            ControlMapMode mapMode = (ControlMapMode)value;
            switch (mapMode)
            {
                case ControlMapMode.Pan:
                    switch (parameter.ToString())
                    {
                        case "Pan":
                            result = true;
                            break;
                        case "DrawFence":
                            result = false;
                            break;
                        case "Measure":
                            result = false;
                            break;
                    }
                    break;
                case ControlMapMode.DrawFence:
                    switch (parameter.ToString())
                    {
                        case "Pan":
                            result = false;
                            break;
                        case "DrawFence":
                            result = true;
                            break;
                        case "Measure":
                            result = false;
                            break;
                    }
                    break;
                case ControlMapMode.Measure:
                    switch (parameter.ToString())
                    {
                        case "Pan":
                            result = false;
                            break;
                        case "DrawFence":
                            result = false;
                            break;
                        case "Measure":
                            result = true;
                            break;
                    }
                    break;
            }
            return result;
        }

        private bool GetDrawFenceModeResult(object value, object parameter)
        {
            bool result = false;
            DrawFenceMode drawFenceMode = (DrawFenceMode)value;
            switch (drawFenceMode)
            {
                case DrawFenceMode.DrawNewFence:
                    switch (parameter.ToString())
                    {
                        case "NewFence":
                            result = true;
                            break;
                        case "EditFence":
                            result = false;
                            break;
                    }
                    break;
                case DrawFenceMode.EditFence:
                    switch (parameter.ToString())
                    {
                        case "NewFence":
                            result = false;
                            break;
                        case "EditFence":
                            result = true;
                            break;
                    }
                    break;
            }
            return result;
        }

        private bool GetMeasureModeResult(object value, object parameter)
        {
            bool result = false;
            MeasureMode measureMode = (MeasureMode)value;
            switch (measureMode)
            {
                case MeasureMode.Line:
                    switch (parameter.ToString())
                    {
                        case "MeasureLine":
                            result = true;
                            break;
                        case "MeasurePolygon":
                            result = false;
                            break;
                    }
                    break;
                case MeasureMode.Polygon:
                    switch (parameter.ToString())
                    {
                        case "MeasureLine":
                            result = false;
                            break;
                        case "MeasurePolygon":
                            result = true;
                            break;
                    }
                    break;
            }
            return result;
        }
    }
}
