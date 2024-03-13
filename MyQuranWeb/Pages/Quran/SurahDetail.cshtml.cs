using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Interfaces;
using MyQuranWeb.Domain.Models;
using MyQuranWeb.Library.Options;

namespace MyQuranWeb.Pages.Quran
{
    public class SurahDetailModel : PageModelCustom
    {
        public IList<Ayah> Ayahs { get; set; } = new List<Ayah>();
        
        [BindProperty(SupportsGet = true)]
        public int? ID { get; set; } = null;

        [BindProperty(SupportsGet = true)]
        public string AyahID { get;set;}

        //public string MP3URL { get; set; }
        public Surah Surah { get; set; } = new Surah();
        public SelectList SurahList;
        public SelectList AyahList;

        public Navigation Navigation
        {
            get
            {
                return new Navigation() { Type = 0, ID = this.ID };
            }
        }

        public SurahDetailModel(IUnitOfWork unitOfWork, IOptions<AppSettingOption> appSettingOption)
        {
            this.unitOfWork = unitOfWork;
            this.AppSettingOption = appSettingOption.Value;
            //MP3URL = unitOfWork.Configuration["AppSettingModel:MP3Url"];
        }

        private async Task RefreshData()
        {
            try
            {
                if (ID.HasValue && ID.Value > 0)
                {
                    //ID = id.Value;
                    //this.Surah = (await unitOfWork.Surahs.GetAll()).Where(q => q.Id == id.Value).FirstOrDefault();
                    this.Surah = (await unitOfWork.Surahs.GetByID(ID.Value));
                    if (this.Surah == null)
                    {
                        throw new Exception("Surat tidak ditemukan.");
                    }

                    //var filter = new AyahFilter();
                    //if (!string.IsNullOrWhiteSpace(Search))
                    //{
                    //    int ayahID = 0;
                    //    int.TryParse(Search, out ayahID);
                    //    if (ayahID > 0)
                    //    {
                    //        filter.ID = ayahID;
                    //    }
                    //    filter.ReadIndo = Search;
                    //    filter.TranslateIndo = Search;
                    //}
                    //Ayahs = (await unitOfWork.Ayahs.Get(filter)).Where(q => q.SurahId == id.Value).ToList();
                    //Ayahs = (await unitOfWork.Ayahs.GetBySurahID(ID.Value)).ToList();
                }
                else
                {
                    throw new Exception("Surat tidak ditemukan.");
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
                var surahs = new SelectList(await unitOfWork.Surahs.GetAll(), nameof(Surah.Id), nameof(Surah.HeaderOnly));
                SurahList = surahs;
                Ayahs = (await unitOfWork.Ayahs.GetBySurahID(ID.Value)).ToList();
                AyahList = new SelectList(Ayahs, nameof(Ayah.AyahId), nameof(Ayah.AyahId));
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

        //public async Task<JsonResult> OnGetTafsirAsync(int id)
        //{
        //    try
        //    {
        //        var tafsir = await unitOfWork.Tafsirs.GetByID(id);
        //        if (tafsir != null)
        //        {
        //            if (!string.IsNullOrWhiteSpace(Request.Cookies["tafsirSetting"])
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

        //public async Task<JsonResult> OnGetTafsirAsync(int surahId, int ayahId)
        //{
        //    try
        //    {
        //        var tafsir = await unitOfWork.TafsirsNew.GetBySurahAndAyahID(surahId, ayahId);
        //        if (tafsir != null)
        //        {
        //            if (!string.IsNullOrWhiteSpace(Request.Cookies["tafsirSetting"])
        //                && Request.Cookies["tafsirSetting"] == "1")
        //            {
        //                return new JsonResult(tafsir.Data.AlJalalain);
        //            }

        //            return new JsonResult(tafsir.Data.Kemenag.ToString());
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
