using System;
using System.Collections.Generic;
using System.IO;
using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.Routing;
using ThinkGeo.MapSuite.SiteSelection.Properties;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public class RouteAccessibleAreaAnalysis : ServiceAreaAnalysis
    {
        public const int DefaultDriveTimeInMinute = 6;

        public RouteAccessibleAreaAnalysis()
            : base()
        { }

        public override AreaBaseShape CreateServiceArea(PointShape pointShape, GeographyUnit geographyUnit, IDictionary<string, object> parameters)
        {
            int tempDrivingTimeInMinutes = DefaultDriveTimeInMinute;
            string streetShapePathFilename = (string)parameters[Resources.StreetShapePathFilenameKey];
            if (parameters.ContainsKey(Resources.DriveTimeInMinutesKey)) tempDrivingTimeInMinutes = (int)parameters[Resources.DriveTimeInMinutesKey];

            string rtgFilePathName = Path.ChangeExtension(streetShapePathFilename, ".rtg");
            RtgRoutingSource routingSource = new RtgRoutingSource(rtgFilePathName);
            FeatureSource featureSource = new ShapeFileFeatureSource(streetShapePathFilename);
            RoutingEngine routingEngine = new RoutingEngine(routingSource, featureSource);

            ManagedProj4Projection projection = new ManagedProj4Projection();
            projection.InternalProjectionParametersString = ManagedProj4Projection.GetBingMapParametersString();
            projection.ExternalProjectionParametersString = ManagedProj4Projection.GetEpsgParametersString(4326);

            featureSource.Open();
            projection.Open();
            pointShape = projection.ConvertToExternalProjection(pointShape) as PointShape;
            Feature feature = featureSource.GetFeaturesNearestTo(pointShape, geographyUnit, 1, ReturningColumnsType.NoColumns)[0];
            PolygonShape polygonShape = routingEngine.GenerateServiceArea(feature.Id, new TimeSpan(0, tempDrivingTimeInMinutes, 0), 100, GeographyUnit.Feet);
            polygonShape = (PolygonShape)projection.ConvertToInternalProjection(polygonShape);
            projection.Close();

            return polygonShape;
        }
    }
}