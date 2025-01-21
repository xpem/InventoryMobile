using Models.DTO;

namespace LocalRepos.Interface
{
    public interface ISubCategoryRepo
    {
        Task<int> CreateAsync(SubCategory subCategory);
        Task<DateTime?> GetUpdatedAtByIdAsync(int id);
        Task<int> UpdateAsync(SubCategory subCategory);
        Task<SubCategory?> GetByIdAsync(int id);
    }
}