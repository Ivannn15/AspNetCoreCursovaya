using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace AspNetCoreCursovaya.Models
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {
        }

        

    }
}
