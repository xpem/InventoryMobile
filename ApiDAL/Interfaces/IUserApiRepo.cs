using Models.Responses;

namespace ApiDAL.Interfaces
{
    public interface IUserApiRepo
    {
        Task<ApiResponse> AddUserAsync(string name, string email, string password);
        Task<ApiResponse> GetUserAsync(string token);
        Task<(bool, string?)> GetUserTokenAsync(string email, string password);
        Task<ApiResponse> RecoverPasswordAsync(string email);
    }
}