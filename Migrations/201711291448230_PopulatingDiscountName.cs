namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulatingDiscountName : DbMigration
    {
        public override void Up()
        {
            Sql("UPDATE MembershipTypes SET DiscountName = 'Pay as You go' WHERE Id = 1");
            Sql("UPDATE MembershipTypes SET DiscountName = 'Monthly' WHERE Id = 2");
            Sql("UPDATE MembershipTypes SET DiscountName = 'Quarterly' WHERE Id = 3");
            Sql("UPDATE MembershipTypes SET DiscountName = 'Annualy' WHERE Id = 4");
        }
        
        public override void Down()
        {
        }
    }
}
