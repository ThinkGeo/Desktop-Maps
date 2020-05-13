//Copyright © 2014 ThinkGeo LLC. All rights reserved.
//The use of this code is for evaluating Map Suite products and the World Map Kit SDK for up to 60 days.  
//To license this product for use in your application please contact ThinkGeo at www.thinkgeo.com

using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.Layers
{
    public partial class WorldStreetsLayer
    {
        protected virtual TextStyle GetGeneralPurposeTextStyle(string labelColumnName, float fontSize)
        {
            return WorldStreetsTextStyles.GeneralPurpose(labelColumnName, fontSize);
        }

        protected virtual TextStyle GetPoiLabelTextStyle(string labelColumnName, float fontSize)
        {
            return WorldStreetsTextStyles.Poi(labelColumnName, fontSize, -5);
        }

        protected virtual TextStyle GetWaterLabelTextStyle(string nameColumnName, float fontSize)
        {
            return WorldStreetsTextStyles.Water(nameColumnName, fontSize);
        }

        protected virtual TextStyle GetMotorwayRoadSheildLabelTextStyle(string nameColumnName, float fontSize)
        {
            return WorldStreetsTextStyles.MotorwayRoadSheild(nameColumnName, fontSize);
        }

        protected virtual TextStyle GetTrunkRoadSheildLabelTextStyle(string nameColumnName, float fontSize)
        {
            return WorldStreetsTextStyles.TrunkRoadSheild(nameColumnName, fontSize);
        }

        protected virtual TextStyle GetPrimaryRoadSheildLabelTextStyle(string nameColumnName, float fontSize)
        {
            return WorldStreetsTextStyles.PrimaryRoadSheild(nameColumnName, fontSize);
        }
    }
}
