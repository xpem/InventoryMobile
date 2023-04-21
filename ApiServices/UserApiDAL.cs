using Models;
using ApiRepos;
using System.Text.Json;
using System.Text.Json.Nodes;
using Models.Responses;

namespace ApiDAL
{
    public static class UserApiDAl
    {
        public static async Task<ApiResponse> AddUser(string name, string email, string password)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { name, email, password });

                return await HttpClientFunctions.PostAsync(ApiKeys.ApiBookshelfUri + "/user", json);             
            }
            catch (Exception ex) { throw ex; }
        }

        public static async Task<ApiResponse> RecoverPassword(string email)
        {
            string json = JsonSerializer.Serialize(new { email });
            return await HttpClientFunctions.PostAsync(ApiKeys.ApiBookshelfUri + "/user/recoverpassword", json);
        }

        public static async Task<ApiResponse> GetUserToken(string email, string password)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { email, password });
                return await HttpClientFunctions.PostAsync(ApiKeys.ApiBookshelfUri + "/user/session", json);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<ApiResponse> GetUser(string token)
        {
            try
            {
                return await HttpClientFunctions.GetAsync(ApiKeys.ApiBookshelfUri + "/user", token);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
