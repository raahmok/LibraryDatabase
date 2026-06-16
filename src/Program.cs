using System.Data;
using DB;

Database.Initialize();

var usermanager = new UserManager();
var bookmanager = new BookManager();
var borrowmanager = new BorrowManager();
var current_user = new User();
var random = new Random();
var menu  = new Menu();


bool running = true;
var screen_num = SCREEN.LOGIN;


Console.WriteLine("|################################|");
Console.WriteLine("|##### HONZIKOVA | KNIHOVNA #####|");
Console.WriteLine("|################################|");


while (running)
{
    Console.WriteLine("---------------------------------------");
    switch(screen_num)
    {
    case SCREEN.LOGIN:
        Console.WriteLine("Vitejte!");
        Console.WriteLine("1 - Prihlasit se");
        Console.WriteLine("2 - Registrace");
        Console.WriteLine("0 - Odejit");

        switch(Console.ReadLine())
        {
        case "1":
            login();
            break;
        case "2":
            addUser();
            break;
        case "0":
            running = false;
            break;
        }
        break;
    case SCREEN.MENU:
        if(current_user.admin == 1) Console.WriteLine($"Prihlasen - {current_user.username}#{current_user.user_id} - (admin)");
        else Console.WriteLine($"Prihlasen - {current_user.username}#{current_user.user_id}");
        Console.WriteLine($"{current_user.borrowed} - pujcenych knih");
        Console.WriteLine($"#######################################");
        Console.WriteLine("1 - Prohlizet katalog");
        Console.WriteLine("2 - Vypujcit knihu");
        Console.WriteLine("3 - Moje Vypujcky");
        Console.WriteLine("4 - Vratit knihu");
        Console.WriteLine("5 - Odhlasit");
        Console.WriteLine("6 - Smazat ucet");
        if(current_user.admin == 1) Console.WriteLine("7 - Pokrocile");
        Console.WriteLine("0 - Odejit");

        switch(Console.ReadLine())
        {
        case "1":
            listBooks();
            break;
        case "2":
            loanBook();
            break;
        case "3":
            listMyLoans();
            break;
        case "4":
            returnBook();
            break;
        case "5":
            current_user = new User();
            screen_num = SCREEN.LOGIN;
            break;
        case "6":
            Console.WriteLine("Opravdu skutecne si prejete trvale odstranit svuj ucet? (y/n)");
            if(Console.ReadLine() == "y")
            {
                usermanager.Delete(current_user);
                screen_num = SCREEN.LOGIN;
            }
            break;
        case "7":
            Console.WriteLine("Zadejte pristupove heslo:");
            if(Console.ReadLine() == "chlebasmaslem" && current_user.admin == 1)
            {
                screen_num = SCREEN.ADMIN_MENU;
            }
            break;
        case "0":
            running = false;
            break;
        }

        break;

    case SCREEN.ADMIN_MENU:
        Console.WriteLine($"Pokrocile moznosti");
        Console.WriteLine($"##################");
        Console.WriteLine("C - Zadat prikaz");
        Console.WriteLine("S - Filtrovat databaze");
        Console.WriteLine("R - Resetovat databazi");
        Console.WriteLine("1 - Vypsat uzivatele");
        Console.WriteLine("2 - Pridat uzivatele");
        Console.WriteLine("3 - Smazat uzivatele");
        Console.WriteLine("4 - Prohlizet katalog");
        Console.WriteLine("5 - Pridat knihu");
        Console.WriteLine("6 - Smazat knihu");
        Console.WriteLine("7 - Aktivni vypujcky");
        Console.WriteLine("8 - Vsechny vypujcky");
        Console.WriteLine("9 - Zpet");
        Console.WriteLine("0 - Odejit");

        switch(Console.ReadLine())
        {
        case "C":
            customCommand();
            break;
        case "S":
            Console.WriteLine("Zatim nefunguje :(");
            break;
        case "R":
            ResetDB();
            break;
        case "1":
            listUsers();
            break;
        case "2":
            addUser();
            break;
        case "3":
            deleteUser();
            break;
        case "4":
            listBooks();
            break;
        case "5":
            addBook();
            break;
        case "6":
            deleteBook();
            break;
        case "7":
            listActiveLoans();
            break;
        case "8":
            listAllLoans();
            break;
        case "9":
            screen_num = SCREEN.MENU;
            break;
        case "0":
            running = false;
            break;
        }
        break;
    }
}

