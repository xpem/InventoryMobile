using BLL.Interface;
using LocalRepos;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Services
{
    public class BuildDbBLL(IDbContextFactory<InventoryDbContext> DbCtx) : IBuildDbService
    {
        public void Init()
        {
            using var context = DbCtx.CreateDbContext();
            bool exists = Directory.Exists(FilePaths.DbPath);

            if (!exists)
                Directory.CreateDirectory(FilePaths.DbPath);

            context.Database.EnsureCreated();

            VersionDbTables? actualVesionDbTables = context.VersionDbTables.FirstOrDefault();

            VersionDbTables newVersionDbTables = new() { Id = 0, VERSION = 11 };

            if (actualVesionDbTables != null)
            {
                if (actualVesionDbTables.VERSION != newVersionDbTables.VERSION)
                {
                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    actualVesionDbTables.VERSION = newVersionDbTables.VERSION;

                    context.VersionDbTables.Add(actualVesionDbTables);

                    context.SaveChanges();
                }
            }
            else
            {
                context.VersionDbTables.Add(newVersionDbTables);
                context.SaveChanges();
            }
        }

        public async Task CleanLocalDatabase()
        {
            using var context = DbCtx.CreateDbContext();
            context.User.RemoveRange(context.User);

            await context.SaveChangesAsync();
        }
    }
}
