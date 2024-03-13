using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Interfaces;
using MyQuranWeb.Domain.Models;
using MyQuranWeb.Library.Options;

namespace MyQuranWeb.Pages.Quran
{
    public class FindModel : PageModelCustom
    {
        public IList<Ayah> Ayahs { get; set; } = new List<Ayah>();
        public int? ID { get; set; } = null;

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }
        public string FoundMessage { get; set; }
        //public string MP3URL { get; set; }

        public FindModel(IUnitOfWork unitOfWork, IOptions<AppSettingOption> appSettingOption)
        {
            base.unitOfWork = unitOfWork;
            //MP3URL = unitOfWork.Configuration["AppSettingModel:MP3Url"];
            AppSettingOption = appSettingOption.Value;
        }

        public async Task OnGetAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Search))
                {
                    return;
                }
                var filter = new AyahFilter();
                int searchID = 0;
                int.TryParse(Search, out searchID);

                filter.ID = searchID;
                filter.ReadIndo = Search;
                filter.TranslateIndo = Search;

                Ayahs = (await unitOfWork.Ayahs.Get(filter)).ToList();

                if (!string.IsNullOrWhiteSpace(Search))
                {
                    if (Ayahs.Count > 0)
                    {
                        FoundMessage = $"Ditemukan {(Ayahs.Count() > 0 ? Ayahs.Select(q => q.Surah).Distinct().Count() : 0)} surat & {Ayahs.Count} ayat dengan kata kunci \"{Search}\".";
                    }
                    else
                    {
                        FoundMessage = $"Kata kunci \"{Search}\"tidak ditemukan.";
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        //public async Task<JsonResult> OnGetTafsirAsync(int id)
        //{
        //    try
        //    {
        //        var tafsir = await unitOfWork.Tafsirs.GetByID(id);
        //        if (tafsir != null)
        //        {
        //            if(!string.IsNullOrWhiteSpace(Request.Cookies["tafsirSetting"])
        //                && Request.Cookies["tafsirSetting"] == "1")
        //            {
        //                return new JsonResult(tafsir.AlJalalain);
        //            }

        //            return new JsonResult(tafsir.Kemenag);
        //        }

        //        return new JsonResult("");
        //    }
        //    catch (Exception ex)
        //    {
        //        ErrorMessage = ex.Message;
        //        return new JsonResult("");
        //    }
        //}
    }
}
