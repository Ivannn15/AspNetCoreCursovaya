using System;
using System.Collections.Generic;

#nullable disable

namespace AspNetCoreCursovaya.Models
{
    public partial class Report
    {
        public int IdReport { get; set; }
        public DateTime DatePublication { get; set; }
        public DateTime? DateDelete { get; set; }
        public string Link { get; set; }
    }
}
