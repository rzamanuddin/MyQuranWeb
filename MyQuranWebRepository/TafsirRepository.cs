using Castle.Core.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Interfaces;
using MyQuranWeb.Domain.Models;
using MyQuranWeb.Library.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWebRepository
{
    public class TafsirNewRepository : ITafsirNewRepository
    {
        private HttpClient _client = new HttpClient(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(2)
        });

        private AppSettingOption _appSettingOption;

        public TafsirNewRepository(IOptions<AppSettingOption> appSettingOption) 
        {
            _appSettingOption = appSettingOption.Value;
        }

        public async Task<AyahNew> GetBySurahAndAyahID(int surahID, int ayahID)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, ($"{_appSettingOption.TafsirUrl}/surahs/{surahID}/ayahs/{ayahID}"));
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var apiResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<AyahNew>(apiResponse);
                return result;
            }
        }

        public async Task<TafsirApiResult> GetBySurahID(int surahID)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, ($"{_appSettingOption.TafsirUrl}/surahs/{surahID}"));
            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var apiResponse =  await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TafsirApiResult>(apiResponse);
                return result;
            }
        }
    }
    public class TafsirRepository : ITafsirRepository
    {
        private MyQuranContext _context;

        public TafsirRepository(MyQuranContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tafsir>> Get(TafsirFilter filter)
        {
            var query = from t in _context.Tafsirs
                        select t;
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Tafsir>> GetAll()
        {
            var query = from t in _context.Tafsirs
                       select t;

            return await query.ToListAsync();
        }

        public async Task<Tafsir> GetByID(int id)
        {
            return await _context.Tafsirs.FindAsync(id);
        }

        public async Task<Tafsir> GetBySurahAndAyahID(int surahID, int ayahID)
        {
            return await _context.Tafsirs.FirstOrDefaultAsync(q => q.AyahId == ayahID && q.SurahId == surahID);
        }

        public async Task<IEnumerable<Tafsir>> GetBySurahID(int surahID)
        {
            return await _context.Tafsirs.Include(q => q.Ayah).ThenInclude(q => q.JuzDetail)
                .Where(q => q.SurahId == surahID).ToListAsync();
        }
    }
}
