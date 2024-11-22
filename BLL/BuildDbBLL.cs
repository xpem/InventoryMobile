using BLL.Interface;
using Models;

namespace BLL
{
    public class BuildDbBLL(LocalRepos.DbContextRepo inventoryDbContextRepo) : IBuildDbBLL
    {
        public void Init()
        {
            bool exists = System.IO.Directory.Exists(FilePaths.DbPath);

            if (!exists)
                System.IO.Directory.CreateDirectory(FilePaths.DbPath);

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
