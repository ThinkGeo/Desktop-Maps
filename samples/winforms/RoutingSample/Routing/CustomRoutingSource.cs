using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ThinkGeo.MapSuite.Core;
using System.Collections.ObjectModel;

namespace ThinkGeo.MapSuite.Routing
{
    //TODO: This class will move to sample project, it can show how to extent RoutingSource.
    public class CustomRoutingSource : RoutingSource
    {
        private FeatureSource featureSource;

        public CustomRoutingSource()
            : this(null)
        {
        }

        public override int RoadCount
        {
            get
            {
                return featureSource.GetCount();
            }
        }

        protected override void OpenCore()
        {
            base.OpenCore();
            featureSource.Open();
        }

        protected override void CloseCore()
        {
            base.CloseCore();
            featureSource.Close();
        }

        public CustomRoutingSource(FeatureSource featureSource)
            : base()
        {
            this.featureSource = featureSource;
        }

        protected override string GetFeatureIdByroadIdCore(string roadId)
        {
            return roadId;
        }

        protected override FeatureSource GetFeatureSourceByroadIdCore(string roadId)
        {
            return featureSource;
        }

        protected override Road GetRoadByIdCore(string roadId)
        {
            Feature feature = featureSource.GetFeatureById(roadId, ReturningColumnsType.NoColumns);

            LineBaseShape sourceShape = (LineBaseShape)feature.GetShape();
            Collection<LineShape> lines = ConvertLineBaseShapeToLines(sourceShape);

            List<string> adjacentIDs = new List<string>();
            foreach (LineShape line in lines)
            {
                Collection<Feature> features = featureSource.GetFeaturesInsideBoundingBox(line.GetBoundingBox(), ReturningColumnsType.NoColumns);
                foreach (Feature tempFeature in features)
                {
                    BaseShape tempShape = tempFeature.GetShape();
                    if (feature.Id != tempFeature.Id && line.Intersects(tempShape))
                    {
                        adjacentIDs.Add(tempFeature.Id);
                    }
                }
            }
            //adjacentIDs.Sort();

            PointShape center = sourceShape.GetCenterPoint();
            float centerX = (float)center.X;
            float centerY = (float)center.Y;
            float length = (float)sourceShape.GetLength(GeographyUnit.Meter, DistanceUnit.Meter);
            Road road = new Road(roadId, RoadType.LocalRoad, length, adjacentIDs, centerX, centerY, roadId);

            return road;
        }
    }
}
