
using HolidayMaker.Data;

DataBaseConnection db = new DataBaseConnection();

try
{
    var connection = db.GetConnection();
    connection.Open();

    Console.WriteLine("Ansluten till databasen");

    connection.Close();
}
catch (Exception exception)
{
    Console.WriteLine("Något gick fel" + exception.Message);
}
