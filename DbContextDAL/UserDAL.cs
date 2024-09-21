using DbContextDAL.Interface;
using Microsoft.EntityFrameworkCore;
using Models.DTO;

namespace DbContextDAL
{
    public class UserDAL(InventoryDbContextDAL inventoryDbContextDAL) : IUserDAL
    {
        public async Task<User?> GetUserLocalAsync() => await inventoryDbContextDAL.User.FirstOrDefaultAsync();

        public void RemoveUserLocal() => _ = inventoryDbContextDAL.Set<User>().ExecuteDeleteAsync();

        public async Task<int?> GetUidAsync() => await inventoryDbContextDAL.User.Select(x => x.Id).FirstOrDefaultAsync();

        public async Task<int> ExecuteAddUserAsync(User user)
        {
            inventoryDbContextDAL.ChangeTracker.Clear();

            inventoryDbContextDAL.User.Add(user);

            return await inventoryDbContextDAL.SaveChangesAsync();
        }

        public int ExecuteUpdateLastUpdateUser(DateTime lastUpdate, int uid)
        {
            inventoryDbContextDAL.ChangeTracker.Clear();

            return inventoryDbContextDAL.User.Where(x => x.Id == uid).ExecuteUpdate(y => y.SetProperty(z => z.LastUpdate, lastUpdate));
        }

    }
}
