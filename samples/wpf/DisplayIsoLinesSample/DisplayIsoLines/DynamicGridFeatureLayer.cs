using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.Layers
{
    public class DynamicGridFeatureLayer : InMemoryGridFeatureLayer
    {
        private double cellHeightInPixel;
        private double cellWidthInPixel;
        private GridInterpolationModel interploationModel;
        private Dictionary<PointShape, double> wellDepthPoints;

        public DynamicGridFeatureLayer(Dictionary<PointShape, double> wellDepthPoints)
            : base()
        {
            this.wellDepthPoints = wellDepthPoints;
        }

        public double CellHeightInPixel
        {
            get { return cellHeightInPixel; }
            set { cellHeightInPixel = value; }
        }

        public double CellWidthInPixel
        {
            get { return cellWidthInPixel; }
            set { cellWidthInPixel = value; }
        }

        public GridInterpolationModel InterpolationModel
        {
            get
            {
                if (interploationModel == null)
                {
                    interploationModel = new InverseDistanceWeightedGridInterpolationModel(2, double.MaxValue);
                }
                return interploationModel;
            }
            set { interploationModel = value; }
        }

        public Dictionary<PointShape, double> WellDepthPoints
        {
            get { return wellDepthPoints; }
            set { wellDepthPoints = value; }
        }

        protected static double GetResolutionFromScale(double scale, GeographyUnit mapUnit)
        {
            double resolution = scale / (GetInchesPerUnit(mapUnit) * 96);
            return resolution;
        }

        protected override void DrawCore(GeoCanvas canvas, Collection<SimpleCandidate> labelsInAllLayers)
        {
            double resolution = GetResolutionFromScale(canvas.CurrentScale, canvas.MapUnit);

            double cellSize = Math.Min(cellHeightInPixel * resolution, cellWidthInPixel * resolution);
            GridDefinition gridDefinition = new GridDefinition(canvas.CurrentWorldExtent, cellSize, NoDataValue, wellDepthPoints);
            GridCell[,] gridMatrix = GridFeatureSource.GenerateGridMatrix(gridDefinition, InterpolationModel);

            this.GridMatrix = gridMatrix;

            this.FeatureSource.Close();
            this.FeatureSource.Open();

            base.DrawCore(canvas, labelsInAllLayers);
        }

        protected override void OpenCore()
        { }

        private static double GetInchesPerUnit(GeographyUnit mapUnit)
        {
            double inchesPerUnit = 0;

            switch (mapUnit)
            {
                case GeographyUnit.DecimalDegree:
                    inchesPerUnit = 4374754;
                    break;

                case GeographyUnit.Feet:
                    inchesPerUnit = 12;
                    break;

                case GeographyUnit.Meter:
                    inchesPerUnit = 39.3701;
                    break;

                default:
                    break;
            }

            return inchesPerUnit;
        }
    }
}