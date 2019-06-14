namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SecondMgrationi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirtName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            AddColumn("dbo.Markers", "Brand", c => c.String());
            AddColumn("dbo.Orders", "MarkerId", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "MarkerId");
            CreateIndex("dbo.Orders", "CustomerId");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.Customers", "CustomerId", cascadeDelete: true);
            AddForeignKey("dbo.Orders", "MarkerId", "dbo.Markers", "MarkerId", cascadeDelete: true);
            DropColumn("dbo.Markers", "Make");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Markers", "Make", c => c.String());
            DropForeignKey("dbo.Orders", "MarkerId", "dbo.Markers");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.Orders", new[] { "MarkerId" });
            DropColumn("dbo.Orders", "CustomerId");
            DropColumn("dbo.Orders", "MarkerId");
            DropColumn("dbo.Markers", "Brand");
            DropTable("dbo.Customers");
        }
    }
}
