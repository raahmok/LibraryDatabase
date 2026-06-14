using System;
using System.Data.SQLite;

namespace SQLiteDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello Sqlite!");
            CreateConnection();
        }

        static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db;Version=3;New=True;Compress=True;");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
                Console.WriteLine("Connection worked!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connection didn't work!");
            }
            return sqlite_conn;
        }
    }
}
