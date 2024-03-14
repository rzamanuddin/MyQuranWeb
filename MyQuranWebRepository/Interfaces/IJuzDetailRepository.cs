using MyQuranWeb.Domain.Models;
using MyQuranWebRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWebRepository.Interfaces
{
    public interface IJuzDetailRepository : IGetRepository<JuzDetail, JuzDetailFilter>
    {
        Task<IEnumerable<JuzDetail>> GetByJuzID(int juzID);
        Task<JuzDetail> GetByJuzSurahAndAyahID(int juzID, int surahID, int ayahID);
    }
}
