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

        public override async Task<ApiResponse> RequestAsync(RequestsTypes requestsType, string url, string? userToken = null, Object? content = null)
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
                        //FileSystem.AppDataDirectory
                        var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Images");
                        bool exists = System.IO.Directory.Exists(path);

                        if (!exists)
                            System.IO.Directory.CreateDirectory(path);

                        string filePath = Path.Combine(path, fileName);

                        using (var resultStream = await httpResponse.Content.ReadAsStreamAsync())
                        {

                            using var fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                            resultStream.CopyTo(fs);

                            return new ApiResponse()
                            {
                                Success = httpResponse.IsSuccessStatusCode,
                                Error = httpResponse.StatusCode == HttpStatusCode.Unauthorized ? ErrorTypes.Unauthorized : null,
                                TryRefreshToken = httpResponse.StatusCode == HttpStatusCode.Unauthorized,
                                Content = filePath
                            };
                        }
                    //case RequestsTypes.Post:
                    //    if (jsonContent is not null)
                    //    {
                    //        StringContent bodyContent = new(jsonContent, Encoding.UTF8, "application/json");
                    //        httpResponse = await httpClient.PostAsync(url, bodyContent);
                    //    }
                    //    else return new ApiResponse() { Success = false, Content = null, Error = ErrorTypes.BodyContentNull };
                    //    break;
                    case RequestsTypes.Put:
                        using (MultipartFormDataContent form = [])
                        {
                            if (content is not null and FileToUpload)
                            {
                                FileToUpload fileToUpload = content as FileToUpload;

                                using MemoryStream ms = new();

                                fileToUpload.FileStream.CopyTo(ms);
                                ms.ToArray();

                                using var fileContent = new ByteArrayContent(ms.ToArray());
                                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse(fileToUpload.FileContentType);

                                form.Add(fileContent, "file1", fileToUpload.FileName);

                                var response = await httpClient.PutAsync(url, form);
                                response.EnsureSuccessStatusCode();
                                var responseContent = await response.Content.ReadAsStringAsync();
                                Console.WriteLine("response :" + responseContent);
                                return new ApiResponse();
                            }
                            else throw new InvalidCastException(nameof(content));
                        }
                        //using (MultipartFormDataContent form = [])
                        //{
                        //    using var fileContent = new ByteArrayContent(await File.ReadAllBytesAsync(content));
                        //    fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("multipart/form-data");

                        //    form.Add(fileContent, "formFile", Path.GetFileName(content));

                        //    var response = await httpClient.PostAsync(url, form);
                        //    response.EnsureSuccessStatusCode();
                        //    var responseContent = await response.Content.ReadAsStringAsync();
                        //    Console.WriteLine("response :" + responseContent);

                        //    if (content is not null)
                        //    {
                        //        StringContent bodyContent = new(content, Encoding.UTF8, "application/json");
                        //        httpResponse = await httpClient.PutAsync(url, bodyContent);

                        //        return new ApiResponse();
                        //    }
                        //    else return new ApiResponse() { Success = false, Content = null, Error = ErrorTypes.BodyContentNull };
                        //}
                        //case RequestsTypes.Delete:
                        //    httpResponse = await httpClient.DeleteAsync(url);
                        //    break;
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
