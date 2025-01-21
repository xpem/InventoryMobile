using LocalRepos.Interface;
using Microsoft.EntityFrameworkCore;
using Models.DTO;

namespace LocalRepos
{
    public class SubCategoryRepo(IDbContextFactory<InventoryDbContext> DbCtx) : ISubCategoryRepo
    {
        public async Task<DateTime?> GetUpdatedAtByIdAsync(int id)
        {
            using var context = DbCtx.CreateDbContext();
            return (await context.SubCategory.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync())?.UpdatedAt;
        }

        public async Task<int> UpdateAsync(SubCategory subCategory)
        {
            using var context = DbCtx.CreateDbContext();

            context.SubCategory.Update(subCategory);

            return await context.SaveChangesAsync();
        }

        public async Task<int> CreateAsync(SubCategory subCategory)
        {
            using var context = DbCtx.CreateDbContext();
            await context.SubCategory.AddAsync(subCategory);

            return await context.SaveChangesAsync();
        }

        public async Task<SubCategory?> GetByIdAsync(int id)
        {
            using var context = DbCtx.CreateDbContext();
            return await context.SubCategory.Where(x => x.Id.Equals(id)).FirstOrDefaultAsync();
        }

    }
}
