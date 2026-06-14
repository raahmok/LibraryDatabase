using DB;

Database.Initialize();

var service = new ItemService();

while (true)
{
    Console.WriteLine("\n=== MENU ===");
    Console.WriteLine("1 - Add item");
    Console.WriteLine("2 - List items");
    Console.WriteLine("3 - Delete item");
    Console.WriteLine("0 - Exit");

    Console.Write("Choice: ");
    string? choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            Console.Write("Item name: ");
            string? name = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(name))
            {
                service.Add(name);
                Console.WriteLine("Added.");
            }
            break;

        case "2":
            var items = service.GetAll();

            Console.WriteLine("\nItems:");

            foreach (var item in items)
            {
                Console.WriteLine($"{item.Id}: {item.Name}");
            }

            break;

        case "3":
            Console.Write("ID to delete: ");

            if (int.TryParse(Console.ReadLine(), out int id))
            {
                service.Delete(id);
                Console.WriteLine("Deleted.");
            }

            break;

        case "0":
            return;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}