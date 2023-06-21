﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Models.PrayerSchedules
{
    public class APIPrayerResult : APIResult
    {
        [JsonProperty("data")]
        public Location Location { get; set; }
    }
}
