//Copyright © 2014 ThinkGeo LLC. All rights reserved.
//The use of this code is for evaluating Map Suite products and the World Map Kit SDK for up to 60 days.
//To license this product for use in your application please contact ThinkGeo at www.thinkgeo.com

using System;
using System.Collections.ObjectModel;
using System.IO;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.Layers
{
    [Serializable]
    public partial class WorldStreetsLayer : Layer
    {
        private GeoCollection<FeatureLayer> layers = new GeoCollection<FeatureLayer>();
        private int srid;
        private string schemaName;
        private string featureIdColumnName;
        private string geometryColumnName;
        private string connectionString;
        private string iconPath;

        public WorldStreetsLayer()
            : this(string.Empty)
        {
        }

        public WorldStreetsLayer(string connectionString)
            : this(connectionString, "worldmapkit", "id", "geometry", 3857)
        {
        }

        public WorldStreetsLayer(string connectionString, string schemaName, string featureIdColumnName, string geometryColumnName, int srid)
        {
            this.connectionString = connectionString;
            this.schemaName = schemaName;
            this.featureIdColumnName = featureIdColumnName;
            this.geometryColumnName = geometryColumnName;
            this.srid = srid;
            this.iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons");
        }

        public GeoCollection<FeatureLayer> Layers
        {
            get { return layers; }
        }

        public int Srid
        {
            get { return srid; }
            set { srid = value; }
        }

        public string IconPath
        {
            get { return iconPath; }
            set { iconPath = value; }
        }

        public string SchemaName
        {
            get { return schemaName; }
            set { schemaName = value; }
        }

        public string FeatureIdColumnName
        {
            get { return featureIdColumnName; }
            set { featureIdColumnName = value; }
        }

        public string GeometryColumnName
        {
            get { return geometryColumnName; }
            set { geometryColumnName = value; }
        }

        public string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        protected override void OpenCore()
        {
            layers.Add("ne_baseland30m_polygon", GetBaseLand30mPolygonLayer());
            layers.Add("ne_state5m_polygon", GetStateLabelLayer());
            layers.Add("ne_ocean1m_polygon", GetOceanLabelLayer());
            layers.Add("ne_place2m_point", GetPlace2mLabelLayer());
            layers.Add("osm_place_city_point", GetPlaceCityLabelLayer());
            layers.Add("osm_place_city_town_point", GetPlaceCityTownLabelLayer());
            layers.Add("osm_place_point", GetPlaceLabelLayer());
            layers.Add("osm_baseland1m_polygon", GetBaseLand1mPolygonLayer());
            layers.Add("osm_baseland_polygon", GetBaseLandPolygonLayer());
            layers.Add("osm_land300k_polygon", GetLand300kPolygonLayer());
            layers.Add("osm_land100k_polygon", GetLand100kPolygonLayer());
            layers.Add("osm_land_polygon", GetLandPolygonLayer());
            layers.Add("ne_water10m_polygon", GetWater10mPolygonLayer());
            layers.Add("osm_water1m_polygon", GetWater1mPolygonLayer());
            layers.Add("osm_water_large_polygon", GetWaterLargePolygonLayer());
            layers.Add("osm_water_polygon", GetWaterPolygonLayer());
            layers.Add("osm_water100k_linestring", GetWater100kLinestringLayer());
            layers.Add("osm_water_linestring", GetWaterLinestringLayer());
            layers.Add("osm_transport_polygon", GetTransportPolygonLayer());
            layers.Add("osm_transport_linestring", GetTransportLinestringLayer());
            layers.Add("ne_road20m_linestring", GetRoad20mLinestringLayer());
            layers.Add("ne_road10m_linestring", GetRoad10mLinestringLayer());
            layers.Add("osm_road5m_linestring", GetRoad5mLinestringLayer());
            layers.Add("osm_road3m_linestring", GetRoad3mLinestringLayer());
            layers.Add("osm_road300k_linestring", GetRoad300kLinestringLayer());
            layers.Add("osm_road_streets_linestring", GetStreetRoadLinestringLayer());
            layers.Add("osm_road_linestring", GetRoadLinestringLayer());
            layers.Add("osm_poi_point", GetPoiPointLayer());
            layers.Add("osm_contruct_large_polygon", GetLargeConstructPolygonLayer());
            layers.Add("osm_contruct_polygon", GetConstructPolygonLayer());
            layers.Add("osm_construct_linestring", GetConstructLinestringLayer());
            layers.Add("osm_construct_point", GetConstructPointLayer());
            layers.Add("ne_state2m_linestring", GetState2mLinestringLayer());
            layers.Add("ne_country10m_linestring", GetCountry10mLinestringLayer());
            layers.Add("osm_boundary_country_state_linestring", GetCountryStateBoundaryLinestringLayer());
            layers.Add("osm_boundary_country_state_county_linestring", GetCountryStateCountyBoundaryLinestringLayer());
            layers.Add("osm_boundary_linestring", GetBoundaryLinestringLayer());

            foreach (FeatureLayer layer in layers)
            {
                layer.Open();
            }
        }

        protected override void CloseCore()
        {
            foreach (var layer in layers)
            {
                layer.Close();
            }

            layers.Clear();
        }

        public void DrawBackground(GeoCanvas canvas, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInAllLayers)
        {
            DrawBackgroundCore(canvas, labelsInAllLayers);
        }

        protected virtual void DrawBackgroundCore(GeoCanvas canvas, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInAllLayers)
        {
            canvas.DrawArea(canvas.CurrentWorldExtent, new GeoPen(GetWaterAreaStyle().OutlinePen.Color), new GeoSolidBrush(GetWaterAreaStyle().FillSolidBrush.Color), DrawingLevel.LevelOne);
        }

        protected override void DrawCore(GeoCanvas canvas, System.Collections.ObjectModel.Collection<SimpleCandidate> labelsInAllLayers)
        {
            DrawBackground(canvas, labelsInAllLayers);

            foreach (FeatureLayer layer in layers)
            {
                layer.Draw(canvas, labelsInAllLayers);
            }
        }

        private FeatureLayer GetFeatureLayer(string tableName)
        {
            FeatureLayer layer = new SqliteFeatureLayer(connectionString, tableName, featureIdColumnName, geometryColumnName);
            layer.Name = ((SqliteFeatureLayer)layer).TableName;

            layer.DrawingMarginInPixel = 512;

            return layer;
        }

        private void SetWhereClause(FeatureLayer layer, string whereClause)
        {
            ((SqliteFeatureLayer)layer).WhereClause = whereClause;
        }

        private FeatureLayer GetBaseLand30mPolygonLayer()
        {
            FeatureLayer ne_baseland30m_polygon = GetFeatureLayer("ne_baseland30m_polygon");

            ne_baseland30m_polygon.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(GetBaseLandAreaStyle());
            ne_baseland30m_polygon.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level02;

            ne_baseland30m_polygon.ZoomLevelSet.ZoomLevel03.CustomStyles.Add(GetBaseLandAreaStyle());
            ne_baseland30m_polygon.ZoomLevelSet.ZoomLevel03.CustomStyles.Add(GetGeneralPurposeTextStyle("name", 7));
            ne_baseland30m_polygon.ZoomLevelSet.ZoomLevel03.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level04;

            ne_baseland30m_polygon.ZoomLevelSet.ZoomLevel05.CustomStyles.Add(GetBaseLandAreaStyle());
            ne_baseland30m_polygon.ZoomLevelSet.ZoomLevel05.CustomStyles.Add(GetGeneralPurposeTextStyle("name", 8));
            ne_baseland30m_polygon.ZoomLevelSet.ZoomLevel05.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level06;

            ne_baseland30m_polygon.ZoomLevelSet.ZoomLevel07.CustomStyles.Add(GetGeneralPurposeTextStyle("name", 9));
            ne_baseland30m_polygon.ZoomLevelSet.ZoomLevel07.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level08;

            ne_baseland30m_polygon.ZoomLevelSet.ZoomLevel09.CustomStyles.Add(GetGeneralPurposeTextStyle("name", 10));
            ne_baseland30m_polygon.ZoomLevelSet.ZoomLevel09.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level10;

            return ne_baseland30m_polygon;
        }

        private FeatureLayer GetBaseLand1mPolygonLayer()
        {
            FeatureLayer osm_baseland1m_polygon = GetFeatureLayer("osm_baseland1m_polygon");

            osm_baseland1m_polygon.ZoomLevelSet.ZoomLevel07.CustomStyles.Add(GetBaseLandAreaStyle());
            osm_baseland1m_polygon.ZoomLevelSet.ZoomLevel07.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level10;

            return osm_baseland1m_polygon;
        }

        private FeatureLayer GetBaseLandPolygonLayer()
        {
            FeatureLayer osm_baseland_polygon = GetFeatureLayer("osm_baseland_polygon");

            osm_baseland_polygon.ZoomLevelSet.ZoomLevel11.CustomStyles.Add(GetBaseLandAreaStyle());
            osm_baseland_polygon.ZoomLevelSet.ZoomLevel11.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            return osm_baseland_polygon;
        }

        private FeatureLayer GetLand300kPolygonLayer()
        {
            FeatureLayer osm_land300k_polygon = GetFeatureLayer("osm_land300k_polygon");
            SetWhereClause(osm_land300k_polygon, " ORDER BY osm_area desc ");

            ValueStyle boundaryValueStyle = new ValueStyle("boundary", new Collection<ValueItem>() {
                new ValueItem ("national_park", GetNationalParkAreaStyle ()),
                new ValueItem ("protected_area", GetProtectedAreaAreaStyle ())
            });

            ValueStyle landuseValueStyle = new ValueStyle("landuse", new Collection<ValueItem>() {
                new ValueItem ("reservoir", GetWaterAreaStyle()),
                new ValueItem ("orchard", GetOrchardAreaStyle ()),
                new ValueItem ("vineyards", GetVineyardsAreaStyle ()),
                new ValueItem ("landfill", GetLandfillAreaStyle ()),
                new ValueItem ("railway", GetRailwayAreaStyle ()),
                new ValueItem ("cemetery", GetCemeteryAreaStyle ()),
                new ValueItem ("quarry", GetQuarryAreaStyle ()),
                new ValueItem ("basin", GetBasinAreaStyle ()),
                new ValueItem ("village_green", GetVillageGreenAreaStyle ()),
                new ValueItem ("recreation_ground", GetRecreationGroundAreaStyle ()),
                new ValueItem ("farmyard", GetFarmlandAreaStyle ()),
                new ValueItem ("farm", GetFarmlandAreaStyle ()),
                new ValueItem ("farmland", GetFarmlandAreaStyle ()),
                new ValueItem ("industrial", GetIndustrialAreaStyle ()),
                new ValueItem ("retail", GetRetailAreaStyle ()),
                new ValueItem ("commercial", GetCommercialAreaStyle ()),
                new ValueItem ("forest", GetForestAreaStyle ()),
                new ValueItem ("military", GetMilitaryAreaStyle ()),
                new ValueItem ("meadow", GetMeadowAreaStyle ()),
                new ValueItem ("grass", GetGrassAreaStyle ()),
                new ValueItem ("residential", GetResidentialAreaStyle ())
            });

            ValueStyle parkValueStyle = new ValueStyle("leisure", new Collection<ValueItem>() {
                new ValueItem ("park", GetParkAreaStyle ())
            });

            ValueStyle naturalValueStyle = new ValueStyle("natural", new Collection<ValueItem>() {
                new ValueItem ("beach", GetBeachAreaStyle ()),
                new ValueItem ("grassland", GetGrasslandAreaStyle ()),
                new ValueItem ("heath", GetHeathAreaStyle ()),
                new ValueItem ("mud", GetMudAreaStyle ()),
                new ValueItem ("sand", GetSandAreaStyle ()),
                new ValueItem ("wood", GetWoodAreaStyle ()),
                new ValueItem ("wetland", GetWetlandAreaStyle ()),
                new ValueItem ("scrub", GetScrubAreaStyle ())
            });

            ValueStyle leisureValueStyle = new ValueStyle("leisure", new Collection<ValueItem>() {
                new ValueItem ("marina", GetMarinaAreaStyle ()),
                new ValueItem ("water_park", GetWaterParkAreaStyle ()),
                new ValueItem ("track", GetTrackAreaStyle ()),
                new ValueItem ("common", GetCommonAreaStyle ()),
                new ValueItem ("pitch", GetPitchAreaStyle ()),
                new ValueItem ("stadium", GetStadiumAreaStyle ()),
                new ValueItem ("sports_center", GetSportsCenterAreaStyle ()),
                new ValueItem ("playground", GetPlaygroundAreaStyle ()),
                new ValueItem ("golf_course", GetGolfcourseAreaStyle ()),
                new ValueItem ("garden", GetGardenAreaStyle ()),
                new ValueItem ("dog_park", GetDogParkAreaStyle ()),
                new ValueItem ("nature_reserve", GetNatureReserveAreaStyle ())
            });

            ValueStyle tourismValueStyle = new ValueStyle("tourism", new Collection<ValueItem>() {
                new ValueItem ("attraction", GetAttractionAreaStyle ()),
                new ValueItem ("zoo", GetZooAreaStyle ())
            });

            ValueStyle aerowayValueStyle = new ValueStyle("aeroway", new Collection<ValueItem>() {
                new ValueItem ("helipad", GetHelipadAreaStyle ()),
                new ValueItem ("aerodrome", GetAerodromeAreaStyle ())
            });

            WorldStreetsCompositeStyle compositeStyle = new WorldStreetsCompositeStyle();
            compositeStyle.Styles.Add(boundaryValueStyle);
            compositeStyle.Styles.Add(landuseValueStyle);
            compositeStyle.Styles.Add(parkValueStyle);
            compositeStyle.Styles.Add(naturalValueStyle);
            compositeStyle.Styles.Add(leisureValueStyle);
            compositeStyle.Styles.Add(tourismValueStyle);
            compositeStyle.Styles.Add(aerowayValueStyle);

            osm_land300k_polygon.ZoomLevelSet.ZoomLevel11.CustomStyles.Add(compositeStyle);
            osm_land300k_polygon.ZoomLevelSet.ZoomLevel11.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level12;

            return osm_land300k_polygon;
        }

        private FeatureLayer GetLand100kPolygonLayer()
        {
            FeatureLayer osm_land100k_polygon = GetFeatureLayer("osm_land100k_polygon");
            SetWhereClause(osm_land100k_polygon, " ORDER BY osm_area desc ");

            ValueStyle boundaryValueStyle = new ValueStyle("boundary", new Collection<ValueItem>() {
                new ValueItem ("national_park", GetNationalParkAreaStyle ()),
                new ValueItem ("protected_area", GetProtectedAreaAreaStyle ())
            });

            ValueStyle landuseValueStyle = new ValueStyle("landuse", new Collection<ValueItem>() {
                new ValueItem ("reservoir", GetWaterAreaStyle ()),
                new ValueItem ("orchard", GetOrchardAreaStyle ()),
                new ValueItem ("vineyards", GetVineyardsAreaStyle ()),
                new ValueItem ("landfill", GetLandfillAreaStyle ()),
                new ValueItem ("railway", GetRailwayAreaStyle ()),
                new ValueItem ("cemetery", GetCemeteryAreaStyle ()),
                new ValueItem ("quarry", GetQuarryAreaStyle ()),
                new ValueItem ("basin", GetBasinAreaStyle ()),
                new ValueItem ("village_green", GetVillageGreenAreaStyle ()),
                new ValueItem ("recreation_ground", GetRecreationGroundAreaStyle ()),
                new ValueItem ("farmyard", GetFarmlandAreaStyle ()),
                new ValueItem ("farm", GetFarmlandAreaStyle ()),
                new ValueItem ("farmland", GetFarmlandAreaStyle ()),
                new ValueItem ("industrial", GetIndustrialAreaStyle ()),
                new ValueItem ("retail", GetRetailAreaStyle ()),
                new ValueItem ("commercial", GetCommercialAreaStyle ()),
                new ValueItem ("forest", GetForestAreaStyle ()),
                new ValueItem ("military", GetMilitaryAreaStyle ()),
                new ValueItem ("meadow", GetMeadowAreaStyle ()),
                new ValueItem ("grass", GetGrassAreaStyle ()),
                new ValueItem ("residential", GetResidentialAreaStyle ())
            });

            ValueStyle parkValueStyle = new ValueStyle("leisure", new Collection<ValueItem>() {
                new ValueItem ("park", GetParkAreaStyle ())
            });

            ValueStyle naturalValueStyle = new ValueStyle("natural", new Collection<ValueItem>() {
                new ValueItem ("beach", GetBeachAreaStyle ()),
                new ValueItem ("grassland", GetGrasslandAreaStyle ()),
                new ValueItem ("heath", GetHeathAreaStyle ()),
                new ValueItem ("mud", GetMudAreaStyle ()),
                new ValueItem ("sand", GetSandAreaStyle ()),
                new ValueItem ("wood", GetWoodAreaStyle ()),
                new ValueItem ("wetland", GetWetlandAreaStyle ()),
                new ValueItem ("scrub", GetScrubAreaStyle ())
            });

            ValueStyle leisureValueStyle = new ValueStyle("leisure", new Collection<ValueItem>() {
                new ValueItem ("marina", GetMarinaAreaStyle ()),
                new ValueItem ("water_park", GetWaterParkAreaStyle ()),
                new ValueItem ("track", GetTrackAreaStyle ()),
                new ValueItem ("common", GetCommonAreaStyle ()),
                new ValueItem ("pitch", GetPitchAreaStyle ()),
                new ValueItem ("stadium", GetStadiumAreaStyle ()),
                new ValueItem ("sports_center", GetSportsCenterAreaStyle ()),
                new ValueItem ("playground", GetPlaygroundAreaStyle ()),
                new ValueItem ("golf_course", GetGolfcourseAreaStyle ()),
                new ValueItem ("garden", GetGardenAreaStyle ()),
                new ValueItem ("dog_park", GetDogParkAreaStyle ()),
                new ValueItem ("nature_reserve", GetNatureReserveAreaStyle ())
            });

            ValueStyle tourismValueStyle = new ValueStyle("tourism", new Collection<ValueItem>() {
                new ValueItem ("attraction", GetAttractionAreaStyle ()),
                new ValueItem ("zoo", GetZooAreaStyle ())
            });

            ValueStyle aerowayValueStyle = new ValueStyle("aeroway", new Collection<ValueItem>() {
                new ValueItem ("helipad", GetHelipadAreaStyle ()),
                new ValueItem ("aerodrome", GetAerodromeAreaStyle ())
            });

            WorldStreetsCompositeStyle compositeStyle = new WorldStreetsCompositeStyle();
            compositeStyle.Styles.Add(boundaryValueStyle);
            compositeStyle.Styles.Add(landuseValueStyle);
            compositeStyle.Styles.Add(parkValueStyle);
            compositeStyle.Styles.Add(naturalValueStyle);
            compositeStyle.Styles.Add(leisureValueStyle);
            compositeStyle.Styles.Add(tourismValueStyle);
            compositeStyle.Styles.Add(aerowayValueStyle);

            osm_land100k_polygon.ZoomLevelSet.ZoomLevel13.CustomStyles.Add(compositeStyle);
            osm_land100k_polygon.ZoomLevelSet.ZoomLevel13.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level14;

            return osm_land100k_polygon;
        }

        private FeatureLayer GetLandPolygonLayer()
        {
            FeatureLayer osm_land_polygon = GetFeatureLayer("osm_land_polygon");
            SetWhereClause(osm_land_polygon, " ORDER BY osm_area desc ");

            ValueStyle boundaryValueStyle = new ValueStyle("boundary", new Collection<ValueItem>() {
                new ValueItem ("national_park", GetNationalParkAreaStyle ()),
                new ValueItem ("protected_area", GetProtectedAreaAreaStyle ())
            });

            ValueStyle landuseValueStyle = new ValueStyle("landuse", new Collection<ValueItem>() {
                new ValueItem ("reservoir", GetWaterAreaStyle ()),
                new ValueItem ("orchard", GetOrchardAreaStyle ()),
                new ValueItem ("vineyards", GetVineyardsAreaStyle ()),
                new ValueItem ("landfill", GetLandfillAreaStyle ()),
                new ValueItem ("railway", GetRailwayAreaStyle ()),
                new ValueItem ("cemetery", GetCemeteryAreaStyle ()),
                new ValueItem ("quarry", GetQuarryAreaStyle ()),
                new ValueItem ("basin", GetBasinAreaStyle ()),
                new ValueItem ("village_green", GetVillageGreenAreaStyle ()),
                new ValueItem ("recreation_ground", GetRecreationGroundAreaStyle ()),
                new ValueItem ("farmyard", GetFarmlandAreaStyle ()),
                new ValueItem ("farm", GetFarmlandAreaStyle ()),
                new ValueItem ("farmland", GetFarmlandAreaStyle ()),
                new ValueItem ("industrial", GetIndustrialAreaStyle ()),
                new ValueItem ("retail", GetRetailAreaStyle ()),
                new ValueItem ("commercial", GetCommercialAreaStyle ()),
                new ValueItem ("forest", GetForestAreaStyle ()),
                new ValueItem ("military", GetMilitaryAreaStyle ()),
                new ValueItem ("meadow", GetMeadowAreaStyle ()),
                new ValueItem ("grass", GetGrassAreaStyle ()),
                new ValueItem ("residential", GetResidentialAreaStyle ())
            });

            ValueStyle parkValueStyle = new ValueStyle("leisure", new Collection<ValueItem>() {
                new ValueItem ("park", GetParkAreaStyle ())
            });

            ValueStyle amenityValueStyle = new ValueStyle("amenity", new Collection<ValueItem>() {
                new ValueItem ("school", GetSchoolAreaStyle ()),
                new ValueItem ("kindergarten", GetSchoolAreaStyle ()),
                new ValueItem ("college", GetSchoolAreaStyle ()),
                new ValueItem ("university", GetSchoolAreaStyle ())
            });

            ValueStyle naturalValueStyle = new ValueStyle("natural", new Collection<ValueItem>() {
                new ValueItem ("beach", GetBeachAreaStyle ()),
                new ValueItem ("grassland", GetGrasslandAreaStyle ()),
                new ValueItem ("heath", GetHeathAreaStyle ()),
                new ValueItem ("mud", GetMudAreaStyle ()),
                new ValueItem ("sand", GetSandAreaStyle ()),
                new ValueItem ("wood", GetWoodAreaStyle ()),
                new ValueItem ("wetland", GetWetlandAreaStyle ()),
                new ValueItem ("scrub", GetScrubAreaStyle ())
            });

            ValueStyle leisureValueStyle = new ValueStyle("leisure", new Collection<ValueItem>() {
                new ValueItem ("swimming_pool", GetSwimmingPoolAreaStyle ()),
                new ValueItem ("marina", GetMarinaAreaStyle ()),
                new ValueItem ("water_park", GetWaterParkAreaStyle ()),
                new ValueItem ("track", GetTrackAreaStyle ()),
                new ValueItem ("common", GetCommonAreaStyle ()),
                new ValueItem ("pitch", GetPitchAreaStyle ()),
                new ValueItem ("stadium", GetStadiumAreaStyle ()),
                new ValueItem ("sports_centre", GetSportsCenterAreaStyle ()),
                new ValueItem ("playground", GetPlaygroundAreaStyle ()),
                new ValueItem ("golf_course", GetGolfcourseAreaStyle ()),
                new ValueItem ("garden", GetGardenAreaStyle ()),
                new ValueItem ("dog_park", GetDogParkAreaStyle ()),
                new ValueItem ("nature_reserve", GetNatureReserveAreaStyle ())
            });

            ValueStyle tourismValueStyle = new ValueStyle("tourism", new Collection<ValueItem>() {
                new ValueItem ("attraction", GetAttractionAreaStyle ()),
                new ValueItem ("zoo", GetZooAreaStyle ())
            });

            ValueStyle aerowayValueStyle = new ValueStyle("aeroway", new Collection<ValueItem>() {
                new ValueItem ("helipad", GetHelipadAreaStyle ()),
                new ValueItem ("aerodrome", GetAerodromeAreaStyle ())
            });

            ValueStyle parkingValueStyle = new ValueStyle("amenity", new Collection<ValueItem>() {
                new ValueItem ("parking", GetParkingAreaStyle ())
            });

            WorldStreetsCompositeStyle compositeStyle = new WorldStreetsCompositeStyle();
            compositeStyle.Styles.Add(boundaryValueStyle);
            compositeStyle.Styles.Add(landuseValueStyle);
            compositeStyle.Styles.Add(parkValueStyle);
            compositeStyle.Styles.Add(amenityValueStyle);
            compositeStyle.Styles.Add(naturalValueStyle);
            compositeStyle.Styles.Add(leisureValueStyle);
            compositeStyle.Styles.Add(tourismValueStyle);
            compositeStyle.Styles.Add(aerowayValueStyle);
            compositeStyle.Styles.Add(parkingValueStyle);
            compositeStyle.Styles.Add(GetGeneralPurposeTextStyle("name", 7));
            compositeStyle.Styles.Add(GetGeneralPurposeTextStyle("name:en", 7));

            osm_land_polygon.ZoomLevelSet.ZoomLevel15.CustomStyles.Add(compositeStyle);
            osm_land_polygon.ZoomLevelSet.ZoomLevel15.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            return osm_land_polygon;
        }

        private FeatureLayer GetWater10mPolygonLayer()
        {
            FeatureLayer ne_water10m_polygon = GetFeatureLayer("ne_water10m_polygon");

            Collection<ValueItem> scalerankValueItems = new Collection<ValueItem>() {
                new ValueItem ("0", GetWaterAreaStyle ()),
                new ValueItem ("1", GetWaterAreaStyle ()),
                new ValueItem ("2", GetWaterAreaStyle ())
            };

            ValueStyle scalerankValueStyle = new ValueStyle("scalerank", scalerankValueItems);

            ne_water10m_polygon.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(scalerankValueStyle);
            ne_water10m_polygon.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level06;
            return ne_water10m_polygon;
        }

        private FeatureLayer GetWater1mPolygonLayer()
        {
            FeatureLayer osm_water1m_polygon = GetFeatureLayer("osm_water1m_polygon");

            osm_water1m_polygon.ZoomLevelSet.ZoomLevel07.CustomStyles.Add(GetWaterAreaStyle());
            osm_water1m_polygon.ZoomLevelSet.ZoomLevel07.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level10;
            return osm_water1m_polygon;
        }

        private FeatureLayer GetWaterLargePolygonLayer()
        {
            FeatureLayer osm_water_large_polygon = GetFeatureLayer("osm_water_large_polygon");

            osm_water_large_polygon.ZoomLevelSet.ZoomLevel11.CustomStyles.Add(GetWaterAreaStyle());
            osm_water_large_polygon.ZoomLevelSet.ZoomLevel11.CustomStyles.Add(GetWaterLabelTextStyle("name", 6));
            osm_water_large_polygon.ZoomLevelSet.ZoomLevel11.CustomStyles.Add(GetWaterLabelTextStyle("name:en", 6));
            osm_water_large_polygon.ZoomLevelSet.ZoomLevel11.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level13;
            return osm_water_large_polygon;
        }

        private FeatureLayer GetWaterPolygonLayer()
        {
            FeatureLayer osm_water_polygon = GetFeatureLayer("osm_water_polygon");

            osm_water_polygon.ZoomLevelSet.ZoomLevel14.CustomStyles.Add(GetWaterAreaStyle());
            osm_water_polygon.ZoomLevelSet.ZoomLevel14.CustomStyles.Add(GetWaterLabelTextStyle("name", 6));
            osm_water_polygon.ZoomLevelSet.ZoomLevel14.CustomStyles.Add(GetWaterLabelTextStyle("name:en", 6));
            osm_water_polygon.ZoomLevelSet.ZoomLevel14.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return osm_water_polygon;
        }

        private FeatureLayer GetWater100kLinestringLayer()
        {
            FeatureLayer osm_water100k_linestring = GetFeatureLayer("osm_water100k_linestring");

            osm_water100k_linestring.ZoomLevelSet.ZoomLevel12.CustomStyles.Add(GetWaterLineStyle(1));
            osm_water100k_linestring.ZoomLevelSet.ZoomLevel12.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level14;
            return osm_water100k_linestring;
        }

        private FeatureLayer GetWaterLinestringLayer()
        {
            FeatureLayer osm_water_linestring = GetFeatureLayer("osm_water_linestring");

            osm_water_linestring.ZoomLevelSet.ZoomLevel15.CustomStyles.Add(GetWaterLineStyle(2));
            osm_water_linestring.ZoomLevelSet.ZoomLevel15.CustomStyles.Add(GetWaterLabelTextStyle("name", 6));
            osm_water_linestring.ZoomLevelSet.ZoomLevel15.CustomStyles.Add(GetWaterLabelTextStyle("name:en", 6));
            osm_water_linestring.ZoomLevelSet.ZoomLevel15.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level17;

            osm_water_linestring.ZoomLevelSet.ZoomLevel18.CustomStyles.Add(GetWaterLineStyle(4));
            osm_water_linestring.ZoomLevelSet.ZoomLevel18.CustomStyles.Add(GetWaterLabelTextStyle("name", 8));
            osm_water_linestring.ZoomLevelSet.ZoomLevel18.CustomStyles.Add(GetWaterLabelTextStyle("name:en", 8));
            osm_water_linestring.ZoomLevelSet.ZoomLevel18.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            return osm_water_linestring;
        }

        private FeatureLayer GetConstructLinestringLayer()
        {
            FeatureLayer osm_construct_linestring = GetFeatureLayer("osm_construct_linestring");

            ValueStyle powerValueStyle = new ValueStyle("power", new Collection<ValueItem>() {
                new ValueItem ("line", GetPowerLineLineStyle (1)),
                new ValueItem ("minor_line", GetPowerLineLineStyle (1))
            });

            osm_construct_linestring.ZoomLevelSet.ZoomLevel13.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level15;

            osm_construct_linestring.ZoomLevelSet.ZoomLevel16.CustomStyles.Add(powerValueStyle);
            osm_construct_linestring.ZoomLevelSet.ZoomLevel16.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            return osm_construct_linestring;
        }

        private FeatureLayer GetTransportPolygonLayer()
        {
            FeatureLayer osm_transport_polygon = GetFeatureLayer("osm_transport_polygon");

            ValueStyle aerowayValueStyle = new ValueStyle("aeroway", new Collection<ValueItem>() {
                new ValueItem ("apron", GetApronAreaStyle ()),
                new ValueItem ("runway", GetRunwayAreaStyle ())
            });

            ValueStyle manMadeValueStyle = new ValueStyle("man_made", new Collection<ValueItem>() {
                new ValueItem ("pier", GetPierAreaStyle ())
            });

            osm_transport_polygon.ZoomLevelSet.ZoomLevel13.CustomStyles.Add(aerowayValueStyle);
            osm_transport_polygon.ZoomLevelSet.ZoomLevel13.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            WorldStreetsCompositeStyle compositeStyle = new WorldStreetsCompositeStyle();
            compositeStyle.Styles.Add(aerowayValueStyle);
            compositeStyle.Styles.Add(manMadeValueStyle);

            osm_transport_polygon.ZoomLevelSet.ZoomLevel16.CustomStyles.Add(compositeStyle);
            osm_transport_polygon.ZoomLevelSet.ZoomLevel16.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            return osm_transport_polygon;
        }

        private FeatureLayer GetTransportLinestringLayer()
        {
            FeatureLayer osm_transport_linestring = GetFeatureLayer("osm_transport_linestring");

            ValueStyle aerowayLevel13ValueStyle = new ValueStyle("aeroway", new Collection<ValueItem>() {
                new ValueItem ("taxiway", GetTaxiwayLineStyle (1)),
                new ValueItem ("runway", GetRunwayLineStyle (2))
            });

            ValueStyle aerialwayLevel13ValueStyle = new ValueStyle("aerialway", new Collection<ValueItem>() {
                new ValueItem ("cable_car", GetCableCarLineStyle (1))
            });

            osm_transport_linestring.ZoomLevelSet.ZoomLevel13.CustomStyles.Add(GetRailwayLevel13ValueStyle());
            osm_transport_linestring.ZoomLevelSet.ZoomLevel13.CustomStyles.Add(aerowayLevel13ValueStyle);
            osm_transport_linestring.ZoomLevelSet.ZoomLevel13.CustomStyles.Add(aerialwayLevel13ValueStyle);
            osm_transport_linestring.ZoomLevelSet.ZoomLevel13.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level15;

            ValueStyle aerowayLevel16ValueStyle = new ValueStyle("aeroway", new Collection<ValueItem>() {
                new ValueItem ("taxiway", GetTaxiwayLineStyle (3)),
                new ValueItem ("runway", GetRunwayLineStyle (4))
            });

            ValueStyle aerialwayLevel16ValueStyle = new ValueStyle("aerialway", new Collection<ValueItem>() {
                new ValueItem ("cable_car", GetCableCarLineStyle (2))
            });

            ValueStyle manMadeLevel16ValueStyle = new ValueStyle("man_made", new Collection<ValueItem>() {
                new ValueItem ("pier", GetPierLineStyle (2))
            });

            osm_transport_linestring.ZoomLevelSet.ZoomLevel16.CustomStyles.Add(GetRailwayLevel16ValueStyle());
            osm_transport_linestring.ZoomLevelSet.ZoomLevel16.CustomStyles.Add(aerowayLevel16ValueStyle);
            osm_transport_linestring.ZoomLevelSet.ZoomLevel16.CustomStyles.Add(aerialwayLevel16ValueStyle);
            osm_transport_linestring.ZoomLevelSet.ZoomLevel16.CustomStyles.Add(manMadeLevel16ValueStyle);
            osm_transport_linestring.ZoomLevelSet.ZoomLevel16.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level17;

            ValueStyle aerowayLevel18ValueStyle = new ValueStyle("aeroway", new Collection<ValueItem>() {
                new ValueItem ("taxiway", GetTaxiwayLineStyle (6)),
                new ValueItem ("runway", GetRunwayLineStyle (8))
            });

            ValueStyle aerialwayLevel18ValueStyle = new ValueStyle("aerialway", new Collection<ValueItem>() {
                new ValueItem ("cable_car", GetCableCarLineStyle (3))
            });

            ValueStyle manMadeLevel18ValueStyle = new ValueStyle("man_made", new Collection<ValueItem>() {
                new ValueItem ("pier", GetPierLineStyle (4))
            });

            osm_transport_linestring.ZoomLevelSet.ZoomLevel18.CustomStyles.Add(GetRailwayLevel18ValueStyle());
            osm_transport_linestring.ZoomLevelSet.ZoomLevel18.CustomStyles.Add(aerowayLevel18ValueStyle);
            osm_transport_linestring.ZoomLevelSet.ZoomLevel18.CustomStyles.Add(aerialwayLevel18ValueStyle);
            osm_transport_linestring.ZoomLevelSet.ZoomLevel18.CustomStyles.Add(manMadeLevel18ValueStyle);
            osm_transport_linestring.ZoomLevelSet.ZoomLevel18.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            return osm_transport_linestring;
        }

        private FeatureLayer GetRoad20mLinestringLayer()
        {
            FeatureLayer ne_road20m_linestring = GetFeatureLayer("ne_road10m_linestring");
            ne_road20m_linestring.Name = "ne_road20m_linestring";
            SetWhereClause(ne_road20m_linestring, " AND ((\"scalerank\" <= '4')) ");

            ne_road20m_linestring.ZoomLevelSet.ZoomLevel06.CustomStyles.Add(GetHighwayLineStyle(1));

            return ne_road20m_linestring;
        }

        private FeatureLayer GetRoad10mLinestringLayer()
        {
            FeatureLayer ne_road10m_linestring = GetFeatureLayer("ne_road10m_linestring");
            SetWhereClause(ne_road10m_linestring, " AND ((\"scalerank\" <= '6')) ");

            ne_road10m_linestring.ZoomLevelSet.ZoomLevel07.CustomStyles.Add(GetHighwayLineStyle(1));

            return ne_road10m_linestring;
        }

        private FeatureLayer GetRoad5mLinestringLayer()
        {
            FeatureLayer osm_road5m_linestring = GetFeatureLayer("osm_road5m_linestring");
            SetWhereClause(osm_road5m_linestring, " AND \"highway\" in ('motorway', 'trunk') ORDER BY way_z_order ");

            osm_road5m_linestring.ZoomLevelSet.ZoomLevel08.CustomStyles.Add(GetRoadLevel08Styles());

            return osm_road5m_linestring;
        }

        private FeatureLayer GetRoad3mLinestringLayer()
        {
            FeatureLayer osm_road3m_linestring = GetFeatureLayer("osm_road5m_linestring");
            osm_road3m_linestring.Name = "osm_road3m_linestring";
            SetWhereClause(osm_road3m_linestring, " ORDER BY way_z_order ");

            osm_road3m_linestring.ZoomLevelSet.ZoomLevel09.CustomStyles.Add(GetRoadLevel09Styles());
            osm_road3m_linestring.ZoomLevelSet.ZoomLevel10.CustomStyles.Add(GetRoadLevel10Styles());
            osm_road3m_linestring.ZoomLevelSet.ZoomLevel11.CustomStyles.Add(GetRoadLevel11Styles());

            return osm_road3m_linestring;
        }

        private FeatureLayer GetRoad300kLinestringLayer()
        {
            FeatureLayer osm_road300k_linestring = GetFeatureLayer("osm_road300k_linestring");
            SetWhereClause(osm_road300k_linestring, " ORDER BY way_z_order ");

            osm_road300k_linestring.ZoomLevelSet.ZoomLevel12.CustomStyles.Add(GetRoadLevel12Styles());
            osm_road300k_linestring.ZoomLevelSet.ZoomLevel13.CustomStyles.Add(GetRoadLevel13Styles());

            return osm_road300k_linestring;
        }

        private FeatureLayer GetStreetRoadLinestringLayer()
        {
            FeatureLayer osm_road_streets_linestring = GetFeatureLayer("osm_road_streets_linestring");
            SetWhereClause(osm_road_streets_linestring, " ORDER BY way_z_order ");

            osm_road_streets_linestring.ZoomLevelSet.ZoomLevel14.CustomStyles.Add(GetRoadLevel14Styles());
            osm_road_streets_linestring.ZoomLevelSet.ZoomLevel15.CustomStyles.Add(GetRoadLevel15Styles());

            return osm_road_streets_linestring;
        }

        private FeatureLayer GetRoadLinestringLayer()
        {
            FeatureLayer osm_road_linestring = GetFeatureLayer("osm_road_linestring");
            SetWhereClause(osm_road_linestring, " ORDER BY way_z_order ");

            osm_road_linestring.ZoomLevelSet.ZoomLevel16.CustomStyles.Add(GetRoadLevel16Styles());
            osm_road_linestring.ZoomLevelSet.ZoomLevel17.CustomStyles.Add(GetRoadLevel17Styles());
            osm_road_linestring.ZoomLevelSet.ZoomLevel18.CustomStyles.Add(GetRoadLevel18Styles());
            osm_road_linestring.ZoomLevelSet.ZoomLevel19.CustomStyles.Add(GetRoadLevel19Styles());
            osm_road_linestring.ZoomLevelSet.ZoomLevel19.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            return osm_road_linestring;
        }

        private FeatureLayer GetLargeConstructPolygonLayer()
        {
            FeatureLayer osm_construct_large_polygon = GetFeatureLayer("osm_construct_large_polygon");
            SetWhereClause(osm_construct_large_polygon, " ORDER BY osm_area desc ");

            FilterStyle buildingFilterStyle = new FilterStyle();
            buildingFilterStyle.Conditions.Add(new FilterCondition("building", "^(?!).*?$"));
            buildingFilterStyle.Styles.Add(GetBuildingAreaStyle());

            WorldStreetsCompositeStyle compositeStyle = new WorldStreetsCompositeStyle();
            compositeStyle.Styles.Add(buildingFilterStyle);

            osm_construct_large_polygon.ZoomLevelSet.ZoomLevel14.CustomStyles.Add(compositeStyle);
            osm_construct_large_polygon.ZoomLevelSet.ZoomLevel14.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level15;

            return osm_construct_large_polygon;
        }

        private FeatureLayer GetConstructPolygonLayer()
        {
            FeatureLayer osm_construct_polygon = GetFeatureLayer("osm_construct_polygon");
            SetWhereClause(osm_construct_polygon, " ORDER BY osm_area desc ");

            ValueStyle powerValueStyle = new ValueStyle("power", new Collection<ValueItem>() {
                new ValueItem ("station", GetPowerStationAreaStyle ()),
                new ValueItem ("sub_station", GetPowerStationAreaStyle ())
            });

            FilterStyle shadowBuildingFilterStyle = new FilterStyle();
            shadowBuildingFilterStyle.Conditions.Add(new FilterCondition("building", "^(?!).*?$"));
            shadowBuildingFilterStyle.Styles.Add(GetBuildingShadowAreaStyle(1, 1, 1));
            shadowBuildingFilterStyle.Styles.Add(GetBuildingAreaStyle());

            WorldStreetsCompositeStyle level16compositeStyle = new WorldStreetsCompositeStyle();
            level16compositeStyle.Styles.Add(shadowBuildingFilterStyle);
            level16compositeStyle.Styles.Add(powerValueStyle);

            osm_construct_polygon.ZoomLevelSet.ZoomLevel16.CustomStyles.Add(level16compositeStyle);
            osm_construct_polygon.ZoomLevelSet.ZoomLevel16.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level17;

            FilterStyle shadowBuildingFilterStyle2 = new FilterStyle();
            shadowBuildingFilterStyle2.Conditions.Add(new FilterCondition("building", "^(?!).*?$"));
            shadowBuildingFilterStyle2.Styles.Add(GetBuildingShadowAreaStyle(2, 2, 2));
            shadowBuildingFilterStyle2.Styles.Add(GetBuildingAreaStyle());
            shadowBuildingFilterStyle2.Styles.Add(GetGeneralPurposeTextStyle("name", 6));
            shadowBuildingFilterStyle2.Styles.Add(GetGeneralPurposeTextStyle("name:en", 6));

            WorldStreetsCompositeStyle level18compositeStyle = new WorldStreetsCompositeStyle();
            level18compositeStyle.Styles.Add(shadowBuildingFilterStyle2);
            level18compositeStyle.Styles.Add(powerValueStyle);

            osm_construct_polygon.ZoomLevelSet.ZoomLevel18.CustomStyles.Add(level18compositeStyle);
            osm_construct_polygon.ZoomLevelSet.ZoomLevel18.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            return osm_construct_polygon;
        }

        private FeatureLayer GetConstructPointLayer()
        {
            FeatureLayer osm_construct_point = GetFeatureLayer("osm_construct_point");

            ValueStyle powerValueStyle = new ValueStyle("power", new Collection<ValueItem>() {
                new ValueItem ("pole", GetPowerPolePointStyle ()),
                new ValueItem ("tower", GetPowerPolePointStyle ())
            });

            osm_construct_point.ZoomLevelSet.ZoomLevel16.CustomStyles.Add(powerValueStyle);
            osm_construct_point.ZoomLevelSet.ZoomLevel16.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            return osm_construct_point;
        }

        private FeatureLayer GetState2mLinestringLayer()
        {
            FeatureLayer ne_state2m_linestring = GetFeatureLayer("ne_state2m_linestring");
            SetWhereClause(ne_state2m_linestring, "AND ((\"scalerank\" <= '7'))");

            ne_state2m_linestring.ZoomLevelSet.ZoomLevel05.CustomStyles.Add(GetBoundaryMinorLineStyle(1));
            ne_state2m_linestring.ZoomLevelSet.ZoomLevel05.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level07;

            return ne_state2m_linestring;
        }

        private FeatureLayer GetCountry10mLinestringLayer()
        {
            FeatureLayer ne_country10m_linestring = GetFeatureLayer("ne_country10m_linestring");

            ne_country10m_linestring.ZoomLevelSet.ZoomLevel02.CustomStyles.Add(GetBoundaryMinorLineStyle(1));
            ne_country10m_linestring.ZoomLevelSet.ZoomLevel02.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level04;

            ne_country10m_linestring.ZoomLevelSet.ZoomLevel05.CustomStyles.Add(GetBoundaryLineStyle(1));
            ne_country10m_linestring.ZoomLevelSet.ZoomLevel05.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level07;

            return ne_country10m_linestring;
        }

        private FeatureLayer GetCountryStateBoundaryLinestringLayer()
        {
            FeatureLayer osm_boundary_country_state_linestring = GetFeatureLayer("osm_boundary_country_state_linestring");

            ValueStyle boundaryLevel8ValueStyle = new ValueStyle("admin_level", new Collection<ValueItem>() {
                new ValueItem ("2", GetBoundaryNationalLineStyle (2)),
                new ValueItem ("4", GetBoundaryStateLineStyle (1))
            });

            osm_boundary_country_state_linestring.ZoomLevelSet.ZoomLevel08.CustomStyles.Add(boundaryLevel8ValueStyle);
            osm_boundary_country_state_linestring.ZoomLevelSet.ZoomLevel08.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level11;

            return osm_boundary_country_state_linestring;
        }

        private FeatureLayer GetCountryStateCountyBoundaryLinestringLayer()
        {
            FeatureLayer osm_boundary_country_state_county_linestring = GetFeatureLayer("osm_boundary_country_state_county_linestring");

            ValueStyle boundaryLevel12ValueStyle = new ValueStyle("admin_level", new Collection<ValueItem>() {
                new ValueItem ("2", GetBoundaryNationalLineStyle (3)),
                new ValueItem ("4", GetBoundaryStateLineStyle (2)),
                new ValueItem ("6", GetBoundaryCountyLineStyle (1))
            });

            osm_boundary_country_state_county_linestring.ZoomLevelSet.ZoomLevel12.CustomStyles.Add(boundaryLevel12ValueStyle);
            osm_boundary_country_state_county_linestring.ZoomLevelSet.ZoomLevel12.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level13;

            return osm_boundary_country_state_county_linestring;
        }

        private FeatureLayer GetBoundaryLinestringLayer()
        {
            FeatureLayer osm_boundary_linestring = GetFeatureLayer("osm_boundary_linestring");

            ValueStyle boundaryLevel14ValueStyle = new ValueStyle("admin_level", new Collection<ValueItem>() {
                new ValueItem ("2", GetBoundaryNationalLineStyle (4)),
                new ValueItem ("4", GetBoundaryStateLineStyle (3)),
                new ValueItem ("6", GetBoundaryCountyLineStyle (2)),
                new ValueItem ("8", GetBoundaryCityLineStyle (1))
            });

            osm_boundary_linestring.ZoomLevelSet.ZoomLevel14.CustomStyles.Add(boundaryLevel14ValueStyle);
            osm_boundary_linestring.ZoomLevelSet.ZoomLevel14.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            return osm_boundary_linestring;
        }

        private FeatureLayer GetOceanLabelLayer()
        {
            FeatureLayer ne_ocean1m_polygon = GetFeatureLayer("ne_ocean1m_polygon");

            ValueStyle scaleRankValueStyle = new ValueStyle("scalerank", new Collection<ValueItem>() {
                new ValueItem ("0", GetWaterLabelTextStyle ("name", 6))
            });

            ne_ocean1m_polygon.ZoomLevelSet.ZoomLevel03.CustomStyles.Add(scaleRankValueStyle);
            ne_ocean1m_polygon.ZoomLevelSet.ZoomLevel03.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level04;

            ne_ocean1m_polygon.ZoomLevelSet.ZoomLevel05.CustomStyles.Add(GetWaterLabelTextStyle("name", 7));
            ne_ocean1m_polygon.ZoomLevelSet.ZoomLevel05.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level06;

            ne_ocean1m_polygon.ZoomLevelSet.ZoomLevel07.CustomStyles.Add(GetWaterLabelTextStyle("name", 8));
            ne_ocean1m_polygon.ZoomLevelSet.ZoomLevel07.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level08;

            ne_ocean1m_polygon.ZoomLevelSet.ZoomLevel09.CustomStyles.Add(GetWaterLabelTextStyle("name", 9));
            ne_ocean1m_polygon.ZoomLevelSet.ZoomLevel09.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level10;

            ne_ocean1m_polygon.ZoomLevelSet.ZoomLevel11.CustomStyles.Add(GetWaterLabelTextStyle("name", 10));
            ne_ocean1m_polygon.ZoomLevelSet.ZoomLevel11.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level12;

            return ne_ocean1m_polygon;
        }

        private FeatureLayer GetStateLabelLayer()
        {
            FeatureLayer ne_state5m_polygon = GetFeatureLayer("ne_state5m_polygon");

            ne_state5m_polygon.ZoomLevelSet.ZoomLevel05.CustomStyles.Add(GetGeneralPurposeTextStyle("name", 7));
            ne_state5m_polygon.ZoomLevelSet.ZoomLevel05.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level06;

            ne_state5m_polygon.ZoomLevelSet.ZoomLevel07.CustomStyles.Add(GetGeneralPurposeTextStyle("name", 8));
            ne_state5m_polygon.ZoomLevelSet.ZoomLevel07.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level08;

            ne_state5m_polygon.ZoomLevelSet.ZoomLevel09.CustomStyles.Add(GetGeneralPurposeTextStyle("name", 9));
            ne_state5m_polygon.ZoomLevelSet.ZoomLevel09.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level10;

            ne_state5m_polygon.ZoomLevelSet.ZoomLevel11.CustomStyles.Add(GetGeneralPurposeTextStyle("name", 10));
            ne_state5m_polygon.ZoomLevelSet.ZoomLevel11.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level12;

            return ne_state5m_polygon;
        }

        private FeatureLayer GetPlace2mLabelLayer()
        {
            FeatureLayer ne_place2m_point = GetFeatureLayer("ne_place2m_point");

            ne_place2m_point.ZoomLevelSet.ZoomLevel05.CustomStyles.Add(GetGeneralPurposeTextStyle("name", 6));
            ne_place2m_point.ZoomLevelSet.ZoomLevel05.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level07;
            return ne_place2m_point;
        }

        private FeatureLayer GetPlaceCityLabelLayer()
        {
            FeatureLayer osm_place_city_point = GetFeatureLayer("osm_place_city_point");

            ValueStyle placeLevel08ValueStyle = new ValueStyle("place", new Collection<ValueItem>() {
                new ValueItem ("city", GetGeneralPurposeTextStyle ("name", 7))
            });

            osm_place_city_point.ZoomLevelSet.ZoomLevel08.CustomStyles.Add(placeLevel08ValueStyle);

            return osm_place_city_point;
        }

        private FeatureLayer GetPlaceCityTownLabelLayer()
        {
            FeatureLayer osm_place_city_town_point = GetFeatureLayer("osm_place_city_town_point");
            SetWhereClause(osm_place_city_town_point, " ORDER BY place ");

            ValueStyle placeLevel09ValueStyle = new ValueStyle("place", new Collection<ValueItem>() {
                new ValueItem ("city", GetGeneralPurposeTextStyle ("name", 8)),
                new ValueItem ("town", GetGeneralPurposeTextStyle ("name", 7))
            });

            osm_place_city_town_point.ZoomLevelSet.ZoomLevel09.CustomStyles.Add(placeLevel09ValueStyle);
            osm_place_city_town_point.ZoomLevelSet.ZoomLevel09.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level10;

            return osm_place_city_town_point;
        }

        private FeatureLayer GetPlaceLabelLayer()
        {
            FeatureLayer osm_place_point = GetFeatureLayer("osm_place_point");
            SetWhereClause(osm_place_point, " ORDER BY place ");

            ValueStyle placeLevel11ValueStyle = new ValueStyle("place", new Collection<ValueItem>() {
                new ValueItem ("city", GetGeneralPurposeTextStyle ("name", 8)),
                new ValueItem ("town", GetGeneralPurposeTextStyle ("name", 7)),
                new ValueItem ("village", GetGeneralPurposeTextStyle ("name", 6)),
            });

            osm_place_point.ZoomLevelSet.ZoomLevel11.CustomStyles.Add(placeLevel11ValueStyle);
            osm_place_point.ZoomLevelSet.ZoomLevel11.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level12;

            ValueStyle placeLevel13ValueStyle = new ValueStyle("place", new Collection<ValueItem>() {
                new ValueItem ("city", GetGeneralPurposeTextStyle ("name", 9)),
                new ValueItem ("town", GetGeneralPurposeTextStyle ("name", 8)),
                new ValueItem ("village", GetGeneralPurposeTextStyle ("name", 7)),
            });

            osm_place_point.ZoomLevelSet.ZoomLevel13.CustomStyles.Add(placeLevel13ValueStyle);
            osm_place_point.ZoomLevelSet.ZoomLevel13.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level14;

            ValueStyle placeLevel15ValueStyle = new ValueStyle("place", new Collection<ValueItem>() {
                new ValueItem ("city", GetGeneralPurposeTextStyle ("name", 10)),
                new ValueItem ("town", GetGeneralPurposeTextStyle ("name", 9)),
                new ValueItem ("village", GetGeneralPurposeTextStyle ("name", 8)),
            });

            osm_place_point.ZoomLevelSet.ZoomLevel15.CustomStyles.Add(placeLevel15ValueStyle);
            osm_place_point.ZoomLevelSet.ZoomLevel15.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            return osm_place_point;
        }

        private FeatureLayer GetPoiPointLayer()
        {
            FeatureLayer osm_poi_point = GetFeatureLayer("osm_poi_point");

            CompositeStyle compositeStyle = new CompositeStyle();

            compositeStyle.Styles.Add(new ValueStyle("aeroway", new Collection<ValueItem>() {
                new ValueItem ("terminal", GetAerowayTerminalPointStyle ()),
                new ValueItem ("helipad", GetAerowayHelipadPointStyle ()),
                new ValueItem ("gate", GetAerowayGatePointStyle ()),
                new ValueItem ("aerodrome", GetAerowayAerodromePointStyle ())
            }));

            compositeStyle.Styles.Add(new ValueStyle("amenity", new Collection<ValueItem>() {
                new ValueItem ("waste_disposal", GetAmenityWasteDisposalPointStyle ()),
                new ValueItem ("veterinary", GetAmenityVeterinaryPointStyle ()),
                new ValueItem ("vending_machine", GetAmenityVendingMachinePointStyle ()),
                new ValueItem ("university", GetAmenityUniversityPointStyle ()),
                new ValueItem ("townhall", GetAmenityTownHallPointStyle ()),
                new ValueItem ("theatre", GetAmenityTheatrePointStyle ()),
                new ValueItem ("telephone", GetAmenityTelephonePointStyle ()),
                new ValueItem ("taxi", GetAmenityTaxiPointStyle ()),
                new ValueItem ("shelter", GetAmenityShelterPointStyle ()),
                new ValueItem ("school", GetAmenitySchoolPointStyle ()),
                new ValueItem ("restaurant", GetAmenityRestaurantPointStyle ()),
                new ValueItem ("recycling", GetAmenityRecyclingPointStyle ()),
                new ValueItem ("pub", GetAmenityPubPointStyle ()),
                new ValueItem ("prison", GetAmenityPrisonPointStyle ()),
                new ValueItem ("post_office", GetAmenityPostOfficePointStyle ()),
                new ValueItem ("post_box", GetAmenityPostBoxPointStyle ()),
                new ValueItem ("police", GetAmenityPolicePointStyle ()),
                new ValueItem ("nursing_home", GetAmenityNursingHomePointStyle ()),
                new ValueItem ("library", GetAmenityLibraryPointStyle ()),
                new ValueItem ("ice_cream", GetAmenityIceCreamPointStyle ()),
                new ValueItem ("fountain", GetAmenityFountainPointStyle ()),
                new ValueItem ("fire_station", GetAmenityFireStationPointStyle ()),
                new ValueItem ("embassy", GetAmenityEmbassyPointStyle ()),
                new ValueItem ("drinking_water", GetAmenityDrinkingWaterPointStyle ()),
                new ValueItem ("doctors", GetAmenityDoctorsPointStyle ()),
                new ValueItem ("dentist", GetAmenityDentistPointStyle ()),
                new ValueItem ("court_house", GetAmenityCourtHousePointStyle ()),
                new ValueItem ("college", GetAmenityCollegePointStyle ()),
                new ValueItem ("clock", GetAmenityClockPointStyle ()),
                new ValueItem ("cinema", GetAmenityCinemaPointStyle ()),
                new ValueItem ("car_rental", GetAmenityCarRentalPointStyle ()),
                new ValueItem ("casino", GetAmenityCasinoPointStyle ()),
                new ValueItem ("cafe", GetAmenityCafePointStyle ()),
                new ValueItem ("bus_station", GetAmenityBusStationPointStyle ()),
                new ValueItem ("biergarten", GetAmenityBiergartenPointStyle ()),
                new ValueItem ("bicycle_rental", GetAmenityBicycleRentalPointStyle ()),
                new ValueItem ("bicycle_parking", GetAmenityBicycleParkingPointStyle ()),
                new ValueItem ("bench", GetAmenityBenchPointStyle ()),
                new ValueItem ("bar", GetAmenityBarPointStyle ()),
                new ValueItem ("toilets", new Collection<Style> () {
                    new ValueStyle ("disability_access", new Collection<ValueItem> () {
                        new ValueItem ("yes", GetAmenityToiletsDisabilityAccessPointStyle ())
                    }),
                    new ValueStyle ("gender_access", new Collection<ValueItem> () {
                        new ValueItem ("men", GetAmenityToiletsGenderAccessMenPointStyle ()),
                        new ValueItem ("women", GetAmenityToiletsGenderAccessWomenPointStyle ()),
                    }),
                    GetAmenityToiletsPointStyle ()
                }),
                new ValueItem ("place_of_worship", new Collection<Style> () {
                    new ValueStyle ("religion", new Collection<ValueItem> () {
                        new ValueItem ("sikh", GetAmenityPlaceOfWorshipSikhPointStyle ()),
                        new ValueItem ("shinto", GetAmenityPlaceOfWorshipShintoPointStyle ()),
                        new ValueItem ("jain", GetAmenityPlaceOfWorshipJainPointStyle ()),
                        new ValueItem ("muslim", GetAmenityPlaceOfWorshipIslamicPointStyle ()),
                        new ValueItem ("hindu", GetAmenityPlaceOfWorshipHinduPointStyle ()),
                        new ValueItem ("christian", GetAmenityPlaceOfWorshipChristianPointStyle ()),
                        new ValueItem ("buddhist", GetAmenityPlaceOfWorshipBuddhistPointStyle ()),
                        new ValueItem ("jewish", GetAmenityPlaceOfWorshipJewishPointStyle ()),
                        new ValueItem ("", GetAmenityPlaceOfWorshipPointStyle ())
                    })
                }),
                new ValueItem ("hospital", new Collection<Style> () {
                    new ValueStyle ("emergency", new Collection<ValueItem> () {
                        new ValueItem ("yes", GetAmenityHospitalEmergencyPointStyle ()),
                        new ValueItem ("", GetAmenityHospitalPointStyle ())
                    })
                }),
                new ValueItem ("parking", new Collection<Style> () {
                    new ValueStyle ("fee", new Collection<ValueItem> () {
                        new ValueItem ("yes", GetAmenityParkingCarPaidPointStyle ())
                    }),
                    new ValueStyle ("access", new Collection<ValueItem> () {
                        new ValueItem ("private", GetAmenityParkingPrivatePointStyle ())
                    }),
                    new ValueStyle ("wheelchair", new Collection<ValueItem> () {
                        new ValueItem ("yes", GetAmenityParkingDisabledPointStyle ())
                    }),
                }),
                new ValueItem ("parking_space", new Collection<Style> () {
                    new ValueStyle ("fee", new Collection<ValueItem> () {
                        new ValueItem ("yes", GetAmenityParkingCarPaidPointStyle ())
                    }),
                    new ValueStyle ("access", new Collection<ValueItem> () {
                        new ValueItem ("private", GetAmenityParkingPrivatePointStyle ())
                    }),
                    new ValueStyle ("wheelchair", new Collection<ValueItem> () {
                        new ValueItem ("yes", GetAmenityParkingDisabledPointStyle ())
                    }),
                }),
                new ValueItem ("parking_entrance", new Collection<Style> () {
                    new ValueStyle ("fee", new Collection<ValueItem> () {
                        new ValueItem ("yes", GetAmenityParkingCarPaidPointStyle ())
                    }),
                    new ValueStyle ("access", new Collection<ValueItem> () {
                        new ValueItem ("private", GetAmenityParkingPrivatePointStyle ())
                    }),
                    new ValueStyle ("wheelchair", new Collection<ValueItem> () {
                        new ValueItem ("yes", GetAmenityParkingDisabledPointStyle ())
                    }),
                }),
                new ValueItem ("pharmacy", new Collection<Style> () {
                    new ValueStyle ("dispensing", new Collection<ValueItem> () {
                        new ValueItem ("yes", GetAmenityPharmacyDispencingPointStyle ()),
                        new ValueItem ("", GetAmenityPharmacyPointStyle ())
                    })
                }),
                new ValueItem ("fuel", new Collection<Style> () {
                    new ValueStyle ("fuel:lpg", new Collection<ValueItem> () {
                        new ValueItem ("yes", GetAmenityFuelLpgPointStyle ()),
                        new ValueItem ("", GetAmenityFuelPointStyle ())
                    })
                }),
                new ValueItem ("fast_food", new Collection<Style> () {
                    new ValueStyle ("cuisine", new Collection<ValueItem> () {
                        new ValueItem ("pizza", GetAmenityFastFoodPizzaPointStyle ())
                    }),
                    GetAmenityFastFoodPointStyle ()
                })
            }));

            compositeStyle.Styles.Add(new ValueStyle("barrier", new Collection<ValueItem>() {
                new ValueItem ("tollbooth", GetBarrierTollboothPointStyle ()),
                new ValueItem ("stile", GetBarrierStilePointStyle ()),
                new ValueItem ("lift_gate", GetBarrierLiftGatePointStyle ()),
                new ValueItem ("kissing_gate", GetBarrierKissingGatePointStyle ()),
                new ValueItem ("gate", GetBarrierGatePointStyle ()),
                new ValueItem ("entrance", GetBarrierEntrancePointStyle ()),
                new ValueItem ("cycle_barrier", GetBarrierCycleBarrierPointStyle ()),
                new ValueItem ("cattle_grid", GetBarrierCattleGridPointStyle ()),
                new ValueItem ("bollard", GetBarrierBollardPointStyle ()),
                new ValueItem ("block", GetBarrierBlockPointStyle ())
            }));

            compositeStyle.Styles.Add(new ValueStyle("ford", new Collection<ValueItem>() {
                new ValueItem ("yes", GetFordPointStyle ())
            }));

            compositeStyle.Styles.Add(new ValueStyle("historic", new Collection<ValueItem>() {
                new ValueItem ("wreck", GetHistoricWreckPointStyle ()),
                new ValueItem ("wayside_shrine", GetHistoricWayside_ShrinePointStyle ()),
                new ValueItem ("wayside_cross", GetHistoricWaysideCrossPointStyle ()),
                new ValueItem ("ruins", GetHistoricRuinsPointStyle ()),
                new ValueItem ("monument", GetHistoricMonumentPointStyle ()),
                new ValueItem ("memorial", GetHistoricMemorialPointStyle ()),
                new ValueItem ("castle", GetHistoricCastlePointStyle ()),
                new ValueItem ("battlefield", GetHistoricBattlefieldPointStyle ())
            }));

            compositeStyle.Styles.Add(new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("traffic_signals", GetHighwayTrafficSignalsPointStyle ()),
                new ValueItem ("bus_stop", GetHighwayBusStopPointStyle ()),
                new ValueItem ("mini_roundabout", new Collection<Style> () {
                    new ValueStyle ("direction", new Collection<ValueItem> () {
                        new ValueItem ("clockwise", GetHighwayMiniRoundaboutClockwisePointStyle ()),
                        new ValueItem ("anticlockwise", GetHighwayMiniRoundaboutAnticlockwisePointStyle ())
                    })
                })
            }));

            compositeStyle.Styles.Add(new ValueStyle("leisure", new Collection<ValueItem>() {
                new ValueItem ("stadium", GetLeisureStadiumPointStyle ()),
                new ValueItem ("slipway", GetLeisureSlipwayPointStyle ()),
                new ValueItem ("marina", GetLeisureMarinaPointStyle ())
            }));

            compositeStyle.Styles.Add(new ValueStyle("man_made", new Collection<ValueItem>() {
                new ValueItem ("windmill", GetManMadeWindmillPointStyle ()),
                new ValueItem ("water_tower", GetManMadeWaterTowerPointStyle ()),
                new ValueItem ("lighthouse", GetManMadeLighthousePointStyle ()),
                new ValueItem ("crane", GetManMadeCranePointStyle ()),
                new ValueItem ("mineshaft", new Collection<Style> () {
                    new ValueStyle ("disused", new Collection<ValueItem> () {
                        new ValueItem ("yes", GetManMadeMineshaftAbandonedPointStyle ())
                    }),
                    GetManMadeMineshaftPointStyle ()
                })
            }));

            compositeStyle.Styles.Add(new ValueStyle("military", new Collection<ValueItem>() {
                new ValueItem ("bunker", GetMilitaryBunkerPointStyle ())
            }));

            compositeStyle.Styles.Add(new ValueStyle("natural", new Collection<ValueItem>() {
                new ValueItem ("peak", GetNaturalPeakPointStyle ()),
                new ValueItem ("cave_entrance", GetNaturalCavePointStyle ()),
                new ValueItem ("wood", new Collection<Style> () {
                    new ValueStyle ("wood", new Collection<ValueItem> () {
                        new ValueItem ("coniferous", GetNaturalWoodConiferousPointStyle ()),
                        new ValueItem ("deciduous", GetNaturalWoodDeciduousPointStyle ()),
                        new ValueItem ("mixed", GetNaturalWoodMixedPointStyle ())
                    })
                })
            }));

            compositeStyle.Styles.Add(new ValueStyle("office", new Collection<ValueItem>() {
                new ValueItem ("estate_agent", GetOfficeEstateAgentPointStyle ())
            }));

            compositeStyle.Styles.Add(new ValueStyle("power", new Collection<ValueItem>() {
                new ValueItem ("tower", GetPowerTowerPointStyle ()),
                new ValueItem ("sub_station", GetPowerSubStationPointStyle ()),
                new ValueItem ("plant", new Collection<Style> () {
                    new ValueStyle ("plant:source", new Collection<ValueItem> () {
                        new ValueItem ("hydro", GetPowerPlantHydroPointStyle ()),
                        new ValueItem ("gas", GetPowerPlantGasPointStyle ()),
                        new ValueItem ("solar", GetPowerPlantSolarPointStyle ()),
                        new ValueItem ("wind", GetPowerPlantWindPointStyle ()),
                    }),
                    GetPowerPlantCoalPointStyle ()
                })
            }));

            compositeStyle.Styles.Add(new ValueStyle("railway", new Collection<ValueItem>() {
                new ValueItem ("tram_stop", GetRailwayTramStopPointStyle ()),
                new ValueItem ("subway", GetRailwaySubwayPointStyle ())
            }));

            compositeStyle.Styles.Add(new ValueStyle("shop", new Collection<ValueItem>() {
                new ValueItem ("video", GetShopVideoPointStyle ()),
                new ValueItem ("toys", GetShopToysPointStyle ()),
                new ValueItem ("tobacco", GetShopTobaccoPointStyle ()),
                new ValueItem ("supermarket", GetShopSupermarketPointStyle ()),
                new ValueItem ("pet", GetShopPetPointStyle ()),
                new ValueItem ("optician", GetShopOpticianPointStyle ()),
                new ValueItem ("newsagent", GetShopNewsagentPointStyle ()),
                new ValueItem ("musical_instrument", GetShopMusicalInstrumentPointStyle ()),
                new ValueItem ("motorcycle", GetShopMotorcyclePointStyle ()),
                new ValueItem ("mobile_phone", GetShopMobliePhonePointStyle ()),
                new ValueItem ("laundry", GetShopLaundryPointStyle ()),
                new ValueItem ("kiosk", GetShopKioskPointStyle ()),
                new ValueItem ("jewelry", GetShopJewelryPointStyle ()),
                new ValueItem ("hifi", GetShopHifiPointStyle ()),
                new ValueItem ("hearing_aids", GetShopHearingAidsPointStyle ()),
                new ValueItem ("hairdresser", GetShopHairdresserPointStyle ()),
                new ValueItem ("greengrocer", GetShopGreengrocerPointStyle ()),
                new ValueItem ("gift", GetShopGiftPointStyle ()),
                new ValueItem ("garden_centre", GetShopGardenCentrePointStyle ()),
                new ValueItem ("florist", GetShopFloristPointStyle ()),
                new ValueItem ("doityourself", GetShopDoItYourselfPointStyle ()),
                new ValueItem ("department_store", GetShopDepartmentStorePointStyle ()),
                new ValueItem ("copyshop", GetShopCopyshopPointStyle ()),
                new ValueItem ("convenience", GetShopConveniencePointStyle ()),
                new ValueItem ("confectionery", GetShopConfectioneryPointStyle ()),
                new ValueItem ("computer", GetShopComputerPointStyle ()),
                new ValueItem ("clothes", GetShopClothesPointStyle ()),
                new ValueItem ("car_repair", GetShopCarRepairPointStyle ()),
                new ValueItem ("car_parts", GetShopCarRepairPointStyle ()),
                new ValueItem ("car", GetShopCarPointStyle ()),
                new ValueItem ("butcher", GetShopButcherPointStyle ()),
                new ValueItem ("books", GetShopBooksPointStyle ()),
                new ValueItem ("bicycle", GetShopBicyclePointStyle ()),
                new ValueItem ("bakery", GetShopBakeryPointStyle ()),
                new ValueItem ("alcohol", GetShopAlcoholPointStyle ())
            }));

            compositeStyle.Styles.Add(new ValueStyle("sport", new Collection<ValueItem>() {
                new ValueItem ("tennis", GetSportTennisPointStyle ()),
                new ValueItem ("soccer", GetSportSoccerPointStyle ()),
                new ValueItem ("shooting", GetSportShootingPointStyle ()),
                new ValueItem ("motor", GetSportMotorPointStyle ()),
                new ValueItem ("ice_stock", GetSportIceStockPointStyle ()),
                new ValueItem ("horse_racing", GetSportHorseRacingPointStyle ()),
                new ValueItem ("gymnastics", GetSportGymnasticsPointStyle ()),
                new ValueItem ("golf", GetSportGolfPointStyle ()),
                new ValueItem ("diving", GetSportDivingPointStyle ()),
                new ValueItem ("cricket", GetSportCricketPointStyle ()),
                new ValueItem ("climbing", GetSportClimbingPointStyle ()),
                new ValueItem ("canoe", GetSportCanoePointStyle ()),
                new ValueItem ("baseball", GetSportBaseballPointStyle ()),
                new ValueItem ("archery", GetSportArcheryPointStyle ()),
                new ValueItem ("skiing", GetSportSkiingPointStyle ()),
                new ValueItem ("swimming", new Collection<Style> () {
                    new ValueStyle ("covered", new Collection<ValueItem> () {
                        new ValueItem ("yes", GetSportTennisPointStyle ())
                    }),
                    GetSportSwimmingCoveredPointStyle ()
                })
            }));

            compositeStyle.Styles.Add(new ValueStyle("tourism", new Collection<ValueItem>() {
                new ValueItem ("zoo", GetTourismZooPointStyle ()),
                new ValueItem ("viewpoint", GetTourismViewpointPointStyle ()),
                new ValueItem ("theme_park", GetTourismThemeParkPointStyle ()),
                new ValueItem ("picnic_site", GetTourismPicnicStiePointStyle ()),
                new ValueItem ("museum", GetTourismMuseumPointStyle ()),
                new ValueItem ("motel", GetTourismMotelPointStyle ()),
                new ValueItem ("hotel", GetTourismHotelPointStyle ()),
                new ValueItem ("hostel", GetTourismHostelPointStyle ()),
                new ValueItem ("chalet", GetTourismChaletPointStyle ()),
                new ValueItem ("caravan_site", GetTourismCaravanSitePointStyle ()),
                new ValueItem ("camp_site", GetTourismCampSitePointStyle ()),
                new ValueItem ("attraction", GetTourismAttractionPointStyle ()),
                new ValueItem ("artwork", GetTourismArtworkPointStyle ()),
                new ValueItem ("alpine_hut", GetTourismAlpineHutPointStyle ()),
                new ValueItem ("information", new Collection<Style> () {
                    new ValueStyle ("information", new Collection<ValueItem> () {
                        new ValueItem ("guidepost", GetTourismInformationGuidepostPointStyle ()),
                        new ValueItem ("map", GetTourismInformationMapPointStyle ()),
                    }),
                    GetTourismInformationPointStyle ()
                })
            }));

            compositeStyle.Styles.Add(new ValueStyle("waterway", new Collection<ValueItem>() {
                new ValueItem ("weir", GetWaterwayWeirPointStyle ())
            }));

            compositeStyle.Styles.Add(GetPoiLabelTextStyle("name", 8));
            compositeStyle.Styles.Add(GetPoiLabelTextStyle("name:en", 8));

            osm_poi_point.ZoomLevelSet.ZoomLevel19.CustomStyles.Add(compositeStyle);
            osm_poi_point.ZoomLevelSet.ZoomLevel19.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            return osm_poi_point;
        }

        /********************************/

        private ValueStyle GetRoadLevel08Styles()
        {
            return new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetHighwayLineStyle (1)),
                new ValueItem ("trunk", GetTrunkFillLineStyle (1))
            });
        }

        private ValueStyle GetRoadLevel09Styles()
        {
            return new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetHighwayLineStyle (2)),
                new ValueItem ("trunk", GetTrunkFillLineStyle (2)),
                new ValueItem ("primary", GetPrimaryOutlineLineStyle (1))
            });
        }

        private CompositeStyle GetRoadLevel10Styles()
        {
            ValueStyle outline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayOutlineLineStyle (3)),
                new ValueItem ("trunk", GetTrunkOutlineLineStyle (3)),
                new ValueItem ("primary", GetPrimaryOutlineLineStyle (2))
            });

            ValueStyle fill = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetHighwayLineStyle (1)),
                new ValueItem ("trunk", GetTrunkFillLineStyle (1))
            });

            ValueStyle label = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayRoadSheildLabelTextStyle ("ref", 6))
            });

            FilterStyle highwayLevel10FilterStyle = new FilterStyle();
            highwayLevel10FilterStyle.Conditions.Add(new FilterCondition("ref", "^((?!;).)*$"));
            highwayLevel10FilterStyle.Styles.Add(label);

            CompositeStyle compositeStyle = new CompositeStyle();
            compositeStyle.Styles.Add(outline);
            compositeStyle.Styles.Add(fill);
            compositeStyle.Styles.Add(highwayLevel10FilterStyle);

            return compositeStyle;
        }

        private CompositeStyle GetRoadLevel11Styles()
        {
            ValueStyle highwayLevel11Outline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayOutlineLineStyle (4)),
                new ValueItem ("trunk", GetTrunkOutlineLineStyle (4)),
                new ValueItem ("primary", GetPrimaryOutlineLineStyle (3)),
                new ValueItem ("secondary", GetRoadOutlineLineStyle (1))
            });

            ValueStyle highwayLevel11Fill = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayFillLineStyle (2)),
                new ValueItem ("trunk", GetTrunkFillLineStyle (2)),
                new ValueItem ("primary", GetPrimaryFillLineStyle (1))
            });

            ValueStyle highwayLevel11Label = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayRoadSheildLabelTextStyle ("ref", 6))
            });

            FilterStyle highwayLevel11FilterStyle = new FilterStyle();
            highwayLevel11FilterStyle.Conditions.Add(new FilterCondition("ref", "^((?!;).)*$"));
            highwayLevel11FilterStyle.Styles.Add(highwayLevel11Label);

            CompositeStyle level11CompositeStyle = new CompositeStyle();
            level11CompositeStyle.Styles.Add(highwayLevel11Outline);
            level11CompositeStyle.Styles.Add(highwayLevel11Fill);
            level11CompositeStyle.Styles.Add(highwayLevel11FilterStyle);
            return level11CompositeStyle;
        }

        private CompositeStyle GetRoadLevel12Styles()
        {
            ValueStyle highwayLevel12Outline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayOutlineLineStyle (4)),
                new ValueItem ("trunk", GetTrunkOutlineLineStyle (4)),
                new ValueItem ("primary", GetPrimaryOutlineLineStyle (3)),
                new ValueItem ("secondary", GetRoadOutlineLineStyle (2)),
                new ValueItem ("tertiary", GetRoadOutlineLineStyle (1))
            });

            ValueStyle highwayLevel12Fill = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayFillLineStyle (2)),
                new ValueItem ("trunk", GetTrunkFillLineStyle (2)),
                new ValueItem ("primary", GetPrimaryFillLineStyle (1))
            });

            ValueStyle highwayLevel12Label = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayRoadSheildLabelTextStyle ("ref", 6)),
                new ValueItem ("trunk", GetTrunkRoadSheildLabelTextStyle ("ref", 6))
            });

            FilterStyle highwayLevel12FilterStyle = new FilterStyle();
            highwayLevel12FilterStyle.Conditions.Add(new FilterCondition("ref", "^((?!;).)*$"));
            highwayLevel12FilterStyle.Styles.Add(highwayLevel12Label);

            CompositeStyle level12CompositeStyle = new CompositeStyle();
            level12CompositeStyle.Styles.Add(highwayLevel12Outline);
            level12CompositeStyle.Styles.Add(highwayLevel12Fill);
            level12CompositeStyle.Styles.Add(highwayLevel12FilterStyle);
            return level12CompositeStyle;
        }

        private CompositeStyle GetRoadLevel13Styles()
        {
            CompositeStyle styles = new CompositeStyle();

            float motorwayOutlineWidth = 6;
            float motorwayFillWidth = 4;
            float motorwayFontSize = 6;

            float trunkOutlineWidth = 6;
            float trunkFillWidth = 4;
            float trunkFontSize = 6;

            float primaryOutlineWidth = 4;
            float primaryFillWidth = 2;
            float primaryFontSize = 6;

            float secondaryOutlineWidth = 3;
            float secondaryFillWidth = 1;

            float tertiaryOutlineWidth = 3;
            float tertiaryFillWidth = 1;

            float motorwayLinkOutlineWidth = 2;

            float trunkLinkOutlineWidth = 2;

            float primaryLinkOutlineWidth = 2;

            float secondaryLinkOutlineWidth = 2;

            float tertiaryLinkOutlineWidth = 2;

            float residentialOutlineWidth = 1;

            float unclassifiedOutlineWidth = 1;

            float roadOutlineWidth = 1;

            ValueStyle outline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayOutlineLineStyle (motorwayOutlineWidth)),
                new ValueItem ("trunk", GetTrunkOutlineLineStyle (trunkOutlineWidth)),
                new ValueItem ("primary", GetPrimaryOutlineLineStyle (primaryOutlineWidth)),
                new ValueItem ("secondary", GetRoadOutlineLineStyle (secondaryOutlineWidth)),
                new ValueItem ("tertiary", GetRoadOutlineLineStyle (tertiaryOutlineWidth)),
                new ValueItem ("motorway_link", GetMotorwayLinkOutlineLineStyle (motorwayLinkOutlineWidth)),
                new ValueItem ("trunk_link", GetTrunkLinkOutlineLineStyle (trunkLinkOutlineWidth)),
                new ValueItem ("primary_link", GetPrimaryLinkOutlineLineStyle (primaryLinkOutlineWidth)),
                new ValueItem ("secondary_link", GetRoadOutlineLineStyle (secondaryLinkOutlineWidth)),
                new ValueItem ("tertiary_link", GetRoadOutlineLineStyle (tertiaryLinkOutlineWidth)),
                new ValueItem ("residential", GetRoadOutlineLineStyle (residentialOutlineWidth)),
                new ValueItem ("unclassified", GetRoadOutlineLineStyle (unclassifiedOutlineWidth)),
                new ValueItem ("road", GetRoadOutlineLineStyle (roadOutlineWidth))
            });

            ValueStyle fill = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayFillLineStyle (motorwayFillWidth)),
                new ValueItem ("trunk", GetTrunkFillLineStyle (trunkFillWidth)),
                new ValueItem ("primary", GetPrimaryFillLineStyle (primaryFillWidth)),
                new ValueItem ("secondary", GetRoadFillLineStyle (secondaryFillWidth)),
                new ValueItem ("tertiary", GetRoadFillLineStyle (tertiaryFillWidth))
            });

            ValueStyle label = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", motorwayFontSize),
                    GetGeneralPurposeTextStyle ("name:en", motorwayFontSize),
                    GetMotorwayRoadSheildLabelTextStyle ("ref", motorwayFontSize)
                }),
                new ValueItem ("trunk", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", trunkFontSize),
                    GetGeneralPurposeTextStyle ("name:en", trunkFontSize),
                    GetTrunkRoadSheildLabelTextStyle ("ref", trunkFontSize)
                }),
                new ValueItem ("primary", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", primaryFontSize),
                    GetGeneralPurposeTextStyle ("name:en", primaryFontSize),
                    GetPrimaryRoadSheildLabelTextStyle ("ref", primaryFontSize)
                })
            });

            FilterStyle filterStyle = new FilterStyle();
            filterStyle.Conditions.Add(new FilterCondition("ref", "^((?!;).)*$"));
            filterStyle.Styles.Add(label);

            styles.Styles.Add(outline);
            styles.Styles.Add(fill);
            styles.Styles.Add(filterStyle);

            return styles;
        }

        private CompositeStyle GetRoadLevel14Styles()
        {
            CompositeStyle styles = new CompositeStyle();

            float motorwayOutlineWidth = 6;
            float motorwayFillWidth = 4;
            float motorwayFontSize = 6;

            float trunkOutlineWidth = 6;
            float trunkFillWidth = 4;
            float trunkFontSize = 6;

            float primaryOutlineWidth = 4;
            float primaryFillWidth = 2;
            float primaryFontSize = 6;

            float secondaryOutlineWidth = 3;
            float secondaryFillWidth = 1;

            float tertiaryOutlineWidth = 3;
            float tertiaryFillWidth = 1;

            float motorwayLinkOutlineWidth = 2;

            float trunkLinkOutlineWidth = 2;

            float primaryLinkOutlineWidth = 2;

            float secondaryLinkOutlineWidth = 2;

            float tertiaryLinkOutlineWidth = 2;

            float residentialOutlineWidth = 2;

            float unclassifiedOutlineWidth = 2;

            float roadOutlineWidth = 2;

            float livingStreetOutlineWidth = 1;

            float serviceOutlineWidth = 1;

            ValueStyle outline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayOutlineLineStyle (motorwayOutlineWidth)),
                new ValueItem ("trunk", GetTrunkOutlineLineStyle (trunkOutlineWidth)),
                new ValueItem ("primary", GetPrimaryOutlineLineStyle (primaryOutlineWidth)),
                new ValueItem ("secondary", GetRoadOutlineLineStyle (secondaryOutlineWidth)),
                new ValueItem ("tertiary", GetRoadOutlineLineStyle (tertiaryOutlineWidth)),
                new ValueItem ("motorway_link", GetMotorwayLinkOutlineLineStyle (motorwayLinkOutlineWidth)),
                new ValueItem ("trunk_link", GetTrunkLinkOutlineLineStyle (trunkLinkOutlineWidth)),
                new ValueItem ("primary_link", GetPrimaryLinkOutlineLineStyle (primaryLinkOutlineWidth)),
                new ValueItem ("secondary_link", GetRoadOutlineLineStyle (secondaryLinkOutlineWidth)),
                new ValueItem ("tertiary_link", GetRoadOutlineLineStyle (tertiaryLinkOutlineWidth)),
                new ValueItem ("residential", GetRoadOutlineLineStyle (residentialOutlineWidth)),
                new ValueItem ("unclassified", GetRoadOutlineLineStyle (unclassifiedOutlineWidth)),
                new ValueItem ("road", GetRoadOutlineLineStyle (roadOutlineWidth)),
                new ValueItem ("living_street", GetRoadOutlineLineStyle (livingStreetOutlineWidth)),
                new ValueItem ("service", GetRoadOutlineLineStyle (serviceOutlineWidth))
            });

            ValueStyle fill = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayFillLineStyle (motorwayFillWidth)),
                new ValueItem ("trunk", GetTrunkFillLineStyle (trunkFillWidth)),
                new ValueItem ("primary", GetPrimaryFillLineStyle (primaryFillWidth)),
                new ValueItem ("secondary", GetRoadFillLineStyle (secondaryFillWidth)),
                new ValueItem ("tertiary", GetRoadFillLineStyle (tertiaryFillWidth))
            });

            ValueStyle label = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", motorwayFontSize),
                    GetGeneralPurposeTextStyle ("name:en", motorwayFontSize),
                    GetMotorwayRoadSheildLabelTextStyle ("ref", motorwayFontSize)
                }),
                new ValueItem ("trunk", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", trunkFontSize),
                    GetGeneralPurposeTextStyle ("name:en", trunkFontSize),
                    GetTrunkRoadSheildLabelTextStyle ("ref", trunkFontSize)
                }),
                new ValueItem ("primary", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", primaryFontSize),
                    GetGeneralPurposeTextStyle ("name:en", primaryFontSize),
                    GetPrimaryRoadSheildLabelTextStyle ("ref", primaryFontSize)
                })
            });

            FilterStyle filterStyle = new FilterStyle();
            filterStyle.Conditions.Add(new FilterCondition("ref", "^((?!;).)*$"));
            filterStyle.Styles.Add(label);

            styles.Styles.Add(outline);
            styles.Styles.Add(fill);
            styles.Styles.Add(filterStyle);

            return styles;
        }

        private CompositeStyle GetRoadLevel15Styles()
        {
            CompositeStyle styles = new CompositeStyle();

            float motorwayOutlineWidth = 8;
            float motorwayFillWidth = 6;
            float motorwayFontSize = 7;

            float trunkOutlineWidth = 8;
            float trunkFillWidth = 6;
            float trunkFontSize = 7;

            float primaryOutlineWidth = 6;
            float primaryFillWidth = 4;
            float primaryFontSize = 7;

            float secondaryOutlineWidth = 4;
            float secondaryFillWidth = 2;
            float secondaryFontSize = 6;

            float tertiaryOutlineWidth = 4;
            float tertiaryFillWidth = 2;
            float tertiaryFontSize = 6;

            float motorwayLinkOutlineWidth = 4;
            float motorwayLinkFillWidth = 2;

            float trunkLinkOutlineWidth = 4;
            float trunkLinkFillWidth = 2;

            float primaryLinkOutlineWidth = 4;
            float primaryLinkFillWidth = 2;

            float secondaryLinkOutlineWidth = 4;
            float secondaryLinkFillWidth = 2;

            float tertiaryLinkOutlineWidth = 4;
            float tertiaryLinkFillWidth = 2;

            float residentialOutlineWidth = 3;
            float residentialFillWidth = 1;

            float unclassifiedOutlineWidth = 3;
            float unclassifiedFillWidth = 1;

            float roadOutlineWidth = 3;
            float roadFillWidth = 1;

            float livingStreetOutlineWidth = 2;
            float livingStreetFillWidth = 1;

            float serviceOutlineWidth = 1;

            ValueStyle outline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayOutlineLineStyle (motorwayOutlineWidth)),
                new ValueItem ("trunk", GetTrunkOutlineLineStyle (trunkOutlineWidth)),
                new ValueItem ("primary", GetPrimaryOutlineLineStyle (primaryOutlineWidth)),
                new ValueItem ("secondary", GetRoadOutlineLineStyle (secondaryOutlineWidth)),
                new ValueItem ("tertiary", GetRoadOutlineLineStyle (tertiaryOutlineWidth)),
                new ValueItem ("motorway_link", GetMotorwayLinkOutlineLineStyle (motorwayLinkOutlineWidth)),
                new ValueItem ("trunk_link", GetTrunkLinkOutlineLineStyle (trunkLinkOutlineWidth)),
                new ValueItem ("primary_link", GetPrimaryLinkOutlineLineStyle (primaryLinkOutlineWidth)),
                new ValueItem ("secondary_link", GetRoadOutlineLineStyle (secondaryLinkOutlineWidth)),
                new ValueItem ("tertiary_link", GetRoadOutlineLineStyle (tertiaryLinkOutlineWidth)),
                new ValueItem ("residential", GetRoadOutlineLineStyle (residentialOutlineWidth)),
                new ValueItem ("unclassified", GetRoadOutlineLineStyle (unclassifiedOutlineWidth)),
                new ValueItem ("road", GetRoadOutlineLineStyle (roadOutlineWidth)),
                new ValueItem ("living_street", GetRoadOutlineLineStyle (livingStreetOutlineWidth)),
                new ValueItem ("service", GetRoadOutlineLineStyle (serviceOutlineWidth))
            });

            ValueStyle fill = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayFillLineStyle (motorwayFillWidth)),
                new ValueItem ("trunk", GetTrunkFillLineStyle (trunkFillWidth)),
                new ValueItem ("primary", GetPrimaryFillLineStyle (primaryFillWidth)),
                new ValueItem ("secondary", GetRoadFillLineStyle (secondaryFillWidth)),
                new ValueItem ("tertiary", GetRoadFillLineStyle (tertiaryFillWidth)),
                new ValueItem ("motorway_link", GetMotorwayLinkFillLineStyle (motorwayLinkFillWidth)),
                new ValueItem ("trunk_link", GetTrunkLinkFillLineStyle (trunkLinkFillWidth)),
                new ValueItem ("primary_link", GetPrimaryLinkFillLineStyle (primaryLinkFillWidth)),
                new ValueItem ("secondary_link", GetRoadFillLineStyle (secondaryLinkFillWidth)),
                new ValueItem ("tertiary_link", GetRoadFillLineStyle (tertiaryLinkFillWidth)),
                new ValueItem ("residential", GetRoadFillLineStyle (residentialFillWidth)),
                new ValueItem ("unclassified", GetRoadFillLineStyle (unclassifiedFillWidth)),
                new ValueItem ("road", GetRoadFillLineStyle (roadFillWidth)),
                new ValueItem ("living_street", GetRoadFillLineStyle (livingStreetFillWidth))
            });

            ValueStyle label = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", motorwayFontSize),
                    GetGeneralPurposeTextStyle ("name:en", motorwayFontSize),
                    GetMotorwayRoadSheildLabelTextStyle ("ref", motorwayFontSize)
                }),
                new ValueItem ("trunk", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", trunkFontSize),
                    GetGeneralPurposeTextStyle ("name:en", trunkFontSize),
                    GetTrunkRoadSheildLabelTextStyle ("ref", trunkFontSize)
                }),
                new ValueItem ("primary", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", primaryFontSize),
                    GetGeneralPurposeTextStyle ("name:en", primaryFontSize),
                    GetPrimaryRoadSheildLabelTextStyle ("ref", primaryFontSize)
                }),
                new ValueItem ("secondary", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", secondaryFontSize),
                    GetGeneralPurposeTextStyle ("name:en", secondaryFontSize)
                }),
                new ValueItem ("tertiary", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", tertiaryFontSize),
                    GetGeneralPurposeTextStyle ("name:en", tertiaryFontSize)
                })
            });

            FilterStyle roadSign = new FilterStyle();
            roadSign.Conditions.Add(new FilterCondition("ref", "^((?!;).)*$"));
            roadSign.Styles.Add(label);

            ValueStyle bridgeOutline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetBridgeLineStyle (motorwayOutlineWidth)),
                new ValueItem ("trunk", GetBridgeLineStyle (trunkOutlineWidth)),
                new ValueItem ("primary", GetBridgeLineStyle (primaryOutlineWidth)),
                new ValueItem ("secondary", GetBridgeLineStyle (secondaryOutlineWidth)),
                new ValueItem ("tertiary", GetBridgeLineStyle (tertiaryOutlineWidth)),
                new ValueItem ("motorway_link", GetBridgeLineStyle (motorwayLinkOutlineWidth)),
                new ValueItem ("trunk_link", GetBridgeLineStyle (trunkLinkOutlineWidth)),
                new ValueItem ("primary_link", GetBridgeLineStyle (primaryLinkOutlineWidth)),
                new ValueItem ("secondary_link", GetBridgeLineStyle (secondaryLinkOutlineWidth)),
                new ValueItem ("tertiary_link", GetBridgeLineStyle (tertiaryLinkOutlineWidth)),
                new ValueItem ("residential", GetBridgeLineStyle (residentialOutlineWidth)),
                new ValueItem ("unclassified", GetBridgeLineStyle (unclassifiedOutlineWidth)),
                new ValueItem ("road", GetBridgeLineStyle (roadOutlineWidth))
            });

            ValueStyle bridge = new ValueStyle("bridge", new Collection<ValueItem>() {
                new ValueItem ("yes", new Collection<Style> () { bridgeOutline, fill,
                    new ValueStyle ("layer", new Collection<ValueItem> () {
                        new ValueItem ("1", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("2", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("3", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("4", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("5", new Collection<Style> () { bridgeOutline, fill })
                    }){ DrawingOrder =  ValueDrawingOrder.OrderByValueItems }
                })
            })
            { DrawingOrder = ValueDrawingOrder.OrderByValueItems };

            ValueStyle tunnelOutline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetTunnelLineStyle (motorwayOutlineWidth)),
                new ValueItem ("trunk", GetTunnelLineStyle (trunkOutlineWidth)),
                new ValueItem ("primary", GetTunnelLineStyle (primaryOutlineWidth)),
                new ValueItem ("secondary", GetTunnelLineStyle (secondaryOutlineWidth)),
                new ValueItem ("tertiary", GetTunnelLineStyle (tertiaryOutlineWidth)),
                new ValueItem ("motorway_link", GetTunnelLineStyle (motorwayLinkOutlineWidth)),
                new ValueItem ("trunk_link", GetTunnelLineStyle (trunkLinkOutlineWidth)),
                new ValueItem ("primary_link", GetTunnelLineStyle (primaryLinkOutlineWidth)),
                new ValueItem ("secondary_link", GetTunnelLineStyle (secondaryLinkOutlineWidth)),
                new ValueItem ("tertiary_link", GetTunnelLineStyle (tertiaryLinkOutlineWidth)),
                new ValueItem ("residential", GetTunnelLineStyle (residentialOutlineWidth)),
                new ValueItem ("unclassified", GetTunnelLineStyle (unclassifiedOutlineWidth)),
                new ValueItem ("road", GetTunnelLineStyle (roadOutlineWidth))
            });

            ValueStyle tunnel = new ValueStyle("tunnel", new Collection<ValueItem>() {
                new ValueItem ("yes", new Collection<Style> () { tunnelOutline, fill,
                    new ValueStyle ("layer", new Collection<ValueItem> () {
                        new ValueItem ("-1", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-2", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-3", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-4", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-5", new Collection<Style> () { tunnelOutline, fill })
                    }){ DrawingOrder =  ValueDrawingOrder.OrderByValueItems }
                })
            })
            { DrawingOrder = ValueDrawingOrder.OrderByValueItems };

            styles.Styles.Add(tunnel);
            styles.Styles.Add(outline);
            styles.Styles.Add(fill);
            styles.Styles.Add(bridge);
            styles.Styles.Add(roadSign);

            return styles;
        }

        private CompositeStyle GetRoadLevel16Styles()
        {
            CompositeStyle styles = new CompositeStyle();

            float motorwayOutlineWidth = 8;
            float motorwayFillWidth = 6;
            float motorwayFontSize = 8;

            float trunkOutlineWidth = 8;
            float trunkFillWidth = 6;
            float trunkFontSize = 8;

            float primaryOutlineWidth = 6;
            float primaryFillWidth = 4;
            float primaryFontSize = 7;

            float secondaryOutlineWidth = 6;
            float secondaryFillWidth = 4;
            float secondaryFontSize = 7;

            float tertiaryOutlineWidth = 6;
            float tertiaryFillWidth = 4;
            float tertiaryFontSize = 7;

            float motorwayLinkOutlineWidth = 6;
            float motorwayLinkFillWidth = 4;

            float trunkLinkOutlineWidth = 6;
            float trunkLinkFillWidth = 4;

            float primaryLinkOutlineWidth = 6;
            float primaryLinkFillWidth = 4;

            float secondaryLinkOutlineWidth = 6;
            float secondaryLinkFillWidth = 4;

            float tertiaryLinkOutlineWidth = 6;
            float tertiaryLinkFillWidth = 4;

            float residentialOutlineWidth = 4;
            float residentialFillWidth = 2;
            float residentialFontSize = 6;

            float unclassifiedOutlineWidth = 4;
            float unclassifiedFillWidth = 2;
            float unclassifiedFontSize = 6;

            float roadOutlineWidth = 4;
            float roadFillWidth = 2;
            float roadFontSize = 6;

            float livingStreetOutlineWidth = 4;
            float livingStreetFillWidth = 2;
            float livingStreetFontSize = 6;

            float serviceOutlineWidth = 3;
            float serviceFillWidth = 1;

            float pedestrianOutlineWidth = 3;
            float pedestrianFillWidth = 1;

            float cyclewayOutlineWidth = 2;
            float cyclewayFillWidth = 1;

            float footwayOutlineWidth = 2;
            float footwayFillWidth = 1;

            float bridlewayOutlineWidth = 2;
            float bridlewayFillWidth = 1;

            float pathOutlineWidth = 2;
            float pathFillWidth = 1;

            float stepsOutlineWidth = 2;
            float stepsFillWidth = 1;

            float trackOutlineWidth = 2;
            float trackFillWidth = 1;

            ValueStyle outline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayOutlineLineStyle (motorwayOutlineWidth)),
                new ValueItem ("trunk", GetTrunkOutlineLineStyle (trunkOutlineWidth)),
                new ValueItem ("primary", GetPrimaryOutlineLineStyle (primaryOutlineWidth)),
                new ValueItem ("secondary", GetRoadOutlineLineStyle (secondaryOutlineWidth)),
                new ValueItem ("tertiary", GetRoadOutlineLineStyle (tertiaryOutlineWidth)),
                new ValueItem ("motorway_link", GetMotorwayLinkOutlineLineStyle (motorwayLinkOutlineWidth)),
                new ValueItem ("trunk_link", GetTrunkLinkOutlineLineStyle (trunkLinkOutlineWidth)),
                new ValueItem ("primary_link", GetPrimaryLinkOutlineLineStyle (primaryLinkOutlineWidth)),
                new ValueItem ("secondary_link", GetRoadOutlineLineStyle (secondaryLinkOutlineWidth)),
                new ValueItem ("tertiary_link", GetRoadOutlineLineStyle (tertiaryLinkOutlineWidth)),
                new ValueItem ("residential", GetRoadOutlineLineStyle (residentialOutlineWidth)),
                new ValueItem ("unclassified", GetRoadOutlineLineStyle (unclassifiedOutlineWidth)),
                new ValueItem ("road", GetRoadOutlineLineStyle (roadOutlineWidth)),
                new ValueItem ("living_street", GetRoadOutlineLineStyle (livingStreetOutlineWidth)),
                new ValueItem ("service", GetRoadOutlineLineStyle (serviceOutlineWidth)),
                new ValueItem ("pedestrian", GetMinorRoadOutlineLineStyle (pedestrianOutlineWidth)),
                new ValueItem ("cycleway", GetMinorRoadOutlineLineStyle (cyclewayOutlineWidth)),
                new ValueItem ("footway", GetMinorRoadOutlineLineStyle (footwayOutlineWidth)),
                new ValueItem ("bridleway", GetMinorRoadOutlineLineStyle (bridlewayOutlineWidth)),
                new ValueItem ("path", GetMinorRoadOutlineLineStyle (pathOutlineWidth)),
                new ValueItem ("steps", GetStepsOutlineLineStyle (stepsOutlineWidth)),
                new ValueItem ("track", GetMinorRoadOutlineLineStyle (trackOutlineWidth))
            });

            ValueStyle fill = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayFillLineStyle (motorwayFillWidth)),
                new ValueItem ("trunk", GetTrunkFillLineStyle (trunkFillWidth)),
                new ValueItem ("primary", GetPrimaryFillLineStyle (primaryFillWidth)),
                new ValueItem ("secondary", GetRoadFillLineStyle (secondaryFillWidth)),
                new ValueItem ("tertiary", GetRoadFillLineStyle (tertiaryFillWidth)),
                new ValueItem ("motorway_link", GetMotorwayLinkFillLineStyle (motorwayLinkFillWidth)),
                new ValueItem ("trunk_link", GetTrunkLinkFillLineStyle (trunkLinkFillWidth)),
                new ValueItem ("primary_link", GetPrimaryLinkFillLineStyle (primaryLinkFillWidth)),
                new ValueItem ("secondary_link", GetRoadFillLineStyle (secondaryLinkFillWidth)),
                new ValueItem ("tertiary_link", GetRoadFillLineStyle (tertiaryLinkFillWidth)),
                new ValueItem ("residential", GetRoadFillLineStyle (residentialFillWidth)),
                new ValueItem ("unclassified", GetRoadFillLineStyle (unclassifiedFillWidth)),
                new ValueItem ("road", GetRoadFillLineStyle (roadFillWidth)),
                new ValueItem ("living_street", GetRoadFillLineStyle (livingStreetFillWidth)),
                new ValueItem ("service", GetRoadFillLineStyle (serviceFillWidth)),
                new ValueItem ("pedestrian", GetMinorRoadFillLineStyle (pedestrianFillWidth)),
                new ValueItem ("cycleway", GetMinorRoadFillLineStyle (cyclewayFillWidth)),
                new ValueItem ("footway", GetMinorRoadFillLineStyle (footwayFillWidth)),
                new ValueItem ("bridleway", GetMinorRoadFillLineStyle (bridlewayFillWidth)),
                new ValueItem ("path", GetMinorRoadFillLineStyle (pathFillWidth)),
                new ValueItem ("steps", GetStepsFillLineStyle (stepsFillWidth)),
                new ValueItem ("track", GetMinorRoadFillLineStyle (trackFillWidth))
            });

            ValueStyle label = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", motorwayFontSize),
                    GetGeneralPurposeTextStyle ("name:en", motorwayFontSize),
                    GetMotorwayRoadSheildLabelTextStyle ("ref", motorwayFontSize)
                }),
                new ValueItem ("trunk", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", trunkFontSize),
                    GetGeneralPurposeTextStyle ("name:en", trunkFontSize),
                    GetTrunkRoadSheildLabelTextStyle ("ref", trunkFontSize)
                }),
                new ValueItem ("primary", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", primaryFontSize),
                    GetGeneralPurposeTextStyle ("name:en", primaryFontSize),
                    GetPrimaryRoadSheildLabelTextStyle ("ref", primaryFontSize)
                }),
                new ValueItem ("secondary", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", secondaryFontSize),
                    GetGeneralPurposeTextStyle ("name:en", secondaryFontSize)
                }),
                new ValueItem ("tertiary", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", tertiaryFontSize),
                    GetGeneralPurposeTextStyle ("name:en", tertiaryFontSize)
                }),
                new ValueItem ("residential", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", residentialFontSize),
                    GetGeneralPurposeTextStyle ("name:en", residentialFontSize)
                }),
                new ValueItem ("unclassified", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", unclassifiedFontSize),
                    GetGeneralPurposeTextStyle ("name:en", unclassifiedFontSize)
                }),
                new ValueItem ("road", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", roadFontSize),
                    GetGeneralPurposeTextStyle ("name:en", roadFontSize)
                }),
                new ValueItem ("living_street", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", livingStreetFontSize),
                    GetGeneralPurposeTextStyle ("name:en", livingStreetFontSize)
                }),
            });

            FilterStyle roadSign = new FilterStyle();
            roadSign.Conditions.Add(new FilterCondition("ref", "^((?!;).)*$"));
            roadSign.Styles.Add(label);

            ValueStyle bridgeOutline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetBridgeLineStyle (motorwayOutlineWidth)),
                new ValueItem ("trunk", GetBridgeLineStyle (trunkOutlineWidth)),
                new ValueItem ("primary", GetBridgeLineStyle (primaryOutlineWidth)),
                new ValueItem ("secondary", GetBridgeLineStyle (secondaryOutlineWidth)),
                new ValueItem ("tertiary", GetBridgeLineStyle (tertiaryOutlineWidth)),
                new ValueItem ("motorway_link", GetBridgeLineStyle (motorwayLinkOutlineWidth)),
                new ValueItem ("trunk_link", GetBridgeLineStyle (trunkLinkOutlineWidth)),
                new ValueItem ("primary_link", GetBridgeLineStyle (primaryLinkOutlineWidth)),
                new ValueItem ("secondary_link", GetBridgeLineStyle (secondaryLinkOutlineWidth)),
                new ValueItem ("tertiary_link", GetBridgeLineStyle (tertiaryLinkOutlineWidth)),
                new ValueItem ("residential", GetBridgeLineStyle (residentialOutlineWidth)),
                new ValueItem ("unclassified", GetBridgeLineStyle (unclassifiedOutlineWidth)),
                new ValueItem ("road", GetBridgeLineStyle (roadOutlineWidth))
            });

            ValueStyle bridge = new ValueStyle("bridge", new Collection<ValueItem>() {
                new ValueItem ("yes", new Collection<Style> () { bridgeOutline, fill,
                    new ValueStyle ("layer", new Collection<ValueItem> () {
                        new ValueItem ("1", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("2", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("3", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("4", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("5", new Collection<Style> () { bridgeOutline, fill })
                    }){ DrawingOrder =  ValueDrawingOrder.OrderByValueItems }
                })
            })
            { DrawingOrder = ValueDrawingOrder.OrderByValueItems };

            ValueStyle tunnelOutline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetTunnelLineStyle (motorwayOutlineWidth)),
                new ValueItem ("trunk", GetTunnelLineStyle (trunkOutlineWidth)),
                new ValueItem ("primary", GetTunnelLineStyle (primaryOutlineWidth)),
                new ValueItem ("secondary", GetTunnelLineStyle (secondaryOutlineWidth)),
                new ValueItem ("tertiary", GetTunnelLineStyle (tertiaryOutlineWidth)),
                new ValueItem ("motorway_link", GetTunnelLineStyle (motorwayLinkOutlineWidth)),
                new ValueItem ("trunk_link", GetTunnelLineStyle (trunkLinkOutlineWidth)),
                new ValueItem ("primary_link", GetTunnelLineStyle (primaryLinkOutlineWidth)),
                new ValueItem ("secondary_link", GetTunnelLineStyle (secondaryLinkOutlineWidth)),
                new ValueItem ("tertiary_link", GetTunnelLineStyle (tertiaryLinkOutlineWidth)),
                new ValueItem ("residential", GetTunnelLineStyle (residentialOutlineWidth)),
                new ValueItem ("unclassified", GetTunnelLineStyle (unclassifiedOutlineWidth)),
                new ValueItem ("road", GetTunnelLineStyle (roadOutlineWidth))
            });

            ValueStyle tunnel = new ValueStyle("tunnel", new Collection<ValueItem>() {
                new ValueItem ("yes", new Collection<Style> () { tunnelOutline, fill,
                    new ValueStyle ("layer", new Collection<ValueItem> () {
                        new ValueItem ("-1", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-2", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-3", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-4", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-5", new Collection<Style> () { tunnelOutline, fill })
                    }){ DrawingOrder =  ValueDrawingOrder.OrderByValueItems }
                })
            })
            { DrawingOrder = ValueDrawingOrder.OrderByValueItems };

            styles.Styles.Add(tunnel);
            styles.Styles.Add(outline);
            styles.Styles.Add(fill);
            styles.Styles.Add(bridge);
            styles.Styles.Add(roadSign);

            return styles;
        }

        private CompositeStyle GetRoadLevel17Styles()
        {
            CompositeStyle styles = new CompositeStyle();

            float motorwayOutlineWidth = 10;
            float motorwayFillWidth = 8;
            float motorwayFontSize = 8;

            float trunkOutlineWidth = 10;
            float trunkFillWidth = 8;
            float trunkFontSize = 8;

            float primaryOutlineWidth = 10;
            float primaryFillWidth = 8;
            float primaryFontSize = 8;

            float secondaryOutlineWidth = 8;
            float secondaryFillWidth = 6;
            float secondaryFontSize = 7;

            float tertiaryOutlineWidth = 8;
            float tertiaryFillWidth = 6;
            float tertiaryFontSize = 7;

            float motorwayLinkOutlineWidth = 8;
            float motorwayLinkFillWidth = 6;

            float trunkLinkOutlineWidth = 8;
            float trunkLinkFillWidth = 6;

            float primaryLinkOutlineWidth = 8;
            float primaryLinkFillWidth = 6;

            float secondaryLinkOutlineWidth = 8;
            float secondaryLinkFillWidth = 6;

            float tertiaryLinkOutlineWidth = 8;
            float tertiaryLinkFillWidth = 6;

            float residentialOutlineWidth = 6;
            float residentialFillWidth = 4;
            float residentialFontSize = 6;

            float unclassifiedOutlineWidth = 6;
            float unclassifiedFillWidth = 4;
            float unclassifiedFontSize = 6;

            float roadOutlineWidth = 6;
            float roadFillWidth = 4;
            float roadFontSize = 6;

            float livingStreetOutlineWidth = 4;
            float livingStreetFillWidth = 2;
            float livingStreetFontSize = 6;

            float serviceOutlineWidth = 4;
            float serviceFillWidth = 2;

            float pedestrianOutlineWidth = 3;
            float pedestrianFillWidth = 2;

            float cyclewayOutlineWidth = 2;
            float cyclewayFillWidth = 1;

            float footwayOutlineWidth = 2;
            float footwayFillWidth = 1;

            float bridlewayOutlineWidth = 2;
            float bridlewayFillWidth = 1;

            float pathOutlineWidth = 2;
            float pathFillWidth = 1;

            float stepsOutlineWidth = 2;
            float stepsFillWidth = 1;

            float trackOutlineWidth = 2;
            float trackFillWidth = 1;

            ValueStyle outline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayOutlineLineStyle (motorwayOutlineWidth)),
                new ValueItem ("trunk", GetTrunkOutlineLineStyle (trunkOutlineWidth)),
                new ValueItem ("primary", GetPrimaryOutlineLineStyle (primaryOutlineWidth)),
                new ValueItem ("secondary", GetRoadOutlineLineStyle (secondaryOutlineWidth)),
                new ValueItem ("tertiary", GetRoadOutlineLineStyle (tertiaryOutlineWidth)),
                new ValueItem ("motorway_link", GetMotorwayLinkOutlineLineStyle (motorwayLinkOutlineWidth)),
                new ValueItem ("trunk_link", GetTrunkLinkOutlineLineStyle (trunkLinkOutlineWidth)),
                new ValueItem ("primary_link", GetPrimaryLinkOutlineLineStyle (primaryLinkOutlineWidth)),
                new ValueItem ("secondary_link", GetRoadOutlineLineStyle (secondaryLinkOutlineWidth)),
                new ValueItem ("tertiary_link", GetRoadOutlineLineStyle (tertiaryLinkOutlineWidth)),
                new ValueItem ("residential", GetRoadOutlineLineStyle (residentialOutlineWidth)),
                new ValueItem ("unclassified", GetRoadOutlineLineStyle (unclassifiedOutlineWidth)),
                new ValueItem ("road", GetRoadOutlineLineStyle (roadOutlineWidth)),
                new ValueItem ("living_street", GetRoadOutlineLineStyle (livingStreetOutlineWidth)),
                new ValueItem ("service", GetRoadOutlineLineStyle (serviceOutlineWidth)),
                new ValueItem ("pedestrian", GetMinorRoadOutlineLineStyle (pedestrianOutlineWidth)),
                new ValueItem ("cycleway", GetMinorRoadOutlineLineStyle (cyclewayOutlineWidth)),
                new ValueItem ("footway", GetMinorRoadOutlineLineStyle (footwayOutlineWidth)),
                new ValueItem ("bridleway", GetMinorRoadOutlineLineStyle (bridlewayOutlineWidth)),
                new ValueItem ("path", GetMinorRoadOutlineLineStyle (pathOutlineWidth)),
                new ValueItem ("steps", GetStepsOutlineLineStyle (stepsOutlineWidth)),
                new ValueItem ("track", GetMinorRoadOutlineLineStyle (trackOutlineWidth))
            });

            ValueStyle fillValueStyle = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayFillLineStyle (motorwayFillWidth)),
                new ValueItem ("trunk", GetTrunkFillLineStyle (trunkFillWidth)),
                new ValueItem ("primary", GetPrimaryFillLineStyle (primaryFillWidth)),
                new ValueItem ("secondary", GetRoadFillLineStyle (secondaryFillWidth)),
                new ValueItem ("tertiary", GetRoadFillLineStyle (tertiaryFillWidth)),
                new ValueItem ("motorway_link", GetMotorwayLinkFillLineStyle (motorwayLinkFillWidth)),
                new ValueItem ("trunk_link", GetTrunkLinkFillLineStyle (trunkLinkFillWidth)),
                new ValueItem ("primary_link", GetPrimaryLinkFillLineStyle (primaryLinkFillWidth)),
                new ValueItem ("secondary_link", GetRoadFillLineStyle (secondaryLinkFillWidth)),
                new ValueItem ("tertiary_link", GetRoadFillLineStyle (tertiaryLinkFillWidth)),
                new ValueItem ("residential", GetRoadFillLineStyle (residentialFillWidth)),
                new ValueItem ("unclassified", GetRoadFillLineStyle (unclassifiedFillWidth)),
                new ValueItem ("road", GetRoadFillLineStyle (roadFillWidth)),
                new ValueItem ("living_street", GetRoadFillLineStyle (livingStreetFillWidth)),
                new ValueItem ("service", GetRoadFillLineStyle (serviceFillWidth)),
                new ValueItem ("pedestrian", GetMinorRoadFillLineStyle (pedestrianFillWidth)),
                new ValueItem ("cycleway", GetMinorRoadFillLineStyle (cyclewayFillWidth)),
                new ValueItem ("footway", GetMinorRoadFillLineStyle (footwayFillWidth)),
                new ValueItem ("bridleway", GetMinorRoadFillLineStyle (bridlewayFillWidth)),
                new ValueItem ("path", GetMinorRoadFillLineStyle (pathFillWidth)),
                new ValueItem ("steps", GetStepsFillLineStyle (stepsFillWidth)),
                new ValueItem ("track", GetMinorRoadFillLineStyle (trackFillWidth))
            });

            CompositeStyle fill = new CompositeStyle();
            fill.Styles.Add(fillValueStyle);
            fill.Styles.Add(GetOneWayLineStyle("oneway", "yes", iconPath));

            ValueStyle label = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", motorwayFontSize),
                    GetGeneralPurposeTextStyle ("name:en", motorwayFontSize),
                    GetMotorwayRoadSheildLabelTextStyle ("ref", motorwayFontSize)
                }),
                new ValueItem ("trunk", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", trunkFontSize),
                    GetGeneralPurposeTextStyle ("name:en", trunkFontSize),
                    GetTrunkRoadSheildLabelTextStyle ("ref", trunkFontSize)
                }),
                new ValueItem ("primary", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", primaryFontSize),
                    GetGeneralPurposeTextStyle ("name:en", primaryFontSize),
                    GetPrimaryRoadSheildLabelTextStyle ("ref", primaryFontSize)
                }),
                new ValueItem ("secondary", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", secondaryFontSize),
                    GetGeneralPurposeTextStyle ("name:en", secondaryFontSize)
                }),
                new ValueItem ("tertiary", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", tertiaryFontSize),
                    GetGeneralPurposeTextStyle ("name:en", tertiaryFontSize)
                }),
                new ValueItem ("residential", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", residentialFontSize),
                    GetGeneralPurposeTextStyle ("name:en", residentialFontSize)
                }),
                new ValueItem ("unclassified", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", unclassifiedFontSize),
                    GetGeneralPurposeTextStyle ("name:en", unclassifiedFontSize)
                }),
                new ValueItem ("road", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", roadFontSize),
                    GetGeneralPurposeTextStyle ("name:en", roadFontSize)
                }),
                new ValueItem ("living_street", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", livingStreetFontSize),
                    GetGeneralPurposeTextStyle ("name:en", livingStreetFontSize)
                }),
            });

            FilterStyle roadSign = new FilterStyle();
            roadSign.Conditions.Add(new FilterCondition("ref", "^((?!;).)*$"));
            roadSign.Styles.Add(label);

            ValueStyle bridgeOutline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetBridgeLineStyle (motorwayOutlineWidth)),
                new ValueItem ("trunk", GetBridgeLineStyle (trunkOutlineWidth)),
                new ValueItem ("primary", GetBridgeLineStyle (primaryOutlineWidth)),
                new ValueItem ("secondary", GetBridgeLineStyle (secondaryOutlineWidth)),
                new ValueItem ("tertiary", GetBridgeLineStyle (tertiaryOutlineWidth)),
                new ValueItem ("motorway_link", GetBridgeLineStyle (motorwayLinkOutlineWidth)),
                new ValueItem ("trunk_link", GetBridgeLineStyle (trunkLinkOutlineWidth)),
                new ValueItem ("primary_link", GetBridgeLineStyle (primaryLinkOutlineWidth)),
                new ValueItem ("secondary_link", GetBridgeLineStyle (secondaryLinkOutlineWidth)),
                new ValueItem ("tertiary_link", GetBridgeLineStyle (tertiaryLinkOutlineWidth)),
                new ValueItem ("residential", GetBridgeLineStyle (residentialOutlineWidth)),
                new ValueItem ("unclassified", GetBridgeLineStyle (unclassifiedOutlineWidth)),
                new ValueItem ("road", GetBridgeLineStyle (roadOutlineWidth))
            });

            ValueStyle bridge = new ValueStyle("bridge", new Collection<ValueItem>() {
                new ValueItem ("yes", new Collection<Style> () { bridgeOutline, fill,
                    new ValueStyle ("layer", new Collection<ValueItem> () {
                        new ValueItem ("1", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("2", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("3", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("4", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("5", new Collection<Style> () { bridgeOutline, fill })
                    }){ DrawingOrder =  ValueDrawingOrder.OrderByValueItems }
                })
            })
            { DrawingOrder = ValueDrawingOrder.OrderByValueItems };

            ValueStyle tunnelOutline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetTunnelLineStyle (motorwayOutlineWidth)),
                new ValueItem ("trunk", GetTunnelLineStyle (trunkOutlineWidth)),
                new ValueItem ("primary", GetTunnelLineStyle (primaryOutlineWidth)),
                new ValueItem ("secondary", GetTunnelLineStyle (secondaryOutlineWidth)),
                new ValueItem ("tertiary", GetTunnelLineStyle (tertiaryOutlineWidth)),
                new ValueItem ("motorway_link", GetTunnelLineStyle (motorwayLinkOutlineWidth)),
                new ValueItem ("trunk_link", GetTunnelLineStyle (trunkLinkOutlineWidth)),
                new ValueItem ("primary_link", GetTunnelLineStyle (primaryLinkOutlineWidth)),
                new ValueItem ("secondary_link", GetTunnelLineStyle (secondaryLinkOutlineWidth)),
                new ValueItem ("tertiary_link", GetTunnelLineStyle (tertiaryLinkOutlineWidth)),
                new ValueItem ("residential", GetTunnelLineStyle (residentialOutlineWidth)),
                new ValueItem ("unclassified", GetTunnelLineStyle (unclassifiedOutlineWidth)),
                new ValueItem ("road", GetTunnelLineStyle (roadOutlineWidth))
            });

            ValueStyle tunnel = new ValueStyle("tunnel", new Collection<ValueItem>() {
                new ValueItem ("yes", new Collection<Style> () { tunnelOutline, fill,
                    new ValueStyle ("layer", new Collection<ValueItem> () {
                        new ValueItem ("-1", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-2", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-3", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-4", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-5", new Collection<Style> () { tunnelOutline, fill })
                    }){ DrawingOrder =  ValueDrawingOrder.OrderByValueItems }
                })
            })
            { DrawingOrder = ValueDrawingOrder.OrderByValueItems };

            styles.Styles.Add(tunnel);
            styles.Styles.Add(outline);
            styles.Styles.Add(fill);
            styles.Styles.Add(bridge);
            styles.Styles.Add(roadSign);

            return styles;
        }

        private CompositeStyle GetRoadLevel18Styles()
        {
            CompositeStyle styles = new CompositeStyle();

            float motorwayOutlineWidth = 12;
            float motorwayFillWidth = 10;
            float motorwayFontSize = 9;

            float trunkOutlineWidth = 12;
            float trunkFillWidth = 10;
            float trunkFontSize = 9;

            float primaryOutlineWidth = 12;
            float primaryFillWidth = 10;
            float primaryFontSize = 9;

            float secondaryOutlineWidth = 10;
            float secondaryFillWidth = 8;
            float secondaryFontSize = 8;

            float tertiaryOutlineWidth = 8;
            float tertiaryFillWidth = 6;
            float tertiaryFontSize = 7;

            float motorwayLinkOutlineWidth = 10;
            float motorwayLinkFillWidth = 8;

            float trunkLinkOutlineWidth = 10;
            float trunkLinkFillWidth = 8;

            float primaryLinkOutlineWidth = 10;
            float primaryLinkFillWidth = 8;

            float secondaryLinkOutlineWidth = 10;
            float secondaryLinkFillWidth = 8;

            float tertiaryLinkOutlineWidth = 8;
            float tertiaryLinkFillWidth = 6;

            float residentialOutlineWidth = 8;
            float residentialFillWidth = 6;
            float residentialFontSize = 7;

            float unclassifiedOutlineWidth = 8;
            float unclassifiedFillWidth = 6;
            float unclassifiedFontSize = 7;

            float roadOutlineWidth = 8;
            float roadFillWidth = 6;
            float roadFontSize = 7;

            float livingStreetOutlineWidth = 6;
            float livingStreetFillWidth = 4;
            float livingStreetFontSize = 6;

            float serviceOutlineWidth = 6;
            float serviceFillWidth = 4;

            float pedestrianOutlineWidth = 4;
            float pedestrianFillWidth = 2;

            float cyclewayOutlineWidth = 4;
            float cyclewayFillWidth = 2;

            float footwayOutlineWidth = 4;
            float footwayFillWidth = 2;

            float bridlewayOutlineWidth = 4;
            float bridlewayFillWidth = 2;

            float pathOutlineWidth = 4;
            float pathFillWidth = 2;

            float stepsOutlineWidth = 4;
            float stepsFillWidth = 2;

            float trackOutlineWidth = 4;
            float trackFillWidth = 2;

            ValueStyle outline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayOutlineLineStyle (motorwayOutlineWidth)),
                new ValueItem ("trunk", GetTrunkOutlineLineStyle (trunkOutlineWidth)),
                new ValueItem ("primary", GetPrimaryOutlineLineStyle (primaryOutlineWidth)),
                new ValueItem ("secondary", GetRoadOutlineLineStyle (secondaryOutlineWidth)),
                new ValueItem ("tertiary", GetRoadOutlineLineStyle (tertiaryOutlineWidth)),
                new ValueItem ("motorway_link", GetMotorwayLinkOutlineLineStyle (motorwayLinkOutlineWidth)),
                new ValueItem ("trunk_link", GetTrunkLinkOutlineLineStyle (trunkLinkOutlineWidth)),
                new ValueItem ("primary_link", GetPrimaryLinkOutlineLineStyle (primaryLinkOutlineWidth)),
                new ValueItem ("secondary_link", GetRoadOutlineLineStyle (secondaryLinkOutlineWidth)),
                new ValueItem ("tertiary_link", GetRoadOutlineLineStyle (tertiaryLinkOutlineWidth)),
                new ValueItem ("residential", GetRoadOutlineLineStyle (residentialOutlineWidth)),
                new ValueItem ("unclassified", GetRoadOutlineLineStyle (unclassifiedOutlineWidth)),
                new ValueItem ("road", GetRoadOutlineLineStyle (roadOutlineWidth)),
                new ValueItem ("living_street", GetRoadOutlineLineStyle (livingStreetOutlineWidth)),
                new ValueItem ("service", GetRoadOutlineLineStyle (serviceOutlineWidth)),
                new ValueItem ("pedestrian", GetMinorRoadOutlineLineStyle (pedestrianOutlineWidth)),
                new ValueItem ("cycleway", GetMinorRoadOutlineLineStyle (cyclewayOutlineWidth)),
                new ValueItem ("footway", GetMinorRoadOutlineLineStyle (footwayOutlineWidth)),
                new ValueItem ("bridleway", GetMinorRoadOutlineLineStyle (bridlewayOutlineWidth)),
                new ValueItem ("path", GetMinorRoadOutlineLineStyle (pathOutlineWidth)),
                new ValueItem ("steps", GetStepsOutlineLineStyle (stepsOutlineWidth)),
                new ValueItem ("track", GetMinorRoadOutlineLineStyle (trackOutlineWidth))
            });

            ValueStyle fillValueStyle = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayFillLineStyle (motorwayFillWidth)),
                new ValueItem ("trunk", GetTrunkFillLineStyle (trunkFillWidth)),
                new ValueItem ("primary", GetPrimaryFillLineStyle (primaryFillWidth)),
                new ValueItem ("secondary", GetRoadFillLineStyle (secondaryFillWidth)),
                new ValueItem ("tertiary", GetRoadFillLineStyle (tertiaryFillWidth)),
                new ValueItem ("motorway_link", GetMotorwayLinkFillLineStyle (motorwayLinkFillWidth)),
                new ValueItem ("trunk_link", GetTrunkLinkFillLineStyle (trunkLinkFillWidth)),
                new ValueItem ("primary_link", GetPrimaryLinkFillLineStyle (primaryLinkFillWidth)),
                new ValueItem ("secondary_link", GetRoadFillLineStyle (secondaryLinkFillWidth)),
                new ValueItem ("tertiary_link", GetRoadFillLineStyle (tertiaryLinkFillWidth)),
                new ValueItem ("residential", GetRoadFillLineStyle (residentialFillWidth)),
                new ValueItem ("unclassified", GetRoadFillLineStyle (unclassifiedFillWidth)),
                new ValueItem ("road", GetRoadFillLineStyle (roadFillWidth)),
                new ValueItem ("living_street", GetRoadFillLineStyle (livingStreetFillWidth)),
                new ValueItem ("service", GetRoadFillLineStyle (serviceFillWidth)),
                new ValueItem ("pedestrian", GetMinorRoadFillLineStyle (pedestrianFillWidth)),
                new ValueItem ("cycleway", GetMinorRoadFillLineStyle (cyclewayFillWidth)),
                new ValueItem ("footway", GetMinorRoadFillLineStyle (footwayFillWidth)),
                new ValueItem ("bridleway", GetMinorRoadFillLineStyle (bridlewayFillWidth)),
                new ValueItem ("path", GetMinorRoadFillLineStyle (pathFillWidth)),
                new ValueItem ("steps", GetStepsFillLineStyle (stepsFillWidth)),
                new ValueItem ("track", GetMinorRoadFillLineStyle (trackFillWidth))
            });

            CompositeStyle fill = new CompositeStyle();
            fill.Styles.Add(fillValueStyle);
            fill.Styles.Add(GetOneWayLineStyle("oneway", "yes", iconPath));
            ValueStyle label = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", motorwayFontSize),
                    GetGeneralPurposeTextStyle ("name:en", motorwayFontSize),
                    GetMotorwayRoadSheildLabelTextStyle ("ref", motorwayFontSize)
                }),
                new ValueItem ("trunk", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", trunkFontSize),
                    GetGeneralPurposeTextStyle ("name:en", trunkFontSize),
                    GetTrunkRoadSheildLabelTextStyle ("ref", trunkFontSize)
                }),
                new ValueItem ("primary", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", primaryFontSize),
                    GetGeneralPurposeTextStyle ("name:en", primaryFontSize),
                    GetPrimaryRoadSheildLabelTextStyle ("ref", primaryFontSize)
                }),
                new ValueItem ("secondary", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", secondaryFontSize),
                    GetGeneralPurposeTextStyle ("name:en", secondaryFontSize)
                }),
                new ValueItem ("tertiary", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", tertiaryFontSize),
                    GetGeneralPurposeTextStyle ("name:en", tertiaryFontSize)
                }),
                new ValueItem ("residential", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", residentialFontSize),
                    GetGeneralPurposeTextStyle ("name:en", residentialFontSize)
                }),
                new ValueItem ("unclassified", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", unclassifiedFontSize),
                    GetGeneralPurposeTextStyle ("name:en", unclassifiedFontSize)
                }),
                new ValueItem ("road", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", roadFontSize),
                    GetGeneralPurposeTextStyle ("name:en", roadFontSize)
                }),
                new ValueItem ("living_street", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", livingStreetFontSize),
                    GetGeneralPurposeTextStyle ("name:en", livingStreetFontSize)
                }),
            });

            FilterStyle roadSign = new FilterStyle();
            roadSign.Conditions.Add(new FilterCondition("ref", "^((?!;).)*$"));
            roadSign.Styles.Add(label);

            ValueStyle bridgeOutline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetBridgeLineStyle (motorwayOutlineWidth)),
                new ValueItem ("trunk", GetBridgeLineStyle (trunkOutlineWidth)),
                new ValueItem ("primary", GetBridgeLineStyle (primaryOutlineWidth)),
                new ValueItem ("secondary", GetBridgeLineStyle (secondaryOutlineWidth)),
                new ValueItem ("tertiary", GetBridgeLineStyle (tertiaryOutlineWidth)),
                new ValueItem ("motorway_link", GetBridgeLineStyle (motorwayLinkOutlineWidth)),
                new ValueItem ("trunk_link", GetBridgeLineStyle (trunkLinkOutlineWidth)),
                new ValueItem ("primary_link", GetBridgeLineStyle (primaryLinkOutlineWidth)),
                new ValueItem ("secondary_link", GetBridgeLineStyle (secondaryLinkOutlineWidth)),
                new ValueItem ("tertiary_link", GetBridgeLineStyle (tertiaryLinkOutlineWidth)),
                new ValueItem ("residential", GetBridgeLineStyle (residentialOutlineWidth)),
                new ValueItem ("unclassified", GetBridgeLineStyle (unclassifiedOutlineWidth)),
                new ValueItem ("road", GetBridgeLineStyle (roadOutlineWidth))
            });

            ValueStyle bridge = new ValueStyle("bridge", new Collection<ValueItem>() {
                new ValueItem ("yes", new Collection<Style> () { bridgeOutline, fill,
                    new ValueStyle ("layer", new Collection<ValueItem> () {
                        new ValueItem ("1", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("2", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("3", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("4", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("5", new Collection<Style> () { bridgeOutline, fill })
                    }){ DrawingOrder =  ValueDrawingOrder.OrderByValueItems }
                })
            })
            { DrawingOrder = ValueDrawingOrder.OrderByValueItems };

            ValueStyle tunnelOutline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetTunnelLineStyle (motorwayOutlineWidth)),
                new ValueItem ("trunk", GetTunnelLineStyle (trunkOutlineWidth)),
                new ValueItem ("primary", GetTunnelLineStyle (primaryOutlineWidth)),
                new ValueItem ("secondary", GetTunnelLineStyle (secondaryOutlineWidth)),
                new ValueItem ("tertiary", GetTunnelLineStyle (tertiaryOutlineWidth)),
                new ValueItem ("motorway_link", GetTunnelLineStyle (motorwayLinkOutlineWidth)),
                new ValueItem ("trunk_link", GetTunnelLineStyle (trunkLinkOutlineWidth)),
                new ValueItem ("primary_link", GetTunnelLineStyle (primaryLinkOutlineWidth)),
                new ValueItem ("secondary_link", GetTunnelLineStyle (secondaryLinkOutlineWidth)),
                new ValueItem ("tertiary_link", GetTunnelLineStyle (tertiaryLinkOutlineWidth)),
                new ValueItem ("residential", GetTunnelLineStyle (residentialOutlineWidth)),
                new ValueItem ("unclassified", GetTunnelLineStyle (unclassifiedOutlineWidth)),
                new ValueItem ("road", GetTunnelLineStyle (roadOutlineWidth))
            });

            ValueStyle tunnel = new ValueStyle("tunnel", new Collection<ValueItem>() {
                new ValueItem ("yes", new Collection<Style> () { tunnelOutline, fill,
                    new ValueStyle ("layer", new Collection<ValueItem> () {
                        new ValueItem ("-1", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-2", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-3", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-4", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-5", new Collection<Style> () { tunnelOutline, fill })
                    }){ DrawingOrder =  ValueDrawingOrder.OrderByValueItems }
                })
            })
            { DrawingOrder = ValueDrawingOrder.OrderByValueItems };

            styles.Styles.Add(tunnel);
            styles.Styles.Add(outline);
            styles.Styles.Add(fill);
            styles.Styles.Add(bridge);
            styles.Styles.Add(roadSign);

            return styles;
        }

        private CompositeStyle GetRoadLevel19Styles()
        {
            CompositeStyle styles = new CompositeStyle();

            float motorwayOutlineWidth = 14;
            float motorwayFillWidth = 12;
            float motorwayFontSize = 10;

            float trunkOutlineWidth = 14;
            float trunkFillWidth = 12;
            float trunkFontSize = 10;

            float primaryOutlineWidth = 14;
            float primaryFillWidth = 12;
            float primaryFontSize = 10;

            float secondaryOutlineWidth = 12;
            float secondaryFillWidth = 10;
            float secondaryFontSize = 9;

            float tertiaryOutlineWidth = 10;
            float tertiaryFillWidth = 8;
            float tertiaryFontSize = 8;

            float motorwayLinkOutlineWidth = 12;
            float motorwayLinkFillWidth = 10;

            float trunkLinkOutlineWidth = 12;
            float trunkLinkFillWidth = 10;

            float primaryLinkOutlineWidth = 12;
            float primaryLinkFillWidth = 10;

            float secondaryLinkOutlineWidth = 12;
            float secondaryLinkFillWidth = 10;

            float tertiaryLinkOutlineWidth = 10;
            float tertiaryLinkFillWidth = 8;

            float residentialOutlineWidth = 10;
            float residentialFillWidth = 8;
            float residentialFontSize = 8;

            float unclassifiedOutlineWidth = 10;
            float unclassifiedFillWidth = 8;
            float unclassifiedFontSize = 8;

            float roadOutlineWidth = 10;
            float roadFillWidth = 8;
            float roadFontSize = 8;

            float livingStreetOutlineWidth = 8;
            float livingStreetFillWidth = 6;
            float livingStreetFontSize = 7;

            float serviceOutlineWidth = 8;
            float serviceFillWidth = 6;

            float pedestrianOutlineWidth = 4;
            float pedestrianFillWidth = 2;

            float cyclewayOutlineWidth = 4;
            float cyclewayFillWidth = 2;

            float footwayOutlineWidth = 4;
            float footwayFillWidth = 2;

            float bridlewayOutlineWidth = 4;
            float bridlewayFillWidth = 2;

            float pathOutlineWidth = 4;
            float pathFillWidth = 2;

            float stepsOutlineWidth = 4;
            float stepsFillWidth = 2;

            float trackOutlineWidth = 4;
            float trackFillWidth = 2;

            ValueStyle outline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayOutlineLineStyle (motorwayOutlineWidth)),
                new ValueItem ("trunk", GetTrunkOutlineLineStyle (trunkOutlineWidth)),
                new ValueItem ("primary", GetPrimaryOutlineLineStyle (primaryOutlineWidth)),
                new ValueItem ("secondary", GetRoadOutlineLineStyle (secondaryOutlineWidth)),
                new ValueItem ("tertiary", GetRoadOutlineLineStyle (tertiaryOutlineWidth)),
                new ValueItem ("motorway_link", GetMotorwayLinkOutlineLineStyle (motorwayLinkOutlineWidth)),
                new ValueItem ("trunk_link", GetTrunkLinkOutlineLineStyle (trunkLinkOutlineWidth)),
                new ValueItem ("primary_link", GetPrimaryLinkOutlineLineStyle (primaryLinkOutlineWidth)),
                new ValueItem ("secondary_link", GetRoadOutlineLineStyle (secondaryLinkOutlineWidth)),
                new ValueItem ("tertiary_link", GetRoadOutlineLineStyle (tertiaryLinkOutlineWidth)),
                new ValueItem ("residential", GetRoadOutlineLineStyle (residentialOutlineWidth)),
                new ValueItem ("unclassified", GetRoadOutlineLineStyle (unclassifiedOutlineWidth)),
                new ValueItem ("road", GetRoadOutlineLineStyle (roadOutlineWidth)),
                new ValueItem ("living_street", GetRoadOutlineLineStyle (livingStreetOutlineWidth)),
                new ValueItem ("service", GetRoadOutlineLineStyle (serviceOutlineWidth)),
                new ValueItem ("pedestrian", GetMinorRoadOutlineLineStyle (pedestrianOutlineWidth)),
                new ValueItem ("cycleway", GetMinorRoadOutlineLineStyle (cyclewayOutlineWidth)),
                new ValueItem ("footway", GetMinorRoadOutlineLineStyle (footwayOutlineWidth)),
                new ValueItem ("bridleway", GetMinorRoadOutlineLineStyle (bridlewayOutlineWidth)),
                new ValueItem ("path", GetMinorRoadOutlineLineStyle (pathOutlineWidth)),
                new ValueItem ("steps", GetStepsOutlineLineStyle (stepsOutlineWidth)),
                new ValueItem ("track", GetMinorRoadOutlineLineStyle (trackOutlineWidth))
            });

            ValueStyle fillValueStyle = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetMotorwayFillLineStyle (motorwayFillWidth)),
                new ValueItem ("trunk", GetTrunkFillLineStyle (trunkFillWidth)),
                new ValueItem ("primary", GetPrimaryFillLineStyle (primaryFillWidth)),
                new ValueItem ("secondary", GetRoadFillLineStyle (secondaryFillWidth)),
                new ValueItem ("tertiary", GetRoadFillLineStyle (tertiaryFillWidth)),
                new ValueItem ("motorway_link", GetMotorwayLinkFillLineStyle (motorwayLinkFillWidth)),
                new ValueItem ("trunk_link", GetTrunkLinkFillLineStyle (trunkLinkFillWidth)),
                new ValueItem ("primary_link", GetPrimaryLinkFillLineStyle (primaryLinkFillWidth)),
                new ValueItem ("secondary_link", GetRoadFillLineStyle (secondaryLinkFillWidth)),
                new ValueItem ("tertiary_link", GetRoadFillLineStyle (tertiaryLinkFillWidth)),
                new ValueItem ("residential", GetRoadFillLineStyle (residentialFillWidth)),
                new ValueItem ("unclassified", GetRoadFillLineStyle (unclassifiedFillWidth)),
                new ValueItem ("road", GetRoadFillLineStyle (roadFillWidth)),
                new ValueItem ("living_street", GetRoadFillLineStyle (livingStreetFillWidth)),
                new ValueItem ("service", GetRoadFillLineStyle (serviceFillWidth)),
                new ValueItem ("pedestrian", GetMinorRoadFillLineStyle (pedestrianFillWidth)),
                new ValueItem ("cycleway", GetMinorRoadFillLineStyle (cyclewayFillWidth)),
                new ValueItem ("footway", GetMinorRoadFillLineStyle (footwayFillWidth)),
                new ValueItem ("bridleway", GetMinorRoadFillLineStyle (bridlewayFillWidth)),
                new ValueItem ("path", GetMinorRoadFillLineStyle (pathFillWidth)),
                new ValueItem ("steps", GetStepsFillLineStyle (stepsFillWidth)),
                new ValueItem ("track", GetMinorRoadFillLineStyle (trackFillWidth))
            });

            CompositeStyle fill = new CompositeStyle();
            fill.Styles.Add(fillValueStyle);
            fill.Styles.Add(GetOneWayLineStyle("oneway", "yes", iconPath));

            ValueStyle label = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", motorwayFontSize),
                    GetGeneralPurposeTextStyle ("name:en", motorwayFontSize),
                    GetMotorwayRoadSheildLabelTextStyle ("ref", motorwayFontSize)
                }),
                new ValueItem ("trunk", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", trunkFontSize),
                    GetGeneralPurposeTextStyle ("name:en", trunkFontSize),
                    GetTrunkRoadSheildLabelTextStyle ("ref", trunkFontSize)
                }),
                new ValueItem ("primary", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", primaryFontSize),
                    GetGeneralPurposeTextStyle ("name:en", primaryFontSize),
                    GetPrimaryRoadSheildLabelTextStyle ("ref", primaryFontSize)
                }),
                new ValueItem ("secondary", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", secondaryFontSize),
                    GetGeneralPurposeTextStyle ("name:en", secondaryFontSize)
                }),
                new ValueItem ("tertiary", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", tertiaryFontSize),
                    GetGeneralPurposeTextStyle ("name:en", tertiaryFontSize)
                }),
                new ValueItem ("residential", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", residentialFontSize),
                    GetGeneralPurposeTextStyle ("name:en", residentialFontSize)
                }),
                new ValueItem ("unclassified", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", unclassifiedFontSize),
                    GetGeneralPurposeTextStyle ("name:en", unclassifiedFontSize)
                }),
                new ValueItem ("road", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", roadFontSize),
                    GetGeneralPurposeTextStyle ("name:en", roadFontSize)
                }),
                new ValueItem ("living_street", new Collection<Style> () {
                    GetGeneralPurposeTextStyle ("name", livingStreetFontSize),
                    GetGeneralPurposeTextStyle ("name:en", livingStreetFontSize)
                }),
            });

            FilterStyle roadSign = new FilterStyle();
            roadSign.Conditions.Add(new FilterCondition("ref", "^((?!;).)*$"));
            roadSign.Styles.Add(label);

            ValueStyle bridgeOutline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetBridgeLineStyle (motorwayOutlineWidth)),
                new ValueItem ("trunk", GetBridgeLineStyle (trunkOutlineWidth)),
                new ValueItem ("primary", GetBridgeLineStyle (primaryOutlineWidth)),
                new ValueItem ("secondary", GetBridgeLineStyle (secondaryOutlineWidth)),
                new ValueItem ("tertiary", GetBridgeLineStyle (tertiaryOutlineWidth)),
                new ValueItem ("motorway_link", GetBridgeLineStyle (motorwayLinkOutlineWidth)),
                new ValueItem ("trunk_link", GetBridgeLineStyle (trunkLinkOutlineWidth)),
                new ValueItem ("primary_link", GetBridgeLineStyle (primaryLinkOutlineWidth)),
                new ValueItem ("secondary_link", GetBridgeLineStyle (secondaryLinkOutlineWidth)),
                new ValueItem ("tertiary_link", GetBridgeLineStyle (tertiaryLinkOutlineWidth)),
                new ValueItem ("residential", GetBridgeLineStyle (residentialOutlineWidth)),
                new ValueItem ("unclassified", GetBridgeLineStyle (unclassifiedOutlineWidth)),
                new ValueItem ("road", GetBridgeLineStyle (roadOutlineWidth))
            });

            ValueStyle bridge = new ValueStyle("bridge", new Collection<ValueItem>() {
                new ValueItem ("yes", new Collection<Style> () { bridgeOutline, fill,
                    new ValueStyle ("layer", new Collection<ValueItem> () {
                        new ValueItem ("1", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("2", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("3", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("4", new Collection<Style> () { bridgeOutline, fill }),
                        new ValueItem ("5", new Collection<Style> () { bridgeOutline, fill })
                    }){ DrawingOrder =  ValueDrawingOrder.OrderByValueItems }
                })
            })
            { DrawingOrder = ValueDrawingOrder.OrderByValueItems };

            ValueStyle tunnelOutline = new ValueStyle("highway", new Collection<ValueItem>() {
                new ValueItem ("motorway", GetTunnelLineStyle (motorwayOutlineWidth)),
                new ValueItem ("trunk", GetTunnelLineStyle (trunkOutlineWidth)),
                new ValueItem ("primary", GetTunnelLineStyle (primaryOutlineWidth)),
                new ValueItem ("secondary", GetTunnelLineStyle (secondaryOutlineWidth)),
                new ValueItem ("tertiary", GetTunnelLineStyle (tertiaryOutlineWidth)),
                new ValueItem ("motorway_link", GetTunnelLineStyle (motorwayLinkOutlineWidth)),
                new ValueItem ("trunk_link", GetTunnelLineStyle (trunkLinkOutlineWidth)),
                new ValueItem ("primary_link", GetTunnelLineStyle (primaryLinkOutlineWidth)),
                new ValueItem ("secondary_link", GetTunnelLineStyle (secondaryLinkOutlineWidth)),
                new ValueItem ("tertiary_link", GetTunnelLineStyle (tertiaryLinkOutlineWidth)),
                new ValueItem ("residential", GetTunnelLineStyle (residentialOutlineWidth)),
                new ValueItem ("unclassified", GetTunnelLineStyle (unclassifiedOutlineWidth)),
                new ValueItem ("road", GetTunnelLineStyle (roadOutlineWidth))
            });

            ValueStyle tunnel = new ValueStyle("tunnel", new Collection<ValueItem>() {
                new ValueItem ("yes", new Collection<Style> () { tunnelOutline, fill,
                    new ValueStyle ("layer", new Collection<ValueItem> () {
                        new ValueItem ("-1", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-2", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-3", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-4", new Collection<Style> () { tunnelOutline, fill }),
                        new ValueItem ("-5", new Collection<Style> () { tunnelOutline, fill })
                    }){ DrawingOrder =  ValueDrawingOrder.OrderByValueItems }
                })
            })
            { DrawingOrder = ValueDrawingOrder.OrderByValueItems };

            styles.Styles.Add(tunnel);
            styles.Styles.Add(outline);
            styles.Styles.Add(fill);
            styles.Styles.Add(bridge);
            styles.Styles.Add(roadSign);

            return styles;
        }

        private ValueStyle GetRailwayLevel13ValueStyle()
        {
            return new ValueStyle("railway", new Collection<ValueItem>() {
                new ValueItem ("rail", GetRailLineStyle (2, 1, 3)),
                new ValueItem ("tram", GetRailLineStyle (1, 1, 1)),
                new ValueItem ("subway", GetSubwayLineStyle (2, 1, 3))
            });
        }

        private ValueStyle GetRailwayLevel16ValueStyle()
        {
            return new ValueStyle("railway", new Collection<ValueItem>() {
                new ValueItem ("rail", GetRailLineStyle (4, 2, 6)),
                new ValueItem ("tram", GetRailLineStyle (2, 1, 3)),
                new ValueItem ("subway", GetSubwayLineStyle (4, 2, 6)),
                new ValueItem ("narrow_gauge", GetMiniRailLineStyle (4, 2, 6)),
                new ValueItem ("light_rail", GetMiniRailLineStyle (2, 1, 3)),
                new ValueItem ("miniature", GetMiniRailLineStyle (2, 1, 3)),
                new ValueItem ("monorail", GetMonorailLineStyle (4, 2, 1))
            });
        }

        private ValueStyle GetRailwayLevel18ValueStyle()
        {
            return new ValueStyle("railway", new Collection<ValueItem>() {
                new ValueItem ("rail", GetRailLineStyle (8, 4, 12)),
                new ValueItem ("tram", GetRailLineStyle (6, 3, 9)),
                new ValueItem ("subway", GetSubwayLineStyle (8, 4, 12)),
                new ValueItem ("narrow_gauge", GetMiniRailLineStyle (8, 6, 10)),
                new ValueItem ("light_rail", GetMiniRailLineStyle (6, 4, 8)),
                new ValueItem ("miniature", GetMiniRailLineStyle (4, 2, 6)),
                new ValueItem ("monorail", GetMonorailLineStyle (6, 4, 2))
            });
        }
    }
}