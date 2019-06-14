

using DataLayer;
using StationeryService;
using Stationery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using System.Data.Common;
using System.Data.Entity;




namespace StationeryServiceTests
{
    public class StationeryServiceTest
    {


        private StationeryDbContext DbBefore;
        private StationeryDbContext DbAfter;
        private StationeryServices service;

        [SetUp]

        public void Setup()
        {
            DbConnection connection = Effort.DbConnectionFactory.CreateTransient();
            DbBefore = new StationeryDbContext(connection);
            DbAfter = new StationeryDbContext(connection);
            StationeryDbContext dbService = new StationeryDbContext(connection);
            service = new StationeryServices(dbService);
            

        }

        [Test]

        public void Addcustomer_AddsCustomerToTheDataBase_WhenCalledWithCustomerObject()
        {
            //Arrange
            string accountName = "Ibrahim";
            string accountName1 = "Ntambiye";
            string gender = "M";
            string password = "123456789";


            //Act
            int customerId = service.AddCustomer(new Customer()
            {
                FirstName = accountName,
                LastName = accountName1,
                Gender = gender,
               Password = password

            });

            //Assert
            Customer customer = DbAfter.Customers.Find(customerId);
            Assert.That(customer, Is.Not.Null);
            Assert.That(customer.FirstName, Is.EqualTo("Ibrahim"));
            Assert.That(customer.LastName, Is.EqualTo("Ntambiye"));
            Assert.That(customer.Gender.ToString(), Is.EqualTo("M"));
            Assert.That(customer.Username, Is.EqualTo("ibrahim.ntambiye"));
            Assert.That(customer.Password, Is.EqualTo(password));
        }
        [Test]

        public void Deletecustomer_DeletesCustomerFromTheDataBase_WhenCalledWithCustomerId()
        {
            //Arrange

            string accountName = "Ibrahim";
            string accountName1 = "Ntambiye";
            string gender = "M";
            string password = "123456789";

            Customer customer = new Customer()
            {
                FirstName = accountName,
                LastName = accountName1,
                Gender = gender,
                Password = password
            };
            DbBefore.Customers.Add(customer);
            DbBefore.SaveChanges();

            //Act
            service.DeleteCustomer(customer.CustomerId);

            //Assert
            Customer customerAfter = DbAfter.Customers.Find(customer.CustomerId);
            Assert.That(customerAfter, Is.Null);
            //Assert.That(customerAfter.FirstName, Is.Null);
            //Assert.That(customerAfter.LastName, Is.Null);
            //Assert.That(customerAfter.Gender.ToString(), Is.Null);
        }

        [Test]
        public void UpdatecustomerInformation_UpdatesCustomerInformationInTheDataBase_WhenCalledWithCustomerDtoObject()
        {
            //Arrange

            string accountName = "Ibrahim";
            string accountName1 = "Ntambiye";
            string gender = "M";

            Customer customer = new Customer()
            {
                FirstName = accountName,
                LastName = accountName1,
                Gender = gender
            };
            DbBefore.Customers.Add(customer);
            DbBefore.SaveChanges();

            //Act
            CustomerDto customerDto = new CustomerDto()
            {
                CustomerId = customer.CustomerId,
                FirstName = "Chris",
                LastName = "Brooks"
            };
            service.UpdateCustomerInformation(customerDto);


            //Assert
            Customer customerAfter = DbAfter.Customers.Find(customer.CustomerId);
            Assert.That(customerAfter, Is.Not.Null);
            Assert.That(customerAfter.FirstName, Is.EqualTo("Chris"));
            Assert.That(customerAfter.LastName, Is.EqualTo("Brooks"));



        }
        [Test]

        public void GetAllcustomers_GetsAllcustomersFromTheDataBase_WhenCalled()
        {
            //Arrange

            string accountName = "Ibrahim";
            string accountName1 = "Ntambiye";
            string gender = "M";
            string password = "123456789";

            Customer customer1 = new Customer()
            {
                FirstName = accountName,
                LastName = accountName1,
                Gender = gender,
                Password = password
            };


            Customer customer2 = new Customer()
            {
                FirstName = "Mike",
                LastName = "Giles",
                Gender = "M",
                Password = password

            };
            DbBefore.Customers.Add(customer1);
            DbBefore.Customers.Add(customer2);
            DbBefore.SaveChanges();


            //Act
            var customers = service.GetAllCustomers();

            //Assert

            Assert.That(customers[0].CustomerId, Is.EqualTo(customer1.CustomerId));
            Assert.That(customers[1].CustomerId, Is.EqualTo(customer2.CustomerId));

        }
        [Test]

