using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IAyahRepository Ayahs { get; }
        ISurahRepository Surahs { get; }
        ITafsirRepository Tafsirs { get; }

        IJuzDetailRepository JuzDetails { get; }

        IJuzHeaderRepository JuzHeaders { get; }

        IConfiguration Configuration { get; }
    }
}
