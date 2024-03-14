using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Models.Hadiths;
using MyQuranWeb.Library.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MyQuranWebRepository.Interfaces;

namespace MyQuranWeb.Pages.Hadith
{
    public class HadithArbainDetailModel : PageModelCustom
    {
        [BindProperty(SupportsGet = true)]
        public int? HadithNumber { get; set; } = null;

        public HadithArbainAPIResult HadithResult { get; set; }
        public SelectList HadithList;

        public HadithArbainDetailModel(IUnitOfWork unitOfWork, IOptions<AppSettingOption> appSettingOption)
        {
            this.unitOfWork = unitOfWork;
            this.AppSettingOption = appSettingOption.Value;
        }

        private async Task RefreshData()
        {
            try
            {
                this.HadithResult = await unitOfWork.Hadiths.GetArbain();
                if (this.HadithResult == null)
                {
                    throw new Exception("Hadis arbain tidak ditemukan.");
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private async Task LoadData()
        {
            try
            {
                HadithList = new SelectList(HadithResult.Data, nameof(HadithArbain.No), nameof(HadithArbain.No));
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public async Task OnGetAsync()
        {
            await RefreshData();
            await LoadData();
        }
    }
}