using MyQuranWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Interfaces
{
    public interface IJuzDetailRepository : IGetRepository<JuzDetail, JuzDetailFilter>
    {
        Task<IEnumerable<JuzDetail>> GetByJuzID(int juzID);
        Task<JuzDetail> GetByJuzSurahAndAyahID(int juzID, int surahID, int ayahID);
    }
}
