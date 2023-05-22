using ApiDAL;

namespace BLL
{
    public static class CheckServerBLL
    {
        public static async Task<bool> CheckServer() => await HttpClientFunctions.CheckServer();
    }
}
