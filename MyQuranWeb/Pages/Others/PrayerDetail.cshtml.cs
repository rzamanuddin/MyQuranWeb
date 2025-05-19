using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Models.Prayers;
using MyQuranWeb.Library.Options;
using MyQuranWebRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyQuranWeb.Pages.Others
{
    public class PrayerDetailModel : PageModelCustom
    {
        [BindProperty(SupportsGet = true)]
        public int? Number { get; set; } = null;

        public List<Intention> Intentions { get; set; }
        public List<PrayerRead> PrayerReads { get; set; }
        public List<PrayerZikr> PrayerZikrs { get; set; }
        public List<PrayerZikr> PrayerZikrs2 { get; set; }

        public PrayerDetailModel(IUnitOfWork unitOfWork, IOptions<AppSettingOption> appSettingOption)
        {
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            AppSettingOption = appSettingOption.Value ?? throw new ArgumentNullException(nameof(appSettingOption));
        }

        public async Task RefreshData()
        {
            try
            {
                Intentions = (await unitOfWork.Prayers.GetIntentions()).ToList();
                if (Intentions == null)
                {
                    throw new Exception("Niat sholat tidak ditemukan.");
                }

                PrayerReads = (await unitOfWork.Prayers.GetPrayerReads()).ToList();
                if (PrayerReads == null)
                {
                    throw new Exception("Bacaan sholat tidak ditemukan.");
                }

                PrayerZikrs = (await unitOfWork.Prayers.GetPrayerZikrs()).ToList();
                if (PrayerZikrs == null)
                {
                    throw new Exception("Zikr setelah sholat 1 tidak ditemukan.");
                }

                PrayerZikrs2 = (await unitOfWork.Prayers.GetPrayerZikrs2()).ToList();
                if (PrayerZikrs2 == null)
                {
                    throw new Exception("Zikir setelah sholat 2 tidak ditemukan.");
                }
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
