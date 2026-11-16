using HolidayMaker.DataAccess;
using HolidayMaker.Tables;

namespace HolidayMaker.Services;

public class HotelService
{
    private HotelDataAccess hotelDataAccess = new HotelDataAccess();

    public List<Hotel> GetAllHotels()
    {
        return hotelDataAccess.GetAllHotels();
    }
}