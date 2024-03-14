using Microsoft.EntityFrameworkCore;
using MyQuranWeb.Domain.Models;
using MyQuranWebRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWebRepository
{
    public class SurahRepository : ISurahRepository
    {
        protected MyQuranContext _context;

        public SurahRepository(MyQuranContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Surah>> GetAll()
        {
            var query = from s in _context.Surahs
                        select s;

            return await query.ToListAsync();
        }

        public async Task<Surah> GetByID(int id)
        {
            return await _context.Surahs.FindAsync(id);
        }
    }
}
