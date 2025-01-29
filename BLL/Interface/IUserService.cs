using Models.DTO;
using Models.Responses;

namespace Services.Interface
{
    public interface IUserService
    {
        ServResp AddUser(string name, string email, string password);
        Task<ServResp> SignIn(string email, string password);

        Task<User?> GetAsync();

        Task<(bool, string?)> GetUserTokenAsync(string email, string password);

        Task<string?> RecoverPasswordAsync(string email);

        void UpdateLastUpdate(int uid);

        void RemoveUserLocal();
    }
}