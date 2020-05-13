using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public abstract class TrackingDataProvider : IDisposable
    {
        public abstract Dictionary<int, Vehicle> GetCurrentVehicles(DateTime currentTime);

        public abstract Collection<Feature> GetSpatialFences();

        public abstract void DeleteSpatialFencesExcluding(IEnumerable<Feature> excludeFeatures);

        public abstract int UpdateSpatialFenceByFeature(Feature feature);

        public abstract void InsertSpatialFence(Feature feature);

        public abstract void Dispose();
    }
}