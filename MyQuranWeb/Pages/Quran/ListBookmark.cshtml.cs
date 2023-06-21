using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyQuranWeb.Domain.Interfaces;
using MyQuranWeb.Domain.Models;

namespace MyQuranWeb.Pages.Quran
{
    public class ListBookmarkModel : PageModelCustom
    {
        public IList<Bookmark> Bookmarks = new List<Bookmark>();
        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }


        public ListBookmarkModel(IUnitOfWork unitOfWork)
        {
            base.unitOfWork = unitOfWork;
        }
        public async Task OnGetAsync()
        {
            var ayahs = (await unitOfWork.Ayahs.GetAll()).ToList();
            //IEnumerable<Bookmark> query;
            for (int i = 1; i <= ayahs.Count; i++)
            {
                if (!string.IsNullOrWhiteSpace(Request.Cookies[i.ToString()]))
                {
                    Bookmarks.Add(new Bookmark() { ID = i, Ayah = ayahs[i-1] });
                }
            }

            

            if (!string.IsNullOrWhiteSpace(Search))
            {
                Bookmarks = Bookmarks.Where(q => q.Description.ToLower().Contains(Search.ToLower())).ToList();
            }
        }
    }
}
