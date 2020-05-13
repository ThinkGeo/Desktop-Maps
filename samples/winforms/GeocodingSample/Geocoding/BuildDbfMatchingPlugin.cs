using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;
using ThinkGeo.MapSuite.MapSuiteGeocoder;
using System.IO;
using System.Collections.ObjectModel;

namespace HowDoISamples
{
    public partial class BuildDbfMatchingPlugin : UserControl
    {
        // We use this to convert the decimal degree value, which is a double, to a integer to save 4 bytes
        private const int coordinatesMultiplier = 1000000;

        public BuildDbfMatchingPlugin()
        {
            InitializeComponent();

            cboSourceText.Text = cboSourceText.Items[0].ToString();
        }

        private void BuildIndexFileFromCntry02_Load(object sender, EventArgs e)
        {
            // Setup the map unit and set the Chicago extent
            winformsMap1.MapUnit = GeographyUnit.DecimalDegree;
            winformsMap1.CurrentExtent = new RectangleShape(-125, 50, -66, 23);

            // Setup the World Map Kit WMS Overlay
            WorldMapKitWmsDesktopOverlay worldMapKitOverlay = new WorldMapKitWmsDesktopOverlay();
            winformsMap1.Overlays.Add("WorldMapKitOverlay", worldMapKitOverlay);

            // Setup the marker overlay and add it to the map            
            LayerOverlay markerOverlay = new LayerOverlay();
            winformsMap1.Overlays.Add("MarkerOverlay", markerOverlay);

            // Create and add the marker layer to the marker overlay
            MarkerLayer markerLayer = new MarkerLayer();
            markerOverlay.Layers.Add("MarkerLayer", markerLayer);

            winformsMap1.Refresh();  
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Get the path to the data files and create the Geocoder
            string dataPath = Path.Combine(SampleHelper.GetDataPath(), @"SampleData\CreateIndexFileData");
            Geocoder geocoder = new Geocoder(dataPath);
            string cntry02IndexFile = dataPath + "\\Countries-PluginFormat.dbf";
            if (File.Exists(cntry02IndexFile))
            {
                geocoder.MatchingPlugIns.Add(new DbfMatchingPlugin(cntry02IndexFile));
            }
            else
            {
                MessageBox.Show("Please build the cntry02 index file first!", "How Do I Samples", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                return;
            }

            // Open the geocoder, get any matches and close it
            Collection<GeocoderMatch> matchResult;
            try
            {
                geocoder.Open();
                matchResult = geocoder.Match(cboSourceText.Text);
            }
            finally
            {
                geocoder.Close();
            }

            PopulateAddressResultList(matchResult);
        }

        private void PopulateAddressResultList(Collection<GeocoderMatch> matchResult)
        {
            // Clear the results
            lstResult.Items.Clear();
            dataGridViewDetail.Rows.Clear();

            // Load the matching items into the grid
            foreach (GeocoderMatch matchItem in matchResult)
            {
                if (matchItem.NameValuePairs.ContainsKey("Name"))
                {
                    lstResult.Items.Add(new ListItem(matchItem, new string[] { "Name", "FIPS" }));
                }
            }

            // If we find addresses then select the first one to zoom in, if not then say we did not find any
            if (lstResult.Items.Count > 0)
            {
                lstResult.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Address can not be found!", "How Do I Samples", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
            }
        }

        private void lstResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Populate the address grid based on the selected address
            GeocoderMatch matchItem = ((ListItem)lstResult.SelectedItem).MatchItem;
            dataGridViewDetail.Rows.Clear();

            foreach (KeyValuePair<string, string> item in matchItem.NameValuePairs)
            {
                dataGridViewDetail.Rows.Add(new object[] { item.Key, item.Value });
            }

            // Set the marker location to the address selected
            LayerOverlay markerOverlay = winformsMap1.Overlays["MarkerOverlay"] as LayerOverlay;
            MarkerLayer markerLayer = markerOverlay.Layers["MarkerLayer"] as MarkerLayer;
            markerLayer.MarkerLocation = new PointShape(matchItem.NameValuePairs["CentroidPoint"]);

            // Set the extent around the address and refresh the map
            winformsMap1.CurrentExtent = new RectangleShape(matchItem.NameValuePairs["BoundingBox"]);
            winformsMap1.Refresh();
        }

