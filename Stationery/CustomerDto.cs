using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Stationery
{
   public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public CustomerDto()
        {
            Username = string.Format("{0}.{1}", FirstName, LastName);
        }
        
    }
}
