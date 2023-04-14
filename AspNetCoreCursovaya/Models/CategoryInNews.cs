using System;
using System.Collections.Generic;

#nullable disable

namespace AspNetCoreCursovaya.Models
{
    public partial class CategoryInNews
    {
        public int IdCategory { get; set; }
        public int IdNews { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual News IdNewsNavigation { get; set; }
    }
}
