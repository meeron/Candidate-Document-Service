using CDS.WSClient.v3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDS.WSClient.v3
{
    public interface IWSClient: IDisposable
    {
        GetModulesResponse GetModules(Guid apiKey);
    }
}
