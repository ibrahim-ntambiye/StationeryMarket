namespace DataLayer.Migrations
{
    using Stationery;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
   

    internal sealed class Configuration : DbMigrationsConfiguration<DataLayer.StationeryDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataLayer.StationeryDbContext context)
        {
            context.Customers.RemoveRange(context.Customers);
            context.Markers.RemoveRange(context.Markers);
            context.Orders.RemoveRange(context.Orders);


            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Orders', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Markers', RESEED, 0)");
            context.Database.ExecuteSqlCommand("DBCC CHECKIDENT ('Customers', RESEED, 0)");

            Customer customer = context.Customers.Add(new Customer()
            {
                //FirstName = "John",
                //LastName = "Books",
                //Password = "helloworld",
                //Gender = "M",
                //Username = "p"
                FirstName = "Chris",
                LastName = "Brooks",
                Password = "asd9fp4h",
                Gender = "M",
                Username = "chris.brooks"


            });
            //context.Customers.Add(new Customer()
            //{
            //    FirstName = "kop",
            //    LastName = "hello",
            //    Password = "helloworld",
            //     Gender = "M"
            //});
            //context.Customers.Add(new Customer()
            //{
            //    FirstName = "Jemma",
            //    LastName = "Crooks",
            //    Password = "helloworld",
            //     Gender = "F"
            //});

            //context.Customers.Add(new Customer()
            //{
            //    FirstName = "klopp",
            //    LastName = "Jurgen",
            //    Password = "helloworld",
            //     Gender = "M"
            //});
            //context.Customers.Add(new Customer()
            //{
            //    FirstName = "Bosco",
            //    LastName = "Danger",
            //    Password = "helloworld",
            //    Gender = "M"

            //});
            //context.Customers.Add(new Customer()
            //{
            //    FirstName = "Bosco",
            //    LastName = "Gibbs",
            //    Password = "helloworld",
            //    Gender = "M"
            //});
            //context.Customers.Add(new Customer()
            //{
            //    FirstName = "Kyle",
            //    LastName = "Danger",
            //    Password = "helloworld",
            //    Gender = "M"
            //});

            Marker LTM = context.Markers.Add(new Marker()
             {
                 Brand = "Stadel",
                 Colour = "Black",
                 Permanent = false,
                 Price = 3m
             });

            //Marker GRM =  context.Markers.Add(new Marker()
            // {
            //     Brand = "Stadel",
            //     Colour = "Green",
            //     Permanent = true,
            //     Price = 8m
            // });

            Marker XRM = context.Markers.Add(new Marker()
            {
                Brand = "Panda",
                Colour = "Red",
                Permanent = true,
                Price = 20m
            });

            context.Orders.Add(new Order()
            {
                Customer = customer,
                Date = DateTime.Now,
                ListOfMarkers = new List<Marker>
                {
                     XRM
                }
            });

            context.SaveChanges();

        }
    }
}
