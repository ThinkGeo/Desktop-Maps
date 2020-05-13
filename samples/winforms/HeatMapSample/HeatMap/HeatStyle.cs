using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using ThinkGeo.MapSuite.Core;


namespace HeatMap
{
    class HeatStyle : Style
    {
        private double intensityRangeStart;
        private double intensityRangeEnd;
        private string intensityColumnName;
        private float pointSize;
        private DistanceUnit pointSizeUnit;
        private int intensity;
        private int alpha;
        private Collection<GeoColor> colorPalette = new Collection<GeoColor>();

        public HeatStyle()
            : this(10, 255, string.Empty, 0, 0, 50, DistanceUnit.Kilometer)
        { }

        public HeatStyle(int intensity)
            : this(intensity, 255, string.Empty, 0, 0, 50, DistanceUnit.Kilometer)
        { }

        public HeatStyle(int intensity, int alpha)
            : this(intensity, alpha, string.Empty, 0, 0, 50, DistanceUnit.Kilometer)
        { }

        public HeatStyle(int intensity, float pointSize, DistanceUnit pointSizeUnit)
            : this(10, 255, string.Empty, 0, 0, pointSize, pointSizeUnit)
        { }

        public HeatStyle(int intensity, int alpha, float pointSize, DistanceUnit pointSizeUnit)
            : this(10, alpha, string.Empty, 0, 0, pointSize, pointSizeUnit)
        { }

        public HeatStyle(string intensityColumnName, double intensityRangeStart, int intensityRangeEnd)
            : this(10, 255, intensityColumnName, intensityRangeStart, intensityRangeEnd,  50, DistanceUnit.Kilometer)
        { }

        public HeatStyle(int alpha, string intensityColumnName, double intensityRangeStart, int intensityRangeEnd)
            : this(10, alpha, intensityColumnName, intensityRangeStart, intensityRangeEnd,  50, DistanceUnit.Kilometer)
        { }

        public HeatStyle(string intensityColumnName, double intensityRangeStart, int intensityRangeEnd, float pointSize, DistanceUnit pointSizeUnit)
            : this(10, 255, intensityColumnName, intensityRangeStart, intensityRangeEnd, pointSize, pointSizeUnit)
        { }

        public HeatStyle(int alpha, string intensityColumnName, double intensityRangeStart, int intensityRangeEnd, float pointSize, DistanceUnit pointSizeUnit)
            : this(10, alpha, intensityColumnName, intensityRangeStart, intensityRangeEnd, pointSize, pointSizeUnit)
        { }

        public HeatStyle(int intensity, int alpha, string intensityColumnName, double intensityRangeStart, int intensityRangeEnd, float pointSize, DistanceUnit pointSizeUnit)
        {
            this.intensity = intensity;
            this.alpha = alpha;
            this.intensityColumnName = intensityColumnName;
            this.intensityRangeStart = intensityRangeStart;
            this.intensityRangeEnd = intensityRangeEnd;
            this.pointSize = pointSize;
            this.PointSizeUnit = DistanceUnit.Kilometer;
        }

        public int Intensity
        {
            get { return intensity; }
            set { intensity = value; }
        }

        public int Alpha
        {
            get { return alpha; }
            set { alpha = value; }
        }

        public double IntensityRangeStart
        {
            get { return intensityRangeStart; }
            set { intensityRangeStart = value; }
        }

        public double IntensityRangeEnd
        {
            get { return intensityRangeEnd; }
            set { intensityRangeEnd = value; }
        }

        public string IntensityColumnName
        {
            get { return intensityColumnName; }
            set { intensityColumnName = value; }
        }

        public float PointSize
        {
            get { return pointSize; }
            set { pointSize = value; }
        }

