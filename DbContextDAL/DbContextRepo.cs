using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTO;

namespace LocalRepos
{
    public class DbContextRepo : DbContext
    {
        public virtual required DbSet<VersionDbTables> VersionDbTables { get; set; }

        public virtual required DbSet<User> User { get; set; }

        public virtual required DbSet<SubCategory> SubCategory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={Path.Combine(FilePaths.DbPath, "Inventory.db")}");
        }
    }
}
