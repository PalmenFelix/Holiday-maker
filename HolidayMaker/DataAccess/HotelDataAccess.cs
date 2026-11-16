using MySql.Data.MySqlClient;
using HolidayMaker.Data;
using HolidayMaker.Tables;

namespace HolidayMaker.DataAccess;

public class HotelDataAccess
{
    public List<Hotel> GetAllHotels()
    {
        List<Hotel> hotels = new List<Hotel>();
        DataBaseConnection db = new DataBaseConnection();

        MySqlConnection connection = db.GetConnection();
        connection.Open();

        string query = "SELECT * FROM Hotel";
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Hotel hotel = new Hotel();

            hotel.ID = reader.GetInt32("ID");
            hotel.Name = reader.GetString("Name");
            hotel.Location = reader.GetString("Location");
            hotel.Pool = reader.GetBoolean("Pool");
            hotel.Restaurant = reader.GetBoolean("Restaurant");
            hotel.DistanceBeach = reader.GetInt32("DistanceBeach");
            hotel.DistanceCenter = reader.GetInt32("DistanceCenter");
            hotel.Rating = reader.GetInt32("Rating");

            hotels.Add(hotel);
        }

        reader.Close();
        connection.Close();

        return hotels;
    }
}