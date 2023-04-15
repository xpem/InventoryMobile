using Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Nodes;

namespace ApiRepos
{
    public class HttpClientFunctions : HttpClient
    {
        static HttpClient httpClient = new();


        public static async Task<Response> GetAsync(string uri, string userToken)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("authorization", "bearer " + userToken);
                    HttpResponseMessage response = await httpClient.GetAsync(uri);

                    JsonNode? obj = JsonNode.Parse(await response.Content.ReadAsStringAsync());

                    if (obj != null)
                        return new Response() { Success = response.IsSuccessStatusCode, Content = obj };
                    else throw new Exception("Erro na consulta: ErrorCode: " + response.StatusCode);

                }
                throw new Exception("O servidor está indisponível");
            }
            catch { throw; }
        }


        public static async Task<Response> PostAsync(string uri, string jsonContent)
        {
            try
            {
                //int forContinue = 0;

                //while (forContinue < 2)
                //{
                StringContent data = new(jsonContent, Encoding.UTF8, "application/json");


                using var httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.PostAsync(uri, data);

                JsonNode? obj = JsonNode.Parse(await response.Content.ReadAsStringAsync());

                if (obj is not null)
                    return new Response() { Success = response.IsSuccessStatusCode, Content = obj };
                else throw new Exception("Erro na consulta: ErrorCode: " + response.StatusCode);
            }

            catch { throw; }

        }
    }
}
