using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyQuranWeb.Domain.Models.PrayerSchedules;
using Newtonsoft.Json;
using MyQuranWebRepository.Interfaces;
using Microsoft.Extensions.Options;
using MyQuranWeb.Library.Options;

namespace MyQuranWeb.Pages.PraySchedule
{
    public class IndexModel : PageModelCustom
    {
        public APIPrayerResult APIPrayerResult { get; set; } = new APIPrayerResult();
        public APILocationResult APILocationResult { get; set; } = new APILocationResult();
        List<Location> Locations { get; set; } = new List<Location>();

        //public string BaseURL { get; set; } = "https://api.myquran.com/v1";

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime SearchDate { get; set; } = DateTime.Today;

        [BindProperty(SupportsGet = true)]
        public string SearchLocationID { get; set; }
        public SelectList CityList { get; set; }

        public IndexModel(IUnitOfWork unitOfWork, IOptions<AppSettingOption> appSettingOption)
        {
            base.unitOfWork = unitOfWork;
            base.AppSettingOption = appSettingOption.Value;
            //BaseURL = unitOfWork.Configuration["AppSettingModel:BaseUrl"];
        }

        public async Task OnGetAsync()
        {
            //string url = $"{BaseURL}/sholat/kota/semua";
            //using (var httpClient = new HttpClient())
            //{
            //    //httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
            //    using (var response = await httpClient.GetAsync(url))
            //    {
            //        string apiResponse = "";
            //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            apiResponse = await response.Content.ReadAsStringAsync();
            //            Locations = JsonConvert.DeserializeObject<List<Location>>(apiResponse);
            //            if (Locations.Count == 0)
            //            {
            //                ErrorMessage = "Daerah / Kota tidak ada";
            //            }
            //            else
            //            {
            //                //cityID = APILocationResult.Location[0].ID;
            //                CityList = new SelectList(Locations.OrderBy(q => q.LocationName), nameof(Location.ID), nameof(Location.LocationName));
            //            }
            //        }
            //        else
            //        {
            //            ErrorMessage = response.StatusCode.ToString();
            //            ErrorMessage = "Error";
            //        }
            //    }
            //}

            ////CityList = new SelectList(await )
            //if (String.IsNullOrWhiteSpace(Search))
            //{
            //    return;
            //}

            //url = $"{BaseURL}/sholat/kota/cari/{Search}";
            //string cityID = "";
            //using (var httpClient = new HttpClient())
            //{
            //    //httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
            //    using (var response = await httpClient.GetAsync(url))
            //    {
            //        string apiResponse = "";
            //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //        {
            //            apiResponse = await response.Content.ReadAsStringAsync();
            //            APILocationResult = JsonConvert.DeserializeObject<APILocationResult>(apiResponse);
            //            if (!APILocationResult.Status)
            //            {
            //                ErrorMessage = APILocationResult.Message;
            //            }
            //            else
            //            {
            //                cityID = APILocationResult.Locations[0].ID;
            //            }
            //        }
            //        else
            //        {
            //            ErrorMessage = response.StatusCode.ToString();
            //            ErrorMessage = "Error";
            //        }
            //    }
            //}

            if (!string.IsNullOrWhiteSpace(Search))
            {
                string date = SearchDate.ToString("yyyy/MM/dd");
                string url = $"{AppSettingOption.BaseUrl}/sholat/jadwal/{SearchLocationID}/{date}";
                using (var httpClient = new HttpClient())
                {
                    //httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                    using (var response = await httpClient.GetAsync(url))
                    {
                        string apiResponse = "";
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            apiResponse = await response.Content.ReadAsStringAsync();
                            APIPrayerResult = JsonConvert.DeserializeObject<APIPrayerResult>(apiResponse);
                            if (!APIPrayerResult.Status)
                            {
                                ErrorMessage = APIPrayerResult.Message;
                            }
                        }
                        else
                        {
                            ErrorMessage = response.StatusCode.ToString();
                            ErrorMessage = "Error";
                        }
                    }
                }
            }
        }
    }
}
