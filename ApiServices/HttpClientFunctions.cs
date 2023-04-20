using Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;

namespace ApiRepos
{
    public class HttpClientFunctions : HttpClient
    {
        public static async Task<Response> GetAsync(string uri, string userToken)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("authorization", "bearer " + userToken);
                    HttpResponseMessage httpResponse = await httpClient.GetAsync(uri);
                    return new Response() { Success = httpResponse.IsSuccessStatusCode, Content = await httpResponse.Content.ReadAsStringAsync() };
                }

                throw new Exception("O servidor está indisponível");
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message == "Nenhuma conexão pôde ser feita porque a máquina de destino as recusou ativamente.")
                {
                    return new Response() { Success = false, Content = null };
                }

                throw ex;
            }
        }


        public static async Task<Response> PostAsync(string uri, string jsonContent, string? userToken = null)
        {
            try
            {
                StringContent data = new(jsonContent, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {
                    if (userToken is not null)
                        httpClient.DefaultRequestHeaders.Add("authorization", "bearer " + userToken);

                    HttpResponseMessage? httpResponse = await httpClient.PostAsync(uri, data);
                    return new Response() { Success = httpResponse.IsSuccessStatusCode, Content = await httpResponse.Content.ReadAsStringAsync() };
                }
            }

            catch { throw; }

        }
    }
}
