using HolidayMaker.DataAccess;
using HolidayMaker.Tables;

namespace HolidayMaker.Services;

public class CustomerService
{
    private CustomerDataAccess customerDataAccess = new CustomerDataAccess();

    public List<Customer> GetAllCustomers()
    {
        return customerDataAccess.GetAllCustomers();
    }
}