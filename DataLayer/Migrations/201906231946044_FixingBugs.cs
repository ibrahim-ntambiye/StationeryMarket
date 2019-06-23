namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingBugs : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MarkerOrders", "Marker_MarkerId", "dbo.Markers");
            DropForeignKey("dbo.MarkerOrders", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.MarkerOrders", new[] { "Marker_MarkerId" });
            DropIndex("dbo.MarkerOrders", new[] { "Order_OrderId" });
            AddColumn("dbo.Markers", "Order_OrderId", c => c.Int());
            CreateIndex("dbo.Markers", "Order_OrderId");
            AddForeignKey("dbo.Markers", "Order_OrderId", "dbo.Orders", "OrderId");
            DropTable("dbo.MarkerOrders");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.MarkerOrders",
                c => new
                    {
                        Marker_MarkerId = c.Int(nullable: false),
                        Order_OrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Marker_MarkerId, t.Order_OrderId });
            
            DropForeignKey("dbo.Markers", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.Markers", new[] { "Order_OrderId" });
            DropColumn("dbo.Markers", "Order_OrderId");
            CreateIndex("dbo.MarkerOrders", "Order_OrderId");
            CreateIndex("dbo.MarkerOrders", "Marker_MarkerId");
            AddForeignKey("dbo.MarkerOrders", "Order_OrderId", "dbo.Orders", "OrderId", cascadeDelete: true);
            AddForeignKey("dbo.MarkerOrders", "Marker_MarkerId", "dbo.Markers", "MarkerId", cascadeDelete: true);
        }
    }
}
