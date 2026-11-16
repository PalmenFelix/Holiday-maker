
namespace HolidayMaker;
using HolidayMaker.Services;
using HolidayMaker.Tables;

public class Menu
{
    private CustomerService customerService = new CustomerService();

    public void ShowMenu()
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("-HOLIDAY MAKER-");
            Console.WriteLine("1. Registrera kund");
            Console.WriteLine("2. Sök lediga rum");
            Console.WriteLine("3. Skapa bokning");
            Console.WriteLine("4. Visa kundens bokningar");
            Console.WriteLine("5. Ändra antal personer");
            Console.WriteLine("6. Avboka");
            Console.WriteLine("0. Avsluta");

            Console.Write("Välj ett alternativ: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    RegisterCustomer();
                    break;

                case "2":
                    Console.WriteLine("Sök lediga rum");
                    break;

                case "3":
                    Console.WriteLine("Skapa bokning");
                    break;

                case "4":
                    Console.WriteLine("Visa kundens bokningar");
                    break;

                case "5":
                    Console.WriteLine("Ändra antal personer");
                    break;

                case "6":
                    Console.WriteLine("Avboka");
                    break;

                case "0":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    break;
            }
        }
    }

    private void RegisterCustomer()
    {
        Console.WriteLine("\n-REGISTRERA KUND-");

        Console.Write("Namn: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("E-post: ");
        string email = Console.ReadLine() ?? "";

        Console.Write("Telefonnummer: ");
        string phoneNumber = Console.ReadLine() ?? "";

        Customer customer = new Customer();

        customer.Name = name;
        customer.Email = email;
        customer.PhoneNumber = phoneNumber;

        customerService.AddCustomer(customer);

        Console.WriteLine("Kunden har registrerats\n");
    }
}
