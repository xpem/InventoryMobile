using Microsoft.Data.Sqlite;
using Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDBRepos
{
    public static class BuildDbRepos
    {
        /// <summary>
        /// create or update the structure of SQLite tables by actual version defined
        /// </summary>
        public static void BuildDb()
        {
            SQLiteDbFunctions.OpenIfClosed();

            UpdateSQLiteTablesByVersions();

            _ = SQLiteDbFunctions.RunSqliteCommand("create table if not exists USER (ID integer primary key autoincrement,NAME text, EMAIL text, UID text, TOKEN text,PASSWORD text, LASTUPDATE datetime);");
            //_ = SQLiteDB.RunSqliteCommand("create table if not exists BOOK (ID integer,LOCAL_TEMP_ID text, UID text, TITLE text, SUBTITLE text, AUTHORS text, " +
            //    "YEAR integer, VOLUME text, PAGES integer, ISBN text, GENRE text, UPDATED_AT datetime, INACTIVE integer, STATUS integer," +
            //    " COVER text, GOOGLE_ID text, SCORE integer, COMMENT text, CREATED_AT datetime);");
            _ = SQLiteDbFunctions.RunSqliteCommand("create table if not exists VERSIONDB (USER integer);");

            SQLiteDbFunctions.CloseIfOpen();
        }

        /// <summary>
        /// delete tables of old versions and recreate it
        /// </summary>
        private static async void UpdateSQLiteTablesByVersions()
        {

            _ = SQLiteDbFunctions.RunSqliteCommand("create table if not exists VERSIONSTABLES (Key integer, USER integer);");
            VersionsDbTables versionsDbTables;

            using (SqliteDataReader Retorno = await SQLiteDbFunctions.RunSqliteCommand("select USER from VERSIONSTABLES"))
            {
                _ = Retorno.Read();

                if (Retorno.HasRows)
                {
                    versionsDbTables = new VersionsDbTables()
                    {
                        USER = Retorno.GetWithNullableInt(0)
                    };

                }
                else
                {
                    versionsDbTables = new VersionsDbTables()
                    {
                        USER = 0
                    };
                    AddorUpdateVersionDb(false, versionsDbTables);
                }
            }
            bool updateVersionDb = false;

            if (versionsDbTables.USER < SQLiteDbFunctions.ActualVersionsDbTables.USER)
            {
                _ = SQLiteDbFunctions.RunSqliteCommand("drop table if exists USER");

                updateVersionDb = true;
            }

            if (updateVersionDb)
                AddorUpdateVersionDb(true, SQLiteDbFunctions.ActualVersionsDbTables);
        }

        private static void AddorUpdateVersionDb(bool isUpdate, VersionsDbTables versionsDbTables)
        {
            string command;

            if (!isUpdate)
            {
                command = "insert into VERSIONSTABLES(key,USER) values ('1',@USER)";
            }
            else
            {
                command = "update VERSIONSTABLES set USER = @USER where key = '1'";
            }

            List<SqliteParameter> parameters = new()
            {
                new SqliteParameter("@USER", versionsDbTables.USER)
            };

            _ = SQLiteDbFunctions.RunSqliteCommand(command, parameters);
        }
    }
}
