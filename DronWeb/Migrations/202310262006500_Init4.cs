namespace DronWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init4 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Localizations", "latitude", c => c.Single(nullable: false));
            AlterColumn("dbo.Localizations", "longitude", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Localizations", "longitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Localizations", "latitude", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