        public DistanceUnit PointSizeUnit
        {
            get { return pointSizeUnit; }
            set { pointSizeUnit = value; }
        }

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInThisLayer, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInAllLayers)
        {
            Bitmap intensityBitmap = null;
            Bitmap colorBitmap = null;
            GeoImage geoImage = null;
            MemoryStream pngStream = null;

            try
            {
                // Load the default color palette if it does not exisit 
                // or does not have the right amount of colors
                if (colorPalette.Count != 256)
                {
                    colorPalette = GetDefaultColorPalette();
                }

                intensityBitmap = new Bitmap(Convert.ToInt32(canvas.Width), Convert.ToInt32(canvas.Height));
                Graphics Surface = Graphics.FromImage(intensityBitmap);
                Surface.Clear(Color.Transparent);
                Surface.Dispose();

                List<HeatPoint> heatPoints = new List<HeatPoint>();

                foreach (Feature feature in features)
                {
                    if (feature.GetWellKnownType() == WellKnownType.Point)
                    {
                        PointShape pointShape = (PointShape)feature.GetShape();
                        ScreenPointF screenPoint = ExtentHelper.ToScreenCoordinate(canvas.CurrentWorldExtent, pointShape, canvas.Width, canvas.Height);

                        double realValue;
                        if (intensityRangeStart != 0 && intensityRangeEnd != 0 && intensityRangeStart != intensityRangeEnd && intensityColumnName != string.Empty)
                        {


                            if (intensityRangeStart < intensityRangeEnd)
                            {
                                realValue = (255 / (intensityRangeEnd - intensityRangeStart)) * (GetDoubleValue(feature.ColumnValues[intensityColumnName], intensityRangeStart, intensityRangeEnd) - intensityRangeStart);
                            }
                            else
                            {
                                realValue = (255 / (intensityRangeEnd - intensityRangeStart)) * (intensityRangeEnd - GetDoubleValue(feature.ColumnValues[intensityColumnName], intensityRangeStart, intensityRangeEnd));
                                
                            }
                        }
                        else
                        {
                            realValue = intensity;
                        }

                        HeatPoint heatPoint = new HeatPoint(Convert.ToInt32(screenPoint.X), Convert.ToInt32(screenPoint.Y), Convert.ToByte(realValue));
                        heatPoints.Add(heatPoint);
                    }
                }


                //Calls the function to get the pixel size of the point based on the world point size and the current extent of the map.
                float size = GetPointSize(canvas);
                
                  
                // Create the intensity bitmap
                intensityBitmap = CreateIntensityMask(intensityBitmap, heatPoints, Convert.ToInt32(size));

                // Color the intensity bitmap
                colorBitmap = Colorize(intensityBitmap, (byte)alpha, colorPalette);

                // Create a geoimage from the color bitmap
                pngStream = new MemoryStream();
                colorBitmap.Save(pngStream, System.Drawing.Imaging.ImageFormat.Png);
                geoImage = new GeoImage(pngStream);

                // Write the geoimage to the canvas
                canvas.DrawWorldImageWithoutScaling(geoImage, canvas.CurrentWorldExtent.GetCenterPoint().X, canvas.CurrentWorldExtent.GetCenterPoint().Y, DrawingLevel.LevelOne);
            }
            finally
            {
                // Make sure we clean up all the memeory that we use durring the process
                if (intensityBitmap != null) { intensityBitmap.Dispose(); }
                if (colorBitmap != null) { colorBitmap.Dispose(); }
                if (geoImage != null) { geoImage.Dispose(); }
                if (pngStream != null) { pngStream.Dispose(); }
            }
        }

