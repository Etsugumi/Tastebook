namespace Tastebook.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Test : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Recipes", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.Recipes", "Text", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Recipes", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.Recipes", "Name", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
