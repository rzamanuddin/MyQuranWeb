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
    public class JuzHeaderRepository : IJuzHeaderRepository
    {
        private MyQuranContext _context;

        public JuzHeaderRepository(MyQuranContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<JuzHeader>> Get(JuzHeaderFilter filter)
        {
            var query = from q in _context.JuzHeaders
                        select q;
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<JuzHeader>> GetAll()
        {
            var query = from q in _context.JuzHeaders
                        select q;
            return await query.ToListAsync();
        }

        public async Task<JuzHeader> GetByID(int id)
        {
            return await _context.FindAsync<JuzHeader>(id);
        }
    }
}
