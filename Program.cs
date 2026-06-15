using DB;

Database.Initialize();

var usermanager = new UserManager();
var current_user = new User();
var random = new Random();
var menu  = new Menu();


var screen_num = SCREEN.LOGIN;


Console.WriteLine("|################################|");
Console.WriteLine("|##### HONZIKOVA | KNIHOVNA #####|");
Console.WriteLine("|################################|");


while (true)
{
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
            case "2":
                addUser();
                break;

            case "3":
            listUsers();
            break;
            }
        break;
    case SCREEN.READ_MENU:
        break;
    case SCREEN.ADMIN_MENU:
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