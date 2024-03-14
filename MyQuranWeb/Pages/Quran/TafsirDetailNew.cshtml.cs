using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MyQuranWebRepository.Interfaces;
using MyQuranWeb.Domain.Models;
using MyQuranWeb.Library.Options;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System;

namespace MyQuranWeb.Pages.Quran
{
    public class TafsirDetailNewModel : PageModelCustom
    {
        public TafsirApiResult Tafsirs { get; set; }

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

        public TafsirDetailNewModel(IUnitOfWork unitOfWork, IOptions<AppSettingOption> appSettingOption)
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
                Tafsirs = await unitOfWork.TafsirsNew.GetBySurahID(ID.Value);
                var ayahs = await unitOfWork.Ayahs.GetBySurahID(ID.Value);
                TafsirList = new SelectList(ayahs, nameof(Ayah.AyahId), nameof(Ayah.AyahId));
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
                var ayah = await unitOfWork.Ayahs.GetByIDForPopUp(id);

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
    }
}
