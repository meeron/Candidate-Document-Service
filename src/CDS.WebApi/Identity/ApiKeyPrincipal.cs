using CDS.WSClient.v3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CDS.WebApi.Identity
{
    public class ApiKeyIdentity : ClaimsIdentity
    {
        private const int EMPLOYER_MODULE_ID = 5;

        public ApiKeyIdentity(GetModulesResponse modulesResponse)
        {
            IsEmployerModuleActive = modulesResponse.Modules.Any(x => x.ModuleId == EMPLOYER_MODULE_ID && x.IsActive);
        }

        public override bool IsAuthenticated { get { return true; } }

        public bool IsEmployerModuleActive { get; private set; }

        public override string Name { get { return this.GetType().Name; } }
    }
}
