using Models.DTO;

namespace Services.Interface
{
    public interface ISyncHelperService
    {
        public const int PAGEMAX = 50;

        Task LocalToApiSync(int uid, DateTime lastUpdate);

        //Task ApiToLocalSync(int uid, DateTime lastUpdate);

        Task<DTOBase?> GetByIdAsync(int id);

        Task<int> CreateAsync(DTOBase entity);

        Task<int> UpdateAsync(DTOBase entity);

        Task<List<DTOBase>?> GetByLastUpdateAsync(DateTime lastUpdate, int page);


    }
}
