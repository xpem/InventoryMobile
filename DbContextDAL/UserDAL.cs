using LocalRepos.Interface;
using Microsoft.EntityFrameworkCore;
using Models.DTO;

namespace LocalRepos
{
    public class UserDAL(DbContextRepo inventoryDbContextRepo) : IUserDAL
    {
        public async Task<User?> GetUserLocalAsync() => await inventoryDbContextRepo.User.FirstOrDefaultAsync();

        public void RemoveUserLocal() => _ = inventoryDbContextRepo.Set<User>().ExecuteDeleteAsync();

        public async Task<int?> GetUidAsync() => await inventoryDbContextRepo.User.Select(x => x.Id).FirstOrDefaultAsync();

        public async Task<int> ExecuteAddUserAsync(User user)
        {
            inventoryDbContextRepo.ChangeTracker.Clear();

            inventoryDbContextRepo.User.Add(user);

            return await inventoryDbContextRepo.SaveChangesAsync();
        }

        public int ExecuteUpdateLastUpdateUser(DateTime lastUpdate, int uid)
        {
            inventoryDbContextRepo.ChangeTracker.Clear();

            return inventoryDbContextRepo.User.Where(x => x.Id == uid).ExecuteUpdate(y => y.SetProperty(z => z.LastUpdate, lastUpdate));
        }

    }
}
