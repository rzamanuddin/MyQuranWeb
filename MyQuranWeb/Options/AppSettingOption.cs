using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyQuranWeb.Options
{
    public class AppSettingOption : PageModel
    {
        public const string APP_SETTING = "AppSetting";
        public string MP3Url { get; set; }
        public string MP3Url1 { get; set; }
        public string MP3Url2 { get; set; }
        public string MP3UrlSetting { get; set; }
        public string BaseUrl { get; set; }
        public string TafsirUrl { get;set; }
        public string WebSiteName { get; set; }

    }
}
