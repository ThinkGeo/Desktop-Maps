//Copyright © 2014 ThinkGeo LLC. All rights reserved.
//The use of this code is for evaluating Map Suite products and the World Map Kit SDK for up to 60 days.  
//To license this product for use in your application please contact ThinkGeo at www.thinkgeo.com

using System;
using System.IO;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Styles;

namespace ThinkGeo.MapSuite.Layers
{
    public partial class WorldStreetsLayer
    {
        protected virtual PointStyle GetPowerPolePointStyle()
        {
            return new PointStyle()
            {
                PointType = PointType.Symbol,
                SymbolType = PointSymbolType.Square,
                SymbolPen = new GeoPen(new GeoColor(255, 153, 153, 153)),
                SymbolSolidBrush = new GeoSolidBrush(new GeoColor(255, 204, 204, 204)),
                SymbolSize = 3,
                // Todo: comment out this line code temporarily to make sure we can complie, we need to check with David.
                //StyleClass = "WorldMapKitPointStyles.PowerPole"
            };
        }

        protected virtual PointStyle GetImagePointStyle(string filename)
        {
            return new PointStyle()
            {
                PointType = PointType.Bitmap,
//                Image = new GeoImage(AppDomain.CurrentDomain.BaseDirectory + "Icons" + filename),
				Image = new GeoImage(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icons", filename)),
                DrawingLevel = DrawingLevel.LabelLevel,
                // Todo: comment out this line code temporarily to make sure we can complie, we need to check with David.
                //StyleClass = "WorldMapKitPointStyles." + Path.GetFileNameWithoutExtension(Path.GetFileNameWithoutExtension(filename)) 
            };
        }

        protected virtual PointStyle GetAerowayTerminalPointStyle()
        {
            return GetImagePointStyle("aerowayterminal.16.png");
        }

        protected virtual PointStyle GetAerowayHelipadPointStyle()
        {
            return GetImagePointStyle("aerowayhelipad.16.png");
        }

        protected virtual PointStyle GetAerowayGatePointStyle()
        {
            return GetImagePointStyle("aerowaygate.16.png");
        }

        protected virtual PointStyle GetAerowayAerodromePointStyle()
        {
            return GetImagePointStyle("aerowayaerodrome.16.png");
        }

        protected virtual PointStyle GetAmenityToiletsDisabilityAccessPointStyle()
        {
            return GetImagePointStyle("amenitytoilets-disabled.16.png");
        }

        protected virtual PointStyle GetAmenityBarPointStyle()
        {
            return GetImagePointStyle("amenitybar.16.png");
        }

        protected virtual PointStyle GetAmenityToiletsGenderAccessMenPointStyle()
        {
            return GetImagePointStyle("amenitytoilets-men.16.png");
        }

        protected virtual PointStyle GetAmenityToiletsGenderAccessWomenPointStyle()
        {
            return GetImagePointStyle("amenitytoilets-women.16.png");
        }

        protected virtual PointStyle GetAmenityToiletsPointStyle()
        {
            return GetImagePointStyle("amenitytoilets.16.png");
        }

        protected virtual PointStyle GetAmenityWasteDisposalPointStyle()
        {
            return GetImagePointStyle("amenitywaste_disposal.16.png");
        }

        protected virtual PointStyle GetAmenityVeterinaryPointStyle()
        {
            return GetImagePointStyle("amenityveterinary.16.png");
        }

        protected virtual PointStyle GetAmenityVendingMachinePointStyle()
        {
            return GetImagePointStyle("amenityvending_machine.16.png");
        }

        protected virtual PointStyle GetAmenityUniversityPointStyle()
        {
            return GetImagePointStyle("amenityuniversity.16.png");
        }

        protected virtual PointStyle GetAmenityTownHallPointStyle()
        {
            return GetImagePointStyle("amenitytownhall-2.16.png");
        }

        protected virtual PointStyle GetAmenityTheatrePointStyle()
        {
            return GetImagePointStyle("amenitytheatre.16.png");
        }

        protected virtual PointStyle GetAmenityTelephonePointStyle()
        {
            return GetImagePointStyle("amenitytelephone.16.png");
        }

        protected virtual PointStyle GetAmenityTaxiPointStyle()
        {
            return GetImagePointStyle("amenitytaxi.16.png");
        }

        protected virtual PointStyle GetAmenityShelterPointStyle()
        {
            return GetImagePointStyle("amenityshelter-2.16.png");
        }

        protected virtual PointStyle GetAmenitySchoolPointStyle()
        {
            return GetImagePointStyle("amenityschool.16.png");
        }

        protected virtual PointStyle GetAmenityRestaurantPointStyle()
        {
            return GetImagePointStyle("amenityrestaurant.16.png");
        }

        protected virtual PointStyle GetAmenityRecyclingPointStyle()
        {
            return GetImagePointStyle("amenityrecycling.16.png");
        }

        protected virtual PointStyle GetAmenityPubPointStyle()
        {
            return GetImagePointStyle("amenitypub.16.png");
        }

        protected virtual PointStyle GetAmenityPrisonPointStyle()
        {
            return GetImagePointStyle("amenityprison.16.png");
        }

        protected virtual PointStyle GetAmenityPostOfficePointStyle()
        {
            return GetImagePointStyle("amenitypost_office.16.png");
        }

        protected virtual PointStyle GetAmenityPostBoxPointStyle()
        {
            return GetImagePointStyle("amenitypost_box.16.png");
        }

        protected virtual PointStyle GetAmenityPolicePointStyle()
        {
            return GetImagePointStyle("amenitypolice-2.16.png");
        }

        protected virtual PointStyle GetAmenityNursingHomePointStyle()
        {
            return GetImagePointStyle("amenitynursing_home.16.png");
        }

        protected virtual PointStyle GetAmenityLibraryPointStyle()
        {
            return GetImagePointStyle("amenitylibrary-2.16.png");
        }

        protected virtual PointStyle GetAmenityIceCreamPointStyle()
        {
            return GetImagePointStyle("amenityice_cream.16.png");
        }

        protected virtual PointStyle GetAmenityFountainPointStyle()
        {
            return GetImagePointStyle("amenityfountain-2.16.png");
        }

        protected virtual PointStyle GetAmenityFireStationPointStyle()
        {
            return GetImagePointStyle("amenityfire_station-2.16.png");
        }

        protected virtual PointStyle GetAmenityEmbassyPointStyle()
        {
            return GetImagePointStyle("amenityembassy.16.png");
        }

        protected virtual PointStyle GetAmenityDrinkingWaterPointStyle()
        {
            return GetImagePointStyle("amenitydrinking_water.16.png");
        }

        protected virtual PointStyle GetAmenityDoctorsPointStyle()
        {
            return GetImagePointStyle("amenitydoctors.16.png");
        }

        protected virtual PointStyle GetAmenityDentistPointStyle()
        {
            return GetImagePointStyle("amenitydentist.16.png");
        }

        protected virtual PointStyle GetAmenityCourtHousePointStyle()
        {
            return GetImagePointStyle("amenitycourthouse.16.png");
        }

        protected virtual PointStyle GetAmenityCollegePointStyle()
        {
            return GetImagePointStyle("amenitycollege.16.png");
        }

        protected virtual PointStyle GetAmenityClockPointStyle()
        {
            return GetImagePointStyle("amenityclock.16.png");
        }

        protected virtual PointStyle GetAmenityCinemaPointStyle()
        {
            return GetImagePointStyle("amenitycinema-2.16.png");
        }

        protected virtual PointStyle GetAmenityCarRentalPointStyle()
        {
            return GetImagePointStyle("amenitycar_rental.16.png");
        }

        protected virtual PointStyle GetAmenityCafePointStyle()
        {
            return GetImagePointStyle("amenitycafe.16.png");
        }

        protected virtual PointStyle GetAmenityBusStationPointStyle()
        {
            return GetImagePointStyle("amenitybus_station.16.png");
        }

        protected virtual PointStyle GetAmenityBiergartenPointStyle()
        {
            return GetImagePointStyle("amenitybiergarten.16.png");
        }

        protected virtual PointStyle GetAmenityBicycleRentalPointStyle()
        {
            return GetImagePointStyle("amenitybicycle_rental.16.png");
        }

        protected virtual PointStyle GetAmenityBicycleParkingPointStyle()
        {
            return GetImagePointStyle("amenityparking-bicycle.16.png");
        }

        protected virtual PointStyle GetAmenityBenchPointStyle()
        {
            return GetImagePointStyle("amenitybench.16.png");
        }

        protected virtual PointStyle GetAmenityPlaceOfWorshipSikhPointStyle()
        {
            return GetImagePointStyle("amenityplace_of_worship-sikh-3.16.png");
        }

        protected virtual PointStyle GetAmenityPlaceOfWorshipShintoPointStyle()
        {
            return GetImagePointStyle("amenityplace_of_worship-shinto-3.16.png");
        }

        protected virtual PointStyle GetAmenityPlaceOfWorshipJewishPointStyle()
        {
            return GetImagePointStyle("amenityplace_of_worship-jewish-3.16.png");
        }

        protected virtual PointStyle GetAmenityPlaceOfWorshipJainPointStyle()
        {
            return GetImagePointStyle("amenityplace_of_worship-jain-3.16.png");
        }

        protected virtual PointStyle GetAmenityPlaceOfWorshipIslamicPointStyle()
        {
            return GetImagePointStyle("amenityplace_of_worship-islamic-3.16.png");
        }

        protected virtual PointStyle GetAmenityPlaceOfWorshipHinduPointStyle()
        {
            return GetImagePointStyle("amenityplace_of_worship-hindu-3.16.png");
        }

        protected virtual PointStyle GetAmenityPlaceOfWorshipChristianPointStyle()
        {
            return GetImagePointStyle("amenityplace_of_worship-christian-3.16.png");
        }

        protected virtual PointStyle GetAmenityPlaceOfWorshipBuddhistPointStyle()
        {
            return GetImagePointStyle("amenityplace_of_worship-buddhist-3.16.png");
        }

        protected virtual PointStyle GetAmenityPlaceOfWorshipPointStyle()
        {
            return GetImagePointStyle("amenityplace_of_worship-unknown-3.16.png");
        }

        protected virtual PointStyle GetAmenityPharmacyDispencingPointStyle()
        {
            return GetImagePointStyle("amenitypharmacy-dispencing.16.png");
        }

        protected virtual PointStyle GetAmenityPharmacyPointStyle()
        {
            return GetImagePointStyle("amenitypharmacy.16.png");
        }

        protected virtual PointStyle GetAmenityParkingPrivatePointStyle()
        {
            return GetImagePointStyle("amenityparking-private.16.png");
        }

        protected virtual PointStyle GetAmenityParkingCarPaidPointStyle()
        {
            return GetImagePointStyle("amenityparking-car_paid.16.png");
        }

        protected virtual PointStyle GetAmenityParkingDisabledPointStyle()
        {
            return GetImagePointStyle("amenityparking-disabled.16.png");
        }

        protected virtual PointStyle GetAmenityParkingPointStyle()
        {
            return GetImagePointStyle("amenityparking.16.png");
        }

        protected virtual PointStyle GetAmenityHospitalEmergencyPointStyle()
        {
            return GetImagePointStyle("amenityhospital-emergency2.16.png");
        }

        protected virtual PointStyle GetAmenityHospitalPointStyle()
        {
            return GetImagePointStyle("amenityhospital.16.png");
        }

        protected virtual PointStyle GetAmenityFuelLpgPointStyle()
        {
            return GetImagePointStyle("amenityfuel-lpg.16.png");
        }

        protected virtual PointStyle GetAmenityFuelPointStyle()
        {
            return GetImagePointStyle("amenityfuel.16.png");
        }

        protected virtual PointStyle GetAmenityFastFoodPizzaPointStyle()
        {
            return GetImagePointStyle("amenityfast_food-pizza.16.png");
        }

        protected virtual PointStyle GetAmenityFastFoodPointStyle()
        {
            return GetImagePointStyle("amenityfast_food.16.png");
        }

        protected virtual PointStyle GetBarrierTollboothPointStyle()
        {
            return GetImagePointStyle("barriertoll_booth.16.png");
        }

        protected virtual PointStyle GetBarrierStilePointStyle()
        {
            return GetImagePointStyle("barrierstile.16.png");
        }

        protected virtual PointStyle GetBarrierLiftGatePointStyle()
        {
            return GetImagePointStyle("barrierlift_gate.16.png");
        }

        protected virtual PointStyle GetBarrierKissingGatePointStyle()
        {
            return GetImagePointStyle("barrierkissing_gate.16.png");
        }

        protected virtual PointStyle GetBarrierGatePointStyle()
        {
            return GetImagePointStyle("barriergate.16.png");
        }

        protected virtual PointStyle GetBarrierEntrancePointStyle()
        {
            return GetImagePointStyle("barrierentrance.16.png");
        }

        protected virtual PointStyle GetBarrierCycleBarrierPointStyle()
        {
            return GetImagePointStyle("barriercycle_barrier.16.png");
        }

        protected virtual PointStyle GetBarrierCattleGridPointStyle()
        {
            return GetImagePointStyle("barriercattle_grid.16.png");
        }

        protected virtual PointStyle GetBarrierBollardPointStyle()
        {
            return GetImagePointStyle("barrierbollard.16.png");
        }

        protected virtual PointStyle GetBarrierBlockPointStyle()
        {
            return GetImagePointStyle("barrierblock.16.png");
        }

        protected virtual PointStyle GetFordPointStyle()
        {
            return GetImagePointStyle("fordyes.16.png");
        }

        protected virtual PointStyle GetHistoricWreckPointStyle()
        {
            return GetImagePointStyle("historicwreck.16.png");
        }

        protected virtual PointStyle GetHistoricWayside_ShrinePointStyle()
        {
            return GetImagePointStyle("historicwayside_shrine.16.png");
        }

        protected virtual PointStyle GetHistoricWaysideCrossPointStyle()
        {
            return GetImagePointStyle("historicwayside_cross.16.png");
        }

        protected virtual PointStyle GetHistoricRuinsPointStyle()
        {
            return GetImagePointStyle("historicruins.16.png");
        }

        protected virtual PointStyle GetHistoricMonumentPointStyle()
        {
            return GetImagePointStyle("historicmonument.16.png");
        }

        protected virtual PointStyle GetHistoricMemorialPointStyle()
        {
            return GetImagePointStyle("historicmemorial.16.png");
        }

        protected virtual PointStyle GetHistoricCastlePointStyle()
        {
            return GetImagePointStyle("historiccastle-2.16.png");
        }

        protected virtual PointStyle GetHistoricBattlefieldPointStyle()
        {
            return GetImagePointStyle("historicbattlefield.16.png");
        }

        protected virtual PointStyle GetHighwayTrafficSignalsPointStyle()
        {
            return GetImagePointStyle("highwaytraffic_signals.16.png");
        }

        protected virtual PointStyle GetHighwayBusStopPointStyle()
        {
            return GetImagePointStyle("highwaybus_stop-2.16.png");
        }

        protected virtual PointStyle GetHighwayMiniRoundaboutAnticlockwisePointStyle()
        {
            return GetImagePointStyle("highwaymini_roundabout-anticlockwise.16.png");
        }

        protected virtual PointStyle GetHighwayMiniRoundaboutClockwisePointStyle()
        {
            return GetImagePointStyle("highwaymini_roundabout-clockwise.16.png");
        }

        protected virtual PointStyle GetLeisureStadiumPointStyle()
        {
            return GetImagePointStyle("leisurestadium.16.png");
        }

        protected virtual PointStyle GetLeisureSlipwayPointStyle()
        {
            return GetImagePointStyle("leisureslipway.16.png");
        }

        protected virtual PointStyle GetLeisureMarinaPointStyle()
        {
            return GetImagePointStyle("leisuremarina.16.png");
        }

        protected virtual PointStyle GetManMadeWindmillPointStyle()
        {
            return GetImagePointStyle("man_madewindmill.16.png");
        }

        protected virtual PointStyle GetManMadeWaterTowerPointStyle()
        {
            return GetImagePointStyle("man_madewater_tower.16.png");
        }

        protected virtual PointStyle GetManMadeLighthousePointStyle()
        {
            return GetImagePointStyle("man_madelighthouse.16.png");
        }

        protected virtual PointStyle GetManMadeCranePointStyle()
        {
            return GetImagePointStyle("man_madecrane.16.png");
        }

        protected virtual PointStyle GetManMadeMineshaftAbandonedPointStyle()
        {
            return GetImagePointStyle("man_mademineshaft-abandoned.16.png");
        }

        protected virtual PointStyle GetManMadeMineshaftPointStyle()
        {
            return GetImagePointStyle("man_mademineshaft.16.png");
        }

        protected virtual PointStyle GetMilitaryBunkerPointStyle()
        {
            return GetImagePointStyle("militarybunker.16.png");
        }

        protected virtual PointStyle GetNaturalPeakPointStyle()
        {
            return GetImagePointStyle("naturalpeak-2.16.png");
        }

        protected virtual PointStyle GetNaturalCavePointStyle()
        {
            return GetImagePointStyle("naturalcave.16.png");
        }

        protected virtual PointStyle GetNaturalWoodConiferousPointStyle()
        {
            return GetImagePointStyle("naturalwood-coniferous.16.png");
        }

        protected virtual PointStyle GetNaturalWoodDeciduousPointStyle()
        {
            return GetImagePointStyle("naturalwood-deciduous.16.png");
        }

        protected virtual PointStyle GetNaturalWoodMixedPointStyle()
        {
            return GetImagePointStyle("naturalwood-mixed.16.png");
        }

        protected virtual PointStyle GetOfficeEstateAgentPointStyle()
        {
            return GetImagePointStyle("officeestate_agent-2.16.png");
        }

        protected virtual PointStyle GetPowerTowerPointStyle()
        {
            return GetImagePointStyle("powertower.16.png");
        }

        protected virtual PointStyle GetPowerSubStationPointStyle()
        {
            return GetImagePointStyle("powersub_station.16.png");
        }

        protected virtual PointStyle GetPowerPlantHydroPointStyle()
        {
            return GetImagePointStyle("powerplant-water.16.png");
        }

        protected virtual PointStyle GetPowerPlantGasPointStyle()
        {
            return GetImagePointStyle("powerplant-gas.16.png");
        }

        protected virtual PointStyle GetPowerPlantSolarPointStyle()
        {
            return GetImagePointStyle("powerplant-solar.16.png");
        }

        protected virtual PointStyle GetPowerPlantWindPointStyle()
        {
            return GetImagePointStyle("powerplant-wind.16.png");
        }

        protected virtual PointStyle GetPowerPlantCoalPointStyle()
        {
            return GetImagePointStyle("powerplant-coal.16.png");
        }

        protected virtual PointStyle GetRailwayTramStopPointStyle()
        {
            return GetImagePointStyle("railwaytram_stop.16.png");
        }

        protected virtual PointStyle GetRailwaySubwayPointStyle()
        {
            return GetImagePointStyle("railwaysubway.16.png");
        }

        protected virtual PointStyle GetShopVideoPointStyle()
        {
            return GetImagePointStyle("shopvideo.16.png");
        }

        protected virtual PointStyle GetShopToysPointStyle()
        {
            return GetImagePointStyle("shoptoys.16.png");
        }

        protected virtual PointStyle GetShopTobaccoPointStyle()
        {
            return GetImagePointStyle("shoptobacco.16.png");
        }

        protected virtual PointStyle GetShopSupermarketPointStyle()
        {
            return GetImagePointStyle("shopsupermarket.16.png");
        }

        protected virtual PointStyle GetShopPetPointStyle()
        {
            return GetImagePointStyle("shoppet-2.16.png");
        }

        protected virtual PointStyle GetShopOpticianPointStyle()
        {
            return GetImagePointStyle("shopoptician.16.png");
        }

        protected virtual PointStyle GetShopNewsagentPointStyle()
        {
            return GetImagePointStyle("shopnewsagent.16.png");
        }

        protected virtual PointStyle GetShopMusicalInstrumentPointStyle()
        {
            return GetImagePointStyle("shopmusic.16.png");
        }

        protected virtual PointStyle GetShopMotorcyclePointStyle()
        {
            return GetImagePointStyle("shopmotorcycle.16.png");
        }

        protected virtual PointStyle GetShopMobliePhonePointStyle()
        {
            return GetImagePointStyle("shopmobile_phone.16.png");
        }

        protected virtual PointStyle GetShopLaundryPointStyle()
        {
            return GetImagePointStyle("shoplaundry.16.png");
        }

        protected virtual PointStyle GetShopKioskPointStyle()
        {
            return GetImagePointStyle("shopkiosk.16.png");
        }

        protected virtual PointStyle GetShopJewelryPointStyle()
        {
            return GetImagePointStyle("shopjewelry.16.png");
        }

        protected virtual PointStyle GetShopHifiPointStyle()
        {
            return GetImagePointStyle("shophifi.16.png");
        }

        protected virtual PointStyle GetShopHearingAidsPointStyle()
        {
            return GetImagePointStyle("shophearing_aids.16.png");
        }

        protected virtual PointStyle GetShopHairdresserPointStyle()
        {
            return GetImagePointStyle("shophairdresser.16.png");
        }

        protected virtual PointStyle GetShopGreengrocerPointStyle()
        {
            return GetImagePointStyle("shopgreengrocer.16.png");
        }

        protected virtual PointStyle GetShopGiftPointStyle()
        {
            return GetImagePointStyle("shopgift.16.png");
        }

        protected virtual PointStyle GetShopGardenCentrePointStyle()
        {
            return GetImagePointStyle("shopgarden_centre.16.png");
        }

        protected virtual PointStyle GetShopFloristPointStyle()
        {
            return GetImagePointStyle("shopflorist.16.png");
        }

        protected virtual PointStyle GetShopDoItYourselfPointStyle()
        {
            return GetImagePointStyle("shopdoityourself.16.png");
        }

        protected virtual PointStyle GetShopDepartmentStorePointStyle()
        {
            return GetImagePointStyle("shopdepartment_store.16.png");
        }

        protected virtual PointStyle GetShopCopyshopPointStyle()
        {
            return GetImagePointStyle("shopcopyshop.16.png");
        }

        protected virtual PointStyle GetShopConveniencePointStyle()
        {
            return GetImagePointStyle("shopconvenience.16.png");
        }

        protected virtual PointStyle GetShopConfectioneryPointStyle()
        {
            return GetImagePointStyle("shopconfectionery.16.png");
        }

        protected virtual PointStyle GetShopComputerPointStyle()
        {
            return GetImagePointStyle("shopcomputer.16.png");
        }

        protected virtual PointStyle GetShopClothesPointStyle()
        {
            return GetImagePointStyle("shopclothes.16.png");
        }

        protected virtual PointStyle GetShopCarPointStyle()
        {
            return GetImagePointStyle("shopcar.16.png");
        }

        protected virtual PointStyle GetShopCarRepairPointStyle()
        {
            return GetImagePointStyle("shopcar_repair.16.png");
        }

        protected virtual PointStyle GetShopButcherPointStyle()
        {
            return GetImagePointStyle("shopbutcher.16.png");
        }

        protected virtual PointStyle GetShopBooksPointStyle()
        {
            return GetImagePointStyle("shopbooks.16.png");
        }

        protected virtual PointStyle GetShopBicyclePointStyle()
        {
            return GetImagePointStyle("shopbicycle.16.png");
        }

        protected virtual PointStyle GetShopBakeryPointStyle()
        {
            return GetImagePointStyle("shopbakery.16.png");
        }

        protected virtual PointStyle GetShopAlcoholPointStyle()
        {
            return GetImagePointStyle("shopalcohol.16.png");
        }

        protected virtual PointStyle GetSportTennisPointStyle()
        {
            return GetImagePointStyle("sporttennis.16.png");
        }

        protected virtual PointStyle GetSportSoccerPointStyle()
        {
            return GetImagePointStyle("sportsoccer.16.png");
        }

        protected virtual PointStyle GetSportShootingPointStyle()
        {
            return GetImagePointStyle("sportshooting.16.png");
        }

        protected virtual PointStyle GetSportMotorPointStyle()
        {
            return GetImagePointStyle("sportmotor.16.png");
        }

        protected virtual PointStyle GetSportIceStockPointStyle()
        {
            return GetImagePointStyle("sportice_stock.16.png");
        }

        protected virtual PointStyle GetSportHorseRacingPointStyle()
        {
            return GetImagePointStyle("sporthorse_racing.16.png");
        }

        protected virtual PointStyle GetSportGymnasticsPointStyle()
        {
            return GetImagePointStyle("sportgymnastics-2.16.png");
        }

        protected virtual PointStyle GetSportGolfPointStyle()
        {
            return GetImagePointStyle("sportgolf.16.png");
        }

        protected virtual PointStyle GetSportDivingPointStyle()
        {
            return GetImagePointStyle("sportdiving.16.png");
        }

        protected virtual PointStyle GetSportCricketPointStyle()
        {
            return GetImagePointStyle("sportcricket.16.png");
        }

        protected virtual PointStyle GetSportClimbingPointStyle()
        {
            return GetImagePointStyle("sportclimbing.16.png");
        }

        protected virtual PointStyle GetSportCanoePointStyle()
        {
            return GetImagePointStyle("sportcanoe.16.png");
        }

        protected virtual PointStyle GetSportBaseballPointStyle()
        {
            return GetImagePointStyle("sportbaseball.16.png");
        }

        protected virtual PointStyle GetSportArcheryPointStyle()
        {
            return GetImagePointStyle("sportarchery.16.png");
        }

        protected virtual PointStyle GetSportSkiingPointStyle()
        {
            return GetImagePointStyle("sportskiing-downhill.16.png");
        }

        protected virtual PointStyle GetSportSwimmingCoveredPointStyle()
        {
            return GetImagePointStyle("sportswimming-indoor.16.png");
        }

        protected virtual PointStyle GetSportSwimmingPointStyle()
        {
            return GetImagePointStyle("sportswimming-outdoor.16.png");
        }

        protected virtual PointStyle GetTourismZooPointStyle()
        {
            return GetImagePointStyle("tourismzoo.16.png");
        }

        protected virtual PointStyle GetTourismViewpointPointStyle()
        {
            return GetImagePointStyle("tourismviewpoint.16.png");
        }

        protected virtual PointStyle GetTourismThemeParkPointStyle()
        {
            return GetImagePointStyle("tourismtheme_park.16.png");
        }

        protected virtual PointStyle GetTourismPicnicStiePointStyle()
        {
            return GetImagePointStyle("tourismpicnic_site.16.png");
        }

        protected virtual PointStyle GetTourismMuseumPointStyle()
        {
            return GetImagePointStyle("tourismmuseum.16.png");
        }

        protected virtual PointStyle GetTourismMotelPointStyle()
        {
            return GetImagePointStyle("tourismmotel.16.png");
        }

        protected virtual PointStyle GetTourismHotelPointStyle()
        {
            return GetImagePointStyle("tourismhotel-2.16.png");
        }

        protected virtual PointStyle GetTourismHostelPointStyle()
        {
            return GetImagePointStyle("tourismhostel.16.png");
        }

        protected virtual PointStyle GetTourismChaletPointStyle()
        {
            return GetImagePointStyle("tourismchalet.16.png");
        }

        protected virtual PointStyle GetTourismCaravanSitePointStyle()
        {
            return GetImagePointStyle("tourismcaravan_site.16.png");
        }

        protected virtual PointStyle GetTourismCampSitePointStyle()
        {
            return GetImagePointStyle("tourismcamp_site.16.png");
        }

        protected virtual PointStyle GetTourismAttractionPointStyle()
        {
            return GetImagePointStyle("tourismattraction.16.png");
        }

        protected virtual PointStyle GetTourismArtworkPointStyle()
        {
            return GetImagePointStyle("tourismartwork-2.16.png");
        }

        protected virtual PointStyle GetTourismAlpineHutPointStyle()
        {
            return GetImagePointStyle("tourismalpine_hut.16.png");
        }

        protected virtual PointStyle GetTourismInformationGuidepostPointStyle()
        {
            return GetImagePointStyle("tourisminformation-guidepost.16.png");
        }

        protected virtual PointStyle GetTourismInformationMapPointStyle()
        {
            return GetImagePointStyle("tourisminformation-map.16.png");
        }

        protected virtual PointStyle GetTourismInformationPointStyle()
        {
            return GetImagePointStyle("tourisminformation.16.png");
        }

        protected virtual PointStyle GetWaterwayWeirPointStyle()
        {
            return GetImagePointStyle("waterwayweir.16.png");
        }

        protected virtual PointStyle GetAmenityCasinoPointStyle()
        {
            return GetImagePointStyle("amenitycasino.16.png");
        }
    }
}
