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

        string sql = @"
            CREATE TABLE IF NOT EXISTS Items (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL
            );
        ";

        var command = connection.CreateCommand();
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}