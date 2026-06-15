using DB;

Database.Initialize();

var usermanager = new UserManager();
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
        Console.WriteLine("2 - pridat uzivatele");
        Console.WriteLine("3 - zobrazit uzivatele");
        Console.WriteLine("0 - Odejit");

        switch(Console.ReadLine())
        {
        case "1":
            login();
            break;
        case "2":
            addUser();
            break;
        case "3":
            listUsers();
            break;
        default:
            running = false;
            break;
        }
        break;
    case SCREEN.READ_MENU:
        Console.WriteLine($"Prihlasen - {current_user.username}#{current_user.usr_id}");
        Console.WriteLine(current_user.username);
        Console.WriteLine("1 - Prohlizet katalog");
        Console.WriteLine("2 - Vypujcit knihu");
        Console.WriteLine("3 - Vratit knihu");
        Console.WriteLine("5 - Odhlasit");
        Console.WriteLine("6 - Smazat ucet");
        Console.WriteLine("0 - Odejit");

        switch(Console.ReadLine())
        {
        case "1":
            break;
        case "2":
            break;
        case "3":
            break;
        case "4":
            break;
        case "5":
            break;
        case "6":
            break;
        default:
            running = false;
            break;
        }

        break;
    case SCREEN.ADMIN_MENU:
        Console.WriteLine($"Prihlasen - {current_user.username}#{current_user.usr_id} - (admin)");

        Console.WriteLine("1 - Prohlizet katalog");
        Console.WriteLine("2 - Vypujcit knihu");
        Console.WriteLine("3 - Vratit knihu");
        Console.WriteLine("5 - Odhlasit");
        Console.WriteLine("6 - Smazat ucet");
        Console.WriteLine("0 - Odejit");

        switch(Console.ReadLine())
        {
        case "1":
            break;
        case "2":
            break;
        case "3":
            break;
        case "4":
            break;
        case "5":
            break;
        case "6":
            break;
        default:
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
    command.CommandText =
    @"
        SELECT username FROM Users;
    ";

    using var reader = command.ExecuteReader();

    var db_username = "";

    while(reader.Read())
    {
        if(login_username == reader.GetString(0))
        {
            db_username = reader.GetString(0);
            break;
        }
    }

    command = connection.CreateCommand();
    command.CommandText =
    @"
        SELECT password FROM Users WHERE username = $username;
    ";
    command.Parameters.AddWithValue("$username", db_username);

    using var reader2 = command.ExecuteReader();
    reader.Read();
    if(reader.GetString(0) == login_password)
    {
        //succesful login

        command = connection.CreateCommand();
        command.CommandText =
        @"
            SELECT username FROM Users;
        ";
        
        using var reader3 = command.ExecuteReader();
        reader.Read();
        current_user = new User
        {
            id = reader.GetInt32(0),
            usr_id = reader.GetInt32(1),
            username = reader.GetString(2),
            email = reader.GetString(3),
            password = reader.GetString(4),
            admin = reader.GetInt32(5)
        };
    }


    if(current_user.admin == 1)
    {
        screen_num = SCREEN.ADMIN_MENU;
    }
    else
    {
        screen_num = SCREEN.ADMIN_MENU;
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

    usermanager.Add(user);
}

void listUsers()
{
    var users = usermanager.GetAll();

    Console.WriteLine("\nItems: (id, username, email, password, admin)");

    foreach (var u in users)
    {
        Console.WriteLine($"{u.id}: {u.username}, {u.email}, {u.password}, {u.admin}");
    }
}