        public void OrdersPerCustomer_GetsCustomerAndTheirOrders_WhenCalledWithCustomerId()
        {
            //Arrange
            Marker marker = new Marker()
            {
                Colour = "Red",
                Brand = "Staddle",
                Permanent = true,
                Price = 500

            };
            Marker marker1 = new Marker()
            {
                Colour = "black",
                Brand = "Staddle",
                Permanent = true,
                Price = 200

            };


            Customer customer2 = new Customer()
            {
                FirstName = "Mike",
                LastName = "Giles",
                Gender = "M",
                Password = "123456789"

            };



            Order order = new Order()
            {
                Customer = customer2,
                CustomerId = customer2.CustomerId,
                Date = DateTime.Today,
                MarkerId = marker.MarkerId,
                ListOfMarkers = new List<Marker>()
                {
                    marker,
                    marker1
                }

            };

            DbBefore.Customers.Add(customer2);
            DbBefore.Orders.Add(order);
            DbBefore.SaveChanges();


            //Act


            //Assert
            var customersPerOrder = service.OrdersPerCustomer(customer2.CustomerId);
            string customerName = string.Format("{0} {1}", customer2.FirstName, customer2.LastName);
            Assert.That(customersPerOrder, Is.Not.Null);
            Assert.That(customersPerOrder.TotalCost, Is.EqualTo(700));
            Assert.That(customersPerOrder.CustomerName, Is.EqualTo(customerName));
            Assert.That(customersPerOrder.OrderList[0].OrderId, Is.EqualTo(order.OrderId));
        }

        [Test]
        public void AddMarker_AddsMarkerToTheDataBase_WhenCalledWithMarkerObject()
        {
            //Arrange
            int markerId = service.AddMarker(new Marker
                {
                    Colour = "black",
                    Brand = "Staddle",
                    Permanent = true,
                    Price = 200,

                });

            //Assert
            Marker marker = DbAfter.Markers.Find(markerId);
            Assert.That(marker.MarkerId, Is.EqualTo(1));
            Assert.That(marker, Is.Not.Null);
            Assert.That(marker.Colour, Is.EqualTo("black"));
            Assert.That(marker.Brand, Is.EqualTo("Staddle"));
            Assert.That(marker.Price, Is.EqualTo(200));
        }

        [Test]
        public void DeleteMarker_DeletesMarkerFromTheDataBase_WhenCalledWithMarkerId()
        {
            //Arrange
            var marker = new Marker()
             {
                 Colour = "black",
                 Brand = "Staddle",
                 Permanent = true,
                 Price = 200,

             };
            DbBefore.Markers.Add(marker);
            DbBefore.SaveChanges();

            //Act
            service.DeleteMarker(marker.MarkerId);


            //Assert
            Marker marker1 = DbAfter.Markers.Find(marker.MarkerId);
            Assert.That(marker1, Is.Null);

        }

        [Test]
        public void AddOrder_AddsOrderToTheDataBase_WhenCalledWithOrderDtoObject()
        {
            
            int orderId = service.AddOrder (new Order()
            {
                Customer =  new Customer()
                {
                    FirstName = "Mike",
                    LastName = "Giles",
                    Gender = "M",
                   Password = "123456789"

                },

                Date = DateTime.Today,
                MarkerId = service.AddMarker( new Marker()
            {
                Colour = "black",
                Brand = "Staddle",
                Permanent = true,
                Price = 200

            }),
                
            });
            DbBefore.SaveChanges();

            //Act
           

            //Assert
            Order orderDb = DbAfter.Orders.Include(o => o.Customer).Single(o =>o.OrderId == orderId);

            //Assert.That(orderDb, Is.Not.Null);
            //Assert.That(orderDb.OrderId, Is.EqualTo(orderId));
            Assert.That(orderDb.Customer.FirstName, Is.EqualTo("Mike"));
            //Assert.That(orderDb.Customer, Is.Not.Empty);

        }

        [Test]
        public void DeleteOrder_DeletesOrderFromTheDataBase_WhenCalledWithOrderId()
        {
            //Arrange
            Marker marker1 = new Marker()
            {
                Colour = "black",
                Brand = "Staddle",
                Permanent = true,
                Price = 200

            };


            Customer customer2 = new Customer()
            {
                FirstName = "Mike",
                LastName = "Giles",
                Gender = "M"

            };
            Order order = new Order()
            {
                Customer = customer2,
                Date = DateTime.Today,
                MarkerId = marker1.MarkerId,
                ListOfMarkers = new List<Marker>()
                {
                    marker1
                }
            };
            DbBefore.Orders.Add(order);
            DbBefore.Customers.Add(customer2);
            DbBefore.Markers.Add(marker1);
            DbBefore.SaveChanges();

            //Act
            service.DeleteOrder(order.OrderId);

            //Assert
            Order orderDb = DbAfter.Orders.Find(order.OrderId);
            Assert.That(orderDb, Is.Null);
        }
    }
}
