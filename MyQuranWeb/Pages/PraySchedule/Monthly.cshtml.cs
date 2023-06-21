using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using MyQuranWeb.Domain.Interfaces;
using MyQuranWeb.Domain.Models.PrayerSchedules;
using MyQuranWeb.Options;
using Newtonsoft.Json;

namespace MyQuranWeb.Pages.PraySchedule
{
    public class MonthlyModel : PageModelCustom
    {
        public APIPrayerResultPerMonth APIPrayerResult { get; set; } = new APIPrayerResultPerMonth();
        public APILocationResult APILocationResult { get; set; } = new APILocationResult();
        List<Location> Locations { get; set; } = new List<Location>();

        //public string BaseURL { get; set; } = "https://api.myquran.com/v1";

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        [BindProperty(SupportsGet = true), DataType("Month")]
        public DateTime SearchPeriod { get; set; } = DateTime.Today;

        [BindProperty(SupportsGet = true)]
        public string SearchLocationID { get; set; }
        public SelectList CityList { get; set; }

        public MonthlyModel(IUnitOfWork unitOfWork, IOptions<AppSettingOption> appSettingOption)
        {
            unitOfWork = unitOfWork;
            AppSettingOption = appSettingOption.Value;
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
                //https://api.myquran.com/v1/sholat/jadwal/1609/2021/04
                string period = SearchPeriod.ToString("yyyy/MM");
                string url = $"{AppSettingOption.BaseUrl}/sholat/jadwal/{SearchLocationID}/{period}";
                using (var httpClient = new HttpClient())
                {
                    //httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                    using (var response = await httpClient.GetAsync(url))
                    {
                        string apiResponse = "";
                        if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        {
                            apiResponse = await response.Content.ReadAsStringAsync();
                            APIPrayerResult = JsonConvert.DeserializeObject<APIPrayerResultPerMonth>(apiResponse);
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
