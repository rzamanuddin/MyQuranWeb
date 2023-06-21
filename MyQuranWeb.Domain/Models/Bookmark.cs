using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Models
{
    public class Bookmark
    {
        public int ID { get; set; }

        public Ayah Ayah { get; set; }

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
                    return "";
                }
            }
        }
    }
}
