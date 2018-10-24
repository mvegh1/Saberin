using Saberin.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Saberin.Data.Repository.ContactManager
{
    public static class Repository
    {
        public static IEnumerable<Contact> GetContacts()
        {
            using(var context = new Context())
            {
                var contacts = context.Contacts.Include("Address").ToList();
                return contacts;
            }
        }

        public static Contact GetContactById(int contactId)
        {
            using (var context = new Context())
            {
                var contact = context.Contacts.Include("Address").SingleOrDefault(x => x.ContactId == contactId);
                return contact;
            }
        }

        public static Result.Save Save(Contact contact)
        {
            var rtn = new Result.Save();
            var sanitizeResult = Repository.SanitizeContact(contact);
            if(sanitizeResult.ResultCode != Result.Sanitize.ResultCodes.Success)
            {
                // For our current system needs, this straight through cast works
                rtn.ResultCode = (Result.Save.ResultCodes)((int)sanitizeResult.ResultCode);
                rtn.Message = sanitizeResult.Message;
                return rtn;
            }
            // New Contact
            if(contact.ContactId == 0)
            {
                using (var context = new Context())
                {
                    context.Contacts.Add(contact);
                    context.SaveChanges();
                    return rtn;
                }
            }

            //Existing Contact
            using (var context = new Context())
            {
                var dbMatch = context.Contacts.Include("Address").Single(x => x.ContactId == contact.ContactId);
                dbMatch.FirstName = contact.FirstName;
                dbMatch.LastName = contact.LastName;
                foreach(var address in contact.Address)
                {
                    var dbAddr = dbMatch.Address.FirstOrDefault(x => x.AddressId == address.AddressId);
                    // New Address
                    if(dbAddr == null)
                    {
                        dbMatch.Address.Add(address);
                    }
                    //Existing Address
                    else
                    {
                        dbAddr.Street = address.Street;
                        dbAddr.City = address.City;
                        dbAddr.State = address.State;
                        dbAddr.PostalCode = address.PostalCode;
                    }
                }
                //Deleted Addresses
                var toRemove = new List<Address>();
                foreach(var address in dbMatch.Address)
                {
                    if(!contact.Address.Any(y=>y.AddressId == address.AddressId))
                    {
                        toRemove.Add(address);
                    }
                }
                context.Addresses.RemoveRange(toRemove);
                context.SaveChanges();
                
            }
            return rtn;
        }

        private static Result.Sanitize SanitizeContact(Contact contact)
        {
            var rtn = new Result.Sanitize();
            for (var i = 0; i < contact.Address.Count; i++)
            {
                var address = contact.Address[i];
                var addressCheck = ExternalApi.USPS.Client.AddressValidate(address);
                if (addressCheck == null)
                {
                    rtn.ResultCode = Result.Sanitize.ResultCodes.InvalidAddress;
                    rtn.Message = $"{address.Display} is an invalid address";
                    return rtn;
                }
                contact.Address[i] = addressCheck;
            }
            return rtn;
        }

    }
}
