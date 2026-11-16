using MySql.Data.MySqlClient;
using HolidayMaker.Data;
using HolidayMaker.Tables;

namespace HolidayMaker.DataAccess;

public class RoomDataAccess
{
    public List<Rooms> GetAllRooms()
    {
        List<Rooms> rooms = new List<Rooms>();
        DataBaseConnection db = new DataBaseConnection();

        MySqlConnection connection = db.GetConnection();
        connection.Open();

        string query = "SELECT * FROM Rooms";
        MySqlCommand command = new MySqlCommand(query, connection);
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