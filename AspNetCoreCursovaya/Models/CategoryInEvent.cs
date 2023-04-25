using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AspNetCoreCursovaya.Models
{
    public partial class CategoryInEvent
    {
        [Key]
        public int? id_category_event_record { get; set; }

        public int? IdCategoryInEvents { get; set; }
        public int? IdEvent { get; set; }

        public virtual Category IdCategoryInEventsNavigation { get; set; }
        public virtual Event IdEventNavigation { get; set; }
    }
}
