using ApiRepos.User;
using LocalDBRepos.User;

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

        //public static async Task<bool> SignIn(string email, string password)
        //{
        //    email = email.ToLower();

        //    try
        //    {
        //        Models.User? user = await UserApiRepos.GetUser(email, password);

        //        if (user == null)
        //        {
        //        }
        //        else return false;


        //    }
        //    catch (Exception ex) { throw ex; }
        //}
    }
}
