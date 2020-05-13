using System;
using System.Globalization;

namespace ThinkGeo.MapSuite.USDemographicMap
{
    public static class TextFormatter
    {
        public static string GetFormatedStringForLegendItem(string columnName, double value)
        {
            string aliasUnitString = GetFormatedString(columnName, value);
            string[] subStrings = aliasUnitString.Split(new string[] { ": " }, StringSplitOptions.None);

            if (subStrings[subStrings.Length - 1].Contains("%"))
            {
                aliasUnitString = subStrings[subStrings.Length - 1];
            }
            else
            {
                aliasUnitString = value.ToString("N0", CultureInfo.InvariantCulture);
            }

            return aliasUnitString;
        }

        public static string GetFormatedString(string columnName, double value)
        {
            string result = string.Empty;
            switch (columnName)
            {
                case "Population":
                    result = string.Format("Population : {0} People", value.ToString("N0", CultureInfo.InvariantCulture));
                    break;
                case "PopDensity":
                    result = string.Format("Population Density : {0} People / sq.mi.", value.ToString("N0", CultureInfo.InvariantCulture));
                    break;
                case "Female":
                    result = string.Format("Female : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "Male":
                    result = string.Format("Male : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "AREALAND":
                    result = string.Format("Land Area : {0} sq.mi.", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "AREAWATR":
                    result = string.Format("Water Area : {0} sq.mi.", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "White":
                    result = string.Format("White : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "Black":
                    result = string.Format("Black : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "Indian":
                    result = string.Format("American Indian : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "Islander":
                    result = string.Format("Native Pacific Islander : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "Asian":
                    result = string.Format("Asian : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "Other":
                    result = string.Format("Other : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "MultiRace":
                    result = string.Format("Multiracial : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "Under5":
                    result = string.Format("<= 5 : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "5to9":
                    result = string.Format("5 ~ 10 : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "10to14":
                    result = string.Format("10 ~ 15 : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "15to17":
                    result = string.Format("15 ~ 18 : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "18to24":
                    result = string.Format("18 ~ 25 : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "25to34":
                    result = string.Format("25 ~ 35 : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "35to44":
                    result = string.Format("35 ~ 45 : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "45to54":
                    result = string.Format("45 ~ 55 : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "55to64":
                    result = string.Format("55 ~ 65 : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "65to74":
                    result = string.Format("65 ~ 75 : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "75to84":
                    result = string.Format("75 ~ 85 : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                case "over85":
                    result = string.Format(">= 85 : {0} %", value.ToString("N2", CultureInfo.InvariantCulture));
                    break;
                default:
                    result = columnName + " : " + value.ToString("N0", CultureInfo.InvariantCulture);
                    break;
            }

            return result;
        }
    }
}