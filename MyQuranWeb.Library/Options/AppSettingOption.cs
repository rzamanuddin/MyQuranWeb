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
        public int LimitPage { get; set; }
        public string WebSiteName { get; set; }
    }
}
