﻿using Models;
using ApiRepos;
using System.Text.Json;

namespace ApiRepos.User
{
    public static class UserApiRepos
    {
        public static async Task<Models.User?> AddUser(string name, string email, string password)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { name, email, password });

                Response resp = await HttpClientFunctions.PostAsync(ApiKeys.ApiBookshelfUri + "/user", json);

                if (resp is not null && resp.Success && resp.Content is not null)
                {
                    Models.User user = new()
                    {
                        Id = resp.Content["id"]?.GetValue<int>().ToString(),
                        Name = resp.Content["name"]?.GetValue<string>(),
                        Email = resp.Content["email"]?.GetValue<string>()
                    };

                    return user;
                }
                else return null;
            }
            catch (Exception ex) { throw ex; }
        }

        public static async Task<string?> RecoverPassword(string email)
        {
            string json = JsonSerializer.Serialize(new { email });
            Response resp = await HttpClientFunctions.PostAsync(ApiKeys.ApiBookshelfUri + "/user/recoverpassword", json);

            if (resp is not null && resp.Success && resp.Content is not null && resp.Content["Mensagem"]?.GetValue<string>() is not null)
                return resp.Content["Mensagem"]?.GetValue<string>();

            return null;
        }

        public static async Task<(bool, string?)> GetUserToken(string email, string password)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { email, password });

                Response resp = await HttpClientFunctions.PostAsync(ApiKeys.ApiBookshelfUri + "/user/session", json);

                if (resp is not null && resp.Success && resp.Content is not null && resp.Content["token"]?.GetValue<string>() is not null)
                    return (true, resp.Content["token"]?.GetValue<string>());

                return (false, null);
            }
            catch (Exception ex) { throw ex; }
        }

        public static async Task<Models.User?> GetUser(string email, string password)
        {
            try
            {
                Models.User? user = new();

                (bool success, string? userTokenRes) = await GetUserToken(email, password);

                if (success && userTokenRes != null)
                {
                    Response resp = await HttpClientFunctions.GetAsync(ApiKeys.ApiUri + "/user", userTokenRes);

                    if (resp.Success)
                    {
                        if (resp.Content != null)
                        {
                            user = new() { Id = resp.Content["id"]?.GetValue<int>().ToString(), Name = resp.Content["name"]?.GetValue<string>(), Email = resp.Content["email"]?.GetValue<string>(), Token = userTokenRes, Password = password };
                        }
                    }
                }
                else user.Error = ErrorType.WrongEmailOrPassword; ;

                return user;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}