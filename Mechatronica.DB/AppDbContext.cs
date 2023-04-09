using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Mechatronica.DB
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
   



            try
            {
                Database.EnsureCreated();
            }
            catch (Exception ex)
            {

            }


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("connectionString");
        }
    }
}