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

        public Marker AddMarker(Marker marker)
        {
            db.Markers.Add(marker);
            db.SaveChanges();
            return marker;
            
        }
        public Marker ViewMarker(int markerId)
        {
            Marker marker = db.Markers.Find(markerId);
            return marker;
        }
        

        public void DeleteMarker(int markerId)
        {
            Marker marker = db.Markers.Find(markerId);
            db.Markers.Remove(marker);
            db.SaveChanges();
        }
        public List<Marker> GetAllMarkers()
        {
           List<Marker> markers =   db.Markers.ToList();
            return markers;
        }


        public int AddOrder(Order order)
        {
            
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
            List<Order> list = new List<Order>();
            var customer = db.Customers.Where(c => c.CustomerId == customerId)
                .Select(c => new OrdersPerCustomerDto
            {
                CustomerName = c.FirstName +" "+ c.LastName,
                OrderList = c.Orders.ToList(),
                TotalCost = c.Orders
                             .Select(o => o.ListOfMarkers)
                             .Select(p => p.DefaultIfEmpty()
                             .Sum(o => o.Price)).DefaultIfEmpty()
                             .Sum(),
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

        public Customer UpdateCustomerInformation(Customer customer)
        {

            Customer customerDb = db.Customers.Find(customer.CustomerId);
            {
                if (customerDb != null)
                {
                    customerDb.IsAdmin = customer.IsAdmin;
                    customerDb.FirstName = customer.FirstName;
                    customerDb.LastName = customer.LastName;
                    customerDb.Password = customer.Password;
                    customerDb.Username =
                        string.Format("{0}" + "." + "{1}", customer.FirstName, customer.LastName).ToLower();
                    db.SaveChanges();
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
            
        }

        public Customer Login(string username,string password)
        {

            try
            {
                var customer = db.Customers.Single(c => c.Username == username && password == c.Password);

                //if (customer != null)
                //{
                return customer;
            }
            catch
            {
                var customerNotFound = new Customer();

                return customerNotFound;
            }
        }
        public Customer GetCustomer(int customerId)
        {
            Customer customer = db.Customers.Find(customerId);
            return customer;
        }



    }
}
