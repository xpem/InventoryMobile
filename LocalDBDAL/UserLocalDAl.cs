using Microsoft.Data.Sqlite;
using Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocalDbDAL
{
    public static class UserLocalDAl
    {
        public static void AddUser(User user)
        {
            SqliteFunctions.OpenIfClosed();

            List<SqliteParameter> parameters = new()
            {
                new SqliteParameter("@TOKEN", user.Token),
                new SqliteParameter("@NAME", user.Name),
                new SqliteParameter("@EMAIL", user.Email),
                new SqliteParameter("@PASSWORD", user.Password),
                new SqliteParameter("@LASTUPDATE", DateTime.MinValue),
            };

            _ = SqliteFunctions.RunSqliteCommand("delete from USER");

            _ = SqliteFunctions.RunSqliteCommand("insert into USER (TOKEN,NAME,EMAIL,PASSWORD,LASTUPDATE) values (@TOKEN,@NAME,@EMAIL,@PASSWORD,@LASTUPDATE)", parameters);

            SqliteFunctions.CloseIfOpen();
        }

        public async static void CleanUserDatabase()
        {
            SqliteFunctions.OpenIfClosed();

            //clean local database
            _ = await SqliteFunctions.RunSqliteCommand("delete from USER");

            SqliteFunctions.CloseIfOpen();
        }

        public static async Task<User?> GetUser()
        {
            try
            {
                SqliteFunctions.OpenIfClosed();

                SqliteDataReader ret = await SqliteFunctions.RunSqliteCommand("select ID,TOKEN,EMAIL,PASSWORD from USER");

                if (ret.HasRows)
                {
                    ret.Read();
                    User user = new()
                    {
                        Id = ret.GetInt32(0),
                        Token = ret.GetWithNullableString(1),
                        Email = ret.GetString(2),
                        Password = ret.GetString(3),
                    };

                    SqliteFunctions.CloseIfOpen();

                    return user;
                }
                else
                {
                    SqliteFunctions.CloseIfOpen();
                    return null;
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static async Task<string> GetUserToken()
        {
            SqliteFunctions.OpenIfClosed();

            SqliteDataReader ret = await SqliteFunctions.RunSqliteCommand("select TOKEN from USER");

                ret.Read();
            if (ret.HasRows)
            {
                string token = ret.GetString(0);

                SqliteFunctions.CloseIfOpen();

                return token;
            }
            else
            {
                SqliteFunctions.CloseIfOpen();
                return null;
            }
        }

        public static void UpdateToken(int id, string token)
        {
            try
            {
                SqliteFunctions.OpenIfClosed();

                List<SqliteParameter> parameters = new() { new SqliteParameter("@Id", id), new SqliteParameter("@token", token) };

                _ = SqliteFunctions.RunSqliteCommand("update USER set TOKEN = @token where ID = @Id", parameters);

                SqliteFunctions.CloseIfOpen();
            }
            catch (Exception) { throw; }
        }
    }
}
