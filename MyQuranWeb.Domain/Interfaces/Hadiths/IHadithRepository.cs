using MyQuranWeb.Domain.Models;
using MyQuranWeb.Domain.Models.Hadiths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Interfaces.Hadiths
{
    public interface IHadithRepository
    {
        Task<IEnumerable<Narrator>> GetNarrator();
        Task<HadithResultAPI> Get(string slug, int page, int limit);
        Task<HadithArbainAPIResult> GetArbain();
        Task<HadithBMAPIResult> GetBulughulMaram(int number);
        //Task<Ayah> GetBySurahAndAyahID(int surahID, int ayahID);
        //Task<Hadith> GetByID(int surahID);
    }
}
