using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Models.Prays;
using MyQuranWeb.Library.Options;
using MyQuranWebRepository.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyQuranWeb.Pages.Prays
{
    public class ListPrayModel : PageModelCustom
    {
        public IList<PrayData> Prays { get; set; } = new List<PrayData>();


        public ListPrayModel(IUnitOfWork unitOfWork, IOptions<AppSettingOption> appSettingOption)
        {
            this.unitOfWork = unitOfWork;
            AppSettingOption = appSettingOption.Value;
        }

        public async Task OnGetAsync()
        {
            try
            {
                Prays = (await unitOfWork.Prays.Get()).ToList();
            }

            catch (Exception ex)
            {
                ErrorMessage = ex.Message + ". " + ex.StackTrace;
            }
        }
    }
}
