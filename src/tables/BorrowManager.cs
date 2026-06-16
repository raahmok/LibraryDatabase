using System.Data.SQLite;
using DB;

namespace DB;

public class BorrowManager
{
    public void Loan(int b_id, int u_id, string b_date, string r_date)
    {

        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
            INSERT INTO Borrowed (book_id, user_id, borrow_date, return_date, return_deadline)
            VALUES ($b_id, $u_id, $borrow_date, '', $return_deadline);

            UPDATE Books
            SET available = available - 1
            WHERE id = 1
            AND available > 0;

            UPDATE Users
            SET borrowed_books = borrowed_books + 1
            WHERE id = 1;
        ";

        command.Parameters.AddWithValue("$b_id", b_id);
        command.Parameters.AddWithValue("$u_id", u_id);
        command.Parameters.AddWithValue("$borrow_date", b_date);
        command.Parameters.AddWithValue("$return_deadline", r_date);
        command.ExecuteNonQuery();
    }

    public void Return( int b_id, int u_id,int borrow_id, string r_date)
    {

        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText =
        @"
            UPDATE Borrowed
            SET return_date = $r_date
            WHERE id = $borrow_id;

            UPDATE Books
            SET available = available + 1
            WHERE $b_id

            UPDATE Users
            SET borrowed = borrowed - 1
            WHERE $u_id;
        ";

        command.Parameters.AddWithValue("$b_id", b_id);
        command.Parameters.AddWithValue("$borrow_id", borrow_id);
        command.Parameters.AddWithValue("$u_id", u_id);
        command.Parameters.AddWithValue("$r_date", r_date);
        command.ExecuteNonQuery();
    }

    public List<Borrow> GetAll()
    {
        var list = new List<Borrow>();
        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Borrowed";

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new Borrow
            {
                id = reader.GetInt32(0),
                book_id = reader.GetInt32(1),
                user_id = reader.GetInt32(2),
                borrow_date = reader.GetString(3),
                return_date = reader.GetString(4),
                return_deadline = reader.GetString(5),
            });
        }

        return list;
    }

    public List<Borrow> GetAll(int user_id)
    {
        var list = new List<Borrow>();
        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM Borrowed WHERE user_id = $user_id";
        command.Parameters.AddWithValue("$user_id", user_id);


        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new Borrow
            {
                id = reader.GetInt32(0),
                book_id = reader.GetInt32(1),
                user_id = reader.GetInt32(2),
                borrow_date = reader.GetString(3),
                return_date = reader.GetString(4),
                return_deadline = reader.GetString(5),
            });
        }

        return list;
    }

    public List<Borrow> GetAllActive()
    {
        var list = new List<Borrow>();
        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = @"SELECT * FROM Borrowed WHERE return_date = NULL";

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new Borrow
            {
                id = reader.GetInt32(0),
                book_id = reader.GetInt32(1),
                user_id = reader.GetInt32(2),
                borrow_date = reader.GetString(3),
                return_date = reader.GetString(4),
                return_deadline = reader.GetString(5),
            });
        }

        return list;
    }

    public void Delete(string id)
    {
        using var connection = Database.GetConnection();
        connection.Open();
        
        var command = connection.CreateCommand();
        command.CommandText =
        @"
            DELETE FROM Borrowed WHERE id = $id;
        ";
        command.Parameters.AddWithValue("$id", id);
        command.ExecuteNonQuery();
    }
}