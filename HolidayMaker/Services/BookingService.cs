using HolidayMaker.DataAccess;
using HolidayMaker.Tables;

namespace HolidayMaker.Services;

public class BookingService
{
    private BookingDataAccess bookingDataAccess = new BookingDataAccess();

    public List<Booking> GetAllBookings()
    {
        return bookingDataAccess.GetAllBookings();
    }
}