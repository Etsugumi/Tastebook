namespace Tastebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nowa2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Comments", "Created", c => c.DateTime());
            AlterColumn("dbo.Recipes", "Created", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipes", "Created", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Comments", "Created", c => c.DateTime(nullable: false));
        }
    }
}
