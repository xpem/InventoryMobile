﻿using Models.DTO;
using Models.Responses;

namespace BLL.Interface
{
    public interface IUserBLL
    {
        BLLResponse AddUser(string name, string email, string password);
        Task<BLLResponse> SignIn(string email, string password);

        Task<User?> GetUserLocalAsync();

        Task<(bool, string?)> GetUserTokenAsync(string email, string password);
        Task<string?> RecoverPasswordAsync(string email);
        void UpdateLocalUserLastUpdate(int uid);

        void RemoveUserLocal();
    }
}