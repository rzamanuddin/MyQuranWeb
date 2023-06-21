using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Models
{
    public partial class Surah
    {
        public string Header
        {
            get
            {
                return $"{Id}. {NameLatin} ({TranslateIndo})";
                //return $"{Id}. {NameLatin}";
            }
        }

        public string HeaderOnly
        {
            get
            {
                return $"{Id}. {NameLatin}";
            }
        }

        public string HeaderDescription
        {
            get
            {
                return $"{Id}. {NameLatin} ({TranslateIndo}), {Ayahs.Count} Ayat.";
            }
        }
    }

    public partial class JuzHeader
    {
        public string HeaderName
        {
            get
            {
                return $"{Id}. ({TotalAyah} Ayat)";
            }
        }

        public string HeaderDescription
        {
            get
            {
                //return $"Juz {Id} ({TotalAyah} Ayat), {SurahNameStart} Ayat {AyahIdstart} - {SurahNameEnd} Ayat {AyahIdend}";
                //return $"{Id}. {SurahNameStart} Ayat {AyahIdstart} sd. {SurahNameEnd} Ayat {AyahIdend}";
                return $"{Id}. ({TotalAyah} Ayat), Q.S. {SurahIdstart}:{AyahIdstart} sd. Q.S. {SurahIdend}:{AyahIdend}";
            }
        }
    }

    public partial class JuzDetail
    {
        public string Description
        {
            get
            {
                return $"Q.S. {SurahId}:{AyahId}";
            }
        }
    }

    public partial class JuzHeader
    {

    }
}
