﻿using ApiRepos;
using Models;
using Models.Responses;
using System.Text.Json;

namespace ApiDAL
{
    public interface IItemDAL
    {
        Task<ApiResponse> AddItem(Item item);
        Task<ApiResponse> AltItem(Item item);
        Task<ApiResponse> DelItem(int id);
        Task<ApiResponse> GetItemById(string id);
        Task<ApiResponse> GetItems();
    }

    public class ItemDAL(IHttpClientFunctions httpClientFunctions) : IItemDAL
    {
        public async Task<ApiResponse> GetItems() =>
            await httpClientFunctions.AuthRequest(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/item");

        public async Task<ApiResponse> GetItemById(string id) =>
           await httpClientFunctions.AuthRequest(RequestsTypes.Get, ApiKeys.ApiAddress + "/Inventory/item/" + id);

        public async Task<ApiResponse> AddItem(Models.Item item)
        {
            try
            {
                string json = JsonSerializer.Serialize(new
                {
                    item.Name,
                    item.TechnicalDescription,
                    item.AcquisitionDate,
                    item.PurchaseValue,
                    item.PurchaseStore,
                    item.ResaleValue,
                    Situation = new { Id = item.Situation },
                    item.Comment,
                    item.AcquisitionType,
                    Category = new { item.Category?.Id, SubCategory = item.Category?.SubCategory is not null ? new { item.Category.SubCategory.Id } : null }
                });

                return await httpClientFunctions.AuthRequest(Models.RequestsTypes.Post, ApiKeys.ApiAddress + "/Inventory/item", json);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<ApiResponse> AltItem(Models.Item item)
        {
            try
            {
                string json = JsonSerializer.Serialize(new
                {
                    item.Name,
                    item.TechnicalDescription,
                    item.AcquisitionDate,
                    item.PurchaseValue,
                    item.PurchaseStore,
                    item.ResaleValue,
                    Situation = new { Id = item.Situation },
                    item.Comment,
                    item.AcquisitionType,
                    Category = new { item.Category?.Id, SubCategory = item.Category?.SubCategory is not null ? new { item.Category.SubCategory.Id } : null }
                });

                return await httpClientFunctions.AuthRequest(Models.RequestsTypes.Put, ApiKeys.ApiAddress + "/Inventory/item/" + item.Id, json);
            }
            catch (Exception ex) { throw; }
        }

        public async Task<ApiResponse> DelItem(int id)
        {
            try
            {
                return await httpClientFunctions.AuthRequest(RequestsTypes.Delete, ApiKeys.ApiAddress + "/Inventory/item/" + id);
            }
            catch (Exception ex) { throw; }
        }

    }
}
