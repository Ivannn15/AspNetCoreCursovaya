using System;
using System.Collections.Generic;

#nullable disable

namespace AspNetCoreCursovaya.Models
{
    public partial class CategoryInEvent
    {
        public int? IdCategoryInEvents { get; set; }
        public int? IdEvent { get; set; }

        public virtual Category IdCategoryInEventsNavigation { get; set; }
        public virtual Event IdEventNavigation { get; set; }
    }
}
