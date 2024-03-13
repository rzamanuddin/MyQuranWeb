using MyQuranWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Interfaces
{
    public interface ITafsirRepository : IGetRepository<Tafsir, TafsirFilter>
    {
        Task<IEnumerable<Tafsir>> GetBySurahID(int surahID);
        Task<Tafsir> GetBySurahAndAyahID(int surahID, int ayahID);
    }

    public interface ITafsirNewRepository
    {
        Task<TafsirApiResult> GetBySurahID(int surahID);
        Task<AyahNew> GetBySurahAndAyahID(int surahID, int ayahID);
    }
}
