using System;
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreCursovaya.Models
{
    public class partners
    {
        [Key]
        public int idPartners { get; set; }

        public string title_partner { get;set; }
        public string text_partner { get;set; }
        public DateTime date_delete { get;set; }
        public string link_photo { get;set; }
    }
}
