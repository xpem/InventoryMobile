using Models;
using Models.Responses;
using System.Text.Json;

namespace BLL
{
    public static class ApiResponseHandler
    {
        public static BLLResponse Handler<TModel>(ApiResponse apiResponse)
        {
            try
            {
                if (apiResponse is not null)
                {
                    if (!apiResponse.Success)
                    {
                        if (apiResponse.Error != null)
                            return new BLLResponse() { Success = false, Error = apiResponse.Error };
                        else if (apiResponse.Content != null)
                            return new BLLResponse() { Success = false, Content = apiResponse.Content };
                    }

                    if (apiResponse.Content is not null)
                        return new BLLResponse() { Success = true, Content = JsonDeserialize<TModel>(apiResponse.Content) };

                    throw new Exception("apiResponse.Content nulo");
                }

                throw new Exception("apiResponse nulo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static TModel JsonDeserialize<TModel>(string content)
        {
            try
            {
                var item = JsonSerializer.Deserialize<TModel>(content);
                if (item is not null)
                    return item;
                else throw new Exception("item nulo");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
