using System.Data.SQLite;
using DB;

namespace DB;

public class BookManager
{
    public void Add(Book book)
    {
        //npotrebuju pridavat knizky asi

        // using var connection = Database.GetConnection();
        // connection.Open();

        // var command = connection.CreateCommand();
        // command.CommandText =
        // @"
        //     INSERT INTO Users (usr_id, username, email, password, admin, borrowed_books, penalty)
        //     VALUES ($usr_id, $username, $email, $password, $admin, $borrowed, $penalty);
        // ";

        // command.Parameters.AddWithValue("$usr_id", user.usr_id);
        // command.Parameters.AddWithValue("$username", user.username);
        // command.Parameters.AddWithValue("$email", user.email);
        // command.Parameters.AddWithValue("$password", user.password);
        // command.Parameters.AddWithValue("$admin", user.admin);
        // command.Parameters.AddWithValue("$borrowed", user.borrowed);
        // command.Parameters.AddWithValue("$penalty", user.penalty);
        // command.ExecuteNonQuery();
    }

    public List<Book> GetAll()
    {
        var list = new List<Book>();

        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Books";

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new Book
            {
                id = reader.GetInt32(0),
                isbn = reader.GetString(1),
                title = reader.GetString(2),
                author = reader.GetString(3),
                year = reader.GetInt32(4),
                genre = reader.GetString(5),
                copies = reader.GetInt32(6),
                available = reader.GetInt32(7)
            });
        }

        return list;
    }

    public void Delete(Book book)
    {
        using var connection = Database.GetConnection();
        connection.Open();
        
        var command = connection.CreateCommand();
        command.CommandText =
        @"
            DELETE FROM Books WHERE id = $id;
        ";
        command.Parameters.AddWithValue("$id", book.id);
        command.ExecuteNonQuery();
    }
}