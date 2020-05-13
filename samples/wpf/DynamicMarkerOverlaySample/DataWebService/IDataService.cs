using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace DataWebService
{
    [ServiceContract(Name = "DataService")]
    public interface IDataService
    {
        /// <summary>
        /// Retrieve latest data for facilities
        /// </summary>
        /// <returns></returns>
        [WebGet]
        [OperationContract]
        List<FacilityModel> GetFacilities();

    }
}
