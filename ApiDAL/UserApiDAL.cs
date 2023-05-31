﻿using Models;
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

        public static async Task<(bool,string?)> GetUserToken(string email, string password)
        {
            try
            {
                string json = JsonSerializer.Serialize(new { email, password });

                var resp = await HttpClientFunctions.Request(RequestsTypes.Post, ApiKeys.ApiBookshelfUri + "/user/session", json);
                
                if (resp is not null && resp.Content is not null)
                {
                    JsonNode? jResp = JsonNode.Parse(resp.Content);

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