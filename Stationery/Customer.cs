using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stationery
{
   public class Customer
    {
        public int CustomerId { get; set; }
         
        [Display(Name = "First Name")]
        [MaxLength(30, ErrorMessage = "First Name should not exceed 30 characters.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a valid name.")]
        public string FirstName { get; set; }

       [Display(Name = "Last Name")] [MaxLength (30, ErrorMessage = "Last Name should not exceed 30 characters.")]
       [Required(AllowEmptyStrings=false, ErrorMessage = "Please enter a valid name.")]
        public string LastName { get; set; }

        public ICollection<Order> Orders { get; set; }

        [Column(TypeName = "char")]
        [RegularExpression("[MF]")]
        [MaxLength(1)]
        public string Gender { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a valid Username.")]
        [MaxLength(30, ErrorMessage = "Username should not exceed 60 characters.")]
        public string Username { get; set; }

        [MinLength(8, ErrorMessage = "Password must be atleast 8 characters long")]
        [MaxLength(30, ErrorMessage = "Password should not exceed 30 characters.")]

        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a valid Password.")]
        public string Password { get; set; }
       

    }
}
