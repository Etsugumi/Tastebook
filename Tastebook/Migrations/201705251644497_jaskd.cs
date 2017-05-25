namespace Tastebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jaskd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ImageId = c.Guid(nullable: false, identity: true),
                        UserId = c.String(),
                        Uploaded = c.DateTime(),
                    })
                .PrimaryKey(t => t.ImageId);
            
            CreateTable(
                "dbo.RecipeImages",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        RecipeId = c.Guid(nullable: false),
                        ImageId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RecipeImages");
            DropTable("dbo.Images");
        }
    }
}
