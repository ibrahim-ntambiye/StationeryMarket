using System;
using Stationery;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationeryService
{
  public interface IStationeryServices
    {
        //Order Services
         int AddOrder(Order order);
         void DeleteOrder(int orderId);

        //Customer Services
         int AddCustomer(Customer customer);
         List<Customer> GetAllCustomers();
         OrdersPerCustomerDto OrdersPerCustomer(int customerId);
         void DeleteCustomer(int customerId);
         Customer UpdateCustomerInformation(CustomerDto customerDto);
         Customer Login(string firstName, string lastName);
         Customer GetCustomer(int customerId);

        //Marker Services
         int AddMarker(Marker marker);
         void DeleteMarker(int markerId);

    }
}
