using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Text;
using ThinkGeo.MapSuite.Shapes;

namespace ThinkGeo.MapSuite.VehicleTracking
{
    public class TrackingAccessProvider : TrackingDataProvider
    {
        private OleDbConnection dataConnection;

        public TrackingAccessProvider(string databaseFilePath)
        {
            string connectionString = string.Format(CultureInfo.InvariantCulture, "Provider=Microsoft.Jet.OLEDB.4.0; Data Source='{0}'", databaseFilePath);
            dataConnection = new OleDbConnection(connectionString);
        }

        public override Dictionary<int, Vehicle> GetCurrentVehicles(DateTime currentTime)
        {
            Dictionary<int, Vehicle> vehiclesList = new Dictionary<int, Vehicle>();
            DataSet vehiclesDataSet = ExecuteQuery("Select * from Vehicle");

            TimeSpan trackHistoryVehicleTimeSpan = TimeSpan.FromHours(8);
            foreach (DataRow row in vehiclesDataSet.Tables[0].Rows)
            {
                int vehicleId = Convert.ToInt32(row["VehicleId"], CultureInfo.InvariantCulture);
                Vehicle vehicle = GetCurrentVehicle(vehicleId, currentTime, trackHistoryVehicleTimeSpan);
                vehiclesList.Add(vehicle.Id, vehicle);
            }

            return vehiclesList;
        }

        private Vehicle GetCurrentVehicle(int vehicleId, DateTime currentTime, TimeSpan trackHistoryVehicleTimeSpan)
        {
            string sql = "SELECT A.VehicleName, A.VehicleID, A.VehicleIconVirtualPath, B.Longitude, B.Latitude, B.[Date], B.Speed FROM (Vehicle A LEFT OUTER JOIN Location B ON A.VehicleID = B.VehicleID) WHERE (A.VehicleID = {0}) AND (B.[Date] <= #{1}# and B.[Date]>=#{2}#) ORDER BY A.VehicleID, B.[Date] DESC";
            DateTime trackStartTime = currentTime.AddTicks(-trackHistoryVehicleTimeSpan.Ticks);
            sql = String.Format(CultureInfo.InvariantCulture, sql, vehicleId, currentTime.ToString(CultureInfo.InvariantCulture), trackStartTime.ToString(CultureInfo.InvariantCulture));

            Vehicle currentVechicle = new Vehicle(vehicleId);
            DataSet currentLocations = null;
            try
            {
                // Get the locations from current time back to the passed time span
                currentLocations = ExecuteQuery(sql);
                Collection<double> historySpeeds = new Collection<double>();
                for (int rowIndex = 0; rowIndex < currentLocations.Tables[0].Rows.Count; rowIndex++)
                {
                    DataRow dataRow = currentLocations.Tables[0].Rows[rowIndex];
                    currentVechicle.IconPath = dataRow["VehicleIconVirtualPath"].ToString();

                    double latitude = Convert.ToDouble(dataRow["Latitude"], CultureInfo.InvariantCulture);
                    double longitude = Convert.ToDouble(dataRow["Longitude"], CultureInfo.InvariantCulture);
                    double speed = Convert.ToDouble(dataRow["Speed"], CultureInfo.InvariantCulture);
                    DateTime dateTime = Convert.ToDateTime(dataRow["Date"], CultureInfo.InvariantCulture);
                    Location currentLocation = new Location(longitude, latitude, speed, dateTime);
                    historySpeeds.Add(speed);

                    if (rowIndex == 0)
                    {
                        string vehicleName = dataRow["VehicleName"].ToString();
                        currentVechicle.Location = currentLocation;
                        currentVechicle.Id = vehicleId;
                        currentVechicle.Name = vehicleName;
                    }
                    else
                    {
                        currentVechicle.HistoryLocations.Add(currentLocation);
                    }
                }
            }
            finally
            {
                if (currentLocations != null)
                {
                    currentLocations.Dispose();
                }
            }

            return currentVechicle;
        }

        public override Collection<Feature> GetSpatialFences()
        {
            Collection<Feature> spatialFences = new Collection<Feature>();
            DataSet dataSet = ExecuteQuery("Select * from SpatialFence");

            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                string wkt = row["FenceGeometry"].ToString();
                string id = row["FeatureID"].ToString();
                spatialFences.Add(new Feature(wkt, id));
            }
            return spatialFences;
        }

        public override void DeleteSpatialFencesExcluding(IEnumerable<Feature> excludeFeatures)
        {
            StringBuilder undeleteFeatureIds = new StringBuilder();
            foreach (Feature undeleteFeature in excludeFeatures)
            {
                undeleteFeatureIds.AppendFormat(CultureInfo.InvariantCulture, "'{0}',", undeleteFeature.Id);
            }
            string sql = String.Format(CultureInfo.InvariantCulture, "DELETE FROM SpatialFence WHERE (FeatureID NOT IN ({0}))", undeleteFeatureIds.ToString().TrimEnd(','));
            ExecuteNonQuery(sql);
        }

        public override int UpdateSpatialFenceByFeature(Feature feature)
        {
            return ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture, "UPDATE SpatialFence SET FenceGeometry = '{0}' WHERE (FeatureID = '{1}')", feature.GetWellKnownText(), feature.Id));
        }

        public override void InsertSpatialFence(Feature feature)
        {
            ExecuteNonQuery(String.Format(CultureInfo.InvariantCulture, "Insert into SpatialFence(FenceGeometry,FeatureID) values('{0}','{1}')", feature.GetWellKnownText(), feature.Id));
        }

        private DataSet ExecuteQuery(string selectCommandText)
        {
            OleDbDataAdapter dataAdapter = null;
            try
            {
                dataAdapter = new OleDbDataAdapter(selectCommandText, dataConnection);
                dataConnection.Open();
                DataSet dataSet = new DataSet();
                dataSet.Locale = CultureInfo.InvariantCulture;
                dataAdapter.Fill(dataSet);
                return dataSet;
            }
            finally
            {
                if (dataAdapter != null) { dataAdapter.Dispose(); }
                if (dataConnection != null) { dataConnection.Close(); }
            }
        }

        private int ExecuteNonQuery(string commandText)
        {
            OleDbCommand dataCommand = null;
            int affectedRowNumber;
            try
            {
                dataCommand = new OleDbCommand(commandText, dataConnection);
                dataConnection.Open();
                affectedRowNumber = dataCommand.ExecuteNonQuery();
            }
            finally
            {
                if (dataCommand != null) { dataCommand.Dispose(); }
                if (dataConnection != null) { dataConnection.Close(); }
            }

            return affectedRowNumber;
        }

        ~TrackingAccessProvider()
        {
            Dispose(false);
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool flag)
        {
            if (flag)
            {
                if (dataConnection != null)
                {
                    dataConnection.Close();
                    dataConnection.Dispose();
                    dataConnection = null;
                }
            }
        }
    }
}