
using System;
using System.Text.RegularExpressions;

namespace RoutingIndexGenerator
{
    public class ClosedRoadOption
    {
        private string closedRoadValue;
        private string closedColumnName;
        private Regex closedRoadValueRegex;

        public string ClosedColumnName
        {
            get { return closedColumnName; }
            set { closedColumnName = value; }
        }

        public string ClosedRoadValue
        {
            get { return closedRoadValue; }
            set { closedRoadValue = value; }
        }

        private Regex GetRegex(string stringToMatch)
        {
            if (String.IsNullOrEmpty(stringToMatch))
            {
                return null;
            }

            Regex regex = null;

            if (stringToMatch.StartsWith("[") && stringToMatch.EndsWith("]"))
            {
                string regexString = stringToMatch.Substring(1, stringToMatch.Length - 2);
                regex = new Regex(regexString, RegexOptions.Compiled);
            }

            return regex;
        }

        public void InitRegexes()
        {
            this.closedRoadValueRegex = GetRegex(this.closedRoadValue);
        }

        public bool MatchClosedRoadValue(string closedRoadValue)
        {
            bool match = false;
            if (closedRoadValue == null)
            {
                match = closedRoadValue.Equals(this.closedRoadValue, StringComparison.InvariantCultureIgnoreCase);
            }
            else
            {
                match = closedRoadValueRegex.Match(closedRoadValue).Success;
            }
            return match;
        }

    }
}
