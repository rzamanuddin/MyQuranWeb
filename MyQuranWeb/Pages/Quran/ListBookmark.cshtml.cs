using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyQuranWebRepository.Interfaces;
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
            try
            {
                //var ayahs = (await unitOfWork.Ayahs.GetAll()).ToList();
                ////IEnumerable<Bookmark> query;
                //for (int i = 1; i <= ayahs.Count; i++)
                //{
                //    if (!string.IsNullOrWhiteSpace(Request.Cookies[i.ToString()]))
                //    {
                //        Bookmarks.Add(new Bookmark() { ID = i, Ayah = ayahs[i-1] });
                //    }
                //}

                for (int i = 1; i <= 6236; i++)
                {
                    if (!string.IsNullOrWhiteSpace(Request.Cookies[i.ToString()]))
                    {
                        var ayah = await unitOfWork.Ayahs.GetByID(i);
                        Bookmarks.Add(new Bookmark() { ID = i, Type = 1, Ayah = ayah });
                    }
                }

                if (!string.IsNullOrEmpty(Request.Cookies["lastAyah"]))
                {
                    var lastAyah = Request.Cookies["lastAyah"].Split("|");
                    var id = Convert.ToInt32(lastAyah[3]);
                    var ayah = await unitOfWork.Ayahs.GetByID(id);
                    Bookmarks.Add(new Bookmark() { ID = id, Type = 3, Ayah = ayah });
                }

                var cArbains = Request.Cookies.Where(x => x.Key.Contains("book_hadith_arbain"))
                    .OrderBy(x => x.Key);

                var cBM = Request.Cookies.Where(x => x.Key.Contains("book_hadith_bm"))
                    .OrderBy(x => x.Key);

                var cPerawi = Request.Cookies.Where(x => x.Key.Contains("book_hadith_perawi"))
                    .OrderBy(x => x.Key);

                var results = new List<KeyValuePair<string, string>>();
                results.AddRange(cArbains);
                results.AddRange(cBM);
                results.AddRange(cPerawi);

                int r = 1;
                foreach (var cookie in results)
                {
                    Bookmark b = new Bookmark();
                    b.ID = r;
                    b.Type = 2;

                    b.Hadith = new HadithBookmark();
                    b.Hadith.Number = Convert.ToInt32(cookie.Value);
                    b.Hadith.Name = cookie.Key;

                    if (cookie.Key.Contains("_arbain"))
                    {
                        b.Hadith.Description = $"Hadis Arbain No. {cookie.Value}";
                    }
                    else if (cookie.Key.Contains("_bm"))
                    {
                        b.Hadith.Description = $"Hadis Bulughul Maram No. {cookie.Value}";
                        var temps = cookie.Key.Split("_");
                        if (temps != null)
                        {
                            b.Hadith.Page = Convert.ToInt32(temps[3]);
                        }
                    }
                    else if (cookie.Key.Contains("_perawi"))
                    {
                        var temps = cookie.Key.Split("_");
                        if (temps != null)
                        {
                            TextInfo textInfo = new CultureInfo("en-US").TextInfo;
                            b.Hadith.Description = $"H.R. {textInfo.ToTitleCase(temps[3]).Replace("-", " ")} No. {cookie.Value}";
                            b.Hadith.Page = Convert.ToInt32(temps[4]);
                            b.Hadith.Slug = temps[3];
                        }
                    }

                    //if (cookie.Key.Contains("arbain"))
                    b.Hadith.Type = 1;

                    Bookmarks.Add(b);

                    r++;
                }

                if (!string.IsNullOrWhiteSpace(Search))
                {
                    Bookmarks = Bookmarks.Where(q => q.Description.ToLower().Contains(Search.ToLower())).ToList();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
