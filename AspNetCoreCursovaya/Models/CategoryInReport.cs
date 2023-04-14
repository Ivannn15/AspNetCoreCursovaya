using System;
using System.Collections.Generic;

#nullable disable

namespace AspNetCoreCursovaya.Models
{
    public partial class CategoryInReport
    {
        public int? IdCategory { get; set; }
        public int? IdReport { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual Report IdReportNavigation { get; set; }
    }
}
