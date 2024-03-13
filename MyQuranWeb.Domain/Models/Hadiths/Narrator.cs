using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MyQuranWeb.Domain.Models.Hadiths
{
    public class Narrator
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("total")]
        public int TotalHadith { get; set; }

        public int ID { get; set; }

        public string Description
        {
            get => $"Total Hadis {TotalHadith}";
        }

        public string DisplaySubTitle
        {
            get
            {
                return String.Format($"H.R. {Name}, {TotalHadith} Hadis.");
            }
        }
    }
}
