using AspNetCoreCursovaya.Models;
using System.Collections.Generic;

namespace AspNetCoreCursovaya.helpingClasses
{
    public class PartnersAndCategory
    {
        public List<partners> partners { get; set; }
        public List<category_in_partners> category_In_Partners { get; set; }
    }
}
