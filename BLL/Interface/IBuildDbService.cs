namespace BLL.Interface
{
    public interface IBuildDbService
    {
        Task CleanLocalDatabase();
        void Init();
    }
}