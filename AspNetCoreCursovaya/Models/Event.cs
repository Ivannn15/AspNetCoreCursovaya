using System;
using System.Collections.Generic;

#nullable disable

namespace AspNetCoreCursovaya.Models
{
    public partial class Event
    {
        public int IdEvents { get; set; }
        public string TitleEvent { get; set; }
        public string TextEvent { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public TimeSpan? TimeStart { get; set; }
        public TimeSpan? TimeEnd { get; set; }
        public DateTime? DateDelete { get; set; }
    }
}
