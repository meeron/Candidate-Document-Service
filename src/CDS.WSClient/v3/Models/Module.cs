using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDS.WSClient.v3.Models
{
    public class Module
    {
        public int ModuleId { get; set; }

        public string Name { get; set; }

        public DateTime CachedTime { get; set; }

        public bool IsActive { get; set; }

        public bool IsVisible { get; set; }
    }
}
