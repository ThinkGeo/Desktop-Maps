using System;
using System.Runtime.Serialization;

namespace DataWebService
{
    [DataContract]
    public class FacilityModel
    {
        [DataMember]
        public string AirportCode { get; set; }

        [DataMember]
        public DateTime DateTime { get; set; }

        [DataMember]
        public int Packages { get; set; }

        [DataMember]
        public int Skids { get; set; }

        [DataMember]
        public double PackageAverage { get; set; }

        [DataMember]
        public double SkidAverage { get; set; }
    }
}