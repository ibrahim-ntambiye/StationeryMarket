namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAvailableInStockProperty : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Markers", "AvailableInStock", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Markers", "AvailableInStock");
        }
    }
}
