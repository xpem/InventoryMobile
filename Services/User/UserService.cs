using ApiRepos.User;
using LocalDBRepos.User;
using Models;

namespace Services.User
{
    public static class UserService // : IUserService
    {
        public static async Task<Models.User?> AddUser(string name, string email, string password)
        {
            email = email.ToLower();
            return await UserApiRepos.AddUser(name, email, password);
        }

        public static async Task<string?> RecoverPassword(string email) => await UserApiRepos.RecoverPassword(email);

        public static Models.User? GetUserLocalDb() => UserRepos.GetUser();

        public static async Task<(bool,ErrorType?)> SignIn(string email, string password)
        {
            email = email.ToLower();

            try
            {
                Models.User user = await UserApiRepos.GetUser(email, password);

                if (user.Error is null)
                {
                    UserRepos.InsertUser(user);
                    return (true, null);
                }
                else return (false, user.Error);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
