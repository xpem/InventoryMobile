using ApiDAL;

namespace BLL
{
    public interface ICheckServerBLL
    {
        Task<bool> CheckServer();
    }

    public class CheckServerBLL(IHttpClientFunctions httpClientFunctions) : ICheckServerBLL
    {
        public async Task<bool> CheckServer() => await httpClientFunctions.CheckServerAsync();
    }
}
