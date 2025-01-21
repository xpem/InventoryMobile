using BLL.Interface;
using Models;

namespace Services
{
    public class BuildDbBLL(LocalRepos.InventoryDbContext inventoryDbContextRepo) : IBuildDbService
    {
        public void Init()
        {
            bool exists = Directory.Exists(FilePaths.DbPath);

            if (!exists)
                Directory.CreateDirectory(FilePaths.DbPath);

            inventoryDbContextRepo.Database.EnsureCreated();

            VersionDbTables? actualVesionDbTables = inventoryDbContextRepo.VersionDbTables.FirstOrDefault();

            VersionDbTables newVersionDbTables = new() { Id = 0, VERSION = 1 };

            if (actualVesionDbTables != null)
            {
                if (actualVesionDbTables.VERSION != newVersionDbTables.VERSION)
                {
                    inventoryDbContextRepo.Database.EnsureDeleted();
                    inventoryDbContextRepo.Database.EnsureCreated();

                    inventoryDbContextRepo.VersionDbTables.Update(newVersionDbTables);
                    inventoryDbContextRepo.SaveChanges();
                }
            }
            else
            {
                inventoryDbContextRepo.VersionDbTables.Add(newVersionDbTables);
                inventoryDbContextRepo.SaveChanges();
            }
        }

        public async Task CleanLocalDatabase()
        {
            inventoryDbContextRepo.User.RemoveRange(inventoryDbContextRepo.User);

            await inventoryDbContextRepo.SaveChangesAsync();
        }
    }
}
