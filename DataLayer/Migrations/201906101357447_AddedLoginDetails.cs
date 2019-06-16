namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedLoginDetails : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "MarkerId", "dbo.Markers");
            DropIndex("dbo.Orders", new[] { "MarkerId" });
            CreateTable(
                "dbo.MarkerOrders",
                c => new
                    {
                        Marker_MarkerId = c.Int(nullable: false),
                        Order_OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Marker_MarkerId, t.Order_OrderId })
                .ForeignKey("dbo.Markers", t => t.Marker_MarkerId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId, cascadeDelete: true)
                .Index(t => t.Marker_MarkerId)
                .Index(t => t.Order_OrderId);
            
            AddColumn("dbo.Markers", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Orders", "Date", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("dbo.Customers", "FirstName", c => c.String(nullable: false, maxLength: 30));
            AddColumn("dbo.Customers", "Gender", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            AddColumn("dbo.Customers", "Username", c => c.String());
            AddColumn("dbo.Customers", "Password", c => c.String());
            DropColumn("dbo.Customers", "FirtName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "FirtName", c => c.String(nullable: false, maxLength: 30));
            DropForeignKey("dbo.MarkerOrders", "Order_OrderId", "dbo.Orders");
            DropForeignKey("dbo.MarkerOrders", "Marker_MarkerId", "dbo.Markers");
            DropIndex("dbo.MarkerOrders", new[] { "Order_OrderId" });
            DropIndex("dbo.MarkerOrders", new[] { "Marker_MarkerId" });
            DropColumn("dbo.Customers", "Password");
            DropColumn("dbo.Customers", "Username");
            DropColumn("dbo.Customers", "Gender");
            DropColumn("dbo.Customers", "FirstName");
            DropColumn("dbo.Orders", "Date");
            DropColumn("dbo.Markers", "Price");
            DropTable("dbo.MarkerOrders");
            CreateIndex("dbo.Orders", "MarkerId");
            AddForeignKey("dbo.Orders", "MarkerId", "dbo.Markers", "MarkerId", cascadeDelete: true);
        }
    }
}
