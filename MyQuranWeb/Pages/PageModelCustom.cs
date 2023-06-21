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

        public AppSettingOption AppSettingOption { get; set; }
    }
}
