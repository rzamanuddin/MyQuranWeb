using Microsoft.Extensions.Configuration;
using MyQuranWeb.Domain.Interfaces;
using MyQuranWeb.Domain.Interfaces.Hadiths;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Models
{
    public class UnitOfWork : IUnitOfWork
    {
        private MyQuranContext _context;
        public IAyahRepository Ayahs { get; }
        public ISurahRepository Surahs { get; }
        public ITafsirRepository Tafsirs { get;  }
        public ITafsirNewRepository TafsirsNew { get; }
        public IJuzDetailRepository JuzDetails { get; }
        public IJuzHeaderRepository JuzHeaders { get; }
        public IHadithRepository Hadiths { get; }

        public IConfiguration Configuration { get; }
        public UnitOfWork(MyQuranContext myQuranContext,
            IAyahRepository ayahRepository, 
            ISurahRepository surahRepository, 
            ITafsirRepository tafsirRepository, 
            ITafsirNewRepository tafsirNewRepository,
            IJuzDetailRepository juzRepository,
            IJuzHeaderRepository juzHeaderRepository,
            IHadithRepository hadithsRepository,
            IConfiguration configuration)
        {
            this._context = myQuranContext;
            this.Ayahs = ayahRepository;
            this.Surahs = surahRepository;
            this.Tafsirs = tafsirRepository;
            this.TafsirsNew = tafsirNewRepository;
            this.JuzDetails = juzRepository;
            this.JuzHeaders = juzHeaderRepository;
            this.Hadiths = hadithsRepository;
            this.Configuration = configuration;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
