using BLL;
using Models;

namespace PersonalAssetsMobile.Services
{
    public class UserService //: IUserService
    {
        //readonly IUserBLL userBLL;

        //public UserService(IUserBLL _userBLL)
        //{
        //    userBLL = _userBLL;
        //}

        public static async Task<(bool, string)> AddUser(string name, string email, string password)
        {
            var userResp = await UserBLL.AddUser(name, email, password);

            if (userResp.Success)
            {
                return (true, "Usuário Adicionado");
            }
            else return (false, "Ocorreu um erro ao tentar adicionar o usuário");
        }

        public static async Task<string> RecoverPassword(string email) => await UserBLL.RecoverPassword(email);


        public static async Task<string> SignIn(string email, string password)
        {
            var resp = await UserBLL.GetUser(email, password);

            if (resp.Success)
            {
                User user = resp.Content as User;

                await SaveUserInformation(user, password);

                return null;
            }
            else if (resp.Error is not null && resp.Error == ErrorTypes.WrongEmailOrPassword)
                return "Email/Senha incorretos";

            return "Ocorreu um erro na tentativa de acesso";
        }

        private static async Task SaveUserInformation(User user, string password)
        {
            try
            {
                //save user information 
                Preferences.Default.Set("ID", user.Id);
                Preferences.Default.Set("NAME", user.Name);
                Preferences.Default.Set("EMAIL", user.Email);

                //user private information
                await SecureStorage.Default.SetAsync("TOKEN", user.Token);
                await SecureStorage.Default.SetAsync("PASSWORD", password);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
