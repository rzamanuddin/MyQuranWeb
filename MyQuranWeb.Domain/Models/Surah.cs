using System;
using System.Collections.Generic;

#nullable disable

namespace MyQuranWeb.Domain.Models
{
    public partial class Surah
    {
        public Surah()
        {
            Ayahs = new HashSet<Ayah>();
            Tafsirs = new HashSet<Tafsir>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string NameLatin { get; set; }
        public string TranslateIndo { get; set; }
        public int NumberOfAyah { get; set; }

        public virtual ICollection<Ayah> Ayahs { get; set; }
        public virtual ICollection<Tafsir> Tafsirs { get; set; }
    }
}
