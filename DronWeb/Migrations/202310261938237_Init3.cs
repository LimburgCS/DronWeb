namespace DronWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Localizations", "CoordinatesId", "dbo.Coordinates");
            DropIndex("dbo.Localizations", new[] { "CoordinatesId" });
            AlterColumn("dbo.Localizations", "latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Localizations", "longitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Localizations", "CoordinatesId");
            DropTable("dbo.Coordinates");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Coordinates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        latitude = c.Int(nullable: false),
                        longitude = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Localizations", "CoordinatesId", c => c.Int(nullable: false));
            AlterColumn("dbo.Localizations", "longitude", c => c.Single(nullable: false));
            AlterColumn("dbo.Localizations", "latitude", c => c.Single(nullable: false));
            CreateIndex("dbo.Localizations", "CoordinatesId");
            AddForeignKey("dbo.Localizations", "CoordinatesId", "dbo.Coordinates", "Id", cascadeDelete: true);
        }
    }
}
