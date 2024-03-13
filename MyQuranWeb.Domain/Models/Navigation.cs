using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Models
{
    public class Navigation
    {
        public int Type { get; set; }
        public int? ID { get; set; }
        public string Slug { get; set; }
        public int LastPage { get; set; }
    }
}