        private void btnBuildIndex_Click(object sender, EventArgs e)
        {
            // Here we create an empty DbfMatchPlugin with all of the columns
            DbfMatchingPlugin dbfMatchingPlugin = CreateEmptyDbfMatchingPlugin();

            // Here we load the shape file data, sort it and get it ready to add
            List<string> dataSource = PrepareData();

            // Here we add the sorted date to the DBFMatchPlugin
            PopulateDbfMatchPlugin(dataSource, dbfMatchingPlugin);

            MessageBox.Show("Build Index Completed", "Build Index File", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
        }

        private DbfMatchingPlugin CreateEmptyDbfMatchingPlugin()
        {
            // Here we create the column collection
            Collection<DbfMatchingPluginColumn> columns = new Collection<DbfMatchingPluginColumn>();

            // Note that the first field needs to have an 'ID_' prefix.  This also needs to be unique and sorted A-Z
            columns.Add(new DbfMatchingPluginColumn("ID_Name", ThinkGeo.MapSuite.MapSuiteGeocoder.DbfMatchingPluginColumnType.String, 50));

            // The columns prefixed with 'DT_' are just detail columns.  They will be returned in the result but we 
            // ignore them from a program perspective
            columns.Add(new DbfMatchingPluginColumn("DT_FIPS", DbfMatchingPluginColumnType.String, 5));
            columns.Add(new DbfMatchingPluginColumn("DT_Pop", DbfMatchingPluginColumnType.Long, 4));
            columns.Add(new DbfMatchingPluginColumn("DT_CuType", DbfMatchingPluginColumnType.String, 20));
            columns.Add(new DbfMatchingPluginColumn("DT_CuCode", DbfMatchingPluginColumnType.String, 10));
            columns.Add(new DbfMatchingPluginColumn("DT_Color", DbfMatchingPluginColumnType.String, 2));

            // The 'BB_CX' & 'BB_CY' stands for the bounding box center point X and Y
            // The 'BB_ULX' stand for the following..  Upper Left X, Upperl Left Y, Lower Right X and Lower Right Y
            // They are used by the geocoder to zoom into the selection once it is found
            columns.Add(new DbfMatchingPluginColumn("BB_CX", DbfMatchingPluginColumnType.Long, 4));
            columns.Add(new DbfMatchingPluginColumn("BB_CY", DbfMatchingPluginColumnType.Long, 4));
            columns.Add(new DbfMatchingPluginColumn("BB_ULX", DbfMatchingPluginColumnType.Long, 4));
            columns.Add(new DbfMatchingPluginColumn("BB_ULY", DbfMatchingPluginColumnType.Long, 4));
            columns.Add(new DbfMatchingPluginColumn("BB_LRX", DbfMatchingPluginColumnType.Long, 4));
            columns.Add(new DbfMatchingPluginColumn("BB_LRY", DbfMatchingPluginColumnType.Long, 4));

            // This method creates the DBF based on the columns defines above
            string dataPath = Path.Combine(SampleHelper.GetDataPath(), @"SampleData\CreateIndexFileData");
            DbfMatchingPlugin.CreateDbf(dataPath + "\\Countries-PluginFormat.dbf", columns);

            // Here we just open the plugin we just created and pass it back out of the method
            DbfMatchingPlugin dbfMathingPlugin = new DbfMatchingPlugin(dataPath + "\\Countries-PluginFormat.dbf", DbfMatchingPluginReadWriteMode.ReadWrite);
            dbfMathingPlugin.Open();

            return dbfMathingPlugin;
        }

        private List<string> PrepareData()
        {
            List<string> dataSource = new List<string>();

            // Build the index file incase it is not built
            string dataPath = Path.Combine(SampleHelper.GetDataPath(), @"SampleData\CreateIndexFileData");
            ShapeFileFeatureLayer.BuildIndexFile(dataPath + "\\cntry02.shp", BuildIndexMode.DoNotRebuild);

            // Open the shape file we will use to construct the match plugin
            ShapeFileFeatureLayer shapeFile = new ShapeFileFeatureLayer(dataPath + "\\cntry02.shp");
            shapeFile.Open();

            // Get all the features from the shapefile with all the columns
            Collection<Feature> features = shapeFile.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);

            // Loop through each feature and get the bounding box and add it to the datasource
            foreach (Feature feature in features)
            {
                // Calculate the boundingbox and center point then converts the doubles to int for storage in the DbfMatchPlugin
                RectangleShape boundingBox = feature.GetBoundingBox();

                // Center Point
                int centerX = Convert.ToInt32(boundingBox.GetCenterPoint().X * coordinatesMultiplier);
                int centerY = Convert.ToInt32(boundingBox.GetCenterPoint().Y * coordinatesMultiplier);

                // Bounding Box
                int ulPointX = Convert.ToInt32(boundingBox.UpperLeftPoint.X * coordinatesMultiplier);
                int ulPointY = Convert.ToInt32(boundingBox.UpperLeftPoint.Y * coordinatesMultiplier);
                int lrPointX = Convert.ToInt32(boundingBox.LowerRightPoint.X * coordinatesMultiplier);
                int lrPointY = Convert.ToInt32(boundingBox.LowerRightPoint.Y * coordinatesMultiplier);

                // Read this row into a string to be added to the data source.  We also add the bounding box
                // to the end of the row
                string sourceStr = string.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}|{10}|{11}", feature.ColumnValues["CNTRY_NAME"], feature.ColumnValues["FIPS_CNTRY"], feature.ColumnValues["POP_CNTRY"], feature.ColumnValues["CURR_TYPE"], feature.ColumnValues["CURR_CODE"], feature.ColumnValues["COLOR_MAP"], centerX, centerY, ulPointX, ulPointY, lrPointX, lrPointY);

                // Add the data to the datasource
                dataSource.Add(sourceStr);
            }

