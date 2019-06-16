namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Markers",
                c => new
                    {
                        MarkerId = c.Int(nullable: false, identity: true),
                        Colour = c.String(),
                        Make = c.String(),
                        Permanent = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MarkerId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Orders");
            DropTable("dbo.Markers");
        }
    }
}
