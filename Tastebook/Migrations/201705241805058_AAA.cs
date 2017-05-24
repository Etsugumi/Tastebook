namespace Tastebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AAA : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recipes", "isCompleted", c => c.Boolean());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipes", "isCompleted", c => c.Boolean(nullable: false));
        }
    }
}
