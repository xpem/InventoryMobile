using Models;
using Models.DTO;
using Services.Interface;

namespace Services;

//public class SyncHelperService<T>(T requester) where T : ISyncHelperService
//{
//    private const int PAGEMAX = 50;

//    public async Task ApiToLocalSync(int uid, DateTime lastUpdate)
//    {
//        int page = 1;
//        DateTime? localLastUpdate;

//        try
//        {
//            while (true)
//            {
//                var apiRespList = await requester.GetByLastUpdateAsync(lastUpdate, page);

//                if (apiRespList is not null)
//                {
//                    foreach (var apiRespObj in apiRespList)
//                    {
//                        if (apiRespObj is null) throw new ArgumentNullException(nameof(apiRespObj));

//                        apiRespObj.UserId = uid;

//                        localLastUpdate = null;

//                        if (apiRespObj.Id is not null)
//                        {
//                            var localObj = await requester.GetByIdAsync(apiRespObj.Id.Value);

//                            apiRespObj.LocalId = localObj?.LocalId ?? 0;
//                        }
//                        else
//                            throw new ArgumentNullException(nameof(apiRespObj.Id));

//                        if (localLastUpdate == null && !apiRespObj.Inactive)
//                            await requester.CreateAsync(apiRespObj);
//                        else if (apiRespObj.UpdatedAt > localLastUpdate)
//                            await requester.UpdateAsync(apiRespObj);
//                    }

//                    if (apiRespList.Count < PAGEMAX)
//                        break;
//                }
//                else break;

//                page++;

//            }
//        }
//        catch (Exception ex)
//        {
//            throw ex;
//        }

//    }
//}

public static class SyncHelperServiceV2
{
    private const int PAGEMAX = 50;

    public static async Task ApiToLocalSync<T>(T requester, int uid, DateTime lastUpdate) where T : ISyncHelperService
    {
        int page = 1;
        DateTime? localLastUpdate;

        try
        {
            while (true)
            {
                var apiRespList = await requester.GetByLastUpdateAsync(lastUpdate, page);

                if (apiRespList is null) break;

                foreach (var apiRespObj in apiRespList)
                {
                    if (apiRespObj is null) throw new ArgumentNullException(nameof(apiRespObj));

                    apiRespObj.UserId = uid;

                    DTOBase? localObj = null;

                    if (apiRespObj.Id is not null)
                    {
                        localObj = await requester.GetByIdAsync(apiRespObj.Id.Value);

                        apiRespObj.LocalId = localObj?.LocalId ?? 0;
                    }
                    else
                        throw new ArgumentNullException(nameof(apiRespObj.Id));

                    if (localObj == null && !apiRespObj.Inactive)
                        await requester.CreateAsync(apiRespObj);
                    else if (apiRespObj.UpdatedAt > localObj?.UpdatedAt)
                        await requester.UpdateAsync(apiRespObj);
                }

                if (apiRespList.Count < PAGEMAX)
                    break;

                page++;

            }
        }
        catch (Exception ex)
        {
            throw;
        }

    }
}
