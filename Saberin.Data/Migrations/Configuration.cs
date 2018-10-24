namespace Saberin.Data.Migrations
{
    using Saberin.Data.Model;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Saberin.Data.Repository.ContactManager.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Saberin.Data.Repository.ContactManager.Context context)
        {
            
            var address1 = new Address
            {
                ContactId = 1,
                AddressId = 1,
                City = "Long Beach",
                State = "NY",
                Street = "123 Monroe Boulevard",
                PostalCode = "11561"
            };
            var address2 = new Address
            {
                ContactId = 2,
                AddressId = 2,
                City = "Levittown",
                State = "NY",
                Street = "456 Water Lane",
                PostalCode = "11756"
            };
            context.Addresses.AddOrUpdate(x => x.AddressId, address1, address2);

            context.Contacts.AddOrUpdate(x => x.ContactId,
            new Contact
            {
                ContactId = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = new List<Address>
                {
                    address1
                }
            },
            new Contact
            {
                ContactId = 2,
                FirstName = "Tom",
                LastName = "Smith",
                Address = new List<Address>
                {
                    address2
                }
            });
        }
    }
}
