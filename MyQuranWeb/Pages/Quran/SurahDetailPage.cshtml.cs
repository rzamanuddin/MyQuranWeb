using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Models;
using MyQuranWeb.Library.Options;
using MyQuranWebRepository.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyQuranWeb.Pages.Quran
{
    public class SurahDetailPageModel : PageModelCustom
    {
        public string PageUrl { get; set; }
        public AyahPage AyahPage { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; } = null;

        [BindProperty(SupportsGet = true)]
        public int? PageId { get; set; }
        public Navigation Navigation
        {
            get
            {
                return new Navigation() { Type = 5, ID = this.Id, AyahPageId = PageId };
            }
        }

        public SurahDetailPageModel(IUnitOfWork unitOfWork, IOptions<AppSettingOption> appSettingOption)
        {
            this.unitOfWork = unitOfWork;
            this.AppSettingOption = appSettingOption.Value;
            this.PageTypeId = 2;
        }

        private async Task RefreshData()
        {
            try
            {
                if (!Id.HasValue)
                {
                    Id = 1;
                    PageId = 1;
                }
                else
                {
                    if (!PageId.HasValue)
                    {
                        AyahPage = (await this.unitOfWork.Ayahs.GetPageBySurahId(Id.Value)).FirstOrDefault();
                        if (AyahPage != null)
                        {
                            PageId = AyahPage.PageStart;
                            Id = AyahPage.SurahId;
                            PageUrl = await unitOfWork.Ayahs.GetPageUrlBySurahId(AyahPage.SurahId, PageId.Value);
                            return;
                        }
                    }
                }

                if (Id.HasValue && Id.Value > 0 && Id.Value <= 115 && PageId.HasValue && PageId.Value > 0 && PageId.Value <= 604)
                {
                    AyahPage = await this.unitOfWork.Ayahs.GetPageById(PageId.Value);
                    if (AyahPage != null)
                    {
                        //PageId = AyahPage.Page;
                        Id = AyahPage.SurahId;
                        PageUrl = await unitOfWork.Ayahs.GetPageUrlBySurahId(AyahPage.SurahId, PageId.Value);                        
                    }
                }
                else
                {
                    throw new Exception("Halaman tidak ditemukan.");
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
