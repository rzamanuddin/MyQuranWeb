using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Models.Prayers
{
    public class PrayerReadBase
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("arabic")]
        public string Arabic { get; set; }
        [JsonProperty("arabiclatin")]
        public string ArabicLatin { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("translatedid")]
        public string TranslateID { get; set; }
    }
    public class Intention : PrayerReadBase
    {        
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("narrator")]
        public string Narrator { get; set; }
    }

    public class PrayerRead : PrayerReadBase
    {

    }

    public class PrayerZikr : PrayerReadBase
    {
    }
}
