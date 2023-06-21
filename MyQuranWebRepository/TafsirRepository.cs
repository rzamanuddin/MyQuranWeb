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
