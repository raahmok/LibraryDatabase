using DB;

Database.Initialize();

var usermanager = new UserManager();
var current_user = new User();
var random = new Random();
var menu  = new Menu();


while (true)
{

    menu.init();
    Console.WriteLine("\n=== MENU ===");
    Console.WriteLine("4 - Add User");
    Console.WriteLine("5 - Login");
    Console.WriteLine("6 - List Users");
    Console.WriteLine("0 - Exit");

    Console.Write("Choice: ");
    string? choice = Console.ReadLine();

    switch (choice)
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

        case "0":
            return;

        default:
            Console.WriteLine("Invalid option.");
            break;
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