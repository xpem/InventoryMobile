﻿using BLL.Interface;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BLL
{
    public class BuildDbBLL(DbContextDAL.InventoryDbContextDAL inventoryDbContextDAL) : IBuildDbBLL
    {
        public void Init()
        {
            inventoryDbContextDAL.Database.EnsureCreated();

            VersionDbTables? actualVesionDbTables = inventoryDbContextDAL.VersionDbTables.FirstOrDefault();

            VersionDbTables newVersionDbTables = new() { Id = 0, VERSION = 1 };

            if (actualVesionDbTables != null)
            {
                if (actualVesionDbTables.VERSION != newVersionDbTables.VERSION)
                {
                    inventoryDbContextDAL.Database.EnsureDeleted();
                    inventoryDbContextDAL.Database.EnsureCreated();

                    inventoryDbContextDAL.VersionDbTables.Update(newVersionDbTables);
                    inventoryDbContextDAL.SaveChanges();
                }
            }
            else
            {
                inventoryDbContextDAL.VersionDbTables.Add(newVersionDbTables);
                inventoryDbContextDAL.SaveChanges();
            }           
        }

        public async Task CleanLocalDatabase()
        {
            inventoryDbContextDAL.User.RemoveRange(inventoryDbContextDAL.User);

            await inventoryDbContextDAL.SaveChangesAsync();
        }
    }
}
