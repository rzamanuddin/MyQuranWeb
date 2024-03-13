using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Interfaces.Hadiths;
using MyQuranWeb.Domain.Models;
using MyQuranWeb.Domain.Models.Hadiths;
using MyQuranWeb.Library.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWebRepository.Hadiths
{
    public class HadithRepository : IHadithRepository
    {
        private HttpClient _client = new HttpClient(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(2)
        });

        private AppSettingOption _appSettingOption;

        public HadithRepository(IOptions<AppSettingOption> appSettingOption)
        {
            _appSettingOption = appSettingOption.Value;
        }

        public async Task<HadithResultAPI> Get(string slug, int page, int limit)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_appSettingOption.HadithUrl}/hadith/{slug}?page={page}&limit={limit}");
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<HadithResultAPI>(apiResponse);
                return result;
            }
        }

        public async Task<HadithArbainAPIResult> GetArbain()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_appSettingOption.BaseUrl}/hadits/arbain/all");
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<HadithArbainAPIResult>(apiResponse);
                return result;
            }
        }

        public async Task<HadithBMAPIResult> GetBulughulMaram(int number)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_appSettingOption.BaseUrl}/hadits/bm/{number}");
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<HadithBMAPIResult>(apiResponse);
                return result;
            }
        }

        public async Task<IEnumerable<Narrator>> GetNarrator()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_appSettingOption.HadithUrl}/hadith");
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<Narrator>>(apiResponse);
                return result;
            }
        }
    }
}
