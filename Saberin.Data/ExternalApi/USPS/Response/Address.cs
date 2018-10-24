using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Saberin.Data.ExternalApi.USPS.Response
{
    public class Address
    {
        [XmlAttribute("ID")]
        public int Id { get; set; }

        [XmlElement("Address1")]
        public string Address1 { get; set; }

        [XmlElement("Address2")]
        public string Address2 { get; set; }

        [XmlElement("City")]
        public string City { get; set; }

        [XmlElement("State")]
        public string State { get; set; }

        [XmlElement("Zip5")]
        public string Zip5 { get; set; }

        [XmlElement("Zip4")]
        public string Zip4 { get; set; }
    }
}
