using System;
using System.Collections.Generic;

#nullable disable

namespace AspNetCoreCursovaya.Models
{
    public partial class CategoryInPoster
    {
        public int? IdCategory { get; set; }
        public int? IdPoster { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual Poster IdPosterNavigation { get; set; }
    }
}
