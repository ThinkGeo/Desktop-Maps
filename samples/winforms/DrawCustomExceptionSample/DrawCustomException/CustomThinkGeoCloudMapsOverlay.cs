using System;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.WinForms;

namespace DrawCustomException
{
    // We inherit from the exisitng overlay to create a new one that helps us simulate
    // going on and offline and drawing a custom exception.  If you used this in production
    // you would only need to override the DrawExceptionCore method and set the this.DrawExceptionMode
    // The rest of the code in this class is just used to simulate going on and offline and
    // showing you the difference between the default drawing and custom drawing of an exception
    public class CustomThinkGeoCloudMapsOverlay : ThinkGeoCloudRasterMapsOverlay
    {
        private bool drawCustomException;
        private bool online;

        // In this public constructor we set the drawing exception mode.
        public CustomThinkGeoCloudMapsOverlay()
            : base(string.Empty, string.Empty)
        { }

        public CustomThinkGeoCloudMapsOverlay(string clientId, string clientSecret)
        {
            this.DrawingExceptionMode = DrawingExceptionMode.DrawException;
            drawCustomException = false;
            online = true;
        }

        // This property is just fir us to simulate switching
        // bewtween custom and standard exception drawing
        public bool DrawCustomException
        {
            get { return drawCustomException; }
            set { drawCustomException = value; }
        }

        // This just simulates being on and offline by throwing
        // an error if you are offline
        public bool Online
        {
            get { return online; }
            set { online = value; }
        }

        // This code is just here to simulate being on and offline
        protected override void DrawCore(GeoCanvas canvas)
        {
            if (online)
            {
                base.DrawCore(canvas);
            }
            else
            {
                throw new Exception("Unable to connect to the ThinkGeo Cloud Map server.");
            }
        }

        // Here we override the DrawException method and do our own drawing of the exception.
        // We use the drawCustomException only for the sample
        protected override void DrawExceptionCore(GeoCanvas canvas, Exception e)
        {
            if (drawCustomException)
            {
                // We are writing directly to the canvas.  Note that above we have access to the exception itself
                // and we could make the image based on that.
                canvas.DrawTextWithScreenCoordinate(e.Message, new GeoFont("Arial", 20), new GeoSolidBrush(new GeoColor(120, GeoColor.StandardColors.Red)), canvas.Width / 2, canvas.Height / 2, DrawingLevel.LevelOne);
            }
            else
            {
                base.DrawExceptionCore(canvas, e);
            }
        }
    }
}
