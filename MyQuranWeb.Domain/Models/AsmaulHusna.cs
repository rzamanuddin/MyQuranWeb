using MyQuranWeb.Domain.Models.Hadiths;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    //public class AsmaulHusna
    //{
    //    [JsonProperty("arab")]
    //    public string Arab { get; set; }
    //    [JsonProperty("id")]
    //    public int Id { get; set; }
    //    [JsonProperty("indo")]
    //    public string Indo { get; set; }
    //    [JsonProperty("latin")]
    //    public string Latin { get; set; }
    //}

    //public class AsmaulHusnaAPIResult
    //{
    //    [JsonProperty("status")]
    //    public bool Status { get; set; }
    //    [JsonProperty("request")]
    //    public Request Request { get; set; }
    //    [JsonProperty("info")]
    //    public Info Info { get; set; }
    //    [JsonProperty("data")]
    //    public List<AsmaulHusna> Data { get; set; }
    //}

    public class AsmaulHusna
    {
        [JsonProperty("urutan")]
        public long ID { get; set; }

        [JsonProperty("arab")]
        public string Arabic { get; set; }

        [JsonProperty("latin")]
        public string ArabicLatin { get; set; }

        [JsonProperty("arti")]
        public string TranslateID { get; set; }
    }
}
