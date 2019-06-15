using DataLayer;
using Stationery;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace StationeryService
{
    public class StationeryServices :IStationeryServices

       
    {
        private StationeryDbContext db;
       
       
        public StationeryServices(StationeryDbContext dbService)
        {
            this.db = dbService;
        }

        public StationeryServices()
        {
            db = new StationeryDbContext();
        }

        public int AddCustomer(Customer customer )
        {
           customer.Username = string.Format("{0}.{1}", customer.FirstName.ToLower(), customer.LastName.ToLower());
           db.Customers.Add(customer);
           db.SaveChanges();
           return customer.CustomerId;
        }

        public int AddMarker(Marker marker)
        {
            //Marker marker = new Marker

            //{
            //    Brand = markerDto.Brand,
            //    Colour = markerDto.Colour,
            //    MarkerId = markerDto.MarkerId,
            //    Permanent = markerDto.Permanent,
            //    Price = markerDto.Price
            //};

            db.Markers.Add(marker);
            db.SaveChanges();
            return marker.MarkerId;
            
        }

        

        public void DeleteMarker(int markerId)
        {
            Marker marker = db.Markers.Find(markerId);
            db.Markers.Remove(marker);
            db.SaveChanges();
        }


        public int AddOrder(Order order)
        {
            //Order order = new Order
            //{
            //    OrderId = orderDto.OrderId,
            //    Customer = orderDto.Customer,
            //    CustomerId = orderDto.CustomerId,
            //    Date = orderDto.Date,
            //    MarkerId = orderDto.MarkerId
            //};
            db.Orders.Add(order);
            db.SaveChanges();
            return order.OrderId;
        }
        public void DeleteOrder(int orderId)
        {
            var order = db.Orders.Find(orderId);
            db.Orders.Remove(order);
            db.SaveChanges();
        }

        public Customer GetCustomerById(int customerId)
        {
            Customer customerDb;

            customerDb = db.Customers
                .Find(customerId);
            
            return customerDb;
           
           
        }
        public List<Customer> GetAllCustomers()
        {
            List<Customer> listOfCustomers;
            listOfCustomers = db.Customers.ToList();

            return listOfCustomers;
        }

        public OrdersPerCustomerDto OrdersPerCustomer(int customerId)
        {
            
            var customer = db.Customers.Where(c => c.CustomerId == customerId)
                .Select(c => new OrdersPerCustomerDto
            {
                CustomerName = c.FirstName +" "+ c.LastName,
                OrderList = c.Orders.ToList(),
                TotalCost = c.Orders
                             .Select(o => o.ListOfMarkers)
                             .Select(p => p.DefaultIfEmpty()
                             .Sum(o => o.Price)).DefaultIfEmpty()
                             .Sum()
            }

           ).First();

            return customer;
        }

        public void DeleteCustomer(int customerId)
        {
            Customer customer = db.Customers
                .Find(customerId);
            db.Customers.Remove(customer);
            db.SaveChanges();
                
        }

        public Customer UpdateCustomerInformation(CustomerDto customerDto)
        {

            Customer customerDb = db.Customers.Find(customerDto.CustomerId);
            {
                if (customerDb != null)
                {
                    customerDb.FirstName = customerDto.FirstName;
                    customerDb.LastName = customerDto.LastName;
                    customerDb.Password = customerDto.Password;
                    customerDb.Username =
                        string.Format("{0}" + "." + "{1}", customerDto.FirstName, customerDto.LastName).ToLower();
                    return customerDb;

                }
                else
                {
                    var nullCustomerResult = new Customer()
                    {
                        FirstName = "CustomerId is invalid. FirstName update has failed",
                        LastName = "CustomerId is invalid. FirstName update has failed",
                        Password = "CustomerId is invalid. Password update has failed",
                    };
                    return nullCustomerResult;

                }
            }
            db.SaveChanges();
        }

        public Customer Login(string username,string password)
        {

            //Customer customer = db.Customers.Single(c => c.Username == username && password == c.Password);
            var customer = new Customer()
            {
                Username = "ibrahim.ntambiye",
                Password = "123456789"
            };
            return customer;
              
        }
        public Customer GetCustomer(int customerId)
        {
            Customer customer = db.Customers.Find(customerId);
            return customer;
        }


    }
}
