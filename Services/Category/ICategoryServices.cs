using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Category
{
    public interface ICategoryServices
    {
        Task<List<Models.Category>> GetCategoriesAsync();

        Task<Models.Category> GetCategoryAsync(int id);
    }
}
