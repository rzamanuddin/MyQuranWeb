using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Models;
using MyQuranWeb.Domain.Models.Hadiths;
using MyQuranWeb.Library.Options;
using MyQuranWebRepository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWebRepository
{
    public class AsmaulHusnaRepository : IAsmaulHusnaRepository
    {
        private HttpClient _client = new HttpClient(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(2)
        });


        private AppSettingOption _appSettingOption;

        public AsmaulHusnaRepository(IOptions<AppSettingOption> appSettingOption)
        {
            _appSettingOption = appSettingOption.Value;
        }
        public async Task<AsmaulHusnaAPIResult> Get()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_appSettingOption.BaseUrl}/husna/all");
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AsmaulHusnaAPIResult>(apiResponse);
                return result;
            }
        }
    }
}
