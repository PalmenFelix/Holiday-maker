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
}