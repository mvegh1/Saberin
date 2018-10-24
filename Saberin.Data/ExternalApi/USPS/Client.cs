using Saberin.Data.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace Saberin.Data.ExternalApi.USPS
{
    public static class Client
    {
        private static HttpClient HttpClient = new HttpClient();
        private static string UserId
        {
            get
            {
                return ConfigurationManager.AppSettings["Saberin.Data.ExternalApi.USPS.UserId"].ToString();
            }
        }
        private static string Password
        {
            get
            {
                return ConfigurationManager.AppSettings["Saberin.Data.ExternalApi.USPS.Password"].ToString();
            }
        }
        private static string ServiceUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["Saberin.Data.ExternalApi.USPS.ServiceUrl"].ToString();
            }
        }
        private static string AddressValidateTemplate
        {
            get
            {
                return HttpUtility.UrlDecode(ConfigurationManager.AppSettings["Saberin.Data.ExternalApi.USPS.AddressValidateTemplate"]);
            }
        }
        public static Saberin.Data.Model.Address AddressValidate(Saberin.Data.Model.Address address)
        {
            var url = $"{Client.ServiceUrl}{Client.AddressValidateTemplate}"
                .Replace("{UserId}", Client.UserId)
                .Replace("{Address1}", "")
                .Replace("{Address2}", address.Street)
                .Replace("{City}", address.City)
                .Replace("{State}", address.State)
                .Replace("{Zip5}", address.PostalCode.Split('-').First())
                .Replace("{Zip4}", string.Join("", address.PostalCode.Split('-').Skip(1)));
            var httpMsg = new HttpRequestMessage { RequestUri = new Uri(url) };
            var result = Client.HttpClient.SendAsync(httpMsg).Result;
            var content = result.Content.ReadAsStringAsync().Result;
            try
            {
                using (StringReader sr = new StringReader(content))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Saberin.Data.ExternalApi.USPS.Response.AddressValidateResponse));
                    var deserialized = (Saberin.Data.ExternalApi.USPS.Response.AddressValidateResponse)serializer.Deserialize(sr);
                    if (deserialized.Address.City == null)
                    {
                        return null;
                    }
                    var rtn = new Saberin.Data.Model.Address();
                    rtn.AddressId = address.AddressId;
                    rtn.ContactId = address.ContactId;
                    rtn.Contact = address.Contact;
                    rtn.Street = deserialized.Address.Address2;
                    rtn.City = deserialized.Address.City;
                    rtn.State = deserialized.Address.State;
                    rtn.PostalCode = deserialized.Address.Zip5;
                    if (deserialized.Address.Zip4.Length > 0)
                    {
                        rtn.PostalCode += $"-{deserialized.Address.Zip4}";
                    }
                    return rtn;
                }
            }
            catch (Exception e)
            {
                return null;
            }

        }
    }
}
