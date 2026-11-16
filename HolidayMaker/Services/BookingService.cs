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

    public List<Rooms> SearchAvailableRooms(
        string location, DateTime checkIn, DateTime checkOut,
        int people, bool pool, bool restaurant,
        int maxBeach, int maxCenter, string sort)
    {
        return bookingDataAccess.SearchAvailableRooms(location, checkIn, checkOut, people, pool, restaurant, maxBeach, maxCenter, sort);
    }

    public bool AddBooking(Booking booking)
    {
        return bookingDataAccess.AddBooking(booking);
    }

    public List<Booking> GetCustomerBookings(int customerID)
    {
        return bookingDataAccess.GetCustomerBookings(customerID);
    }

    public bool UpdateBookingPeople(int bookingID, int people)
    {
        return bookingDataAccess.UpdateBookingPeople(bookingID, people);
    }

    public void DeleteBooking(int bookingID)
    {
        bookingDataAccess.DeleteBooking(bookingID);
    }
}