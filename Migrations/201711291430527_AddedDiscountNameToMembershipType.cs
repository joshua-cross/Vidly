namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDiscountNameToMembershipType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "DiscountName", c => c.String());


        }

        public override void Down()
        {
            DropColumn("dbo.MembershipTypes", "DiscountName");
        }
    }
}
