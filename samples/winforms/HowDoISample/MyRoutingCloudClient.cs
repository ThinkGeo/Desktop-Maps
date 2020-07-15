using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThinkGeo.Core;

namespace ThinkGeo.UI.WinForms.HowDoI
{
    class MyRoutingCloudClient :RoutingCloudClient
    {
        public MyRoutingCloudClient(string clientId, string clientSecret)
            : base(clientId, clientSecret)
        { }

        protected override string GetNextCandidateBaseUriCore()
        {
            //return base.GetNextCandidateBaseUriCore();
            return "http://54.153.114.36/";
        }
    }
}
