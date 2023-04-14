using System;
using System.Collections.Generic;

#nullable disable

namespace AspNetCoreCursovaya.Models
{
    public partial class CategoriesInDocument
    {
        public int? IdCategory { get; set; }
        public int? IdDocument { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual Document IdDocumentNavigation { get; set; }
    }
}
