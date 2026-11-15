namespace HolidayMaker.Tables;

public class Booking
{
    public int ID { get; set; }
    public int RoomID { get; set; }
    public int CustomerID { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public int People { get; set; }
}