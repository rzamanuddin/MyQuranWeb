using System;
using System.Collections.Generic;

#nullable disable

namespace MyQuranWeb.Domain.Models
{
    public partial class Ayah
    {
        public int Id { get; set; }
        public int AyahId { get; set; }
        public int SurahId { get; set; }
        public string ReadText { get; set; }
        public string ReadIndo { get; set; }
        public string TranslateIndo { get; set; }
        public string ReadTextUthmani { get; set; }

        public virtual Tafsir Tafsir { get; set; }
        public virtual Surah Surah { get; set; }
        public virtual JuzDetail JuzDetail { get; set; }
    }
}
