using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stationery
{
    public class MarkerDto
    {
        public int MarkerId { get; set; }
        public string Colour { get; set; }
        public string Brand { get; set; }
        public bool Permanent { get; set; }
        public decimal Price { get; set; }
    }
}
