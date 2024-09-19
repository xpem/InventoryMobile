using Models.DTO;

namespace DbContextDAL.Interface
{
    public interface IUserDAL
    {
        Task<int> ExecuteAddUserAsync(User user);

        int ExecuteUpdateLastUpdateUser(DateTime lastUpdate, int uid);

        Task<User?> GetUserLocalAsync();

        Task<int?> GetUidAsync();

        void RemoveUserLocal();
    }
}