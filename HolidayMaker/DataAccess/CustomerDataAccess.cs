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

    public void AddCustomer(Customer customer)
    {
        DataBaseConnection db = new DataBaseConnection();

        MySqlConnection connection = db.GetConnection();
        connection.Open();

        string query = @"
            INSERT INTO Customer (Name, Email, PhoneNumber)
            VALUES (@Name, @Email, @PhoneNumber)";

        MySqlCommand command = new MySqlCommand(query, connection);

        command.Parameters.AddWithValue("@Name", customer.Name);
        command.Parameters.AddWithValue("@Email", customer.Email);
        command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);

        command.ExecuteNonQuery();

        connection.Close();
    }
}