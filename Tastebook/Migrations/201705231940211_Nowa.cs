namespace Tastebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nowa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Ingredients", "RecipeId", "dbo.Recipes");
            DropIndex("dbo.Comments", new[] { "RecipeId" });
            DropIndex("dbo.Ingredients", new[] { "RecipeId" });
            CreateTable(
                "dbo.RecipeCommentMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecipeId = c.Guid(nullable: false),
                        CommentId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecipeIngredientMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecipeId = c.Guid(nullable: false),
                        IngredientId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AlterColumn("dbo.Comments", "Created", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Recipes", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Recipes", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.Recipes", "Created", c => c.DateTime(nullable: false));
            DropColumn("dbo.Comments", "RecipeId");
            DropColumn("dbo.Ingredients", "RecipeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ingredients", "RecipeId", c => c.Guid(nullable: false));
            AddColumn("dbo.Comments", "RecipeId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Recipes", "Created", c => c.DateTime());
            AlterColumn("dbo.Recipes", "Text", c => c.String());
            AlterColumn("dbo.Recipes", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Comments", "Created", c => c.DateTime());
            DropTable("dbo.RecipeIngredientMaps");
            DropTable("dbo.RecipeCommentMaps");
            CreateIndex("dbo.Ingredients", "RecipeId");
            CreateIndex("dbo.Comments", "RecipeId");
            AddForeignKey("dbo.Ingredients", "RecipeId", "dbo.Recipes", "RecipeId", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "RecipeId", "dbo.Recipes", "RecipeId", cascadeDelete: true);
        }
    }
}
