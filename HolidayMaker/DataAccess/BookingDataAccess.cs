using MySql.Data.MySqlClient;
using HolidayMaker.Data;
using HolidayMaker.Tables;

namespace HolidayMaker.DataAccess;

public class BookingDataAccess
{
    public List<Booking> GetAllBookings()
    {
        List<Booking> bookings = new List<Booking>();
        DataBaseConnection db = new DataBaseConnection();

        MySqlConnection connection = db.GetConnection();
        connection.Open();

        string query = "SELECT * FROM Booking";
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Booking booking = new Booking();

            booking.ID = reader.GetInt32("ID");
            booking.RoomID = reader.GetInt32("RoomID");
            booking.CustomerID = reader.GetInt32("CustomerID");
            booking.CheckIn = reader.GetDateTime("CheckIn");
            booking.CheckOut = reader.GetDateTime("CheckOut");
            booking.People = reader.GetInt32("People");

            bookings.Add(booking);
        }

        reader.Close();
        connection.Close();

        return bookings;
    }

    public List<Rooms> SearchAvailableRooms(
    string location, DateTime checkIn, DateTime checkOut,
    int people, bool pool, bool restaurant,
    int maxBeach, int maxCenter, string sort)
{
    List<Rooms> rooms = new List<Rooms>();
    DataBaseConnection db = new DataBaseConnection();

    MySqlConnection connection = db.GetConnection();
    connection.Open();

    string query = @"
        SELECT Rooms.*
        FROM Rooms
        JOIN Hotel ON Rooms.HotelID = Hotel.ID
        WHERE Hotel.Location = @Location
        AND Rooms.MaxPersons >= @People
        AND (@Pool = 0 OR Hotel.Pool = 1)
        AND (@Restaurant = 0 OR Hotel.Restaurant = 1)
        AND (@MaxBeach = -1 OR Hotel.DistanceBeach <= @MaxBeach)
        AND (@MaxCenter = -1 OR Hotel.DistanceCenter <= @MaxCenter)
        AND NOT EXISTS
        (
            SELECT 1
            FROM Booking
            WHERE Booking.RoomID = Rooms.ID
            AND Booking.CheckIn < @CheckOut
            AND Booking.CheckOut > @CheckIn
        )";

    if (sort == "price")
        query += " ORDER BY Rooms.Price ASC";
    else if (sort == "rating")
        query += " ORDER BY Hotel.Rating DESC";

    MySqlCommand command = new MySqlCommand(query, connection);

    command.Parameters.AddWithValue("@Location", location);
    command.Parameters.AddWithValue("@People", people);
    command.Parameters.AddWithValue("@Pool", pool);
    command.Parameters.AddWithValue("@Restaurant", restaurant);
    command.Parameters.AddWithValue("@MaxBeach", maxBeach);
    command.Parameters.AddWithValue("@MaxCenter", maxCenter);
    command.Parameters.AddWithValue("@CheckIn", checkIn);
    command.Parameters.AddWithValue("@CheckOut", checkOut);

    MySqlDataReader reader = command.ExecuteReader();

    while (reader.Read())
    {
        Rooms room = new Rooms();

        room.ID = reader.GetInt32("ID");
        room.HotelID = reader.GetInt32("HotelID");
        room.RoomNumber = reader.GetInt32("RoomNumber");
        room.MaxPersons = reader.GetInt32("MaxPersons");
        room.Price = reader.GetInt32("Price");

        rooms.Add(room);
    }

    reader.Close();
    connection.Close();

    return rooms;
}

    public bool AddBooking(Booking booking)
    {
        if (booking.CheckOut <= booking.CheckIn || booking.People <= 0)
            return false;

        DataBaseConnection db = new DataBaseConnection();

        using (MySqlConnection connection = db.GetConnection())
        {
            connection.Open();

            string checkQuery = @"
                SELECT COUNT(*)
                FROM Rooms
                WHERE ID = @RoomID
                AND MaxPersons >= @People
                AND NOT EXISTS
                (
                    SELECT 1 FROM Booking
                    WHERE RoomID = @RoomID
                    AND CheckIn < @CheckOut
                    AND CheckOut > @CheckIn
                )";

            MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection);

            checkCommand.Parameters.AddWithValue("@RoomID", booking.RoomID);
            checkCommand.Parameters.AddWithValue("@People", booking.People);
            checkCommand.Parameters.AddWithValue("@CheckIn", booking.CheckIn);
            checkCommand.Parameters.AddWithValue("@CheckOut", booking.CheckOut);

            int available = Convert.ToInt32(checkCommand.ExecuteScalar());

            if (available == 0)
                return false;

            string query = @"
                INSERT INTO Booking
                (RoomID, CustomerID, CheckIn, CheckOut, People)
                VALUES
                (@RoomID, @CustomerID, @CheckIn, @CheckOut, @People)";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@RoomID", booking.RoomID);
            command.Parameters.AddWithValue("@CustomerID", booking.CustomerID);
            command.Parameters.AddWithValue("@CheckIn", booking.CheckIn);
            command.Parameters.AddWithValue("@CheckOut", booking.CheckOut);
            command.Parameters.AddWithValue("@People", booking.People);

            command.ExecuteNonQuery();

            return true;
        }
    }

    public List<Booking> GetCustomerBookings(int customerID)
    {
        List<Booking> bookings = new List<Booking>();
        DataBaseConnection db = new DataBaseConnection();

        using (MySqlConnection connection = db.GetConnection())
        {
            connection.Open();

            string query = "SELECT * FROM Booking WHERE CustomerID = @CustomerID";

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@CustomerID", customerID);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Booking booking = new Booking();

                booking.ID = reader.GetInt32("ID");
                booking.RoomID = reader.GetInt32("RoomID");
                booking.CustomerID = reader.GetInt32("CustomerID");
                booking.CheckIn = reader.GetDateTime("CheckIn");
                booking.CheckOut = reader.GetDateTime("CheckOut");
                booking.People = reader.GetInt32("People");

                bookings.Add(booking);
            }

            reader.Close();
        }

        return bookings;
    }

    public bool UpdateBookingPeople(int bookingID, int people)
    {
        if (people <= 0)
            return false;

        DataBaseConnection db = new DataBaseConnection();

        using (MySqlConnection connection = db.GetConnection())
        {
            connection.Open();

            string query = @"
                UPDATE Booking
                JOIN Rooms ON Booking.RoomID = Rooms.ID
                SET Booking.People = @People
                WHERE Booking.ID = @ID
                AND Rooms.MaxPersons >= @People";

            MySqlCommand command = new MySqlCommand(query, connection);

            command.Parameters.AddWithValue("@People", people);
            command.Parameters.AddWithValue("@ID", bookingID);

            return command.ExecuteNonQuery() > 0;
        }
    }
    
    public void DeleteBooking(int bookingID)
    {
        DataBaseConnection db = new DataBaseConnection();

        using (MySqlConnection connection = db.GetConnection())
        {
            connection.Open();

            string query = "DELETE FROM Booking WHERE ID = @ID";

            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", bookingID);

            command.ExecuteNonQuery();
        }
    }
}