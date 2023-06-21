using Microsoft.EntityFrameworkCore;
using MyQuranWeb.Domain.Interfaces;
using MyQuranWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWebRepository
{
    public class AyahRepository : IAyahRepository
    {
        protected readonly MyQuranContext _context;

        public AyahRepository(MyQuranContext context) //: base(context)
        {
            _context = context;
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
            return await _context.Ayahs.Include(q => q.Tafsir).FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Ayah> GetBySurahAndAyahID(int surahID, int ayahID)
        {
            return await _context.Ayahs.FirstOrDefaultAsync(q => q.AyahId == ayahID && q.SurahId == surahID);
        }

        public async Task<IEnumerable<Ayah>> GetBySurahID(int surahID)
        {
            return await _context.Ayahs.Include(q => q.JuzDetail).Where(q => q.SurahId == surahID).ToListAsync();
        }
    }
}
