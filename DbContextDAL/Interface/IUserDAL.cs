using Models.DTO;

namespace LocalRepos.Interface
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