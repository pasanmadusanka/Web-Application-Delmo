namespace DelmoChickenWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Product_Discription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Discription", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Discription");
        }
    }
}