            // Sort the data so that the country name is sorted from A-Z
            dataSource.Sort(delegate(string lineA, string lineB)
            { return string.Compare(lineA.Split('|')[0], lineB.Split('|')[0], StringComparison.OrdinalIgnoreCase); });

            return dataSource;
        }

        private void PopulateDbfMatchPlugin(List<string> dataSource, DbfMatchingPlugin dbfMatchingPlugin)
        {
            // Loop through all the data in the data source, which should already be sorted, and add it to
            // the DbfMatchPlugin
            foreach (string line in dataSource)
            {
                // Cast all of the data to the proper types
                string[] parts = line.Split('|');
                string cntryName = parts[0];
                string fips = parts[1];
                int pop = int.Parse(parts[2]);
                string currType = parts[3];
                string currCode = parts[4];
                string colorMap = parts[5];
                int centerX = int.Parse(parts[6]);
                int centerY = int.Parse(parts[7]);
                int ulPointX = int.Parse(parts[8]);
                int ulPointY = int.Parse(parts[9]);
                int lrPointX = int.Parse(parts[10]);
                int lrPointY = int.Parse(parts[11]);

                //  Add the record to the DbfMatchPlugin
                dbfMatchingPlugin.AddRecord(new object[] { cntryName, fips, pop, currType, currCode, colorMap, centerX, centerY, ulPointX, ulPointY, lrPointX, lrPointY });
            }

            dbfMatchingPlugin.Close();
        }
    }
}
