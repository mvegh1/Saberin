using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Saberin.Data.ExternalApi.USPS.Response
{
    [Serializable, XmlRoot("AddressValidateResponse")]
    public class AddressValidateResponse
    {
        [XmlElement("Address")]
        public USPS.Response.Address Address { get; set; }
    }
}
