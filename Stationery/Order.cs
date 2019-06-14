using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stationery
{
    public class Order
    {
        
        public int OrderId { get; set; }
        [Required]
        public int MarkerId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<Marker> ListOfMarkers { get; set; }

        public Order()
        {
            Date = DateTime.Now;
        }
    }
}
