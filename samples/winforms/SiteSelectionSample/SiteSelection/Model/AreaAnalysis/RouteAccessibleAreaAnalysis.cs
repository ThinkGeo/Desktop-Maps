using System;
using System.Collections.Generic;
using System.IO;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Routing;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.SiteSelection.Properties;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public class RouteAccessibleAreaAnalysis : ServiceAreaAnalysis
    {
        public RouteAccessibleAreaAnalysis()
        { }

        public override AreaBaseShape CreateServiceArea(PointShape pointShape, GeographyUnit geographyUnit, IDictionary<string, object> parameters)
        {
            int tempDrivingTimeInMinutes = 6;
            string streetShapePathFilename = (string)parameters[Resources.StreetShapePathFilenameKey];
            if (parameters.ContainsKey(Resources.DriveTimeInMinutesKey)) tempDrivingTimeInMinutes = (int)parameters[Resources.DriveTimeInMinutesKey];

            string rtgFilePathName = Path.ChangeExtension(streetShapePathFilename, ".rtg");
            RtgRoutingSource routingSource = new RtgRoutingSource(rtgFilePathName);
            FeatureSource featureSource = new ShapeFileFeatureSource(streetShapePathFilename);
            RoutingEngine routingEngine = new RoutingEngine(routingSource, featureSource);

            Proj4Projection projection = new Proj4Projection();
            projection.InternalProjectionParametersString = Proj4Projection.GetBingMapParametersString();
            projection.ExternalProjectionParametersString = Proj4Projection.GetEpsgParametersString(4326);

            projection.Open();
            featureSource.Open();
            pointShape = projection.ConvertToExternalProjection(pointShape) as PointShape;
            Feature feature = featureSource.GetFeaturesNearestTo(pointShape, geographyUnit, 1, ReturningColumnsType.NoColumns)[0];
            PolygonShape polygonShape = routingEngine.GenerateServiceArea(feature.Id, new TimeSpan(0, tempDrivingTimeInMinutes, 0), 100, GeographyUnit.Feet);
            polygonShape = (PolygonShape)projection.ConvertToInternalProjection(polygonShape);
            projection.Close();

            return polygonShape;
        }
    }
}