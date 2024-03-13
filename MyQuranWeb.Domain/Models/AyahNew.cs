using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MyQuranWeb.Domain.Models
{
    public class TafsirApiResult 
    {
        [JsonProperty("ayahs")]
        public List<AyahNew> Ayahs { get; set; } = new List<AyahNew>();
    }

    public class AyahNew
    {
        [JsonProperty("tafsir")]
        public TafsirNew Data { get; set; }
        [JsonProperty("number")]
        public Number Number { get; set; }
    }
    public class Kemenag
    {
        [JsonProperty("short")]
        public string Short { get; set; }
        [JsonProperty("long")]
        public string Long { get; set; }

        public override string ToString()
        {
            return Long;
        }
    }

    public class TafsirNew
    {
        [JsonProperty("kemenag")]
        public Kemenag Kemenag { get; set; }
        [JsonProperty("quraish")]
        public string Quraish { get; set; }
        [JsonProperty("Jalalayn")]
        public string AlJalalain { get; set; }
    }
    public class Number
    {
        [JsonProperty("inQuran")]
        public int InQuran { get; set; }
        [JsonProperty("inSurah")]
        public int InSurah { get; set; }
    }
}
