using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using ThinkGeo.MapSuite.Shapes;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public class TrackingAccessProvider
    {
        private string dataRootPath;

        public TrackingAccessProvider(string dataRootPath)
        {
            this.dataRootPath = dataRootPath;
        }

        public Dictionary<int, Vehicle> GetCurrentVehicles(DateTime currentTime)
        {
            Dictionary<int, Vehicle> vehiclesList = new Dictionary<int, Vehicle>();

            var path = Path.Combine(dataRootPath, "Vehicle.txt");
            var records = ParseCsv(path);
            TimeSpan trackHistoryVehicleTimeSpan = TimeSpan.FromHours(8);
            foreach (var record in records)
            {
                int vehicleId = Convert.ToInt32(record[1], CultureInfo.InvariantCulture);
                Vehicle vehicle = GetCurrentVehicle(vehicleId, currentTime, trackHistoryVehicleTimeSpan);
                vehiclesList.Add(vehicle.Id, vehicle);
            }

            return vehiclesList;
        }

        private Vehicle GetCurrentVehicle(int vehicleId, DateTime currentTime, TimeSpan trackHistoryVehicleTimeSpan)
        {
            DateTime trackStartTime = currentTime.AddTicks(-trackHistoryVehicleTimeSpan.Ticks);
            Vehicle currentVechicle = new Vehicle(vehicleId);

            var vehicleFilePath = Path.Combine(dataRootPath, "Vehicle.txt");
            var vehicleRecords = ParseCsv(vehicleFilePath);
            foreach (var vehicleRecord in vehicleRecords)
            {
                int id = Convert.ToInt32(vehicleRecord[1], CultureInfo.InvariantCulture);
                if (id == vehicleId)
                {
                    currentVechicle.Id = vehicleId;
                    currentVechicle.Name = vehicleRecord[0];
                    currentVechicle.IconPath = vehicleRecord[2];
                    break;
                }
            }

            // Get the locations from current time back to the passed time span
            Collection<double> historySpeeds = new Collection<double>();
            var locationFilePath = Path.Combine(dataRootPath, "Location.txt");
            var records = ParseCsv(locationFilePath).Where(r =>
            {
                DateTime dateTime = Convert.ToDateTime(r[4], CultureInfo.InvariantCulture);
                return (r[1] == vehicleId.ToString()) && dateTime <= currentTime && dateTime >= trackStartTime;
            }).OrderByDescending(r => r[4]).ToList();
            for (int rowIndex = 0; rowIndex < records.Count; rowIndex++)
            {
                var columns = records[rowIndex];
                double latitude = Convert.ToDouble(columns[3], CultureInfo.InvariantCulture);
                double longitude = Convert.ToDouble(columns[2], CultureInfo.InvariantCulture);
                double speed = Convert.ToDouble(columns[5], CultureInfo.InvariantCulture);
                DateTime dateTime = Convert.ToDateTime(columns[4], CultureInfo.InvariantCulture);
                Location currentLocation = new Location(longitude, latitude, speed, dateTime);
                historySpeeds.Add(speed);

                if (rowIndex == 0)
                {
                    currentVechicle.Location = currentLocation;
                }
                else
                {
                    currentVechicle.HistoryLocations.Add(currentLocation);
                }
            }

            return currentVechicle;
        }

        public Collection<Feature> GetSpatialFences()
        {
            Collection<Feature> spatialFences = new Collection<Feature>();
            var path = Path.Combine(dataRootPath, "SpatialFence.txt");
            var records = ParseCsv(path);
            foreach (var record in records)
            {
                string wkt = record[1];
                string id = record[0];
                spatialFences.Add(new Feature(wkt, id));
            }
            return spatialFences;
        }

        public void DeleteSpatialFencesExcluding(IEnumerable<Feature> features)
        {
            List<string> resultRecords = new List<string>();
            var path = Path.Combine(dataRootPath, "SpatialFence.txt");
            var records = ParseCsv(path);
            foreach (var record in records)
            {
                bool needDelete = false;
                foreach (Feature feature in features)
                {
                    if (feature.Id == record[0])
                    {
                        record[1] = $"\"{record[1]}\"";
                        resultRecords.Add(string.Join(",", record));
                        break;
                    }
                }
            }

            File.WriteAllLines(path, resultRecords);
        }

        public void UpdateSpatialFenceByFeature(Feature feature)
        {
            var path = Path.Combine(dataRootPath, "SpatialFence.txt");
            var records = ParseCsv(path);
            bool isAdded = false;
            int number = 1;
            List<string> result = new List<string>();
            foreach (var record in records)
            {
                if (record[0] == feature.Id)
                {
                    record[1] = $"\"{feature.GetWellKnownText()}\"";
                    result.Add(string.Join(",", record));
                    isAdded = true;
                }
                else
                {
                    record[1] = $"\"{record[1]}\"";
                    result.Add(string.Join(",", record));

                }
            }
            if (!isAdded)
            {
                List<string> record = new List<string>();
                record.Add(feature.Id);
                record.Add($"\"{feature.GetWellKnownText()}\"");
                result.Add(string.Join(",", record));
            }

            File.WriteAllLines(path, result);
        }

        public void InsertSpatialFence(Feature feature)
        {
            var path = Path.Combine(dataRootPath, "SpatialFence.txt");
            var records = ParseCsv(path);
            Dictionary<int, string> result = new Dictionary<int, string>();
            foreach (var record in records)
            {
                result.Add(int.Parse(record[0]), string.Join(",", record));
            }

            result = result.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            var latestId = result.ElementAt(result.Count - 1).Key;
            latestId += 1;
            result.Add(latestId, $"{latestId},\"{feature.GetWellKnownText()}\",{feature.Id}");

            File.WriteAllLines(path, result.Values);
        }


        private List<List<string>> ParseCsv(string filePath)
        {
            List<List<string>> result = new List<List<string>>();

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();
                    List<string> oneLine = new List<string>();
                    foreach (string field in fields)
                    {
                        oneLine.Add(field);
                        //TODO: Process field
                    }
                    result.Add(oneLine);
                }
                return result;

            }
        }
    }
}
