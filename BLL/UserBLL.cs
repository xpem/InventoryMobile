using ApiDAL;
using ApiDAL.Handlers;
using BLL.Handlers;
using LocalDbDAL;
using Models;
using Models.Responses;
using System.Text.Json.Nodes;

namespace BLL
{
    public static class UserBLL// : IUserBLL
    {
        public static async Task<BLLResponse> AddUser(string name, string email, string password)
        {
            email = email.ToLower();
            ApiResponse? resp = await UserApiDAl.AddUser(name, email, password);

            if (resp is not null && resp.Success && resp.Content is not null)
            {
                JsonNode? jResp = JsonNode.Parse(resp.Content);
                if (jResp is not null)
                {
                    User user = new()
                    {
                        Id = jResp["id"]?.GetValue<int>() ?? 0,
                        Name = jResp["name"]?.GetValue<string>(),
                        Email = jResp["email"]?.GetValue<string>()
                    };

                    if (user.Id is not 0)
                        return new BLLResponse() { Success = resp.Success };
                }
            }

            return new BLLResponse() { Success = false, Content = null };
        }

        public static async Task<string?> RecoverPassword(string email)
        {
            email = email.ToLower();
            ApiResponse? resp = await UserApiDAl.RecoverPassword(email);

            if (resp is not null && resp.Content is not null)
            {
                JsonNode? jResp = JsonNode.Parse(resp.Content);
                if (jResp is not null)
                {
                    return jResp["Mensagem"]?.GetValue<string>();
                }
            }

            return null;
        }

        public static async Task<(bool, string?)> GetUserToken(string email, string password)
        {
            try
            {
                email = email.ToLower();

                return await UserApiDAl.GetUserToken(email, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<BLLResponse> GetUser(string email, string password)
        {
            try
            {
                email = email.ToLower();

                (bool success, string? userTokenRes) = await GetUserToken(email, password);

                if (success && userTokenRes != null)
                {
                    ApiResponse resp = await UserApiDAl.GetUser(userTokenRes);

                    if (resp.Success && resp.Content != null)
                    {
                        JsonNode? userResponse = JsonNode.Parse(resp.Content);

                        if (userResponse is not null)
                        {
                            User? user = new()
                            {
                                Id = userResponse["id"]?.GetValue<int>() ?? 0,
                                Name = userResponse["name"]?.GetValue<string>(),
                                Email = userResponse["email"]?.GetValue<string>(),
                                Token = userTokenRes,
                                Password = PasswordHandler.Encrypt(password)
                            };

                            UserLocalDAl.AddUser(user);

                            return new BLLResponse() { Success = true };
                        }
                    }
                }
                //maybe use a errorcodes instead a message?
                else if (!success && userTokenRes is not null && userTokenRes == "User/Password incorrect")
                    return new BLLResponse() { Success = false, Error = ErrorTypes.WrongEmailOrPassword };
                else throw new Exception("Erro não mapeado");

                return new BLLResponse() { Success = false, Error = ErrorTypes.Unknown };
            }
            catch (Exception ex) { throw ex; }
        }

    }
}
