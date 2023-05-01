using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AspNetCoreCursovaya.Models
{
    public partial class CategoryInReport
    {
        [Key]
        public int idCategoryInReport { get; set; }

        public int? IdCategory { get; set; }
        public int? IdReport { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual Report IdReportNavigation { get; set; }
    }
}
