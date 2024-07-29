using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Models.Hadiths;
using MyQuranWeb.Library.Options;
using MyQuranWebRepository.Interfaces;
using System.Threading.Tasks;
using System;
using MyQuranWeb.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace MyQuranWeb.Pages.Others
{
    public class AsmaulHusnaDetailModel : PageModelCustom
    {
        [BindProperty(SupportsGet = true)]
        public int? Number { get; set; } = null;

        public List<AsmaulHusna> AsmaulHusnas { get; set; }

        public AsmaulHusnaDetailModel(IUnitOfWork unitOfWork, IOptions<AppSettingOption> appSettingOption)
        {
            this.unitOfWork = unitOfWork;
            AppSettingOption = appSettingOption.Value;
        }

        private async Task RefreshData()
        {
            try
            {
                AsmaulHusnas = (await unitOfWork.AsmaulHusnas.Get()).ToList();
                if (AsmaulHusnas == null)
                {
                    throw new Exception("Asmaul husna tidak ditemukan.");
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