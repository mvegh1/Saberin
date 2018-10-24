using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Saberin.Data.Model;
using System.Data.Entity.Migrations;

namespace Saberin.Data.Repository.ContactManager
{
    internal class Context : DbContext
    {
        public Context()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<Saberin.Data.Repository.ContactManager.Context, Saberin.Data.Migrations.Configuration>(useSuppliedContext: true));
        }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