void login()
{
    Console.Write("Enter username: ");
    var login_username = Console.ReadLine();
    
    Console.Write("Enter password: ");
    var login_password = Console.ReadLine();

    
    using var connection = Database.GetConnection();
    connection.Open();

    var command = connection.CreateCommand();
    //TODO pridat check jestli username existuje
    command.CommandText =
    @"
        SELECT * FROM Users WHERE username = $username;
    ";
    command.Parameters.AddWithValue("$username", login_username);

    using var reader = command.ExecuteReader();


    while(reader.Read())
    {
        if(reader.GetString(2) == login_username &&
           reader.GetString(4) == login_password)
        {
            //succesful login
            current_user.id = reader.GetInt32(0);
            current_user.user_id = reader.GetInt32(1);
            current_user.username = reader.GetString(2);
            current_user.email = reader.GetString(3);
            current_user.password = reader.GetString(4);
            current_user.admin = reader.GetInt32(5);
            current_user.borrowed = reader.GetInt32(6);
            current_user.penalty = reader.GetInt32(7);

            screen_num = SCREEN.MENU;

            break;
        }
        else
        {
            Console.WriteLine("Blby prihlasovaci udaje!");
        }
    }
}

void listUsers()
{
    var users = usermanager.GetAll();

    Console.WriteLine("Users: (user_id, username, email, password, admin, borrowed_books, penalty)");

    foreach (var u in users)
    {
        Console.WriteLine($"{u.user_id}: {u.username}, {u.email}, {u.password}, {u.admin}, {u.borrowed}, {u.penalty}");
    }
}

void addUser()
{
    var user = new User();
    Console.Write("Enter username: ");
    user.username = Console.ReadLine();
    Console.Write("Enter email: ");
    user.email = Console.ReadLine();
    Console.Write("Enter very stong password: ");
    user.password = Console.ReadLine();

    Console.WriteLine("Do you want admin privileges?: (y/n)");
    var ans = Console.ReadLine();
    if (ans == "y")
    {
        for(int i = 0; i < 3; i++)
        {
            Console.WriteLine("Enter aministrator passphrase: ");
            if(Console.ReadLine() == "chlebasmaslem")
            {
                user.admin = 1;
                break; 
            }
        }
    }
    else
    {
        user.admin = 0;
    }

    user.user_id = random.Next(100, 1000);

    usermanager.Add(user);
}

void deleteUser()
{
    Console.WriteLine("zadejte user_id uzivatele ktery bude odstranen:");
    var delete_id = int.Parse(Console.ReadLine());
    usermanager.Delete(delete_id);
}

void listBooks()
{
    var books = bookmanager.GetAll();

    Console.WriteLine("##################################");
    Console.WriteLine("########## KATALOG KNIH ##########");
    Console.WriteLine("##################################");
    Console.WriteLine("");
    Console.WriteLine("| isbn | title | author | year of release | genre | copies | available copies");

    foreach (var b in books)
    {
        Console.WriteLine($"| {b.isbn} | {b.title} | {b.author} | {b.year} | {b.genre} | {b.copies} | {b.available} |");
    }
}

void addBook()
{
    var book = new Book();
    Console.Write("Enter title: ");
    book.title = Console.ReadLine();
    Console.Write("Enter Author: ");
    book.author = Console.ReadLine();
    Console.Write("Enter year of release: ");
    book.year = int.Parse(Console.ReadLine());
    Console.Write("Enter genre: ");
    book.genre = Console.ReadLine();
    Console.Write("Enter number of copies: ");
    book.copies = int.Parse(Console.ReadLine());
    book.available = book.copies;

    string rand_isbn = "";
    
    for(int i = 0; i < 13; i++)
    {
        rand_isbn += random.Next(-1, 10);
    }

    book.isbn = rand_isbn;

    bookmanager.Add(book);
}

void deleteBook()
{
    Console.WriteLine("zadejte isbn knihy ktera bude odstranena:");
    var delete_id = Console.ReadLine();
    bookmanager.Delete(delete_id);
}

void customCommand()
{
    Console.WriteLine("Zadejte prikaz ve spravnem formatu SQL:");
    var sql = Console.ReadLine();

    using var connection = Database.GetConnection();
    connection.Open();
    
    var command = connection.CreateCommand();
    command.CommandText = sql;
    command.ExecuteNonQuery();
}

void ResetDB()
{
    Console.WriteLine("Skutecne???");
    if(Console.ReadLine() != "y") return;
    Console.WriteLine("Vopravdu???");
    if(Console.ReadLine() != "y") return;
    Console.WriteLine("Fakt jo???");
    if(Console.ReadLine() != "y") return;
    Console.WriteLine("oukej");
    using var connection = Database.GetConnection();
    connection.Open();
    var command = connection.CreateCommand();
    var sql_path = "sql/generate.sql";
    var sql = File.ReadAllText(sql_path);
    command.CommandText = sql;
    command.ExecuteNonQuery();
}

