namespace BLL.Interface
{
    public interface IBuildDbBLL
    {
        Task CleanLocalDatabase();
        void Init();
    }
}