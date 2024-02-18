using System.ComponentModel.DataAnnotations;

namespace AspNetCoreCursovaya.Models
{
    public class category_in_partners
    {
        [Key]
        public int idCategory_in_partners { get; set; }

        public int id_category { get; set; }
        public int id_partner { get;set; }
    }
}
