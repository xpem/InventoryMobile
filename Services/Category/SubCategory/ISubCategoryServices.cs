using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Category.SubCategory
{
    public interface ISubCategoryServices
    {
        Task<List<Models.SubCategory>> GetSubCategoriesAsync(int CategoryId);

        Task<Models.SubCategory> GetSubCategoryAsync(int id);
    }
}
