using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Models.Hadiths
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class HadithArbain
    {
        private string arab;

        [JsonProperty("arab")]
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

        [JsonProperty("indo")]
        public string Indo { get; set; }
        [JsonProperty("judul")]
        public string Judul { get; set; }
        [JsonProperty("no")]
        public int No { get; set; }
    }

    public class Info
    {
        [JsonProperty("min")]
        public int Min { get; set; }
        [JsonProperty("max")]
        public int Max { get; set; }
    }

    public class Request
    {
        [JsonProperty("path")]
        public string Path { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class HadithArbainAPIResult
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
        [JsonProperty("request")]
        public Request Request { get; set; }
        [JsonProperty("info")]
        public Info Info { get; set; }
        [JsonProperty("data")]
        public List<HadithArbain> Data { get; set; } = new List<HadithArbain>();
    }

}