        private float GetPointSize(GeoCanvas canvas)
        {
            // Calculate the size of the points in pixel based on the world point size and the current extent.
            //Gets the proper point size according to the current extent. (meter is used as base unit)
            double canvasSizeMeter;
            double pointSizeMeter = Conversion.ConvertMeasureUnits(pointSize, pointSizeUnit, DistanceUnit.Meter);
            //if the mapunit is in Decimal Degrees, it uses a different logic than with other mapunits such as meters and feet.
            if (canvas.MapUnit == GeographyUnit.DecimalDegree)
            {
                try
                {
                    canvasSizeMeter = DecimalDegreesHelper.GetDistanceFromDecimalDegrees(canvas.CurrentWorldExtent.UpperLeftPoint.X,
                        canvas.CurrentWorldExtent.UpperLeftPoint.Y, canvas.CurrentWorldExtent.LowerLeftPoint.X, canvas.CurrentWorldExtent.LowerLeftPoint.Y, DistanceUnit.Meter);
                }
                catch { canvasSizeMeter = DecimalDegreesHelper.GetDistanceFromDecimalDegrees(180, 90, 180, -90, DistanceUnit.Meter); }
                finally { }
            }
            else
            {
                DistanceUnit fromUnit = DistanceUnit.Meter;
                if (canvas.MapUnit == GeographyUnit.Feet) fromUnit = DistanceUnit.Feet;
                canvasSizeMeter = Conversion.ConvertMeasureUnits(canvas.CurrentWorldExtent.Height, fromUnit, DistanceUnit.Meter);
            }
            float size = (float)((pointSizeMeter * canvas.Height) / canvasSizeMeter);

            return size;
        }

        // This is retatded and needs to be redone.  First the low and high values could be reversed and 
        // if so we need to consider what to do there
        private static double GetDoubleValue(string value, double lowValue, double highValue)
        {
            double returnValue;

            if (value == "")
            {
                returnValue = lowValue;
            }
            else
            {
                double doubleValue;
                if (double.TryParse(value, out  doubleValue))
                {
                    returnValue = doubleValue;
                }
                else
                {
                    returnValue = lowValue;
                }                
            }

            if (returnValue < lowValue)
            {
                returnValue = lowValue;
            }

            if (returnValue > highValue)
            {
                returnValue = highValue;
            }

            return returnValue;
        }

        protected override Collection<string> GetRequiredColumnNamesCore()
        {
            Collection<string> requiredColumnNames = new Collection<string>();
            if (intensityColumnName.Length > 0)
            {
                requiredColumnNames.Add(intensityColumnName);
            }

            return requiredColumnNames;
        }

        private Bitmap CreateIntensityMask(Bitmap bSurface, List<HeatPoint> aHeatPoints, int radius)
        {
            // Create new graphics surface from memory bitmap
            Graphics DrawSurface = Graphics.FromImage(bSurface);

            // Set background color to white so that pixels can be correctly colorized
            DrawSurface.Clear(Color.White);

            // Traverse heat point data and draw masks for each heat point
            foreach (HeatPoint DataPoint in aHeatPoints)
            {
                // Render current heat point on draw surface
                DrawHeatPoint(DrawSurface, DataPoint, radius);
            }

            return bSurface;
        }

