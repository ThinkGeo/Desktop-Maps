using System;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;

namespace Projections
{
    //This projection class allows to project and rotate at the same time.
    class CustomRotationProjection : Projection, IDisposable
    {
        private Proj4Projection proj4 = new Proj4Projection();
        private RotationProjection rotateProjection = new RotationProjection();
        private double angle;
        private Vertex pivotVertex;

        public CustomRotationProjection()
            : base()
        {

        }

        public CustomRotationProjection(String InternalProjectionString, String ExternalProjectionString)
            : base()
        {
            proj4.InternalProjectionParametersString = InternalProjectionString;
            proj4.ExternalProjectionParametersString = ExternalProjectionString;
        }

        public string InternalProjectionString
        {
            get
            {
                return proj4.InternalProjectionParametersString;
            }
            set
            {
                proj4.InternalProjectionParametersString = value;
            }
        }

        public string ExternalProjectionString
        {
            get
            {
                return proj4.ExternalProjectionParametersString;
            }
            set
            {
                proj4.ExternalProjectionParametersString = value;
            }
        }

        public double Angle
        {
            get
            {
                return angle;
            }
            set
            {
                angle = value;
                rotateProjection.Angle = angle;
            }
        }
        public Vertex PivotVertex
        {
            get
            {
                return pivotVertex;
            }
            set
            {
                pivotVertex = value;
            }
        }

        protected override Vertex[] ConvertToExternalProjectionCore(double[] x, double[] y)
        {
            //First, converts to the external projection
            Vertex[] projVertices = new Vertex[x.Length];
            proj4.Open();
            for (int i = 0; i < projVertices.Length; i++)
            {
                projVertices[i] = proj4.ConvertToExternalProjection(x[i], y[i]);
            }
            proj4.Close();

            Vertex[] rotateVertices = new Vertex[x.Length];

            //Second, rotates based on angle and pivot point.
            for (int i = 0; i < rotateVertices.Length; i++)
            {
                rotateVertices[i] = RotateVertex(projVertices[i].X, projVertices[i].Y, angle);
            }

            return rotateVertices;
        }

        private Vertex RotateVertex(double x, double y, double angle)
        {
            Vertex rotatedVertex = new Vertex(x, y);
            if ((angle % 360) != 0)
            {
                double rotatedX = x;
                double rotatedY = y;

                double distanceToPivot = Math.Sqrt(Math.Pow((x - pivotVertex.X), 2) + Math.Pow((y - pivotVertex.Y), 2));
                if (distanceToPivot != 0)
                {
                    double beta = Math.Atan((y - pivotVertex.Y) / (x - pivotVertex.X));
                    if ((beta <= 0 | y < pivotVertex.Y) && x < pivotVertex.X)
                    {
                        beta = Math.PI + beta;
                    }

                    double radiantAngle = angle * Math.PI / 180;
                    rotatedX = (distanceToPivot * Math.Cos(radiantAngle + beta)) + pivotVertex.X;
                    rotatedY = (distanceToPivot * Math.Sin(radiantAngle + beta)) + pivotVertex.Y;
                }

                rotatedVertex = new Vertex(rotatedX, rotatedY);
            }

            return rotatedVertex;
        }

        protected override Vertex[] ConvertToInternalProjectionCore(double[] x, double[] y)
        {
            //Converts back to internal projection with the rotation.
            Vertex[] vertices = new Vertex[x.Length];

            proj4.Open();
            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = proj4.ConvertToInternalProjection(x[i], y[i]);
            }
            proj4.Close();

            return vertices;
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
