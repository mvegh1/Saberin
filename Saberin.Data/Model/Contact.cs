using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saberin.Data.Model
{
    public class Contact
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Address> Address { get; set; }
        public string Display
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