// void selectDB()
// {
//     Console.WriteLine("Zadejte prikaz ve spravnem formatu SQL pomoci SELECT:");
//     var sql = Console.ReadLine();

//     using var connection = Database.GetConnection();
//     connection.Open();
    
//     var command = connection.CreateCommand();
//     command.CommandText = sql;
//     using var reader = command.ExecuteReader();

//     while (reader.Read())
//     {
//         ReadRow((IDataRecord)reader);
//     }
// }


// void ReadRow(IDataRecord dataRecord)
// {
//     dataRecord.GetData
//     foreach(var d in dataRecord)
//     {  
//         Console.Write(String.Format("| {0} ", d));
//         Console.WriteLine("|");
//     }
// }

void loanBook()
{
    Console.WriteLine("Zadejte isbn knihy kterou si prejete pujcit:");
    int loan_isbn =int.Parse(Console.ReadLine());


    // using var connection = Database.GetConnection();
    // connection.Open();

    // var loan = connection.CreateCommand();
    // loan.CommandText =
    // @"
    //     SELECT id FROM Books WHERE isbn = $isbn;
    // ";

    // loan.Parameters.AddWithValue("$isbn", loan_isbn);

    // using var reader = loan.ExecuteReader();
    // reader.Read();
    // var book_id = reader.GetInt32(0);
    //Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd"));

    //borrowmanager.Add(book_id, current_user.id, DateTime.Now.ToString("yyyy-MM-dd"),  DateTime.Now.AddDays(30).ToString("yyyy-MM-dd"));
    borrowmanager.Loan(loan_isbn, current_user.id, DateTime.Now.ToString("yyyy-MM-dd"),  DateTime.Now.AddDays(30).ToString("yyyy-MM-dd"));
}

void returnBook()
{
    Console.WriteLine("Zadejte isbn knihy kterou si prejete vratit:");
    string loan_isbn = Console.ReadLine();


    using var connection = Database.GetConnection();
    connection.Open();
    var command = connection.CreateCommand();
    command.CommandText =
    @"
        SELECT id FROM Books WHERE isbn = $isbn;
    ";
    command.Parameters.AddWithValue("$isbn", loan_isbn);
    using var reader = command.ExecuteReader();
    reader.Read();
    var book_id = reader.GetInt32(0);

    command = connection.CreateCommand();
    command.CommandText =
    @"
        SELECT id FROM Borrowed WHERE book_id = $book_id AND user_id = $user_id;
    ";
    command.Parameters.AddWithValue("$book_id", book_id);
    command.Parameters.AddWithValue("$user_id", current_user.id);
    using var reader2 = command.ExecuteReader();
    reader2.Read();

    borrowmanager.Return(book_id, current_user.id, reader.GetInt32(0), DateTime.Now.ToString("yyyy-MM-dd"));
}

void listMyLoans()
{
    var loans = borrowmanager.GetAll(current_user.id);

    Console.WriteLine("###################################");
    Console.WriteLine("########## VASE VYPUJCKY ##########");
    Console.WriteLine("###################################");
    Console.WriteLine("");
    Console.WriteLine("| isbn | title | author | loan_date | return_date | return_deadline ");

    foreach (var l in loans)
    {
        using var connection = Database.GetConnection();
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = "SELECT isbn, title, author FROM Books WHERE id = $book_id";
        command.Parameters.AddWithValue("$book_id", l.book_id);

        using var reader = command.ExecuteReader();
        reader.Read();

        Console.WriteLine($"| {reader.GetString(0)} | {reader.GetString(1)} | {reader.GetString(2)} | {l.borrow_date} | {l.return_date} | {l.return_deadline} |");
    }
}

void listAllLoans()
{
    var loans = borrowmanager.GetAll();

    Console.WriteLine("######################################");
    Console.WriteLine("########## VSECHNY VYPUJCKY ##########");
    Console.WriteLine("######################################");
    Console.WriteLine("");
    Console.WriteLine("| id | book id | user id | loan date | return date | return deadline |");

    foreach (var l in loans)
    {
        Console.WriteLine($"| {l.id} | {l.book_id} | {l.user_id} | {l.borrow_date} | {l.return_date} | {l.return_deadline} |");
    }
}

void listActiveLoans()
{
    var loans = borrowmanager.GetAllActive();

    Console.WriteLine("######################################");
    Console.WriteLine("########## AKTIVNI VYPUJCKY ##########");
    Console.WriteLine("######################################");
    Console.WriteLine("");
    Console.WriteLine("| id | book id | user id | loan date | return date | return deadline |");

    foreach (var l in loans)
    {
        Console.WriteLine($"| {l.id} | {l.book_id} | {l.user_id} | {l.borrow_date} | {l.return_date} | {l.return_deadline} |");
    }
}