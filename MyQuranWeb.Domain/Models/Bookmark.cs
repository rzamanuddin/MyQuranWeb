using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Models
{
    public class HadithBookmark
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int Number { get; set; }
        public int Page {  get; set; }
        public string Slug { get; set; }
    }

    public class Bookmark
    {
        public int ID { get; set; }
        public int Type { get;set; }

        public Ayah Ayah { get; set; }
        public HadithBookmark Hadith { get; set; }
        public string Description
        {
            get
            {
                if (Ayah != null)
                {
                    return $"Q.S. {Ayah.Surah.NameLatin} Ayat {Ayah.AyahId}";
                }
                else
                {
                    if (Hadith != null)
                    {
                        return Hadith.Description;
                    }

                    return "";
                }
            }
        }
    }
}
