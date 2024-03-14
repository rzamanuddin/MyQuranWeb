using MyQuranWeb.Domain.Models.Hadiths;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Models.Hadiths
{
    public class HadithBM
    {
        private string arab;

        [JsonProperty("ar")]
        public string Arab
        {
            get
            {
                return arab;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    arab = value.Replace("للهِ", "للَّهِ")
                        .Replace("للهُ", "للّٰهُ")
                        .Replace("للهَ", "للّٰهَ");
                }
                else
                {
                    arab = value;
                }
            }
        }

        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("no")]
        public int No { get; set; }
    }

    public class HadithBMAPIResult
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
        [JsonProperty("request")]
        public Request Request { get; set; }
        [JsonProperty("info")]
        public Info Info { get; set; }
        [JsonProperty("data")]
        public HadithBM Data { get; set; }
    }
}
