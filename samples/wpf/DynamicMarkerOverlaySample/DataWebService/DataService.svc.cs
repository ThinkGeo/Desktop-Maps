using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DataWebService
{
    public class DataService : IDataService
    {
        public List<FacilityModel> GetFacilities()
        {
            List<FacilityModel> facilities = FacilityDataMocker.Generate();

            return facilities;
        }
    }
}
