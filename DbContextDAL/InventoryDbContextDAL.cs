using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;

namespace DbContextDAL
{
    public class InventoryDbContextDAL : DbContext
    {
        public virtual DbSet<Models.VersionDbTables> VersionDbTables { get; set; }

        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={Path.Combine(FilePaths.DbPath, "Inventory.db")}");
        }
    }
}
