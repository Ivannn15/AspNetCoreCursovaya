using System;
using System.Collections.Generic;

#nullable disable

namespace AspNetCoreCursovaya.Models
{
    public partial class Poster
    {
        public int IdPoster { get; set; }
        public string TitlePoster { get; set; }
        public string TextPoster { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public DateTime? DatePublication { get; set; }
        public DateTime? DateDelete { get; set; }
    }
}
