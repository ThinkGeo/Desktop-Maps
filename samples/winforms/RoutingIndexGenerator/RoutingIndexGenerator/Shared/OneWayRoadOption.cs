using System;
using System.Text.RegularExpressions;

namespace RoutingIndexGenerator
{
    public class OneWayRoadOption
    {
        private string oneWayColumnName;
        private string oneWayIdentifier;
        private string indicatorColumnName;
        private string startToEnd;
        private string endToStart;

        private Regex startToEndRegex;
        private Regex endToStartRegex;
        private Regex oneWayIdentifierRegex;

        public OneWayRoadOption()
        { }

        public string OneWayColumnName
        {
            get { return oneWayColumnName; }
            set { oneWayColumnName = value; }
        }

        public string IndicatorColumnName
        {
            get { return indicatorColumnName; }
            set { indicatorColumnName = value; }
        }

        public string OneWayIdentifier
        {
            get { return oneWayIdentifier; }
            set { oneWayIdentifier = value; }
        }

        public string StartToEnd
        {
            get { return startToEnd; }
            set { startToEnd = value; }
        }

        public string EndToStart
        {
            get { return endToStart; }
            set { endToStart = value; }
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
            this.endToStartRegex = GetRegex(this.endToStart);
            this.startToEndRegex = GetRegex(this.startToEnd);
            this.oneWayIdentifierRegex = GetRegex(this.oneWayIdentifier);
        }

        public bool MatchEndToStart(string endToStart)
        {
            bool match = false;
            if (endToStartRegex == null)
            {
                match = endToStart.Equals(this.endToStart, StringComparison.InvariantCultureIgnoreCase);
            }
            else
            {
                match = endToStartRegex.Match(endToStart).Success;
            }
            return match;
        }

        public bool MatchStartToEnd(string startToEnd)
        {
            bool match = false;
            if (startToEndRegex == null)
            {
                match = startToEnd.Equals(this.startToEnd, StringComparison.InvariantCultureIgnoreCase);
            }
            else
            {
                match = startToEndRegex.Match(startToEnd).Success;
            }
            return match;
        }

        public bool MatchOneWayIdentifier(string oneWayIdentifier)
        {
            bool match = false;
            if (oneWayIdentifierRegex == null)
            {
                match = oneWayIdentifier.Equals(this.oneWayIdentifier, StringComparison.InvariantCultureIgnoreCase);
            }
            else
            {
                match = oneWayIdentifierRegex.Match(oneWayIdentifier).Success;
            }
            return match;
        }
    }
}
