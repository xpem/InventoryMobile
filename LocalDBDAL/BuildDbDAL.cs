using Microsoft.Data.Sqlite;
using Models;

namespace LocalDbDAL
{
    public class BuildDbDAL
    {
        /// <summary>
        /// create or update the structure of SQLite tables by actual version defined
        /// </summary>
        public static async Task BuildDB()
        {
            SqliteFunctions.OpenIfClosed();

            await UpdateSQLiteTablesByVersions();

            await SqliteFunctions.RunSqliteCommand("create table if not exists USER (ID integer primary key autoincrement,NAME text, EMAIL text, UID text, TOKEN text,PASSWORD text, LASTUPDATE datetime);");
            //_ = SqliteFunctions.RunSqliteCommand("create table if not exists VERSIONDB (USER integer);");

            SqliteFunctions.CloseIfOpen();
        }

        /// <summary>
        /// delete tables of old versions and recreate it
        /// </summary>
        private static async Task UpdateSQLiteTablesByVersions()
        {
            try
            {
                await SqliteFunctions.RunSqliteCommand("create table if not exists VERSIONSTABLES (id integer, USER integer);");
                
                VersionsDbTables versionsDbTables;

                using (SqliteDataReader Retorno = await SqliteFunctions.RunSqliteCommand("select USER from VERSIONSTABLES"))
                {
                    Retorno.Read();

                    if (Retorno.HasRows)
                    {
                        versionsDbTables = new VersionsDbTables() { USER = Retorno.GetWithNullableInt(0) };
                    }
                    else
                    {
                        versionsDbTables = new VersionsDbTables() { USER = 0 };
                        AddorUpdateVersionDb(false, versionsDbTables);
                    }
                }

                bool updateVersionDb = false;

                if (versionsDbTables.USER < SqliteFunctions.ActualVersionsDbTables.USER)
                {
                    _ = SqliteFunctions.RunSqliteCommand("drop table if exists USER");

                    updateVersionDb = true;
                }

                if (updateVersionDb)
                    AddorUpdateVersionDb(true, SqliteFunctions.ActualVersionsDbTables);
            }
            catch (Exception ex) { throw ex; }
        }

        private static void AddorUpdateVersionDb(bool isUpdate, VersionsDbTables versionsDbTables)
        {
            string command;

            if (!isUpdate)
                command = "insert into VERSIONSTABLES(id,USER) values ('1',@USER)";
            else
                command = "update VERSIONSTABLES set USER = @USER where id = '1'";

            List<SqliteParameter> parameters = new()
            {
                new SqliteParameter("@USER", versionsDbTables.USER)
            };

            _ = SqliteFunctions.RunSqliteCommand(command, parameters);
        }
    }
}
