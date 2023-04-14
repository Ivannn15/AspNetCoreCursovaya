using System;
using System.Collections.Generic;

#nullable disable

namespace AspNetCoreCursovaya.Models
{
    public partial class News
    {
        public News()
        {
            Comments = new HashSet<Comment>();
            PhotoInNews = new HashSet<PhotoInNews>();
        }

        public int IdNews { get; set; }
        public string TitleNews { get; set; }
        public string TextNews { get; set; }
        public DateTime DatePublication { get; set; }
        public DateTime DateDelete { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<PhotoInNews> PhotoInNews { get; set; }
    }
}
