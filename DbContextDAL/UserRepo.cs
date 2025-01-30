using LocalRepos.Interface;
using Microsoft.EntityFrameworkCore;
using Models.DTO;

namespace LocalRepos
{
    public class UserRepo(IDbContextFactory<InventoryDbContext> DbCtx) : IUserDAL
    {
        public async Task<User?> GetUserLocalAsync()
        {
            using var context = DbCtx.CreateDbContext();
            return await context.User.FirstOrDefaultAsync();
        }

        public void RemoveUserLocal()
        {
            using var context = DbCtx.CreateDbContext();
            _ = context.Set<User>().ExecuteDeleteAsync();
        }

        public async Task<int?> GetUidAsync()
        {
            using var context = DbCtx.CreateDbContext();

            return await context.User.Select(x => x.Id).FirstOrDefaultAsync();
        }

        public async Task<int> AddUserAsync(User user)
        {
            using var context = DbCtx.CreateDbContext();

            context.User.Add(user);

            return await context.SaveChangesAsync();
        }

        public int ExecuteUpdateLastUpdateUser(DateTime lastUpdate, int uid)
        {
            using var context = DbCtx.CreateDbContext();

            return context.User.Where(x => x.Id == uid).ExecuteUpdate(y => y.SetProperty(z => z.LastUpdate, lastUpdate));
        }

    }
}
