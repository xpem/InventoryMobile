using ApiDAL.Handlers;
using ApiDAL.Interfaces;
using LocalRepos.Interface;
using Models.DTO;
using Models.Responses;
using Services.Interface;
using System.Text.Json.Nodes;

namespace Services
{

    public class UserService(IUserDAL userDAL, IUserApiRepo userApiDAL) : IUserService
    {
        public ServResp AddUser(string name, string email, string password)
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
                        return new ServResp() { Success = resp.Success };
                }
            }

            return new ServResp() { Success = false, Content = null };
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

        public async Task<User?> GetAsync() => await userDAL.GetUserLocalAsync();

        public void RemoveUserLocal() => userDAL.RemoveUserLocal();

        public async Task<ServResp> SignIn(string email, string password)
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
                        JsonNode? userResponse = JsonNode.Parse((string)resp.Content);

                        if (userResponse is not null)
                        {
                            User? user = new()
                            {
                                Id = userResponse["id"]?.GetValue<int>() ?? 0,
                                Name = userResponse["name"]?.GetValue<string>(),
                                Email = userResponse["email"]?.GetValue<string>(),
                                Token = userTokenRes,
                                Password = EncryptionService.Encrypt(password)
                            };

                            _ = userDAL.AddUserAsync(user);

                            return new ServResp() { Success = true };
                        }
                    }
                }
                //maybe use a errorcodes instead a message?
                else if (!success && userTokenRes is not null && userTokenRes == "User/Password incorrect")
                    return new ServResp() { Success = false, Error = ErrorTypes.WrongEmailOrPassword };
                else throw new Exception("Erro não mapeado");

                return new ServResp() { Success = false, Error = ErrorTypes.Unknown };
            }
            catch { throw; }
        }

        public void UpdateLastUpdate(int uid) => userDAL.ExecuteUpdateLastUpdateUser(DateTime.Now, uid);

    }
}
