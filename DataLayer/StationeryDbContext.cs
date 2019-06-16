using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Stationery;
using System.Data.Common;

namespace DataLayer
{
    public class StationeryDbContext : DbContext
    {
        public DbSet<Marker> Markers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public StationeryDbContext()
            : base("context")
        {

        }
        public StationeryDbContext(DbConnection connection)
            : base(connection, true)
        {

        }



    }
}
