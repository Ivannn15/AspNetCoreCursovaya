using System;
using System.Collections.Generic;

#nullable disable

namespace AspNetCoreCursovaya.Models
{
    public partial class Admin
    {
        public int IdAdmin { get; set; }
        public string Username { get; set; }
        public string HashPassword { get; set; }
        public string Salt { get; set; }
    }
}
