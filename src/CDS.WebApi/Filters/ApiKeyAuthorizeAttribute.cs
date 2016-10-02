using CDS.WebApi.Identity;
using CDS.WSClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CDS.WebApi.Filters
{
    public class ApiKeyAuthorizeAttribute : Attribute, IFilterFactory
    {
        public bool IsReusable { get { return false; } }

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            var wsClient = serviceProvider.GetService(typeof(WSClient.v3.IWSClient)) as WSClient.v3.IWSClient;
            if (wsClient == null)
                throw new InvalidOperationException(string.Format("Service {0} not found.", typeof(WSClient.v3.IWSClient).FullName));

            return new ApiKeyAuthorizeFilter(wsClient);
        }
    }

    public class ApiKeyAuthorizeFilter : IActionFilter
    {
        private readonly WSClient.v3.IWSClient _wsClient;

        public ApiKeyAuthorizeFilter(WSClient.v3.IWSClient wsClient)
        {
            _wsClient = wsClient;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            string apiKeyText = context.HttpContext.Request.Query["apikey"];
            Guid apiKey = Guid.Empty;

            Guid.TryParse(apiKeyText, out apiKey);

            var modules = _wsClient.GetModules(apiKey);
            context.HttpContext.User.AddIdentity(new ApiKeyIdentity(modules));
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
