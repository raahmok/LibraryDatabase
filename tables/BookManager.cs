using System.Data.SQLite;
using DB;

namespace DB;

public class BookManager
{
    public void Add(Book book)
    {

        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
            INSERT INTO Books (isbn, title, author, year, genre, copies, available)
            VALUES ($isbn, $title, $author, $year, $genre, $copies, $available);
        ";

        command.Parameters.AddWithValue("$isbn", book.isbn);
        command.Parameters.AddWithValue("$title", book.title);
        command.Parameters.AddWithValue("$author", book.author);
        command.Parameters.AddWithValue("$year", book.year);
        command.Parameters.AddWithValue("$genre", book.genre);
        command.Parameters.AddWithValue("$copies", book.copies);
        command.Parameters.AddWithValue("$available", book.available);
        command.ExecuteNonQuery();
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

    public void Delete(string isbn)
    {
        using var connection = Database.GetConnection();
        connection.Open();
        
        var command = connection.CreateCommand();
        command.CommandText =
        @"
            DELETE FROM Books WHERE isbn = $isbn;
        ";
        command.Parameters.AddWithValue("$isbn", isbn);
        command.ExecuteNonQuery();
    }
}