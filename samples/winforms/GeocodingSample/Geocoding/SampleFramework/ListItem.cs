using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ThinkGeo.MapSuite.Geocoding;

namespace Geocoding
{
    public class ListItem
    {
        private GeocoderMatch matchItem;
        private IEnumerable<string> keyList;

        public ListItem(GeocoderMatch matchItem, IEnumerable<string> keyList)
        {
            this.matchItem = matchItem;
            this.keyList = keyList;
        }

        public GeocoderMatch MatchItem
        {
            get { return matchItem; }
            set { matchItem = value; }
        }

        public override string ToString()
        {
            StringBuilder keyValuesBuilder = new StringBuilder();
            foreach(string item in keyList)
            {
                keyValuesBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0} ", MatchItem.NameValuePairs[item]));
            }

            return keyValuesBuilder.ToString();
        }
    }
}
