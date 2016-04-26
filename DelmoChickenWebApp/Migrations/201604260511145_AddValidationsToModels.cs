namespace DelmoChickenWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidationsToModels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "ProductName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "ProductName", c => c.String());
        }
    }
}
