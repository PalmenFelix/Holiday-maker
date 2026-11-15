using MySql.Data.MySqlClient;

namespace HolidayMaker.Data;
public class DataBaseConnection
{

    private string connectionString;
    public DataBaseConnection()
    {
        string? password = Environment.GetEnvironmentVariable("DB_PASSWORD");

        connectionString = $"Server=localhost;Port=3306;Database=Holiday_maker;Uid=root;Pwd={password};";
    }

       public MySqlConnection GetConnection()
    {
        return new MySqlConnection(connectionString);
    }
}