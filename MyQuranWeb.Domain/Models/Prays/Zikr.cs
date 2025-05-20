using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Models.Prays
{
    public class ZikrData
    {

        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("description")]
        public string Description
        {
            get
            {
                return $"{ID}. Doa Terkait {Title}";
            }
        }

        [JsonProperty("data")]
        public List<Zikr> Data { get; set; } = new List<Zikr>();
    }
    public class Zikr
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("arabic")]
        public string Arabic { get; set; }

        [JsonProperty("arabic_latin")]
        public string ArabicLatin { get; set; }

        [JsonProperty("faedah")]
        public string Faedah { get; set; }

        [JsonProperty("note")]
        public string Note { get; set; }

        [JsonProperty("narrator")]
        public string Narrator { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("translated_id")]
        public string TranslateID { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }
    }
}
