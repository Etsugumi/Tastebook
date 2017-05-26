namespace Tastebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test231 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.RecipeImages", newName: "RecipeImageMaps");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.RecipeImageMaps", newName: "RecipeImages");
        }
    }
}
