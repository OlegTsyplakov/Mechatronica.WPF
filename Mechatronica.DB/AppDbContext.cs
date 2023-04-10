using Mechatronica.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Mechatronica.DB
{
    public class AppDbContext : DbContext
    {
        public DbSet<CarDbModel> Cars { get; set; }
        public DbSet<PersonDbModel> Persons { get; set; }
        public DbSet<MainDbModel> Main { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
   


        }


    }
}