namespace Tastebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OOO : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Recipes", "isCompleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Recipes", "isCompleted");
        }
    }
}
