namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Username : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Customers", "Username", c => c.String(nullable: false, maxLength: 30));
            AlterColumn("dbo.Markers", "Colour", c => c.String(maxLength: 30));
            AlterColumn("dbo.Markers", "Brand", c => c.String(maxLength: 30));
            DropColumn("dbo.Customers", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "Password", c => c.String());
            AlterColumn("dbo.Markers", "Brand", c => c.String());
            AlterColumn("dbo.Markers", "Colour", c => c.String());
            AlterColumn("dbo.Customers", "Username", c => c.String());
        }
    }
}
