using ApiDAL.Handlers;
using ApiDAL.Interfaces;
using BLL.Interface;
using DbContextDAL.Interface;
using Models.DTO;
using Models.Responses;
using System.Text.Json.Nodes;

namespace BLL
{

    public class UserBLL(IUserDAL userDAL, IUserApiDAL userApiDAL) : IUserBLL
    {
        public BLLResponse AddUser(string name, string email, string password)
        {
            email = email.ToLower();
            ApiResponse? resp = userApiDAL.AddUserAsync(name, email, password).Result;

            if (resp is not null && resp.Success && resp.Content is not null and string)
            {
                JsonNode? jResp = JsonNode.Parse(resp.Content as string);

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

        public async Task<string?> RecoverPasswordAsync(string email)
        {
            email = email.ToLower();
            ApiResponse? resp = await userApiDAL.RecoverPasswordAsync(email);

            if (resp is not null && resp.Content is not null and string)
            {
                JsonNode? jResp = JsonNode.Parse(resp.Content as string);
                if (jResp is not null)
                    return jResp["Mensagem"]?.GetValue<string>();
            }

            return null;
        }

        public async Task<(bool, string?)> GetUserTokenAsync(string email, string password) => await userApiDAL.GetUserTokenAsync(email.ToLower(), password);

        public User? GetUserLocal() => userDAL.GetUserLocal();

        public void RemoveUserLocal() => userDAL.RemoveUserLocal();

        public async Task<BLLResponse> SignIn(string email, string password)
        {
            try
            {
                email = email.ToLower();

                (bool success, string? userTokenRes) = await GetUserTokenAsync(email, password);

                if (success && userTokenRes != null)
                {
                    ApiResponse resp = await userApiDAL.GetUserAsync(userTokenRes);

                    if (resp.Success && resp.Content is not null and string)
                    {
                        JsonNode? userResponse = JsonNode.Parse(resp.Content as string);

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

                            _ = userDAL.ExecuteAddUserAsync(user);

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
            catch { throw; }
        }

        public void UpdateLocalUserLastUpdate(int uid) => userDAL.ExecuteUpdateLastUpdateUser(DateTime.Now, uid);

    }
}
