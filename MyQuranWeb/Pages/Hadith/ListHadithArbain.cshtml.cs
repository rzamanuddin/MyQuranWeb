using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Models.Hadiths;
using MyQuranWeb.Library.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using MyQuranWebRepository.Interfaces;

namespace MyQuranWeb.Pages.Hadith
{
    public class ListHadithArbainModel : PageModelCustom
    {
        public HadithArbainAPIResult Hadiths { get; set; }

        public bool IsMobileDevice { get; set; } = false;

        public ListHadithArbainModel(IUnitOfWork unitOfWork, IOptions<AppSettingOption> appSettingOption)
        {
            this.unitOfWork = unitOfWork;
            AppSettingOption = appSettingOption.Value;
        }

        public async Task OnGetAsync()
        {
            try
            {
                Hadiths = (await unitOfWork.Hadiths.GetArbain());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message + ". " + ex.StackTrace;
            }
        }
    }
}
