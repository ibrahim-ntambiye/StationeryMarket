using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stationery
{
    public class OrdersPerCustomerDto
    {
        [MaxLength(60)]
        public string CustomerName { get; set; }
        public List<Order> OrderList { get; set; }
        public decimal TotalCost { get; set; }
    }
}
