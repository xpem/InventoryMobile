using Models;
using Models.Responses;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;

namespace ApiRepos
{
    public class HttpClientFunctions : HttpClient
    {
        public static async Task<ApiResponse> GetAsync(string uri, string userToken)
        {
            try
            {
                using var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Add("authorization", "bearer " + userToken);
                HttpResponseMessage httpResponse = await httpClient.GetAsync(uri);
                return new ApiResponse() { Success = httpResponse.IsSuccessStatusCode, Content = await httpResponse.Content.ReadAsStringAsync() };
            }
            catch (Exception ex)
            {
                if (ex.InnerException is not null && ex.InnerException.Message == "Nenhuma conexão pôde ser feita porque a máquina de destino as recusou ativamente.")
                    return new ApiResponse() { Success = false, Content = null, Error = ErrorTypes.ServerUnavaliable };

                throw ex;
            }
        }


        public static async Task<ApiResponse> PostAsync(string uri, string jsonContent, string? userToken = null)
        {
            try
            {
                StringContent data = new(jsonContent, Encoding.UTF8, "application/json");

                using var httpClient = new HttpClient();
                if (userToken is not null)
                    httpClient.DefaultRequestHeaders.Add("authorization", "bearer " + userToken);

                HttpResponseMessage? httpResponse = await httpClient.PostAsync(uri, data);
                return new ApiResponse() { Success = httpResponse.IsSuccessStatusCode, Content = await httpResponse.Content.ReadAsStringAsync() };
            }
            catch (Exception ex)
            {
                if (ex.InnerException is not null && ex.InnerException.Message == "Nenhuma conexão pôde ser feita porque a máquina de destino as recusou ativamente.")
                    return new ApiResponse() { Success = false, Content = null, Error = ErrorTypes.ServerUnavaliable };

                throw ex;
            }

        }
    }
}
