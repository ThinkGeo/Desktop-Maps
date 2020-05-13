using System.Collections.Generic;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.SiteSelection.Properties;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public class BufferServiceAreaAnalysis : ServiceAreaAnalysis
    {
        public BufferServiceAreaAnalysis()
        { }

        public override AreaBaseShape CreateServiceArea(PointShape pointShape, GeographyUnit geographyUnit,
            IDictionary<string, object> parameters)
        {
            double distance = 2;
            DistanceUnit distanceUnit = DistanceUnit.Mile;

            if (parameters.ContainsKey(Resources.DistanceUnitKey)) distanceUnit = (DistanceUnit)parameters[Resources.DistanceUnitKey];
            if (parameters.ContainsKey(Resources.DistanceKey)) distance = (double)parameters[Resources.DistanceKey];
            return pointShape.Buffer(distance, 40, geographyUnit, distanceUnit);
        }
    }
}