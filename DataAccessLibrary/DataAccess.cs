using DataAccessLibrary.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Controls;

namespace DataAccessLibrary
{
    public static class DataAccess
    {
        private static readonly string _dbname = "sqlitedb.db";
        private static readonly string _dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, _dbname);

        public static async Task InitializeDatabaseAsync()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync(_dbname, CreationCollisionOption.OpenIfExists);
            Console.WriteLine($"dbPath = {_dbpath}");

            using (var db = new SqliteConnection($"Filename={_dbpath}"))
            {
                db.Open();

                var query = "CREATE TABLE IF NOT EXISTS Persons (Id INTEGER PRIMARY KEY, Name NVARCHAR(2048) NULL)";
                var cmd = new SqliteCommand(query, db);

                cmd.ExecuteReader();
                db.Close();
            }
        }

        public static async Task Addsync(string input)
        {
            using (var db = new SqliteConnection($"Filename={_dbpath}"))
            {
                db.Open();

                var query = "INSERT INTO Persons (Id,Name) VALUES(Null, @Name)";
                var cmd = new SqliteCommand(query, db);

                cmd.Parameters.AddWithValue("@Name", input);

                await cmd.ExecuteReaderAsync();
                db.Close();
            }

        }

        public static IEnumerable<string> GetAllAsync()
        {
            var list = new List<string>();

            using (var db = new SqliteConnection($"Filename={_dbpath}"))
            {
                db.Open();

                var query = "SELECT Name FROM Persons";
                var cmd = new SqliteCommand(query, db);

                var result = cmd.ExecuteReader();


                while (result.Read())
                {
                    list.Add(result.GetString(0));
                }

                db.Close();

                return list;
            }
        }

        //public static async Task<Person> GetAsync(int id)
        //{
        //    using (var db = new SqliteConnection($"Filename={_dbpath}"))
        //    {
        //        db.Open();

        //        var query = "SELECT * FROM Persons WHERE Id = @Id";
        //        var cmd = new SqliteCommand(query, db);
        //        cmd.Parameters.AddWithValue("@Id", id);

        //        Person result = (Person)await cmd.ExecuteScalarAsync();

        //        db.Close();

        //        return result;
        //    }
        //}
    }
    
}
