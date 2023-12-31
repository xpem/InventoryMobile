using Models;

namespace DbContextDAL.Interface
{
    public interface IUserDAL
    {
        Task<int> ExecuteAddUserAsync(User user);

        int ExecuteUpdateLastUpdateUser(DateTime lastUpdate, int uid);

        int GetUid();

        User? GetUserLocal();
    }
}