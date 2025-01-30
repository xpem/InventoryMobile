﻿using Microsoft.EntityFrameworkCore;
using Models.DTO;
namespace LocalRepos;

public class OperationQueueRepo(IDbContextFactory<InventoryDbContext> bookshelfDbContext) : IOperationQueueRepo
{
    public async Task UpdateOperationStatusAsync(ApiOperationStatus operationStatus, int operationId)
    {
        using var context = bookshelfDbContext.CreateDbContext();

        await context.ApiOperationQueue.Where(x => x.Id == operationId)
            .ExecuteUpdateAsync(y => y
            .SetProperty(z => z.Status, operationStatus)
            .SetProperty(z => z.UpdatedAt, DateTime.Now));
    }

    public async Task<List<ApiOperationDTO>> GetPendingOperationsByStatusAsync(ApiOperationStatus operationStatus)
    {
        using var context = bookshelfDbContext.CreateDbContext();
        return await context.ApiOperationQueue.Where(x => x.Status == operationStatus).OrderBy(x => x.CreatedAt).ToListAsync();
    }

    public async Task InsertOperationInQueueAsync(ApiOperationDTO apiOperation)
    {
        using var context = bookshelfDbContext.CreateDbContext();
        context.ApiOperationQueue.Add(apiOperation);

        await context.SaveChangesAsync();
    }

    public async Task<bool> CheckIfHasPendingOperationWithObjectId(string objectId)
    {
        using var context = bookshelfDbContext.CreateDbContext();
        return await context.ApiOperationQueue.AnyAsync(x => x.ObjectId == objectId && x.Status == ApiOperationStatus.Pending);
    }

    public async Task<bool> CheckIfHasPendingOperation()
    {
        using var context = bookshelfDbContext.CreateDbContext();
        return await context.ApiOperationQueue.AnyAsync(x => x.Status == ApiOperationStatus.Pending);
    }
}
