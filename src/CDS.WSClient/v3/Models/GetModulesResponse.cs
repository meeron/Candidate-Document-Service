using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDS.WSClient.v3.Models
{
    public class GetModulesResponse
    {
        public DateTime CachedTime { get; set; }

        public Module[] Modules { get; set; }
    }
}
