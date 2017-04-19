namespace OrangeBricks.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserIdToOffer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offers", "BuyerId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Offers", "BuyerId");
        }
    }
}
