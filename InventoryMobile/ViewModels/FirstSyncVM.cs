using BLL.Interface;
using InventoryMobile.Infra.Services;
using InventoryMobile.Views;
using Models.DTO;
using Services;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryMobile.ViewModels
{
    public partial class FirstSyncVM : ViewModelBase
    {

        private decimal progress;
        private readonly ISyncService SyncService;

        public decimal Progress { get => progress; set { if (progress != value) { progress = value; OnPropertyChanged(nameof(Progress)); } } }

        public IUserService UserBLL { get; }

        public IBuildDbService BuildDbBLL { get; }

        public ISubCategoryService SubCategoryService { get; }

        public FirstSyncVM(IUserService userBLL, ISubCategoryService subCategoryService, ISyncService syncService)
        {
            UserBLL = userBLL;
            SubCategoryService = subCategoryService;
            SyncService = syncService;
            _ = SynchronizingProcess();
        }

        private async Task SynchronizingProcess()
        {
            try
            {
                User user = await UserBLL.GetLocalAsync();

                if (user != null)
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {

                        await SubCategoryService.ApiToLocalSync(user.Id, user.LastUpdate);

                        Progress = 0.25M;

                        //await BooksSyncBLL.LocalToApiSync(user.Id, user.LastUpdate);

                        Progress = 0.5M;

                        //await BookHistoricSyncBLL.ApiToLocalSync(user.Id, user.LastUpdate);

                        Progress = 0.75M;

                        UserBLL.UpdateLocalUserLastUpdate(user.Id);

                        Progress = 1;

                        //_ = Task.Run(() => { Task.Delay(5000); SyncServices.StartThread(); });

                        _ = Shell.Current.GoToAsync($"//{nameof(Main)}");

                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
