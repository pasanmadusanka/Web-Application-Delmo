namespace DelmoChickenWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePostsModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Content", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "ImagePath", c => c.String(maxLength: 128));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "ImagePath", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Content", c => c.String());
        }
    }
}
