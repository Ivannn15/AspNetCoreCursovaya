using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AspNetCoreCursovaya.Models
{
    public partial class CategoriesInDocument
    {
        [Key]
        public int? IdCategoryInDocument { get; set; }

        public int? IdCategory { get; set; }
        public int? IdDocument { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual Document IdDocumentNavigation { get; set; }
    }
}
