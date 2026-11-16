using MySql.Data.MySqlClient;
using HolidayMaker.Data;
using HolidayMaker.Tables;

namespace HolidayMaker.DataAccess;

public class CustomerDataAccess
{
    public List<Customer> GetAllCustomers()
    {
        List<Customer> customers = new List<Customer>();
        DataBaseConnection db = new DataBaseConnection();

        MySqlConnection connection = db.GetConnection();
        connection.Open();

        string query = "SELECT * FROM Customer";
        MySqlCommand command = new MySqlCommand(query, connection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            Customer customer = new Customer();

            customer.ID = reader.GetInt32("ID");
            customer.Name = reader.GetString("Name");
            customer.Email = reader.GetString("Email");
            customer.PhoneNumber = reader.GetString("PhoneNumber");

            customers.Add(customer);
        }

        reader.Close();
        connection.Close();

        return customers;
    }
}