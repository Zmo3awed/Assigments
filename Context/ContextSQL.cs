using EF_Core.Models;
using Microsoft.EntityFrameworkCore;
namespace EF_Core.Context
{
    internal class ContextSQL : DbContext
    {
        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=MyEFCore;Trusted_Connection=True;TrustServerCertificate=True;");
        }



        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }


    }
}
