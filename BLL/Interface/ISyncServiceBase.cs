using Models.DTO;

namespace Services.Interface
{
    public interface ISyncServiceBase
    {
        public const int PAGEMAX = 50;

        //Task LocalToApiSync(int uid, DateTime lastUpdate);

        //Task ApiToLocalSync(int uid, DateTime lastUpdate);

        Task<DTOModelBase?> GetByIdAsync(int id);

        Task<int> CreateAsync(DTOModelBase entity);

        Task<int> UpdateAsync(DTOModelBase entity);

        Task<List<DTOModelBase>?> GetByLastUpdateAsync(DateTime lastUpdate, int page);
    }
}
