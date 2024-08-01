using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Models.Prays
{
    public class PrayData
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
        public List<Pray> Data { get; set; } = new List<Pray>();
    }

    public class Pray
    {
        [JsonProperty("id")]
        public int ID { get; set; }
        [JsonProperty("arabic")]
        public string Arabic { get; set; }
        [JsonProperty("arabiclatin")]
        public string ArabicLatin { get; set; }
        [JsonProperty("faedah")]
        public string Faedah { get; set; }
        [JsonProperty("note")]
        public string Note { get; set; }
        [JsonProperty("narrator")]
        public string Narrator { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("translatedid")]
        public string TranslateID { get; set; }
        [JsonProperty("description")]
        public string Description
        {
            get
            {
                return $"{ID}. {Title}";
            }
        }
        [JsonProperty("tag")]
        public List<string> Tag { get; set; }
    }
}
