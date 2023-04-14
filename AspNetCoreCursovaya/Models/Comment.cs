using System;
using System.Collections.Generic;

#nullable disable

namespace AspNetCoreCursovaya.Models
{
    public partial class Comment
    {
        public int Idcomments { get; set; }
        public string NameCommentator { get; set; }
        public string TextComment { get; set; }
        public int IdNews { get; set; }
        public DateTime? DatePublication { get; set; }
        public DateTime? DateDelete { get; set; }

        public virtual News IdNewsNavigation { get; set; }
    }
}
