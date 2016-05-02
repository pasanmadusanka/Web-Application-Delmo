namespace DelmoChickenWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageURLPath : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Image", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Image", c => c.String());
        }
    }
}
