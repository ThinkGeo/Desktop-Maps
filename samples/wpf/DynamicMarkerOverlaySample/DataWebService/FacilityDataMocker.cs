using System;
using System.Collections.Generic;

namespace DataWebService
{
    /// <summary>
    /// This class is used to mock the whole day data of facilities.
    /// </summary>
    internal class FacilityDataMocker
    {
        private const int maxPackageValue = 15000;
        private const int minPackageValue = 3000;
        private const int maxSkidValue = 300;
        private const int minSkidValue = 100;

        /// <summary>
        ///  generate data for facilities.
        /// </summary>
        /// <returns></returns>
        public static List<FacilityModel> Generate()
        {
            string[] airportCodes = new[] {
                                            "CYHM", "MTG", "YVR", "YYC", "YHZ", "YYZ", "YUL",
                                            "YOW", "YTZ", "YZR", "YWG", "CYKZ", "YYY", "DFW",
                                            "ATL", "BOS", "BUF", "ORD", "CVG", "CLE", "DEN",
                                            "DTW", "HOU", "IND", "LAX", "MIA", "MKE", "MSP",
                                            "BNA", "JFK", "PHL", "PIT", "RDU", "SLC", "SFO",
                                            "SEA"
                                        };
            List<FacilityModel> result = new List<FacilityModel>();

            var packageRandomer = new Random();
            var skidRandomer = new Random();
            foreach (string airportCode in airportCodes)
            {
                int packages = packageRandomer.Next(minPackageValue, maxPackageValue);
                int averagePackages = packageRandomer.Next(minPackageValue, maxPackageValue);
                int skids = skidRandomer.Next(minSkidValue, maxSkidValue);
                int averageSkids = skidRandomer.Next(minSkidValue, maxSkidValue);

                var currentItem = new FacilityModel()
                {
                    AirportCode = airportCode,
                    DateTime = DateTime.Now,
                    Packages = packages,
                    Skids = skids,
                    PackageAverage = averagePackages,
                    SkidAverage = averageSkids
                };

                result.Add(currentItem);
            }

            return result;
        }
    }
}