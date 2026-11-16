
namespace HolidayMaker;
using HolidayMaker.Services;
using HolidayMaker.Tables;
using HolidayMaker.DataAccess;

public class Menu
{
    private CustomerService customerService = new CustomerService();
    private BookingService bookingService = new BookingService();
    private HotelService hotelService = new HotelService();
    private HotelDataAccess hotelDataAccess = new HotelDataAccess();
    private RoomDataAccess roomDataAccess = new RoomDataAccess();

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
            Console.WriteLine("7. Visa alla kunder");
            Console.WriteLine("8. Visa alla hotell");
            Console.WriteLine("0. Avsluta");

            Console.Write("Välj ett alternativ: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    RegisterCustomer();
                    break;
                case "2":
                    SearchAvailableRooms();
                    break;
                case "3":
                    CreateBooking();
                    break;
                case "4":
                    ShowBookings();
                    break;
                case "5":
                    UpdatePeople();
                    break;
                case "6":
                    CancelBooking();
                    break;
                case "7":
                    ShowAllCustomers();
                    break;
                case "8":
                    ShowAllHotels();
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

    private int ReadInt(string message)
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine() ?? "";

            if (int.TryParse(input, out int number))
            {
                return number;
            }

            Console.WriteLine("Felaktig inmatning. Ange ett heltal.");
        }
    }

    private DateTime ReadDate(string message)
    {
        while (true)
        {
            Console.Write(message);
            string input = Console.ReadLine() ?? "";

            if (DateTime.TryParse(input, out DateTime date))
            {
                return date;
            }

            Console.WriteLine("Felaktigt datum. Försök igen.");
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

    private void SearchAvailableRooms()
    {
        Console.WriteLine("\n-SÖK LEDIGA RUM-");

        Console.Write("Ort: ");
        string location = Console.ReadLine() ?? "";

        DateTime checkIn = ReadDate("Incheckning (ÅÅÅÅ-MM-DD): ");
        DateTime checkOut = ReadDate("Utcheckning (ÅÅÅÅ-MM-DD): ");

        int people = ReadInt("Antal personer: ");

        if (checkOut <= checkIn || people <= 0)
        {
            Console.WriteLine("Ogiltiga uppgifter");
            return;
        }

        List<Rooms> rooms = bookingService.SearchAvailableRooms(
            location, checkIn, checkOut, people,
            false, false, -1, -1, ""
        );

        List<Hotel> hotels = hotelDataAccess.GetAllHotels();

        if (rooms.Count == 0)
        {
            Console.WriteLine("Inga lediga rum hittades");
            return;
        }

        foreach (Rooms room in rooms)
        {
            foreach (Hotel hotel in hotels)
            {
                if (room.HotelID == hotel.ID)
                {
                    Console.WriteLine(
                        $"Hotell: {hotel.Name}, Ort: {hotel.Location}, " +
                        $"Betyg: {hotel.Rating}/5, " +
                        $"Rum: {room.RoomNumber}, Kapacitet: {room.MaxPersons}, " +
                        $"Pris per natt: {room.Price} kr, Rummets ID: {room.ID}"
                    );
                }
            }
        }
    }

    private void CreateBooking()
    {
        Console.WriteLine("\n-SKAPA BOKNING-");

        List<Customer> customers = customerService.GetAllCustomers();

        foreach (Customer customer in customers)
        {
            Console.WriteLine($"{customer.ID}. {customer.Name}");
        }

        int customerID = ReadInt("Kundens ID: ");
        int roomID = ReadInt("Rummets ID: ");

        DateTime checkIn = ReadDate("Incheckning (ÅÅÅÅ-MM-DD): ");
        DateTime checkOut = ReadDate("Utcheckning (ÅÅÅÅ-MM-DD): ");

        int people = ReadInt("Antal personer: ");

        Booking booking = new Booking();
        booking.CustomerID = customerID;
        booking.RoomID = roomID;
        booking.CheckIn = checkIn;
        booking.CheckOut = checkOut;
        booking.People = people;

        if (bookingService.AddBooking(booking))
        {
            List<Rooms> rooms = roomDataAccess.GetAllRooms();

            foreach (Rooms room in rooms)
            {
                if (room.ID == roomID)
                {
                    int nights = (checkOut - checkIn).Days;
                    Console.WriteLine("Bokningen är skapad");
                    Console.WriteLine($"Totalpris: {nights * room.Price} kr");
                }
            }
        }
        else
        {
            Console.WriteLine("Bokningen kunde inte genomföras");
        }
    }

    private void ShowBookings()
    {
        Console.WriteLine("\n-VISA BOKNINGAR-");

        int customerID = ReadInt("Kundens ID: ");

        List<Booking> bookings = bookingService.GetCustomerBookings(customerID);
        List<Rooms> rooms = roomDataAccess.GetAllRooms();
        List<Hotel> hotels = hotelDataAccess.GetAllHotels();

        foreach (Booking booking in bookings)
        {
            foreach (Rooms room in rooms)
            {
                if (booking.RoomID == room.ID)
                {
                    foreach (Hotel hotel in hotels)
                    {
                        if (room.HotelID == hotel.ID)
                        {
                            int nights = (booking.CheckOut - booking.CheckIn).Days;

                            Console.WriteLine(
                                $"Bokning: {booking.ID}, Hotell: {hotel.Name}, " +
                                $"Rum: {room.RoomNumber}, " +
                                $"Datum: {booking.CheckIn:yyyy-MM-dd} - {booking.CheckOut:yyyy-MM-dd}, " +
                                $"Personer: {booking.People}, Totalpris: {nights * room.Price} kr"
                            );
                        }
                    }
                }
            }
        }

        if (bookings.Count == 0)
        {
            Console.WriteLine("Kunden har inga bokningar");
        }
    }

    private void UpdatePeople()
    {
        Console.WriteLine("\n-ÄNDRA ANTAL PERSONER-");

        int bookingID = ReadInt("Bokningsnummer: ");
        int people = ReadInt("Nytt antal personer: ");

        if (people <= 0)
        {
            Console.WriteLine("Antalet personer måste vara större än 0");
            return;
        }

        if (bookingService.UpdateBookingPeople(bookingID, people))
        {
            Console.WriteLine("Antalet personer har ändrats");
        }
        else
        {
            Console.WriteLine("Kunde inte ändra antal personer");
        }
    }

    private void CancelBooking()
    {
        Console.WriteLine("\n-AVBOKA-");

        int bookingID = ReadInt("Bokningsnummer: ");

        bookingService.DeleteBooking(bookingID);

        Console.WriteLine("Bokningen har avbokats");
    }
    private void ShowAllCustomers()
    {
        Console.WriteLine("\n-VISA ALLA KUNDER-");

        List<Customer> customers = customerService.GetAllCustomers();

        if (customers.Count == 0)
        {
            Console.WriteLine("Inga kunder hittades");
            return;
        }

        foreach (Customer customer in customers)
        {
            Console.WriteLine($"ID: {customer.ID}, Namn: {customer.Name}, E-post: {customer.Email}, Telefonnummer: {customer.PhoneNumber}");
        }
    }
    private void ShowAllHotels()
    {
        Console.WriteLine("\n-VISA ALLA HOTELL-");

        List<Hotel> hotels = hotelService.GetAllHotels();

        if (hotels.Count == 0)
        {
            Console.WriteLine("Inga hotell hittades");
            return;
        }

        foreach (Hotel hotel in hotels)
        {
            Console.WriteLine($"Namn: {hotel.Name}");
            Console.WriteLine($"Ort: {hotel.Location}");
            Console.WriteLine($"Pool: Ja");
            Console.WriteLine($"Restaurang: Ja");
            Console.WriteLine($"Avstånd till strand: {hotel.DistanceBeach} meter");
            Console.WriteLine($"Avstånd till centrum: {hotel.DistanceCenter} meter");
            Console.WriteLine($"Betyg: {hotel.Rating}/5");

            Console.WriteLine();
        }
    }
}
