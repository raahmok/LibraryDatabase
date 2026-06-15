using System.Data.SQLite;
using DB;

namespace DB;

public class UserManager
{
    public void Add(User user)
    {
        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
            INSERT INTO Users (username, email, password, admin)
            VALUES ($username, $email, $password, $admin);
        ";

        command.Parameters.AddWithValue("$username", user.username);
        command.Parameters.AddWithValue("$email", user.email);
        command.Parameters.AddWithValue("$password", user.password);
        command.Parameters.AddWithValue("$admin", user.admin);
        command.ExecuteNonQuery();
    }

    public List<User> GetAll()
    {
        var usr_list = new List<User>();

        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Users";

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            usr_list.Add(new User
            {
                id = reader.GetInt32(0),
                usr_id = reader.GetInt32(1),
                username = reader.GetString(2),
                email = reader.GetString(3),
                password = reader.GetString(4),
                admin = reader.GetInt32(5)
            });
        }

        return usr_list;
    }
}