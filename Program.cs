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
            current_user = new User();
            screen_num = SCREEN.LOGIN;
            break;
        case "6":
            deleteUser();
            break;
        default:
            running = false;
            break;
        }

        break;
    case SCREEN.ADMIN_MENU:
        // current_user.username ="kral hoven";
        // current_user.usr_id =123;
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
            current_user = new User();
            screen_num = SCREEN.LOGIN;
            break;
        case "6":
            deleteUser();
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
            current_user.usr_id = reader.GetInt32(1);
            current_user.username = reader.GetString(2);
            current_user.email = reader.GetString(3);
            current_user.password = reader.GetString(4);
            current_user.admin = reader.GetInt32(5);
            current_user.borrowed = reader.GetInt32(6);
            current_user.penalty = reader.GetInt32(7);
            if(current_user.admin == 1)
            {
                screen_num = SCREEN.ADMIN_MENU;
            }
            else
            {
                screen_num = SCREEN.READ_MENU;
            }

            break;
        }
        else
        {
            Console.WriteLine("Blby prihlasovaci udaje!");
        }
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

    user.usr_id = random.Next(100, 1000);

    usermanager.Add(user);
}

void deleteUser()
{
    Console.WriteLine("Opravdu skutecne si prejete trvale odstranit svuj ucet? (y/n)");
    if(Console.ReadLine() == "y")
    {
        usermanager.Delete(current_user);
        screen_num = SCREEN.LOGIN;
    }
}

void listUsers()
{
    var users = usermanager.GetAll();

    Console.WriteLine("Users: (usr_id, username, email, password, admin, borrowed_books, penalty)");

    foreach (var u in users)
    {
        Console.WriteLine($"{u.usr_id}: {u.username}, {u.email}, {u.password}, {u.admin}, {u.borrowed}, {u.penalty}");
    }
}