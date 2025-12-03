using Microsoft.EntityFrameworkCore;
using MyQuranWebRepository.Interfaces;
using MyQuranWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using MyQuranWeb.Library.Options;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MyQuranWebRepository
{
    public class AyahRepository : IAyahRepository
    {
        private HttpClient _client = new HttpClient(new SocketsHttpHandler()
        { 
            PooledConnectionLifetime = TimeSpan.FromSeconds(5),
        });

        private readonly AppSettingOption _appSettingOption;

        protected readonly MyQuranContext _context;

        public AyahRepository(MyQuranContext context, IOptions<AppSettingOption> appSettingOption) //: base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _appSettingOption = appSettingOption.Value ?? throw new ArgumentNullException(nameof(appSettingOption));
        }

        public async Task<IEnumerable<Ayah>> Get(AyahFilter filter)
        {
            var query = from cr in _context.Ayahs.Include(q => q.Surah).Include(q => q.JuzDetail)
                        select cr;

            query = query.Where(q => q.SurahId == filter.ID || q.AyahId == filter.ID || q.ReadIndo.Contains(filter.ReadIndo) || q.TranslateIndo.Contains(filter.TranslateIndo));
            
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Ayah>> GetAll()
        {
            var query = from cr in _context.Ayahs.Include(q => q.Surah).Include(q => q.JuzDetail)
                        select cr;

            return await query.ToListAsync();
        }

        public async Task<Ayah> GetByID(int id)
        {
            return await _context.Ayahs.Include(q => q.Tafsir).Include(q => q.Surah).Include(q => q.JuzDetail).FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Ayah> GetByIDForPopUp(int id)
        {
            return await _context.Ayahs.FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Ayah> GetBySurahAndAyahID(int surahID, int ayahID)
        {
            return await _context.Ayahs.FirstOrDefaultAsync(q => q.AyahId == ayahID && q.SurahId == surahID);
        }

        public async Task<IEnumerable<Ayah>> GetBySurahID(int surahID)
        {
            return await _context.Ayahs.Include(q => q.JuzDetail).Where(q => q.SurahId == surahID).ToListAsync();
        }

        public List<AyahPage> GetPages()
        {
            var temps = AyahPage.AyahPages;

            var ayahPages = new List<AyahPage>();
            foreach (var item in temps)
            {
                for (int i = item.PageStart; i <= item.PageEnd; i++)
                {
                    ayahPages.Add(new AyahPage { SurahId = item.SurahId, PageStart = i, PageEnd = i });
                }
            }

            return ayahPages;
        }

        public async Task<IEnumerable<AyahPage>> GetPageBySurahId(int surahID)
        {
            //var request = new HttpRequestMessage(HttpMethod.Get, $"{_appSettingOption.QuranPageUrl}/{surahID - 1}");

            //using (var response = await _client.SendAsync(request))
            //{
            //    response.EnsureSuccessStatusCode();

            //    var result = JsonConvert.DeserializeObject<AyahPage>(await response.Content.ReadAsStringAsync());
            //    return result;
            //}

            return await Task.FromResult(GetPages().Where(x => x.SurahId == surahID));
        }

        public async Task<AyahPage> GetPageBySurahAndPageId(int surahId, int pageId)
        {
            return (await GetPageBySurahId(surahId)).FirstOrDefault(x => x.PageStart == pageId);
        }
        public Task<AyahPage> GetPageById(int pageId)
        {
            return Task.FromResult(GetPages().FirstOrDefault(x => x.PageStart == pageId));
        }

        public async Task<string> GetPageUrlBySurahId(int surahID, int pageId)
        {
            var page = await GetPageBySurahAndPageId(surahID, pageId);
            if (page != null)
            {
                //page.PageStart += (pageId - 1);
                return $"{_appSettingOption.QuranPageKemenagUrl}/QK_{page.PageStart:000}.webp";                
            }
            else
            {
                return "";
            }
        }

        public Task<IEnumerable<Ayah>> GetBySurahID(int surahID, int page, int take)
        {
            throw new NotImplementedException();
        }
    }
}
