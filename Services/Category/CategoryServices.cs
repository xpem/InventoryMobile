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
              new Models.Category(){Id=1,Name="Casa",Color= "#0C5455", SystemDefault=true },
              new Models.Category(){Id=2,Name="Vestimenta",Color= "#847700", SystemDefault=true},
              new Models.Category(){Id=3,Name="Carro",Color= "#550C0C", SystemDefault=true}
            };

        public Task<List<Models.Category>> GetCategoriesAsync()
        {
            //pega do banco de dados(pela api por enquanto)

            return Task.FromResult(listCategories);
        }

        public Task<Models.Category> GetCategoryAsync(int id)
        {
            return Task.FromResult(listCategories.Where(s => s.Id == id).First());
        }
    }
}
