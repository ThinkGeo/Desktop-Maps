//Copyright © 2014 ThinkGeo LLC. All rights reserved.
//The use of this code is for evaluating Map Suite products and the World Map Kit SDK for up to 60 days.  
//To license this product for use in your application please contact ThinkGeo at www.thinkgeo.com

using System;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.Layers
{
    public partial class WorldStreetsLayer
    {
        protected virtual LineStyle GetWaterLineStyle(float outerWidth)
        {
            return WorldStreetsLineStyles.Water(outerWidth);
        }

        protected virtual LineStyle GetBridgeLineStyle(float outerWidth)
        {
            return WorldStreetsLineStyles.Bridge(outerWidth);
        }

        protected virtual LineStyle GetRailLineStyle(float outerWidth, float innerWidth, float centerWidth)
        {
            return WorldStreetsLineStyles.Rail(outerWidth, innerWidth, centerWidth);
        }

        protected virtual LineStyle GetSubwayLineStyle(float outerWidth, float innerWidth, float centerWidth)
        {
            return WorldStreetsLineStyles.Subway(outerWidth, innerWidth, centerWidth);
        }

        protected virtual LineStyle GetMonorailLineStyle(float outerWidth, float innerWidth, float centerWidth)
        {
            return WorldStreetsLineStyles.Monorail(outerWidth, innerWidth, centerWidth);
        }

        protected virtual LineStyle GetMiniRailLineStyle(float outerWidth, float innerWidth, float centerWidth)
        {
            return WorldStreetsLineStyles.MiniRail(outerWidth, innerWidth, centerWidth);
        }

        protected virtual LineStyle GetPowerLineLineStyle(float outerWidth)
        {
            return WorldStreetsLineStyles.PowerLine(outerWidth);
        }

        protected virtual LineStyle GetApronLineStyle(float outerWidth)
        {
            return WorldStreetsLineStyles.Apron(outerWidth);
        }

        protected virtual LineStyle GetRunwayLineStyle(float outerWidth)
        {
            return WorldStreetsLineStyles.Runway(outerWidth);
        }

        protected virtual LineStyle GetTaxiwayLineStyle(float outerWidth)
        {
            return WorldStreetsLineStyles.Taxiway(outerWidth);
        }

        protected virtual LineStyle GetPierLineStyle(float outerWidth)
        {
            return WorldStreetsLineStyles.Pier(outerWidth);
        }

        protected virtual LineStyle GetCableCarLineStyle(float outerWidth)
        {
            return WorldStreetsLineStyles.CableCar(outerWidth);
        }

        protected virtual LineStyle GetHighwayLineStyle(float outerWidth)
        {
            return WorldStreetsLineStyles.Highway(outerWidth);
        }

        protected virtual LineStyle GetBoundaryMinorLineStyle(float outerWidth)
        {
            return WorldStreetsLineStyles.BoundaryMinor(outerWidth);
        }

        protected virtual LineStyle GetBoundaryLineStyle(float outerWidth)
        {
            return WorldStreetsLineStyles.Boundary(outerWidth);
        }

        protected virtual LineStyle GetBoundaryNationalLineStyle(float outerWidth)
        {
            return WorldStreetsLineStyles.BoundaryNational(outerWidth);
        }

        protected virtual LineStyle GetBoundaryStateLineStyle(float outerWidth)
        {
            return WorldStreetsLineStyles.BoundaryState(outerWidth);
        }

        protected virtual LineStyle GetBoundaryCountyLineStyle(float outerWidth)
        {
            return WorldStreetsLineStyles.BoundaryCounty(outerWidth);
        }

        protected virtual LineStyle GetBoundaryCityLineStyle(float outerWidth)
        {
            return WorldStreetsLineStyles.BoundaryCity(outerWidth);
        }

        protected virtual LineStyle GetMotorwayOutlineLineStyle(float width)
        {
            return WorldStreetsLineStyles.MotorwayOutline(width);
        }

        protected virtual LineStyle GetMotorwayFillLineStyle(float width)
        {
            return WorldStreetsLineStyles.MotorwayFill(width);
        }

        protected virtual LineStyle GetTrunkOutlineLineStyle(float width)
        {
            return WorldStreetsLineStyles.TrunkOutline(width);
        }

        protected virtual LineStyle GetTrunkFillLineStyle(float width)
        {
            return WorldStreetsLineStyles.TrunkFill(width);
        }

        protected virtual LineStyle GetPrimaryOutlineLineStyle(float width)
        {
            return WorldStreetsLineStyles.PrimaryOutline(width);
        }

        protected virtual LineStyle GetPrimaryFillLineStyle(float width)
        {
            return WorldStreetsLineStyles.PrimaryFill(width);
        }

        protected virtual LineStyle GetRoadOutlineLineStyle(float width)
        {
            return WorldStreetsLineStyles.RoadOutline(width);
        }

        protected virtual LineStyle GetRoadFillLineStyle(float width)
        {
            return WorldStreetsLineStyles.RoadFill(width);
        }

        protected virtual LineStyle GetMotorwayLinkOutlineLineStyle(float width)
        {
            return WorldStreetsLineStyles.MotorwayLinkOutline(width);
        }

        protected virtual LineStyle GetMotorwayLinkFillLineStyle(float width)
        {
            return WorldStreetsLineStyles.MotorwayLinkFill(width);
        }

        protected virtual LineStyle GetTrunkLinkOutlineLineStyle(float width)
        {
            return WorldStreetsLineStyles.TrunkLinkOutline(width);
        }

        protected virtual LineStyle GetTrunkLinkFillLineStyle(float width)
        {
            return WorldStreetsLineStyles.TrunkLinkFill(width);
        }

        protected virtual LineStyle GetPrimaryLinkOutlineLineStyle(float width)
        {
            return WorldStreetsLineStyles.PrimaryLinkOutline(width);
        }

        protected virtual LineStyle GetPrimaryLinkFillLineStyle(float width)
        {
            return WorldStreetsLineStyles.PrimaryLinkFill(width);
        }

        protected virtual LineStyle GetMinorRoadOutlineLineStyle(float width)
        {
            return WorldStreetsLineStyles.MinorRoadOutline(width);
        }

        protected virtual LineStyle GetMinorRoadFillLineStyle(float width)
        {
            return WorldStreetsLineStyles.MinorRoadFill(width);
        }

        protected virtual LineStyle GetStepsOutlineLineStyle(float width)
        {
            return WorldStreetsLineStyles.StepsOutline(width);
        }

        protected virtual LineStyle GetStepsFillLineStyle(float width)
        {
            return WorldStreetsLineStyles.StepsFill(width);
        }

        protected virtual LineStyle GetTunnelLineStyle(float width)
        {
            return WorldStreetsLineStyles.Tunnel(width);
        }

        protected virtual LineStyle GetOneWayLineStyle(string columnName, string columnValue, string iconPath)
        {
            return WorldStreetsLineStyles.OneWay(columnName, columnValue, iconPath);
        }
    }
}
