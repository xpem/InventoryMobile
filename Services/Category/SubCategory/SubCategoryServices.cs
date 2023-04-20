using ApiDAL;
using Models;
using System.Text.Json;

namespace Services.Category.SubCategory
{
    public class SubCategoryServices : ISubCategoryServices
    {
        public List<Models.SubCategory> SubCategoryList = new() {
            new Models.SubCategory(){ Id=1,CategoryId=1,Name="Móveis",SystemDefault=true,Icon="\uf4b8"},
            new Models.SubCategory(){ Id=2,CategoryId=1,Name="Eletrodomésticos",SystemDefault=true, Icon="\uf26c"},
            new Models.SubCategory(){ Id=3,CategoryId=1,Name="Computadores",SystemDefault=true, Icon="\ue4e5"},
             new Models.SubCategory(){ Id=4,CategoryId=2,Name="Eletrônicos",SystemDefault=true,Icon="\uf10b"},
             new Models.SubCategory(){ Id=5,CategoryId=2,Name="Calçados",SystemDefault=true,Icon="\uf54b"},
             new Models.SubCategory(){ Id=6,CategoryId=2,Name="Roupas",SystemDefault=true,Icon="\uf553"},
             new Models.SubCategory(){ Id=7,CategoryId=3,Name="Utensílios",SystemDefault=true,Icon="\uf553"},
             new Models.SubCategory(){ Id=8,CategoryId=3,Name="Peças internas",SystemDefault=true,Icon="\uf0ad"},
             new Models.SubCategory(){ Id=9,CategoryId=3,Name="Peças externas",SystemDefault=true,Icon="\uf1b9"}
        };

        string userToken;

        public SubCategoryServices(string _userToken)
        {
            userToken = _userToken;
        }

        //public async Task<BLLResponse> GetSubCategoriesAsync() { 
            
        //  var apiResponse =  await SubCategoryApiDAL.GetSubCategories(userToken);


        //    // WeatherForecast? weatherForecast =             JsonSerializer.Deserialize<WeatherForecast>(jsonString);
        //}

        public Task<Models.SubCategory> GetSubCategoryAsync(int id)
        {
            return Task.FromResult(SubCategoryList.Where(s => s.Id == id).First());
        }
    }
}
