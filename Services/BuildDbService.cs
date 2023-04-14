using LocalDBRepos;

namespace Services
{
    public static class BuildDbService
    {
        public static void BuildSQLiteDb() => BuildDbRepos.BuildDb();
    }
}
