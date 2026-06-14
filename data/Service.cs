using System.Data.SQLite;
using DB;

namespace DB;

public class ItemService
{
    public void Add(string name)
    {
        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
            INSERT INTO Items (Name)
            VALUES ($name);
        ";

        command.Parameters.AddWithValue("$name", name);
        command.ExecuteNonQuery();
    }

    public List<Item> GetAll()
    {
        var items = new List<Item>();

        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Items";

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            items.Add(new Item
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            });
        }

        return items;
    }

    public void Delete(int id)
    {
        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
            DELETE FROM Items
            WHERE Id = $id;
        ";

        command.Parameters.AddWithValue("$id", id);
        command.ExecuteNonQuery();
    }
}