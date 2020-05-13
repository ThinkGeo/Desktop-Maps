using System.Collections.Generic;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.SiteSelection
{
    public abstract class ServiceAreaAnalysis
    {
        protected ServiceAreaAnalysis()
        { }

        public Feature CreateServiceAreaFeature(PointShape pointShape, GeographyUnit geographyUnit,
            IDictionary<string, object> parameters)
        {
            AreaBaseShape areaShape = CreateServiceArea(pointShape, geographyUnit, parameters);
            return new Feature(areaShape);
        }

        public abstract AreaBaseShape CreateServiceArea(PointShape pointShape, GeographyUnit geographyUnit, IDictionary<string, object> parameters);
    }
}