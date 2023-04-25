using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AspNetCoreCursovaya.Models
{
    public partial class CategoryInPoster
    {
        [Key]
        public int? idCategoryInPoster { get; set; }

        public int? IdCategory { get; set; }
        public int? IdPoster { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual Poster IdPosterNavigation { get; set; }
    }
}
