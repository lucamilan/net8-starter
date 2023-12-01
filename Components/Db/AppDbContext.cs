using Microsoft.EntityFrameworkCore;

namespace sample.Components.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=data\\app.db");
        }
    }
}