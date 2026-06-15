using System.Data.SqlClient;
using System.Data.SQLite;

namespace DB;

public static class Database
{
    private const string ConnectionString = "Data Source=database.db";

    public static SQLiteConnection GetConnection()
    {
        return new SQLiteConnection(ConnectionString);
    }

    public static void Initialize()
    {
        using var connection = GetConnection();
        connection.Open();

        string sql2 = @"
            CREATE TABLE IF NOT EXISTS Books (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                isbn TEXT NOT NULL,
                title TEXT NOT NULL,
                author TEXT NOT NULL,
                year INTEGER,
                genre TEXT NOT NULL,
                copies INTEGER,
                available INTEGER
            );

            CREATE TABLE IF NOT EXISTS Users (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                usr_id INTEGER UNIQUE NOT NULL,
                username TEXT NOT NULL UNIQUE,
                email TEXT NOT NULL UNIQUE,
                password TEXT NOT NULL,
                admin INTEGER,
                borrowed INTEGER,
                penalty INTEGER
            );

            CREATE TABLE IF NOT EXISTS Borrowed (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            book_id INTEGER,
            member_id INTEGER,
            borrow_date DATE,
            return_date DATE,
            return_deadline DATE,
            FOREIGN KEY (book_id) REFERENCES Books (id),
            FOREIGN KEY (member_id) REFERENCES Users (id)
            )
        ";

        var command = connection.CreateCommand();
        var sql_path = "init.sql";
        var sql = File.ReadAllText(sql_path);
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}