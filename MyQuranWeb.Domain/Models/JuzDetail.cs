using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace MyQuranWeb.Domain.Models
{
    public partial class JuzDetail
    {
        [ForeignKey("Ayah")]
        public int Id { get; set; }

        [ForeignKey("JuzHeader")]
        public int JuzId { get; set; }        
        public int SurahId { get; set; }
        public int AyahId { get; set; }

        public virtual JuzHeader Juz { get; set; }
        public virtual Ayah Ayah { get; set; }
    }
}
