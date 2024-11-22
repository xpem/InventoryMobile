namespace Services.Interface
{
    public interface ISyncServiceBase
    {
        public const int PAGEMAX = 50;

        Task LocalToApiSync(int uid, DateTime lastUpdate);

        Task ApiToLocalSync(int uid, DateTime lastUpdate);
    }
}
