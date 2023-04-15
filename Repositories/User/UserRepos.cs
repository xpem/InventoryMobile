using Microsoft.Data.Sqlite;

namespace LocalDBRepos.User
{
    public static class UserRepos
    {
        public static void InsertUser(Models.User user)
        {
            SQLiteDbFunctions.OpenIfClosed();

            List<SqliteParameter> parameters = new()
            {
                new SqliteParameter("@TOKEN", user.Token),
                new SqliteParameter("@NAME", user.Name),
                new SqliteParameter("@EMAIL", user.Email),
                new SqliteParameter("@PASSWORD", user.Password),
                new SqliteParameter("@LASTUPDATE", DateTime.MinValue),
            };

            _ = SQLiteDbFunctions.RunSqliteCommand("delete from USER");

            _ = SQLiteDbFunctions.RunSqliteCommand("insert into USER (TOKEN,NAME,EMAIL,PASSWORD,LASTUPDATE) values (@TOKEN,@NAME,@EMAIL,@PASSWORD,@LASTUPDATE)", parameters);

            SQLiteDbFunctions.CloseIfOpen();
        }


        public static Models.User? GetUser()
        {
            SQLiteDbFunctions.OpenIfClosed();

            SqliteDataReader ret = Task.Run(async () => await SQLiteDbFunctions.RunSqliteCommand("select Id,token,email,password,lastUpdate from USER")).Result;
            ret.Read();

            if (ret.HasRows)
            {
                Models.User user = new()
                {
                    Id = ret.GetWithNullableString(0),
                    Token = ret.GetWithNullableString(1),
                    Email = ret.GetWithNullableString(2),
                    Password = ret.GetWithNullableString(3),
                    LastUpdate = ret.GetDateTime(4),
                };

                SQLiteDbFunctions.CloseIfOpen();

                return user;
            }
            else
            {
                SQLiteDbFunctions.CloseIfOpen();
                return null;
            }
        }
    }
}
