namespace Tastebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Created", c => c.DateTime());
            AlterColumn("dbo.Recipes", "Created", c => c.DateTime());
            AlterColumn("dbo.Recipes", "Edited", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipes", "Edited", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Recipes", "Created", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Comments", "Created", c => c.DateTime(nullable: false));
        }
    }
}
