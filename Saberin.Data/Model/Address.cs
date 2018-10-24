using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saberin.Data.Model
{
    public class Address
    {
        [ForeignKey("Contact")]
        public int ContactId { get; set; }
        public Contact Contact { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Display
        {
            get
            {
                return $"{this.Street} {this.City},{this.State} {this.PostalCode}";
            }
        }
    }
}
