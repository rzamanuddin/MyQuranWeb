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
    public class JuzDetailRepository : IJuzDetailRepository
    {
        private MyQuranContext _context;

        public JuzDetailRepository(MyQuranContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<JuzDetail>> Get(JuzDetailFilter filter)
        {
            var query = from q in _context.JuzDetails
                        select q;

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<JuzDetail>> GetAll()
        {
            var query = from q in _context.JuzDetails
                        select q;

            return await query.ToListAsync();
        }

        public async Task<JuzDetail> GetByID(int id)
        {
            return await _context.JuzDetails.FindAsync(id);
        }

        public async Task<JuzDetail> GetByJuzSurahAndAyahID(int juzID, int surahID, int ayahID)
        {
            return await _context.JuzDetails.FirstOrDefaultAsync(q => q.SurahId == surahID && q.AyahId == ayahID);
        }

        public async Task<IEnumerable<JuzDetail>> GetByJuzID(int juzID)
        {
            return await _context.JuzDetails.Include(q => q.Juz).Include(q => q.Ayah).ThenInclude(q => q.Surah)
                .Where(q => q.JuzId == juzID).ToListAsync();
        }
    }
}
