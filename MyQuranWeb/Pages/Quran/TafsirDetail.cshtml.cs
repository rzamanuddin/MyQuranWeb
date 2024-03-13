using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Interfaces;
using MyQuranWeb.Domain.Models;
using MyQuranWeb.Library.Options;
using Newtonsoft.Json;

namespace MyQuranWeb.Pages.Quran
{
    public class TafsirDetailModel : PageModelCustom
    {
        public IList<Tafsir> Tafsirs { get; set; } = new List<Tafsir>();

        [BindProperty(SupportsGet = true)]
        public int? ID { get; set; } = null;

        [BindProperty(SupportsGet = true)]
        public string TafsirID { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }
        public SelectList SurahList;
        public SelectList TafsirList;

        public Navigation Navigation
        {
            get
            {
                return new Navigation() { Type = 2, ID = this.ID };
            }
        }

        public TafsirDetailModel(IUnitOfWork unitOfWork, IOptions<AppSettingOption> appSettingOption)
        {
            this.unitOfWork = unitOfWork;
            AppSettingOption = appSettingOption.Value;
        }

        public Surah Surah { get; set; } = new Surah();

        public async Task OnGetAsync()
        {
            await LoadData();
            await RefreshData();
        }

        private async Task RefreshData()
        {
            try
            {
                if (ID.HasValue && ID.Value > 0)
                {
                    this.Surah = (await unitOfWork.Surahs.GetByID(ID.Value));
                    //Tafsirs = (await unitOfWork.Tafsirs.GetBySurahID(ID.Value)).ToList();
                    if (Surah == null)
                    {
                        throw new Exception("Tafsir tidak ditemukan.");
                    }
                }
                else
                {
                    throw new Exception("Tafsir tidak ditemukan.");
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
                Tafsirs = (await unitOfWork.Tafsirs.GetBySurahID(ID.Value)).ToList();
                TafsirList = new SelectList(Tafsirs, nameof(Tafsir.AyahId), nameof(Ayah.AyahId));
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public async Task<JsonResult> OnGetAyahAsync(int id)
        {
            try
            {
                var ayah = await unitOfWork.Ayahs.GetByID(id);

                var result = JsonConvert.SerializeObject(ayah, Formatting.Indented,
                    new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                if (result != null)
                {
                    return new JsonResult(result)
                    {
                        StatusCode = (int)HttpStatusCode.OK
                    };
                }
                else
                {
                    return new JsonResult("")
                    {
                        StatusCode = (int)HttpStatusCode.NotFound
                    };
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return new JsonResult(ex.Message) { StatusCode = (int)HttpStatusCode.InternalServerError };
            }
        }

        public async Task<JsonResult> OnGetTafsirAsync(int surahId, int ayahId)
        {
            try
            {
                var tafsir = await unitOfWork.TafsirsNew.GetBySurahAndAyahID(surahId, ayahId);
                if (tafsir != null)
                {
                    if (!string.IsNullOrWhiteSpace(Request.Cookies["tafsirSetting"])
                        && Request.Cookies["tafsirSetting"] == "1")
                    {
                        return new JsonResult(tafsir.Data.AlJalalain);
                    }

                    return new JsonResult(tafsir.Data.Kemenag.ToString());
                }

                return new JsonResult("");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return new JsonResult("");
            }
        }
    }
}
