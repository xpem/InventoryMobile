using Microsoft.EntityFrameworkCore;

namespace DbContextDAL
{
    public class InventoryDbContextDAL : DbContext
    {
        public virtual DbSet<Models.VersionDbTables> VersionDbTables { get; set; }

        public virtual DbSet<Models.User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Inventory.db")}");
        }
    }
}