        private void DrawHeatPoint(Graphics Canvas, HeatPoint HeatPoint, int Radius)
        {
            // Create points generic list of points to hold circumference points
            List<Point> CircumferencePointsList = new List<Point>();

            // Create an empty point to predefine the point struct used in the circumference loop
            Point CircumferencePoint;

            // Create an empty array that will be populated with points from the generic list
            Point[] CircumferencePointsArray;

            // Calculate ratio to scale byte intensity range from 0-255 to 0-1
            float fRatio = 1F / Byte.MaxValue;
            // Precalulate half of byte max value
            byte bHalf = Byte.MaxValue / 2;
            // Flip intensity on it's center value from low-high to high-low
            int iIntensity = (byte)(HeatPoint.Intensity - ((HeatPoint.Intensity - bHalf) * 2));
            // Store scaled and flipped intensity value for use with gradient center location
            float fIntensity = iIntensity * fRatio;

            // Loop through all angles of a circle
            // Define loop variable as a double to prevent casting in each iteration
            // Iterate through loop on 10 degree deltas, this can change to improve performance
            for (double i = 0; i <= 360; i += 10)
            {
                // Replace last iteration point with new empty point struct
                CircumferencePoint = new Point();

                // Plot new point on the circumference of a circle of the defined radius
                // Using the point coordinates, radius, and angle
                // Calculate the position of this iterations point on the circle
                CircumferencePoint.X = Convert.ToInt32(HeatPoint.X + Radius * Math.Cos(ConvertDegreesToRadians(i)));
                CircumferencePoint.Y = Convert.ToInt32(HeatPoint.Y + Radius * Math.Sin(ConvertDegreesToRadians(i)));

                // Add newly plotted circumference point to generic point list
                CircumferencePointsList.Add(CircumferencePoint);
            }

            // Populate empty points system array from generic points array list
            // Do this to satisfy the datatype of the PathGradientBrush and FillPolygon methods
            CircumferencePointsArray = CircumferencePointsList.ToArray();

            // Create new PathGradientBrush to create a radial gradient using the circumference points
            PathGradientBrush GradientShaper = new PathGradientBrush(CircumferencePointsArray);

            // Create new color blend to tell the PathGradientBrush what colors to use and where to put them
            ColorBlend GradientSpecifications = new ColorBlend(3);

            // Define positions of gradient colors, use intesity to adjust the middle color to
            // show more mask or less mask
            GradientSpecifications.Positions = new float[3] { 0, fIntensity, 1 };
            // Define gradient colors and their alpha values, adjust alpha of gradient colors to match intensity
            GradientSpecifications.Colors = new Color[3] { Color.FromArgb(0, Color.White), Color.FromArgb(HeatPoint.Intensity, Color.Black), Color.FromArgb(HeatPoint.Intensity, Color.Black) };

            // Pass off color blend to PathGradientBrush to instruct it how to generate the gradient
            GradientShaper.InterpolationColors = GradientSpecifications;

            // Draw polygon (circle) using our point array and gradient brush
            Canvas.FillPolygon(GradientShaper, CircumferencePointsArray);
        }

