using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Models;
using MyQuranWeb.Domain.Models.Prays;
using MyQuranWeb.Library.Options;
using MyQuranWebRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyQuranWeb.Pages.Prays
{
    public class ZikrDetailModel : PageModelCustom
    {
        [BindProperty(SupportsGet = true)]
        public int? Number { get; set; }

        public ZikrData Zikrs { get; set; }
        public List<Zikr> ZikrMornings { get; set; } = new List<Zikr>();
        public List<Zikr> ZikrAfterNoons { get; set; } = new List<Zikr>();
        public List<Ayah> Ayahs { get; set; } = new List<Ayah>();

        public ZikrDetailModel(IUnitOfWork unitOfWork, IOptions<AppSettingOption> appSettingOption)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            AppSettingOption = appSettingOption.Value ?? throw new ArgumentNullException(nameof(appSettingOption));
        }

        public async Task RefreshData()
        {
            try
            {
                Zikrs = await unitOfWork.Prays.GetZikr();
                if (Zikrs.Data.Count == 0)
                {
                    throw new Exception("Zikir pagi/petang tidak ditemukan");
                }

                ZikrMornings = Zikrs.Data.Where(x => string.IsNullOrWhiteSpace(x.Time) || x.Time.Equals("pagi", StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (ZikrMornings == null || ZikrMornings.Count == 0)
                {
                    throw new Exception("Zikir pagi tidak ditemukan");
                }

                ZikrAfterNoons = Zikrs.Data.Where(x => string.IsNullOrWhiteSpace(x.Time) || x.Time.Equals("petang", StringComparison.InvariantCultureIgnoreCase)).ToList();
                if (ZikrAfterNoons == null || ZikrAfterNoons.Count == 0)
                {
                    throw new Exception("Zikir petang tidak ditemukan");
                }

                var alIkhlas = (await unitOfWork.Ayahs.GetBySurahID(112));
                var alFalaq = (await unitOfWork.Ayahs.GetBySurahID(113));
                var anNaas = (await unitOfWork.Ayahs.GetBySurahID(114));

                var bismillah = await unitOfWork.Ayahs.GetBySurahAndAyahID(1, 1);
                Ayahs.Add(bismillah);
                Ayahs.AddRange(alIkhlas);
                Ayahs.Add(bismillah);
                Ayahs.AddRange(alFalaq);
                Ayahs.Add(bismillah);
                Ayahs.AddRange(anNaas);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public async Task OnGetAsync()
        {
            await RefreshData();
        }
    }
}
