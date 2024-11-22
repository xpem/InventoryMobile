using Models.DTO;
using Models.Responses;

namespace BLL.Interface
{
    public interface IUserService
    {
        ServResp AddUser(string name, string email, string password);
        Task<ServResp> SignIn(string email, string password);

        Task<User?> GetLocalAsync();

        Task<(bool, string?)> GetUserTokenAsync(string email, string password);
        Task<string?> RecoverPasswordAsync(string email);
        void UpdateLocalUserLastUpdate(int uid);

        void RemoveUserLocal();
    }
}