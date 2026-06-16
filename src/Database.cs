using System.Data.SQLite;

namespace DB;

public static class Database
{
    private const string ConnectionString = "Data Source=sql/database.db";

    public static SQLiteConnection GetConnection()
    {
        return new SQLiteConnection(ConnectionString);
    }

    public static void Initialize()
    {
        using var connection = GetConnection();
        connection.Open();
        var command = connection.CreateCommand();
        var sql_path = "sql/init.sql";
        var sql = File.ReadAllText(sql_path);
        command.CommandText = sql;
        command.ExecuteNonQuery();
    }
}