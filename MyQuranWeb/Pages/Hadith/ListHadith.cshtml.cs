using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Models;
using MyQuranWeb.Library.Options;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;
using MyQuranWeb.Domain.Models.Hadiths;
using System.Linq;
using MyQuranWebRepository.Interfaces;

namespace MyQuranWeb.Pages.Hadith
{
    public class ListHadithModel : PageModelCustom
    {
        public IList<Narrator> Narrators { get; set; } = new List<Narrator>();

        public bool IsMobileDevice { get; set; } = false;

        public ListHadithModel(IUnitOfWork unitOfWork, IOptions<AppSettingOption> appSettingOption)
        {
            this.unitOfWork = unitOfWork;
            AppSettingOption = appSettingOption.Value;
        }

        public async Task OnGetAsync()
        {
            try
            {                
                Narrators = (await unitOfWork.Hadiths.GetNarrator()).ToList();
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message + ". " + ex.StackTrace;
            }
        }
    }
}
