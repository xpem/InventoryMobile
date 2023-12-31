using Models;
using Models.Responses;

namespace BLL.Interface
{
    public interface IUserBLL
    {
        BLLResponse AddUser(string name, string email, string password);
        Task<BLLResponse> SignIn(string email, string password);
        User? GetUserLocal();
        Task<(bool, string?)> GetUserTokenAsync(string email, string password);
        string? RecoverPassword(string email);
        void UpdateLocalUserLastUpdate(int uid);
    }
}