using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Models;
using MyQuranWeb.Library.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using MyQuranWeb.Domain.Models.Hadiths;
using MyQuranWebRepository.Interfaces;

namespace MyQuranWeb.Pages.Hadith
{
    public class HadithDetailModel : PageModelCustom
    {
        [BindProperty(SupportsGet = true)]
        public int? PageNumber { get; set; } = null;

        [BindProperty(SupportsGet = true)]
        public int? HadithNumber { get; set; } = null;

        [BindProperty(SupportsGet = true)]
        public string Slug { get; set; }

        public HadithResultAPI HadithResult { get; set; } = new HadithResultAPI();
        public SelectList NarratorList;
        public SelectList HadithList;
        public SelectList PageList;

        public Navigation Navigation
        {
            get
            {
                return new Navigation() { Type = 3, ID = this.PageNumber, Slug = Slug, LastPage = HadithResult.Pagination.TotalPages };
            }
        }

        public HadithDetailModel(IUnitOfWork unitOfWork, IOptions<AppSettingOption> appSettingOption)
        {
            this.unitOfWork = unitOfWork;
            this.AppSettingOption = appSettingOption.Value;
        }

        private async Task RefreshData()
        {
            try
            {
                if (PageNumber.HasValue && PageNumber.Value > 0 && !string.IsNullOrWhiteSpace(Slug))
                {
                    this.HadithResult = (await unitOfWork.Hadiths.Get(Slug, PageNumber.Value, AppSettingOption.LimitPage));
                    if (this.HadithResult == null)
                    {
                        throw new Exception("Hadis tidak ditemukan.");
                    }
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
                var narrators = new SelectList(await unitOfWork.Hadiths.GetNarrator(), nameof(Narrator.Slug), nameof(Narrator.Name));
                NarratorList = narrators;
                HadithList = new SelectList(HadithResult.Items, "Number", "NumberDesc");

                var pages = new Dictionary<int, string>();
                for (int i = 1; i <= HadithResult.Pagination.TotalPages; i++)
                {
                    pages.Add(i, $"Hal. {i}");
                }
                PageList = new SelectList(pages, "Key", "Value");
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
