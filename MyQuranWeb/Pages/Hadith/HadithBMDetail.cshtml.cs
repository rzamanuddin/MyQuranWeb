using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Models.Hadiths;
using MyQuranWeb.Library.Options;
using System.Threading.Tasks;
using System;
using MyQuranWeb.Domain.Models;
using System.Collections.Generic;
using MyQuranWebRepository.Interfaces;

namespace MyQuranWeb.Pages.Hadith
{
    public class HadithBMDetailModel : PageModelCustom
    {
        [BindProperty(SupportsGet = true)]
        public int? PageNumber { get; set; } = null;

        [BindProperty(SupportsGet = true)]
        public int? HadithNumber { get; set; } = null;

        public HadithBMAPIResult HadithResult { get; set; } = new HadithBMAPIResult();
        public List<HadithBM> HadithBMs { get; set; } = new List<HadithBM>();
        public SelectList HadithList;
        public SelectList PageList;

        public Navigation Navigation
        {
            get
            {
                return new Navigation() { Type = 4, ID = this.PageNumber, LastPage = HadithResult.Info.Max / AppSettingOption.LimitPage };
            }
        }

        public HadithBMDetailModel(IUnitOfWork unitOfWork, IOptions<AppSettingOption> appSettingOption)
        {
            this.unitOfWork = unitOfWork;
            this.AppSettingOption = appSettingOption.Value;
        }

        private async Task RefreshData()
        {
            try
            {
                if (PageNumber.HasValue && PageNumber.Value > 0)
                {
                    if (PageNumber > 79)
                    {
                        throw new Exception("Hadis tidak ditemukan.");
                    }
                    int start = PageNumber.Value == 1 ? 1 : (PageNumber.Value * AppSettingOption.LimitPage) + 1;
                    var length = start + AppSettingOption.LimitPage - 1;
                    if (length > 1598)
                    {
                        length = 1597;
                    }

                    for (int i = start; i <= length; i++)
                    {
                        this.HadithResult = (await unitOfWork.Hadiths.GetBulughulMaram(i));
                        if (this.HadithResult == null)
                        {
                            throw new Exception("Hadis tidak ditemukan.");
                        }
                        HadithBMs.Add(HadithResult.Data);
                    }

                    await LoadData();
                }
                else
                {
                    throw new Exception("Hadis tidak ditemukan.");
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
                var pages = new Dictionary<int, string>();
                //var length = Convert.ToInt32(Math.Round(Convert.ToDouble(HadithResult.Info.Max) / Convert.ToDouble(AppSettingOption.LimitPage), 0));
                var length = HadithResult.Info.Max / AppSettingOption.LimitPage;
                for (int i = 1; i <= length; i++)
                {
                    pages.Add(i, $"Hal. {i}");
                }
                PageList = new SelectList(pages, "Key", "Value");

                var numbers = new Dictionary<int, string>();
                int start = PageNumber.Value == 1 ? 1 : (PageNumber.Value * AppSettingOption.LimitPage) + 1;
                length = start - 1 + AppSettingOption.LimitPage;
                if (length > HadithResult.Info.Max)
                {
                    length = HadithResult.Info.Max;
                }
                for (int i = start; i <= length; i++)
                {
                    numbers.Add(i, $"No. {i}");
                }
                HadithList = new SelectList(numbers, "Key", "Value");
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
