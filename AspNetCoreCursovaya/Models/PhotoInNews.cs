using System;
using System.Collections.Generic;

#nullable disable

namespace AspNetCoreCursovaya.Models
{
    public partial class PhotoInNews
    {
        public int IdPhoto { get; set; }
        public int? IdNews { get; set; }
        public string Link { get; set; }
        public DateTime? DateDelete { get; set; }

        public virtual News IdNewsNavigation { get; set; }
    }
}
