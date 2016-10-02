using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CDS.WSClient.v3.Models;
using Newtonsoft.Json;

namespace CDS.WSClient.v3.Impl
{
    internal class WSClient: IWSClient
    {
        private readonly HttpClient _httpClient;

        internal WSClient(Uri endpoint)
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = endpoint;
        }

        public void Dispose()
        {
            _httpClient.Dispose();
        }

        public GetModulesResponse GetModules(Guid apiKey)
        {
            string url = string.Format("/api/Module/GetModules?apiKey={0}", apiKey.ToString().ToUpper());

            var response = _httpClient.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                string jsonContent = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<GetModulesResponse>(jsonContent);
            }

            throw new HttpRequestException(response.StatusCode.ToString());
        }
    }
}
