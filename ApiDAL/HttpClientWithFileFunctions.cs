using DbContextDAL;
using Models;
using Models.Responses;
using System.Net;
using System.Text;

namespace ApiDAL
{
    public interface IHttpClientWithFileFunctions : IHttpClientFunctions;

    public class HttpClientWithFileFunctions(InventoryDbContextDAL inventoryDbContextDAL) : HttpClientFunctions(inventoryDbContextDAL), IHttpClientFunctions, IHttpClientWithFileFunctions
    {
        public override async Task<ApiResponse> RequestAsync(RequestsTypes requestsType, string url, string? userToken = null, string? jsonContent = null)
        {
            try
            {
                HttpClient httpClient = new();

                if (userToken is not null)
                    httpClient.DefaultRequestHeaders.Add("authorization", "bearer " + userToken);

                HttpResponseMessage httpResponse = new();

                switch (requestsType)
                {
                    case RequestsTypes.Get:
                        httpResponse = await httpClient.GetAsync(url);
                        break;
                        //case RequestsTypes.Post:
                        //    if (jsonContent is not null)
                        //    {
                        //        StringContent bodyContent = new(jsonContent, Encoding.UTF8, "application/json");
                        //        httpResponse = await httpClient.PostAsync(url, bodyContent);
                        //    }
                        //    else return new ApiResponse() { Success = false, Content = null, Error = ErrorTypes.BodyContentNull };
                        //    break;
                        //case RequestsTypes.Put:
                        //    if (jsonContent is not null)
                        //    {
                        //        StringContent bodyContent = new(jsonContent, Encoding.UTF8, "application/json");
                        //        httpResponse = await httpClient.PutAsync(url, bodyContent);
                        //    }
                        //    else return new ApiResponse() { Success = false, Content = null, Error = ErrorTypes.BodyContentNull };
                        //    break;
                        //case RequestsTypes.Delete:
                        //    httpResponse = await httpClient.DeleteAsync(url);
                        //    break;
                }

                var fileName = httpResponse.Content.Headers.ContentDisposition?.FileName ?? throw new ArgumentNullException("filename in headers not found!");
                //FileSystem.AppDataDirectory
                string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)+"", fileName);
                using var resultStream = await httpResponse.Content.ReadAsStreamAsync();

                //passar isso para a bll, para separar itens por pasta, e só acessar pela api caso n tenha o arquivo local.
                using var fs = new FileStream(filePath, FileMode.OpenOrCreate,FileAccess.ReadWrite);
                resultStream.CopyTo(fs);

                return new ApiResponse()
                {
                    Success = httpResponse.IsSuccessStatusCode,
                    Error = httpResponse.StatusCode == HttpStatusCode.Unauthorized ? ErrorTypes.Unauthorized : null,
                    TryRefreshToken = httpResponse.StatusCode == HttpStatusCode.Unauthorized,
                    Content = filePath
                };
            }
            catch (Exception ex)
            {
                if (ex.InnerException is not null && (ex.InnerException.Message == "Nenhuma conexão pôde ser feita porque a máquina de destino as recusou ativamente." || ex.InnerException.Message.Contains("Este host não é conhecido.")))
                    return new ApiResponse() { Success = false, Content = null, Error = ErrorTypes.ServerUnavaliable };

                throw ex;
            }
        }

    }
}
