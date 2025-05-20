using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Models.Hadiths;
using MyQuranWeb.Domain.Models.Prays;
using MyQuranWeb.Library.Options;
using MyQuranWebRepository.Interfaces.Prays;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWebRepository.Prays
{
    public class PrayRepository : IPrayRepository
    {
        private HttpClient _client = new HttpClient(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromSeconds(5)
        });

        private AppSettingOption _appSettingOption;

        public PrayRepository(IOptions<AppSettingOption> appSettingOption)
        {
            _appSettingOption = appSettingOption.Value;
        }

        public async Task<IEnumerable<PrayData>> Get()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_appSettingOption.PrayUrl}");
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<PrayData>>(apiResponse);
                return result;
            }
        }

        public async Task<PrayData> Get(int prayDataId)
        {
            var result = await Get();
            var prayData = result.FirstOrDefault(q => q.ID == prayDataId);

            if (prayData == null) 
            {
                prayData = new PrayData(); 
            }
            return prayData;
        }

        public async Task<ZikrData> GetZikr()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_appSettingOption.ZikrUrl}");
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<ZikrData>(await response.Content.ReadAsStringAsync());
                return result;
            }
        }
    }
}
