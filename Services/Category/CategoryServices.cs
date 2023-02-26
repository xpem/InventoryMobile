using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Category
{
    public class CategoryServices : ICategoryServices
    {

       public List<Models.Category> listCategories = new()
            {
              new Models.Category(){Id=1,Name="Casa",Color= "#bfc9ca", Padrao=true },
              new Models.Category(){Id=2,Name="Vestimenta",Color= "#f5cba7", Padrao=true},
              new Models.Category(){Id=3,Name="Carro",Color= "#f5b7b1", Padrao=true}
            };

        public Task<List<Models.Category>> GetCategoriesAsync()
        {
            //pega do banco de dados(pela api por enquanto)

            return Task.FromResult(listCategories);
        }

        public Task<Models.Category> GetCategoryAsync(int id)
        {
            return Task.FromResult(listCategories[id]);
        }
    }
}
