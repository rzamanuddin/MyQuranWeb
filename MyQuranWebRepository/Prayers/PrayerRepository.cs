using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Models;
using MyQuranWeb.Domain.Models.Prayers;
using MyQuranWeb.Library.Options;
using MyQuranWebRepository.Interfaces.Prayers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWebRepository.Prayers
{
    public class PrayerRepository : IPrayerRepository
    {
        private HttpClient _client = new HttpClient(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(2)
        });

        private readonly AppSettingOption _appSettingOption;

        public PrayerRepository(IOptions<AppSettingOption> appSettingOption)
        {
            _appSettingOption = appSettingOption.Value ?? throw new ArgumentNullException(nameof(appSettingOption));
        }

        public async Task<IEnumerable<Intention>> GetIntentions()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_appSettingOption.IntentionUrl}");
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<IEnumerable<Intention>>(await response.Content.ReadAsStringAsync());
                return result;
            }
        }

        public async Task<IEnumerable<PrayerRead>> GetPrayerReads()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_appSettingOption.PrayerReadUrl}");
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<IEnumerable<PrayerRead>>(await response.Content.ReadAsStringAsync());
                return result;
            }
        }

        public async Task<IEnumerable<PrayerZikr>> GetPrayerZikrs()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_appSettingOption.PrayerZikrUrl}");
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<IEnumerable<PrayerZikr>>(await response.Content.ReadAsStringAsync());
                return result;
            }
        }

        public async Task<IEnumerable<PrayerZikr>> GetPrayerZikrs2()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_appSettingOption.PrayerZikrUrl2}");
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var result = JsonConvert.DeserializeObject<IEnumerable<PrayerZikr>>(await response.Content.ReadAsStringAsync());
                return result;
            }
        }
    }
}
