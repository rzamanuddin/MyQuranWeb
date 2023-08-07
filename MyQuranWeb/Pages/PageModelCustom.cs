using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyQuranWeb.Domain.Interfaces;
using MyQuranWeb.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyQuranWeb.Pages
{
    public class PageModelCustom : PageModel
    {
        protected IUnitOfWork unitOfWork;

        public string ErrorMessage { get; set; }

        private AppSettingOption _appSettingOption;
        public AppSettingOption AppSettingOption 
        { 
            get
            {
                string qariSetting = Request.Cookies["qariSetting"];

                if (!string.IsNullOrWhiteSpace(qariSetting) && qariSetting != "0")
                {
                    if (qariSetting == "1")
                    {
                        _appSettingOption.MP3UrlSetting = _appSettingOption.MP3Url1;
                        return _appSettingOption;
                    }
                    else if (qariSetting == "2")
                    {
                        _appSettingOption.MP3UrlSetting = _appSettingOption.MP3Url2;
                        return _appSettingOption;
                    }
                };

                _appSettingOption.MP3UrlSetting = _appSettingOption.MP3Url;
                return _appSettingOption;
            }
            set
            {
                _appSettingOption = value;
            }
        }
    }
}
