namespace Saberin.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        ContactId = c.Int(nullable: false),
                        Street = c.String(),
                        City = c.String(),
                        State = c.String(),
                        PostalCode = c.String(),
                    })
                .PrimaryKey(t => t.AddressId)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.ContactId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "ContactId", "dbo.Contacts");
            DropIndex("dbo.Addresses", new[] { "ContactId" });
            DropTable("dbo.Contacts");
            DropTable("dbo.Addresses");
        }
    }
}
