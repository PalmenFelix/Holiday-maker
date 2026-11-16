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

    public List<Rooms> SearchAvailableRooms(string location, DateTime checkIn, DateTime checkOut, int people)
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
            AND NOT EXISTS
            (
                SELECT 1
                FROM Booking
                WHERE Booking.RoomID = Rooms.ID
                AND Booking.CheckIn < @CheckOut
                AND Booking.CheckOut > @CheckIn
            )";

        MySqlCommand command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@Location", location);
        command.Parameters.AddWithValue("@People", people);
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
}



