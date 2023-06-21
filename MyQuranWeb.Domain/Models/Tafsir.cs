using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace MyQuranWeb.Domain.Models
{
    public partial class Tafsir
    {
        public int Id { get; set; }
        public int AyahId { get; set; }
        public int SurahId { get; set; }
        public string Kemenag { get; set; }

        public virtual Surah Surah { get; set; }
        public virtual Ayah Ayah { get; set; }
    }
}
