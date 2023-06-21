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
using MyQuranWeb.Options;

namespace MyQuranWeb.Pages.Quran
{
    public class JuzDetailModel : PageModelCustom
    {
        public IList<JuzDetail> JuzDetails { get; set; } = new List<JuzDetail>();

        [BindProperty(SupportsGet = true)]
        public int? ID { get; set; } = null;
        
        [BindProperty(SupportsGet = true)]
        public int? JuzDetailID { get; set; } = null;

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }
        public Navigation Navigation { 
            get
            {
                return new Navigation() { Type = 1, ID = this.ID };
            }
        }
        public SelectList JuzList;
        public SelectList JuzDetailList;

        public JuzDetailModel(IUnitOfWork unitOfWork, IOptions<AppSettingOption> appSettingOption)
        {
            base.unitOfWork = unitOfWork;
            AppSettingOption = appSettingOption.Value;
            //MP3URL = unitOfWork.Configuration["AppSettingModel:MP3Url"];
        }

        public JuzHeader JuzHeader { get; set; } = new JuzHeader();
        //public string MP3URL { get; set; }

        private async Task LoadData()
        {
            try
            {
                JuzList = new SelectList(await unitOfWork.JuzHeaders.GetAll(), nameof(JuzHeader.Id), nameof(JuzHeader.Id));
                JuzDetailList = new SelectList(await unitOfWork.JuzDetails.GetByJuzID(ID.Value), nameof(JuzDetail.Id), nameof(JuzDetail.Description));
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        private async Task RefreshData()
        {
            try
            {
                if (ID.HasValue && ID.Value > 0)
                {
                    var juzDetails = await unitOfWork.JuzDetails.GetByJuzID(ID.Value);
                    if (juzDetails.Count() > 0)
                    {
                        this.JuzHeader = juzDetails.FirstOrDefault().Juz;
                    }

                    JuzDetails = juzDetails.ToList(); //(await unitOfWork.JuzDetails.GetByJuzID(ID.Value)).ToList();
                }
                else
                {
                    throw new Exception("Juz tidak ditemukan.");
                }
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

        public async Task<JsonResult> OnGetTafsirAsync(int id)
        {
            try
            {
                var tafsir = await unitOfWork.Tafsirs.GetByID(id);
                if (tafsir != null)
                {
                    return new JsonResult(tafsir.Kemenag);
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
