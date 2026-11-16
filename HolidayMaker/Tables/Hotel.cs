namespace HolidayMaker.Tables;

public class Hotel
{
    public int ID { get; set; }
    public string Name { get; set; } = "";
    public string Location { get; set; } = "";
    public bool Pool { get; set; }
    public bool Restaurant { get; set; }
    public int DistanceBeach { get; set; }
    public int DistanceCenter { get; set; }
    public int Rating { get; set; }
}