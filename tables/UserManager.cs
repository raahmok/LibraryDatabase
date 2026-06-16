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
            INSERT INTO Users (usr_id, username, email, password, admin, borrowed_books, penalty)
            VALUES ($usr_id, $username, $email, $password, $admin, $borrowed, $penalty);
        ";

        command.Parameters.AddWithValue("$usr_id", user.usr_id);
        command.Parameters.AddWithValue("$username", user.username);
        command.Parameters.AddWithValue("$email", user.email);
        command.Parameters.AddWithValue("$password", user.password);
        command.Parameters.AddWithValue("$admin", user.admin);
        command.Parameters.AddWithValue("$borrowed", user.borrowed);
        command.Parameters.AddWithValue("$penalty", user.penalty);
        command.ExecuteNonQuery();
    }

    public List<User> GetAll()
    {
        var list = new List<User>();

        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Users";

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new User
            {
                id = reader.GetInt32(0),
                usr_id = reader.GetInt32(1),
                username = reader.GetString(2),
                email = reader.GetString(3),
                password = reader.GetString(4),
                admin = reader.GetInt32(5),
                borrowed = reader.GetInt32(6),
                penalty = reader.GetInt32(7)
            });
        }

        return list;
    }

    public void Delete(User user)
    {
        using var connection = Database.GetConnection();
        connection.Open();
        
        var command = connection.CreateCommand();
        command.CommandText =
        @"
            DELETE FROM Users WHERE id = $id;
        ";
        command.Parameters.AddWithValue("$id", user.id);
        command.ExecuteNonQuery();
    }

    public void Delete(int usr_id)
    {
        using var connection = Database.GetConnection();
        connection.Open();
        
        var command = connection.CreateCommand();
        command.CommandText =
        @"
            DELETE FROM Users WHERE usr_id = $usr_id;
        ";
        command.Parameters.AddWithValue("$id", usr_id);
        command.ExecuteNonQuery();
    }
}