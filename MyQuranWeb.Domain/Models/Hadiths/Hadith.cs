using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MyQuranWeb.Domain.Models.Hadiths
{
    public class Pagination
    {
        [JsonProperty("totalItems")]
        public int TotalItems { get; set; }
        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; }
        [JsonProperty("pageSize")]
        public int PageSize { get; set; }
        [JsonProperty("totalPages")]
        public int TotalPages { get; set; }
        [JsonProperty("startPage")]
        public int StartPage { get; set; }
        [JsonProperty("endPage")]
        public int EndPage { get; set; }
        [JsonProperty("startIndex")]
        public int StartIndex { get; set; }
        [JsonProperty("endIndex")]
        public int EndIndex { get; set; }
        [JsonProperty("pages")]
        public List<int> Pages { get; set; }
    }

    public class HadithData
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("data")]
        public List<Hadith> Data { get; set; }
    }

    public class Hadith
    {
        [JsonProperty("Number")]
        public int Number { get; set; }

        [JsonProperty("Arab")]
        public string Arabic { get; set; }

        [JsonProperty("Id")]
        public string Id { get; set; }

        public string NumberDesc
        {
            get 
            {
                return $"No. {Number}";
            }
        }
    }

    public class HadithResultAPI : Narrator
    {
        
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }

        [JsonProperty("items")]
        public List<Hadith> Items { get; set; }
    }
        
}
