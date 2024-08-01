using Microsoft.Extensions.Configuration;
using MyQuranWebRepository.Interfaces;
using MyQuranWebRepository.Interfaces.Hadiths;
using MyQuranWebRepository.Interfaces.Prays;
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
        public IAsmaulHusnaRepository AsmaulHusnas { get; }
        public IPrayRepository Prays { get; }

        public IConfiguration Configuration { get; }
        public UnitOfWork(MyQuranContext myQuranContext,
            IAyahRepository ayahRepository, 
            ISurahRepository surahRepository, 
            ITafsirRepository tafsirRepository, 
            ITafsirNewRepository tafsirNewRepository,
            IJuzDetailRepository juzRepository,
            IJuzHeaderRepository juzHeaderRepository,
            IHadithRepository hadithsRepository,
            IAsmaulHusnaRepository asmaulHusnasRepository,
            IPrayRepository prayRepository,
            IConfiguration configuration)
        {
            this._context = myQuranContext ?? throw new ArgumentNullException(nameof(myQuranContext));
            this.Ayahs = ayahRepository ?? throw new ArgumentNullException(nameof(ayahRepository)); ;
            this.Surahs = surahRepository;
            this.Tafsirs = tafsirRepository;
            this.TafsirsNew = tafsirNewRepository;
            this.JuzDetails = juzRepository;
            this.JuzHeaders = juzHeaderRepository;
            this.Hadiths = hadithsRepository;
            this.AsmaulHusnas = asmaulHusnasRepository;
            this.Prays = prayRepository;
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
