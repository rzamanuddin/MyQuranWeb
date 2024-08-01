using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Models.Hadiths;
using MyQuranWeb.Domain.Models.Prays;
using MyQuranWeb.Library.Options;
using MyQuranWebRepository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyQuranWeb.Pages.Prays
{
    public class PrayDetailModel : PageModelCustom
    {
        [BindProperty(SupportsGet = true)]
        public int? PrayDataId { get; set; } = null;
        [BindProperty(SupportsGet = true)]
        public int? PrayDetailId { get; set; } = null;

        public PrayData PrayData { get; set; } = new();
        public SelectList PrayList;
        public SelectList PrayDetailList;

        public PrayDetailModel(IUnitOfWork unitOfOfWork, IOptions<AppSettingOption> appSettingOption)
        {
            this.unitOfWork = unitOfOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.AppSettingOption = appSettingOption.Value ?? throw new ArgumentNullException(nameof(appSettingOption));
        }

        private async Task LoadData()
        {
            try
            {
                var prays = new SelectList(await unitOfWork.Prays.Get(), nameof(PrayData.ID), nameof(PrayData.Description));
                PrayList = prays;

                if (PrayDataId.HasValue)
                {
                    var prayData = await unitOfWork.Prays.Get(PrayDataId.Value);
                    PrayDetailList = new SelectList(prayData.Data, nameof(Pray.ID), nameof(Pray.ID));
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private async Task RefreshData()
        {
            try
            {
                if (PrayDataId.HasValue)
                {
                    PrayData = await unitOfWork.Prays.Get(PrayDataId.Value);
                }
                else
                {
                    throw new Exception("Doa tidak ditemukan.");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public async Task OnGetAsync()
        {
            await LoadData();
            await RefreshData();
        }
    }
}
