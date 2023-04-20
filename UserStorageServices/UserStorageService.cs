using System.Reflection;

namespace UserStorageServices
{
    public class UserStorageService
    {
        private static async Task SaveUserInformation(Models.User user)
        {
            try
            {
                //save user information 
                Preferences.Default.Set("ID", user.Id);
                Preferences.Default.Set("NAME", user.Name);
                Preferences.Default.Set("EMAIL", user.Email);

                //user private information
                await SecureStorage.Default.SetAsync("TOKEN", user.Token);
                await SecureStorage.Default.SetAsync("PASSWORD", user.Password);
            }
            catch (Exception ex) { throw ex; }
        }
    }
}