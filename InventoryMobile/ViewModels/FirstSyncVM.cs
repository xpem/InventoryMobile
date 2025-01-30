using BLL.Interface;
using InventoryMobile.Infra.Services;
using InventoryMobile.Views;
using Models.DTO;
using Services.Interface;

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
                User user = await UserBLL.GetAsync();

                if (user != null)
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {

                        await SubCategoryService.ApiToLocalAsync(user.Id, user.LastUpdate);

                        Progress = 0.25M;

                        await SubCategoryService.LocalToApiAsync();

                        Progress = 0.5M;

                        //await BookHistoricSyncBLL.ApiToLocalSync(user.Id, user.LastUpdate);

                        Progress = 0.75M;

                        UserBLL.UpdateLastUpdate(user.Id);

                        Progress = 1;

                        _ = Task.Run(() => { Task.Delay(5000); SyncService.StartThread(); });

                        //_ = AppShellVM.AtualizaUserShowData();


                        _ = Shell.Current.GoToAsync($"//{nameof(Main)}");

                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
