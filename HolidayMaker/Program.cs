
using HolidayMaker.Data;

DataBaseConnection database = new DataBaseConnection();

try
{
    var connection = database.GetConnection();
    connection.Open();

    Console.WriteLine("Ansluten till databasen");

    connection.Close();
}
catch (Exception exception)
{
    Console.WriteLine("Något gick fel" + exception.Message);
}
