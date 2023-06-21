using Microsoft.Extensions.Configuration;
using MyQuranWeb.Domain.Interfaces;
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

        public IJuzDetailRepository JuzDetails { get; }
        public IJuzHeaderRepository JuzHeaders { get; }

        public IConfiguration Configuration { get; }
        public UnitOfWork(MyQuranContext myQuranContext,
            IAyahRepository ayahRepository, 
            ISurahRepository surahRepository, 
            ITafsirRepository tafsirRepository, 
            IJuzDetailRepository juzRepository,
            IJuzHeaderRepository juzHeaderRepository,
            IConfiguration configuration)
        {
            this._context = myQuranContext;
            this.Ayahs = ayahRepository;
            this.Surahs = surahRepository;
            this.Tafsirs = tafsirRepository;
            this.JuzDetails = juzRepository;
            this.JuzHeaders = juzHeaderRepository;
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
