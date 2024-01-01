using DbContextDAL.Interface;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DbContextDAL
{
    public class UserDAL(InventoryDbContextDAL inventoryDbContextDAL) : IUserDAL
    {
        public Models.User? GetUserLocal() => inventoryDbContextDAL.User.FirstOrDefault();

        public int? GetUid() => inventoryDbContextDAL.User.Select(x => x.Id).FirstOrDefault();

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
