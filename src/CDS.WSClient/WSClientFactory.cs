using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDS.WSClient
{
    public static class WSClientFactory
    {
        public static v3.IWSClient CreateV3(string endpointUri)
        {
            return new v3.Impl.WSClient(new Uri(endpointUri));
        }
    }
}
