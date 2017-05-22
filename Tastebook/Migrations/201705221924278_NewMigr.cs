namespace Tastebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewMigr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Guid(nullable: false, identity: true),
                        Created = c.DateTime(nullable: false),
                        Text = c.String(),
                        AuthorName = c.String(),
                        RecipeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        RecipeId = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Text = c.String(nullable: false),
                        Difficulty = c.Int(nullable: false),
                        DishSize = c.Int(nullable: false),
                        Created = c.DateTime(nullable: false),
                        Edited = c.DateTime(nullable: false),
                        RecipeType = c.Int(nullable: false),
                        AuthorId = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.RecipeId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        IngredientId = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Amount = c.String(nullable: false),
                        RecipeId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.IngredientId)
                .ForeignKey("dbo.Recipes", t => t.RecipeId, cascadeDelete: true)
                .Index(t => t.RecipeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingredients", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Comments", "RecipeId", "dbo.Recipes");
            DropIndex("dbo.Ingredients", new[] { "RecipeId" });
            DropIndex("dbo.Comments", new[] { "RecipeId" });
            DropTable("dbo.Ingredients");
            DropTable("dbo.Recipes");
            DropTable("dbo.Comments");
        }
    }
}