        private static double ConvertDegreesToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }

        private static Bitmap Colorize(Bitmap Mask, byte Alpha, Collection<GeoColor> colorPalette)
        {
            // Create new bitmap to act as a work surface for the colorization process
            Bitmap Output = new Bitmap(Mask.Width, Mask.Height, PixelFormat.Format32bppArgb);

            // Create a graphics object from our memory bitmap so we can draw on it and clear it's drawing surface
            Graphics Surface = Graphics.FromImage(Output);
            Surface.Clear(Color.Transparent);

            // Build an array of color mappings to remap our greyscale mask to full color
            // Accept an alpha byte to specify the transparancy of the output image
            ColorMap[] Colors = CreatePaletteIndex(Alpha, colorPalette);

            // Create new image attributes class to handle the color remappings
            // Inject our color map array to instruct the image attributes class how to do the colorization
            ImageAttributes Remapper = new ImageAttributes();
            Remapper.SetRemapTable(Colors);

            // Draw our mask onto our memory bitmap work surface using the new color mapping scheme
            Surface.DrawImage(Mask, new Rectangle(0, 0, Mask.Width, Mask.Height), 0, 0, Mask.Width, Mask.Height, GraphicsUnit.Pixel, Remapper);

            // Send back newly colorized memory bitmap
            return Output;
        }

        private static ColorMap[] CreatePaletteIndex(byte Alpha, Collection<GeoColor> colorPalette)
        {
            ColorMap[] OutputMap = new ColorMap[256];

            // Loop through the palette and create a new color mapping
            for (int X = 0; X <= 255; X++)
            {
                OutputMap[X] = new ColorMap();
                OutputMap[X].OldColor = Color.FromArgb(X, X, X);

                // If the user defined an alpha value that is below the one specified for the 
                // entire map then use the user defined one
                if (colorPalette[X].AlphaComponent < Alpha)
                {
                    OutputMap[X].NewColor = Color.FromArgb(colorPalette[X].AlphaComponent, colorPalette[X].RedComponent, colorPalette[X].GreenComponent, colorPalette[X].BlueComponent);
                }
                else
                {
                    OutputMap[X].NewColor = Color.FromArgb(Alpha, colorPalette[X].RedComponent, colorPalette[X].GreenComponent, colorPalette[X].BlueComponent);
                }
            }

            return OutputMap;
        }

        private static Collection<GeoColor> GetDefaultColorPalette()
        {
            Collection<GeoColor> colorPalette = new Collection<GeoColor>();

            colorPalette.Add(GeoColor.FromArgb(255, 254, 248, 248));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 249, 249));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 251, 250));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 252, 251));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 255, 253));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 255, 253));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 255, 255));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 255, 255));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 255, 255));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 255, 255));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 255, 255));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 255, 255));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 255, 255));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 255, 255));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 254, 255));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 253, 253));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 245, 245));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 236, 235));
            colorPalette.Add(GeoColor.FromArgb(255, 254, 223, 221));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 207, 205));
            colorPalette.Add(GeoColor.FromArgb(255, 253, 188, 186));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 170, 167));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 149, 149));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 130, 128));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 109, 109));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 91, 89));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 71, 71));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 53, 53));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 35, 35));
            colorPalette.Add(GeoColor.FromArgb(255, 254, 22, 22));
            colorPalette.Add(GeoColor.FromArgb(255, 254, 10, 10));
            colorPalette.Add(GeoColor.FromArgb(255, 250, 2, 2));
            colorPalette.Add(GeoColor.FromArgb(255, 253, 1, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 253, 1, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 254, 0, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 254, 0, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 253, 1, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 3, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 6, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 9, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 11, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 16, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 20, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 25, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 253, 30, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 253, 35, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 39, 3));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 45, 2));
            colorPalette.Add(GeoColor.FromArgb(255, 254, 48, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 58, 1));
            colorPalette.Add(GeoColor.FromArgb(255, 254, 60, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 254, 67, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 74, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 253, 78, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 87, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 92, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 99, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 253, 105, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 112, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 253, 118, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 253, 124, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 253, 131, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 137, 1));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 142, 1));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 150, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 156, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 161, 1));
            colorPalette.Add(GeoColor.FromArgb(255, 254, 167, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 174, 2));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 180, 1));
            colorPalette.Add(GeoColor.FromArgb(255, 254, 186, 1));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 192, 1));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 197, 2));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 203, 3));
            colorPalette.Add(GeoColor.FromArgb(255, 254, 208, 1));
            colorPalette.Add(GeoColor.FromArgb(255, 253, 212, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 254, 218, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 254, 222, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 254, 225, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 231, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 235, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 254, 239, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 241, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 245, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 246, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 250, 1));
            colorPalette.Add(GeoColor.FromArgb(255, 255, 251, 1));
            colorPalette.Add(GeoColor.FromArgb(255, 253, 252, 1));
            colorPalette.Add(GeoColor.FromArgb(255, 251, 252, 1));
            colorPalette.Add(GeoColor.FromArgb(255, 247, 252, 0));
            colorPalette.Add(GeoColor.FromArgb(255, 245, 251, 3));
            colorPalette.Add(GeoColor.FromArgb(255, 241, 252, 2));
            colorPalette.Add(GeoColor.FromArgb(255, 237, 251, 5));
            colorPalette.Add(GeoColor.FromArgb(255, 233, 252, 4));
            colorPalette.Add(GeoColor.FromArgb(255, 231, 251, 6));
            colorPalette.Add(GeoColor.FromArgb(255, 226, 252, 5));
            colorPalette.Add(GeoColor.FromArgb(255, 221, 251, 5));
            colorPalette.Add(GeoColor.FromArgb(255, 216, 252, 4));
            colorPalette.Add(GeoColor.FromArgb(255, 212, 252, 6));
            colorPalette.Add(GeoColor.FromArgb(255, 207, 251, 6));
            colorPalette.Add(GeoColor.FromArgb(255, 202, 252, 7));
            colorPalette.Add(GeoColor.FromArgb(255, 198, 252, 10));
            colorPalette.Add(GeoColor.FromArgb(255, 191, 250, 8));
            colorPalette.Add(GeoColor.FromArgb(255, 186, 249, 10));
            colorPalette.Add(GeoColor.FromArgb(255, 180, 247, 9));
            colorPalette.Add(GeoColor.FromArgb(255, 176, 246, 10));
            colorPalette.Add(GeoColor.FromArgb(255, 168, 244, 11));
            colorPalette.Add(GeoColor.FromArgb(255, 163, 242, 11));
            colorPalette.Add(GeoColor.FromArgb(255, 158, 241, 13));
            colorPalette.Add(GeoColor.FromArgb(255, 152, 239, 13));
            colorPalette.Add(GeoColor.FromArgb(255, 147, 235, 13));
            colorPalette.Add(GeoColor.FromArgb(255, 140, 231, 12));
            colorPalette.Add(GeoColor.FromArgb(255, 135, 232, 15));
            colorPalette.Add(GeoColor.FromArgb(255, 129, 230, 16));
            colorPalette.Add(GeoColor.FromArgb(255, 122, 226, 15));
            colorPalette.Add(GeoColor.FromArgb(255, 116, 224, 17));
            colorPalette.Add(GeoColor.FromArgb(255, 110, 221, 20));
            colorPalette.Add(GeoColor.FromArgb(255, 104, 217, 21));
            colorPalette.Add(GeoColor.FromArgb(255, 96, 216, 22));
            colorPalette.Add(GeoColor.FromArgb(255, 92, 215, 23));
            colorPalette.Add(GeoColor.FromArgb(255, 87, 212, 22));
            colorPalette.Add(GeoColor.FromArgb(255, 82, 211, 23));
            colorPalette.Add(GeoColor.FromArgb(255, 75, 207, 25));
            colorPalette.Add(GeoColor.FromArgb(255, 70, 205, 25));
            colorPalette.Add(GeoColor.FromArgb(255, 66, 202, 30));
            colorPalette.Add(GeoColor.FromArgb(255, 60, 200, 31));
            colorPalette.Add(GeoColor.FromArgb(255, 56, 197, 33));
            colorPalette.Add(GeoColor.FromArgb(255, 50, 194, 34));
            colorPalette.Add(GeoColor.FromArgb(255, 43, 193, 34));
            colorPalette.Add(GeoColor.FromArgb(255, 38, 192, 36));
            colorPalette.Add(GeoColor.FromArgb(255, 36, 189, 36));
            colorPalette.Add(GeoColor.FromArgb(255, 31, 188, 37));
            colorPalette.Add(GeoColor.FromArgb(255, 29, 187, 41));
            colorPalette.Add(GeoColor.FromArgb(255, 24, 185, 43));
            colorPalette.Add(GeoColor.FromArgb(255, 19, 184, 45));
            colorPalette.Add(GeoColor.FromArgb(255, 15, 183, 46));
            colorPalette.Add(GeoColor.FromArgb(255, 12, 182, 49));
            colorPalette.Add(GeoColor.FromArgb(255, 8, 182, 51));
            colorPalette.Add(GeoColor.FromArgb(255, 6, 181, 52));
            colorPalette.Add(GeoColor.FromArgb(255, 4, 180, 55));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 179, 57));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 178, 61));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 179, 62));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 180, 65));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 181, 68));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 181, 70));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 182, 72));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 182, 77));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 183, 82));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 184, 86));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 185, 89));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 186, 91));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 188, 97));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 190, 100));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 192, 103));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 194, 108));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 195, 113));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 197, 118));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 198, 121));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 199, 126));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 201, 131));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 204, 135));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 205, 140));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 208, 143));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 210, 149));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 212, 154));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 213, 157));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 214, 162));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 216, 167));
            colorPalette.Add(GeoColor.FromArgb(255, 2, 219, 174));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 222, 181));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 224, 187));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 228, 195));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 230, 201));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 232, 206));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 233, 211));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 234, 219));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 234, 223));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 234, 225));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 234, 230));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 234, 235));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 234, 239));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 234, 243));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 233, 247));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 235, 249));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 234, 251));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 233, 255));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 233, 255));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 229, 255));
            colorPalette.Add(GeoColor.FromArgb(255, 2, 227, 255));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 222, 255));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 217, 252));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 213, 253));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 206, 248));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 200, 249));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 194, 247));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 185, 242));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 177, 239));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 169, 236));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 160, 230));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 153, 226));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 144, 220));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 136, 214));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 127, 208));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 120, 205));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 116, 203));
            colorPalette.Add(GeoColor.FromArgb(255, 2, 111, 202));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 105, 197));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 97, 197));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 91, 192));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 82, 186));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 76, 182));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 69, 180));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 62, 175));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 54, 168));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 50, 165));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 43, 158));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 39, 153));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 33, 149));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 30, 143));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 24, 140));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 20, 135));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 15, 130));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 13, 127));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 11, 119));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 10, 115));
            colorPalette.Add(GeoColor.FromArgb(255, 0, 7, 111));
            colorPalette.Add(GeoColor.FromArgb(255, 1, 5, 104));
            colorPalette.Add(GeoColor.FromArgb(255, 2, 5, 100));
            colorPalette.Add(GeoColor.FromArgb(255, 3, 4, 94));
            colorPalette.Add(GeoColor.FromArgb(255, 4, 4, 90));
            colorPalette.Add(GeoColor.FromArgb(255, 4, 5, 87));
            colorPalette.Add(GeoColor.FromArgb(255, 6, 5, 85));
            colorPalette.Add(GeoColor.FromArgb(255, 7, 6, 82));
            colorPalette.Add(GeoColor.FromArgb(255, 6, 9, 80));
            colorPalette.Add(GeoColor.FromArgb(255, 7, 10, 77));
            colorPalette.Add(GeoColor.FromArgb(255, 11, 12, 77));
            colorPalette.Add(GeoColor.FromArgb(255, 13, 15, 76));
            colorPalette.Add(GeoColor.FromArgb(255, 16, 16, 76));
            colorPalette.Add(GeoColor.FromArgb(255, 16, 17, 73));
            colorPalette.Add(GeoColor.FromArgb(255, 20, 20, 74));
            colorPalette.Add(GeoColor.FromArgb(255, 22, 22, 72));
            colorPalette.Add(GeoColor.FromArgb(255, 27, 25, 74));
            colorPalette.Add(GeoColor.FromArgb(255, 30, 29, 73));
            colorPalette.Add(GeoColor.FromArgb(255, 31, 31, 69));
            colorPalette.Add(GeoColor.FromArgb(255, 33, 33, 67));
            colorPalette.Add(GeoColor.FromArgb(255, 37, 36, 67));
            colorPalette.Add(GeoColor.FromArgb(255, 39, 39, 65));
            colorPalette.Add(GeoColor.FromArgb(255, 41, 41, 67));
            colorPalette.Add(GeoColor.FromArgb(255, 44, 45, 66));
            colorPalette.Add(GeoColor.FromArgb(255, 46, 44, 65));
            colorPalette.Add(GeoColor.FromArgb(255, 48, 47, 63));
            colorPalette.Add(GeoColor.FromArgb(255, 49, 48, 62));
            colorPalette.Add(GeoColor.FromArgb(255, 50, 50, 60));
            colorPalette.Add(GeoColor.FromArgb(255, 54, 52, 63));
            colorPalette.Add(GeoColor.FromArgb(0, 53, 52, 60));

            return colorPalette;
        }
    }
}
