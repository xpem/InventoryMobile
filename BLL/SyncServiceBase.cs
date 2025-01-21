using Models.DTO;
using Services.Interface;

namespace Services
{
    public class SyncServiceBase<T>(T classBase) where T : ISyncServiceBase
    {
        private const int PAGEMAX = 50;

        public async Task ApiToLocalSync(int uid, DateTime lastUpdate)
        {
            int page = 1;
            DateTime? localLastUpdate;

            try
            {
                while (true)
                {
                    var apiRespList = await classBase.GetByLastUpdateAsync(lastUpdate, page);

                    if (apiRespList is not null)
                    {
                        foreach (var apiRespObj in apiRespList)
                        {
                            if (apiRespObj is null) throw new ArgumentNullException(nameof(apiRespObj));

                            apiRespObj.UserId = uid;

                            localLastUpdate = null;

                            if (apiRespObj.Id is not null)
                            {
                                var localObj = await classBase.GetByIdAsync(apiRespObj.Id.Value);

                                apiRespObj.LocalId = localObj?.LocalId ?? 0;
                            }
                            else
                                throw new ArgumentNullException(nameof(apiRespObj.Id));

                            if (localLastUpdate == null && !apiRespObj.Inactive)
                                await classBase.CreateAsync(apiRespObj);
                            else if (apiRespObj.UpdatedAt > localLastUpdate)
                                await classBase.UpdateAsync(apiRespObj);
                        }

                        if (apiRespList.Count < PAGEMAX)
                            break;
                    }
                    else break;

                    page++;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
