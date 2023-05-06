using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalAssetsMobile.Services
{
    public interface ISubCategoryService
    {
        Task<List<Models.SubCategory>> GetSubCategoriesByCategoryId(int categoryId);

        Task<(bool, string)> AddSubcategory(SubCategory subCategory);
    }
}
