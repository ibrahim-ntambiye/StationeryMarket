namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingMarkerImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Markers", "ImageFileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Markers", "ImageFileName");
        }
    }
}
