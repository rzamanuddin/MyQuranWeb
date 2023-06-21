using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Models.PrayerSchedules
{
    public class APIPrayerResultPerMonth : APIResult
    {
        [JsonProperty("data")]
        public LocationSchedulePerMonth Location { get; set; }
    }
}
