using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;



namespace Stationery
{
    public class Marker
    {
        [Key]
        public int MarkerId { get; set; }
        [MaxLength(30)]
        public string Colour { get; set; }
        [MaxLength(30)]
        public string Brand { get; set; }
        public bool Permanent { get; set; }
        public ICollection<Order> Orders { get; set; }
        public decimal Price { get; set; }
    }
}
