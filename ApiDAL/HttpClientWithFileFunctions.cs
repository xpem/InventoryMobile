using DbContextDAL;
using Models;
using Models.ItemModels;
using Models.Responses;
using System.Net;
using System.Net.Http.Headers;

namespace ApiDAL
{
    public interface IHttpClientWithFileFunctions : IHttpClientFunctions;

    public class HttpClientWithFileFunctions(InventoryDbContextDAL inventoryDbContextDAL) : HttpClientFunctions(inventoryDbContextDAL), IHttpClientFunctions, IHttpClientWithFileFunctions
    {

        public async Task<ApiResponse> RequestFileAsync(RequestsTypes requestsType, string url,string imagesFilePath, string? userToken = null, Object? content = null)
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

                        var fileName = httpResponse.Content.Headers.ContentDisposition?.FileName ?? throw new ArgumentNullException("filename in headers not found!");

                        var resultStream = await httpResponse.Content.ReadAsStreamAsync();

                        return new ApiResponse()
                        {
                            Success = httpResponse.IsSuccessStatusCode,
                            Error = httpResponse.StatusCode == HttpStatusCode.Unauthorized ? ErrorTypes.Unauthorized : null,
                            TryRefreshToken = httpResponse.StatusCode == HttpStatusCode.Unauthorized,
                            Content = resultStream
                        };
                    case RequestsTypes.Put:
                        using (MultipartFormDataContent form = [])
                        {
                            if (content is not null and ItemFilesToUpload itemFilesToUpload)
                            {
                            
                                if (itemFilesToUpload.Image1 != null)
                                {
                                    string filePath = Path.Combine(imagesFilePath, itemFilesToUpload.Image1.FileName);

                                    using var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                                    using MemoryStream memoryStream = new();
                                    fs.CopyTo(memoryStream);

                                    var fileContent = new ByteArrayContent(memoryStream.ToArray());
                                    //fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(itemFilesToUpload.Image1.FileContentType);

                                    form.Add(fileContent, "file1", itemFilesToUpload.Image1.FileName);
                                }

                                if (itemFilesToUpload.Image2 != null)
                                {
                                    string filePath = Path.Combine(imagesFilePath, itemFilesToUpload.Image2.FileName);

                                    using var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                                    using MemoryStream memoryStream = new();
                                    fs.CopyTo(memoryStream);

                                    var fileContent = new ByteArrayContent(memoryStream.ToArray());
                                    //fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(itemFilesToUpload.Image2.FileContentType);

                                    form.Add(fileContent, "file2", itemFilesToUpload.Image2.FileName);
                                }

                                var response = await httpClient.PutAsync(url, form);
                                response.EnsureSuccessStatusCode();
                                var responseContent = await response.Content.ReadAsStringAsync();

                                return new ApiResponse() { Success = true, Content = responseContent };
                            }
                            else throw new InvalidCastException(nameof(content));
                        }
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is not null && (ex.InnerException.Message == "Nenhuma conexão pôde ser feita porque a máquina de destino as recusou ativamente." || ex.InnerException.Message.Contains("Este host não é conhecido.")))
                    return new ApiResponse() { Success = false, Content = null, Error = ErrorTypes.ServerUnavaliable };

                throw ex;
            }

            throw new Exception("Retorno não esperado");
        }

    }
}
