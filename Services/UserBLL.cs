using ApiDAL;
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
            var resp = await UserApiDAl.AddUser(name, email, password);

            if (resp is not null && resp.Content is not null)
            {
                var jResp = JsonNode.Parse(resp.Content);
                if (jResp is not null)
                {
                    User user = new()
                    {
                        Id = jResp["id"]?.GetValue<int>().ToString(),
                        Name = jResp["name"]?.GetValue<string>(),
                        Email = jResp["email"]?.GetValue<string>()
                    };

                    return new BLLResponse() { Success = resp.Success, Content = user };
                }
            }

            return new BLLResponse() { Success = false, Content = null };
        }

        public static async Task<string?> RecoverPassword(string email)
        {
            email = email.ToLower();
            var resp = await UserApiDAl.RecoverPassword(email);

            if (resp is not null && resp.Content is not null)
            {
                var jResp = JsonNode.Parse(resp.Content);
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

                var resp = await UserApiDAl.GetUserToken(email, password);

                if (resp is not null && resp.Content is not null)
                {
                    var jResp = JsonNode.Parse(resp.Content);

                    if (resp.Success && jResp is not null && jResp["token"]?.GetValue<string>() is not null)
                        return (true, jResp["token"]?.GetValue<string>());
                    else if (!resp.Success && jResp is not null && jResp["error"]?.GetValue<string>() is not null)
                        return (false, jResp["error"]?.GetValue<string>());
                    else throw new Exception("Response nao mapeado: " + resp.Content);
                }

                return (false, null);
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
                    var resp = await UserApiDAl.GetUser(userTokenRes);

                    if (resp.Success && resp.Content != null)
                    {
                        var userResponse = JsonNode.Parse(resp.Content);

                        if (userResponse is not null)
                        {
                            User? user = new() { Id = userResponse["id"]?.GetValue<int>().ToString(), Name = userResponse["name"]?.GetValue<string>(), Email = userResponse["email"]?.GetValue<string>(), Token = userTokenRes };
                            return new BLLResponse() { Success = true, Content = user };
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
