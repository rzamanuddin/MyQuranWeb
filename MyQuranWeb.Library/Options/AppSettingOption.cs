using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Library.Options
{
    public class AppSettingOption
    {
        public const string APP_SETTING = "AppSetting";
        public string MP3Url { get; set; }
        public string MP3Url1 { get; set; }
        public string MP3Url2 { get; set; }
        public string MP3UrlSetting { get; set; }
        public string BaseUrl { get; set; }
        public string TafsirUrl { get; set; }
        public string HadithUrl { get; set; }
        public string AsmaulHusnaUrl { get; set; }
        public string PrayUrl { get; set; }
        public string IntentionUrl { get; set; }
        public string PrayerReadUrl { get; set; }
        public string PrayerZikrUrl { get; set; }
        public string PrayerZikrUrl2 { get; set; }
        public string ZikrUrl { get; set; }
        public string QuranPageUrl { get; set; }
        public string QuranPageKemenagUrl { get; set; }
        public int LimitPage { get; set; }
        public string WebSiteName { get; set; }
    }
}
