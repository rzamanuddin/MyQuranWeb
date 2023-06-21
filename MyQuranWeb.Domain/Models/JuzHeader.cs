using System;
using System.Collections.Generic;

#nullable disable

namespace MyQuranWeb.Domain.Models
{
    public partial class JuzHeader
    {
        public JuzHeader()
        {
            JuzDetails = new HashSet<JuzDetail>();
        }

        public int Id { get; set; }
        public int SurahIdstart { get; set; }
        public string SurahNameStart { get; set; }
        public int AyahIdstart { get; set; }
        public int SurahIdend { get; set; }
        public string SurahNameEnd { get; set; }
        public int AyahIdend { get; set; }
        public int TotalAyah { get; set; }

        public virtual ICollection<JuzDetail> JuzDetails { get; set; }
    }
}
