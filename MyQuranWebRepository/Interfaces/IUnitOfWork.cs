using Microsoft.Extensions.Configuration;
using MyQuranWebRepository.Interfaces.Hadiths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWebRepository.Interfaces
{
    public interface IUnitOfWork
    {
        IAyahRepository Ayahs { get; }
        ISurahRepository Surahs { get; }
        ITafsirRepository Tafsirs { get; }
        ITafsirNewRepository TafsirsNew { get; }
        IHadithRepository Hadiths { get; }
        IJuzDetailRepository JuzDetails { get; }
        IJuzHeaderRepository JuzHeaders { get; }
        IConfiguration Configuration { get; }
        IAsmaulHusnaRepository AsmaulHusnas { get; }
    }
}
