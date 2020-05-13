using System;
using ThinkGeo.MapSuite.Shapes;

namespace WrapDatelineMode
{
    class WrapDatelineProjection : Projection, IDisposable 
    {
        private double nHalfExtentWidth;
        private double [] an = new double[100];
        private double[] an_minus = new double[100];

        //Property for half the extent of the central map.
        //For example:
        //If the map is in decimal degree, the value will be 180 (worldMapKitWmsWpfOverlay.GetBoundingBox().Width / 2).
        //If the map is using Google map, the value will be 14000955.5 (googleMapsOverlay.GetBoundingBox().Width / 2)
        public double HalfExtentWidth
        {
            get { return nHalfExtentWidth; }
            set 
            { 
                nHalfExtentWidth = value; 

                double sum = 0;
                for (int i = 0; i <= an.Length - 1; i += 1)
                {
                    an[i] = nHalfExtentWidth + sum;
                    sum = sum + (nHalfExtentWidth * 2);
                }

                sum = 0;
                for (int i = 0; i <= an_minus.Length - 1; i += 1)
                {
                    an_minus[i] = -nHalfExtentWidth - sum;
                    sum = sum + (nHalfExtentWidth * 2);
                }
            }
        }

        protected override Vertex[] ConvertToExternalProjectionCore(double[] x, double[] y)
        {
            Vertex[] vertices = new Vertex[x.Length];

            for (int i = 0; i < vertices.Length; i++)
            {
                if ((x[i] > HalfExtentWidth)|| (x[i] < (-HalfExtentWidth)))
                {
                double realx = GetAp(x[i]);
                vertices[i] = new Vertex(realx, y[i]);
                }
                else
                {
                    vertices[i] = new Vertex(x[i], y[i]);
                }
            }
            return vertices;
        }

        private double GetAp(double Xp)
        {
            double result = 0;
            if (Xp > 0)
            {
                for (int i = 0; i < an.Length - 2; i++)
                {
                    if ((Xp >= an[i]) && (Xp < an[i + 1]))
                    {
                        result = Xp - (an[i] + HalfExtentWidth);
                        break;
                    }
                }
            }
            else
            {
                for (int i = 0; i < an_minus.Length - 2; i++)
                {
                    if ((Xp <= an_minus[i]) && (Xp > an_minus[i + 1]))
                    {
                        result = Xp + (an[i] + HalfExtentWidth);
                        break;
                    }
                }
            }
            return result;
        }

        protected override Vertex[] ConvertToInternalProjectionCore(double[] x, double[] y)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            Close();
        }
    }
}
